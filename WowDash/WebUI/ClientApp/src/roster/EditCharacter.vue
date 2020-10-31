<template>
  <div>
    <div class="flex justify-start items-center mb-2">
      <router-link
        to="/roster"
        class="bg-red-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
      >
        <font-awesome-icon icon="caret-left" /> Back</router-link
      >
    </div>
    <div class="w-1/2 mx-auto dark-rounded">
      <div class="border-b border-gray-800 p-3">
        <h5 class="table-title">Add a Character</h5>
      </div>

      <FormulateForm @submit="updateCharacter" v-model="characterValues">
        <div class="bg-gray-800 text-gray-500 text-lg p-2 pl-4 pr-4 mb-3">
          <div class="w-3/4 space-y-4">
            <FormulateInput
              type="text"
              name="name"
              label="Name"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            />
            <FormulateInput
              type="number"
              name="level"
              label="Level"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            />
            <FormulateInput
              type="radio"
              name="gender"
              label="Gender"
              :options="genders"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['text-gray-500']"
            />
            <FormulateInput
              type="select"
              name="playableClass"
              :options="playableClasses"
              label="Class"
              placeholder="Select a Class"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            />
            <FormulateInput
              type="select"
              v-if="characterValues.playableClass != ''"
              name="specialization"
              :option-groups="specializations"
              label="Specialization"
              placeholder="Select a Specialization"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            />
            <FormulateInput
              type="select"
              name="race"
              :option-groups="playableRaces"
              label="Race"
              placeholder="Select a Race"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            />
            <FormulateInput
              type="select"
              name="realm"
              :options="realms"
              label="Realm"
              placeholder="Select a Realm"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            />
          </div>
        </div>
        <div class="flex justify-end mr-2">
          <button
            class="bg-blue-400 text-gray-800 p-2 mb-3 font-bold text-center border-gray-800 rounded shadow"
          >
            Update Roster
          </button>
        </div>
      </FormulateForm>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditCharacter",
  props: ["id"],
  data() {
    return {
      playerId: localStorage.playerId,
      characterId: this.id,
      characterValues: {
        name: "",
        gender: 1,
        level: 0,
        playableClass: "",
        specialization: "",
        race: "",
        realm: "",
      },

      // select list values
      genders: {
        0: "Male",
        1: "Female",
      },
      playableClasses: {
        "Death Knight": "Death Knight",
        "Demon Hunter": "Demon Hunter",
        Druid: "Druid",
        Hunter: "Hunter",
        Mage: "Mage",
        Monk: "Monk",
        Paladin: "Paladin",
        Priest: "Priest",
        Rogue: "Rogue",
        Shaman: "Shaman",
        Warlock: "Warlock",
        Warrior: "Warrior",
      },
      specializations: {
        "Death Knight": {
          Blood: "Blood",
          Frost: "Frost",
          Unholy: "Unholy",
        },
        "Demon Hunter": {
          Havoc: "Havoc",
          Vengeance: "Vengeance",
        },
        Druid: {
          Balance: "Balance",
          Feral: "Feral",
          Guardian: "Guardian",
          Restoration: "Restoration",
        },
        Hunter: {
          "Beast Mastery": "Beast Mastery",
          Marksmanship: "Marksmanship",
          Survival: "Survival",
        },
        Mage: {
          Arcane: "Arcane",
          Fire: "Fire",
          Frost: "Frost",
        },
        Monk: {
          Brewmaster: "Brewmaster",
          Mistweaver: "Mistweaver",
          Windwalker: "Windwalker",
        },
        Paladin: {
          Holy: "Holy",
          Protection: "Protection",
          Retribution: "Retribution",
        },
        Priest: {
          Discipline: "Discipline",
          Holy: "Holy",
          Shadow: "Shadow",
        },
        Rogue: {
          Assassination: "Assassination",
          Outlaw: "Outlaw",
          Subtlety: "Subtlety",
        },
        Shaman: {
          Elemental: "Elemental",
          Enhancement: "Enhancement",
          Restoration: "Restoration",
        },
        Warlock: {
          Affliction: "Affliction",
          Demonology: "Demonology",
          Destruction: "Destruction",
        },
        Warrior: {
          Arms: "Arms",
          Fury: "Fury",
          Protection: "Protection",
        },
      },
      playableRaces: {
        Horde: {
          "Blood Elf": "Blood Elf",
          Goblin: "Goblin",
          "Highmountain Tauren": "Highmountain Tauren",
          "Mag'har Orc": "Mag'har Orc",
          Nightborne: "Nightborne",
          Orc: "Orc",
          Tauren: "Tauren",
          Troll: "Troll",
          Undead: "Undead",
          Vulpera: "Vulpera",
          "Zandalari Troll": "Zandalari Troll",
        },
        Alliance: {
          "Dark Iron Dwarf": "Dark Iron Dwarf",
          Draenei: "Draenei",
          Dwarf: "Dwarf",
          Gnome: "Gnome",
          Human: "Human",
          "Kul Tiran": "Kul Tiran",
          "Lightforged Draenei": "Lightforged Draenei",
          Mechagnome: "Mechagnome",
          "Night Elf": "Night Elf",
          "Void Elf": "Void Elf",
          Worgen: "Worgen",
        },
        Neutral: {
          Pandaren: "Pandaren",
        },
      },
      realms: [],
    };
  },
  methods: {
    updateCharacter: function () {
      let vm = this;

      this.$http
        .put(`/api/characters/`, {
          characterId: vm.characterId,
          name: vm.characterValues.name,
          gender: Number(vm.characterValues.gender),
          level: Number(vm.characterValues.level),
          class: vm.characterValues.playableClass,
          specialization: vm.characterValues.specialization,
          race: vm.characterValues.race,
          realm: vm.characterValues.realm,
        })
        .then(function () {
          vm.$router.push("/roster");
        })
        .catch(function (error) {
          console.log("had an error");
          console.log(error);
        });
    },
  },
  mounted: function () {
    let vm = this;

    this.$http
      .get(`/api/characters/${vm.characterId}`)
      .then(function (character) {
        vm.characterValues.name = character.data.name;
        vm.characterValues.gender = Number(character.data.gender);
        vm.characterValues.level = Number(character.data.level);
        vm.characterValues.playableClass = character.data.class;
        vm.characterValues.specialization = character.data.specialization;
        vm.characterValues.race = character.data.race;
        vm.characterValues.realm = character.data.realm;
      })
      .catch(function (error) {
        console.log("had an error");
        console.log(error);
      });

    this.$http
      .get(`/api/realms`)
      .then(function (response) {
        response.data.map((r) => {
          vm.realms.push({ value: r.slug, label: r.name });
        });
      })
      .catch(function (error) {
        console.log("had an error");
        console.log(error);
      });
  },
};
</script>

<style>
.standard-input {
  @apply bg-gray-400;
  @apply text-gray-800;
  @apply rounded;
  @apply p-2;
}

.formulate-input-element--radio {
  display: inline-block !important;
}
</style>