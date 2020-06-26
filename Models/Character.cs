using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wow_dashboard.Models
{
    public class Character
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public int PlayableClassId { get; set; }
        public int PlayableRaceId { get; set; }
        public CharacterGender Gender { get; set; }
        public int Level { get; set; }
        
        // TODO
        // Primary professions
        // Secondary professions


        public enum CharacterGender
        {
            Female,
            Male
        }

    }
}
