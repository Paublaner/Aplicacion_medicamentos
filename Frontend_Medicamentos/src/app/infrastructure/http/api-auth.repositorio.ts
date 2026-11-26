import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Sesion, Tokens } from '../../domain/models/usuario';
import { AuthRepositorio } from '../../domain/ports/auth.repositorio';
import { API_URL } from './api-url';
import { traducirError } from './traducir-error';

/** Adaptador del puerto AuthRepositorio que habla con la API de .NET. */
@Injectable()
export class ApiAuthRepositorio extends AuthRepositorio {
  private readonly http = inject(HttpClient);
  private readonly api = inject(API_URL);

  registrar(nombre: string, correo: string, contrasena: string): Observable<void> {
    return this.http
      .post<void>(`${this.api}/usuarios`, { nombre, correo, contrasena })
      .pipe(traducirError('No se ha podido crear la cuenta.'));
  }

  login(correo: string, contrasena: string): Observable<Sesion> {
    // Por POST y en el cuerpo: si fueran en la URL, la contraseña acabaría guardada en logs e historiales
    return this.http
      .post<Sesion>(`${this.api}/usuarios/login`, { correo, contrasena })
      .pipe(traducirError('No se ha podido iniciar sesión.'));
  }

  renovar(refreshToken: string): Observable<Tokens> {
    return this.http
      .post<Tokens>(`${this.api}/refresh-token`, { refreshToken })
      .pipe(traducirError('La sesión ha caducado.'));
  }
}
