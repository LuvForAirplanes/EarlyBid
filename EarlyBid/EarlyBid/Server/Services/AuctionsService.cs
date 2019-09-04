using EarlyBid.Server.Data;
using EarlyBid.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarlyBid.Server.Services
{
    public class AuctionsService : Repository<Auction, string>
    {
        public AuctionsService(ApplicationDbContext context) : base(context) { }

        public async Task<Auction> CurrentAuctionAsync()
        {
            return await context.Auction.FirstOrDefaultAsync(a => a.IsActive);
        }

        public override async Task<List<Auction>> ListAsync()
        {
            return (await base.ListAsync()).OrderBy(a => a.Created).ToList();
        }
    } 
}
