import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import { MensajeChat } from '../../domain/models/chat';
import { AsistenteRepositorio } from '../../domain/ports/asistente.repositorio';
import { CHAT_URL } from './chat-url';
import { traducirError } from './traducir-error';

/** Adaptador del puerto AsistenteRepositorio que habla con el servidor de Lucía (FastAPI + Ollama). */
@Injectable()
export class ApiAsistenteRepositorio extends AsistenteRepositorio {
  private readonly http = inject(HttpClient);
  private readonly url = inject(CHAT_URL);

  responder(conversacion: MensajeChat[]): Observable<string> {
    // El servidor usa los roles de Ollama: user / assistant
    const mensajes = conversacion.map(m => ({ role: m.rol === 'usuario' ? 'user' : 'assistant', content: m.texto }));

    return this.http.post<{ respuesta: string }>(`${this.url}/chat`, { mensajes }).pipe(
      map(r => r.respuesta),
      traducirError('Lucía no ha podido responder. Inténtalo de nuevo.'),
    );
  }
}
