using System;
using System.Collections.Generic;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.Entities
{
    /// <summary>
    /// Represents a playable character in a user's roster.
    /// </summary>
    public class Character
    {
        public Guid Id { get; set; }
        public virtual Player Player { get; set; }
        public Guid PlayerId { get; set; }
        public int? GameId { get; set; }
        public virtual ICollection<TaskCharacter> TaskCharacters { get; set; }
        public string Name { get; set; }
        public CharacterGender Gender { get; set; }
        public int? Level { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        /// <summary>
        /// The home realm of the character in kebab-case.
        /// </summary>
        public string Realm { get; set; }

        public Character() { }      // TODO: Update SeedData method and take this out if not needed

        public Character(Guid playerId, int? gameId, string name, CharacterGender gender, int? level, string @class,
            string race, string realm)
        {
            PlayerId = playerId;
            GameId = gameId;
            Name = name;
            Gender = gender;
            Level = level;
            Class = @class;
            Race = race;
            Realm = realm;

            TaskCharacters = new List<TaskCharacter>();
        }
    }
}
