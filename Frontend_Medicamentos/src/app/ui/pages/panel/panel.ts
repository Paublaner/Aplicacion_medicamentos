import { Component, OnDestroy, OnInit, computed, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AsistenteServicio } from '../../../application/asistente.servicio';
import { AuthServicio } from '../../../application/auth.servicio';
import { MedicamentosServicio } from '../../../application/medicamentos.servicio';
import { mensajeDeError } from '../../../domain/models/error-dominio';
import {
  DIAS,
  DiaSemana,
  MOMENTOS,
  Medicamento,
  MedicamentosPorMomento,
  MomentoDia,
  NuevoMedicamento,
  agruparPorMomento,
  diaDeHoy,
  etiquetaCantidad,
} from '../../../domain/models/medicamento';
import { ChatLucia } from '../../components/chat-lucia/chat-lucia';
import { FormularioMedicamento } from '../../components/formulario-medicamento/formulario-medicamento';
import { Icono } from '../../components/icono/icono';
import { ToastService } from '../../toast.service';

const ICONO_MOMENTO: Record<MomentoDia, string> = { Desayuno: 'sol', Comida: 'plato', Cena: 'luna' };

@Component({
  selector: 'app-panel',
  imports: [Icono, FormularioMedicamento, ChatLucia],
  templateUrl: './panel.html',
  styleUrl: './panel.css',
})
export class Panel implements OnInit, OnDestroy {
  private readonly servicio = inject(MedicamentosServicio);
  private readonly toast = inject(ToastService);
  private readonly router = inject(Router);
  private readonly asistente = inject(AsistenteServicio);
  protected readonly auth = inject(AuthServicio);

  protected readonly dias = DIAS;
  protected readonly momentos = MOMENTOS;
  protected readonly hoy = diaDeHoy();
  protected readonly etiquetaCantidad = etiquetaCantidad;
  protected readonly iconoMomento = ICONO_MOMENTO;
  protected readonly fechaHoy = new Intl.DateTimeFormat('es-ES', {
    weekday: 'long',
    day: 'numeric',
    month: 'long',
  }).format(new Date());

  protected readonly diaSeleccionado = signal<DiaSemana>(this.hoy);
  protected readonly medicamentos = signal<MedicamentosPorMomento>(agruparPorMomento([]));
  protected readonly cargando = signal(true);
  protected readonly errorCarga = signal<string | null>(null);

  protected readonly modal = signal<{ dia: DiaSemana; momento: MomentoDia } | null>(null);
  protected readonly confirmando = signal<string | null>(null);
  protected readonly eliminando = signal<string | null>(null);

  protected readonly total = computed(() =>
    Object.values(this.medicamentos()).reduce((suma, lista) => suma + lista.length, 0),
  );
  protected readonly etiquetaDia = computed(
    () => DIAS.find(d => d.valor === this.diaSeleccionado())?.etiqueta ?? '',
  );

  private peticion?: Subscription;

  ngOnInit(): void {
    this.cargar();
  }

  ngOnDestroy(): void {
    this.peticion?.unsubscribe();
  }

  protected seleccionarDia(dia: DiaSemana): void {
    if (dia === this.diaSeleccionado()) {
      return;
    }
    this.diaSeleccionado.set(dia);
    this.confirmando.set(null);
    this.cargar();
  }

  protected cargar(): void {
    // Si se cambia de día rápido, se cancela la petición anterior para que no pise a la nueva
    this.peticion?.unsubscribe();
    this.cargando.set(true);
    this.errorCarga.set(null);

    this.peticion = this.servicio.obtenerDia(this.diaSeleccionado()).subscribe({
      next: datos => {
        this.medicamentos.set(datos);
        this.cargando.set(false);
      },
      error: e => {
        this.errorCarga.set(mensajeDeError(e, 'No se han podido cargar los medicamentos.'));
        this.cargando.set(false);
      },
    });
  }

  protected abrirFormulario(momento: MomentoDia = 'Desayuno'): void {
    this.modal.set({ dia: this.diaSeleccionado(), momento });
  }

  protected alGuardar(medicamento: NuevoMedicamento): void {
    this.modal.set(null);
    this.toast.exito(`${medicamento.nombre} añadido correctamente.`);

    // Si se añadió a otro día, se salta a ese día para que el usuario lo vea
    if (medicamento.diaSemana !== this.diaSeleccionado()) {
      this.diaSeleccionado.set(medicamento.diaSemana);
    }
    this.cargar();
  }

  protected clave(medicamento: Medicamento): string {
    return `${medicamento.id}-${medicamento.momentoDia}`;
  }

  protected eliminar(medicamento: Medicamento): void {
    const clave = this.clave(medicamento);
    this.eliminando.set(clave);

    this.servicio.eliminar(medicamento).subscribe({
      next: () => {
        this.medicamentos.update(actual => ({
          ...actual,
          [medicamento.momentoDia]: actual[medicamento.momentoDia].filter(m => m.id !== medicamento.id),
        }));
        this.eliminando.set(null);
        this.confirmando.set(null);
        this.toast.exito(`${medicamento.nombre} eliminado.`);
      },
      error: e => {
        this.eliminando.set(null);
        this.confirmando.set(null);
        this.toast.error(mensajeDeError(e, 'No se ha podido eliminar el medicamento.'));
      },
    });
  }

  protected cerrarSesion(): void {
    this.auth.cerrarSesion();
    // Para que la siguiente persona que entre no vea la conversación anterior
    this.asistente.vaciar();
    this.router.navigate(['/login']);
  }
}
