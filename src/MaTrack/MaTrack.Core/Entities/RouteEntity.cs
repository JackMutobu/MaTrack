using System.Collections.Generic;

namespace MaTrack.Core.Entities
{
    public class RouteEntity:BaseEntity
    {
        public string RouteName { get; set; }
        public ICollection<RouteStageEntity> RouteStages { get; set; }
        public ICollection<VehicleRouteEntity> VehicleRoutes { get; set; }
    }
}
