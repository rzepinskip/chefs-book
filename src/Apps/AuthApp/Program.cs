﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChefsBook.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChefsBook.AuthApp
{
    public class Program
    {
        private const int Port = 5000;

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(cfg => 
                {
                    cfg.Listen(IPAddress.Any, Port, c => c.UseHttps(Cert.Load()));
                })
                .UseIISIntegration()
                .UseUrls($"https://*:{Port}")
                .UseStartup<Startup>()
                .Build();
    }
}
