<template>
  <div>
    <div class="flex justify-start items-center mb-2">
      <router-link
        to="/"
        class="bg-red-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
      >
        <font-awesome-icon icon="caret-left" /> Back</router-link
      >
    </div>
    <div class="w-1/2 mx-auto dark-rounded">
      <div class="border-b border-gray-800 p-3">
        <h5 class="table-title">Add a Task</h5>
      </div>

      <FormulateForm @submit="addTask" v-model="formInput">
        <div class="bg-gray-800 text-gray-500 text-lg p-2 pl-4 pr-4 mb-3">
          <div class="w-3/4 space-y-4">
            <!-- Task Type -->
            <FormulateInput
              type="select"
              name="taskType"
              :options="taskTypes"
              label="Task Type"
              placeholder="What type of task is this?"
              validation="required"
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

            <!-- General Description -->
            <FormulateInput
              type="text"
              name="description"
              key="generalDescription"
              label="Description"
              v-if="formInput.taskType == '0'"
              placeholder="What's your goal?"
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

            <!-- Achievement Description -->
            <FormulateInput
              type="autocomplete"
              name="description"
              key="achievementDescription"
              label="Achievement Name"
              :options="achievementNames"
              v-if="formInput.taskType == '1'"
              placeholder="Start typing..."
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
              @input="debouncedUpdateAchievementNames"
              @add-game-data-reference="appendGameDataReference"
            />

            <!-- Collectible Type -->
            <FormulateInput
              type="select"
              v-if="formInput.taskType == '2'"
              key="collectibleTypeList"
              name="collectibleType"
              :options="collectibleTypes"
              label="Collectible Type"
              placeholder="What type of collectible?"
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

            <!-- Collectible Source -->
            <FormulateInput
              type="select"
              v-if="formInput.taskType == '2'"
              key="sourceList"
              name="source"
              :options="sources"
              label="Source"
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

            <!-- Game Data References -->
            <FormulateInput
              v-if="formInput.taskType == '2'"
              type="group"
              name="gameDataReferenceItems"
              label="Game Data"
              remove-label="X"
              :repeatable="true"
              add-label="+ Another reference"
              :outer-class="['pt-4']"
              :wrapper-class="[
                'inline-flex',
                'justify-center',
                'w-3/4',
                'items-center',
              ]"
              :label-class="['font-bold', 'w-1/2', 'text-right', 'mr-8', 'pr-2']"
              :grouping-class="[]"
              :group-repeatable-class="['mb-4']"
              :group-add-more-class="['inline-flex', 'bg-green-400', 'text-center', 'font-bold', 'pl-2', 'pr-2', 'rounded', 'text-gray-800']"
              :group-repeatable-remove-class="['inline-flex', 'bg-red-400', 'text-center', 'font-bold', 'pl-2', 'pr-2', 'rounded', 'text-gray-800']"
            >
              <template v-slot:default="groupProps">
                <div class="space-y-4">
                <FormulateInput
                  type="select"
                  name="type"
                  label="Data Type"
                  :options="gameDataTypes"
                  :wrapper-class="[
                'inline-flex',
                'justify-between',
                'w-full',
                'items-center',
              ]"
                  :input-class="[
                    'bg-gray-200',
                    'text-gray-700',
                    'rounded',
                    'p-2',
                  ]"
                  :label-class="[]"
                />
                <FormulateInput
                  type="autocomplete"
                  name="description"
                  :options="gameDataReferences[groupProps.index]"
                  v-if="formInput.taskType == '2'"
                  placeholder="Type a name..."
                  :wrapper-class="[
                'inline-flex',
                'justify-between',
                'w-full',
                'items-center',
              ]"
                  :input-class="[
                    'bg-gray-200',
                    'text-gray-700',
                    'rounded',
                    'p-2',
                  ]"
                  @input="debouncedUpdateGameDataReference($event, groupProps.index)"
                  @add-game-data-reference="
                    formatGameDataReference($event, groupProps.index)
                  "
                />
                </div>
              </template>
            </FormulateInput>

            <div class="w-3/4 space-y-4">

            <!-- Add Characters -->
            <FormulateInput
              type="checkbox"
              name="characters"
              :options="characterList"
              label="Assign Characters"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['text-gray-300', 'rounded', 'p-2']"
            />

            <!-- Priority -->
            <FormulateInput
              type="select"
              name="priority"
              :options="priorities"
              label="Priority"
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

            <!-- Refresh Frequency -->
            <FormulateInput
              type="select"
              name="refreshFrequency"
              :options="refreshFrequencies"
              label="Repeats"
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

            <!-- Notes -->
            <FormulateInput
              type="textarea"
              name="notes"
              label="Notes"
              placeholder="Anything else you need to remember to complete this?"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="[
                'bg-gray-200',
                'text-gray-700',
                'rounded',
                'p-2',
                'h-32',
              ]"
            />
          </div>
        </div>
        <div class="flex justify-end mr-2">
          <button
            class="bg-blue-400 text-gray-800 p-2 mb-3 font-bold text-center border-gray-800 rounded shadow"
          >
            Add Task
          </button>
        </div>
      </FormulateForm>
    </div>
    <div id="debug" class="text-lg text-white">
      {{ formInput }}
    </div>
  </div>
