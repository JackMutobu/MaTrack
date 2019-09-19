namespace MaTrack.Core.Entities
{
    public class VehicleRouteEntity
    {
        public int RouteId { get; set; }
        public RouteEntity Route { get; set; }
        public int VeicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
