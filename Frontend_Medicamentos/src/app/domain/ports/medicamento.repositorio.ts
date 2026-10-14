import { Observable } from 'rxjs';
import { DiaSemana, Medicamento, MomentoDia, NuevoMedicamento } from '../models/medicamento';

/**
 * Puerto de salida (driven): de dónde salen y dónde se guardan los medicamentos.
 * Es una clase abstracta en vez de una interfaz porque Angular la usa como "token"
 * para la inyección de dependencias (las interfaces desaparecen al compilar).
 */
export abstract class MedicamentoRepositorio {
  abstract obtenerPorDia(dia: DiaSemana): Observable<Medicamento[]>;
  abstract anadir(medicamento: NuevoMedicamento): Observable<void>;
  abstract eliminar(nombre: string, dia: DiaSemana, momento: MomentoDia): Observable<void>;
}
