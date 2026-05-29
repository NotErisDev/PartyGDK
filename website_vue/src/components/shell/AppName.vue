<script setup>
import { computed } from 'vue';
import { useConnectionStore } from '@/stores/connection';

const store = useConnectionStore();
const text = computed(() => {
  if (store.roomData) {
    switch (store.roomData.status) {
      case 404:
        return 'Комната не найдена';

      case 429:
        return 'Вы делаете слишком много запросов';

      case 500:
        return 'Внутренняя ошибка сервера';

      case 0:
        return 'Потеряно соединение с сервером';
    }

    if (store.connectionData.moderator && !store.roomData.moderationEnabled) {
      return 'Модерация отключена';
    }

    return store.roomData.appName;
  }
})
</script>

<template>
  <p class="form-label gameName" v-if="store.roomData">{{ text }}</p>
</template>