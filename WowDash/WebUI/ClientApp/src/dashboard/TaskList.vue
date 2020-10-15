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
        />
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
  },
  data() {
    return {
      playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
      tasks: [
        {
          "taskId": "22129c9d-0518-4fad-86d0-08d8706ce7cb",
          //"playerId": "d8a57467-008e-4ebb-286a-08d86586cf0f",
          "description": "Invincible's Reins",
          "gameDataReferences": [
            {
              "id": 2,
              "gameId": 758,
              "type": 3,
              "subclass": null,
              "description": "Icecrown Citadel"
            },
            {
              "id": 3,
              "gameId": 1636,
              "type": 4,
              "subclass": null,
              "description": "The Lich King"
            },
            {
              "id": 4,
              "gameId": 50818,
              "type": 1,
              "subclass": "Mount",
              "description": "Invincible's Reins"
            }
          ],
          "isFavourite": false,
          "notes": null,
          "taskType": 2,
          "collectibleType": 2,
          "source": 0,
          "priority": 3,
          "refreshFrequency": 2
        }
      ]
    }
  }
};
</script>

<style scoped>
</style>