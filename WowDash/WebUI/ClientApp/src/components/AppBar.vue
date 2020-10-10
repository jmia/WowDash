<template>
  <nav id="header" class="bg-gray-900 fixed w-full z-10 top-0 shadow">
    <div
      class="w-full container mx-auto flex flex-wrap items-center mt-0 pt-3 pb-3 md:pb-0"
    >
      <div class="w-1/2 pl-2 md:pl-0">
        <router-link to="/">
          <div
            class="flex text-gray-100 text-base xl:text-xl no-underline hover:no-underline font-bold"
            href="index.html"
          >
            <div class="text-blue-400 pr-3">
              <font-awesome-icon icon="crow" />
            </div>
            WowDash
          </div>
        </router-link>
      </div>
      <div class="w-1/2 pr-0">
        <div class="flex relative float-right">
          <div class="relative text-sm text-gray-100">
            <button
              id="userButton"
              class="flex items-center focus:outline-none mr-3"
            >
              <div class="text-blue-400 pr-3">
                <font-awesome-icon icon="user" />
              </div>
              <span class="hidden md:inline-block text-gray-100"
                >Hi, Nerd.</span
              >
              <svg
                class="pl-2 h-2 fill-current text-gray-100"
                version="1.1"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 129 129"
                xmlns:xlink="http://www.w3.org/1999/xlink"
                enable-background="new 0 0 129 129"
              >
                <g>
                  <path
                    d="m121.3,34.6c-1.6-1.6-4.2-1.6-5.8,0l-51,51.1-51.1-51.1c-1.6-1.6-4.2-1.6-5.8,0-1.6,1.6-1.6,4.2 0,5.8l53.9,53.9c0.8,0.8 1.8,1.2 2.9,1.2 1,0 2.1-0.4 2.9-1.2l53.9-53.9c1.7-1.6 1.7-4.2 0.1-5.8z"
                  />
                </g>
              </svg>
            </button>
            <div
              id="userMenu"
              class="bg-gray-900 rounded shadow-md mt-2 absolute mt-12 top-0 right-0 min-w-full overflow-auto z-30 invisible"
            >
              <ul class="list-reset">
                <li>
                    <router-link to="/profile">
                  <span 
                    class="px-4 py-2 block text-gray-100 hover:bg-gray-800 no-underline hover:no-underline"
                    >Profile</span
                  >
                    </router-link>
                </li>
                <li><hr class="border-t mx-2 border-gray-400" /></li>
                <li>
                  <a
                    href="#"
                    class="px-4 py-2 block text-gray-100 hover:bg-gray-800 no-underline hover:no-underline"
                    >Logout</a
                  >
                </li>
              </ul>
            </div>
          </div>

          <!-- Mobile Menu -->
          <div class="block lg:hidden pr-4">
            <button
              id="nav-toggle"
              class="flex items-center px-3 py-2 border rounded text-gray-500 border-gray-600 hover:text-gray-100 hover:border-teal-500 appearance-none focus:outline-none"
            >
              <svg
                class="fill-current h-3 w-3"
                viewBox="0 0 20 20"
                xmlns="http://www.w3.org/2000/svg"
              >
                <title>Menu</title>
                <path d="M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z" />
              </svg>
            </button>
          </div>
        </div>
      </div>

      <div
        class="w-full flex-grow lg:flex lg:items-center lg:w-auto hidden mt-2 lg:mt-0 bg-gray-900 z-20"
        id="nav-content"
      >
        <ul class="list-reset lg:flex flex-1 items-center px-4 md:px-0">
          <li class="mr-6 my-2 md:my-0">
            <div
              class="block py-1 md:py-3 pl-1 align-middle text-gray-500 no-underline hover:text-gray-100 border-b-2 border-gray-900 hover:border-blue-400"
            >
              <router-link to="/">
                <span class="mr-3"><font-awesome-icon icon="tasks" /></span
                ><span class="pb-1 md:pb-0 text-sm"
                  >Dashboard</span
                ></router-link
              >
            </div>
          </li>
          <li class="mr-6 my-2 md:my-0">
            <div
              class="block py-1 md:py-3 pl-1 align-middle text-gray-500 no-underline hover:text-gray-100 border-b-2 border-gray-900 hover:border-purple-400"
            >
              <router-link to="/roster">
                <span class="mr-3"><font-awesome-icon icon="user-ninja" /></span
                ><span class="pb-1 md:pb-0 text-sm">Roster</span>
              </router-link>
            </div>
          </li>
        </ul>

        <div class="relative pull-right pl-4 pr-4 md:pr-0">
          <form id="searchWowhead" v-on:submit.prevent="searchWowhead">
            <input
                type="search"
                placeholder="Search Wowhead"
                v-model="wowheadSearchTerm"
                class="w-full bg-gray-900 text-sm text-gray-400 transition border border-gray-800 focus:outline-none focus:border-gray-600 rounded py-1 px-2 pl-10 appearance-none leading-normal"
            />
            <div
                class="absolute search-icon"
                style="top: 0.375rem; left: 1.75rem"
            >
                <img src="@/assets/wowheadicon.png" class="pt-1" />
            </div>
          </form>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>

// Toggle dropdown list
// Came with the CSS template, need to fix up later
/*https://gist.github.com/slavapas/593e8e50cf4cc16ac972afcbad4f70c8*/

document.onclick = check;

function check(e) {

var userMenuDiv = document.getElementById("userMenu");
var userMenu = document.getElementById("userButton");

var navMenuDiv = document.getElementById("nav-content");
var navMenu = document.getElementById("nav-toggle");

  var target = (e && e.target) || (event && event.srcElement);

  //User Menu
  if (!checkParent(target, userMenuDiv)) {
    // click NOT on the menu
    if (checkParent(target, userMenu)) {
      // click on the link
      if (userMenuDiv.classList.contains("invisible")) {
        userMenuDiv.classList.remove("invisible");
      } else {
        userMenuDiv.classList.add("invisible");
      }
    } else {
      // click both outside link and outside menu, hide menu
      userMenuDiv.classList.add("invisible");
    }
  }

  //Nav Menu
  if (!checkParent(target, navMenuDiv)) {
    // click NOT on the menu
    if (checkParent(target, navMenu)) {
      // click on the link
      if (navMenuDiv.classList.contains("hidden")) {
        navMenuDiv.classList.remove("hidden");
      } else {
        navMenuDiv.classList.add("hidden");
      }
    } else {
      // click both outside link and outside menu, hide menu
      navMenuDiv.classList.add("hidden");
    }
  }
}

function checkParent(t, elm) {
  while (t.parentNode) {
    if (t == elm) {
      return true;
    }
    t = t.parentNode;
  }
  return false;
}

export default {
  name: "AppBar",
  data () {
      return {
          wowheadSearchTerm: ""
      }
  },
  methods: {
        searchWowhead: function() {
            console.log(this.wowheadSearchTerm);
            window.open('https://www.wowhead.com/search?q=' + this.wowheadSearchTerm);
        }
    }
};
</script>

<style scoped>
</style>