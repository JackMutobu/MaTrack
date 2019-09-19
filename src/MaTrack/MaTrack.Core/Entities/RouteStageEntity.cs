using MaTrack.Core.Enumerations;

namespace MaTrack.Core.Entities
{
    public class RouteStageEntity
    {
        public int RouteId { get; set; }
        public RouteEntity Route { get; set; }
        public int StageId { get; set; }
        public StageEntity Stage { get; set; }
        public EnumStagePosition StagePosition { get; set; }
    }
}
