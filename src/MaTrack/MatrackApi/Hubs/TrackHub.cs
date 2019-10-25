using MaTrack.Core.Dtos;
using MaTrack.Shared.Data.Repositories;
using MatrackApi.Data.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MaTrack.Core.Helpers.GpsHelpers;

namespace MatrackApi.Hubs
{
    public class TrackHub:Hub
    {
        private List<TrackedDriverDto> _trackedDrivers;
        private IDriverRepository _driverRepository;
        private ITripRepository _tripRepository;
        public TrackHub(IDriverRepository driverRepository,ITripRepository tripRepository)
        {
            _trackedDrivers = new List<TrackedDriverDto>();
            _driverRepository = driverRepository;
            _tripRepository = tripRepository;

        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task GetLocation(LocationDto from,LocationDto to)
        {
            var destinationStage = "Nairobi CBD";
            await Clients.Group(destinationStage).SendAsync("ReceiveLocation", to,Context.ConnectionId);
        }
        public async Task ReceiveLocation(UserAuthDto userAuthDto,LocationDto location)
        {
            var driverInDb = _driverRepository.GetAll().SingleOrDefault(d => d.Phone == userAuthDto.Phone);
            var trip = _tripRepository.GetAll().FirstOrDefault(t => t.VehicleId == driverInDb.VehicleId && t.UploadDate == DateTime.Today);
            if(trip?.TripState == MaTrack.Core.Enumerations.TripState.Arrival)
            {
                var driver = _trackedDrivers.Select(x => x.UserAuthDto).Where(y => y.Phone == userAuthDto.Phone);
                if (driver != null)
                {
                    foreach (var trackedDriver in _trackedDrivers)
                    {
                        if (trackedDriver.UserAuthDto.Phone == userAuthDto.Phone)
                        {
                            _trackedDrivers.Remove(trackedDriver);
                            trackedDriver.Location = location;
                            trackedDriver.Destination = new LocationDto("-1.309331", "36.812798");
                            _trackedDrivers.Add(trackedDriver);
                            break;
                        }
                    }
                }
                else
                {
                    var trackDriver = new TrackedDriverDto
                    {
                        Location = location,
                        UserAuthDto = userAuthDto
                    };
                    _trackedDrivers.Add(trackDriver);
                }
            }
            
            var selectedDriver = _driverRepository.GetAll().FirstOrDefault(x => x.Phone == userAuthDto.Phone);
            await Clients.All.SendAsync("ReceiveLocation", selectedDriver, location);
        }

        public async Task SendLocation(RequestMatatuResponse requestMatatuResponse,string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("RequestResponse", requestMatatuResponse);
        }
        public double GetDistance(LocationDto from, LocationDto to)
        {
            var distance = from.DistanceTo(to, UnitOfLength.Kilometers);
            return distance;    
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }
    }
}
