import requests


usuario = {"correo": "user@example.com", "contrasena":"string"}

url = "http://localhost:5241/usuarios"

respuesta = requests.get(url, params=usuario)

print(respuesta.status_code)

resultado = respuesta.json()

token= resultado["token"]

urlMedicamentos=f"http://localhost:5241/medicamentos/dia/{"Lunes"}"

mis_headers = {
    "Authorization": f"Bearer {token }",
    "Accept": "application/json" 
}


parametros= {"momentoDia": "Cena"}
medicamentos = requests.get(urlMedicamentos,headers=mis_headers ,params=parametros)

print(medicamentos.status_code)
medicamentosValores = medicamentos.json()


for medicamento in medicamentosValores["medicamentos"]:
    print(medicamento["nombre"])