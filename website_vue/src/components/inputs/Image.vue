<script setup>
const props = defineProps({
  schema: Object
})
</script>

<template>
  <img :src="schema.src" :width="schema.width" :height="schema.height" :alt="schema.alt"
  :draggable="schema.draggable || false" />
</template>