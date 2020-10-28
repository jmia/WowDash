<template>
  <div>
    <div class="flex justify-start items-center mb-2">
      <router-link
        to="/roster"
        class="bg-red-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
      >
        <font-awesome-icon icon="caret-left" /> Back</router-link
      >
    </div>
    <div class="w-5/6 mx-auto dark-rounded">
      <div class="border-b border-gray-800 p-3">
        <h5 class="table-title">Add a Character</h5>
      </div>

      <div class="bg-gray-800 text-gray-500 text-xl p-2 pl-4 pr-4 mb-3">
        This is where the form goes.
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AddCharacter",
};
</script>

<style scoped>
</style>