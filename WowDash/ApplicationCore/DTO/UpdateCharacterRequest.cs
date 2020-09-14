using System;
using System.ComponentModel.DataAnnotations;
using WowDash.ApplicationCore.Entities;

namespace WowDash.ApplicationCore.DTO
{
    public class UpdateCharacterRequest
    {
        [Required]
        public Guid CharacterId { get; set; }
        public int? GameId { get; set; }
        public string Name { get; set; }
        public CharacterGender Gender { get; set; }
        public int? Level { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public string Realm { get; set; }

        public UpdateCharacterRequest() { }

        public UpdateCharacterRequest(
            Guid characterId,
            int? gameId,
            string name,
            CharacterGender gender,
            int? level,
            string @class,
            string race,
            string realm)
        {
            CharacterId = characterId;
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
