using MaTrack.Core.Enumerations;
using System;

namespace MaTrack.Core.Entities
{
    public class TripEntity : BaseEntity
    {
        private DateTime _lastTripStateTime;
        public TripState TripState { get; set; }
        public DateTime LastTripStateTime {
            get { return _lastTripStateTime; }
            set
            {
                _lastTripStateTime = value;
                ShortTime = _lastTripStateTime.ToShortTimeString();
            }
        }
        public string ShortTime { get; set; }
        public byte NumberOfTrip { get; set; }
        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; } 
        public decimal Revenue { get; set; }
        public int? DepartureStageId { get; set; }
        public StageEntity DepartureStage { get; set; }
    }
}
