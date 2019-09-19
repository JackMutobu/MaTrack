using System.Collections.Generic;

namespace MaTrack.Core.Entities
{
    public class StageEntity:BaseEntity
    {
        public string Name { get; set; }
        public string GPSCoordinate { get; set; }
        public ICollection<RouteStageEntity> RouteStages { get; set; }
    }
}
