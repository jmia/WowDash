<template>
  <div>
    <!-- probably put layout stuff here, yeah? -->
    <g-signin-button
      :params="googleSignInParams"
      @success="onSignInSuccess"
      @error="onSignInError">
      Sign in with Google
    </g-signin-button>
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
      /**
       * The Auth2 parameters, as seen on
       * https://developers.google.com/identity/sign-in/web/reference#gapiauth2initparams.
       * As the very least, a valid client_id must present.
       * @type {Object}
       */
      googleSignInParams: {
        client_id: '180473183157-02l2vm6ckc94nlqqemdspfmft6vdr42o.apps.googleusercontent.com'
      }
    }
  },
  methods: {
    onSignInSuccess (googleUser) {
      // `googleUser` is the GoogleUser object that represents the just-signed-in user.
      // See https://developers.google.com/identity/sign-in/web/reference#users
      console.log('we got something!');
      console.log(googleUser);
      const profile = googleUser.getBasicProfile();
      const email = profile.getEmail();
      const userId = profile.getId();
      const displayName = profile.getName();
      console.log('email is ' + email);
          console.log('id is ' + userId);
          console.log('displayName is ' + displayName);
      this.registerUser(userId, email, displayName);
    },
    onSignInError (error) {
      // `error` contains any error occurred.
      console.log('OH NOES', error)
    },
    registerUser: function (userId, email, displayName) {
      const user = {
        userId: userId,
          email: email,
          displayName: displayName
      };
      const baseURI = 'https://localhost:44378/api/users/register'
      this.$http.post(baseURI, user)
      .then((result) => {
        console.log(result);
      })
    }
  }
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
.g-signin-button {
  /* This is where you control how the button looks. Be creative! */
  display: inline-block;
  padding: 4px 8px;
  border-radius: 3px;
  background-color: #3c82f7;
  color: #fff;
  box-shadow: 0 3px 0 #0f69ff;
}
</style>
