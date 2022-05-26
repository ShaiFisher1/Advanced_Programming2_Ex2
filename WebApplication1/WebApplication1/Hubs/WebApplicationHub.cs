using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public class WebApplicationHub : Hub
    {
        public async Task connect(string username)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, username);

        }
    }
}
