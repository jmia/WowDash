<template>
  <div class="hover:underline font-bold flex">
    <div class="w-4 h-4 mr-2 text-gray-400 text-center">
      <font-awesome-icon :icon="linkIcon" />
    </div>
    <a :href="linkHref" target="_blank">{{ description }}</a>
  </div>
</template>

<script>
export default {
  name: "TaskGameDataReference",
  data() {
    return {
      gameDataTypes: [
        "achievement",
        "item",
        "item set",
        "dungeon",
        "boss",
        "npc",
        "pet",
        "quest",
        "zone",
      ],
    };
  },
  props: {
    gameId: {
      type: Number,
    },
    dataType: {
      type: Number,
    },
    subclass: {
      type: String,
    },
    description: {
      type: String,
    },
  },
  computed: {
    linkIcon: function () {
      switch (this.dataType) {
        case 3: // dungeon
          return "dungeon"; // or map-marked-alt
        case 4: // boss
          return "user-secret";
        case 5: // npc
          return "male";
        case 7: // quest
          return "exclamation";
        case 8: // zone
          return "map-marked-alt";
        default:
          // anything else
          return "skull-crossbones";
      }
    },
    linkHref: function () {
      var baseUrl = "https://www.wowhead.com/";
      switch (this.dataType) {
        case 3: // dungeon
        case 8: // zone
          return (
            baseUrl +
            this.description
              .replace(/ /g, "-") // kebab-case
              .replace(/[:']/g, "") // no funny business
              .toLowerCase()
          );
        case 4: // boss
          return (
            baseUrl + "search?q=" + encodeURI(this.description.toLowerCase())
          );
        case 5: // npc
          return (
            baseUrl +
            (Number.isInteger(this.gameId)
              ? "npc=" + this.gameId
              : "search?q=" + encodeURI(this.description.toLowerCase()))
          );
        case 7: // quest
          return (
            baseUrl +
            (Number.isInteger(this.gameId)
              ? "quest=" + this.gameId
              : "search?q=" + encodeURI(this.description.toLowerCase()))
          );
        default:
          // anything else
          return (
            baseUrl + "search?q=" + encodeURI(this.description.toLowerCase())
          );
      }
    },
  },
};
</script>

<style scoped>
</style>