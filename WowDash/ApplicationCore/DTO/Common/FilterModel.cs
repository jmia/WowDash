using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO.Common
{
    public class FilterModel
    {
        /// <summary>
        /// The ID of the player.
        /// </summary>
        [Required]
        public Guid PlayerId { get; set; }
        /// <summary>
        /// The characters to filter on, separated by "|".
        /// </summary>
        public string CharacterId { get; set; }
        /// <summary>
        /// The task types to filter on, separated by "|".
        /// </summary>
        public string TaskType { get; set; }
        /// <summary>
        /// The collection types to filter on, separated by "|".
        /// </summary>
        public string CollectibleType { get; set; }
        /// <summary>
        /// The dungeon IDs to filter on, separated by "|".
        /// </summary>
        public string DungeonId { get; set; }
        /// <summary>
        /// The zone IDs to filter on, separated by "|".
        /// </summary>
        public string ZoneId { get; set; }
        /// <summary>
        /// The refresh frequencies to filter on, separated by "|".
        /// </summary>
        public string RefreshFrequency { get; set; }
        public bool IsFavourite { get; set; } = false;
        /// <summary>
        /// Whether the filter should return only tasks with assigned
        /// task characters that are active for this lockout (refresh frequency).
        /// </summary>
        public bool OnlyActiveAttempts { get; set; } = false;
        /// <summary>
        /// The property on which to sort, 
        /// can be "priority" or "alpha",
        /// suffixed with "_asc" or "_desc."
        /// </summary>
        public string SortBy { get; set; }

        public FilterModel() { }

        public FilterModel(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}
