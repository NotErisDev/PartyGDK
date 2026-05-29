<script setup>
import { computed } from 'vue';
import { send } from '@/core/websocket/socket';
import { useModeratorStore } from '@/stores/moderator';

const props = defineProps({
  data: Object
})

const censorBtnClass = computed(() => ('btn btnYellow ' + (props.data.nameCensored ? 'disabled' : '')).trim());
const kickBtnClass = computed(() => ('btn btnRed ' + (props.data.kicked ? 'disabled' : '')).trim());

const censorBtnText = computed(() => props.data.nameCensored ? 'Имя скрыто' : 'Скрыть имя');
const kickBtnText = computed(() => props.data.kicked ? 'Исключён' : 'Исключить');

function censorName() {
  send({
    key: 'censorName',
    val: {
      name: props.data.name
    }
  });
}

function kick() {
  let text = 'Вы можете указать причину исключения ниже.';
  if (useModeratorStore().kickByIpEnabled)
    text += '<br><strong>IP-адрес также будет заблокирован для подключения в роли игрока.</strong>';

  Swal.fire({
    title: `Исключить игрока ${props.data.name}?`,
    html: text,
    input: 'text',
    inputAttributes: {
      maxlength: 100,
      autocomplete: 'off'
    },
    confirmButtonText: 'Исключить',
    preConfirm: reason => {
      send({
        key: 'kickPlayer',
        val: {
          name: props.data.name,
          reason: reason.trim()
        }
      });
    }
  });
}
</script>

<template>
  <div class="card card-body text-start my-2 playerCard">
    <div class="row">
      <div class="col float-start h-100 text-lg-start text-center">
        <p>{{ data.name }}</p>
      </div>
      <div class="col float-end d-flex flex-column">
        <button :class="censorBtnClass" @click="censorName">{{ censorBtnText }}</button>
        <button :class="kickBtnClass" @click="kick">{{ kickBtnText }}</button>
      </div>
    </div>
  </div>
</template>