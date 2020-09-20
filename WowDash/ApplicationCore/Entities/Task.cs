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
        public virtual Player Player { get; set; }
        public Guid PlayerId { get; private set; }
        public string Description { get; set; }
        public ICollection<GameDataReference> GameDataReferences { get; set; }
        public virtual ICollection<TaskCharacter> TaskCharacters { get; set; }
        public bool IsFavourite { get; set; }
        public string Notes { get; set; }
        public TaskType TaskType { get; private set; }
        public CollectibleType? CollectibleType { get; set; }
        public Source? Source { get; set; }
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
        public int Id { get; set; }
        /// <summary>
        /// The ID of the record on the Blizzard API (or the Wowhead Database).
        /// </summary>
        public int? GameId { get; private set; }
        public GameDataType Type { get; private set; }
        /// <summary>
        /// The subclass of the reference (e.g. toy, weapon)
        /// </summary>
        public string Subclass { get; private set; }
        /// <summary>
        /// A description of the reference. For achievements,
        /// this is a description of the achievement requirements.
        /// For items and NPCs, this is their name.
        /// </summary>
        public string Description { get; private set; }

        public GameDataReference(int? gameId, GameDataType type, string subclass, string description)
        {
            GameId = gameId;
            Type = type;
            Subclass = subclass;
            Description = description;
        }

        /// <summary>
        /// The type or category (i.e. endpoint) of data reference.<br />
        /// `0` for Achievement<br />
        /// `1` for Item<br />
        /// `2` for ItemSet<br />
        /// `3` for JournalInstance (Dungeon or raid)<br />
        /// `4` for JournalEncounter (Boss)<br />
        /// `5` for NPC (Manually entered)<br />
        /// `6` for Pet (Battle pets)<br />
        /// `7` for Quest<br />
        /// `8` for QuestArea (Zone)
        /// </summary>
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