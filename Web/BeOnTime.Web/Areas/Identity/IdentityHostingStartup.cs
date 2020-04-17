using System;
using AspNetCoreTemplate.Data.Models;
using BeOnTime.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BeOnTime.Web.Areas.Identity.IdentityHostingStartup))]
namespace BeOnTime.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BeOnTimeWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BeOnTimeWebContextConnection")));
            });
        }
    }
}