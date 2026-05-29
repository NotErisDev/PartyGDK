import { defineStore } from "pinia";

export const useModeratorStore = defineStore('moderator', {
  state: () => ({
    players: {},
    inputs: {},
    kickByIpEnabled: false
  }),

  actions: {
    addPlayer(data) {
      this.players[data.name] = data;
    },

    addInput(data) {
      this.inputs[data.id] = data;
    },

    modifyPlayer(data) {
      Object.entries(data).forEach(([key, value]) => {
        this.players[data.name][key] = value;
      });
    },

    modifyInput(data) {
      Object.entries(data).forEach(([key, value]) => {
        this.inputs[data.id][key] = value;
      });
    }
  },

  getters: {
    anyInputs() {
      return Object.values(this.inputs).length > 0;
    }
  }
})