import { HttpErrorResponse } from '@angular/common/http';

/**
 * La API devuelve los errores en varios formatos:
 *  - texto plano: BadRequest("El nombre no puede estar vacío.")
 *  - { mensaje: "..." }
 *  - ValidationProblemDetails de [ApiController]: { errors: { Contrasena: ["..."] } }
 * Esta función saca un mensaje legible de cualquiera de ellos.
 */
export function extraerMensaje(error: unknown, porDefecto = 'Ha ocurrido un error inesperado.'): string {
  if (!(error instanceof HttpErrorResponse)) {
    return porDefecto;
  }

  if (error.status === 0) {
    return 'No se puede conectar con el servidor. ¿Está la API arrancada?';
  }

  if (error.status === 429) {
    return 'Demasiadas peticiones. Espera un minuto e inténtalo de nuevo.';
  }

  const cuerpo = error.error;

  if (typeof cuerpo === 'string' && cuerpo.trim()) {
    return cuerpo;
  }

  if (cuerpo?.mensaje) {
    return cuerpo.mensaje;
  }

  if (cuerpo?.errors) {
    const primero = Object.values(cuerpo.errors as Record<string, string[]>).flat()[0];
    if (primero) {
      return primero;
    }
  }

  return porDefecto;
}
