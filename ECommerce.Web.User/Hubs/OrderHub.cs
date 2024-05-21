using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace ECommerce.Web.User.Hubs
{
    public class OrderHub : Hub
    {
        
            public override Task OnConnectedAsync()
            {
                var userId = Context.UserIdentifier;
                if (Context.User.IsInRole("Admin"))
                {
                    Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
                }
            if (Context.User.IsInRole("User"))

            {
                // Assuming userId is the order id for simplicity, adjust as needed
                Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
                return base.OnConnectedAsync();
            }
       

        //public async Task JoinGroup(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");

        //}

        //public async Task LeaveGroup(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");

        //}
        //public override Task OnConnectedAsync()
        //{
        //    var userId = Context.UserIdentifier;
        //    if (userId != null)
        //    {
        //        Groups.AddToGroupAsync(Context.ConnectionId, userId);
        //    }
        //    return base.OnConnectedAsync();
        //}
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        //public async Task SendNotificationtoGroup(string groupName, string message)
        //{
        //    await Clients.Group(groupName).SendAsync("NewOrder", message);
        //}


    }
}


