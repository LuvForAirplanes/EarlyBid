﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using MSUDTrack.Services;

namespace EarlyBid.Server
{
    public class Program
    {
        private static IWebHost WebHostInstance;
        public static void Main(string[] args)
        {
            try
            {
                var isService = !(Debugger.IsAttached || args.Contains("--console") || args.Contains("-c"));
                var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console" && arg != "-c").ToArray());

                if (isService)
                {
                    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                    builder.UseContentRoot(pathToContentRoot);
                }

                WebHostInstance = builder.Build();

                var scope = WebHostInstance.Services.CreateScope();

                var seedDataService = scope.ServiceProvider.GetService<SeedDataService>();
                seedDataService.SeedPeriodsAsync(); //Don't hold up the startup process. Seeding will catch up later.

                if (isService && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WebHostInstance.RunAsService();
                else
                    WebHostInstance.Run();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:32981")
                .UseStartup<Startup>();
    }
}
