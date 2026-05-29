<script setup>
import '@/assets/css/index.css';

import { computed } from 'vue';
import { useConnectionStore } from './stores/connection';

import AppRuntime from './components/AppRuntime.vue';
import ShellLayout from './components/ShellLayout.vue';

const store = useConnectionStore();
const connected = computed(() => store.connected);
</script>

<template>
  <AppRuntime v-if="connected" />
  <ShellLayout v-else />
</template>
