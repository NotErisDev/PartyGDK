<script setup>
import { computed } from 'vue';
import { useRuntimeStore } from '@/stores/runtime';

const props = defineProps({
  textPlayer: String,
  textAudience: String
})

const store = useRuntimeStore();
const text = computed(() => store.playerData.role === 'player' ? props.textPlayer : props.textAudience);
</script>

<template>
  <small class="roleText">{{ text }}</small>
</template>