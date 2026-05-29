<script setup>
import { ref } from 'vue';
import '@/assets/css/header.css';
import config from '@/json/config.json';

const links = ref([
  { text: 'Играть', url: '/' },
  { text: 'Модерировать', url: '/moderator' }
])

const iconLinks = ref([
  { image: 'controller/github.svg', alt: 'GitHub Icon', url: config.github_url }
])
</script>

<template>
  <header class="d-flex flex-row align-items-center">
    <a href="/" draggable="false">
      <img src="/src/assets/img/controller/logo_transparent.png" alt="PartyGDK Logo" class="logo" draggable="false">
    </a>
    <div class="links">
      <a v-for="link in links" :href="link.url">{{ link.text }}</a>
    </div>
    <div class="icon-links">
      <a v-for="link in iconLinks" :href="link.url" target="_blank" draggable="false">
        <img :src="'/static/img/' + link.image" :alt="link.alt" draggable="false">
      </a>
    </div>
  </header>
</template>