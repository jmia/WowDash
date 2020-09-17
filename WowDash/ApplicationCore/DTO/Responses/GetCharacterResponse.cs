using System;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetCharacterResponse
    {
        public Guid CharacterId { get; set; }
        public Guid PlayerId { get; set; }  // TODO: Probably don't need?
        public int? GameId { get; set; }
        public string Name { get; set; }
        public CharacterGender Gender { get; set; }
        public int? Level { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public string Realm { get; set; }

        public GetCharacterResponse() { }

        public GetCharacterResponse(Guid characterId, Guid playerId, int? gameId, string name, CharacterGender gender, int? level,
            string @class, string race, string realm)
        {
            CharacterId = characterId;
            PlayerId = playerId;
            GameId = gameId;
            Name = name;
            Gender = gender;
            Level = level;
            Class = @class;
            Race = race;
            Realm = realm;
        }
    }
}
