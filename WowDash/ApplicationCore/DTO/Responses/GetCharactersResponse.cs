using System;
using System.Collections.Generic;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetCharactersResponse
    {
        public Guid PlayerId { get; set; }
        public ICollection<CharacterResponse> Characters { get; set; }

        public GetCharactersResponse() 
        {
            Characters = new List<CharacterResponse>();
        }

        public GetCharactersResponse(Guid playerId, ICollection<CharacterResponse> characters)
        {
            PlayerId = playerId;

            // Ensures it will never be null, just empty
            Characters = new List<CharacterResponse>();

            if (characters != null)
                foreach (var c in characters)
                    Characters.Add(c);
        }
    }
}
