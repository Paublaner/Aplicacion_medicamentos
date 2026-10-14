import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, forkJoin, map } from 'rxjs';
import { DiaSemana, MOMENTOS, Medicamento, MomentoDia, NuevoMedicamento } from './models';

export type MedicamentosPorMomento = Record<MomentoDia, Medicamento[]>;

@Injectable({ providedIn: 'root' })
export class MedicamentosService {
  private readonly http = inject(HttpClient);

  obtener(dia: DiaSemana, momentoDia: MomentoDia): Observable<Medicamento[]> {
    const params = new HttpParams().set('momentoDia', momentoDia);

    return this.http
      .get<{ medicamentos: Medicamento[] }>(`/api/medicamentos/dia/${dia}`, { params })
      .pipe(map(respuesta => respuesta.medicamentos));
  }

  /** La API filtra por día y momento, así que para un día completo se hacen 3 peticiones en paralelo. */
  obtenerDia(dia: DiaSemana): Observable<MedicamentosPorMomento> {
    return forkJoin({
      Desayuno: this.obtener(dia, 'Desayuno'),
      Comida: this.obtener(dia, 'Comida'),
      Cena: this.obtener(dia, 'Cena'),
    });
  }

  anadir(medicamento: NuevoMedicamento): Observable<void> {
    return this.http.post<void>('/api/medicamentos', medicamento);
  }

  eliminar(nombre: string, diaSemana: DiaSemana, momentoDia: MomentoDia): Observable<void> {
    const params = new HttpParams()
      .set('nombre', nombre)
      .set('diaSemana', diaSemana)
      .set('momentoDia', momentoDia);

    return this.http.delete<void>('/api/medicamentos', { params });
  }
}

export function medicamentosVacios(): MedicamentosPorMomento {
  return Object.fromEntries(MOMENTOS.map(m => [m.valor, []])) as unknown as MedicamentosPorMomento;
}
