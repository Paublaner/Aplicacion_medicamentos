import { Injectable, inject, signal } from '@angular/core';
import { MensajeChat } from '../domain/models/chat';
import { mensajeDeError } from '../domain/models/error-dominio';
import { AsistenteRepositorio } from '../domain/ports/asistente.repositorio';

/**
 * Caso de uso del chat con Lucía. Guarda la conversación aquí (y no en el servidor), porque
 * el modelo no recuerda nada: en cada pregunta hay que mandarle la conversación entera.
 */
@Injectable({ providedIn: 'root' })
export class AsistenteServicio {
  private readonly repositorio = inject(AsistenteRepositorio);

  readonly mensajes = signal<MensajeChat[]>([]);
  readonly pensando = signal(false);
  readonly error = signal<string | null>(null);

  enviar(texto: string): void {
    texto = texto.trim();
    if (!texto || this.pensando()) {
      return;
    }

    this.mensajes.update(lista => [...lista, { rol: 'usuario', texto }]);
    this.preguntar();
  }

  /** Vuelve a intentar la última pregunta si falló. */
  reintentar(): void {
    if (!this.pensando() && this.mensajes().at(-1)?.rol === 'usuario') {
      this.preguntar();
    }
  }

  vaciar(): void {
    this.mensajes.set([]);
    this.error.set(null);
  }

  private preguntar(): void {
    this.pensando.set(true);
    this.error.set(null);

    this.repositorio.responder(this.mensajes()).subscribe({
      next: respuesta => {
        this.mensajes.update(lista => [...lista, { rol: 'lucia', texto: respuesta }]);
        this.pensando.set(false);
      },
      error: e => {
        this.error.set(mensajeDeError(e, 'Lucía no ha podido responder. Inténtalo de nuevo.'));
        this.pensando.set(false);
      },
    });
  }
}
