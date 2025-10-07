# 🧑‍💻 Peer Review - Patrones Estructurales (GoF)

## 📘 Descripción General

Esta actividad tiene como objetivo **revisar el Pull Request (PR) de un compañero** y brindar retroalimentación técnica sobre el uso e implementación de **patrones estructurales (GoF)** en su código.

---

## 🎯 Objetivo

- Evaluar si el código refactorizado aplica correctamente un patrón estructural.
- Detectar posibles *code smells* y sugerir mejoras.
- Desarrollar habilidades de análisis y comunicación técnica.

---

## 🧭 PASOS PARA REALIZAR LA REVISIÓN

---

### 🟢 1. Clona o navega al repositorio del compañero

- Accede al repositorio compartido por el docente.
- Entra a la pestaña **"Pull Requests"**.
- Elige un PR que **no sea el tuyo**.
- Lee atentamente el **título y la descripción del PR**.

---

### 🟡 2. Lee y comprende el código

- Revisa el **diff** del PR (las líneas modificadas).
- Analiza qué patrón estructural fue implementado.
- Observa si el cambio **resuelve un code smell** o genera nuevos.
- Piensa en la intención de diseño y en la claridad del código.

---

### 🟠 3. Usa la plantilla de revisión

Puedes:

- **Comentar directamente en el PR** con `Add Review Comment`, o  
- Copiar la plantilla de abajo (`peer-review-template`) y completarla en un archivo dentro del repositorio, por ejemplo:  
  `/reviews/mi-review.md`.

Completa todos los apartados con observaciones técnicas y sugerencias.

---

### 🔵 4. Ofrece sugerencias constructivas

✅ Sé específico y útil:

> “Podrías extraer la lógica del constructor a un método privado para simplificar el decorador.”

❌ Evita vaguedades o juicios personales:

> “Esto está mal.” o “No entiendo nada.”

🎯 El objetivo es **ayudar a mejorar el código y la comprensión del patrón.**

---

### 🟣 5. Marca tu revisión como completa

- Si trabajas en GitHub/GitLab: marca el PR como **"Reviewed"**.
- Si usas archivo `.md`: súbelo con un commit o entrégalo según las instrucciones del docente.

---

### 🟤 6. Respeta tiempos y formato

- ⏰ Entrega tu revisión **antes de que termine la clase**.  
- 👥 Si trabajas en pareja, **cada uno debe revisar un PR diferente**.

---

## 🧠 Buenas Prácticas para Revisores

- 📚 Consulta la rúbrica y la lista de *code smells estructurales*.  
- 🙋 Pregunta si no entiendes un patrón antes de juzgarlo.  
- 🤝 Sé claro, respetuoso y profesional en tu retroalimentación.  

---

## 🎓 Resultado Esperado

Completar este proceso te brinda:

- Puntos por participación activa.
- Experiencia real en revisión de código.
- Mejora en tus habilidades de comunicación técnica y análisis de diseño.

---

# 🧩 Plantilla de Revisión Técnica

Usa esta plantilla dentro de tu archivo `/reviews/peer-review.md` o como comentario en el PR.

---

## 🧑‍💻 Revisión de Código - Patrones Estructurales (GoF)

### 👤 Revisor:
_Nombre completo del estudiante que revisa._

### 📌 PR Revisado:
_Link al Pull Request o nombre de la rama (`fix/_____`)._

---

## ✅ Checklist Técnica

| Ítem | ¿Cumple? | Comentarios |
|------|-----------|-------------|
| **1. Identifica al menos un code smell estructural real** | [ ] Sí / [ ] No | _¿Cuál fue? ¿Se justifica bien?_ |
| **2. Aplica un patrón estructural adecuado** | [ ] Sí / [ ] No | _¿Cuál patrón usó? ¿Es el más apropiado?_ |
| **3. La solución es coherente y mejora el diseño** | [ ] Sí / [ ] No | _¿Hay redundancias o errores aún?_ |
| **4. El código es legible y está bien estructurado** | [ ] Sí / [ ] No | _¿Se entiende fácilmente?_ |
| **5. El PR está bien documentado y argumentado** | [ ] Sí / [ ] No | _¿Explica claramente el cambio?_ |

---

## 🧠 Observaciones Técnicas

_Describe lo que se hizo bien, con base en principios de diseño estructural._  
_Por ejemplo: “El patrón Adapter fue implementado correctamente para desacoplar la clase Cliente del servicio externo.”_

---

## 🛠️ Sugerencias de Mejora

_Incluye al menos una recomendación específica sobre nombres, estructura, SRP, dependencias o claridad de diseño._

---

## 🎯 Entrega Final

_Un resumen general como revisor:_

> “Buen trabajo aplicando el patrón Composite. Se nota una mejora clara respecto a la versión anterior, aunque podrías considerar simplificar la estructura jerárquica de clases. Sigue así.”

---

🔚 **Gracias por compartir tu código. Todo feedback busca mejorar nuestra práctica como desarrolladores.**

---

## 📎 Cómo usar esta plantilla

- Sube el archivo completado en `/reviews/` con el nombre `peer-review-[tu-nombre].md`.  
- O pega tus observaciones directamente como comentario dentro del Pull Request revisado.
