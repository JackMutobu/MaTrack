
namespace MaTrack.Core.Dtos
{
    public class LocationDto
    {
        public LocationDto()
        {

        }
        public LocationDto(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public string Altitude { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Speed { get; set; }

        
    }
}
