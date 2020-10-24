<template>
  <div
    class="bg-gray-800 p-2 pl-4 pr-4 flex flex-col justify-between leading-normal mb-3"
  >
    <div class="text-gray-500">
      <div class="w-full inline-flex justify-between items-center">
        <div>
          <div class="inline-flex w-auto font-bold text-2xl text-gray-300 mb-1">
            <a
              :href="descriptionHref"
              class="hover:underline"
              target="_blank"
              >{{ description }}</a
            >
            <sub
              v-if="priority != 2"
              class="ml-1"
              :class="'priority-' + priorities[priority]"
              ><font-awesome-icon
                :icon="priorityIcon"
                :title="'priority: ' + priorities[priority]"
            /></sub>
          </div>
        </div>
        <div class="w-auto">
          <button class="mr-4" @click="$emit('set-favourite')">
            <span class="text-yellow-400"
              ><font-awesome-icon :icon="['fas', 'star']" v-if="isFavourite"
            /><font-awesome-icon :icon="['far', 'star']" v-else
            /></span>
          </button>
          <button class="mr-4">
            <span class="text-blue-400"><font-awesome-icon icon="edit" /></span>
          </button>
          <button class="mr-4">
            <span class="text-blue-400"
              ><font-awesome-icon icon="trash"
            /></span>
          </button>
          <button
            class="bg-green-400 text-gray-800 font-bold text-center p-1 border-gray-800 rounded shadow"
          >
            Got it!
          </button>
        </div>
      </div>
      <div class="inline-flex items-start mb-2 text-xs text-center">
        <div
          v-if="collectibleType != null"
          class="text-gray-400 border-gray-400 border rounded-full p-1 pr-2 pl-2 w-auto uppercase ml-1 cursor-default"
        >
          {{ collectibleTypes[collectibleType] }}
        </div>
        <div
          v-else-if="taskType == 1"
          class="text-gray-400 border-gray-400 border rounded-full p-1 pr-2 pl-2 w-auto uppercase ml-1 cursor-default"
        >
          {{ taskTypes[taskType] }}
        </div>
        <div
          v-if="refreshFrequency != 0"
          class="border rounded-full p-1 pr-2 pl-2 w-auto uppercase ml-1 cursor-default"
          :class="refreshFrequencyStyle"
        >
          {{ refreshFrequencies[refreshFrequency] }}
        </div>
      </div>
      <div class="mb-2">
        <TaskGameDataReference
          v-for="(item, index) in referenceLinkList"
          :item="item"
          :index="index"
          :key="item.id"
          :gameId="item.gameId"
          :dataType="item.type"
          :subclass="item.subclass"
          :description="item.description"
        />
      </div>

      <div>
        {{ notes }}
      </div>

      <template v-if="characters.length">
        <hr class="border-b-1 border-gray-700 my-2 mx-4" />

        <div class="text-xs uppercase">Character Attempts</div>

        <div
          class="inline-flex flex-wrap items-start text-sm mb-2 mt-2 text-center"
        >
          <TaskCharacterButton
            v-for="(item, index) in characters"
            :item="item"
            :index="index"
            :key="item.id"
            :taskId="taskId"
            :characterId="item.characterId"
            :name="item.name"
            :playableClass="item.class"
            :isActive="item.isActive"
            @mark-attempt="markAttempt(item)"
          />
        </div>
      </template>
    </div>
  </div>
</template>

<script>
import TaskCharacterButton from "./TaskCharacterButton";
import TaskGameDataReference from "./TaskGameDataReference";

