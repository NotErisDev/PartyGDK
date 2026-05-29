import { connect, on } from "@/core/websocket/socket";
import { useConnectionStore } from "@/stores/connection";
import { useModeratorStore } from "@/stores/moderator";
import { useRuntimeStore } from "@/stores/runtime";
import { useWebsocketStore } from "@/stores/websocket";
import { addQueryParameters, getConnectionURL } from "@/utils/connection";
import { escapeHTML, getCookie, reloadWithoutQuery, setCookie, swalError } from "@/utils/controller";


export function play(password) {
  const store = useConnectionStore();
  if (store.connected || store.connecting)
    return swalError('Подключение либо в процессе, либо оно уже произошло.');
  if (!store.connectionData || !store.roomData || !store.connectionData.roomCode)
    return swalError('Укажите верный код комнаты.');
  if (store.connectionData.moderator && !password)
    return swalError('Укажите пароль модератора.');

  if (!store.connectionData.moderator && store.connectionData.role === 'player' && store.roomData.passwordRequired && !password) {
    let text = 'Чтобы зайти как игрок, нужно ввести 5-значный пароль.';
    if (store.roomData.audienceEnabled)
      text += ' Если у вас нет пароля, вы можете попробовать присоединиться к зрителям.';

    return Swal.fire({
      title: 'Требуется пароль',
      text: text,
      input: 'password',
      inputAttributes: {
        maxlength: 5,
        autocomplete: 'off'
      },
      confirmButtonText: 'Подтвердить',
      preConfirm: password => {
        play(password)
      }
    })
  }

  store.connecting = true;
  on('message', onWsMessage);
  on('error', onWsError);
  on('close', onWsClose);

  if (store.connectionData.moderator) {
    connect(addQueryParameters(getConnectionURL(true, true), {
      code: store.connectionData.roomCode,
      password: password
    }))
    return;
  }

  const params = {
    code: store.connectionData.roomCode,
    name: store.connectionData.playerName,
    role: store.connectionData.role,
    id: crypto.randomUUID()
  }

  const playerId = getCookie('playerId');
  if (playerId)
    params.id = playerId;
  if (password)
    params.password = password;

  const url = addQueryParameters(getConnectionURL(true), params);

  connect(url);
}

function onWsMessage(data) {
  switch (data.key) {
    case 'welcome':
      const connStore = useConnectionStore();
      connStore.connected = true;

      if (connStore.connectionData.moderator) {
        useModeratorStore().kickByIpEnabled = data.val.kickByIpEnabled;
      } else {
        setCookie('playerId', data.val.id);
        useRuntimeStore().playerData = data.val;
      }
      break;

    case 'playerKicked':
      if (!useConnectionStore().connectionData.moderator) {
        const wsStore = useWebsocketStore();
        wsStore.kicked = true;
        wsStore.kickReason = data.val.reason;
      }
      break;
  }

  if (data.error) {
    const store = useWebsocketStore();
    store.errorTime = Date.now();
    store.error = data.error;
  }
}

function onWsError() {
  const connStore = useConnectionStore();
  connStore.connecting = false;
  swalError('Произошла ошибка при подключении.');
}

function onWsClose() {
  const connStore = useConnectionStore();
  const wsStore = useWebsocketStore();

  const errors = {
    'Incorrect password': 'Неверный пароль.',
    'Sorry, this name has already been taken': 'Данное имя уже занято.',
    'The room is full': 'Комната заполнена.',
    'Audience limit exceeded': 'Достигнуто максимальное число зрителей в комнате.',
    'Audience is not enabled for this room': 'Зрители отключены для этой комнаты.',
    'Moderation is not enabled for this room': 'Модерация отключена для этой комнаты.',
    'You are being rate limited': 'Вы отправляете запросы слишком часто.',
    'Room not found': 'Комната не найдена.',
    'The room is locked': 'Игра уже началась.'
  };

  connStore.connecting = false;
  if (wsStore.error && Date.now() - wsStore.errorTime <= 5000) {
    swalError(errors[wsStore.error] || escapeHTML(wsStore.error), 'Ошибка', connStore.connected);
  } else {
    let description;
    if (wsStore.kicked) {
      description = 'Вы были исключены модератором.';
      if (wsStore.kickReason)
        description += `<br>Причина: ${escapeHTML(wsStore.kickReason)}`;
      else
        description += '<br>Причина не указана.';
    }

    if (description)
      description = escapeHTML(description);

    Swal.fire('Отключено', description).then(reloadWithoutQuery);
  }
}