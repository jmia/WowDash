using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using wow_dashboard.Utilities;

namespace wow_dashboard.Models
{
    /// <summary>
    /// Represents a one-off or recurring task or goal.
    /// </summary>
    public class Task
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<GameDataReference> GameDataReferences { get; set; }
        public ICollection<WowheadDataReference> WowheadDataReferences { get; set; }
        public ICollection<TaskCharacter> TaskCharacters { get; set; }
        public bool IsTodaysGoal { get; set; }
        public bool IsFavourite { get; set; }
        public string Notes { get; set; }
        public TaskType TaskType { get; set; }
        public CollectionType CollectionType { get; set; }
        public Source Source { get; set; }
        public ZoneDifficulty ZoneDifficulty { get; set; }
        public Priority Priority { get; set; }
        public RefreshDuration RefreshDuration { get; set; }

    }

    /// <summary>
    /// The type of collectible to track.
    /// Used for xrefs during sort & filter.
    /// </summary>
    [Owned]
    public class CollectionType : Enumeration
    {
        public static readonly CollectionType Item
            = new CollectionType(0, "Items (Gear, Weapons)");
        public static readonly CollectionType ItemSet
            = new CollectionType(1, "Item Sets");
        public static readonly CollectionType Pet
            = new CollectionType(2, "Battle Pets");
        public static readonly CollectionType Recipe
            = new CollectionType(3, "Recipes");
        public static readonly CollectionType Toy
            = new CollectionType(4, "Toys");

        private CollectionType() { }
        private CollectionType(int value, string displayName) : base(value, displayName) { }
    }

    /// <summary>
    /// These will be pulled from the Blizzard API.
    /// Quest areas and reputations can be cross-referenced for Wowhead Tooltip.
    /// </summary>
    [Owned]
    public class GameDataReference
    {
        /// <summary>
        /// The ID of the record on the Blizzard API.
        /// </summary>
        public int Id { get; set; }
        public GameDataType Type { get; set; }

        public enum GameDataType
        {
            JournalInstance,    // Dungeon or Raid
            JournalEncounter,   // Boss
            QuestArea,          // Zone (e.g. Grizzly Hills)
            ReputationFaction
        }

    }


    /// <summary>
    /// The priority of the task.
    /// </summary>
    public enum Priority
    {
        Lowest,
        Low,
        Medium,
        High,
        Highest
    }


    /// <summary>
    /// How often the task should recur.
    /// </summary>
    public enum RefreshDuration
    {
        Never,
        Daily,
        Weekly,
        Custom  // Do we really want to go down this road?
    }


    /// <summary>
    /// The source of the item (where you can buy it or find it).
    /// Used to streamline the form.
    /// </summary>
    [Owned]
    public class Source : Enumeration
    {
        public static readonly Source Dungeon
            = new Source(0, "Dungeon");
        public static readonly Source Quest
            = new Source(1, "Quest");
        public static readonly Source Vendor
            = new Source(2, "Vendor");
        public static readonly Source WorldDrop
            = new Source(3, "World Drop");
        public static readonly Source Other
            = new Source(4, "Other");

        private Source() { }
        private Source(int value, string displayName) : base(value, displayName) { }
    }


    /// <summary>
    /// The type of task.
    /// Used to streamline the form and for sort & filter.
    /// </summary>
    public enum TaskType
    {
        Achievement,
        Appearance,
        Collectible,
        Reputation,
        General
    }


    /// <summary>
    /// The user will retrieve these manually from Wowhead.
    /// They will be used to generate useful tooltips.
    /// All but NPC ID's can be matched 1:1 with Blizzard API IDs.
    /// </summary>
    [Owned]
    public class WowheadDataReference
    {
        /// <summary>
        /// The ID of the record on the Wowhead database.
        /// </summary>
        public int Id { get; set; }
        public WowheadDataType Type { get; set; }

        public enum WowheadDataType
        {
            Achievement,
            Item,       // Recipes, mounts, gear
            ItemSet,    // Transmog sets, dungeon sets
            Npc,        // Boss, vendor, battle pet, monster
            Quest
        }

    }


    /// <summary>
    /// The difficulty of the related dungeon.
    /// </summary>
    [Owned]
    public class ZoneDifficulty : Enumeration
    {
        public static readonly ZoneDifficulty Normal
            = new ZoneDifficulty(0, "Normal");
        public static readonly ZoneDifficulty Heroic
            = new ZoneDifficulty(1, "Heroic");
        public static readonly ZoneDifficulty RaidFinder
            = new ZoneDifficulty(2, "Raid Finder (LFR)");
        public static readonly ZoneDifficulty Mythic
            = new ZoneDifficulty(3, "Mythic");
        public static readonly ZoneDifficulty TenPlayer
            = new ZoneDifficulty(4, "10 Player");
        public static readonly ZoneDifficulty TenPlayerHeroic
            = new ZoneDifficulty(5, "10 Player (Heroic)");
        public static readonly ZoneDifficulty TwentyFivePlayer
            = new ZoneDifficulty(6, "25 Player");
        public static readonly ZoneDifficulty TwentyFivePlayerHeroic
            = new ZoneDifficulty(7, "25 Player (Heroic)");

        private ZoneDifficulty() { }
        private ZoneDifficulty(int value, string displayName) : base(value, displayName) { }
    }
}
