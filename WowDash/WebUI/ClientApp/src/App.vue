<template>
  <div class="bg-black-alt font-sans leading-normal tracking-normal">
    <AppBar />
    <div class="container min-h-screen w-full mx-auto pt-20">
      <div
        class="w-full px-4 md:px-0 md:mt-8 mb-16 text-gray-800 leading-normal"
      >
        <router-view></router-view>
      </div>
    </div>
    <AppFooter />
  </div>
</template>

<script>
import AppBar from "./components/AppBar";
import AppFooter from "./components/AppFooter";

export default {
  name: "App",
  components: {
    AppBar,
    AppFooter,
  },
  mounted() {
    // Add default player ID to localStorage
    localStorage.playerId = "d8a57467-008e-4ebb-286a-08d86586cf0f";

    // Add Google Sign-In JavaScript CDN
    let googleScript = document.createElement("script");
    googleScript.setAttribute(
      "src",
      "https://apis.google.com/js/api:client.js"
    );
    document.head.appendChild(googleScript);

    // Add Wowhead Tooltips JavaScript
    let wowheadScript = document.createElement("script");
    wowheadScript.setAttribute(
      "src",
      "https://wow.zamimg.com/widgets/power.js"
    );
    document.head.appendChild(wowheadScript);
  }
};
</script>

<style>
.bg-black-alt {
  background: #191919;
}
.table-title {
  @apply font-bold;
  @apply uppercase;
  @apply text-blue-400;
}
.dark-rounded {
  @apply bg-gray-900;
  @apply border;
  @apply border-gray-800;
  @apply rounded;
  @apply shadow;
}
.checkbox-text {
  @apply text-left;
  @apply pl-2;
  @apply text-gray-400;
}
.checkbox-size {
  @apply w-4;
  @apply h-4;
}
</style>
