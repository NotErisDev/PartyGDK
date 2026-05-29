<script setup>
import './styles.css';

import { useModeratorStore } from '@/stores/moderator';
import InputCard from '@/components/moderator/InputCard.vue';
import PlayerCard from '@/components/moderator/PlayerCard.vue';

const store = useModeratorStore();
</script>

<template>
  <div class="container content col-lg-5 col-md-6">
    <div class="buttonsHeader">
      <button class="btn btn-primary mx-2" id="managePlayersBtn" data-bs-toggle="collapse"
      data-bs-target="#playersCollapse" aria-expanded="false" aria-controls="playersCollapse">
        Управление игроками
      </button>
    </div>
    <div class="collapse col-7 mx-auto" id="playersCollapse">
      <PlayerCard v-for="card in Object.values(store.players)" :data="card" />
    </div>
    <hr>
    <div class="container col-7 mx-auto">
      <p class="waitingForInputs" v-if="!store.anyInputs">Пока нечего модерировать…</p>
      <InputCard v-for="card in Object.values(store.inputs)" :data="card" />
    </div>
  </div>
</template>