<template>
  <div class="dark-rounded p-2">
    <div class="flex flex-row items-start">
      <div class="flex-1 text-center">
        <h5 class="table-title">Filters</h5>
        <div>
          <h3 class="filter-bar-category">Task</h3>
          <div class="text-1xl text-left pl-6 text-gray-400">
              <div class="flex items-start">
                    <div class="flex items-center">
                        &#8203;
                        <input type="checkbox" id="general" name="general" value="0" class="w-4 h-4" />
                    </div>
                <label for="general" class="ml-2">General</label>
                </div>
            <!-- <li>General</li>
            <li>Achievement</li>
            <li>Collectible</li> -->
          </div>
        </div>
        <div>
          <h3 class="filter-bar-category">Collectible</h3>
          <ul class="text-1xl text-left pl-6 text-gray-400 list-disc">
            <li>Item</li>
            <li>Item Set</li>
            <li>Mount</li>
            <li>Pet</li>
          </ul>
        </div>
        <div>
          <h3 class="filter-bar-category">Character</h3>
          <ul class="text-1xl text-left pl-6 text-gray-400 list-disc">
            <li>Scully</li>
            <li>Chakwas</li>
            <li>Temperance</li>
            <li>Meraddison</li>
          </ul>
        </div>
        <div>
          <h3 class="filter-bar-category">Dungeon</h3>
          <ul class="text-1xl text-left pl-6 text-gray-400 list-disc">
            <li>Icecrown Citadel</li>
            <li>Black Temple</li>
            <li>Ahn'kahet: The Old Kingdom</li>
          </ul>
        </div>
        <div>
          <h3 class="filter-bar-category">Zone</h3>
          <ul class="text-1xl text-left pl-6 text-gray-400 list-disc">
            <li>The Cape of Stranglethorn</li>
            <li>Nagrand</li>
            <li>Netherstorm</li>
          </ul>
        </div>
        <div>
          <h3 class="filter-bar-category">Refresh</h3>
          <ul class="text-1xl text-left pl-6 text-gray-400 list-disc">
            <li>Daily</li>
            <li>Weekly</li>
            <li>Never</li>
          </ul>
        </div>
        <div>
          <h3 class="filter-bar-category">Other</h3>
          <ul class="text-1xl text-left pl-6 text-gray-400 list-disc">
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
};
</script>

<style scoped>
.filter-bar-category {
    @apply font-bold; 
    @apply text-2xl;
    @apply text-gray-600;
}
</style>