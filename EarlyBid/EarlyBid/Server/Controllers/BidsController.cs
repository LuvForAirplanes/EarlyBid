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
    public class BidsController : ControllerBase
    {
        private readonly ILogger<BidsController> logger;
        private readonly BidService bidService;
        private readonly AuctionsService auctionsService;

        public BidsController(ILogger<BidsController> logger, BidService bidService, AuctionsService auctionsService)
        {
            this.logger = logger;
            this.bidService = bidService;
            this.auctionsService = auctionsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await bidService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var currentAuction = await auctionsService.CurrentAuctionAsync();

            return Ok(await bidService.GetBidsForAuctionAsync(currentAuction.Id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Bid bid)
        {
            return Ok(await bidService.CreateAsync(bid));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody]Bid bid)
        {
            return Ok(await bidService.UpdateAsync(bid, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await bidService.DeleteAsync(id);
            return Ok("Deleted Successfully!");
        }
    }
}
