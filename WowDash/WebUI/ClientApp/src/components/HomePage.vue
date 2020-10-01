<template>
    <div>
        <div>You're on the home page</div>
        <button v-google-signin-button="clientId" class="google-signin-button bg-blue-300 text-blue-900"> Continue with Google</button>
    </div>
</template>

<script>
import GoogleSignInButton from 'vue-google-signin-button-directive'

export default {
    name: 'HomePage',
    directives: {
        GoogleSignInButton
    },
    data: () => ({
        clientId: '180473183157-02l2vm6ckc94nlqqemdspfmft6vdr42o.apps.googleusercontent.com'
    }),
    methods: {
        OnGoogleAuthSuccess (idToken) {
            console.log(idToken);
        },
        OnGoogleAuthFail (error) {
            console.log(error)
        }
    }
}
</script>

<style>
.google-signin-button {
  height: 50px;
  font-size: 16px;
  border-radius: 10px;
  padding: 10px 20px 25px 20px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
}
</style>