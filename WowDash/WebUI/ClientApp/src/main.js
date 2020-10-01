import Vue from 'vue';
//import VeeValidate from 'vee-validate';
//import { store } from './_store';
import { router } from './_common';
import App from './App.vue'
import '@/assets/css/tailwind.css'

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
