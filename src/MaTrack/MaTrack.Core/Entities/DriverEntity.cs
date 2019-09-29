using MaTrack.Core.Enumerations;

namespace MaTrack.Core.Entities
{
    public class DriverEntity:BasePersonEntity
    {
        public EnumStatus DriverStatus { get; set; }
        public int? VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
