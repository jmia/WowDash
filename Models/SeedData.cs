using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using wow_dashboard.Data;

namespace wow_dashboard.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {

                if (context.Characters.Any())
                {
                    return;   // DB has been seeded
                }

                context.Characters.AddRange(
                    new Character
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
                        }
                    },
                    new Character
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
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
