import Vue from 'vue';
import Router from 'vue-router';

import Dashboard from '../dashboard/Dashboard'
import CharacterRoster from '../roster/CharacterRoster'
import AddCharacter from '../roster/AddCharacter'
import EditCharacter from '../roster/EditCharacter'
import UserProfile from '../profile/UserProfile'
import AddTask from '../dashboard/AddTask'
import EditTask from '../dashboard/EditTask'

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
    { path: '/edit-task/:id', component: EditTask, name: 'edit-task', props: true },
    { path: '/api/*' },
    { path: '/api/*/*'},
    { path: '/api/*/*/*'}

    // otherwise redirect to dashboard
    //{ path: '*', redirect: '/' }
  ]
});