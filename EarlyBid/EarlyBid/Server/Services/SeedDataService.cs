using EarlyBid.Server.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSUDTrack.Services
{
    public class SeedDataService
    {
        public readonly ApplicationDbContext _trackerDbContext;

        public SeedDataService(ApplicationDbContext trackerDbContext)
        {
            _trackerDbContext = trackerDbContext;
        }

        public void SeedPeriodsAsync()
        {
            _trackerDbContext.Database.Migrate();
        }
    }
}
