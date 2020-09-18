using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetCharactersForTaskResponse
    {
        public Guid TaskId { get; set; }
        public ICollection<CharacterForTaskEntry> Characters { get; set; }

        public GetCharactersForTaskResponse() { }

        public GetCharactersForTaskResponse(Guid taskId, List<CharacterForTaskEntry> characters)
        {
            TaskId = taskId;
            Characters = characters;
        }
    }
}
