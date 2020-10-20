using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WowDash.ApplicationCore.DTO.Common;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO.Requests
{
    public class UpdateTaskRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        public ICollection<GameDataReferenceItem> GameDataReferenceItems { get; set; }
        [Required]
        public bool IsFavourite { get; set; }
        public string Notes { get; set; }
        [Required]
        public TaskType TaskType { get; set; }
        public CollectibleType? CollectibleType { get; set; }
        public Source? Source { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public RefreshFrequency RefreshFrequency { get; set; }

        public UpdateTaskRequest()
        {
            GameDataReferenceItems = new List<GameDataReferenceItem>();
        }
    }
}
