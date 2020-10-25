using System;

namespace WowDash.ApplicationCore.DTO.Common
{
    public class FilterListRosterResponse
    {
        public Guid CharacterId { get; set; }
        public string Name { get; set; }

        public FilterListRosterResponse() { }

        public FilterListRosterResponse(Guid characterId, string name)
        {
            CharacterId = characterId;
            Name = name;
        }
    }
}
