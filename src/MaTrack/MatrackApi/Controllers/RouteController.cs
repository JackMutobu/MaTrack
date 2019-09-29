using System.Threading.Tasks;
using MaTrack.Core.Entities;
using MaTrack.Shared.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Route("api/[controller]")]
    public class RouteController : BaseController<RouteEntity>
    {
        public RouteController(IRouteRepository routeRepository):base(routeRepository)
        {

        }
        [HttpPost("add")]
        public async override Task<IActionResult> Add([FromBody] RouteEntity entity)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(entity);
                return Ok("Registered");
            }
            return BadRequest(ModelState);
        }
    }
} 
