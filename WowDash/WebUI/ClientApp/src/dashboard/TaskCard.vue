<template>
  <div
    class="bg-gray-800 p-2 pl-4 pr-4 flex flex-col justify-between leading-normal mb-3"
  >
    <div class="text-gray-500">
      <div class="w-full inline-flex justify-between items-center">
        <div>
          <div class="inline-flex w-auto font-bold text-2xl text-gray-300 mb-1">
            <a
              href="https://www.wowhead.com/item=50818/invincibles-reins"
              class="hover:underline"
              target="_blank"
              >{{ description }}</a
            >
            <sub
              class="fas fa-caret-up fa-fw text-red-400"
              title="priority: highest"
            ><font-awesome-icon icon="caret-up" /></sub>
          </div>
        </div>
        <div class="w-auto">
          <button class="mr-4">
            <span class="text-yellow-400"><font-awesome-icon icon="star" /></span>
          </button>
          <button class="mr-4">
            <span class="text-blue-400"><font-awesome-icon icon="edit" /></span>
          </button>
          <button class="mr-4">
            <span class="text-blue-400"><font-awesome-icon icon="trash" /></span>
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
          class="text-gray-400 border-gray-400 border rounded-full p-1 pr-2 pl-2 w-auto uppercase ml-1 cursor-default"
        >
          mount
        </div>
        <div
          class="text-blue-400 border-blue-400 border rounded-full p-1 pr-2 pl-2 w-auto uppercase ml-1 cursor-default"
        >
          weekly
        </div>
      </div>

      <div class="mb-2">
        <div class="hover:underline font-bold">
          <span class="mr-2 text-gray-400"><font-awesome-icon icon="user-secret" /></span
          ><a
            href="https://www.wowhead.com/npc=36597/the-lich-king"
            target="_blank"
            >The Lich King</a
          >
        </div>
        <div class="hover:underline font-bold">
          <span class="mr-2 text-gray-400"><font-awesome-icon icon="map-marked-alt" /></span
          ><a href="https://www.wowhead.com/icecrown-citadel" target="_blank"
            >Icecrown Citadel</a
          >
        </div>
      </div>

      <div>
        {{ notes }}
      </div>

      <hr class="border-b-1 border-gray-700 my-2 mx-4" />

      <div class="text-xs uppercase">Character Attempts</div>

      <div
        class="inline-flex flex-wrap items-start text-sm mb-2 mt-2 text-center"
      >
        <TaskCharacterButton v-for="(item, index) in characters"
        :item="item"
        :index="index"
        :key="item.id" 
        :taskId="taskId"
        :characterId="item.characterId"
        :name="item.name"
        :playableClass="item.class" 
        :isActive="item.isActive" />
      </div>
    </div>
  </div>
</template>

<script>
import TaskCharacterButton from './TaskCharacterButton'

export default {
  name: "TaskCard",
  components: {
    TaskCharacterButton
  },
  data() {
    return {
      characters: [
        // If the structure of this changes (e.g. "id" is "characterId"), 
        // check the component info above and the template in TaskCharacterButton.vue
        { characterId: "fake", name: "Chakwas", class: "Druid", isActive: true },
        { characterId: "fake", name: "Scully", class: "Hunter", isActive: true },
        { characterId: "fake", name: "Temperance", class: "Paladin", isActive: true },
        { characterId: "fake", name: "Meraddison", class: "Warlock", isActive: true },
        { characterId: "fake", name: "Cadidylus", class: "Demon Hunter", isActive: true },
        { characterId: "fake", name: "Orbrand", class: "Death Knight", isActive: true },
        { characterId: "fake", name: "Diabetty", class: "Warrior", isActive: true },
        { characterId: "fake", name: "Candor", class: "Rogue", isActive: false },
        { characterId: "fake", name: "Oleander", class: "Mage", isActive: true },
        { characterId: "fake", name: "Mozart", class: "Monk", isActive: true },
        { characterId: "fake", name: "Rienne", class: "Shaman", isActive: true },
        { characterId: "fake", name: "Rashael", class: "Priest", isActive: true },
      ]
    }
  },
  props: {
    taskId: {
      type: String,
      required: true
    },
    description: {
      type: String
    },
    gameDataReferences: {
      type: Array,
    },
    isFavourite: {
      type: Boolean
    },
    notes: {
      type: String
    },
    taskType: {
      type: Number
    },
    collectibleType: {
      type: Number
    },
    source: {
      type: Number
    },
    priority: {
      type: Number
    },
    refreshFrequency: {
      type: Number
    }
  },
};
</script>

<style scoped>

</style>