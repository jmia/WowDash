using Microsoft.EntityFrameworkCore;
using System;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.Tests.Common
{
    public class ApplicationContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("WowDash-Testing-" + Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            var defaultUser = new Player { DisplayName = "Jen", DefaultRealm = "area-52" };

            var scully = new Character
            {
                Name = "Scully",
                Gender = CharacterGender.Female,
                Realm = "area-52",
                Class = "Hunter",
                Race = "Blood Elf",
                Level = 120,
                PlayerId = defaultUser.Id
            };

            var chakwas = new Character
            {
                Name = "Chakwas",
                Gender = CharacterGender.Female,
                Realm = "area-52",
                Class = "Druid",
                Race = "Highmountain Tauren",
                Level = 120,
                PlayerId = defaultUser.Id
            };

            context.Players.Add(defaultUser);

            context.Characters.AddRange(new[] { scully, chakwas });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
