using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarlyBid.Shared.Models
{
    public class Bid : BaseModel
    {
        public Auction Auction { get; set; }

        public string AuctionId { get; set; }

        public string BidderNumber { get; set; }

        public string Description { get; set; }

        public string StickerNumber { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal EndPrice { get; set; }
    }
}
