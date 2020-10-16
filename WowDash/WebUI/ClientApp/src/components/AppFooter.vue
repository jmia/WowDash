<template>
  <footer class="bg-gray-900 border-t border-gray-400 shadow">
    <div class="container w-full mx-auto flex py-8">
      <div class="w-full mx-auto flex flex-wrap">
        <div class="flex w-full md:w-1/2">
          <div class="px-8">
            <h3 class="font-bold text-gray-100">About the App</h3>
            <p class="py-4 text-gray-600 text-sm">
              WowDash allows players to track in-game goals and recurring tasks for World of Warcraft. It was built for 
              players with large character rosters to help automate away some of the work of keeping a spreadsheet of raid lockouts 
              and transmog wish-lists, while providing cross-referenced filters across multiple fields to maximize those attempts when play time is limited. The more data you 
              give it, the harder it works.
            </p>
            <p class="text-gray-600 text-sm">Report issues and bugs on the <a href="https://github.com/jmia/WowDash" target="_blank" class="text-gray-400 hover:text-underline hover:text-gray-100">GitHub</a> page.</p>
          </div>
        </div>

        <div class="flex w-full md:w-1/2">
          <div class="px-8">
            <h3 class="font-bold text-gray-100">About the Dev</h3>
            <p class="py-2 text-gray-600 text-sm">WowDash is a college capstone project turned passion project by <a href="https://github.com/jmia" target="_blank" class="text-gray-400 hover:text-underline hover:text-gray-100 py-1">Jen Armstrong</a>.</p>
            <p class="pb-2 text-gray-600 text-sm">She's been playing since Ahn'Qiraj in vanilla, a retired raider since the end of WotLK, and now enjoys the filthy casual life on Area 52.</p>
            <p class="text-gray-600 text-sm">Still trying to get <a href="https://www.wowhead.com/item=50818/invincibles-reins" target="_blank" class="text-gray-400 hover:text-underline hover:text-gray-100">Invincible</a> after all these years.</p>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: "AppFooter",
};
</script>

<style scoped>
</style>