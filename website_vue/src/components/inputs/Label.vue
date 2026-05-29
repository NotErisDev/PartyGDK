<script setup>
import { toCSS } from '@/utils/styles';

const props = defineProps({
  schema: Object
})

function getFontSize() {
  const size = props.schema.size || 16;
  const presets = ['medium', 'medium', 'medium', 'x-large', 'xx-large', 'xxx-large'];

  if (size <= presets.length) {
  return presets[size - 1];
  }

  return size + 'px';
}

function getCSS() {
  const data = {
  'font-size': getFontSize()
  };

  if (props.schema.color) {
  data.color = props.schema.color;
  }

  return toCSS(data);
}
</script>

<template>
  <p :style="getCSS()">{{ schema.text }}</p>
</template>