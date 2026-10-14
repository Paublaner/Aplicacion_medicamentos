import { HttpErrorResponse } from '@angular/common/http';
import { OperatorFunction, catchError, throwError } from 'rxjs';
import { ErrorDominio } from '../../domain/models/error-dominio';

/**
 * La API devuelve los errores en varios formatos:
 *  - texto plano: BadRequest("El nombre no puede estar vacío.")
 *  - { mensaje: "..." }
 *  - ValidationProblemDetails de [ApiController]: { errors: { Contrasena: ["..."] } }
 *  - el servidor de Lucía (FastAPI): { detail: "..." }
 * Aquí se convierten todos en un ErrorDominio, para que la interfaz no dependa de HTTP.
 */
export function aErrorDominio(error: unknown, porDefecto: string): ErrorDominio {
  if (error instanceof ErrorDominio) {
    return error;
  }
  if (!(error instanceof HttpErrorResponse)) {
    return new ErrorDominio(porDefecto);
  }

  if (error.status === 0) {
    return new ErrorDominio('No se puede conectar con el servidor. Revisa la conexión a internet.', 'sin-conexion');
  }
  if (error.status === 429) {
    return new ErrorDominio('Demasiadas peticiones. Espera un minuto e inténtalo de nuevo.');
  }

  const tipo =
    error.status === 401 ? 'no-autorizado' : error.status === 404 ? 'no-encontrado' : error.status === 400 ? 'validacion' : 'desconocido';
  const cuerpo = error.error;

  if (typeof cuerpo === 'string' && cuerpo.trim()) {
    return new ErrorDominio(cuerpo, tipo);
  }
  if (cuerpo?.mensaje) {
    return new ErrorDominio(cuerpo.mensaje, tipo);
  }
  if (typeof cuerpo?.detail === 'string') {
    return new ErrorDominio(cuerpo.detail, tipo);
  }
  if (cuerpo?.errors) {
    const primero = Object.values(cuerpo.errors as Record<string, string[]>).flat()[0];
    if (primero) {
      return new ErrorDominio(primero, tipo);
    }
  }

  return new ErrorDominio(porDefecto, tipo);
}

/** Operador para usar en los adaptadores: `.pipe(traducirError('No se pudo...'))` */
export function traducirError<T>(porDefecto: string): OperatorFunction<T, T> {
  return catchError((error: unknown) => throwError(() => aErrorDominio(error, porDefecto)));
}
