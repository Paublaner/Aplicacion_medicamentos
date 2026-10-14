import { InjectionToken } from '@angular/core';

/**
 * Dirección del servidor de Lucía (Chatbot_Medicamentos/servidor.py). Sale de src/environments:
 *  - en el navegador (ng serve) es '/chat-api' y el proxy la redirige a localhost:8000
 *  - en el móvil tiene que ser la dirección completa del PC donde corre el servidor
 */
export const CHAT_URL = new InjectionToken<string>('CHAT_URL');
