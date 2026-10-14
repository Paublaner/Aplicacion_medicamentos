import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { EnvironmentProviders, Provider, makeEnvironmentProviders } from '@angular/core';
import { environment } from '../../environments/environment';
import { AsistenteRepositorio } from '../domain/ports/asistente.repositorio';
import { AuthRepositorio } from '../domain/ports/auth.repositorio';
import { MedicamentoRepositorio } from '../domain/ports/medicamento.repositorio';
import { SesionAlmacen } from '../domain/ports/sesion.almacen';
import { ApiAsistenteRepositorio } from './http/api-asistente.repositorio';
import { ApiAuthRepositorio } from './http/api-auth.repositorio';
import { ApiMedicamentoRepositorio } from './http/api-medicamento.repositorio';
import { API_URL } from './http/api-url';
import { authInterceptor } from './http/auth.interceptor';
import { CHAT_URL } from './http/chat-url';
import { LocalSesionAlmacen } from './storage/local-sesion.almacen';

/**
 * Aquí se "enchufa" cada puerto con su adaptador. Es el único sitio que sabe qué
 * implementación se usa: para cambiar de API a, por ejemplo, guardar los datos en el
 * propio móvil, bastaría con crear otro adaptador y cambiar la línea `useClass`.
 */
export function provideInfraestructura(): EnvironmentProviders {
  const adaptadores: Provider[] = [
    { provide: API_URL, useValue: environment.apiUrl },
    { provide: CHAT_URL, useValue: environment.chatUrl },
    { provide: AuthRepositorio, useClass: ApiAuthRepositorio },
    { provide: MedicamentoRepositorio, useClass: ApiMedicamentoRepositorio },
    { provide: SesionAlmacen, useClass: LocalSesionAlmacen },
    { provide: AsistenteRepositorio, useClass: ApiAsistenteRepositorio },
  ];

  return makeEnvironmentProviders([provideHttpClient(withInterceptors([authInterceptor])), ...adaptadores]);
}
