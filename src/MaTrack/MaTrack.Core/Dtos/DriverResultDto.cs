using MaTrack.Core.Entities;

namespace MaTrack.Core.Dtos
{
    public class DriverResultDto
    {
        public DriverEntity Driver { get; set; }
        public LocationDto Location { get; set; }
    }
}
