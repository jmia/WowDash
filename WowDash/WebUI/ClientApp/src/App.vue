<template>
  <div class="bg-black-alt font-sans leading-normal tracking-normal">
    <AppBar />
      <div class="container w-full mx-auto pt-20">
        <div class="w-full px-4 md:px-0 md:mt-8 mb-16 text-gray-800 leading-normal">
          <router-view></router-view>
        </div>
      </div>
    <!-- a footer might go here -->
  </div>
</template>

<script>
import AppBar from './components/AppBar'

export default {
  name: 'App',
  components: {
    AppBar
  },
  mounted() {
    // Add Google Sign-In JavaScript CDN
    let googleScript = document.createElement('script')
    googleScript.setAttribute('src', 'https://apis.google.com/js/api:client.js')
    document.head.appendChild(googleScript)

    // Add Wowhead Tooltips JavaScript
    let wowheadScript = document.createElement('script')
    wowheadScript.setAttribute('src', 'https://wow.zamimg.com/widgets/power.js')
    document.head.appendChild(wowheadScript)
  },
  data () {
    return {
    }
  },
  methods: {
  }
}
</script>

<style>
  .bg-black-alt  {
    background:#191919;
  }
</style>
