using EarlyBid.Server.Data;
using EarlyBid.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarlyBid.Server.Services
{
    public class BidService : Repository<Bid, string>
    {
        public BidService(ApplicationDbContext context) : base(context) { }

        public async Task<List<Bid>> GetBidsForAuctionAsync(string auctionId) 
        {
            return await context.Bids.Where(b => b.AuctionId == auctionId).ToListAsync();
        }
    }
}
