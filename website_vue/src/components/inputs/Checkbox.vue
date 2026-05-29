<script setup>
const props = defineProps({
  schema: Object
})
</script>

<template>
  <div class="checkboxContainer">
    <input type="checkbox" :name="schema.id" :id="schema.id" />
    <label :for="schema.id">{{ schema.label }}</label>
  </div>
</template>