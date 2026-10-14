import { Component, ElementRef, effect, inject, signal, viewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AsistenteServicio } from '../../../application/asistente.servicio';
import { Icono } from '../icono/icono';

const SUGERENCIAS = [
  '¿Qué me toca tomar hoy en el desayuno?',
  '¿Qué tengo que tomar esta noche?',
  '¿Qué me toca el lunes en la comida?',
];

/** Botón flotante que abre el chat con Lucía, la asistente. */
@Component({
  selector: 'app-chat-lucia',
  imports: [FormsModule, Icono],
  templateUrl: './chat-lucia.html',
  styleUrl: './chat-lucia.css',
})
export class ChatLucia {
  protected readonly asistente = inject(AsistenteServicio);
  protected readonly sugerencias = SUGERENCIAS;

  protected readonly abierto = signal(false);
  protected texto = '';

  private readonly lista = viewChild<ElementRef<HTMLElement>>('lista');
  private readonly campo = viewChild<ElementRef<HTMLTextAreaElement>>('campo');

  constructor() {
    // Cada vez que llega un mensaje (o Lucía empieza a pensar) se baja hasta el final
    effect(() => {
      this.asistente.mensajes();
      this.asistente.pensando();
      this.asistente.error();
      const lista = this.lista()?.nativeElement;
      if (lista) {
        setTimeout(() => lista.scrollTo({ top: lista.scrollHeight, behavior: 'smooth' }));
      }
    });
  }

  protected alternar(): void {
    this.abierto.update(abierto => !abierto);
    if (this.abierto()) {
      setTimeout(() => this.campo()?.nativeElement.focus());
    }
  }

  protected enviar(texto = this.texto): void {
    this.asistente.enviar(texto);
    this.texto = '';
  }

  /** Enter envía; Mayús + Enter hace un salto de línea. */
  protected alPulsarTecla(evento: KeyboardEvent): void {
    if (evento.key === 'Enter' && !evento.shiftKey) {
      evento.preventDefault();
      this.enviar();
    }
  }
}
