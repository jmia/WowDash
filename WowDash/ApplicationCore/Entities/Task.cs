using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.Entities
{
    /// <summary>
    /// Represents a singular or recurring task or goal.
    /// </summary>
    public class Task
    {
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public Guid PlayerId { get; private set; }
        public string Description { get; set; }
        public ICollection<GameDataReference> GameDataReferences { get; set; }
        public ICollection<TaskCharacter> TaskCharacters { get; set; }
        public bool IsFavourite { get; set; }
        public string Notes { get; set; }
        public TaskType TaskType { get; private set; }
        public CollectibleType CollectibleType { get; set; }
        public Source Source { get; set; }
        public Priority Priority { get; set; }
        public RefreshFrequency RefreshFrequency { get; set; }

        public Task(Guid playerId, TaskType taskType)
        {
            PlayerId = playerId;
            TaskType = taskType;
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
        /// The subclass of the reference (e.g. toy, weapon)
        /// </summary>
        public string Subclass { get; set; }
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
}