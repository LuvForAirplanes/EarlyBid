using EarlyBid.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EarlyBid.Server.Services;
using EarlyBid.Shared.Models;
using EarlyBid.Shared.ViewModels;

namespace EarlyBid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly ILogger<BidsController> logger;
        private readonly BidService bidService;
        private readonly AuctionsService auctionsService;
        private readonly BidEditMapper bidEditMapper;

        public BidsController(ILogger<BidsController> logger, BidService bidService, AuctionsService auctionsService, BidEditMapper bidEditMapper)
        {
            this.logger = logger;
            this.bidService = bidService;
            this.auctionsService = auctionsService;
            this.bidEditMapper = bidEditMapper;
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

            var bids = await bidService.GetBidsForAuctionAsync(currentAuction.Id);
            var mappedList = new List<BidEdit>();

            foreach (var bid in bids)
                mappedList.Add(bidEditMapper.Map(bid));

            return Ok(mappedList);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Bid bid)
        {
            var auction = await auctionsService.CurrentAuctionAsync();

            bid.AuctionId = auction.Id;
            bid.Created = DateTime.Now;
            return Ok(await bidService.CreateAsync(bid));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody]Bid bid)
        {
            bid.Updated = DateTime.Now;
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
