<template>
  <div class="flex flex-wrap">
    <div class="w-1/3 l:w-1/4 xl:w-1/6 p-3">
        <FilterBar @update-query="updateQuery"/>
    </div>
    <div class="w-2/3 l:w-3/4 xl:w-5/6 p-3">
        <TaskList :query="query"/>
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
  },
  data() {
    return {
      query: ""
    }
  },
  methods: {
    updateQuery: function(event) {
      this.query = event;
    }
  }
};
</script>

<style scoped>
</style>