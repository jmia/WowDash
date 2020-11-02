import Vue from 'vue';
import Router from 'vue-router';

import Dashboard from '../dashboard/Dashboard'
import CharacterRoster from '../roster/CharacterRoster'
import AddCharacter from '../roster/AddCharacter'
import EditCharacter from '../roster/EditCharacter'
import UserProfile from '../profile/UserProfile'
import AddTask from '../dashboard/AddTask'

Vue.use(Router);

export const router = new Router({
  mode: 'history',
  routes: [
    { path: '/', component: Dashboard },
    { path: '/roster', component: CharacterRoster },
    { path: '/profile', component: UserProfile },
    { path: '/add-character', component: AddCharacter },
    { path: '/edit-character/:id', component: EditCharacter, name: 'edit-character', props: true },
    { path: '/add-task', component: AddTask },

    // otherwise redirect to dashboard
    { path: '*', redirect: '/' }
  ]
});