<template>
  <div class="flex flex-wrap">
    <div class="w-1/3 l:w-1/4 xl:w-1/6 p-3">
        <FilterBar />
    </div>
    <div class="w-2/3 l:w-3/4 xl:w-5/6 p-3">
      <!-- Action Buttons -->
      <div class="flex justify-end items-center mb-2">
        <a
          class="bg-green-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
          href="task-start.html"
          ><i class="fas fa-plus fa-fw"></i> Add New Task</a
        >
        <button
          class="bg-blue-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
        >
          <i class="fas fa-sync-alt fa-fw"></i> Refresh Dailies
        </button>
        <button
          class="bg-blue-400 p-2 font-bold text-center border-gray-800 rounded shadow"
        >
          <i class="fas fa-sync-alt fa-fw"></i> Refresh Weeklies
        </button>
      </div>
      <!-- /Action Buttons -->

      <!--Card List-->
      <div class="w-full dark-rounded">
        <div class="border-b border-gray-800 p-3">
          <h5 class="table-title">Tasks</h5>
        </div>

        <!-- Card -->
        <div
          class="bg-gray-800 p-2 pl-4 pr-4 flex flex-col justify-between leading-normal mb-3"
        >
          <div class="text-gray-500">
            <div class="w-full inline-flex justify-between items-center">
              <div>
                <div
                  class="inline-flex w-auto font-bold text-2xl text-gray-300 mb-1"
                >
                  <a
                    href="https://www.wowhead.com/item=50818/invincibles-reins"
                    class="hover:underline"
                    target="_blank"
                    >Invincible's Reins</a
                  >
                  <i
                    class="fas fa-caret-up fa-fw text-red-400"
                    title="priority: highest"
                  ></i>
                </div>
              </div>
              <div class="w-auto">
                <button class="mr-3">
                  <i class="fas fa-star fa-fw text-yellow-400"></i>
                </button>
                <button class="mr-3">
                  <i class="fas fa-edit fa-fw text-blue-400"></i>
                </button>
                <button class="mr-3">
                  <i class="fas fa-trash fa-fw text-blue-400"></i>
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
                <i class="fas fa-user-secret fa-fw mr-2 text-gray-400"></i
                ><a
                  href="https://www.wowhead.com/npc=36597/the-lich-king"
                  target="_blank"
                  >The Lich King</a
                >
              </div>
              <div class="hover:underline font-bold">
                <i class="fas fa-map-marked-alt fa-fw mr-2 text-gray-400"></i
                ><a
                  href="https://www.wowhead.com/icecrown-citadel"
                  target="_blank"
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
        <!-- /Card -->

        <!-- Card -->
        <div
          class="bg-gray-800 p-2 pl-4 pr-4 flex flex-col justify-between leading-normal mb-3"
        >
          <div class="text-gray-500">
            <div class="w-full inline-flex justify-between items-center">
              <div>
                <div
                  class="inline-flex w-auto font-bold text-2xl text-gray-300 mb-1"
                >
                  <a
                    href="https://www.wowhead.com/achievement=11631/extreme-makeover-fel-edition"
                    class="hover:underline"
                    target="_blank"
                    >Extreme Makeover: Fel Edition</a
                  >
                  <i
                    class="fas fa-caret-up fa-fw text-yellow-400"
                    title="priority: medium"
                  ></i>
                </div>
              </div>
              <div class="w-auto">
                <button class="mr-3">
                  <i class="far fa-star fa-fw text-yellow-400"></i>
                </button>
                <button class="mr-3">
                  <i class="fas fa-edit fa-fw text-blue-400"></i>
                </button>
                <button class="mr-3">
                  <i class="fas fa-trash fa-fw text-blue-400"></i>
                </button>
                <button
                  class="bg-green-400 text-gray-800 font-bold text-center p-1 border-gray-800 rounded shadow"
                >
                  Got it!
                </button>
              </div>
            </div>
            <div class="mb-2">
              <div class="inline-flex items-start mb-2 text-xs text-center">
                <div
                  class="text-gray-400 border-gray-400 border rounded-full p-1 pr-2 pl-2 w-auto uppercase ml-1 cursor-default"
                >
                  achievement
                </div>
              </div>
            </div>

            <div>
              There's potential that I'll want to go look at other gear sets
              from here, but primarily I want to run on my mage.
            </div>

            <hr class="border-b-1 border-gray-700 my-2 mx-4" />

            <div class="text-xs uppercase">Character Attempts</div>

            <div
              class="inline-flex flex-wrap text-sm items-start mb-2 mt-2 text-center"
            >
              <button
                class="bg-blue-400 text-gray-800 rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1"
              >
                Oleander
              </button>
            </div>
          </div>
        </div>
        <!-- /Card -->
      </div>
      <!--/Card List-->
    </div>
  </div>
</template>

<script>
import FilterBar from './FilterBar'

export default {
  name: "Dashboard",
  components: {
      FilterBar
  }
};
</script>

<style scoped>
</style>