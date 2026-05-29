<script setup>
import { computed, ref } from 'vue';

const props = defineProps({
  schema: Object
})

const value = ref(props.schema.value || (props.schema.min || 0));
const className = computed(() => ('form-range ' + (props.schema.class || '')).trim());

function onChange(event) {
  value.value = event.target.value;
}
</script>

<template>
  <div class="rangeContainer">
    <input type="range" @change="onChange" :name="schema.id" :id="schema.id" :class="className"
    :min="schema.min || 0" :max="schema.max || 100" :step="schema.step || 1" :value="value" />
    <label :for="schema.id" v-if="schema.showValue">{{ value }}</label>
  </div>
</template>