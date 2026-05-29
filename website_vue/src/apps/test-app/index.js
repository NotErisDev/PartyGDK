export default {
  id: 'test-app',
  load: () => import('./index.vue'),
  handlers: {
    message(msg, runtime) {
      switch (msg.key) {
        case 'input':
          runtime.handleInputMessage(msg);
          break;

        case 'logo':
          runtime.clearInputs();
          break;
      }
    }
  }
}