using MaTrack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaTrack.Core.Dtos
{
    public class RequestMatatuResponse
    {
        public DriverEntity Driver { get; set; }
        public VehicleEntity Vehicle { get; set; }
        public double Distance { get; set; }
        public int EstimatedTime { get; set; }
        public LocationDto LocationDto { get; set; }
    }
}
