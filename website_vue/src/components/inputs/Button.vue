<script setup>
import { computed } from 'vue';
import { send } from '@/core/websocket/socket';
import { useRuntimeStore } from '@/stores/runtime';

const props = defineProps({
  schema: Object,
  key: String,
  value: String,
  disabled: Boolean
})

const store = useRuntimeStore();

const className = computed(() => {
  let classes = 'btn btn-secondary choiceBtn';
  if (props.disabled || props.schema.disabled)
    classes += ' disabled';
  if (props.schema.class)
    classes += ' ' + props.schema.class;

  return classes;
})

function onClick() {
  if (props.disabled || props.schema.disabled || !store.canSendInputs)
    return;

  store.canSendInputs = false;
  const val = {};
  val[props.key || props.schema.key] = props.value || props.schema.value;

  send({
    key: 'input',
    val: val
  });
}
</script>

<template>
  <button :class="className" @click="onClick">
    <img v-if="schema.image" :src="schema.image.src" :alt="schema.image.alt" draggable="false"
    :width="schema.image.width" :height="schema.image.height" />
    {{ schema.text }}
  </button>
</template>