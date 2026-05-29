<script setup>
import { useConnectionStore } from '@/stores/connection';
import { getRoomData } from '@/utils/connection';
import { getCookie, getUrlParameter } from '@/utils/controller';

import LabeledInput from '../shell/LabeledInput.vue';

const store = useConnectionStore();
const roomCode = getUrlParameter('code') || getCookie('roomCode') || '';

async function updateRoomData(code) {
  store.roomData = await getRoomData(code || roomCode);
  if (store.roomData && store.roomData.audienceEnabled === false) {
    store.connectionData.role = 'player';
  }
}

if (roomCode)
  updateRoomData();

function onInput(event) {
  const value = event.target.value;
  if (value.length >= event.target.maxLength)
    updateRoomData(value.toUpperCase());
  else
    store.roomData = null;
}
</script>

<template>
  <LabeledInput label="Код комнаты" name="roomCode" id="roomCode" :maxlength="4" placeholder="4-значный код"
  className="text-uppercase mb-4" :value="roomCode" :onInput="onInput" />
</template>