using MaTrack.Core.Dtos;
using MaTrack.Shared.Models;
using System.Collections.Generic;

namespace MaTrack.Shared.Services
{
    public class PlacesService:IPlacesService
    {
        public IEnumerable<MatrackPlace> GetMatrackPlaces()
        {
            var listPlaces = new List<MatrackPlace>()
            {
                new MatrackPlace
                {
                    Name = "Strathmore University",
                    LocationDto = new LocationDto
                    {
                        Latitude = "-1.309331",
                        Longitude ="36.812798"
                    }
                },
                new MatrackPlace
                {
                    Name = "Nyayo Stadium",
                    LocationDto = new LocationDto
                    {
                        Latitude = "-1.304254",
                        Longitude = "36.824730"
                    }
                },
                new MatrackPlace
                {
                    Name = "Afya Center",
                    LocationDto = new LocationDto
                    {
                        Latitude = "-1.287495",
                        Longitude ="36.828649"
                    }
                },
                new MatrackPlace
                {
                    Name = "Nairobi Railways",
                    LocationDto = new LocationDto
                    {
                        Latitude = "-1.290143",
                        Longitude = "36.826944"
                    }
                },
                new MatrackPlace
                {
                    Name = "Kenyatta University - CBD Campus",
                    LocationDto = new LocationDto
                    {
                        Latitude = "-1.291132",
                        Longitude = "36.823402"
                    }
                },
                new MatrackPlace
                {
                    Name = "Nairobi CBD",
                    LocationDto = new LocationDto
                    {
                        Latitude = "-1.292066",
                        Longitude = "36.821945"
                    }
                },
            };
            return listPlaces;
        }
    }
    public interface IPlacesService
    {
        IEnumerable<MatrackPlace> GetMatrackPlaces();
    }
}
