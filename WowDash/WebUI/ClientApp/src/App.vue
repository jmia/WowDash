<template>
  <div>
    <!-- probably put layout stuff here, yeah? -->
    <router-link to="/">Home</router-link> |
    <router-link to="/login">Login</router-link> |
    <router-link to="/register">Register</router-link>
    <router-view></router-view>
  </div>
</template>

<script>

// Received a decent head-start from:
// https://medium.com/software-ateliers/asp-net-core-vue-template-with-custom-configuration-using-cli-3-0-8288e18ae80b
// https://jasonwatmore.com/post/2018/09/21/vuejs-basic-http-authentication-tutorial-example

export default {
  name: 'App',
  mounted() {
    let googleScript = document.createElement('script')
    googleScript.setAttribute('src', 'https://apis.google.com/js/api:client.js')
    document.head.appendChild(googleScript)
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
