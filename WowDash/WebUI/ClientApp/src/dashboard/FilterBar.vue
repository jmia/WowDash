<template>
  <FormulateForm v-model="formModel">
    <div class="dark-rounded p-2">
      <div class="flex flex-row items-start">
        <div class="flex-1 text-center">
          <h5 class="table-title">Filters</h5>
          <div>
            <h3 class="filter-bar-category">Task</h3>
            <div class="checkbox-text">
            <FormulateInput
              type="checkbox"
              name="taskType"
              :options="taskTypes" />
            </div>
          </div>
          <div>
            <h3 class="filter-bar-category">Collectible</h3>
            <div class="checkbox-text">
              <FormulateInput
              type="checkbox"
              name="collectibleType"
              :options="collectibleTypes" />
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
                    v-model="formModel.isFavourite"
                    class="checkbox-size"
                  />
                </div>
                <label for="favouritesOnly" class="ml-2">
                  Favourites Only</label
                >
              </div>
            </div>
            <div class="checkbox-text">
              <div class="flex items-start">
                <!-- This little blank space trick for aligning checkboxes 
            is from: https://twitter.com/adamwathan/status/1217864323466432516 -->
                <div class="flex items-center">
                  &#8203;
                  <input
                    type="checkbox"
                    id="activeAttempts"
                    v-model="formModel.onlyActiveAttempts"
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
              v-model="formModel.sortBy"
            >
              <option value="alpha_asc">Alphabet A-Z</option>
              <option value="alpha_desc">Alphabet Z-A</option>
              <option value="priority_asc">Priority Low-High</option>
              <option value="priority_desc">Priority High-Low</option>
            </select>
          </div>
          <button
            class="bg-blue-400 w-3/4 font-bold text-center p-1 m-2 border-gray-800 rounded shadow"
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
  </FormulateForm>
</template>

<script>
export default {
  name: "FilterBar",
  data() {
    return {
      formModel: {
        // entries are separated by |
        playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
        characterId: [],
        taskType: [],
        collectibleType: [], // int
        dungeonId: [], // int
        zoneId: [], // int
        refreshFrequency: [], // int
        isFavourite: false,
        onlyActiveAttempts: false,
        sortBy: "alpha_asc", // priority_asc priority_desc alpha_asc alpha_desc
      },
      taskTypes: [
        { value: "0", label: "General" },
        { value: "1", label: "Achievement" },
        { value: "2", label: "Collectible" },
      ],
      collectibleTypes: [
        { value: "0", label: "Gear and Items" },
        { value: "1", label: "Dungeon Sets" },
        { value: "2", label: "Mounts" },
        { value: "3", label: "Battle Pets" },
      ],
      refreshFrequencies: [
        { value: "0", label: "Never" },
        { value: "1", label: "Daily" },
        { value: "2", label: "Weekly" },
      ],
      sources: [
        { value: "0", label: "Dungeon" },
        { value: "1", label: "Quest" },
        { value: "2", label: "Vendor" },
        { value: "3", label: "World Drop" },
        { value: "4", label: "Other" },
      ],
      characterList: [
        
      ],
      dungeonList: [],
      zoneList: [],
    };
  },
  computed: {
    filterModel: function () {
      return {
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
      };
    },
  },
  mounted: function () {
    let vm = this;
    // TODO: Change these to grab current player

    // Populate list of dungeons
    this.$http
      .get(`/api/tasks/dungeon-index/${vm.formModel.playerId}`)
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
      .get(`/api/tasks/zone-index/${vm.formModel.playerId}`)
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
      .get(`/api/tasks/character-index/${vm.formModel.playerId}`)
      .then(function (response) {
        if (response.status == 200) {
          console.log(response.data);
          // TODO: This needs to be a label & value
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

<style>
.filter-bar-category {
  @apply font-bold;
  @apply text-2xl;
  @apply text-gray-600;
}

.formulate-input-element {
  display: inline-block !important;
}

</style>