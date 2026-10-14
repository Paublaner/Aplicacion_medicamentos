import { Sesion } from '../models/usuario';

/** Puerto de salida (driven): dónde se guarda la sesión para no tener que volver a entrar. */
export abstract class SesionAlmacen {
  abstract leer(): Sesion | null;
  abstract guardar(sesion: Sesion): void;
  abstract borrar(): void;
}
