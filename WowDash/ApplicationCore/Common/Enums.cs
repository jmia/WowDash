using System.ComponentModel;

namespace WowDash.ApplicationCore.Common
{
    public class Enums
    {
        /// <summary>
        /// The type of collectible associated with the task.
        /// </summary>
        public enum CollectibleType
        {
            [Description("Gear and Items")]
            Item,
            [Description("Dungeon Sets")]
            ItemSet,
            [Description("Mounts")]
            Mount,
            [Description("Battle Pets")]
            Pet
        }

        /// <summary>
        /// The priority of a task.
        /// </summary>
        public enum Priority
        {
            Lowest,
            Low,
            Medium,
            High,
            Highest
        }

        /// <summary>
        /// How often a task should set all TaskCharacters back to active.
        /// </summary>
        public enum RefreshFrequency
        {
            Never,
            Daily,
            Weekly
        }

        /// <summary>
        /// The source of the collectible. 
        /// Used for sorting, filtering, and form field generation.
        /// </summary>
        public enum Source
        {
            [Description("Dungeon")]
            Dungeon,
            [Description("Quest")]
            Quest,
            [Description("Vendor")]
            Vendor,
            [Description("World Drop")]
            WorldDrop,
            [Description("Other")]
            Other
        }

        /// <summary>
        /// The type of collectible. 
        /// Used for sorting, filtering, and form field generation.
        /// </summary>
        public enum TaskType
        {
            General,
            Achievement,
            Collectible
        }
    }
}
