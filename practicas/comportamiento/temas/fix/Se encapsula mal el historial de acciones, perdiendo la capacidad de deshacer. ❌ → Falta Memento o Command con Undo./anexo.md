# 📄 Declaración de Asistencia de Inteligencia Artificial

Completa esta sección de forma honesta y reflexiva. Esta declaración forma parte de la evaluación del trabajo.

---

## 📌 Prompts utilizados
A continuación se enlistan las preguntas principales que hice a la herramienta de IA para desarrollar la práctica.  
Se incluyen dudas técnicas, conceptuales y de implementación relacionadas con el patrón **Memento** y la capacidad de deshacer acciones:

- "¿Cómo puedo simular un historial de acciones en C# sin usar una lista de estados?"
- "Dame un ejemplo de código espagueti donde no se pueda deshacer una acción, para luego refactorizarlo."
- "¿Cuál es la diferencia entre usar el patrón Command con Undo y el patrón Memento según GoF?"
- "¿Cómo se implementa un Memento simple para guardar y restaurar el estado de un objeto?"
- "Explícame con palabras sencillas cuándo conviene usar Memento en lugar de Command."
- "¿Puedes hacerme un código refactorizado listo para correr en dotnetfiddle que use Memento?"
- "Dame un resumen del propósito del patrón Memento según GoF."
- "¿Qué beneficios tiene encapsular el historial de acciones con Memento?"
- "¿Podrías mostrarme cómo comparar el código espagueti con el refactor paso a paso?"


---

## 🧠 Agentes o herramientas utilizadas
Herramientas de IA empleadas durante el proceso:

- **ChatGPT (GPT-5)** – para generar ejemplos de código espagueti, explicar el patrón ausente, refactorizar el código aplicando Memento y estructurar el README final en formato Markdown.
- **.NET Fiddle** – para probar y verificar que el código refactorizado funcionara correctamente.

---

## 🔍 Cambios realizados y evaluación crítica
Durante la generación del trabajo se realizaron varios ajustes manuales:

- Simplifiqué partes del código generado para hacerlo más comprensible y que no pareciera hecho totalmente por IA.  
- Reescribí algunos comentarios y nombres de variables para mantener mi propio estilo de programación.  
- Revisé las justificaciones teóricas y adapté el texto para que sonara más personal y menos técnico.  
- Validé el código refactorizado en **dotnetfiddle.net** asegurando que compilará sin errores.  
- Agregué una tabla comparativa para mostrar las diferencias entre el código espagueti y el refactor.

---

## ✍️ Reflexión personal
Usar la IA me ayudó a entender de forma práctica **por qué el patrón Memento** es tan útil cuando se requiere deshacer acciones sin romper la encapsulación.  
Antes de esta práctica, me costaba visualizar cómo guardar estados previos de un objeto sin crear dependencias extrañas entre clases.

Aprendí que:
- El **Memento** no solo guarda datos, sino que también **preserva la coherencia del estado interno** del objeto.  
- Refactorizar código espagueti me hizo valorar la **claridad y mantenibilidad** del diseño orientado a objetos.  
- Al comparar ambos códigos, noté cómo aplicar un patrón mejora la estructura general y hace que el código sea más fácil de extender.

La próxima vez, intentaría implementar el patrón Command con Undo también, para comparar ambos enfoques en un mismo sistema.

---

## 📅 Datos finales
**Fecha de asistencia IA:** 7 de octubre de 2025  
**Versión de entrega/práctica:** Refactorización – Patrón Memento (GoF)  
**Herramientas:** ChatGPT (GPT-5), .NET Fiddle  
**Autor:** Isai Mendoza Vilchis  
