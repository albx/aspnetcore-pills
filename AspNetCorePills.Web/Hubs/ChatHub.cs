using Microsoft.AspNetCore.SignalR;

namespace AspNetCorePills.Web.Hubs;

public class ChatHub : Hub<ChatHub.IChatClient>
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.ReceiveMessage(user, message);
    }

    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
}
