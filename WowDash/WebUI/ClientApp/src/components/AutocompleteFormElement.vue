<template>
  <div
    :class="`formulate-input-element formulate-input-element--${context.type}`"
    :data-type="context.type"
  >
    <input
      type="text"
      v-model="context.model"
      v-bind="context.attributes"
      autocomplete="off"
      @blur="context.blurHandler"
    />
    <ul
      v-if="filteredOptions.length"
      class="z-10 absolute h-48 overflow-auto formulate-input-dropdown text-md rounded p-2 bg-gray-200 text-gray-700"
    >
      <li
        class="cursor-default"
        :class="{ highlight: selectedIndex == index }"
        v-for="(option, index) in filteredOptions"
        :key="option.value"
        v-text="option.label"
        @mouseenter="selectedIndex = index"
        @click="sendValues"
      />
    </ul>
  </div>
</template>

<script>
export default {
  name: "AutocompleteFormElement",
  props: {
    context: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      selectedIndex: 0,
      selectedModelValue: "",
    };
  },
  watch: {
    model: function () {
      this.selectedIndex = 0;
      // need to check if we should refresh values
      if (this.selectedModelValue != this.context.model) {
        this.debouncedRefreshOptions();
      }
    },
  },
  computed: {
    model: function () {
      return this.context.model;
    },
    selection: function () {
      if (this.context.options[this.selectedIndex]) {
        return this.context.options[this.selectedIndex];
      }
      return false;
    },
    filteredOptions: function () {
      if (Array.isArray(this.context.options) && this.context.model) {
        // BUG: This will not show any results if
        // the text input exactly matches any result
        const isAlreadySelected = this.context.options.find(
          (option) => option.label === this.context.model
        );
        if (!isAlreadySelected) {
          return this.context.options;
        }
      }
      return [];
    },
  },
  methods: {
    sendValues: function () {
      this.selectedModelValue = this.selection.label;
      this.context.model = this.selection.label;
      this.context.rootEmit("append-references", {
        gameId: this.selection.value,
        description: this.selection.label,
      });
    },
    refreshOptions: function () {
      this.context.rootEmit("refresh-options", this.context.model);
    },
  },
  mounted: function () {
    // BUG: This will sometimes overlap with returns from previous calls
    // and cause the options list to double-up
    this.debouncedRefreshOptions = this.$_.debounce(this.refreshOptions, 1000);
  },
};
</script>

<style scoped>
.highlight {
  @apply bg-gray-700;
  @apply text-gray-300;
}
</style>