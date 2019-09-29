using MaTrack.Core.Enumerations;
using System.Collections.Generic;

namespace MaTrack.Core.Entities
{
    public class VehicleEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string NumPlate { get; set; }
        public EnumStatus VehicleStatus { get; set; }
        public DriverEntity Driver { get; set; }
        public ICollection<VehicleRouteEntity> VehicleRoutes { get; set; }
    }
}
