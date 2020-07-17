using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using wow_dashboard.Utilities;

namespace wow_dashboard.Models
{
    /// <summary>
    /// Represents a playable character in a user's roster.
    /// </summary>
    public class Character
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public int? GameId { get; set; }
        public ICollection<Profession> Professions { get; set; }
        public ICollection<TaskCharacter> TaskCharacters { get; set; }
        public string Name { get; set; }
        public string Realm { get; set; }
        public PlayableClass Class { get; set; }
        public int? PlayableRaceGameId { get; set; } // Growing too quickly to be cached in DB
        public CharacterGender Gender { get; set; }
        public int Level { get; set; }
    }

    public enum CharacterGender
    {
        Male,
        Female
    }

    [Owned]
    public class PlayableClass : Enumeration
    {
        public static readonly PlayableClass Warrior
            = new PlayableClass(1, "Warrior");
        public static readonly PlayableClass Paladin
            = new PlayableClass(2, "Paladin");
        public static readonly PlayableClass Hunter
            = new PlayableClass(3, "Hunter");
        public static readonly PlayableClass Rogue
            = new PlayableClass(4, "Rogue");
        public static readonly PlayableClass Priest
            = new PlayableClass(5, "Priest");
        public static readonly PlayableClass DeathKnight
            = new PlayableClass(6, "Death Knight");
        public static readonly PlayableClass Shaman
            = new PlayableClass(7, "Shaman");
        public static readonly PlayableClass Mage
            = new PlayableClass(8, "Mage");
        public static readonly PlayableClass Warlock
            = new PlayableClass(9, "Warlock");
        public static readonly PlayableClass Monk
            = new PlayableClass(10, "Monk");
        public static readonly PlayableClass Druid
            = new PlayableClass(11, "Druid");
        public static readonly PlayableClass DemonHunter
            = new PlayableClass(12, "Demon Hunter");

        private PlayableClass() { }
        /// <summary>
        /// Represents a character's class.
        /// </summary>
        /// <param name="id">The Blizzard API ID of the class.</param>
        /// <param name="displayName">The name of the class.</param>
        private PlayableClass(int id, string displayName) : base(id, displayName) { }
    }

    [Owned]
    public class Profession : Enumeration
    {
        public static readonly Profession Blacksmithing
            = new Profession(164, "Blacksmithing");
        public static readonly Profession Leatherworking
            = new Profession(165, "Leatherworking");
        public static readonly Profession Alchemy
            = new Profession(171, "Alchemy");
        public static readonly Profession Herbalism
            = new Profession(182, "Herbalism");
        public static readonly Profession Cooking
            = new Profession(185, "Cooking");
        public static readonly Profession Mining
            = new Profession(186, "Mining");
        public static readonly Profession Tailoring
            = new Profession(197, "Tailoring");
        public static readonly Profession Engineering
            = new Profession(202, "Engineering");
        public static readonly Profession Enchanting
            = new Profession(333, "Enchanting");
        public static readonly Profession Fishing
            = new Profession(356, "Fishing");
        public static readonly Profession Skinning
            = new Profession(393, "Skinning");
        public static readonly Profession Jewelcrafting
            = new Profession(755, "Jewelcrafting");
        public static readonly Profession Inscription
            = new Profession(773, "Inscription");
        public static readonly Profession Archaeology
            = new Profession(794, "Archaeology");

        private Profession() { }
        /// <summary>
        /// Represents one of a character's professions.
        /// </summary>
        /// <param name="id">The Blizzard API ID of the profession.</param>
        /// <param name="displayName">The name of the profession.</param>
        private Profession(int id, string displayName) : base(id, displayName) { }
    }
}
