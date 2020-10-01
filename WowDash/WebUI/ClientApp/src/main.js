import Vue from 'vue';
//import VeeValidate from 'vee-validate';
//import { store } from './_store';
import { router } from './_common';
import App from './App.vue';
import '@/assets/css/tailwind.css';
import axios from 'axios';

Vue.prototype.$http = axios;

import GSignInButton from 'vue-google-signin-button';
Vue.use(GSignInButton);

Vue.config.productionTip = false;

// new Vue({
//   router,
// //  store,
//   render: h => h(App),
// }).$mount('#app')

new Vue({
  el: '#app',
  router,
//  store,
  render: h => h(App)
});
