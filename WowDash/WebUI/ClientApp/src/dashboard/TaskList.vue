<template>
  <div>
    <!-- Action Buttons -->
    <div class="flex justify-end items-center mb-2">
      <a
        class="bg-green-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
        href="task-start.html"
        ><font-awesome-icon icon="plus" /> Add New Task</a
      >
      <button
        class="bg-blue-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
      >
        <font-awesome-icon icon="sync-alt" /> Refresh Dailies
      </button>
      <button
        class="bg-blue-400 p-2 font-bold text-center border-gray-800 rounded shadow"
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

      <TaskCard />
    </div>
    <!--/Card List-->
  </div>
</template>

<script>
import TaskCard from './TaskCard'

export default {
  name: "TaskList",
  components: {
      TaskCard
  }
};
</script>

<style scoped>
</style>