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
              type="text"
              name="description"
              key="achievementDescription"
              label="Achievement Name"
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
        isFavourite: false
      },

      // Form values
      collectibleTypes: [
        { value: "0", label: "Gear and Items" },
        { value: "1", label: "Dungeon Sets" },
        { value: "2", label: "Mounts" },
        { value: "3", label: "Battle Pets" },
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
            characters: vm.formInput.characters
          })
          .then(function (response) {
            vm.$router.push("/");
          })
          .catch(function (error) {
            console.log('had an error');
            console.log(error);
          });
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