using System;
using System.Collections.Generic;
using MaTrack.Core.Entities;
using MaTrack.Shared.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Route("api/[controller]")]
    public class VehiclesController : BaseController<VehicleEntity>
    {
        public VehiclesController(IVehicleRepository vehicleRepository):base(vehicleRepository)
        {

        }
        [HttpGet("getallwith")]
        public IActionResult GetAllWith()
        {
            try
            {
                var vehicles = _repository.GetAll();
                var vehiclesWithIncude = new List<VehicleEntity>();
                foreach (var vehicle in vehicles)
                {
                    var vehicleWithIncude = _repository.GetById(vehicle.Id,v => v.Include(d => d.Driver));
                    vehiclesWithIncude.Add(vehicleWithIncude);
                }
                return Ok(vehiclesWithIncude);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
