import Vue from 'vue';
import Router from 'vue-router';

import Dashboard from '../dashboard/Dashboard'
import CharacterRoster from '../roster/CharacterRoster'
import UserProfile from '../profile/UserProfile'

// These aren't very tasty yet
import LoginPage from '../components/LoginPage'
import RegisterPage from '../components/RegisterPage'

Vue.use(Router);

export const router = new Router({
  mode: 'history',
  routes: [
    { path: '/', component: Dashboard },
    { path: '/roster', component: CharacterRoster },
    { path: '/profile', component: UserProfile },
    { path: '/login', component: LoginPage },
    { path: '/register', component: RegisterPage },

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