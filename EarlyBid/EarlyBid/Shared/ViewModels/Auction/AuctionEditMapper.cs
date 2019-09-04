using EarlyBid.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EarlyBid.Shared.ViewModels
{
    public class AuctionEditMapper
    {
        public AuctionEdit Map(Auction auction)
        {
            return new AuctionEdit()
            {
                Id = auction.Id,
                Created = auction.Created,
                IsActive = auction.IsActive,
                Updated = auction.Updated
            };
        }

        public Auction Map(AuctionEdit auction)
        {
            return new Auction()
            {
                Id = auction.Id,
                Created = auction.Created ,
                IsActive = auction.IsActive,
                Updated = auction.Updated
            };
        }
    }
}
