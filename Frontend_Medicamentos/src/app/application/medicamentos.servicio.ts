import { Injectable, inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import {
  DiaSemana,
  Medicamento,
  MedicamentosPorMomento,
  NuevoMedicamento,
  agruparPorMomento,
} from '../domain/models/medicamento';
import { MedicamentoRepositorio } from '../domain/ports/medicamento.repositorio';

/** Casos de uso de los medicamentos. No sabe si los datos vienen de la API o de otro sitio. */
@Injectable({ providedIn: 'root' })
export class MedicamentosServicio {
  private readonly repositorio = inject(MedicamentoRepositorio);

  obtenerDia(dia: DiaSemana): Observable<MedicamentosPorMomento> {
    return this.repositorio.obtenerPorDia(dia).pipe(map(agruparPorMomento));
  }

  anadir(medicamento: NuevoMedicamento): Observable<void> {
    return this.repositorio.anadir({
      ...medicamento,
      nombre: medicamento.nombre.trim(),
      descripcion: medicamento.descripcion.trim(),
    });
  }

  eliminar(medicamento: Medicamento): Observable<void> {
    return this.repositorio.eliminar(medicamento.nombre, medicamento.diaSemana, medicamento.momentoDia);
  }
}
