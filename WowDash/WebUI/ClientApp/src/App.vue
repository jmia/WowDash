<template>
  <div>
    <!-- probably put layout stuff here, yeah? -->
    <router-view></router-view>
  </div>
</template>

<script>

// Received a decent head-start from:
// https://medium.com/software-ateliers/asp-net-core-vue-template-with-custom-configuration-using-cli-3-0-8288e18ae80b
// https://jasonwatmore.com/post/2018/09/21/vuejs-basic-http-authentication-tutorial-example

export default {
  name: 'App'
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
