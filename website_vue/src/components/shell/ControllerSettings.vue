<script setup>
import messages from '@/json/messages.json'

const props = defineProps({
  title: String
})
</script>

<template>
  <div class="main mx-auto">
    <small class="text-warning">{{ messages.controllerWarningMessage }}</small>
    <h1 class="title">{{ title }}</h1>
    <div class="settings mx-auto">
      <slot></slot>
    </div>
  </div>
</template>

<style scoped>
.main {
  margin-top: 64px;
  text-align: center;
  max-width: 512px;
}

.main .title {
  font-weight: 600;
  border-bottom: 2px solid var(--color-secondary);
  line-height: 4.5rem;
  margin-bottom: 32px;
}

.main .settings {
  display: flex;
  flex-direction: column;
  justify-content: center;
  max-width: 320px;
}
</style>