<script setup>
import { computed } from 'vue';
import { getInput } from '@/core/registry/input_registry';

const props = defineProps({
  schema: Object
})

const component = computed(() => {
  return getInput(props.schema.type);
})
</script>

<template>
  <component
    :is="component"
    :schema="schema"
  />
</template>