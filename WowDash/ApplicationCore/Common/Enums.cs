using System.ComponentModel;

namespace WowDash.ApplicationCore.Common
{
    public class Enums
    {
        /// <summary>
        /// The type of collectible associated with the task.<br />
        /// `0` for Item<br />
        /// `1` for ItemSet<br />
        /// `2` for Mount<br />
        /// `3` for Pet<br /><br />
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
        /// The priority of a task.<br />
        /// `0` for Lowest<br />
        /// `1` for Low<br />
        /// `2` for Medium<br />
        /// `3` for High<br />
        /// `4` for Highest
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
        /// How often a task should set all its TaskCharacters back to active.<br />
        /// `0` for Never<br />
        /// `1` for Daily<br />
        /// `2` for Weekly
        /// </summary>
        public enum RefreshFrequency
        {
            Never,
            Daily,
            Weekly
        }

        /// <summary>
        /// The source of the collectible. 
        /// Used for sorting, filtering, and form field generation.<br />
        /// `0` for Dungeon<br />
        /// `1` for Quest<br />
        /// `2` for Vendor<br />
        /// `3` for WorldDrop<br />
        /// `4` for Other
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
        /// Used for sorting, filtering, and form field generation.<br />
        /// `0` for General Tasks<br />
        /// `1` for Achievements<br />
        /// `2` for Collectibles
        /// </summary>
        public enum TaskType
        {
            General,
            Achievement,
            Collectible
        }
    }
}
