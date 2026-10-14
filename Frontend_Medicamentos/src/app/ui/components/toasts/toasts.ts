import { Component, inject } from '@angular/core';
import { ToastService } from '../../toast.service';
import { Icono } from '../icono/icono';

@Component({
  selector: 'app-toasts',
  imports: [Icono],
  template: `
    <div class="contenedor" aria-live="polite">
      @for (toast of servicio.toasts(); track toast.id) {
        <div class="toast" [class.error]="toast.tipo === 'error'">
          <span class="icono">
            <app-icono [nombre]="toast.tipo === 'exito' ? 'check' : 'alerta'" [tamano]="18" />
          </span>
          <p>{{ toast.mensaje }}</p>
          <button class="cerrar" (click)="servicio.cerrar(toast.id)" aria-label="Cerrar">
            <app-icono nombre="cerrar" [tamano]="16" />
          </button>
        </div>
      }
    </div>
  `,
  styles: `
    .contenedor {
      position: fixed;
      right: 20px;
      bottom: 20px;
      z-index: 100;
      display: flex;
      flex-direction: column;
      gap: 10px;
      max-width: min(380px, calc(100vw - 40px));
    }
    .toast {
      display: flex;
      align-items: center;
      gap: 12px;
      padding: 14px 16px;
      background: var(--blanco);
      border: 1px solid var(--borde);
      border-left: 4px solid var(--exito);
      border-radius: 14px;
      box-shadow: var(--sombra-lg);
      animation: aparecer 0.25s ease;
    }
    .toast.error { border-left-color: var(--peligro); }
    .icono {
      display: grid;
      place-items: center;
      width: 30px;
      height: 30px;
      border-radius: 50%;
      background: var(--exito-suave);
      color: var(--exito);
    }
    .toast.error .icono { background: var(--peligro-suave); color: var(--peligro); }
    p { flex: 1; font-size: 0.9rem; font-weight: 500; }
    .cerrar {
      border: none;
      background: none;
      color: var(--texto-suave);
      cursor: pointer;
      padding: 4px;
      display: grid;
    }
  `,
})
export class Toasts {
  protected readonly servicio = inject(ToastService);
}
