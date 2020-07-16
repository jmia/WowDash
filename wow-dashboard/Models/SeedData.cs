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
                Guid defaultUser;

                if (!context.Users.Any())
                {
                    var result = context.Users.Add(
                        new User()
                        {
                            DisplayName = "Jen"
                        }
                    );
                    defaultUser = result.Entity.Id;
                } else
                {
                    defaultUser = context.Users.FirstOrDefault().Id;
                }

                if (!context.Characters.Any())
                {
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
                            },
                            UserId = defaultUser
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
                            },
                            UserId = defaultUser
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
