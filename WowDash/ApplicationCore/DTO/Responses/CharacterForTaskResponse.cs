﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class CharacterForTaskResponse
    {
        public Guid CharacterId { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }

        public CharacterForTaskResponse() { }

        public CharacterForTaskResponse(Guid characterId, string name, string @class)
        {
            CharacterId = characterId;
            Name = name;
            Class = @class;
        }
    }
}
