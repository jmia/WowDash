<template>
  <div class="dark-rounded p-2">
    <div class="flex flex-row items-start">
      <div class="flex-1 text-center">
        <h5 class="table-title">Filters</h5>
        <div>
          <!-- TODO: Refactor these to components probably? -->
          <h3 class="filter-bar-category">Task</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="type in taskTypes"
              v-bind:key="type.id"
            >
              <!-- This little blank space trick for aligning checkboxes 
            is from: https://twitter.com/adamwathan/status/1217864323466432516 -->
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="type.value"
                  v-bind:value="type.id"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="type.value" class="ml-2">{{
                type.display
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Collectible</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="type in collectibleTypes"
              v-bind:key="type.id"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="type.value"
                  v-bind:value="type.id"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="type.value" class="ml-2">{{
                type.display
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Character</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="character in characterList"
              v-bind:key="character.characterId"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="character.characterId"
                  v-bind:value="character.characterId"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="character.characterId" class="ml-2">{{
                character.name
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Dungeon</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="dungeon in dungeonList"
              v-bind:key="dungeon.gameId"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="dungeon.gameId"
                  v-bind:value="dungeon.gameId"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="dungeon.gameId" class="ml-2">{{
                dungeon.name
              }}</label>
            </div>
          </div>
        </div>
        <!-- You get the gist... have to build out logic using Vue Formulate later anyway, it'll all be gutted -->
        <div>
          <h3 class="filter-bar-category">Zone</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="zone in zoneList"
              v-bind:key="zone.gameId"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="zone.gameId"
                  v-bind:value="zone.gameId"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="zone.gameId" class="ml-2">{{
                zone.name
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Source</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="source in sources"
              v-bind:key="source.id"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="source.value"
                  v-bind:value="source.id"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="source.value" class="ml-2">{{
                source.display
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Refresh</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="frequency in refreshFrequencies"
              v-bind:key="frequency.id"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="frequency.value"
                  v-bind:value="frequency.id"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="frequency.value" class="ml-2">{{
                frequency.display
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Other</h3>
          <div class="checkbox-text">
            <div class="flex items-start">
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  id="favouritesOnly"
                  v-model="filterModel.isFavourite"
                  class="checkbox-size"
                />
              </div>
              <label for="favouritesOnly" class="ml-2"> Favourites Only</label>
            </div>
          </div>
          <div class="checkbox-text">
            <div class="flex items-start">
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  id="activeAttempts"
                  v-model="filterModel.onlyActiveAttempts"
                  class="checkbox-size"
                />
              </div>
              <label for="activeAttempts" class="ml-2">
                Active Attempts Remaining</label
              >
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Sort By</h3>
          <select
            name="cars"
            id="cars"
            class="bg-gray-400 rounded w-auto p-1 m-2 mb-8 text-sm"
            v-model="filterModel.sortBy"
          >
            <option value="alpha_asc">Alphabet A-Z</option>
            <option value="alpha_desc">Alphabet Z-A</option>
            <option value="priority_asc">Priority Low-High</option>
            <option value="priority_desc">Priority High-Low</option>
          </select>
        </div>
        <button
          class="bg-blue-400 w-3/4 font-bold text-center p-1 m-2 border-gray-800 rounded shadow"
          @click="$emit('update-query', filterModel)"
        >
          Apply Filters</button
        ><br />
        <button
          class="bg-gray-800 w-3/4 font-bold text-center text-gray-400 p-1 m-2 border-blue-400 rounded shadow"
        >
          Clear Filters
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "FilterBar",
  data() {
    return {
      //     /// The characters to filter on, separated by "|".
      //     public string CharacterId { get; set; }
      //     /// The task types to filter on, separated by "|".
      //     public string TaskType { get; set; }
      //     /// The collection types to filter on, separated by "|".
      //     public string CollectibleType { get; set; }
      //     /// The dungeon IDs to filter on, separated by "|".
      //     public string DungeonId { get; set; }
      //     /// The zone IDs to filter on, separated by "|".
      //     public string ZoneId { get; set; }
      //     /// The refresh frequencies to filter on, separated by "|".
      //     public string RefreshFrequency { get; set; }
      //     public bool IsFavourite { get; set; } = false;
      //     /// <summary>
      //     /// Whether the filter should return only tasks with assigned
      //     /// task characters that are active for this lockout (refresh frequency).
      //     /// </summary>
      //     public bool OnlyActiveAttempts { get; set; } = false;
      //     /// The property on which to sort,
      //     /// can be "priority" or "alpha",
      //     /// suffixed with "_asc" or "_desc."
      //     public string SortBy { get; set; }
      filterModel: {
        // entries are separated by |
        playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
        characterId: "", // guid
        taskType: "", // int
        collectibleType: "", // int
        dungeonId: "", // int
        zoneId: "", // int
        refreshFrequency: "", // int
        isFavourite: false,
        onlyActiveAttempts: false,
        sortBy: "alpha_asc", // priority_asc priority_desc alpha_asc alpha_desc
      },
      taskTypes: [
        { id: 0, value: "general", display: "General" }, // TODO: Remove this domain logic from the front-end
        { id: 1, value: "achievement", display: "Achievement" },
        { id: 2, value: "collectible", display: "Collectible" },
      ],
      collectibleTypes: [
        { id: 0, value: "item", display: "Gear and Items" },
        { id: 1, value: "itemSet", display: "Dungeon Sets" },
        { id: 2, value: "mount", display: "Mounts" },
        { id: 3, value: "pet", display: "Battle Pets" },
      ],
      refreshFrequencies: [
        { id: 0, value: "never", display: "Never" },
        { id: 1, value: "daily", display: "Daily" },
        { id: 2, value: "weekly", display: "Weekly" },
      ],
      sources: [
        { id: 0, value: "dungeon", display: "Dungeon" },
        { id: 1, value: "quest", display: "Quest" },
        { id: 2, value: "vendor", display: "Vendor" },
        { id: 3, value: "worldDrop", display: "World Drop" },
        { id: 4, value: "other", display: "Other" },
      ],
      characterList: [
        {
          id: "cbf62c90-5358-4059-a04d-f798cb2d300c",
          value: "cbf62c90-5358-4059-a04d-f798cb2d300c",
          display: "Scully",
        },
        {
          id: "92100f04-7a0c-4138-809b-73a8cdaf7d6b",
          value: "92100f04-7a0c-4138-809b-73a8cdaf7d6b",
          display: "Chakwas",
        },
        {
          id: "859cfab5-725c-4310-b5e4-9d8e92533109",
          value: "859cfab5-725c-4310-b5e4-9d8e92533109",
          display: "Temperance",
        },
        {
          id: "4f42b206-b106-42c2-9d3e-29ded8128b75",
          value: "4f42b206-b106-42c2-9d3e-29ded8128b75",
          display: "Meraddison",
        },
      ],
      dungeonList: [],
      zoneList: [],
    };
  },
  mounted: function () {
    let vm = this;
    // TODO: Change these to grab current player

    // Populate list of dungeons
    this.$http
      .get(`/api/tasks/dungeon-index/${vm.filterModel.playerId}`)
      .then(function (response) {
        if (response.status == 200) {
          vm.dungeonList = response.data;
        }
      })
      .catch(function (error) {
        console.log("had an error");
        console.log(error);
      });
    // Populate list of zones
    this.$http
      .get(`/api/tasks/zone-index/${vm.filterModel.playerId}`)
      .then(function (response) {
        if (response.status == 200) {
          vm.zoneList = response.data;
        }
      })
      .catch(function (error) {
        console.log("had an error");
        console.log(error);
      });
    // Populate character roster
    this.$http
      .get(`/api/tasks/character-index/${vm.filterModel.playerId}`)
      .then(function (response) {
        if (response.status == 200) {
          console.log(response.data);
          vm.characterList = response.data;
        }
      })
      .catch(function (error) {
        console.log("had an error");
        console.log(error);
      });
  },
};
</script>

<style scoped>
.filter-bar-category {
  @apply font-bold;
  @apply text-2xl;
  @apply text-gray-600;
}
</style>