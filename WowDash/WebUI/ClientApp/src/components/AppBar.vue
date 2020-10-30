<template>
  <nav id="header" class="bg-gray-900 fixed w-full z-10 top-0 shadow">
    <div
      class="w-full container mx-auto flex flex-wrap items-center mt-0 pt-3 pb-3 lg:pb-0"
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
              class="flex items-center focus:outline-none mr-3"
              @click="toggleUserMenu"
              ref="btnUserMenuRef"
            >
              <div class="text-blue-400 pr-3">
                <font-awesome-icon icon="user" />
              </div>
              <span class="hidden lg:inline-block text-gray-100"
                >Hi,
                {{
                  playerDisplayName ? playerDisplayName : localDisplayName
                }}.</span
              >
              <!-- Caret, possibly replace? -->
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
              v-show="userMenuShow"
              @click="toggleUserMenu"
              class="bg-gray-800 text-gray-400 z-50 float-left py-2 list-none text-left rounded shadow-lg mt-1"
              style="min-width: 12rem"
              ref="popoverUserMenuRef"
            >
              <router-link to="/profile">
                <span
                  class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
                >
                  Profile
                </span>
              </router-link>
              <div
                class="h-0 my-2 border border-solid border-t-0 border-gray-400 opacity-25"
              ></div>
              <router-link to="/login">
                <span
                  class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
                >
                  Logout
                </span>
              </router-link>
            </div>
          </div>

          <!-- Mobile Menu -->
          <div class="block lg:hidden pr-4">
            <button
              class="flex items-center text-gray-500 hover:text-gray-100 hover:border-teal-500 appearance-none focus:outline-none"
              @click="toggleNavContent"
            >
              <div class="pl-1 pr-1 fill-current text-gray-100">
                <font-awesome-icon icon="bars" />
              </div>
            </button>
          </div>
        </div>
      </div>

      <div
        v-show="menuVisible"
        class="w-full flex-grow lg:flex lg:items-center lg:w-auto mt-2 lg:mt-0 bg-gray-900 z-20"
      >
        <ul class="list-reset lg:flex flex-1 items-center px-4 md:px-0">
          <li class="mr-6 my-2 md:my-0">
            <router-link to="/">
              <div
                class="block py-1 md:py-3 pl-1 align-middle text-gray-500 no-underline hover:text-gray-100 border-b-2 border-gray-900 hover:border-blue-400"
              >
                <span class="mr-3"><font-awesome-icon icon="tasks" /></span
                ><span class="pb-1 md:pb-0 text-sm">Dashboard</span>
              </div>
            </router-link>
          </li>
          <li class="mr-6 my-2 md:my-0">
            <router-link to="/roster">
              <div
                class="block py-1 md:py-3 pl-1 align-middle text-gray-500 no-underline hover:text-gray-100 border-b-2 border-gray-900 hover:border-purple-400"
              >
                <span class="mr-3"><font-awesome-icon icon="user-ninja" /></span
                ><span class="pb-1 md:pb-0 text-sm">Roster</span>
              </div>
            </router-link>
          </li>
        </ul>

        <div class="text-sm text-gray-100">
          <div class="">
            <button
              class="inline-flex items-center text-gray-500 text-sm rounded shadow hover:text-gray-100 hover:shadow-lg outline-none focus:outline-none"
              @click="toggleHelpfulLinks"
              ref="btnHelpfulLinksRef"
            >
              Helpful Links
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
              v-show="helpfulLinksShow"
              @click="toggleHelpfulLinks"
              class="bg-gray-800 text-gray-400 z-50 float-left py-2 list-none text-left rounded shadow-lg mt-1"
              style="min-width: 12rem"
              ref="popoverHelpfulLinksRef"
            >
              <p class="text-gray-600 uppercase font-bold px-2">Tools</p>
              <a
                href="https://www.wowhead.com/planner"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                Character Planner
              </a>
              <a
                href="https://www.wowhead.com/classes"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                Class Guides
              </a>
              <a
                href="https://www.wowhead.com/talent-calc"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                Talent Calculator
              </a>
              <div
                class="h-0 my-2 border border-solid border-t-0 border-gray-400 opacity-25"
              ></div>
              <p class="text-gray-600 uppercase font-bold px-2">Economy</p>
              <a
                href="https://www.dataforazeroth.com/calculator"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                Data for Azeroth Calculator
              </a>
              <a
                href="https://theunderminejournal.com/"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                The Undermine Journal
              </a>
              <a
                href="https://wah.jonaskf.net/"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                WoW Auction Helper (WAH)
              </a>
              <a
                href="https://www.reddit.com/r/woweconomy/"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                /r/woweconomy
              </a>
              <div
                class="h-0 my-2 border border-solid border-t-0 border-gray-400 opacity-25"
              ></div>
              <p class="text-gray-600 uppercase font-bold px-2">Help</p>
              <a
                href="https://worldofwarcraft.com/en-us/game/status/us"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                Realm Status
              </a>
              <a
                href="https://twitter.com/BlizzardCS"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                @BlizzardCS
              </a>
              <a
                href="https://us.forums.blizzard.com/en/wow/"
                target="_blank"
                class="text-sm py-2 px-4 font-normal block w-full whitespace-no-wrap bg-transparent hover:text-gray-100"
              >
                WoW Forums
              </a>
            </div>
          </div>
        </div>
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
import Popper from "popper.js";

