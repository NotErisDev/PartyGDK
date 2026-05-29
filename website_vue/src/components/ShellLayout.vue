<script setup>
import { RouterView } from 'vue-router';
import Header from './shell/Header.vue';
import Footer from './shell/Footer.vue';
Footer
</script>

<template>
  <Header />
  <main>
    <RouterView />
  </main>
  <Footer />
</template>