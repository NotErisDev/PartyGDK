import { on } from "../websocket/socket";

const apps = { };

export function registerApp(app, runtime) {
  apps[app.id] = app;
  Object.entries(app.handlers).forEach(([type, handler]) => {
    on(type, (msg) => {
      handler(msg, runtime);
    });
  });
}

export function getApp(id) {
  return apps[id];
}