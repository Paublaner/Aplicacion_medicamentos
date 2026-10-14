import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, forkJoin, map } from 'rxjs';
import { DiaSemana, MOMENTOS, Medicamento, MomentoDia, NuevoMedicamento } from '../../domain/models/medicamento';
import { MedicamentoRepositorio } from '../../domain/ports/medicamento.repositorio';
import { API_URL } from './api-url';
import { traducirError } from './traducir-error';

/** Adaptador del puerto MedicamentoRepositorio que habla con la API de .NET. */
@Injectable()
export class ApiMedicamentoRepositorio extends MedicamentoRepositorio {
  private readonly http = inject(HttpClient);
  private readonly api = inject(API_URL);

  /** La API filtra por día y momento, así que para un día completo se hacen 3 peticiones en paralelo. */
  obtenerPorDia(dia: DiaSemana): Observable<Medicamento[]> {
    return forkJoin(MOMENTOS.map(m => this.obtener(dia, m.valor))).pipe(
      map(listas => listas.flat()),
      traducirError('No se han podido cargar los medicamentos.'),
    );
  }

  anadir(medicamento: NuevoMedicamento): Observable<void> {
    return this.http
      .post<void>(`${this.api}/medicamentos`, medicamento)
      .pipe(traducirError('No se ha podido guardar el medicamento.'));
  }

  eliminar(nombre: string, dia: DiaSemana, momento: MomentoDia): Observable<void> {
    const params = new HttpParams().set('nombre', nombre).set('diaSemana', dia).set('momentoDia', momento);

    return this.http
      .delete<void>(`${this.api}/medicamentos`, { params })
      .pipe(traducirError('No se ha podido eliminar el medicamento.'));
  }

  private obtener(dia: DiaSemana, momento: MomentoDia): Observable<Medicamento[]> {
    const params = new HttpParams().set('momentoDia', momento);

    return this.http
      .get<{ medicamentos: Medicamento[] }>(`${this.api}/medicamentos/dia/${dia}`, { params })
      .pipe(map(respuesta => respuesta.medicamentos));
  }
}
