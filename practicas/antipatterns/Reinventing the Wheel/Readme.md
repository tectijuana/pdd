# 🧩 Antipatrón: “Reinventing the Wheel”
## Alumna: Díaz Zavala Ximena Michelle, #21211934

### 🧠 Definición  
El antipatrón **“Reinventing the Wheel”** ocurre cuando un desarrollador decide **crear desde cero una funcionalidad que ya existe**, está probada y disponible a través de **bibliotecas, frameworks o APIs estándar**.

---

### ⚠️ Por qué se considera una mala práctica  

- 🚫 **Duplica esfuerzo y tiempo** de desarrollo.  
- 🐞 **Produce código menos optimizado o menos probado.**  
- 🔒 **Incrementa el riesgo de errores y vulnerabilidades.**  
- 🤝 **Dificulta la colaboración**, ya que otros desarrolladores no entienden por qué no se usó la solución estándar.

---

### 💡 Ejemplo conceptual  

> Ejemplo en Python:

❌ Antipatrón — “Reinventando la rueda” al crear un parser JSON manual:

```python
# ❌ Ejemplo de "Reinventing the Wheel"
# El desarrollador implementa su propio parser de JSON desde cero

def parse_json_manual(data):
    result = {}
    data = data.strip('{}').split(',')
    for item in data:
        key, value = item.split(':')
        result[key.strip().replace('"', '')] = value.strip().replace('"', '')
    return result

print(parse_json_manual('{"nombre": "Juan", "edad": "25"}'))

```
## 👉 Problemas del Antipatrón “Reinventing the Wheel”

Al implementar una solución propia para algo que ya existe, como un parser de JSON, se presentan diversos problemas técnicos y de mantenimiento:

---

### ❌ Problemas principales

1. ⚠️ **No maneja errores de sintaxis.**  
   Si el formato del texto JSON no es válido, el código puede fallar o producir resultados incorrectos, ya que no hay manejo de excepciones robusto.

2. 🚫 **No soporta estructuras complejas.**  
   No puede interpretar **listas, booleanos, valores nulos ni objetos anidados**, limitando su utilidad en casos reales.

3. 🔁 **Es completamente redundante.**  
   Python ya ofrece la librería estándar **`json`**, optimizada, segura y ampliamente probada.  
   Reescribirla solo añade **riesgo, mantenimiento innecesario y pérdida de tiempo**.

---

💬 En resumen, este antipatrón **va en contra de la eficiencia y la reutilización de código**, dos principios clave del desarrollo de software profesional.

## ✅ Solución correcta (usando librería estándar):

En lugar de reinventar la funcionalidad, se debe **utilizar las herramientas nativas** del lenguaje.  
Python proporciona el módulo `json`, que permite analizar (parsear) y generar datos JSON de forma **segura, eficiente y compatible**.

---

### 💻 Código de ejemplo correcto

```python
import json

data = json.loads('{"nombre": "Michet", "edad": 22}')
print(data)
```

## 👉 Beneficios de evitar el Antipatrón “Reinventing the Wheel”

Adoptar soluciones existentes y probadas trae múltiples ventajas en el desarrollo de software:

---

### ✅ Beneficios principales

- ✨ **Código más corto, robusto y mantenible.**  
  Se reduce la complejidad y se facilita la lectura y actualización del código.

- 📏 **Cumple con los estándares del lenguaje y la industria.**  
  Las librerías oficiales están alineadas con las mejores prácticas y garantizan compatibilidad.

- ⏱️ **Evita errores y pérdida de tiempo.**  
  Permite enfocarse en la lógica de negocio en lugar de recrear componentes ya existentes.

---

## Consecuencias 

Cuando se ignoran las herramientas estándar y se reimplementan funcionalidades, surgen efectos negativos tanto técnicos como organizacionales.

---

### ⚠️ Efectos negativos

| Aspecto         | Consecuencia                                                                 |
|-----------------|------------------------------------------------------------------------------|
| 🔧 **Mantenimiento** | Dificulta futuras actualizaciones y depuración del código.                 |
| 🚀 **Rendimiento**   | El código propio puede ser más lento que las librerías optimizadas.        |
| 🌐 **Escalabilidad** | Las soluciones caseras no están diseñadas para crecer ni integrarse fácilmente. |
| 🧠 **Conocimiento**  | Nuevos desarrolladores pierden tiempo entendiendo código innecesario.      |

