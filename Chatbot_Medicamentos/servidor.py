"""Servidor web de Lucía. El front de Angular le manda la conversación y él responde.

Arrancarlo (con el .venv activado):
    uvicorn servidor:app --host 0.0.0.0 --port 8000 --reload
"""

from typing import Literal

import ollama
from fastapi import FastAPI, Header, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel, Field

from lucia import TokenCaducado, responder

# Solo se mandan al modelo los últimos mensajes: más contexto = respuestas más lentas
MAX_MENSAJES_HISTORIAL = 20

app = FastAPI(title="Lucía")

# Orígenes desde los que el navegador deja llamar a este servidor:
# ng serve (por si se llama sin proxy) y la app de Android (Capacitor).
app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://localhost:4200", "https://localhost", "http://localhost", "capacitor://localhost"],
    allow_methods=["POST"],
    allow_headers=["Authorization", "Content-Type"],
)


class Mensaje(BaseModel):
    # Solo user y assistant: así nadie puede colar desde fuera un mensaje "system" con otras instrucciones
    role: Literal["user", "assistant"]
    content: str = Field(min_length=1, max_length=2000)


class PeticionChat(BaseModel):
    mensajes: list[Mensaje] = Field(min_length=1)


class RespuestaChat(BaseModel):
    respuesta: str


@app.post("/chat", response_model=RespuestaChat)
def chat(peticion: PeticionChat, authorization: str | None = Header(default=None)) -> RespuestaChat:
    # Sin token no se habla con Lucía: lo necesita para consultar la medicación en la API .NET
    if not authorization or not authorization.startswith("Bearer "):
        raise HTTPException(status_code=401, detail="Falta el token.")
    token = authorization.removeprefix("Bearer ")

    if peticion.mensajes[-1].role != "user":
        raise HTTPException(status_code=400, detail="El último mensaje tiene que ser del usuario.")

    historial = [m.model_dump() for m in peticion.mensajes[-MAX_MENSAJES_HISTORIAL:]]

    try:
        texto = responder(historial, token)
    except TokenCaducado:
        # El front renueva el token al ver un 401 y repite la petición
        raise HTTPException(status_code=401, detail="La sesión ha caducado.")
    except (ollama.ResponseError, ConnectionError):
        raise HTTPException(status_code=503, detail="Lucía no está disponible ahora mismo. ¿Está Ollama encendido?")

    return RespuestaChat(respuesta=texto.strip())
