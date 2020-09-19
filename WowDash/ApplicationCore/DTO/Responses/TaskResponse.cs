using System;
using System.Collections.Generic;
using WowDash.ApplicationCore.DTO.Common;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class TaskResponse
    {
        public Guid TaskId { get; set; }
        public Guid PlayerId { get; set; }  // TODO: Probably don't need?
        public string Description { get; set; }
        public ICollection<GameDataReferenceItem> GameDataReferences { get; set; }
        public bool IsFavourite { get; set; }
        public string Notes { get; set; }
        public TaskType TaskType { get; set; }
        public CollectibleType? CollectibleType { get; set; }
        public Source? Source { get; set; }
        public Priority Priority { get; set; }
        public RefreshFrequency RefreshFrequency { get; set; }

        public TaskResponse() { }

        public TaskResponse(Guid taskId, Guid playerId, string description,
            List<GameDataReferenceItem> gameDataReferences, bool isFavourite, string notes, TaskType taskType,
            CollectibleType? collectibleType, Source? source, Priority priority, RefreshFrequency refreshFrequency)
        {
            TaskId = taskId;
            PlayerId = playerId;
            Description = description;
            GameDataReferences = gameDataReferences;
            IsFavourite = isFavourite;
            Notes = notes;
            TaskType = taskType;
            CollectibleType = collectibleType;
            Source = source;
            Priority = priority;
            RefreshFrequency = refreshFrequency;
        }
    }
}