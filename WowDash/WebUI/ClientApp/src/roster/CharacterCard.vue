<template>
  <div
    class="w-full sm:w-1/2 lg:w-1/3 xl:w-1/4 p-3 bg-gray-900 border border-gray-800 rounded shadow"
  >
    <div
      class="bg-gray-800 rounded p-2 pl-4 pr-4 flex flex-col justify-between leading-normal"
    >
      <div class="text-gray-500">
        <div class="w-full inline-flex justify-between items-center">
          <div class="inline-flex items-center">
            <img :src="classImagePath" class="w-8 h-8 mr-2" />
            <div
              class="inline-flex w-auto font-bold text-2xl mb-1"
              :class="formattedClass"
            >
              {{ name }}
            </div>
          </div>
          <div class="w-auto">
            <router-link :to="{ name: 'edit-character', params: { id: characterId }}" class="mr-4">
              <span class="text-blue-400"
                ><font-awesome-icon icon="edit"
              /></span>
            </router-link>
            <button class="mr-4" @click="deleteCharacter">
              <span class="text-blue-400"
                ><font-awesome-icon icon="trash"
              /></span>
            </button>
          </div>
        </div>
        <div class="mb-2">
          <div class="font-bold">
            {{ level }}
            <span class="text-gray-600"
              ><font-awesome-icon :icon="genderIconName"
            /></span>
            {{ race }}
          </div>
          <div class="font-bold">{{ specialization }} {{ playableClass }}</div>
        </div>

        <hr class="border-b-1 border-gray-700 my-2 mx-4" />

        <div class="flex justify-center mb-2 text-xs text-center">
          <div
            class="p-1 w-auto uppercase cursor-default"
            :class="factionColour"
          >
            {{ formattedRealm }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "CharacterCard",
  props: {
    characterId: {
      type: String,
      required: true,
    },
    name: {
      type: String,
    },
    gender: {
      type: Number,
      default: 1,
    },
    level: {
      type: Number,
      default: 0,
    },
    playableClass: {
      type: String,
    },
    specialization: {
      type: String,
    },
    race: {
      type: String,
    },
    realm: {
      type: String,
    },
  },
  data() {
    return {
      // Class icons must be imported before runtime
      // because webpack changes them into embedded images
      // check it: https://stackoverflow.com/a/49080214
      icons: {
        deathknight: require("@/assets/deathknight-vector.png"),
        demonhunter: require("@/assets/demonhunter-vector.png"),
        druid: require("@/assets/druid-vector.png"),
        hunter: require("@/assets/hunter-vector.png"),
        mage: require("@/assets/mage-vector.png"),
        monk: require("@/assets/monk-vector.png"),
        paladin: require("@/assets/paladin-vector.png"),
        priest: require("@/assets/priest-vector.png"),
        rogue: require("@/assets/rogue-vector.png"),
        shaman: require("@/assets/shaman-vector.png"),
        warlock: require("@/assets/warlock-vector.png"),
        warrior: require("@/assets/warrior-vector.png"),
      },
    };
  },
  computed: {
    classImagePath: function () {
      return this.icons[this.formattedClass];
    },
    factionColour: function () {
      var hordeRaces = [
        "Blood Elf",
        "Goblin",
        "Highmountain Tauren",
        `Mag'har Orc`,
        "Nightborne",
        "Orc",
        "Pandaren",
        "Tauren",
        "Troll",
        "Undead",
        "Vulpera",
        "Zandalari Troll",
      ];
      var allianceRaces = [
        "Dark Iron Dwarf",
        "Draenei",
        "Dwarf",
        "Gnome",
        "Human",
        "Kul Tiran",
        "Lightforged Draenei",
        "Mechagnome",
        "Night Elf",
        "Pandaren",
        "Void Elf",
        "Worgen",
      ];

      if (hordeRaces.includes(this.race)) {
        return "horde";
      } else if (allianceRaces.includes(this.race)) {
        return "alliance";
      } else {
        return "neutral";
      }
    },
    formattedClass: function () {
      return this.playableClass.toLowerCase().replace(/ /g, "");
    },
    formattedRealm: function () {
      return this.realm.replace(/-/g, " ");
    },
    genderIconName: function () {
      if (this.gender == 0) {
        return "mars";
      } else {
        return "venus";
      }
    },
  },
  methods: {
    deleteCharacter: function () {
      let vm = this;
      console.log('deleting character...');
      if (window.confirm("Do you really want to delete this character?")) {
        this.$http.delete(`/api/characters/${vm.characterId}`)
          .then(function (response) {
            console.log(response);
            vm.$emit("reload-roster");
          })
          .catch(function (error) {
            console.log("had an error");
            console.log(error);
          });
      }
    },
  },
};
</script>

<style scoped>
/* Factions */
.horde {
  @apply text-red-400;
}
.alliance {
  @apply text-blue-400;
}
.neutral {
  @apply text-gray-500;
}
/* Classes */
.deathknight {
  @apply text-indigo-700;
}
.demonhunter {
  @apply text-purple-700;
}
.druid {
  @apply text-orange-400;
}
.hunter {
  @apply text-green-400;
}
.mage {
  @apply text-blue-300;
}
.monk {
  @apply text-teal-400;
}
.paladin {
  @apply text-pink-400;
}
.priest {
  @apply text-white;
}
.rogue {
  @apply text-yellow-400;
}
.shaman {
  @apply text-blue-700;
}
.warlock {
  @apply text-purple-400;
}
.warrior {
  @apply text-tan;
}
</style>