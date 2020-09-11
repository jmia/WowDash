using System;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO
{
    public class SetTaskCollectibleTypeAndSourceRequest
    {
        public Guid TaskId { get; set; }
        public CollectibleType CollectibleType { get; set; }
        public Source Source { get; set; }

        public SetTaskCollectibleTypeAndSourceRequest(Guid taskId, CollectibleType collectibleType, Source source)
        {
            TaskId = taskId;
            CollectibleType = collectibleType;
            Source = source;
        }
    }
}
