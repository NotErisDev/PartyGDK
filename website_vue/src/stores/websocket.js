import { defineStore } from "pinia";

export const useWebsocketStore = defineStore('websocket', {
  state: () => ({
    error: null,
    kicked: false,
    kickReason: null,
    errorTime: 0
  })
})