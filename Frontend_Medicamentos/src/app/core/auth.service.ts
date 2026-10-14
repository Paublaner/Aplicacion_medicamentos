import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, finalize, map, shareReplay, tap } from 'rxjs';
import { RespuestaLogin, RespuestaRefresh, Usuario } from './models';

const CLAVE_SESION = 'meditrack.sesion';

interface Sesion {
  token: string;
  refreshToken: string;
  usuario: Usuario;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);

  private readonly sesion = signal<Sesion | null>(this.leerSesion());
  private refreshEnCurso$: Observable<string> | null = null;

  readonly usuario = computed(() => this.sesion()?.usuario ?? null);
  readonly autenticado = computed(() => this.sesion() !== null);

  get token(): string | null {
    return this.sesion()?.token ?? null;
  }

  registrar(nombre: string, correo: string, contrasena: string): Observable<void> {
    return this.http.post<void>('/api/usuarios', { nombre, correo, contrasena });
  }

  login(correo: string, contrasena: string): Observable<Usuario> {
    // La API hace el login por GET con los datos en la URL (pendiente de pasar a POST en el backend)
    const params = new HttpParams().set('correo', correo).set('contrasena', contrasena);

    return this.http.get<RespuestaLogin>('/api/usuarios', { params }).pipe(
      tap(respuesta => this.guardarSesion(respuesta)),
      map(respuesta => respuesta.usuario),
    );
  }

  /**
   * Pide un JWT nuevo con el refresh token. Si varias peticiones fallan a la vez con 401,
   * todas comparten la misma renovación: el backend revoca el refresh token al usarlo,
   * así que una segunda renovación con el mismo token fallaría.
   */
  renovarToken(): Observable<string> {
    const actual = this.sesion();
    if (!actual) {
      throw new Error('No hay sesión');
    }

    this.refreshEnCurso$ ??= this.http
      .post<RespuestaRefresh>('/api/refresh-token', { refreshToken: actual.refreshToken })
      .pipe(
        tap(r => this.guardarSesion({ ...actual, token: r.token, refreshToken: r.refreshToken })),
        map(r => r.token),
        finalize(() => (this.refreshEnCurso$ = null)),
        shareReplay(1),
      );

    return this.refreshEnCurso$;
  }

  cerrarSesion(): void {
    this.sesion.set(null);
    try {
      localStorage.removeItem(CLAVE_SESION);
    } catch {}
    this.router.navigate(['/login']);
  }

  private guardarSesion(sesion: Sesion): void {
    this.sesion.set(sesion);
    try {
      localStorage.setItem(CLAVE_SESION, JSON.stringify(sesion));
    } catch {}
  }

  private leerSesion(): Sesion | null {
    try {
      const guardada = localStorage.getItem(CLAVE_SESION);
      return guardada ? (JSON.parse(guardada) as Sesion) : null;
    } catch {
      return null;
    }
  }
}
