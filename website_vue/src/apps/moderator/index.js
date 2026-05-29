export default {
  id: 'moderator',
  load: () => import('./index.vue'),
  handlers: {
    message(msg, runtime) {
      switch (msg.key) {
        case 'moderateInput':
          runtime.addInput(msg.val);
          break;

        case 'dropInput':
          if (runtime.inputs[msg.val.id])
            delete runtime.inputs[msg.val.id];
          break;

        case 'playerConnected':
          runtime.addPlayer(msg.val);
          break;

        case 'inputModerated':
          runtime.modifyInput(msg.val);
          break;

        case 'nameCensored':
          runtime.modifyPlayer({
            name: msg.val.name,
            nameCensored: true
          });
          break;

        case 'playerKicked':
          runtime.modifyPlayer({
            name: msg.val.name,
            kicked: true
          });
          break;

        case 'welcomeRestoreState':
          msg.val.players.forEach(player => {
            runtime.addPlayer(player);
          });

          msg.val.inputs.forEach(input => {
            runtime.addInput(input);
          });
          break;
      }
    }
  }
}