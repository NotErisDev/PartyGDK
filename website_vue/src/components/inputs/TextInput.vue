<script setup>
import { computed } from 'vue';

const props = defineProps({
  schema: Object
})

const className = computed(() => ('form-control ' +
(props.schema.class ? props.schema.class : '')).trim());
</script>

<template>
  <input type="text" :name="schema.id" :id="schema.id" :maxlength="schema.maxLength || 100" :class="className"
  :placeholder="schema.placeholder" autocomplete="off" :required="schema.required">
</template>