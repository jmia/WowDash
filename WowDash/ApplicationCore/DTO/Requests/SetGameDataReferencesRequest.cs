using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO.Requests
{
    public class SetGameDataReferencesRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        /// <summary>
        /// A collection of game data references.
        /// </summary>
        [Required]
        public ICollection<GameDataReferenceItem> GameDataReferenceItems { get; set; }

        public SetGameDataReferencesRequest() { }

        public SetGameDataReferencesRequest(Guid taskId, ICollection<GameDataReferenceItem> gameDataReferenceItems)
        {
            TaskId = taskId;

            // Ensures it will never be null, just empty
            GameDataReferenceItems = new List<GameDataReferenceItem>();

            if (gameDataReferenceItems != null)
                foreach (var ri in gameDataReferenceItems)
                    GameDataReferenceItems.Add(ri);
        }
    }
}
