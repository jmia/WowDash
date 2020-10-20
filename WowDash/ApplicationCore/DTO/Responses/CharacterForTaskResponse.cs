﻿using System;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class CharacterForTaskResponse
    {
        public Guid CharacterId { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public bool IsActive { get; set; }

        public CharacterForTaskResponse() { }

        public CharacterForTaskResponse(Guid characterId, string name, string @class, bool isActive)
        {
            CharacterId = characterId;
            Name = name;
            Class = @class;
            IsActive = isActive;
        }
    }
}
