# 📎 Anexo — Declaración de Asistencia de Inteligencia Artificial

Completa esta sección de forma honesta y reflexiva. Esta declaración es obligatoria y forma parte de la evaluación del trabajo.

---

## 📌 Prompts utilizados (orientativos por etapas)

### 1) Comprender el problema y generar el punto de partida
- “Explícame en 3–4 frases por qué es mala práctica manejar tres pantallas con `if` dentro de `ActualizarPantallas()`.”
- “Dame un **código espagueti mínimo** en C# con un método `ActualizarPantallas()` que tenga **tres if** actuando como observadores hardcodeados.”
- “Señala 3–5 *code smells* que introduce ese diseño (acoplamiento, rigidez, etc.).”

### 2) Identificar el patrón y justificarlo
- “¿Qué **patrón de diseño** resuelve mejor este caso y **por qué**? Compáralo brevemente con Pub/Sub, Mediator y Strategy.”
- “Resume en 5 bullets los **beneficios clave** del patrón Observer aplicados a este caso.”

### 3) Guiar el refactor (versión mínima, entendible)
- “Dame una **explicación simple** del patrón Observer para este caso: ‘sujeto = emisor de eventos; observadores = pantallas’. Sin jerga.”
- “Propón un **refactor ultra-simple** en C# usando **delegados** (`Func<T,bool>` y `Action<T>`) para suscribirse con filtros.”
- “Incluye un ejemplo de **suscripción, notificación y desuscripción** en 10–15 líneas para probarlo.”

### 4) Robustez, pruebas y DX
- “Dime 5 **pruebas manuales** para validar el refactor (eventos sin suscriptor, múltiples suscriptores, filtros que no coinciden, etc.).”
- “¿Qué **errores comunes** debería evitar (filtrados que lanzan excepciones, modificar la colección durante el `Notify`, etc.) y cómo mitigarlos?”
- “Reescribe el ejemplo en una **versión lista para .NET Fiddle** (Console/C#) con `Main()` y salidas visibles.”

### 5) Entregables y documentación
- “Genera un **README.md** corto con: (a) problema, (b) patrón ausente, (c) refactor, (d) salida esperada y (e) beneficios.”
- “Añade una sección de **limitaciones conocidas** y posibles mejoras (p. ej., prioridad de observadores, async, manejo de errores).”
- “Propón 3 **extensiones opcionales**: métricas, logging, o PrioritizedObserver.”

---

## 🧠 Agentes o herramientas utilizadas
> Indica con precisión cuáles herramientas de IA usarías y **para qué** (ejemplo orientativo):

- **ChatGPT (GPT-5 Thinking)** — ideación de prompts por etapas, contraste de patrones (Observer vs Mediator), y generación del refactor mínimo con delegados.
- **GitHub Copilot** — autocompletado de pequeñas rutinas (métodos `Subscribe/Unsubscribe/Notify`) y comentarios XML de métodos.
- **DocFX / Markdown Preview** — verificación rápida de formato del README.

---

## 🔍 Cambios realizados y evaluación crítica
> Explica qué partes del contenido generado **modificaste**, **adaptaste** o **descartaste**, y **cómo validaste** lo que usaste.

- Reemplacé una versión con interfaces pesadas por un **EventBus con delegados** para mantenerlo pedagógico.
- Simplifiqué el dominio a un **evento** con pocas propiedades y **filtros inline** para que el foco sea Observer.
- Añadí **snapshot** de suscriptores en `Notify` para evitar modificar la colección durante la iteración.
- Validé el comportamiento con **pruebas manuales** (sin suscriptores, múltiples coincidencias, desuscripción selectiva).
- Documenté **limitaciones** (sin prioridades ni async) y **posibles mejoras**.

---

## ✍️ Reflexión personal
> Qué aprendiste usando IA, cómo afectó tu comprensión del tema y qué harías diferente.

- La IA ayuda a **acotar el ejemplo** hasta un núcleo claro (delegados + filtros), evitando sobrediseño.
- Comparar Observer con patrones cercanos me aclaró **cuándo usar cada uno** y **qué trade-offs** asumo.
- La revisión guiada me hizo detectar **puntos frágiles** (mutación en notificación, manejo de excepciones en handlers).
- Para la próxima, empezaría con **tests de comportamiento** (salidas esperadas) antes de escribir el EventBus.

---

## 📅 Datos finales
- **Fecha de la asistencia IA**: 7/Oct/2025
- **Versión de entrega/práctica**: 1
- **Herramientas**: ChatGPT

