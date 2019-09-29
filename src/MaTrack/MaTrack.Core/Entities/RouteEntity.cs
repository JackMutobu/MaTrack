using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MaTrack.Core.Entities
{
    public class RouteEntity:BaseEntity
    {
        public RouteEntity()
        {
            RouteStages = new Collection<RouteStageEntity>();
            VehicleRoutes = new Collection<VehicleRouteEntity>();
        }
        public string RouteName { get; set; }
        public ICollection<RouteStageEntity> RouteStages { get; set; }
        public ICollection<VehicleRouteEntity> VehicleRoutes { get; set; }
    }
}
