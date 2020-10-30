<template>
  <div>
    <div class="flex justify-start items-center mb-2">
      <router-link
        to="/roster"
        class="bg-red-400 p-2 mr-2 font-bold text-center border-gray-800 rounded shadow"
      >
        <font-awesome-icon icon="caret-left" /> Back</router-link
      >
    </div>
    <div class="w-1/2 mx-auto dark-rounded">
      <div class="border-b border-gray-800 p-3">
        <h5 class="table-title">Update Profile</h5>
      </div>

      <FormulateForm @submit="updateProfile">
        <div class="bg-gray-800 text-gray-500 text-lg p-2 pl-4 pr-4 mb-3">
          <div class="w-3/4 space-y-4">
            <FormulateInput
              type="text"
              v-model="displayName"
              label="Display Name"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            />
            <!-- TODO: Reenable in 2.0 -->
            <!-- <FormulateInput
              type="select"
              v-model="defaultTaskType"
              :options="taskTypes"
              label="Default Task Type"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            /> -->
            <FormulateInput
              type="select"
              v-model="defaultRealm"
              :options="realms"
              label="Default Realm"
              :wrapper-class="[
                'inline-flex',
                'justify-around',
                'w-full',
                'items-center',
              ]"
              :label-class="['font-bold', 'text-right', 'w-1/2', 'pr-8']"
              :element-class="['w-1/2']"
              :input-class="['bg-gray-200', 'text-gray-700', 'rounded', 'p-2']"
            />
          </div>
        </div>
        <div class="flex justify-end mr-2">
          <button
            class="bg-blue-400 text-gray-800 p-2 mb-3 font-bold text-center border-gray-800 rounded shadow"
          >
            Update Profile
          </button>
        </div>
      </FormulateForm>
    </div>
  </div>
</template>

<script>
export default {
  name: "UserProfile",
  data() {
    return {
      playerId: "d8a57467-008e-4ebb-286a-08d86586cf0f",
      displayName: "",
    //  defaultTaskType: null,
      defaultRealm: "",
      realms: [],
    //   taskTypes: [
    //     { value: null, label: "No default (always ask)"},
    //     { value: "0", label: "General" },
    //     { value: "1", label: "Achievement" },
    //     { value: "2", label: "Collectible" },
    //   ],
    };
  },
  methods: {
    updateProfile: function () {
      let vm = this;

      this.$http
        .put(`/api/players/`, {
          playerId: vm.playerId,
          displayName: vm.displayName,
         // defaultTaskType: vm.defaultTaskType,
          defaultRealm: vm.defaultRealm,
        })
        .then(function () {
          vm.$router.push("/");
        })
        .catch(function (error) {
          console.log("had an error");
          console.log(error);
        });
    },
  },
  mounted: function () {
    let vm = this;
    this.$http
      .get(`/api/players/${vm.playerId}`)
      .then(function (profile) {
          console.log(profile);
        vm.displayName = profile.data.displayName;
        vm.defaultRealm = profile.data.defaultRealm;
       // vm.defaultTaskType = Number(profile.data.defaultTaskType);
      })
      .catch(function (error) {
        console.log("had an error");
        console.log(error);
      });

    this.$http
      .get(`/api/realms`)
      .then(function (response) {
        response.data.map((r) => {
          vm.realms.push({ value: r.slug, label: r.name });
        });
      })
      .catch(function (error) {
        console.log("had an error");
        console.log(error);
      });
  },
};
</script>

<style>
.standard-input {
  @apply bg-gray-400;
  @apply text-gray-800;
  @apply rounded;
  @apply p-2;
}

.formulate-input-element--radio {
  display: inline-block !important;
}
</style>