export function swalError(message, title, reload) {
  Swal.fire({
    icon: 'error',
    title: title || 'Ошибка',
    text: message
  }).then(() => {
    if (reload)
      window.location.reload();
  });
}

export function setCookie(name, value, lifetime) {
  const d = new Date();
  d.setTime(d.getTime() + ((lifetime || 2592000) * 1000));
  let expires = "expires=" + d.toUTCString();

  document.cookie = name + "=" + value + ";" + expires + ";path=/";
}

export function getCookie(name) {
  let nameEQ = name + "=";
  let ca = document.cookie.split(';');

  for (let i = 0; i < ca.length; i++) {
    let c = ca[i];

    while (c.charAt(0) == ' ')
      c = c.substring(1, c.length);

    if (c.indexOf(nameEQ) == 0)
      return c.substring(nameEQ.length, c.length);
  }
  return null;
}

export function randomUUID() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}

export function escapeHTML(string) {
  const characterMap = {
    '&': '&amp;',
    '<': '&lt;',
    '>': '&gt;',
    '"': '&quot;',
    "'": '&#39;',
    '/': '&#x2F;',
    '`': '&#x60;',
    '=': '&#x3D;'
  };

  return String(string).replace(/[&<>"'`=\/]/g, function (c) {
    return characterMap[c];
  });
}

export function getUrlParameter(parameter) {
  let url = window.location.search.substring(1),
  vars = url.split('&');

  for (let i = 0; i < vars.length; i++) {
    let currentParam = vars[i].split('=');

    if (currentParam[0] === parameter)
      return decodeURIComponent(currentParam[1]) || true;
  }

  return false;
}

export function reloadWithoutQuery() {
  window.location.href = window.location.href.split('?')[0];
}