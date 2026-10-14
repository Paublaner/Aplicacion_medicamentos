import { HttpClient, HttpParams } from '@angular/common/http';
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
    // La API hace el login por GET con los datos en la URL (pendiente de pasar a POST en el backend)
    const params = new HttpParams().set('correo', correo).set('contrasena', contrasena);

    return this.http
      .get<Sesion>(`${this.api}/usuarios`, { params })
      .pipe(traducirError('No se ha podido iniciar sesión.'));
  }

  renovar(refreshToken: string): Observable<Tokens> {
    return this.http
      .post<Tokens>(`${this.api}/refresh-token`, { refreshToken })
      .pipe(traducirError('La sesión ha caducado.'));
  }
}
