/**
 * Error que entienden la aplicación y la interfaz. Los adaptadores traducen sus errores
 * técnicos (HttpErrorResponse, fallos de almacenamiento...) a este tipo, así la interfaz
 * nunca sabe si los datos vienen de una API, del móvil o de otro sitio.
 */
export class ErrorDominio extends Error {
  constructor(
    mensaje: string,
    readonly tipo: 'sin-conexion' | 'no-autorizado' | 'no-encontrado' | 'validacion' | 'desconocido' = 'desconocido',
  ) {
    super(mensaje);
    this.name = 'ErrorDominio';
  }
}

export function mensajeDeError(error: unknown, porDefecto = 'Ha ocurrido un error inesperado.'): string {
  return error instanceof ErrorDominio ? error.message : porDefecto;
}
