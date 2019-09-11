using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarlyBid.Server.Hubs
{
    public class AuctionHub : Hub
    {
        public async Task ReceivedBid()
        {
            await Clients.All.SendAsync("ReceivedBid");
        }
    }
}
