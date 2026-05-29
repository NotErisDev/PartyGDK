<script setup>
import { computed } from 'vue';
import { play } from '@/core/websocket/connection';
import { useConnectionStore } from '@/stores/connection';
import { setCookie, swalError } from '@/utils/controller';

const store = useConnectionStore();
const props = defineProps({
  textDefault: String,
  textBusy: String
})

const isBusy = computed(() => store.connecting);

function isDisabled() {
  if (!store.connectionData || !store.roomData)
  return true;

  if (store.connectionData.moderator && !store.roomData.moderationEnabled)
  return true;

  return store.connecting || store.roomData.status !== 200 || store.connected;
}

const className = computed(() => ('btn btn-primary mb-2 connectBtn ' + (isDisabled() ? 'disabled' : '')).trim());
const text = computed(() => isBusy.value ? props.textBusy : props.textDefault);

function onClick() {
  const codeField = document.getElementById('roomCode');
  const roomCode = codeField.value.toUpperCase();
  if (roomCode.length < codeField.maxLength) {
  return swalError('Неверно введён код комнаты.');
  }

  let password;
  if (store.connectionData.moderator) {
  password = document.getElementById('password').value;
  } else {
  const playerName = document.getElementById('playerName').value.trim();
  if (playerName.length === 0)
    return swalError('Укажите имя.');

  store.connectionData.playerName = playerName;
  setCookie('playerName', playerName);
  }

  store.connectionData.roomCode = roomCode;
  setCookie('roomCode', roomCode);

  play(password);
}
</script>

<template>
  <button :class="className" @click="onClick" ref="connectBtn">
  <img src="/static/img/controller/loading.svg" alt="Loading Icon" class="loadingIcon" draggable="false" v-if="isBusy">
  <span>{{ text }}</span>
  </button>
</template>

<style scoped>
button {
  font-size: 24px;
}

button .loadingIcon {
  width: 16px;
  height: 16px;
  margin-right: 8px;
  position: relative;
  top: -1px;
  animation: loadingIcon .75s linear infinite;
}

@keyframes loadingIcon {
  0% {
  transform: rotate(0deg);
  }

  100% {
  transform: rotate(360deg);
  }
}
</style>