using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Entities.GameDataReference;

namespace WowDash.ApplicationCore.DTO.Common
{
    /// <summary>
    /// An item in a list of game data references
    /// for a given request.
    /// </summary>
    public class GameDataReferenceItem
    {
        /// <summary>
        /// The database ID of the reference, if it exists.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// The in-game ID of the reference, if provided.
        /// </summary>
        public int? GameId { get; set; }
        /// <summary>
        /// The type or category (i.e. endpoint) of data reference.
        /// </summary>
        [Required]
        public GameDataType Type { get; set; }
        /// <summary>
        /// The subclass of the reference (e.g. toy, mount)
        /// </summary>
        public string Subclass { get; set; }
        /// <summary>
        /// A description of the reference. For achievements,
        /// this is a description of the achievement requirements.
        /// For items and NPCs, this is their name.
        /// </summary>
        public string Description { get; set; }

        public GameDataReferenceItem() { }

        public GameDataReferenceItem(int? id, int? gameId, GameDataType type, string subclass, string description)
        {
            Id = id;
            GameId = gameId;
            Type = type;
            Subclass = subclass;
            Description = description;
        }
    }
}
