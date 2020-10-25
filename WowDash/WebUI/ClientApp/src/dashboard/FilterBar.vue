<template>
  <FormulateForm
    v-model="formModel"
    @submit="$emit('update-query', filterModel)"
  >
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
                :options="taskTypes"
              />
            </div>
          </div>
          <div>
            <h3 class="filter-bar-category">Collectible</h3>
            <div class="checkbox-text">
              <FormulateInput
                type="checkbox"
                name="collectibleType"
                :options="collectibleTypes"
              />
            </div>
          </div>
          <div>
            <h3 v-show="characterList.length > 0" class="filter-bar-category">
              Character
            </h3>
            <div class="checkbox-text">
              <FormulateInput
                type="checkbox"
                name="characterId"
                :options="characterList"
              />
            </div>
          </div>
          <div>
            <h3 v-show="dungeonList.length > 0" class="filter-bar-category">
              Dungeon
            </h3>
            <div class="checkbox-text">
              <FormulateInput
                type="checkbox"
                name="dungeonId"
                :options="dungeonList"
              />
            </div>
          </div>
          <!-- You get the gist... have to build out logic using Vue Formulate later anyway, it'll all be gutted -->
          <div>
            <h3 v-show="zoneList.length > 0" class="filter-bar-category">
              Zone
            </h3>
            <div class="checkbox-text">
              <FormulateInput
                type="checkbox"
                name="zoneId"
                :options="zoneList"
              />
            </div>
          </div>
          <div>
            <h3 class="filter-bar-category">Source</h3>
            <div class="checkbox-text">
              <FormulateInput
                type="checkbox"
                name="source"
                :options="sources"
              />
            </div>
          </div>
          <div>
            <h3 class="filter-bar-category">Refresh</h3>
            <div class="checkbox-text">
              <FormulateInput
                type="checkbox"
                name="refreshFrequency"
                :options="refreshFrequencies"
              />
            </div>
          </div>
          <div>
            <h3 class="filter-bar-category">Other</h3>
            <div class="checkbox-text">
              <FormulateInput
                name="isFavourite"
                type="checkbox"
                label="Favourites Only"
              />
            </div>
            <div class="checkbox-text">
              <FormulateInput
                name="onlyActiveAttempts"
                type="checkbox"
                label="Active Attempts Remaining"
              />
            </div>
          </div>
          <div>
            <h3 class="filter-bar-category">Sort By</h3>
            <FormulateInput
              name="sortBy"
              :options="{
                alpha_asc: 'Alphabet A-Z',
                alpha_desc: 'Alphabet Z-A',
                priority_asc: 'Priority Low-High',
                priority_desc: 'Priority High-Low',
              }"
              type="select"
              placeholder="Select an option"
              :input-class="['bg-gray-400 rounded w-auto p-1 m-2 mb-8 text-sm']"
            />
          </div>
          <button
            class="bg-blue-400 w-3/4 font-bold text-center p-1 m-2 border-gray-800 rounded shadow"
          >
            Apply Filters</button
          ><br />
          <button
            class="bg-gray-800 w-3/4 font-bold text-center text-gray-400 p-1 m-2 border-blue-400 rounded shadow"
            @click.prevent="clearFilters"
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
        sortBy: "", // priority_asc priority_desc alpha_asc alpha_desc
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
      characterList: [],
      dungeonList: [],
      zoneList: [],
    };
  },
  computed: {
    filterModel: function () {
      let model = {};
      model.playerId = this.formModel.playerId;
      if (this.formModel.characterId.length) {
        model.characterId = this.formModel.characterId.join("|");
      }
      if (this.formModel.taskType.length) {
        model.taskType = this.formModel.taskType.join("|");
      }
      if (this.formModel.collectibleType.length) {
        model.collectibleType = this.formModel.collectibleType.join("|");
      }
      if (this.formModel.dungeonId.length) {
        model.dungeonId = this.formModel.dungeonId.join("|");
      }
      if (this.formModel.zoneId.length) {
        model.zoneId = this.formModel.zoneId.join("|");
      }
      if (this.formModel.refreshFrequency.length) {
        model.refreshFrequency = this.formModel.refreshFrequency.join("|");
      }
      if (this.formModel.isFavourite) {
        model.isFavourite = this.formModel.isFavourite;
      }
      if (this.formModel.onlyActiveAttempts) {
        model.onlyActiveAttempts = this.formModel.onlyActiveAttempts;
      }
      if (this.formModel.sortBy != null && this.formModel.sortBy != "") {
        model.sortBy = this.formModel.sortBy;
      }
      return model;
    },
  },
  methods: {
    clearFilters: function () {
      this.formModel = {
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
        sortBy: "", // priority_asc priority_desc alpha_asc alpha_desc
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
          response.data.forEach((d) =>
            vm.dungeonList.push({ value: d.gameId, label: d.name })
          );
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
          response.data.forEach((z) =>
            vm.zoneList.push({ value: z.gameId, label: z.name })
          );
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
          response.data.forEach((ch) =>
            vm.characterList.push({ value: ch.characterId, label: ch.name })
          );
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