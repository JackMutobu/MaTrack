using System.Linq;
using MaTrack.Core.Entities;
using MaTrack.Shared.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DriversController : BaseController<DriverEntity>
    {
        public DriversController(IDriverRepository driverRepository):base(driverRepository)
        {
        }
        [HttpGet("getbyphone/{phone}")]
        public IActionResult GetByPhone(string phone)
        {
            var driver = _repository.GetAll().SingleOrDefault(a => a.Phone == phone);
            return Ok(driver);
        }
       
       
    }
}
