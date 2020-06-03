using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wow_dashboard.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<GameDataReference> GameDataReferences { get; set; }
        public bool IsTodaysGoal { get; set; }
        public bool IsFavourite { get; set; }
        public string Notes { get; set; }

        // TODO: Add foreign keys
        //task_type_id
        //collection_type_id
        //source_id
        //zone_size_id
        //zone_difficulty_id
        //priority_id
        //refresh_duration_id

    }

    [Owned]
    public class GameDataReference
    {
        public int GameDataId { get; set; }
        public GameDataType Type { get; set; }

        public enum GameDataType
        {
            Achievement,
            Item,
            ItemSet,
            Mount,
            Npc,
            Pet,
            Quest,
            Zone
        }

    }
}
