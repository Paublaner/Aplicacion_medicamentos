# MediTrack · Gestor de medicación

App familiar para recordar qué medicamentos hay que tomar cada día de la semana y en cada momento (desayuno, comida y cena). Tiene tres partes:

| Carpeta | Qué es | Tecnología |
|---|---|---|
| `Backend_Medicamentos/` | API REST con usuarios, login con JWT y medicamentos | .NET 10, ASP.NET Core, EF Core, PostgreSQL |
| `Frontend_Medicamentos/` | Interfaz web y APK de Android | Angular 22, Capacitor 8 |
| `Chatbot_Medicamentos/` | **Lucía**, asistente de IA que consulta la medicación del usuario | Python, FastAPI, Ollama (modelo local) |

```
Angular / APK ──► API .NET (:5241) ──► PostgreSQL
      │                  ▲
      └──► Lucía (:8000) ┘   (Lucía llama a la API con el token del usuario)
```

## Arquitectura

El backend y el frontend siguen una **arquitectura hexagonal** (puertos y adaptadores):

- `Domain/` — modelos y puertos (interfaces). No depende de nada externo.
- `Application/` — servicios con la lógica de negocio (usuarios, medicamentos, JWT, refresh tokens).
- `Infrastructure/Driving/` — controladores REST y DTOs (lo que *entra*).
- `Infrastructure/Driven/` — base de datos con EF Core: entidades, mappers y adaptadores (lo que *sale*).

Los errores de negocio se propagan con `Result` ([FluentResults](https://github.com/altmann/FluentResults)) hasta los controladores, que los traducen a códigos HTTP.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Node.js](https://nodejs.org/) (para el frontend)
- [Python 3.12+](https://www.python.org/) y [Ollama](https://ollama.com/) (para el chatbot)
- Android Studio (solo para generar la APK)

## Puesta en marcha

### 1. Backend (API .NET)

Los secretos **no están en el repositorio**. Se guardan con *user-secrets* de .NET (fuera de la carpeta del proyecto):

```bash
cd Backend_Medicamentos
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=TU_CONTRASEÑA"
dotnet user-secrets set "Jwt:Key" "UNA_CLAVE_ALEATORIA_DE_AL_MENOS_32_CARACTERES"
```

Para generar una clave JWT aleatoria (PowerShell):

```powershell
[Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Maximum 256 }))
```

Después, crear la base de datos y arrancar:

```bash
dotnet tool install --global dotnet-ef   # solo la primera vez
dotnet ef database update
dotnet run --launch-profile http
```

La API queda en `http://localhost:5241` y la documentación Swagger en `http://localhost:5241/swagger` (solo en desarrollo).

En producción (o en Docker) los mismos valores se pasan como **variables de entorno**, con `__` en lugar de `:`:

```bash
ConnectionStrings__DefaultConnection="Host=...;Password=..."
Jwt__Key="..."
```

### 2. Frontend (Angular)

```bash
cd Frontend_Medicamentos
npm install
npm start
```

Se abre en `http://localhost:4200`. El proxy (`proxy.conf.json`) redirige `/api` a la API y `/chat-api` a Lucía.

**APK de Android:** poner en `src/environments/environment.prod.ts` la IP del PC donde corre la API y ejecutar `npm run android`.

### 3. Chatbot (Lucía)

```bash
cd Chatbot_Medicamentos
python -m venv .venv
.venv\Scripts\activate        # Windows  (Linux/Mac: source .venv/bin/activate)
pip install -r requirements.txt
ollama pull qwen3
uvicorn servidor:app --host 0.0.0.0 --port 8000
```

`main.py` es una versión de consola para probar el bot sin la interfaz.

## Endpoints de la API

| Método | Ruta | Auth | Descripción |
|---|---|---|---|
| `POST` | `/usuarios` | — | Registro de usuario |
| `GET` | `/usuarios?correo=&contrasena=` | — | Login: devuelve token JWT y refresh token |
| `POST` | `/refresh-token` | — | Renueva el token JWT |
| `POST` | `/medicamentos` | JWT | Añade un medicamento |
| `GET` | `/medicamentos/dia/{dia}?momentoDia=` | JWT | Medicamentos de un día y momento |
| `DELETE` | `/medicamentos?nombre=&diaSemana=&momentoDia=` | JWT | Elimina un medicamento |

- `dia`: `Lunes`, `Martes`, `Miercoles`, `Jueves`, `Viernes`, `Sabado`, `Domingo` (sin tildes)
- `momentoDia`: `Desayuno`, `Comida`, `Cena`

Chatbot: `POST /chat` en el puerto 8000, con la cabecera `Authorization: Bearer <token>`.

## Seguridad

- Contraseñas guardadas con hash (`PasswordHasher` de ASP.NET Core).
- Autenticación con JWT + refresh tokens revocables.
- Rate limiting: 100 peticiones por minuto.
- CORS abierto solo en desarrollo.
- Swagger solo en desarrollo.
- Ningún secreto en el código: ver `.gitignore` y la sección de *user-secrets*.

## Tests

```bash
cd Backend_Medicamentos
dotnet test
```

Tests unitarios con xUnit, Moq y FluentAssertions en `Backend_Medicamentos/Test/`.

## Pendiente

- Login por `POST` (ahora la contraseña viaja en la URL).
- Mover los tests a un proyecto aparte.
- Desplegar con Docker.
