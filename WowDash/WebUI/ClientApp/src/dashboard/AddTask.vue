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
              @input="updateAchievementNames"
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

            <!-- Game Data References -->
            <!-- <FormulateInput
                  type="autocomplete"
                  name="gameDataReferenceItems"
                  label="Search for a user"
                  :options="[
                    { value: 1, label: 'Jon Doe'},
                    { value: 2, label: 'Jane Roe'},
                    { value: 3, label: 'Bob Foe'},
                    { value: 4, label: 'Ben Cho'},
                  ]"
                /> -->
            <!-- <FormulateInput
              type="group"
              name="gameDataReferenceItems"
              :repeatable="true"
              label="Add References"
              add-label="+ Add Reference"
            >
              <div class="attendee">
                <FormulateInput
                  type="select"
                  name="type"
                  :options="gameDataTypes"
                  label="Data Type"
                />
              </div>
            </FormulateInput> -->

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
              label="Refresh Frequency"
              placeholder="How often does this repeat?"
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
        { value: "0", label: "achievement"},
        { value: "1", label: "item"},
        { value: "2", label: "item set"},
        { value: "3", label: "dungeon"},
        { value: "4", label: "boss"},
        { value: "5", label: "npc"},
        { value: "6", label: "pet"},
        { value: "7", label: "quest"},
        { value: "8", label: "zone"},
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
      characterList: [],

      achievementNames: [],
    };
  },
  methods: {
      addTask: function() {
          let vm = this;
          console.log(this.formInput);
          this.$http.post(`api/tasks/`, {
            playerId: vm.formInput.playerId,
            description: vm.formInput.description,
            notes: vm.formInput.notes,
            taskType: Number(vm.formInput.taskType),
            collectibleType: (vm.formInput.collectibleType == null) ? null : Number(vm.formInput.collectibleType),
            source: (vm.formInput.source == null) ? null: Number(vm.formInput.source),
            priority: Number(vm.formInput.priority),
            refreshFrequency: Number(vm.formInput.refreshFrequency),
            characters: vm.formInput.characters,
            gameDataReferenceItems: vm.formInput.gameDataReferenceItems
          })
          .then(function () {
            vm.$router.push("/");
          })
          .catch(function (error) {
            console.log('had an error');
            console.log(error);
          });
      },
      updateAchievementNames: function(event) {
        console.log('event emitted');
        console.log(event);
        let vm = this;
        this.$http.get(`/api/achievements/search/${event}`)
        .then(function (response) {
          console.log(response);
          // { gameId: X, type: X, subclass: X, description: X }
          vm.achievementNames = [];
          response.data.forEach((a) => {
            vm.achievementNames.push({ value: `{ "gameId": ${Number(a.id)}, "type": 0, "subclass": null, "description": "${a.name}" }`, label: a.name })
          })
          console.log(vm.achievementNames);
        })
        .catch(function (error) {
          console.log('had an error');
          console.log(error);
        });
      },
      appendGameDataReference: function(event) {
        console.log('event received by add task form');
        let parsedGdr = JSON.parse(event);
        console.log(parsedGdr);
        this.formInput.gameDataReferenceItems.push(parsedGdr);
      }
  },
  mounted: function() {
    let vm = this;
    this.$http
      .get(`/api/tasks/character-index/${vm.formInput.playerId}`)
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
  }
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

.formulate-input-element--checkbox {
  display: inline-block !important;
}
</style>