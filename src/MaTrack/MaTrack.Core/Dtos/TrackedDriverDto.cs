using MaTrack.Core.Entities;
using System.Collections.Generic;

namespace MaTrack.Core.Dtos
{
    public class TrackedDriverDto
    {
        public UserAuthDto UserAuthDto { get; set; }
        public LocationDto Location { get; set; }
        public LocationDto Destination { get; set; }
    }
}
