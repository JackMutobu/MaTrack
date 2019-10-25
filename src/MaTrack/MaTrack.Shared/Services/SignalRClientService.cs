using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MaTrack.Shared.Services
{
    public class SignalRClientService:ISignalRClientService
    {
        public SignalRClientService()
        {
            var localUri = new Uri("https://localhost:44389/trackHub/");
            var onlineUri = new Uri("http://afrisofttech-001-site31.btempurl.com/trackHub/");
            var androidEmulator = new Uri("http://127.0.0.1:6950/api/");
            var localNetwork = new Uri("https://192.168.100.6:44389/trackHub/");
            HubConnection = new HubConnectionBuilder()
                .WithUrl(onlineUri, options=> 
                {
                    options.HttpMessageHandlerFactory = (handler) =>
                    {
                        if (handler is HttpClientHandler clientHandler)
                        {
                            clientHandler.ServerCertificateCustomValidationCallback = ValidateCertificate;
                        }
                        return handler;
                    };
                })
                .Build();
            HubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await HubConnection.StartAsync();
            };
        }

        bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // TODO: You can do custom validation here, or just return true to always accept the certificate.
            // DO NOT use custom validation logic in a production application as it is insecure.
            return true;
        }
        public HubConnection HubConnection { get; set; }
    }
}
