using System.Collections.Generic;
using MaTrack.Core.Entities;
using MaTrack.Shared.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Route("api/[controller]")]
    public class VehiclesController : BaseController<VehicleEntity>
    {
        public VehiclesController(IVehicleRepository vehicleRepository):base(vehicleRepository)
        {

        }
    }
}
