<script setup>
import { computed } from 'vue';
import { useRuntimeStore } from '@/stores/runtime';

const store = useRuntimeStore();
const playerName = computed(() => store.playerData ? store.playerData.name : '');
</script>

<template>
  <h1 class="playerName">{{ playerName }}</h1>
</template>