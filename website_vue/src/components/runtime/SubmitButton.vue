<script setup>
import { computed } from 'vue';
import { send } from '@/core/websocket/socket';
import { useRuntimeStore } from '@/stores/runtime';
import { swalError } from '@/utils/controller';

const store = useRuntimeStore();

const className = computed(() => ('btn btn-primary submitBtn ' + (!store.canSendInputs ? 'disabled' : '')).trim())

function submit() {
  if (!store.canSendInputs)
    return;

  const inputs = {};
  const affectedTypes = ['checkbox', 'range', 'text'];
  let error;

  store.inputs.forEach(function(input) {
    if (!affectedTypes.includes(input.type))
      return;

    let value;
    const elem = document.getElementById(input.id);
    if (!elem)
      return;

    switch (input.type) {
      case 'checkbox':
        value = elem.checked;
        break;
      
      case 'range':
        value = Number(elem.value);
        break;

      case 'text':
        value = elem.value.trim();
        if (!value && elem.required) {
          error = 'Не все поля заполнены';
          return;
        }
        break;
    }

    if (value != null)
      inputs[input.id] = value;
  });

  if (error)
    return swalError(error);

  store.canSendInputs = false;
  send({
    key: 'input',
    val: inputs
  });
}
</script>

<template>
  <button :class="className" @click="submit">Отправить</button>
</template>