// Finally properly fixed display/hide of nav content on
// small screens with the help of:
// https://stackoverflow.com/questions/58157764/how-do-i-dynamically-show-a-mobile-menu-with-tailwind-and-vue-js
const tailwindConfig = require("../../tailwind.config");

export default {
  name: "AppBar",
  data() {
    return {
      playerId: localStorage.playerId,
      localDisplayName: "Nerd",
      helpfulLinksShow: false,
      navContentShow: false,
      lgBreakPoint: Number(tailwindConfig.theme.screens.lg.replace("px", "")),
      userMenuShow: false,
      windowWidth: 0,
      wowheadSearchTerm: "",
      ready: false,
    };
  },
  computed: {
    menuVisible: function () {
      return this.windowWidth > this.lgBreakPoint ? true : this.navContentShow;
    },
    playerDisplayName: function () {
      return this.$store.state.playerDisplayName;
    },
  },
  methods: {
    searchWowhead: function () {
      window.open("https://www.wowhead.com/search?q=" + this.wowheadSearchTerm);
    },
    toggleHelpfulLinks: function () {
      if (this.helpfulLinksShow) {
        this.helpfulLinksShow = false;
      } else {
        this.helpfulLinksShow = true;
        new Popper(
          this.$refs.btnHelpfulLinksRef,
          this.$refs.popoverHelpfulLinksRef,
          {
            placement: "bottom-start",
          }
        );
      }
    },
    toggleUserMenu: function () {
      if (this.userMenuShow) {
        this.userMenuShow = false;
      } else {
        this.userMenuShow = true;
        new Popper(this.$refs.btnUserMenuRef, this.$refs.popoverUserMenuRef, {
          placement: "bottom-start",
        });
      }
    },
    toggleNavContent: function () {
      this.navContentShow = !this.navContentShow;
    },
    updateWindowSize: function () {
      this.windowWidth = window.innerWidth;
    },
  },
  watch: {
    // I admit that I don't understand Vuex at all
    // which is why I'm trying not to use it if I can avoid it
    ready: function() {
      let vm = this;
      this.$http
        .get(`/api/players/${vm.playerId}`)
        .then(function (profile) {
          vm.$store.commit('updateDisplayName', profile.data.displayName);
        })
        .catch(function (error) {
          console.log("had an error");
          console.log(error);
        });
    }
  },
  mounted: function () {
    this.updateWindowSize();
    window.addEventListener("resize", this.updateWindowSize);
    let vm = this;
      this.$http
        .get(`/api/players/${vm.playerId}`)
        .then(function (profile) {
          vm.localDisplayName = profile.data.displayName;
        })
        .catch(function (error) {
          console.log("had an error");
          console.log(error);
        });
  },
  beforeDestroyed: function () {
    window.removeEventListener("resize", this.updateWindowSize);
  },
};
</script>

<style scoped>
</style>