using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a singular or recurring task or goal.
    /// </summary>
    public class Task
    {
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public Guid PlayerId { get; set; }
        public ICollection<GameDataReference> GameDataReferences { get; set; }
        public ICollection<TaskCharacter> TaskCharacters { get; set; }
        public bool IsTodaysGoal { get; set; }
        public bool IsFavourite { get; set; }
        public string Notes { get; set; }
        public TaskType TaskType { get; set; }
        public CollectibleType CollectibleType { get; set; }
        public Source Source { get; set; }
        public Priority Priority { get; set; }
        public RefreshFrequency RefreshFrequency { get; set; }

        public Task()
        {
            GameDataReferences = new List<GameDataReference>();
            TaskCharacters = new List<TaskCharacter>();
        }

    }

    /// <summary>
    /// A collection of references to Blizzard API data.
    /// </summary>
    [Owned]
    public class GameDataReference
    {
        /// <summary>
        /// The database-generated ID of the reference.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The ID of the record on the Blizzard API (or the Wowhead Database).
        /// </summary>
        public int? GameId { get; set; }
        /// <summary>
        /// The type or category (i.e. endpoint) of data reference.
        /// </summary>
        public GameDataType Type { get; set; }
        /// <summary>
        /// A string display of the data.
        /// </summary>
        public string Description { get; set; }

        public enum GameDataType
        {
            Achievement,
            Item,               // Dropped items (e.g. gear, toys, recipes)
            ItemSet,
            JournalInstance,    // Dungeon or Raid
            JournalEncounter,   // Boss
            Npc,                // ** Manually entered, Wowhead only
            Pet,                // Battle pets
            Quest,
            QuestArea           // Zone
        }

    }

    /// <summary>
    /// The type of collectible associated with the task.
    /// </summary>
    public enum CollectibleType
    {
        [Description("Gear and Items")]
        Item,
        [Description("Dungeon Sets")]
        ItemSet,
        [Description("Mounts")]
        Mount,
        [Description("Battle Pets")]
        Pet
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
    /// The source of the collectible. 
    /// Used for sorting, filtering, and form field generation.
    /// </summary>
    public enum Source
    {
        [Description("Dungeon")]
        Dungeon,
        [Description("Quest")]
        Quest,
        [Description("Vendor")]
        Vendor,
        [Description("World Drop")]
        WorldDrop,
        [Description("Other")]
        Other
    }

    /// <summary>
    /// The type of collectible. 
    /// Used for sorting, filtering, and form field generation.
    /// </summary>
    public enum TaskType
    {
        General,
        Achievement,
        Collectible
    }
}
