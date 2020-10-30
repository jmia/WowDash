import Vue from 'vue';
import Router from 'vue-router';

import Dashboard from '../dashboard/Dashboard'
import CharacterRoster from '../roster/CharacterRoster'
import AddCharacter from '../roster/AddCharacter'
import EditCharacter from '../roster/EditCharacter'
import UserProfile from '../profile/UserProfile'

Vue.use(Router);

export const router = new Router({
  mode: 'history',
  routes: [
    { path: '/', component: Dashboard },
    { path: '/roster', component: CharacterRoster },
    { path: '/profile', component: UserProfile },
    { path: '/add-character', component: AddCharacter },
    { path: '/edit-character/:id', component: EditCharacter, name: 'edit-character', props: true },

    // otherwise redirect to dashboard
    { path: '*', redirect: '/' }
  ]
});

// router.beforeEach((to, from, next) => {
//   // redirect to login page if not logged in and trying to access a restricted page
//   const publicPages = ['/login', '/register'];
//   const authRequired = !publicPages.includes(to.path);
//   const loggedIn = localStorage.getItem('user');

//   if (authRequired && !loggedIn) {
//     return next('/login');
//   }

//   next();
// })