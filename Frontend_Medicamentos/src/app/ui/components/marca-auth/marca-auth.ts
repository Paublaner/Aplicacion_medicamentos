import { Component } from '@angular/core';
import { Icono } from '../icono/icono';

/** Columna morada con la marca que comparten las pantallas de login y registro. */
@Component({
  selector: 'app-marca-auth',
  imports: [Icono],
  host: { class: 'auth-marca' },
  template: `
    <div class="logo">
      <span class="logo-icono"><app-icono nombre="pastilla" [tamano]="22" /></span>
      MediTrack
    </div>

    <div>
      <h1>Tu medicación,<br />siempre bajo control.</h1>
      <p class="lema">
        Organiza qué tomar cada día de la semana en el desayuno, la comida y la cena. Sin olvidos.
      </p>

      <div class="auth-lista">
        <div><span><app-icono nombre="calendario" [tamano]="18" /></span> Planifica toda tu semana</div>
        <div><span><app-icono nombre="reloj" [tamano]="18" /></span> Organizado por momentos del día</div>
        <div><span><app-icono nombre="escudo" [tamano]="18" /></span> Tus datos protegidos con tu cuenta</div>
      </div>
    </div>

    <p class="copy">© {{ anio }} MediTrack</p>
  `,
  styles: `.copy { font-size: 0.85rem; opacity: 0.7; } @media (max-width: 860px) { .copy { display: none; } }`,
})
export class MarcaAuth {
  protected readonly anio = new Date().getFullYear();
}
