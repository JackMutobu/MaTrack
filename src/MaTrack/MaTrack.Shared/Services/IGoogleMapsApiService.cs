using MaTrack.Shared.Models;
using System.Threading.Tasks;

namespace MaTrack.Shared.Services
{
    public interface IGoogleMapsApiService
    {
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
    }
}
