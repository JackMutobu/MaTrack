using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BaseController<T> : Controller where T : BaseEntity
    {
        protected IBaseRepository<T> _repository;
        public BaseController(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        [HttpGet("all")]
        public virtual IActionResult GetAll()
        {
            var entities = _repository.GetAll();
            return Ok(entities);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return Ok(entity);
        }
        [HttpPost("add")]
        public async virtual Task<IActionResult> Add([FromBody]T entity)
        { 
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(entity);
                return Ok(entity);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("update")]
        public virtual async Task<IActionResult> Update([FromBody]T entity)
        {
            if (ModelState.IsValid)
            {
                var entityExist = _repository.GetById(entity.Id, null);
                if (entityExist != null)
                {
                    entityExist = entity;
                    await _repository.UpdateAsync(entityExist);
                    return Ok(entityExist);
                }
                return NotFound("Entity not found");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("delete")]
        public virtual async Task<IActionResult> Delete([FromBody]T entity)
        {
            if (ModelState.IsValid)
            {
                var entityExist = await _repository.GetByIdAsync(entity.Id);
                if (entityExist != null)
                {
                    await _repository.DeleteAsync(entityExist);
                    return Ok(entityExist);
                }
                return NotFound("Entity not found");
            }
            return BadRequest(ModelState);
        }
    }
}
