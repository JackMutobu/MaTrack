using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IUserRepository _userRepository;

        public MediaController(IHostingEnvironment hostingEnvironment,IUserRepository userRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _userRepository = userRepository;
        }
        [HttpPost("uploadfile/{phone}"), DisableRequestSizeLimit]
        public async virtual Task<IActionResult> FileUpload(string phone, [FromForm]UploadFile model)
        {
            var entity =  _userRepository.GetAll().SingleOrDefault(u=> u.Phone == phone);
            if (entity == null)
            {
                return BadRequest(new { Message = "User not found" });
            }
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            try
            {
                if (model.File != null)
                {
                    string folderName = $"Files/Images/{entity.Id}";
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    if (model.File.Length > 0)
                    {
                        string fileName = Path.GetFileName(model.File.FileName);
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            model.File.CopyTo(stream);                           
                            using (var memStream = new System.IO.MemoryStream())
                            {
                                model.File.CopyTo(memStream);
                            }
                        }
                        entity.ProfileLink = $"{folderName}/{fileName}";
                        await _userRepository.UpdateAsync(entity);
                    }
                    return Ok(entity);
                }
                return BadRequest(new { Message = "files can not be empty" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet("getpath")]
        public virtual IActionResult GetRootPath()
        {
            var webRoot = _hostingEnvironment.WebRootPath;
            return Ok(webRoot);

        }
        [HttpGet("getfile"), DisableRequestSizeLimit]
        public async Task<IActionResult> GetFile([FromQuery]int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            try
            {

                string curentDirectory = _hostingEnvironment.WebRootPath;
                var stream = new FileStream($"{curentDirectory}/{user.ProfileLink}", FileMode.OpenOrCreate);
                if (stream == null)
                    return NotFound("File not found");
                return File(stream, "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }
    }
}
