import { Component, computed, input } from '@angular/core';

// Trazos de iconos estilo "outline" (24x24). Se dibujan con stroke = currentColor,
// así que toman el color del texto que los rodea.
const ICONOS: Record<string, string> = {
  pastilla:
    'M10.5 20.5 3.5 13.5a4.95 4.95 0 1 1 7-7l7 7a4.95 4.95 0 1 1-7 7Z M8.5 8.5l7 7',
  sol: 'M12 4V2 M12 22v-2 M4.93 4.93 3.51 3.51 M20.49 20.49l-1.42-1.42 M4 12H2 M22 12h-2 M4.93 19.07l-1.42 1.42 M20.49 3.51l-1.42 1.42 M12 16a4 4 0 1 0 0-8 4 4 0 0 0 0 8Z',
  plato: 'M12 21a9 9 0 1 0 0-18 9 9 0 0 0 0 18Z M12 16a4 4 0 1 0 0-8 4 4 0 0 0 0 8Z',
  luna: 'M12 3a6 6 0 0 0 9 9 9 9 0 1 1-9-9Z',
  mas: 'M12 5v14 M5 12h14',
  papelera: 'M3 6h18 M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6 M8 6V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2 M10 11v6 M14 11v6',
  salir: 'M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4 M16 17l5-5-5-5 M21 12H9',
  cerrar: 'M18 6 6 18 M6 6l12 12',
  check: 'M20 6 9 17l-5-5',
  alerta: 'M12 22a10 10 0 1 0 0-20 10 10 0 0 0 0 20Z M12 8v4 M12 16h.01',
  calendario: 'M8 2v4 M16 2v4 M3 10h18 M5 4h14a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V6a2 2 0 0 1 2-2Z',
  escudo: 'M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10Z',
  reloj: 'M12 22a10 10 0 1 0 0-20 10 10 0 0 0 0 20Z M12 6v6l4 2',
  chat: 'M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2Z',
  enviar: 'M22 2 11 13 M22 2l-7 20-4-9-9-4Z',
};

@Component({
  selector: 'app-icono',
  template: `
    <svg
      [attr.width]="tamano()"
      [attr.height]="tamano()"
      viewBox="0 0 24 24"
      fill="none"
      stroke="currentColor"
      [attr.stroke-width]="grosor()"
      stroke-linecap="round"
      stroke-linejoin="round"
      aria-hidden="true"
    >
      <path [attr.d]="trazo()" />
    </svg>
  `,
  styles: `:host { display: inline-flex; flex-shrink: 0; }`,
})
export class Icono {
  readonly nombre = input.required<string>();
  readonly tamano = input(20);
  readonly grosor = input(2);

  protected readonly trazo = computed(() => ICONOS[this.nombre()] ?? '');
}
