import { Injectable, signal } from '@angular/core';

export interface Toast {
  id: number;
  tipo: 'exito' | 'error';
  mensaje: string;
}

@Injectable({ providedIn: 'root' })
export class ToastService {
  private siguienteId = 0;
  readonly toasts = signal<Toast[]>([]);

  exito(mensaje: string): void {
    this.mostrar('exito', mensaje);
  }

  error(mensaje: string): void {
    this.mostrar('error', mensaje);
  }

  cerrar(id: number): void {
    this.toasts.update(lista => lista.filter(t => t.id !== id));
  }

  private mostrar(tipo: Toast['tipo'], mensaje: string): void {
    const id = ++this.siguienteId;
    this.toasts.update(lista => [...lista, { id, tipo, mensaje }]);
    setTimeout(() => this.cerrar(id), 4000);
  }
}
