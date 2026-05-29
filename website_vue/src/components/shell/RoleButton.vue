<script setup>
import { computed } from 'vue';
import { useConnectionStore } from '@/stores/connection';

const props = defineProps({
  role: String
})

const store = useConnectionStore();

function isDisabled() {
  return props.role === 'audience' && store.roomData && store.roomData.audienceEnabled === false;
}

function isSelected() {
  return !isDisabled() && store.connectionData.role === props.role;
}

const className = computed(() => {
  const classes = ['btn', 'roleBtn', isSelected() ? 'btn-primary' : 'btn-outline-primary'];
  if (isDisabled())
    classes.push('disabled');

  return classes.join(' ');
})

function select() {
  if (!isDisabled())
    store.connectionData.role = props.role;
}
</script>

<template>
  <button :class="className" @click="select">
    <slot></slot>
  </button>
</template>