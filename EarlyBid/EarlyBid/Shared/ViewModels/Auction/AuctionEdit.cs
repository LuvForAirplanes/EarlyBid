using EarlyBid.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EarlyBid.Shared.ViewModels
{
    public class AuctionEdit : Auction
    {
        public bool IsChanged { get; set; }
    }
}
