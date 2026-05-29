import config from "@/json/config.json";


export function getConnectionURL(websocket, moderator) {
  const parts = [
    websocket ? 'ws' : 'http',
    config.connection.secure ? 's' : '',
    '://',
    config.connection.host,
    ':',
    config.connection.port,
    websocket ? config.connection.websocket_suffix : ''
  ]

  if (websocket)
    parts.push(moderator ? '/moderate' : '/play');
  else
    parts.push(config.connection.get_room_url);

  return parts.join('');
}

export function addQueryParameters(url, params) {
  const encoded = Object.entries(params).map(([key, value]) => encodeURIComponent(key) + '=' + encodeURIComponent(value));
  return url + '?' + encoded.join('&');
}

export async function getRoomData(code) {
  const response = await fetch(addQueryParameters(getConnectionURL(), { code: code }));
  let data;

  try {
    data = await response.json();
  } catch {
    data = {};
  }

  data = data.body || {};
  data.status = response.status;
  return data;
}