export default {
  name: "TaskCard",
  components: {
    TaskCharacterButton,
    TaskGameDataReference,
  },
  data() {
    return {
      characters: [
        // If the structure of this changes (e.g. "id" is "characterId"),
        // check the component info above and the template in TaskCharacterButton.vue
      ],
      // Super tightly bound to back end logic, will need refactoring
      // maybe just change to a get request for a view model collection of all these
      taskTypes: ["general", "achievement", "collectible"],
      collectibleTypes: ["item", "gear set", "mount", "battle pet"],
      priorities: ["lowest", "low", "medium", "high", "highest"],
      refreshFrequencies: ["never", "daily", "weekly"],
      sources: ["dungeon", "quest", "vendor", "world drop", "other"],
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
    taskId: {
      type: String,
      required: true,
    },
    description: {
      type: String,
    },
    gameDataReferences: {
      type: Array,
    },
    isFavourite: {
      type: Boolean,
    },
    notes: {
      type: String,
    },
    taskType: {
      type: Number,
    },
    collectibleType: {
      type: Number,
    },
    source: {
      type: Number,
    },
    priority: {
      type: Number,
    },
    refreshFrequency: {
      type: Number,
    },
  },
  computed: {
    // Dynamic Wowhead tooltip & link generation
    descriptionHref: function () {
      var baseUrl = "https://www.wowhead.com/";

      var matchingDescriptions = this.gameDataReferences.filter(
        (g) => g.description.toLowerCase() == this.description.toLowerCase()
      );

      if (matchingDescriptions.length) {
        var match = matchingDescriptions[0];
        switch (match.type) {
          case 0: // achievement
            return (
              baseUrl +
              (Number.isInteger(match.gameId)
                ? "achievement=" + match.gameId
                : "search?q=" + encodeURI(match.description.toLowerCase()))
            );
          case 1: // item
            return (
              baseUrl +
              (Number.isInteger(match.gameId)
                ? "item=" + match.gameId
                : "search?q=" + encodeURI(match.description.toLowerCase()))
            );
          case 2: // item set
            return (
              baseUrl +
              (Number.isInteger(match.gameId)
                ? "item-set=" + match.gameId
                : "search?q=" + encodeURI(match.description.toLowerCase()))
            );
          default:
            // everything else
            break;
        }
      }
      return baseUrl + "search?q=" + encodeURI(this.description.toLowerCase());
    },
    // Change colour and style based on priority
    priorityIcon: function () {
      if (this.priority > 2) {
        return "caret-up";
      } else if (this.priority < 2) {
        return "caret-down";
      } else {
        return null;
      }
    },
    refreshFrequencyStyle: function () {
      return this.refreshFrequencies[this.refreshFrequency];
    },
    // Save the TaskGameDataReference the trouble of deciding which things
    // to display, and send in a list of only the ones we want links for
    // This is all veryyyy brute force, definitely some room for elegance here
    referenceLinkList: function () {
      var result;

      // GameDataTypes: 3,4,5,7,8. Exclude 7 if there's no 1 in the list anywhere
      // i.e. If it's a quest, we want it passed as a link if the primary
      // goal is an item and not the quest itself
      if (this.gameDataReferences.some((r) => r.type == 1)) {
        result = this.gameDataReferences.filter((r) =>
          [3, 4, 5, 7, 8].includes(r.type)
        );
      } else {
        result = this.gameDataReferences.filter((r) =>
          [3, 4, 5, 8].includes(r.type)
        );
      }
      return result;
    },
  },
  methods: {
    markAttempt: function (character) {
      let vm = this;
      if (character.isActive) {
        // Set as inactive
        this.$http
          .patch(`/api/task-characters/complete`, {
            characterId: character.characterId,
            taskId: vm.taskId,
          })
          .then(function (response) {
            if (response.status == 204) {
              console.log("we did it. refresh the page.");
              vm.$http
                .get(`/api/task-characters/task/${vm.taskId}`)
                .then(function (response) {
                  vm.characters = response.data.characters;
                })
                .catch(function (error) {
                  console.log("had an error");
                  console.log(error);
                });
            }
          })
          .catch(function (error) {
            console.log("had an error");
            console.log(error);
          });
      } else {
        // Set as active
        this.$http
          .patch(`/api/task-characters/revert`, {
            characterId: character.characterId,
            taskId: vm.taskId,
          })
          .then(function (response) {
            if (response.status == 204) {
              console.log("we did it. refresh the page.");
              vm.$http
                .get(`/api/task-characters/task/${vm.taskId}`)
                .then(function (response) {
                  vm.characters = response.data.characters;
                })
                .catch(function (error) {
                  console.log("had an error");
                  console.log(error);
                });
            }
          })
          .catch(function (error) {
            console.log("had an error");
            console.log(error);
          });
      }
    },
  },
  mounted: function () {
    let vm = this;
    this.$http
      .get(`/api/task-characters/task/${vm.taskId}`)
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
/* Priority */
.priority-lowest {
  @apply text-blue-400;
}
.priority-low {
  @apply text-green-400;
}
.priority-high {
  @apply text-yellow-400;
}
.priority-highest {
  @apply text-red-400;
}

/* Refresh Frequency */
.daily {
  @apply text-purple-400;
  @apply border-purple-400;
}
.weekly {
  @apply text-blue-400;
  @apply border-blue-400;
}
</style>