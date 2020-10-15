<template>
  <div>
    <div class="flex justify-end items-center mb-2">
      <a
        class="bg-green-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
        href="#"
        ><font-awesome-icon icon="plus" /> Add New Character</a
      >
    </div>
    <div class="flex flex-wrap justify-start cursor-default">
      <CharacterCard
        v-for="(item, index) in characters"
        :item="item"
        :index="index"
        :key="item.characterId"
        :characterId="item.characterId"
        :name="item.name"
        :gender="item.gender"
        :level="item.level"
        :playableClass="item.class"
        :race="item.race"
        :realm="item.realm"
      />
    </div>
  </div>
</template>

<script>
import CharacterCard from "./CharacterCard";

export default {
  name: "CharacterRoster",
  components: {
    CharacterCard,
  },
  data() {
    return {
      playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
      characters: [
        {
          characterId: "fake",
          //playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
          //gameId: null,
          name: "Scully",
          gender: 1,
          level: 120,
          class: "Hunter",
          race: "Blood Elf",
          realm: "area-52",
        },
        {
          characterId: "fake",
          //playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
          //gameId: null,
          name: "Chakwas",
          gender: 1,
          level: 120,
          class: "Druid",
          race: "Highmountain Tauren",
          realm: "area-52",
        },
        {
          characterId: "fake",
          //playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
          //gameId: null,
          name: "Meraddison",
          gender: 1,
          level: 120,
          class: "Warlock",
          race: "Undead",
          realm: "area-52",
        },
        {
          characterId: "fake",
          //playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
          //gameId: null,
          name: "Temperance",
          gender: 1,
          level: 120,
          class: "Paladin",
          race: "Blood Elf",
          realm: "area-52",
        },
        {
          characterId: "fake",
          //playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
          //gameId: null,
          name: "Diabetty",
          gender: 1,
          level: 120,
          class: "Warrior",
          race: "Gnome",
          realm: "area-52",
        },
        {
          characterId: "fake",
          //playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
          //gameId: null,
          name: "Addison",
          gender: 1,
          level: 120,
          class: "rogue",
          race: "Gnome",
          realm: "steamwheedle-cartel",
        },
      ],
    };
  }
};
</script>

<style scoped>
</style>