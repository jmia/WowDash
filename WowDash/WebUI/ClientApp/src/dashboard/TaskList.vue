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
import TaskCard from "./TaskCard";

export default {
  name: "TaskList",
  components: {
    TaskCard,
  },
  data() {
    return {
      playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
      tasks: [
        {
          taskId: "22129c9d-0518-4fad-86d0-08d8706ce7cb",
          //"playerId": "d8a57467-008e-4ebb-286a-08d86586cf0f",
          description: "Battle for Azeroth Pathfinder, Part Two",
          gameDataReferences: [
            {
              id: 2,
              gameId: 758,
              type: 3,
              subclass: null,
              description: "Icecrown Citadel",
            },
            {
              id: 3,
              gameId: 1636,
              type: 4,
              subclass: null,
              description: "The Lich King",
            },
            {
              id: 4,
              gameId: 50818,
              type: 1,
              subclass: "Mount",
              description: "Invincible's Reins",
            },
            {
              id: 5,
              gameId: null,
              type: 7,
              subclass: null,
              description: "Gambling Debt",
            },
            {
              id: 6,
              gameId: 11509,
              type: 7,
              subclass: null,
              description: 'Street "Cred"',
            },
            {
              id: 7,
              gameId: 1458,
              type: 5,
              subclass: null,
              description: "Telurinon Moonshadow",
            },
            {
              id: 8,
              gameId: null,
              type: 5,
              subclass: null,
              description: "Daleohm",
            },
            {
              id: 9,
              gameId: null,
              type: 8,
              subclass: null,
              description: "Zul'Drak",
            },
            {
              id: 10,
              gameId: 13250,
              type: 0,
              subclass: null,
              description: "Battle for Azeroth Pathfinder, Part Two",
            },
            {
              id: 10,
              gameId: 12940,
              type: 1,
              subclass: null,
              description: "Dal'Rend's Sacred Charge",
            },
            {
              id: 12,
              gameId: 1190,
              type: 2,
              subclass: null,
              description: "Battlegear of Winged Triumph",
            },
          ],
          isFavourite: false,
          notes: null,
          taskType: 0,
          collectibleType: 0,
          source: 0,
          priority: 2,
          refreshFrequency: 2,
        },
      ],
    };
  },
};
</script>

<style scoped>
</style>