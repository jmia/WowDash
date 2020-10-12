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
              >Invincible's Reins</a
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
        I've been trying to get this sucker for 15 years, why am I still
        bothering? It's a flying undead horse. You figure it out.
      </div>

      <hr class="border-b-1 border-gray-700 my-2 mx-4" />

      <div class="text-xs uppercase">Character Attempts</div>

      <div
        class="inline-flex flex-wrap items-start text-sm mb-2 mt-2 text-center"
      >
        <button
          class="bg-orange-400 text-gray-800 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1"
        >
          Chakwas
        </button>
        <button
          class="bg-green-400 text-gray-800 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1"
        >
          Scully
        </button>
        <button
          class="bg-gray-600 text-gray-800 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1 line-through"
        >
          Temperance
        </button>
        <button
          class="bg-purple-700 text-gray-400 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1"
        >
          Cadidylus
        </button>
        <button
          class="bg-gray-600 text-gray-800 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1 line-through"
        >
          Oleander
        </button>
        <button
          class="bg-yellow-700 text-gray-800 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1"
        >
          Diabetty
        </button>
        <button
          class="bg-yellow-400 text-gray-800 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1"
        >
          Candor
        </button>
        <button
          class="bg-purple-400 text-gray-800 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1"
        >
          Meraddison
        </button>
        <button
          class="bg-indigo-700 text-gray-400 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1"
        >
          Orbrand
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TaskCard",
};
</script>

<style scoped>
</style>