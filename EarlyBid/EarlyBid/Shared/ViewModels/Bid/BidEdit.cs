using EarlyBid.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EarlyBid.Shared.ViewModels
{
    public class BidEdit : Bid
    {
        public bool IsChanged { get; set; }
    }
}
