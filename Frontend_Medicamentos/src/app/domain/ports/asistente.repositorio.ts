import { Observable } from 'rxjs';
import { MensajeChat } from '../models/chat';

/** Puerto de salida (driven): quién contesta en el chat. Recibe la conversación entera y devuelve la respuesta. */
export abstract class AsistenteRepositorio {
  abstract responder(conversacion: MensajeChat[]): Observable<string>;
}
