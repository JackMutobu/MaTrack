using Microsoft.AspNetCore.SignalR.Client;

namespace MaTrack.Shared.Services
{
    public interface ISignalRClientService
    {
        HubConnection HubConnection { get; set; }
    }
}
