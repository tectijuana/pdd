# 🤖 Asistencia de Inteligencia Artificial

## 🪪 BARBOZA NORIEGA JESUS ENRIQUE - 21211913

- **Prompts utilizados**:
  - "Genera en C# (consola) un programa de **vehículos** con **constructores de más de 4 parámetros** y otras malas prácticas para usarlo como BadCode."
  - "Identifica y explica **al menos 3 problemas creacionales** (constructor telescópico, primitivismo, baja cohesión, etc.) en ese BadCode."
  - "Propón una **estrategia de refactor** usando patrones creacionales adecuados (Builder, Factory Method, Prototype) y **justifica** cada elección."
  - "Refactoriza el código aplicando **Builder** para eliminar el constructor telescópico y validar invariantes."
  - "Introduce **Factory Method** para desacoplar la **creación por tipo** de vehículo (eléctrico/gasolina) y evitar `new` dispersos."
  - "Agrega un **Prototype ligero** o presets para crear **variantes** sin repetir toda la construcción (ej. cambiar color)."
  - "Redacta un **README** que incluya: problemas detectados, patrones aplicados, justificación e impacto."
  - "Entrega bloques **Markdown** separados con el **código malo** y el **código refactorizado** listos para pegar en el repositorio."

- **Agentes usados**:
  - ChatGPT (GPT-5 Thinking)
  - GitHub Copilot (Visual Studio Code)

- **Cambios y evaluación**:
  - Se reemplazó el constructor con 13 parámetros por **Builders** con métodos expresivos y validaciones, mejorando **legibilidad** y **robustez**.
  - Se centralizó la creación con **Factory Method**, reduciendo **acoplamiento** y facilitando la extensión a nuevos tipos de vehículo.
  - Se añadió **Prototype** (variantes por color) y **objetos de valor** (`SafetyPackage`, `ConveniencePackage`) para eliminar primitivismo.
  - Se verificó que los invariantes (capacidad de batería/tanque, ruedas mínimas, año válido) se **enforzan** en la construcción.
  - El README documenta **problemas → patrones → justificación → impacto**, alineado con los objetivos de la práctica.

- **Reflexión personal**:
  La refactorización paso a paso me permitió ver cómo **Builder** elimina el ruido de parámetros y cómo **Factory Method** aísla decisiones de creación. Usar **Prototype** para variaciones pequeñas resultó práctico para evitar duplicación. El resultado es más **cohesivo**, **testeable** y **extensible**.

- **Fecha**: 2025-09-24  
- **Versión del trabajo**: `practica_vehiculos_creacionales_v1`
