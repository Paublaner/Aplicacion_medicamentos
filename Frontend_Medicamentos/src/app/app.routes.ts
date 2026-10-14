import { Routes } from '@angular/router';
import { soloAutenticados, soloInvitados } from './ui/guards';

export const routes: Routes = [
  {
    path: 'login',
    canActivate: [soloInvitados],
    title: 'Iniciar sesión · MediTrack',
    loadComponent: () => import('./ui/pages/login/login').then(m => m.Login),
  },
  {
    path: 'registro',
    canActivate: [soloInvitados],
    title: 'Crear cuenta · MediTrack',
    loadComponent: () => import('./ui/pages/registro/registro').then(m => m.Registro),
  },
  {
    path: 'panel',
    canActivate: [soloAutenticados],
    title: 'Mis medicamentos · MediTrack',
    loadComponent: () => import('./ui/pages/panel/panel').then(m => m.Panel),
  },
  { path: '', pathMatch: 'full', redirectTo: 'panel' },
  { path: '**', redirectTo: 'panel' },
];
