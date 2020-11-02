<template>
<div class="text-white">{{ id }}</div>
</template>

<script>
export default {
  name: "EditTask",
  props: ["id"],
  data() {
    return {
    };
  },
};
</script>

<style>
.standard-input {
  @apply bg-gray-400;
  @apply text-gray-800;
  @apply rounded;
  @apply p-2;
}
</style>