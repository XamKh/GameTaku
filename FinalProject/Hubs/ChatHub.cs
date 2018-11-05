using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Hubs
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage(FinalProject.Models.ChatMessage message)
        {
            await Clients.All.SendCoreAsync("RecieveMessage", new object[] { message });
        }
    }
}
