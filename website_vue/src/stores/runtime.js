import { defineStore } from "pinia";

export const useRuntimeStore = defineStore('runtime', {
  state: () => ({
    inputs: [],
    canSendInputs: true,
    playerData: null,
    kickByIpEnabled: false,
    inputData: {}
  }),

  actions: {
    addInput(id, input) {
      input.id = id;
      this.inputs.push(input);
    },
    clearInputs() {
      this.inputs = [];
    },
    handleInputMessage(message) {
      this.clearInputs();
      this.canSendInputs = true;
      Object.entries(message.val.inputs).forEach(([id, data]) => {
        this.addInput(id, data);
      });
      this.inputData = {
        prompt: message.val.prompt,
        submitButton: message.val.submitButton
      }
    }
  },

  getters: {
    anyInputs: (state) => state.inputs.length > 0
  }
})