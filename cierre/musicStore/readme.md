<img width="2446" height="1444" alt="image" src="https://github.com/user-attachments/assets/c3dd4a28-0863-4f4f-be0d-1eaf120ca39f" />

---

# 🧠 Refactorización de Código Defectuoso con Patrones de Diseño GoF

## 🎯 Objetivo de la práctica

Aplicar patrones de diseño del catálogo GoF para **refactorizar un diseño rígido, acoplado y poco escalable**, mejorando su arquitectura mediante el uso de abstracciones, principios SOLID y creación de objetos mediante fábricas.

---

## 🎵 Escenario: Tienda de Música Digital

Una tienda de música en línea administra un catálogo de instrumentos musicales (como guitarras, pianos, baterías, etc.). El sistema actual permite reproducir sonidos de instrumentos, pero **cada nuevo tipo de instrumento requiere modificar múltiples partes del código existente**.

---

## 📋 Comportamiento Actual del Sistema

Actualmente, la tienda funciona así:

1. Tiene clases concretas como `Guitar`, `Piano`, etc., que contienen un método `Play()` para ejecutar el sonido del instrumento.
2. Existe una clase principal, llamada `MusicStore`, que decide **qué instrumento crear** basándose en una cadena de texto (`"Guitar"`, `"Piano"`, etc.).
3. Esta clase contiene condicionales (`if`, `else`, `switch`, etc.) para crear y usar los instrumentos.

---

### 💣 Problemas Identificados

| Problema                                | Descripción                                                                      |
| --------------------------------------- | -------------------------------------------------------------------------------- |
| ❌ Uso excesivo de condicionales         | Cada nuevo instrumento requiere modificar `MusicStore`.                          |
| ❌ Alto acoplamiento                     | `MusicStore` depende directamente de clases concretas (`Guitar`, `Piano`, etc.). |
| ❌ No se aplica el principio Open/Closed | No se puede extender el sistema sin modificar código existente.                  |
| ❌ Sin abstracción                       | No hay interfaz o clase base común entre los instrumentos.                       |
| ❌ Baja escalabilidad                    | Difícil de mantener cuando se agregan más instrumentos.                          |

---

## 🧪 Actividad del Estudiante: Refactorizar con GoF

Se te pide refactorizar el sistema aplicando principios de diseño y patrones GoF. Específicamente:

### 🎯 Objetivos de la Refactorización

| #  | Objetivo                       | Acción esperada                                                                   |
| -- | ------------------------------ | --------------------------------------------------------------------------------- |
| 1  | Eliminar condicionales         | Usar una fábrica para crear instrumentos.                                         |
| 2  | Introducir abstracción         | Crear una interfaz común (por ejemplo, `Instrument`) con un método como `Play()`. |
| 3  | Reducir acoplamiento           | `MusicStore` no debe conocer clases concretas.                                    |
| 4  | Aplicar Open/Closed            | Agregar nuevos instrumentos sin modificar `MusicStore`.                           |
| 5  | Usar patrón de creación        | Aplicar el patrón `Factory Method` o `Abstract Factory`.                          |
| 6  | Mejorar cohesión               | Cada clase debe tener una única responsabilidad.                                  |
| 7  | Facilitar pruebas unitarias    | Permitir pruebas con objetos simulados o falsos.                                  |
| 8  | Preparar para expansión        | Añadir nuevos instrumentos sin duplicar lógica.                                   |
| 9  | Usar inversión de dependencias | Permitir inyección de dependencias si el lenguaje lo permite.                     |
| 10 | Fomentar reutilización         | Que los instrumentos puedan ser usados en otros contextos.                        |

---

## 📎 Requerimientos

* La solución **debe implementarse en el lenguaje de tu elección.**
* Debes usar al menos un **patrón de creación** del catálogo GoF:

  * `Factory Method`
  * `Abstract Factory` (si aplicas una solución más general)
* Aplica principios de diseño como:

  * **Open/Closed**
  * **Single Responsibility**
  * **Inversión de dependencias**

---

## 🧠 Pistas para el Desarrollo

* ¿Qué patrón te ayuda a delegar la creación de objetos según un identificador?
* ¿Qué clase no debería cambiar cuando agregas un nuevo instrumento?
* ¿Qué ventajas trae tener una interfaz común para todos los instrumentos?

---

## 📦 Entregables Esperados

1. 🧪 Código fuente refactorizado, en el lenguaje que elijas.
2. 📄 Un archivo `README.md` con:

   * Descripción del problema original.
   * Qué problemas detectaste.
   * Qué patrón(es) aplicaste y por qué.
   * Cómo tu solución respeta principios SOLID.
3. ✅ (Opcional) Pruebas unitarias de las clases desacopladas.
4. 🧩 (Opcional) Diagrama UML de tu solución final.

---

## 📚 Recursos Recomendados

* 🔗 [Refactoring Guru – Factory Method](https://refactoring.guru/design-patterns/factory-method)
* 🔗 [Principios SOLID](https://solidprinciples.com/)
* 📘 *Design Patterns* (Gamma, Helm, Johnson, Vlissides – GoF)

---

## 🚦Criterios de Evaluación

| Criterio                                     | Puntaje |
| -------------------------------------------- | ------- |
| Refactorización correcta del diseño original | 25%     |
| Aplicación de patrones GoF adecuados         | 25%     |
| Principios SOLID aplicados correctamente     | 20%     |
| Código limpio, mantenible y extensible       | 20%     |
| Documentación clara y concisa                | 10%     |

---

¿
