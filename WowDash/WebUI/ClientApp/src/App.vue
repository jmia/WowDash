<template>
  <div class="font-sans leading-normal tracking-normal">
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

// Received a decent head-start from:
// https://medium.com/software-ateliers/asp-net-core-vue-template-with-custom-configuration-using-cli-3-0-8288e18ae80b
// https://jasonwatmore.com/post/2018/09/21/vuejs-basic-http-authentication-tutorial-example
// https://github.com/tailwindtoolbox/Admin-Template-Night


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
</style>
