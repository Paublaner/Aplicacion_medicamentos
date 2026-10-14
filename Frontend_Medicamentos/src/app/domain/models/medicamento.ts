// Los nombres de los valores coinciden con los enums de la API (se serializan como texto con JsonStringEnumConverter)

export type DiaSemana = 'Lunes' | 'Martes' | 'Miercoles' | 'Jueves' | 'Viernes' | 'Sabado' | 'Domingo';
export type MomentoDia = 'Desayuno' | 'Comida' | 'Cena';
export type Cantidad = 'Una' | 'Media' | 'DiezMiligramos' | 'VeinteMiligramos';

export interface Medicamento {
  id: number;
  nombre: string;
  descripcion: string;
  cantidad: Cantidad;
  diaSemana: DiaSemana;
  momentoDia: MomentoDia;
}

export type NuevoMedicamento = Omit<Medicamento, 'id'>;

export type MedicamentosPorMomento = Record<MomentoDia, Medicamento[]>;

export const DIAS: { valor: DiaSemana; etiqueta: string; corta: string }[] = [
  { valor: 'Lunes', etiqueta: 'Lunes', corta: 'Lun' },
  { valor: 'Martes', etiqueta: 'Martes', corta: 'Mar' },
  { valor: 'Miercoles', etiqueta: 'Miércoles', corta: 'Mié' },
  { valor: 'Jueves', etiqueta: 'Jueves', corta: 'Jue' },
  { valor: 'Viernes', etiqueta: 'Viernes', corta: 'Vie' },
  { valor: 'Sabado', etiqueta: 'Sábado', corta: 'Sáb' },
  { valor: 'Domingo', etiqueta: 'Domingo', corta: 'Dom' },
];

export const MOMENTOS: { valor: MomentoDia; etiqueta: string; hora: string }[] = [
  { valor: 'Desayuno', etiqueta: 'Desayuno', hora: 'Mañana' },
  { valor: 'Comida', etiqueta: 'Comida', hora: 'Mediodía' },
  { valor: 'Cena', etiqueta: 'Cena', hora: 'Noche' },
];

export const CANTIDADES: { valor: Cantidad; etiqueta: string }[] = [
  { valor: 'Una', etiqueta: '1 pastilla' },
  { valor: 'Media', etiqueta: '½ pastilla' },
  { valor: 'DiezMiligramos', etiqueta: '10 mg' },
  { valor: 'VeinteMiligramos', etiqueta: '20 mg' },
];

export function etiquetaCantidad(cantidad: Cantidad): string {
  return CANTIDADES.find(c => c.valor === cantidad)?.etiqueta ?? cantidad;
}

export function diaDeHoy(): DiaSemana {
  // getDay(): 0 = domingo, 1 = lunes...
  const indice = (new Date().getDay() + 6) % 7;
  return DIAS[indice].valor;
}

/** Reparte una lista de medicamentos en Desayuno / Comida / Cena. */
export function agruparPorMomento(medicamentos: Medicamento[]): MedicamentosPorMomento {
  const grupos: MedicamentosPorMomento = { Desayuno: [], Comida: [], Cena: [] };
  for (const medicamento of medicamentos) {
    grupos[medicamento.momentoDia].push(medicamento);
  }
  return grupos;
}
