<script setup>
import { computed } from 'vue';

const props = defineProps({
  label: String,
  name: String,
  id: String,
  password: Boolean,
  maxlength: Number,
  placeholder: String,
  className: String,
  value: String,
  onInput: Function
})

const type = computed(() => props.password ? 'password' : 'text');
const className = computed(() => ('form-control ' + props.className).trim());
</script>

<template>
  <label :for="name" class="form-label">{{ label }}</label>
  <input :type="type" :class="className" :name="name" :id="id" autocomplete="off" :maxlength="maxlength" :placeholder="placeholder"
  :value="value" @input="onInput">
</template>

<style scoped>
input {
  font-size: 24px;
  font-weight: 600;
  text-align: center;
}
</style>