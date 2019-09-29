using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AdminController : BaseController<AdminEntity>
    {
        public AdminController(IAdminRepository adminRepository):base(adminRepository)
        {
        }
       
        [HttpGet("getbyphone/{phone}")]
        public IActionResult GetByPhone(string phone)
        {
            var admin = _repository.GetAll().SingleOrDefault(a=> a.Phone == phone);
            return Ok(admin);
        }
        [AllowAnonymous]
        [HttpPost("add")]
        public override async Task<IActionResult> Add([FromBody]AdminEntity admin)
        {
            if(ModelState.IsValid)
            {
                await _repository.AddAsync(admin);
                return Ok(new
                {
                    Id = admin.Id,
                    Phone = admin.Phone,
                    FirstName = admin.Firstname,
                    LastName = admin.Lastname,
                });
            }
            return BadRequest(ModelState);
        }
        
    }
}
