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
        :specialization="item.specialization"
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
      characters: [],
    };
  },
  mounted: function() {
    let vm = this;
    this.$http
      .get(`/api/characters/roster/${vm.playerId}`)
      .then(function (response) {
        vm.characters = response.data.characters;
      })
      .catch(function (error) {
        console.log("had an error");
        console.log(error);
      });
  }
};
</script>

<style scoped>
</style>