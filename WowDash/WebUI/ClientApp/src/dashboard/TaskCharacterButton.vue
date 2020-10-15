<template>
    <button class="rounded-full p-2 pl-3 pr-3 mb-1 w-auto ml-1" :class="formattedClass">{{ name }}</button>
</template>

<script>
export default {
    name: 'TaskCharacterButton',
    props: {
        characterId: {
            type: String,
            required: true
        },
        name: {
            type: String
        },
        playableClass: {
            type: String
        },
        isActive: {
            type: Boolean,
            required: true
        }
    },
    computed: {
        formattedClass: function () {
            var characterClass = this.playableClass.toLowerCase().replace(/ /g, "");
            if (this.isActive) {
                return characterClass;
            } else {
                return "attempt-complete"
            }
        }
    }
}
</script>

<style scoped>
/* Active Status */
.attempt-complete {
    @apply bg-gray-600;
    @apply text-gray-800;
    @apply line-through;
}
/* Classes */
.deathknight {
  @apply bg-indigo-700;
  @apply text-gray-400;
}
.demonhunter {
  @apply bg-purple-700;
  @apply text-gray-400;
}
.druid {
  @apply bg-orange-400;
  @apply text-gray-800;
}
.hunter {
  @apply bg-green-400;
  @apply text-gray-800;
}
.mage {
  @apply bg-blue-300;
  @apply text-gray-800;
}
.monk {
  @apply bg-teal-400;
  @apply text-gray-800;
}
.paladin {
  @apply bg-pink-400;
  @apply text-gray-800;
}
.priest {
  @apply bg-white;
  @apply text-gray-800;
}
.rogue {
  @apply bg-yellow-400;
  @apply text-gray-800;
}
.shaman {
  @apply bg-blue-700;
  @apply text-gray-400;
}
.warlock {
  @apply bg-purple-400;
  @apply text-gray-800;
}
.warrior {
  @apply bg-tan;
  @apply text-gray-800;
}
</style>