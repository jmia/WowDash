<template>
  <div>
    <!-- Action Buttons -->
    <div class="flex justify-end items-center mb-2">
      <router-link
        to="/add-task"
        class="bg-green-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
        ><font-awesome-icon icon="plus" /> Add New Task</router-link
      >
      <button
        class="bg-blue-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
        @click="refreshDailies"
      >
        <font-awesome-icon icon="sync-alt" /> Refresh Dailies
      </button>
      <button
        class="bg-blue-400 p-2 font-bold text-center border-gray-800 rounded shadow"
        @click="refreshWeeklies"
      >
        <font-awesome-icon icon="sync-alt" /> Refresh Weeklies
      </button>
    </div>
    <!-- /Action Buttons -->

    <!--Card List-->
    <div class="w-full dark-rounded">
      <div class="border-b border-gray-800 p-3">
        <h5 class="table-title">Tasks</h5>
      </div>

      <div
        v-if="tasks.length == 0"
        class="bg-gray-800 text-gray-500 text-xl p-2 pl-4 pr-4 flex flex-col justify-between leading-normal mb-3"
      >
        No tasks found.
      </div>
      <TaskCard
        v-for="(item, index) in tasks"
        :item="item"
        :index="index"
        :key="item.taskId"
        :taskId="item.taskId"
        :description="item.description"
        :gameDataReferences="item.gameDataReferences"
        :isFavourite="item.isFavourite"
        :notes="item.notes"
        :taskType="item.taskType"
        :collectibleType="item.collectibleType"
        :source="item.source"
        :priority="item.priority"
        :refreshFrequency="item.refreshFrequency"
        @set-favourite="setFavourite(item, index)"
        @reload-task-list="loadTasks"
      />
    </div>
    <!--/Card List-->
  </div>
</template>

<script>
// TODO: Reduce code duplication in this script
import TaskCard from "./TaskCard";

export default {
  name: "TaskList",
  components: {
    TaskCard,
  },
  props: ["query"],
  data() {
    return {
      playerId: localStorage.playerId, // will eventually be replaced with logged-in user
      tasks: [],
    };
  },
  methods: {
    loadTasks: function () {
      let vm = this;

      // If they cleared filters, set it back to everything
      if (this.query == "") {
        this.$http
          .get("/api/tasks", {
            params: {
              playerId: vm.playerId,
            },
          })
          .then(function (response) {
            vm.tasks = response.data.tasks;
          })
          .catch(function (error) {
            console.log("had an error");
            console.log(error);
          });
      } else {
        this.$http
          .get("/api/tasks", {
            params: {
              playerId: vm.playerId,
              characterId: vm.query.characterId ?? null,
              collectibleType: vm.query.collectibleType ?? null,
              dungeonId: vm.query.dungeonId ?? null,
              isFavourite: vm.query.isFavourite ?? false,
              onlyActiveAttempts: vm.query.onlyActiveAttempts ?? false,
              refreshFrequency: vm.query.refreshFrequency ?? null,
              sortBy: vm.query.sortBy ?? null,
              taskType: vm.query.taskType ?? null,
              zoneId: vm.query.zoneId ?? null,
            },
          })
          .then(function (response) {
            vm.tasks = response.data.tasks;
          })
          .catch(function (error) {
            console.log("had an error");
            console.log(error);
          });
      }
    },
    refreshDailies: function () {
      this.$http
        .post(`/api/task-characters/refresh/daily`)
        .then(function (response) {
          if (response.status == 204) {
            window.location.reload();
          }
        })
        .catch(function (error) {
          console.log("had an error");
          console.log(error);
        });
    },
    refreshWeeklies: function () {
      this.$http
        .post(`/api/task-characters/refresh/weekly`)
        .then(function (response) {
          if (response.status == 204) {
            window.location.reload();
          }
        })
        .catch(function (error) {
          console.log("had an error");
          console.log(error);
        });
    },
    setFavourite: function (task, index) {
      let vm = this;
      if (task.isFavourite) {
        // Unmark as fave and refresh
        this.$http
          .patch(`/api/tasks/favourites/remove`, {
            taskId: task.taskId,
          })
          .then(function (response) {
            if (response.status == 200) {
              vm.$http
                .get(`/api/tasks/${task.taskId}`)
                .then(function (response) {
                  vm.tasks.splice(index, 1, response.data);
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
        // Mark as fave and refresh
        this.$http
          .patch(`/api/tasks/favourites/add`, {
            taskId: task.taskId,
          })
          .then(function (response) {
            if (response.status == 200) {
              vm.$http
                .get(`/api/tasks/${task.taskId}`)
                .then(function (response) {
                  vm.tasks.splice(index, 1, response.data);
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
  watch: {
    // This isn't my favourite way to pass state
    // I'm trying to avoid using Vuex as long as possible
    // while also properly composing components.
    // I'm aware this sucks. I promise.

    query: function () {
      this.loadTasks();
    },
  },
  mounted: function () {
    this.loadTasks();
  },
};
</script>

<style scoped>
</style>