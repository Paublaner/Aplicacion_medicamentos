// Producción / APK de Android. En el móvil no hay proxy ni "localhost": hay que poner
// la dirección completa del servidor donde corre la API, accesible desde el teléfono.
//  - Emulador de Android Studio: 'http://10.0.2.2:5241'  (10.0.2.2 = el localhost del PC)
//  - Móvil real, misma WiFi:     'http://192.168.1.XX:5241'  (la IP del PC que ejecuta la API)
//  - Publicada en internet:      'https://tu-api.ejemplo.com'
export const environment = {
  apiUrl: 'http://192.168.0.30:5241',
  // Servidor de Lucía (Chatbot_Medicamentos/servidor.py), en el mismo PC que la API
  chatUrl: 'http://192.168.0.30:8000',
};
