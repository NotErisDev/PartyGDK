import mitt from "mitt";
import config from "@/json/config.json"

const emitter = mitt();

let socket;
let pingInterval;

export function connect(url) {
  socket = new WebSocket(url);

  socket.onmessage = (event) => {
    const message = JSON.parse(event.data);
    console.log(message);

    emitter.emit('message', message);
  }

  socket.onopen = (_) => {
    const interval = config.websocket.ping_interval || 30;
    if (interval > 0) {
      pingInterval = setInterval(() => {
        send({ key: 'ping', val: { } });
      }, interval * 1000);
    }

    emitter.emit('open');
  }

  socket.onclose = (_) => {
    if (pingInterval) {
      clearInterval(pingInterval);
    }

    emitter.emit('close');
  }

  socket.onerror = (_) => {
    emitter.emit('error');
  }
}

export function on(type, handler) {
  emitter.on(type, handler);
}

export function send(data) {
  socket.send(JSON.stringify(data));
}

export function imitate(type, data) {
  emitter.emit(type, data);
}