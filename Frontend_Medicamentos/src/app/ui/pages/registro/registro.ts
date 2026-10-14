import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthServicio } from '../../../application/auth.servicio';
import { mensajeDeError } from '../../../domain/models/error-dominio';
import { MarcaAuth } from '../../components/marca-auth/marca-auth';
import { ToastService } from '../../toast.service';

type Campo = 'nombre' | 'correo' | 'contrasena';

@Component({
  selector: 'app-registro',
  imports: [ReactiveFormsModule, RouterLink, MarcaAuth],
  templateUrl: './registro.html',
  host: { class: 'auth' },
})
export class Registro {
  private readonly auth = inject(AuthServicio);
  private readonly router = inject(Router);
  private readonly toast = inject(ToastService);

  protected readonly cargando = signal(false);
  protected readonly error = signal<string | null>(null);

  // Mismas reglas que UsuarioDto en la API: contraseña entre 8 y 100 caracteres
  protected readonly formulario = inject(FormBuilder).nonNullable.group({
    nombre: ['', Validators.required],
    correo: ['', [Validators.required, Validators.email]],
    contrasena: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(100)]],
  });

  protected invalido(campo: Campo): boolean {
    const control = this.formulario.controls[campo];
    return control.invalid && control.touched;
  }

  protected enviar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.cargando.set(true);
    this.error.set(null);

    const { nombre, correo, contrasena } = this.formulario.getRawValue();

    this.auth.registrarYEntrar(nombre, correo, contrasena).subscribe({
      next: usuario => {
        this.toast.exito(`¡Bienvenido, ${usuario.nombre}! Tu cuenta está lista.`);
        this.router.navigate(['/panel']);
      },
      error: e => {
        this.error.set(mensajeDeError(e, 'No se ha podido crear la cuenta.'));
        this.cargando.set(false);
      },
    });
  }
}
