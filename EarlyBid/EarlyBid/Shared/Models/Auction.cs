using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarlyBid.Shared.Models
{
    public class Auction : BaseModel 
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }    
    }
}
