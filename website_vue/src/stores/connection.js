import { defineStore } from "pinia";

export const useConnectionStore = defineStore('connection', {
  state: () => ({
  connected: false,
  connecting: false,
  roomData: null,
  connectionData: {
    role: 'player',
    moderator: false
  }
  })
})