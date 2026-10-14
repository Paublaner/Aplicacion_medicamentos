import { Observable } from 'rxjs';
import { Sesion, Tokens } from '../models/usuario';

/** Puerto de salida (driven): quién comprueba usuarios y emite tokens. */
export abstract class AuthRepositorio {
  abstract registrar(nombre: string, correo: string, contrasena: string): Observable<void>;
  abstract login(correo: string, contrasena: string): Observable<Sesion>;
  abstract renovar(refreshToken: string): Observable<Tokens>;
}
