<template>
  <div class="dark-rounded p-2">
    <div class="flex flex-row items-start">
      <div class="flex-1 text-center">
        <h5 class="table-title">Filters</h5>
        <div>
          <!-- TODO: Refactor these to components probably? -->
          <h3 class="filter-bar-category">Task</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="type in taskTypes"
              v-bind:key="type.id"
            >
            <!-- This little blank space trick for aligning checkboxes 
            is from: https://twitter.com/adamwathan/status/1217864323466432516 -->
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="type.value"
                  v-bind:value="type.id"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="type.value" class="ml-2">{{
                type.display
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Collectible</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="type in collectibleTypes"
              v-bind:key="type.id"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="type.value"
                  v-bind:value="type.id"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="type.value" class="ml-2">{{
                type.display
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Character</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="character in characterList"
              v-bind:key="character.id"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="character.value"
                  v-bind:value="character.id"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="character.value" class="ml-2">{{
                character.display
              }}</label>
            </div>
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Dungeon</h3>
          <div class="checkbox-text">
            <div
              class="flex items-start"
              v-for="dungeon in dungeonList"
              v-bind:key="dungeon.id"
            >
              <div class="flex items-center">
                &#8203;
                <input
                  type="checkbox"
                  v-bind:id="dungeon.value"
                  v-bind:value="dungeon.id"
                  class="checkbox-size"
                />
              </div>
              <label v-bind:for="dungeon.value" class="ml-2">{{
                dungeon.display
              }}</label>
            </div>
          </div>
        </div>    <!-- You get the gist... have to build out logic using Vue Formulate later anyway, it'll all be gutted -->
        <div>
          <h3 class="filter-bar-category">Zone</h3>
          <ul class="checkbox-text">
            <li>The Cape of Stranglethorn</li>
            <li>Nagrand</li>
            <li>Netherstorm</li>
          </ul>
        </div>
        <div>
          <h3 class="filter-bar-category">Refresh</h3>
          <ul class="checkbox-text">
            <li>Daily</li>
            <li>Weekly</li>
            <li>Never</li>
          </ul>
        </div>
        <div>
          <h3 class="filter-bar-category">Other</h3>
          <ul class="checkbox-text">
            <li>Favourites Only</li>
          </ul>
        </div>
        <div>
          <h3 class="filter-bar-category">Sort By</h3>
          <select
            name="cars"
            id="cars"
            class="bg-gray-400 rounded w-auto p-1 m-2 mb-8 text-sm"
          >
            <option value="alpha_asc">Alphabet A-Z</option>
            <option value="alpha_desc">Alphabet Z-A</option>
            <option value="priority_asc">Priority Low-High</option>
            <option value="priority_desc">Priority High-Low</option>
          </select>
        </div>
        <button
          class="bg-blue-400 w-3/4 font-bold text-center p-1 m-2 border-gray-800 rounded shadow"
        >
          Apply Filters</button
        ><br />
        <button
          class="bg-gray-800 w-3/4 font-bold text-center text-gray-400 p-1 m-2 border-blue-400 rounded shadow"
        >
          Clear Filters
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "FilterBar",
  data() {
    return {
      taskTypes: [
        { id: 0, value: "general", display: "General" }, // TODO: Remove this domain logic from the front-end
        { id: 1, value: "achievement", display: "Achievement" },
        { id: 2, value: "collectible", display: "Collectible" },
      ],
      collectibleTypes: [
        { id: 0, value: "item", display: "Gear and Items" },
        { id: 1, value: "itemSet", display: "Dungeon Sets" },
        { id: 2, value: "mount", display: "Mounts" },
        { id: 3, value: "pet", display: "Battle Pets" },
      ],
      characterList: [
        {
          id: "cbf62c90-5358-4059-a04d-f798cb2d300c",
          value: "cbf62c90-5358-4059-a04d-f798cb2d300c",
          display: "Scully",
        },
        {
          id: "92100f04-7a0c-4138-809b-73a8cdaf7d6b",
          value: "92100f04-7a0c-4138-809b-73a8cdaf7d6b",
          display: "Chakwas",
        },
        {
          id: "859cfab5-725c-4310-b5e4-9d8e92533109",
          value: "859cfab5-725c-4310-b5e4-9d8e92533109",
          display: "Temperance",
        },
        {
          id: "4f42b206-b106-42c2-9d3e-29ded8128b75",
          value: "4f42b206-b106-42c2-9d3e-29ded8128b75",
          display: "Meraddison",
        },
      ],
      dungeonList: [
        { id: 101, value: 101, display: "Icecrown Citadel" },
        { id: 102, value: 102, display: "Black Temple" },
        { id: 103, value: 103, display: "Ahn'kahet: The Old Kingdom" },
      ],
    };
  },
};
</script>

<style scoped>
.filter-bar-category {
  @apply font-bold;
  @apply text-2xl;
  @apply text-gray-600;
}
</style>