---

### 💡 Ejemplo real

> En muchos proyectos web, algunos desarrolladores **crean su propio framework MVC** en lugar de usar **Django, Flask o Express**.  
> Esto **retrasa el desarrollo**, genera **inconsistencias con los estándares de la industria** y **aumenta los costos de mantenimiento**.

---
## Solución Correctiva 
```python
# ❌ Antipatrón: Reinventar la Rueda (parser JSON manual)
def parse_json_manual(text):
    """
    Intenta convertir un texto JSON en un diccionario,
    pero de manera artesanal (incorrecta).
    """
    text = text.strip("{}")
    items = text.split(",")
    result = {}
    for item in items:
        try:
            key, value = item.split(":")
            result[key.strip('" ')] = value.strip('" ')
        except ValueError:
            print(f"Error al procesar el elemento: {item}")
    return result


# Ejemplo de uso del parser manual
print("=== Antipatrón: Reinventing the Wheel ===")
text_json = '{"nombre": "Michet", "edad": "22"}'
data_manual = parse_json_manual(text_json)
print("Resultado del parser manual:", data_manual)
print("\n")


# ✅ Solución correcta: usar librería estándar 'json'
import json

print("=== Solución Correcta: Uso de Librería Estandar ===")

try:
    data_correcta = json.loads('{"nombre": "Michet", "edad": 22, "habilidades": ["Python", "SQL"]}')
    print("Resultado con json.loads():", data_correcta)

    # Convertir nuevamente a formato JSON (opcional)
    json_text = json.dumps(data_correcta, indent=4, ensure_ascii=False)
    print("\nJSON formateado correctamente:")
    print(json_text)

except json.JSONDecodeError as e:
    print("Error al decodificar JSON:", e)
```

Para evitar caer en el antipatrón **“Reinventing the Wheel”**, es fundamental adoptar **buenas prácticas de ingeniería de software** que promuevan la reutilización, la eficiencia y la estandarización.

---

### 🛠️ Buenas Prácticas

1. 🔍 **Investigar primero**  
   Antes de comenzar a programar, **verificar si ya existe una solución confiable**.  
   Consultando la documentación oficial, comunidades de desarrolladores y repositorios open-source.

2. 📚 **Usar librerías probadas**  
   Aprovechar **frameworks, APIs estándar y software open-source** que ya han sido **testeados y optimizados** por la comunidad.

3. ♻️ **Aplicar el principio DRY (Don’t Repeat Yourself)**  
   Evitar **duplicar código o funcionalidad**.  
   Si una función ya existe o puede reutilizarse, **no reescribirla**.

4. 🧩 **Adoptar patrones de diseño reconocidos (GoF)**  
   En lugar de reinventar mecanismos de control, manejo de eventos o instanciación, utilizar patrones como:  
   - 🕹️ **Observer**: para notificación de eventos.  
   - 🧠 **Strategy**: para intercambiar comportamientos dinámicamente.  
   - 🏭 **Factory**: para crear objetos de forma flexible y extensible.

5. 📝 **Documentar y justificar decisiones técnicas**  
   Si se decide implementar una nueva solución, **explica claramente el motivo** (por ejemplo, mejorar rendimiento, reducir dependencias o resolver una limitación específica).

---

## 🧠 Reflexión Personal

El antipatrón **“Reinventing the Wheel”** nos enseña que **no siempre es necesario crear desde cero lo que ya existe**.  

- Reutilizar **librerías y soluciones probadas**:  
  - Ahorra tiempo  
  - Reduce errores  
  - Mejora el mantenimiento del código  

> Un buen desarrollador **no busca reinventar la rueda**, sino **hacer que gire mejor**.

---

💡 **Lección clave:**  
La eficiencia y calidad en el desarrollo de software no se mide por la cantidad de código escrito, sino por la **inteligencia al reutilizar y aplicar soluciones ya existentes**.



