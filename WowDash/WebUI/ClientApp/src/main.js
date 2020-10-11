// Main imports
import Vue from 'vue';
import axios from 'axios';
//import { store } from './_store';
import { router } from './_common';
import App from './App.vue';

// Utility imports
import './_common/whtooltips';
import GSignInButton from 'vue-google-signin-button';

// Styling imports
import '@/assets/css/tailwind.css';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faBars, faCrow, faTasks, faUser, faUserNinja, faUserSecret } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

// Add font-awesome icons
library.add(faBars, faCrow, faTasks, faUser, faUserNinja, faUserSecret)
Vue.component('font-awesome-icon', FontAwesomeIcon)

// Assign axios to global
Vue.prototype.$http = axios;

// Use google sign-in
Vue.use(GSignInButton);

Vue.config.productionTip = false;

new Vue({
  router,
//  store,
  render: h => h(App),
}).$mount('#app')
