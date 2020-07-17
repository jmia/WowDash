using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using wow_dashboard.Utilities;

namespace wow_dashboard.Models
{
    /// <summary>
    /// Represents a singular or recurring task or goal.
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
        public CollectibleType CollectibleType { get; set; }
        public Source Source { get; set; }
        public Priority Priority { get; set; }
        public RefreshFrequency RefreshFrequency { get; set; }

    }

    public enum Priority
    {
        Lowest,
        Low,
        Medium,
        High,
        Highest
    }

    public enum RefreshFrequency
    {
        Never,
        Daily,
        Weekly
    }

    /// <summary>
    /// The type of collectible. Used for sorting, filtering, and form field generation.
    /// </summary>
    public enum TaskType
    {
        General,
        Achievement,
        Collectible,
    }

    /// <summary>
    /// A collection of references to Blizzard API data.
    /// </summary>
    [Owned]
    public class GameDataReference
    {
        /// <summary>
        /// The ID of the record on the Blizzard API.
        /// </summary>
        public int Id { get; set; }
        public GameDataType Type { get; set; }
        public string Description { get; set; }

        public enum GameDataType
        {
            // Quest areas can be cross-referenced for Wowhead Tooltips
            // with the key "zone."
            JournalInstance,    // Dungeon or Raid
            JournalEncounter,   // Boss
            QuestArea           // Zone
        }

    }

    /// <summary>
    /// A collection of references to Wowhead data.
    /// Used to generate useful tooltips and Wowhead links.
    /// All but NPC IDs can be matched 1:1 with Blizzard API IDs.
    /// </summary>
    [Owned]
    public class WowheadDataReference
    {
        /// <summary>
        /// The ID of the record on the Wowhead database.
        /// </summary>
        public int Id { get; set; }
        public WowheadDataType Type { get; set; }
        public string Description { get; set; }

        public enum WowheadDataType
        {
            // NPC, Achievement, and Quest IDs will need to be manually
            // retrieved from Wowhead by the user. The datasets are too
            // large to be cached and type-ahead searched.
            Achievement,
            Item,       // Recipes, mounts, gear
            ItemSet,    // Transmog sets, dungeon sets
            Npc,        // Boss, vendor, monster
            Quest
        }

    }

    [Owned]
    public class CollectibleType : Enumeration
    {
        public static readonly CollectibleType Item
            = new CollectibleType(0, "Weapons and Armour");
        public static readonly CollectibleType ItemSet
            = new CollectibleType(1, "Armour Sets");
        public static readonly CollectibleType Mount
            = new CollectibleType(1, "Mounts");
        public static readonly CollectibleType Pet
            = new CollectibleType(2, "Battle Pets");
        public static readonly CollectibleType Recipe
            = new CollectibleType(3, "Recipes");
        public static readonly CollectibleType Toy
            = new CollectibleType(4, "Toys");

        private CollectibleType() { }
        /// <summary>
        /// The type of collectible associated with the task.
        /// </summary>
        /// <param name="id">The ID of the collectible type.</param>
        /// <param name="displayName">The display name of the collectible type.</param>
        private CollectibleType(int id, string displayName) : base(id, displayName) { }
    }

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
        /// <summary>
        /// The source of an associated collectible.
        /// </summary>
        /// <param name="id">The ID of the source.</param>
        /// <param name="displayName">The display name of the source.</param>
        private Source(int id, string displayName) : base(id, displayName) { }
    }
}
