using System;
using System.Collections.Generic;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetCharactersResponse
    {
        public Guid PlayerId { get; set; }
        public ICollection<GetCharacterResponse> Characters { get; set; }

        public GetCharactersResponse() 
        {
            Characters = new List<GetCharacterResponse>();
        }

        public GetCharactersResponse(Guid playerId, ICollection<GetCharacterResponse> characters)
        {
            PlayerId = playerId;

            // Ensures it will never be null, just empty
            Characters = new List<GetCharacterResponse>();

            if (characters != null)
                foreach (var c in characters)
                    Characters.Add(c);
        }
    }
}
