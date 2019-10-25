using System.Linq;
using System.Threading.Tasks;
using MaTrack.Core.Entities;
using MaTrack.Shared.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Route("api/[controller]")]
    public class RouteController : BaseController<RouteEntity>
    {
        private IStageRepository _stageRepository;

        public RouteController(IRouteRepository routeRepository,IStageRepository stageRepository):base(routeRepository)
        {
            _stageRepository = stageRepository;
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
        [HttpGet("getRouteWithStage/{stageId}")]
        public IActionResult GetRouteWithStage(int stageId)
        {
            var stage = _stageRepository.GetById(stageId, s => s.Include(t => t.RouteStages));
            return Ok(stage);
        }
    }
} 
