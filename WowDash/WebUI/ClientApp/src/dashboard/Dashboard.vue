<template>
  <div class="flex flex-wrap">
    <div class="w-1/3 l:w-1/4 xl:w-1/6 p-3">
        <FilterBar />
    </div>
    <div class="w-2/3 l:w-3/4 xl:w-5/6 p-3">
        <TaskList />
    </div>
  </div>
</template>

<script>
import FilterBar from './FilterBar'
import TaskList from './TaskList'

export default {
  name: "Dashboard",
  components: {
      FilterBar,
      TaskList
  }
};
</script>

<style scoped>
</style>