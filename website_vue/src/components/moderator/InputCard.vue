<script setup>
import { computed } from 'vue';
import { send } from '@/core/websocket/socket';

const props = defineProps({
  data: Object
})

const approveBtnClass = computed(() => ('btn btnGreen ' + (props.data.status === 'approved' ? 'disabled' : '')).trim());
const rejectBtnClass = computed(() => ('btn btnRed ' + (props.data.status === 'rejected' ? 'disabled' : '')).trim());
const cardClass = computed(() => {
  let className = 'card card-body text-start my-2 input-card ';
  if (props.data.status === 'approved')
    className += 'bg-approved';
  else if (props.data.status === 'rejected')
    className += 'bg-rejected';

  return className.trim();
});

const approveBtnText = computed(() => props.data.status === 'approved' ? 'Одобрено' : 'Одобрить');
const rejectBtnText = computed(() => props.data.status === 'rejected' ? 'Отклонено' : 'Отклонить');

function approve() {
  send({
    key: 'moderateInput',
    val: {
      id: props.data.id,
      status: 'approved'
    }
  });
}

function reject() {
  send({
    key: 'moderateInput',
    val: {
      id: props.data.id,
      status: 'rejected'
    }
  });
}
</script>

<template>
  <div :class="cardClass">
    <div class="row">
      <div class="col float-start h-100 text-lg-start text-center">
        <p>{{ data.content }}</p>
        <p class="authorName">Автор: {{ data.playerName }}</p>
      </div>
      <div class="col float-end d-flex flex-column">
        <button :class="approveBtnClass" @click="approve">{{ approveBtnText }}</button>
        <button :class="rejectBtnClass" @click="reject">{{ rejectBtnText }}</button>
      </div>
    </div>
  </div>
</template>