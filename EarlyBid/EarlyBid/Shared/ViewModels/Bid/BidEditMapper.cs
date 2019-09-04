using EarlyBid.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EarlyBid.Shared.ViewModels
{
    public class BidEditMapper
    {
        public BidEditMapper() { }

        public BidEdit Map(Bid existing)
        {
            return new BidEdit()
            {
                Id = existing.Id,
                AuctionId = existing.AuctionId,
                BidderNumber = existing.BidderNumber,
                Created = existing.Created,
                EndPrice = existing.EndPrice,
                StartingPrice = existing.StartingPrice,
                StickerNumber = existing.StickerNumber,
                Updated = existing.Updated
            };
        }

        public Bid Map(BidEdit existing)
        {
            return new Bid()
            {
                Id = existing.Id,
                AuctionId = existing.AuctionId,
                BidderNumber = existing.BidderNumber,
                Created = existing.Created,
                EndPrice = existing.EndPrice,
                StartingPrice = existing.StartingPrice,
                StickerNumber = existing.StickerNumber,
                Updated = existing.Updated
            };
        }
    }
}