</template>

<script>
export default {
  name: "AddTask",
  data() {
    return {
      formInput: {
        playerId: localStorage.playerId,
        description: "",
        taskType: null,
        collectibleType: null,
        priority: "2",
        refreshFrequency: null,
        source: null,
        notes: "",
        characters: [],
        gameDataReferenceItems: [
          // { gameId: X, type: X, subclass: X, description: X }
        ],
      },

      // Form values
      collectibleTypes: [
        { value: "0", label: "Gear and Items" },
        { value: "1", label: "Dungeon Sets" },
        { value: "2", label: "Mounts" },
        { value: "3", label: "Battle Pets" },
      ],
      gameDataTypes: [
        { value: "1", label: "Item" },
        { value: "2", label: "Item Set" },
        { value: "3", label: "Dungeon" },
        { value: "4", label: "Boss" },
        { value: "8", label: "Zone" },
      ],
      priorities: [
        { value: "0", label: "Lowest" },
        { value: "1", label: "Low" },
        { value: "2", label: "Medium" },
        { value: "3", label: "High" },
        { value: "4", label: "Highest" },
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
      taskTypes: [
        { value: "0", label: "General" },
        { value: "1", label: "Achievement" },
        { value: "2", label: "Collectible" },
      ],
      characterList: [
        { value: "fake1", label: "Scully" },
        { value: "fake2", label: "Chakwas" },
        { value: "fake3", label: "Temperance" }
      ],
      achievementNames: [],
      gameDataReferences: [[]],
    };
  },
  methods: {
    addTask: function () {
      let vm = this;
      console.log(this.formInput);

      // Set description value before sending for collectible types
      if (this.formInput.taskType == "2") {
        let newDescription;
        // If there's an item in here, set that as the desc
        if (this.formInput.gameDataReferenceItems.some((r) => r.type == "1")) {
          newDescription = this.formInput.gameDataReferenceItems.find((r) => r.type == "1").description;
        }
        // Else if there's an item set, set THAT as the desc
        else if (this.formInput.gameDataReferenceItems.some((r) => r.type == "2")) {
          newDescription = this.formInput.gameDataReferenceItems.find((r) => r.type == "2").description;
        }
        // Else if there's a boss, set THAT
        else if (this.formInput.gameDataReferenceItems.some((r) => r.type == "3")) {
          newDescription = this.formInput.gameDataReferenceItems.find((r) => r.type == "3").description;
        }
        // Else if there's a dungeon, set THAT
        else if (this.formInput.gameDataReferenceItems.some((r) => r.type == "4")) {
          newDescription = this.formInput.gameDataReferenceItems.find((r) => r.type == "4").description;
        }
        // Else set the first thing in the list
        else {
          if (this.formInput.gameDataReferenceItems[0] != null) {
            newDescription = this.formInput.gameDataReferenceItems[0].description;
          } else {
            newDescription = "Untitled Task"
          }
        }

        this.formInput.description = newDescription;
      }

      // Change number string values to ints
      this.formInput.gameDataReferenceItems.forEach(gdr => {
        gdr.type = Number(gdr.type);
        gdr.gameId = Number(gdr.gameId);
      });

      console.log(this.formInput.gameDataReferenceItems);
      console.log(this.formInput);

      this.$http
        .post(`api/tasks/`, {
          playerId: vm.formInput.playerId,
          description: vm.formInput.description,
          notes: vm.formInput.notes,
          taskType: Number(vm.formInput.taskType),
          collectibleType:
            vm.formInput.collectibleType == null
              ? null
              : Number(vm.formInput.collectibleType),
          source:
            vm.formInput.source == null ? null : Number(vm.formInput.source),
          priority: Number(vm.formInput.priority),
          refreshFrequency: Number(vm.formInput.refreshFrequency),
          characters: vm.formInput.characters,
          gameDataReferenceItems: vm.formInput.gameDataReferenceItems,
        })
        .then(function () {
          vm.$router.push("/");
        })
        .catch(function (error) {
          console.log("had an error");
          console.log(error);
        });
    },
    updateAchievementNames: function (event) {
      let vm = this;
      vm.$http
        .get(`/api/achievements/search/${event}`)
        .then(function (response) {
          // { gameId: X, type: X, subclass: X, description: X }
          vm.achievementNames = [];
          response.data.forEach((a) => {
            vm.achievementNames.push({
              value: `{ "gameId": ${Number(
                a.id
              )}, "type": 0, "subclass": null, "description": "${a.name}" }`,
              label: a.name,
            });
          });
        })
        .catch(function (error) {
          console.log("had an error");
          console.log(error);
        });
    },
    // Hack the hell out of this application for science
    updateGameDataReference: function (event, index) {
      console.log("we made it to update game data reference at index " + index);
      console.log(event);
      let vm = this;
      // find the gdr with matching description
      // find the type associated with it
      let match = vm.formInput.gameDataReferenceItems.find(
        (gdr) => gdr.description == event
      );
      console.log(match.type);
      let url = "";
      switch (Number(match.type)) {
        // pick the right api endpoint based on type
        case 1: // item
          url = `/api/items/search/${event}`;
          break;
        case 2: // item set
          url = `/api/item-sets/search/${event}`;
          break;
        case 3: // dungeon
          url = `/api/dungeons/search/${event}`;
          break;
        case 4: // boss
          url = `/api/bosses/search/${event}`;
          break;
        case 8: // zone
          url = `/api/zones/search/${event}`;
          break;
        default:
          // anything else
          break;
      }

      console.log(url);
      // send it off and assign it to the list
      vm.$http.get(url)
      .then(function (response) {
        console.log(response.data);
        vm.gameDataReferences.splice(index, 1, []);   // have to do this instead of gdr[index] = [];
        console.log(vm.gameDataReferences);
         response.data.forEach((a) => {
            vm.gameDataReferences[index].push({
              value: `{ "gameId": ${Number(
                a.id
              )}, "subclass": "${a.subclass}", "description": "${a.name}" }`,
              label: a.name,
            });
          });
      })
      .catch(function (error) {
        console.log('had an error');
        console.log(error);
      });
    },
    // Called by achievement name
    appendGameDataReference: function (event) {
      console.log("event received by add task form");
      let parsedGdr = JSON.parse(event);
      console.log(parsedGdr);
      this.formInput.gameDataReferenceItems.push(parsedGdr);
    },
    formatGameDataReference: function (event, index) {
      console.log("modifying data for " + index);
      console.log(event);
      console.log('the gdr at that index is');
      console.log(this.formInput.gameDataReferenceItems[index]);
      let parsedGdr = JSON.parse(event);
      console.log('the parsed gdr at that index is');
      console.log(parsedGdr);
      this.formInput.gameDataReferenceItems[index].gameId = parsedGdr.gameId;
      this.formInput.gameDataReferenceItems[index].subclass = parsedGdr.subclass == "undefined" ? null : parsedGdr.subclass;
      console.log('now after adding stuff, the item looks like');
      console.log(this.formInput.gameDataReferenceItems[index]);
    },
  },
  created: function () {
    this.debouncedUpdateAchievementNames = this.$_.debounce(
      this.updateAchievementNames,
      400
    );
    this.debouncedUpdateGameDataReference = this.$_.debounce(
      this.updateGameDataReference,
      400
    );
  },
  mounted: function () {
    let vm = this;
    this.$http
      .get(`/api/tasks/character-index/${vm.formInput.playerId}`)
      .then(function (response) {
        if (response.status == 200) {
          vm.characterList = [];
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
.standard-input {
  @apply bg-gray-400;
  @apply text-gray-800;
  @apply rounded;
  @apply p-2;
}

/* .formulate-input-element--checkbox {
  display: inline-block !important;
} */
</style>