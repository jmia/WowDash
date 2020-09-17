using System;
using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO.Requests
{
    public class AddCharacterRequest
    {
        [Required]
        public Guid PlayerId { get; set; }
        /// <summary>
        /// The in-game ID of the character, if provided.
        /// </summary>
        public int? GameId { get; set; }
        public string Name { get; set; }
        public CharacterGender Gender { get; set; }
        public int? Level { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public string Realm { get; set; }

        public AddCharacterRequest() { }

        public AddCharacterRequest(
            Guid playerId,
            int? gameId,
            string name,
            CharacterGender gender,
            int? level,
            string @class,
            string race,
            string realm)
        {
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
