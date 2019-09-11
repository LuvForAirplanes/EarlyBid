using EarlyBid.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EarlyBid.Server.Services;
using EarlyBid.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using EarlyBid.Server.Hubs;

namespace EarlyBid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionsController : ControllerBase
    {
        private readonly ILogger<BidsController> logger;
        private readonly BidService bidService;
        private readonly AuctionsService auctionsService;

        public AuctionsController(ILogger<BidsController> logger, BidService bidService, AuctionsService auctionsService)
        {
            this.logger = logger;
            this.bidService = bidService;
            this.auctionsService = auctionsService;
        }

        [HttpGet("{auctionId}")]
        public async Task<IActionResult> GetAsync(string auctionId)
        {
            var currentAuction = await auctionsService.CurrentAuctionAsync();

            if (string.IsNullOrEmpty(auctionId))
                if (currentAuction != null)
                {
                    return Ok(await bidService.GetBidsForAuctionAsync(currentAuction.Id));
                }
                else
                    return Ok("0101: No active auction!");
            else
                return Ok(await bidService.GetBidsForAuctionAsync(auctionId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await auctionsService.ListAsync());
        }

        [HttpPost("active/{auctionId}")]
        public async Task<IActionResult> PostActiveAuctionAsync(string auctionId)
        {
            await SetAuctionActiveAsync(auctionId);
            return Ok(await auctionsService.ListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Auction auction)
        {
            var create = await auctionsService.CreateAsync(auction);
            DispatchData();
            return Ok(create);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody]Auction auction)
        {
            auction.Updated = DateTime.Now;
            var updateed = await auctionsService.UpdateAsync(auction, id);
            DispatchData();
            return Ok(updateed);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await auctionsService.DeleteAsync(id);

            var first = await auctionsService.Get().OrderByDescending(a => a.Created).FirstOrDefaultAsync();
            await SetAuctionActiveAsync(first.Id);

            return Ok("Deleted Successfully!");
        }

        public async Task SetAuctionActiveAsync(string id)
        {
            var auctions = await auctionsService.ListAsync();
            foreach (var auction in auctions)
            {
                auction.IsActive = false;
                await auctionsService.UpdateAsync(auction, auction.Id);
            }

            var activeAuction = await auctionsService.GetByIdAsync(id);
            activeAuction.IsActive = true;
            await auctionsService.UpdateAsync(activeAuction, activeAuction.Id);

            DispatchData();
        }

        public void DispatchData()
        {
            //var scope = Program.WebHostInstance.Services.CreateScope();
            //var hubContext = scope.ServiceProvider.GetService<IHubContext<AuctionHub>>();
            //hubContext.Clients.All.SendAsync("ReceivedBid", "", "").Wait();
        }
    }
}
