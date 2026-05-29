<script setup>
import { computed } from 'vue';
import '@/assets/css/footer.css';
import version from '@/json/version.json'

const releaseDateFormatted = computed(() => {
  const date = Date.parse(version.release_date);
  const formatter = new Intl.DateTimeFormat('ru');
  return formatter.format(date);
})
</script>

<template>
  <footer>
    <p class="text-center footerText">
      Пользуясь данным сайтом, вы соглашаетесь с <a href="/privacy">политикой конфиденциальности</a>. Проект не имеет никакого отношения к Jackbox Games.
      <br>
      <span class="versionText">PartyGDK Controller (Vue) v{{ version.version }} ({{ releaseDateFormatted }})</span>
    </p>
  </footer>
</template>