import { InjectionToken } from '@angular/core';

/**
 * Dirección base de la API. Sale de src/environments:
 *  - en el navegador (ng serve) es '/api' y el proxy la redirige a localhost:5241
 *  - en el móvil tiene que ser una dirección completa que el teléfono pueda alcanzar
 */
export const API_URL = new InjectionToken<string>('API_URL');
