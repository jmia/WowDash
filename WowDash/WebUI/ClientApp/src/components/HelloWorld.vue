<template>
  <div class="grid grid-cols-12"><!-- the container grid -->
    <div class="col-span-12 h-16 bg-gray-800 inline-grid grid-cols-8 gap-4"> <!-- the app bar -->
    <div class="col-span-2 text-2xl tracking-wider text-blue-300">WowDash</div>
      <a href="#" class="col-span-2 px-3 py-2 rounded text-gray-400 hover:bg-gray-900 hover:text-white">Log In</a>
      </div> <!-- end app bar -->
    <div class="col-span-2 min-h-screen bg-gray-600"> <!-- the side bar -->
      <div class="text-2xl tracking-wider">Filters</div>
    </div> <!-- end side bar -->
    <div class="col-span-5 min-h-screen bg-gray-200"> <!-- the task list -->
      <div class="text-2xl tracking-wider">Task List</div>
    </div><!-- end task list -->
  </div> <!-- end container grid -->
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
