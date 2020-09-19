using System;
using System.Collections.Generic;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetCharacterRosterResponse
    {
        public Guid PlayerId { get; set; }
        public ICollection<CharacterResponse> Characters { get; set; }

        public GetCharacterRosterResponse() 
        {
            Characters = new List<CharacterResponse>();
        }

        public GetCharacterRosterResponse(Guid playerId, List<CharacterResponse> characters)
        {
            PlayerId = playerId;
            Characters = characters;
        }
    }
}
