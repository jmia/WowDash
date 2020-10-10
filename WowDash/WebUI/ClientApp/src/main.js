import Vue from 'vue';
//import VeeValidate from 'vee-validate';
//import { store } from './_store';
import { router } from './_common';
import App from './App.vue';
import '@/assets/css/tailwind.css';
import axios from 'axios';
import './_common/whtooltips';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCrow, faTasks, faUser, faUserNinja, faUserSecret } from '@fortawesome/free-solid-svg-icons'  // whatever the list is of stuff i need
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(faCrow, faTasks, faUser, faUserNinja, faUserSecret)
Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.prototype.$http = axios;

import GSignInButton from 'vue-google-signin-button';
Vue.use(GSignInButton);

Vue.config.productionTip = false;

new Vue({
  router,
//  store,
  render: h => h(App),
}).$mount('#app')

// new Vue({
//   el: '#app',
//   router,
// //  store,
//   render: h => h(App)
// });
