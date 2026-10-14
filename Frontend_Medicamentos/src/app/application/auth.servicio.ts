import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable, finalize, map, shareReplay, switchMap, tap, throwError } from 'rxjs';
import { ErrorDominio } from '../domain/models/error-dominio';
import { Sesion, Usuario } from '../domain/models/usuario';
import { AuthRepositorio } from '../domain/ports/auth.repositorio';
import { SesionAlmacen } from '../domain/ports/sesion.almacen';

/** Casos de uso de la cuenta: registrarse, entrar, salir y mantener la sesión viva. */
@Injectable({ providedIn: 'root' })
export class AuthServicio {
  private readonly repositorio = inject(AuthRepositorio);
  private readonly almacen = inject(SesionAlmacen);

  private readonly sesion = signal<Sesion | null>(this.almacen.leer());
  private renovacionEnCurso$: Observable<string> | null = null;

  readonly usuario = computed<Usuario | null>(() => this.sesion()?.usuario ?? null);
  readonly autenticado = computed(() => this.sesion() !== null);

  get token(): string | null {
    return this.sesion()?.token ?? null;
  }

  login(correo: string, contrasena: string): Observable<Usuario> {
    return this.repositorio.login(correo, contrasena).pipe(
      tap(sesion => this.guardar(sesion)),
      map(sesion => sesion.usuario),
    );
  }

  /** Tras registrarse se inicia sesión directamente, para no obligar a escribir los datos otra vez. */
  registrarYEntrar(nombre: string, correo: string, contrasena: string): Observable<Usuario> {
    return this.repositorio
      .registrar(nombre, correo, contrasena)
      .pipe(switchMap(() => this.login(correo, contrasena)));
  }

  /**
   * Pide un token nuevo con el refresh token. Si varias peticiones caducan a la vez,
   * todas comparten la misma renovación: la API revoca el refresh token al usarlo,
   * así que una segunda renovación con el mismo token fallaría.
   */
  renovarToken(): Observable<string> {
    const actual = this.sesion();
    if (!actual) {
      return throwError(() => new ErrorDominio('No hay sesión.', 'no-autorizado'));
    }

    this.renovacionEnCurso$ ??= this.repositorio.renovar(actual.refreshToken).pipe(
      tap(tokens => this.guardar({ ...actual, ...tokens })),
      map(tokens => tokens.token),
      finalize(() => (this.renovacionEnCurso$ = null)),
      shareReplay(1),
    );

    return this.renovacionEnCurso$;
  }

  cerrarSesion(): void {
    this.sesion.set(null);
    this.almacen.borrar();
  }

  private guardar(sesion: Sesion): void {
    this.sesion.set(sesion);
    this.almacen.guardar(sesion);
  }
}
