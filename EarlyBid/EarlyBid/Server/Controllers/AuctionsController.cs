using EarlyBid.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EarlyBid.Server.Services;
using EarlyBid.Shared.Models;

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
            var auctions = await auctionsService.ListAsync();
            foreach (var auction in auctions)
            {
                auction.IsActive = false;
                await auctionsService.UpdateAsync(auction, auction.Id);
            }

            var activeAuction = await auctionsService.GetByIdAsync(auctionId);
            activeAuction.IsActive = true;
            await auctionsService.UpdateAsync(activeAuction, activeAuction.Id);

            return Ok(await auctionsService.ListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Auction auction)
        {
            return Ok(await auctionsService.CreateAsync(auction));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody]Auction auction)
        {
            return Ok(await auctionsService.UpdateAsync(auction, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await auctionsService.DeleteAsync(id);
            return Ok("Deleted Successfully!");
        }
    }
}
