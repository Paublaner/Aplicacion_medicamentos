import { HttpErrorResponse, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthServicio } from '../../application/auth.servicio';
import { API_URL } from './api-url';

// Peticiones que no llevan token y que no deben intentar renovarlo si fallan
const RUTAS_PUBLICAS = ['/usuarios', '/refresh-token'];

function conToken(peticion: HttpRequest<unknown>, token: string) {
  return peticion.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
}

/**
 * Añade el JWT a cada petición. El JWT caduca a los 15 minutos: si la API responde 401,
 * se renueva con el refresh token y se repite la petición una vez. Si la renovación
 * también falla, la sesión ha caducado del todo y se vuelve al login.
 */
export const authInterceptor: HttpInterceptorFn = (peticion, siguiente) => {
  const auth = inject(AuthServicio);
  const router = inject(Router);
  const api = inject(API_URL);

  if (RUTAS_PUBLICAS.some(ruta => peticion.url.startsWith(api + ruta))) {
    return siguiente(peticion);
  }

  const token = auth.token;
  const peticionFinal = token ? conToken(peticion, token) : peticion;

  return siguiente(peticionFinal).pipe(
    catchError((error: unknown) => {
      if (!(error instanceof HttpErrorResponse) || error.status !== 401 || !auth.autenticado()) {
        return throwError(() => error);
      }

      return auth.renovarToken().pipe(
        switchMap(nuevoToken => siguiente(conToken(peticion, nuevoToken))),
        catchError(errorRenovacion => {
          auth.cerrarSesion();
          router.navigate(['/login']);
          return throwError(() => errorRenovacion);
        }),
      );
    }),
  );
};
