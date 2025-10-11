# 🧑‍💻 Peer Review - Patrones de Comportamiento (GoF)

## 📘 Descripción General

Esta actividad tiene como objetivo **revisar un Pull Request (PR)** y brindar **retroalimentación técnica** sobre el uso e implementación de **patrones de diseño de comportamiento (GoF)** en el código de un compañero.

---

## 🎯 Objetivo

- Evaluar si el código refactorizado aplica correctamente un **patrón de comportamiento**.  
- Detectar posibles *code smells* y sugerir mejoras.  
- Desarrollar habilidades de **análisis crítico** y **comunicación técnica** en revisiones de código.

---

## 🧭 Pasos para Realizar la Revisión

### 🟢 1. Clonar o Navegar al Repositorio

- Accede al repositorio compartido por el docente.  
- Entra a la pestaña **“Pull Requests”**.  
- Elige un PR que **no sea el tuyo**.  
- Lee atentamente el **título** y la **descripción del PR** para entender su propósito.

---

### 🟡 2. Leer y Comprender el Código

- Revisa el **diff del PR** (las líneas agregadas o modificadas).  
- Analiza **qué patrón de comportamiento fue implementado**.  
- Observa si el cambio **resuelve un code smell existente** o introduce uno nuevo.  
- Considera la **intención del diseño** y la **claridad del código resultante**.

---

### 🟠 3. Usa esta Plantilla para tu Revisión

Crea o edita el archivo `/reviews/review.md`  
y completa **todos los apartados** con tus observaciones técnicas y sugerencias.  
También puedes dejar comentarios directamente dentro del PR.

---

### 🔵 4. Ofrece Sugerencias Constructivas

✅ Sé **específico y útil**:

> “Podrías extraer la lógica del método `update()` a una clase Command para separar responsabilidades.”

❌ Evita comentarios vagos o personales:

> “Esto está mal.” o “No entiendo nada.”

🎯 El objetivo es **mejorar la calidad del código y la comprensión del patrón aplicado.**

---

### 🟣 5. Marca tu Revisión como Completa

- En GitHub/GitLab: marca el PR como **“Reviewed”**.  
- Si trabajas con archivo `.md`: súbelo al repositorio o entrégalo según las instrucciones del docente.

---

### 🟤 6. Respeta Tiempos y Formato

- ⏰ Entrega tu revisión **antes de que termine la clase.**  
- 👥 Si trabajas en pareja, **cada integrante revisa un PR distinto.**

---

## 🧠 Buenas Prácticas del Revisor

- 📚 Consulta la **rúbrica de evaluación** y la lista de *code smells de comportamiento*.  
- 🙋 Si no entiendes un patrón, **pregunta antes de juzgarlo**.  
- 🤝 Sé **claro, respetuoso y profesional** en tu retroalimentación.  

---

## 🎓 Resultado Esperado

Completar esta revisión te ayudará a:

- Obtener **puntos por participación activa**.  
- Ganar **experiencia práctica** en revisión de código profesional.  
- Mejorar tu **pensamiento de diseño** y tus habilidades de análisis técnico.

---

# 🧩 Plantilla de Revisión Técnica

Usa esta plantilla dentro del archivo `/reviews/review.md`.

---

## 🧑‍💻 Revisión de Código - Patrones de Comportamiento (GoF)

### 👤 Revisor:
_Nombre completo del estudiante que realiza la revisión._

### 📌 PR Revisado:
_Link al Pull Request o nombre de la rama (por ejemplo: `feature/observer-pattern`)._

---

## ✅ Checklist Técnica

| Ítem | ¿Cumple? | Comentarios |
|------|-----------|-------------|
| **1. Identifica al menos un code smell de comportamiento** | [ ] Sí / [ ] No | _¿Cuál fue y cómo lo aborda?_ |
| **2. Aplica un patrón de comportamiento adecuado** | [ ] Sí / [ ] No | _¿Qué patrón usó? ¿Es el más apropiado?_ |
| **3. La solución mejora la estructura del diseño** | [ ] Sí / [ ] No | _¿Reduce acoplamiento? ¿Aumenta cohesión?_ |
| **4. El código es legible y está bien estructurado** | [ ] Sí / [ ] No | _¿El flujo es claro y entendible?_ |
| **5. El PR está bien documentado y argumentado** | [ ] Sí / [ ] No | _¿Explica claramente las decisiones de diseño?_ |

---

## 🧠 Observaciones Técnicas

_Describe lo que se implementó correctamente en relación con el patrón de comportamiento._  
_Por ejemplo:_  
> “El patrón Strategy se aplicó correctamente para permitir intercambiar algoritmos sin modificar el cliente.”

---

## 🛠️ Sugerencias de Mejora

_Añade al menos una recomendación concreta sobre:_  
- Nombres de clases o métodos  
- Responsabilidad única (SRP)  
- Manejo de dependencias  
- Claridad del flujo de comportamiento  

_Por ejemplo:_  
> “Considera utilizar el patrón Command en lugar de Template Method para lograr un mejor desacoplamiento entre acciones.”

---

## 🎯 Conclusión del Revisor

_Resumen final de tu evaluación:_

> “Buena implementación del patrón Observer. Se logra una clara separación entre sujetos y observadores, aunque podría mejorarse la nomenclatura para mayor claridad.”

---

🔚 **Gracias por compartir tu código.**  
Tu revisión contribuye al aprendizaje colaborativo y al fortalecimiento de las prácticas de diseño profesional.

---
