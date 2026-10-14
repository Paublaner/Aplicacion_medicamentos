import { Component, OnInit, inject, input, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MedicamentosServicio } from '../../../application/medicamentos.servicio';
import { mensajeDeError } from '../../../domain/models/error-dominio';
import {
  CANTIDADES,
  Cantidad,
  DIAS,
  DiaSemana,
  MOMENTOS,
  MomentoDia,
  NuevoMedicamento,
} from '../../../domain/models/medicamento';
import { Icono } from '../icono/icono';

@Component({
  selector: 'app-formulario-medicamento',
  imports: [ReactiveFormsModule, Icono],
  templateUrl: './formulario-medicamento.html',
  styleUrl: './formulario-medicamento.css',
  host: { '(document:keydown.escape)': 'cerrar.emit()' },
})
export class FormularioMedicamento implements OnInit {
  private readonly servicio = inject(MedicamentosServicio);

  readonly diaInicial = input.required<DiaSemana>();
  readonly momentoInicial = input<MomentoDia>('Desayuno');
  readonly guardado = output<NuevoMedicamento>();
  readonly cerrar = output<void>();

  protected readonly dias = DIAS;
  protected readonly momentos = MOMENTOS;
  protected readonly cantidades = CANTIDADES;

  protected readonly guardando = signal(false);
  protected readonly error = signal<string | null>(null);

  protected readonly formulario = inject(FormBuilder).nonNullable.group({
    nombre: ['', [Validators.required, Validators.maxLength(100)]],
    descripcion: ['', [Validators.required, Validators.maxLength(300)]],
    cantidad: ['Una' as Cantidad, Validators.required],
    diaSemana: ['Lunes' as DiaSemana, Validators.required],
    momentoDia: ['Desayuno' as MomentoDia, Validators.required],
  });

  ngOnInit(): void {
    // Se abre con el día y momento desde los que se pulsó "Añadir"
    this.formulario.patchValue({ diaSemana: this.diaInicial(), momentoDia: this.momentoInicial() });
  }

  protected invalido(campo: 'nombre' | 'descripcion'): boolean {
    const control = this.formulario.controls[campo];
    return control.invalid && control.touched;
  }

  protected guardar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    const medicamento = this.formulario.getRawValue();

    this.guardando.set(true);
    this.error.set(null);

    this.servicio.anadir(medicamento).subscribe({
      next: () => this.guardado.emit(medicamento),
      error: e => {
        this.error.set(mensajeDeError(e, 'No se ha podido guardar el medicamento.'));
        this.guardando.set(false);
      },
    });
  }
}
