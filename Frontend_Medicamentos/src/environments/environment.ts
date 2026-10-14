// Desarrollo (ng serve): el proxy de proxy.conf.json manda /api a http://localhost:5241
// y /chat-api al servidor de Lucía en http://localhost:8000
export const environment = {
  apiUrl: '/api',
  chatUrl: '/chat-api',
};
