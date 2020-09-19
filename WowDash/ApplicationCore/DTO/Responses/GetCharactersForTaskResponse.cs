using System;
using System.Collections.Generic;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetCharactersForTaskResponse
    {
        public Guid TaskId { get; set; }
        public ICollection<CharacterForTaskResponse> Characters { get; set; }

        public GetCharactersForTaskResponse() { }

        public GetCharactersForTaskResponse(Guid taskId, List<CharacterForTaskResponse> characters)
        {
            TaskId = taskId;
            Characters = characters;
        }
    }
}
