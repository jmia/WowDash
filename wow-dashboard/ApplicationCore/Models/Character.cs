using System;
using System.Collections.Generic;

namespace wow_dashboard.ApplicationCore.Models
{
    /// <summary>
    /// Represents a playable character in a user's roster.
    /// </summary>
    public class Character
    {
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public Guid PlayerId { get; set; }
        public int? GameId { get; set; }
        public ICollection<TaskCharacter> TaskCharacters { get; set; }
        public string Name { get; set; }
        public CharacterGender Gender { get; set; }
        public int? Level { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public string Realm { get; set; }

        public Character()
        {
            TaskCharacters = new List<TaskCharacter>();
        }
    }

    public enum CharacterGender
    {
        Male,
        Female
    }
}
