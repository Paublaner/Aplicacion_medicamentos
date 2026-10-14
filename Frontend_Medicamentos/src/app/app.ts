import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Toasts } from './ui/components/toasts/toasts';

@Component({
  imports: [RouterOutlet, Toasts],
  selector: 'app-root',
  template: `
    <router-outlet />
    <app-toasts />
  `,
})
export class App {}
