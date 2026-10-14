export interface Usuario {
  id: number;
  nombre: string;
  correo: string;
}

export interface Sesion {
  token: string;
  refreshToken: string;
  usuario: Usuario;
}

export interface Tokens {
  token: string;
  refreshToken: string;
}
