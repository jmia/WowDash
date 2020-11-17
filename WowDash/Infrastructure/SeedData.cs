using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>());

            Guid defaultUser;

            if (!context.Players.Any())
            {
                var result = context.Players.Add(
                    new Player() { Id = Guid.Parse("d8a57467-008e-4ebb-286a-08d86586cf0f"), DisplayName = "Jen" });

                defaultUser = result.Entity.Id;
            }
            else
                defaultUser = context.Players.FirstOrDefault().Id;

            if (!context.Characters.Any())
            {
                context.Characters.AddRange(
                    new Character
                    {
                        Name = "Scully",
                        Gender = CharacterGender.Female,
                        Realm = "area-52",
                        Class = "Hunter",
                        Race = "Blood Elf",
                        Level = 120,
                        PlayerId = defaultUser
                    },
                    new Character
                    {
                        Name = "Chakwas",
                        Gender = CharacterGender.Female,
                        Realm = "area-52",
                        Class = "Druid",
                        Race = "Highmountain Tauren",
                        Level = 120,
                        PlayerId = defaultUser
                    });
            }

            context.SaveChanges();
        }
    }
}
