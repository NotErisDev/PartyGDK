<script setup>
import '@/assets/css/app.css';

import { computed } from 'vue';
import { useRuntimeStore } from '@/stores/runtime';
import InputRenderer from '@/components/InputRenderer.vue';
import PlayerNameText from './PlayerNameText.vue';
import RoleText from './RoleText.vue';
import SubmitButton from './SubmitButton.vue';

const store = useRuntimeStore();
const inputListClassName = computed(() => ('inputList ' + (!store.canSendInputs ? 'disabled' : '')).trim())
</script>

<template>
  <PlayerNameText />
  <RoleText textPlayer="Игрок" textAudience="Зритель" />
  <div class="content">
    <div class="inputs" v-if="store.anyInputs">
      <p class="prompt">{{ store.inputData.prompt }}</p>
      <div :class="inputListClassName">
        <InputRenderer v-for="input in store.inputs" :schema="input" />
      </div>
      <SubmitButton v-if="store.inputData.submitButton" />
    </div>
    <div class="standby" v-else>
      <slot></slot>
    </div>
  </div>
</template>