using System.IO;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Stock_Management.Hubs
{
    public class SignalrServer : Hub
    {
        public Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}