using System;
using System.Collections.Generic;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetCharacterRosterResponse
    {
        public Guid PlayerId { get; set; }
        public ICollection<CharacterRosterEntry> Characters { get; set; }

        public GetCharacterRosterResponse() 
        {
            Characters = new List<CharacterRosterEntry>();
        }

        public GetCharacterRosterResponse(Guid playerId, List<CharacterRosterEntry> characters)
        {
            PlayerId = playerId;
            Characters = characters;
        }
    }
}
