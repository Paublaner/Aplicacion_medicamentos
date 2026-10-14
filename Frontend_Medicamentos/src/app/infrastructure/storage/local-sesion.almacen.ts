import { Injectable } from '@angular/core';
import { Sesion } from '../../domain/models/usuario';
import { SesionAlmacen } from '../../domain/ports/sesion.almacen';

const CLAVE = 'meditrack.sesion';

/**
 * Guarda la sesión en localStorage. Funciona igual en el navegador y dentro de la app
 * de Android (Capacitor usa un WebView, que conserva el localStorage entre aperturas).
 */
@Injectable()
export class LocalSesionAlmacen extends SesionAlmacen {
  leer(): Sesion | null {
    try {
      const guardada = localStorage.getItem(CLAVE);
      return guardada ? (JSON.parse(guardada) as Sesion) : null;
    } catch {
      return null;
    }
  }

  guardar(sesion: Sesion): void {
    try {
      localStorage.setItem(CLAVE, JSON.stringify(sesion));
    } catch {}
  }

  borrar(): void {
    try {
      localStorage.removeItem(CLAVE);
    } catch {}
  }
}
