<script setup>
import { shallowRef } from 'vue';
import { useConnectionStore } from '@/stores/connection';
import { useModeratorStore } from '@/stores/moderator';
import { useRuntimeStore } from '@/stores/runtime';
import { getApp, registerApp } from '@/core/registry/app_registry';

import testApp from '@/apps/test-app';
import genZHumor2 from '@/apps/gen-z-humor-2';
import adjust from '@/apps/adjust';
import moderator from '@/apps/moderator';

const currentApp = shallowRef(null);
const connStore = useConnectionStore();
const runtimeStore = useRuntimeStore();

registerApp(testApp, runtimeStore);
registerApp(genZHumor2, runtimeStore);
registerApp(adjust, runtimeStore);
registerApp(moderator, useModeratorStore());

const roomData = connStore.roomData;

async function loadApp() {
  if (roomData) {
    document.title = roomData.appName;
    const app = getApp(roomData.appTag);
    if (app) {
      const loaded = await app.load();
      currentApp.value = loaded.default;
      console.log(`Loaded app "${roomData.appTag}"`);
    } else {
      console.error(`App "${roomData.appTag}" not found`);
    }
  }
}

async function loadModeratorApp() {
  document.title = 'Модерация';
  const app = await moderator.load();
  currentApp.value = app.default;
  console.log('Loaded moderator app');
}

if (connStore.connectionData.moderator) {
  loadModeratorApp();
} else {
  loadApp();
}
</script>

<template>
  <component :is="currentApp" />
</template>

<style scoped>
  * {
    text-align: center;
  }

  .playerName {
    margin-top: 12px;
  }

  .content {
    margin-top: 64px;
  }
</style>