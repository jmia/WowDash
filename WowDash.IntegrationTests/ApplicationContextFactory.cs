using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.UnitTests.Common
{
    public class ApplicationContextFactory      // How does this work with Integration Testing?
    {
        //public static ApplicationDbContext Create()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", true, true)
        //        .AddEnvironmentVariables();

        //    var configuration = builder.Build();

        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseSqlServer(configuration.GetConnectionString("DefaultConnection")).Options;

        //    var context = new ApplicationDbContext(options);

        //    return context;
        //}

        //public static void Destroy(ApplicationDbContext context)
        //{
        //    context.Database.EnsureDeleted();

        //    context.Dispose();
        //}
    }
}
