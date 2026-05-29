<script setup>
import { useConnectionStore } from '@/stores/connection';

import AppName from '../shell/AppName.vue';
import ConnectButton from '../shell/ConnectButton.vue';
import ControllerSettings from '../shell/ControllerSettings.vue';
import LabeledInput from '../shell/LabeledInput.vue';
import RoomCodeInput from '../shell/RoomCodeInput.vue';

const store = useConnectionStore();
store.connectionData.moderator = true;

document.title = 'PartyGDK - Модерация'
</script>

<template>
  <ControllerSettings title="Модерация">
    <RoomCodeInput />
    <LabeledInput label="Пароль" :password="true" name="password" id="password" :maxlength="5" placeholder="5-значный пароль" className="mb-5" />
    <ConnectButton textDefault="Модерировать" textBusy="Подключение..." />
    <AppName />
  </ControllerSettings>
</template>