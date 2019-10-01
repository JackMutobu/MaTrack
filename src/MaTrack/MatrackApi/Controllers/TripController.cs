using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using MatrackApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Route("api/[controller]")]
    public class TripController : BaseController<TripEntity>
    {
        public TripController(ITripRepository repository) : base(repository)
        {

        }
        [HttpGet("getallwith")]
        public IActionResult GetAllWith()
        {
            try
            {
                var trips = _repository.GetAll();
                var tripsWithIncude = new List<TripEntity>();
                foreach (var trip in trips)
                {
                    var tripWithInclude = _repository.GetById(trip.Id, t => t.Include(s => s.Vehicle).ThenInclude(v => v.Driver));
                    tripsWithIncude.Add(tripWithInclude);
                }
                return Ok(tripsWithIncude);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
