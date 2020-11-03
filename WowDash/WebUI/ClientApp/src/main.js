// Main imports
import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import { router } from "./_common";
import App from "./App.vue";

// Form imports
import VueFormulate from "@braid/vue-formulate";
import AutocompleteFormElement from './components/AutocompleteFormElement';

// Utility imports
import "./_common/whtooltips";
import _ from 'lodash'; 

// Styling imports
import "@/assets/css/tailwind.css";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faBars,
  faCaretUp,
  faCaretDown,
  faCaretLeft,
  faCrow,
  faDungeon,
  faEdit,
  faExclamation,
  faMale,
  faMapMarkedAlt,
  faMars,
  faPlus,
  faSkullCrossbones,
  faSyncAlt,
  faTasks,
  faTrash,
  faUser,
  faUserNinja,
  faUserSecret,
  faVenus,
} from "@fortawesome/free-solid-svg-icons";
import { faStar as fasStar } from "@fortawesome/free-solid-svg-icons";
import { faStar as farStar } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

// Add font-awesome icons
library.add(
  faBars,
  faCaretUp,
  faCaretDown,
  faCaretLeft,
  faCrow,
  faDungeon,
  faEdit,
  faExclamation,
  faMale,
  faMapMarkedAlt,
  faMars,
  faPlus,
  faSkullCrossbones,
  farStar,
  fasStar,
  faSyncAlt,
  faTasks,
  faTrash,
  faUser,
  faUserNinja,
  faUserSecret,
  faVenus
);
Vue.component("font-awesome-icon", FontAwesomeIcon);

// Assign axios to global
Vue.prototype.$http = axios;
   
Object.defineProperty(Vue.prototype, '$_', { value: _ });

// Use vue-formulate
Vue.component('AutocompleteFormElement', AutocompleteFormElement)
Vue.use(VueFormulate, {
  library: {
    autocomplete: {
      classification: 'text',
      component: 'AutocompleteFormElement',
    }
  }
});

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    playerDisplayName: "",
  },
  mutations: {
    updateDisplayName(state, payload) {
      state.playerDisplayName = payload.playerDisplayName;
    },
  },
});

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
