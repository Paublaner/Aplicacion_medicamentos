import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';

/** Solo deja pasar a usuarios con sesión; si no, los manda al login. */
export const soloAutenticados: CanActivateFn = () => {
  const auth = inject(AuthService);
  return auth.autenticado() ? true : inject(Router).createUrlTree(['/login']);
};

/** Si ya hay sesión, no tiene sentido ver el login o el registro: se va al panel. */
export const soloInvitados: CanActivateFn = () => {
  const auth = inject(AuthService);
  return auth.autenticado() ? inject(Router).createUrlTree(['/panel']) : true;
};
