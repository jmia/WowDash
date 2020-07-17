using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using wow_dashboard.Data;
using wow_dashboard.Models;

namespace wow_dashboard_tests.Common
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

			var defaultUser = new User() { DisplayName = "Jen", DefaultRealm = "area-52" };

			var scully = new Character
			{
				Name = "Scully",
				Gender = CharacterGender.Female,
				Realm = "area-52",
				Class = PlayableClass.Hunter,
				Professions = new List<Profession>()
					{
					Profession.Leatherworking,
					Profession.Skinning,
					Profession.Cooking,
					Profession.Fishing,
					Profession.Archaeology
					},
				UserId = defaultUser.Id
			};

			var chakwas = new Character
			{
				Name = "Chakwas",
				Gender = CharacterGender.Female,
				Realm = "area-52",
				Class = PlayableClass.Druid,
				Professions = new List<Profession>()
					{
						Profession.Inscription,
						Profession.Herbalism,
						Profession.Fishing
					},
				UserId = defaultUser.Id
			};

			context.Users.Add(defaultUser);

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
