using Microsoft.AspNetCore.SignalR;

namespace WebManagement.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendUpdateNotification()
        {
            await Clients.Others.SendAsync("ReceiveUpdate");
        }
    }
}