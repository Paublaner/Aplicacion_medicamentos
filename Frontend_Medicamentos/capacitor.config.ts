import type { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.meditrack.app',
  appName: 'MediTrack',
  // Carpeta que genera `ng build` y que Capacitor mete dentro de la APK
  webDir: 'dist/Frontend_Medicamentos/browser',
  server: {
    // Permite llamar a una API por http:// (sin "s"), por ejemplo la del PC de casa.
    // Si la API se publica con https, se puede quitar.
    cleartext: true,
  },
  android: {
    // La app se sirve como https://localhost y la API puede ser http://: sin esto Android lo bloquea
    allowMixedContent: true,
  },
};

export default config;
