

# 📱 Actividad de Refactorización: Tienda de Celulares con Malas Prácticas

> 🧠 *Actividad práctica para que los estudiantes reconozcan malas prácticas en código fuente y apliquen patrones de diseño del catálogo GoF para mejorar su estructura.*

---
<img width="1390" height="790" alt="image" src="https://github.com/user-attachments/assets/bfd49549-ffe5-4166-98f3-69cb94588229" />

---

## 📋 Descripción de la Actividad

Esta actividad presenta un proyecto mal estructurado que simula una **Tienda de Celulares (CellShop)**. El código está cargado de errores comunes de diseño y mantenimiento (también conocidos como *Bad Code* o *code smells*).

Tu misión como estudiante o arquitecto de software es **identificar y refactorizar** cada una de las malas prácticas detectadas, **aplicando patrones de diseño GoF adecuados** para cada caso.

Puedes usar el lenguaje de programación con el que te sientas más cómodo. El objetivo es aplicar **buenas prácticas de diseño orientado a objetos**, y no el dominio de un lenguaje específico.

---

## 🔎 Diagnóstico del Código – Problemas Detectados

Aquí se listan 10 malas prácticas introducidas **intencionalmente** en el código fuente. Tu tarea es aplicar el patrón de diseño correcto en cada caso, justificando tu elección y mostrando una versión mejorada.

| Nº | Problema Detectado                                        | Recomendación                                   | Patrón GoF o Principio                       |
| -- | --------------------------------------------------------- | ----------------------------------------------- | -------------------------------------------- |
| 1  | Atributos públicos en clase `Mobile`                      | Encapsular con propiedades `get`/`set`          | ⚠️ No es GoF, es buena práctica de POO       |
| 2  | Método `ProcessSale` es largo y confuso                   | Dividir en métodos con responsabilidades únicas | 🧱 **SRP (Single Responsibility Principle)** |
| 3  | `InventoryAndBilling` mezcla inventario y facturación     | Separar en dos clases                           | 🧱 **SRP**                                   |
| 4  | `Promotion` contiene lógica rígida por marca              | Usar estrategias configurables para descuento   | 🧠 **Strategy Pattern**                      |
| 5  | `Mobile` se instancia directamente                        | Encapsular creación de objetos                  | 🏭 **Factory Method**                        |
| 6  | Múltiples instancias de `StoreManager`                    | Garantizar única instancia                      | 🧍 **Singleton Pattern**                     |
| 7  | Cambios de inventario no notifican a otros módulos        | Implementar sistema de notificaciones           | 📢 **Observer Pattern**                      |
| 8  | Añadir características fijas a celulares                  | Añadir funcionalidad sin modificar clases       | 🧩 **Decorator Pattern**                     |
| 9  | Clases fuertemente acopladas a implementaciones concretas | Introducir fachada para simplificar uso         | 🏛️ **Facade Pattern**                       |
| 10 | Lógica de promociones embebida en la venta                | Manejar múltiples promociones secuenciales      | 🔗 **Chain of Responsibility Pattern**       |

---

## 🧪 Actividades por Resolver

1. Revisa el código fuente proporcionado.
2. Identifica y documenta los problemas en el diseño.
3. Refactoriza aplicando los patrones de diseño correctos (uno por línea de la tabla).
4. Justifica tu elección para cada patrón aplicado.
5. Usa el lenguaje de programación que prefieras.
6. Sube tu solución en un repositorio (GitHub, GitLab, etc.) o presenta evidencia clara del refactor.

---

## ✨ Recomendaciones

* Organiza tu solución por carpetas o módulos: uno por patrón aplicado.
* No elimines completamente el código original: muéstralo comentado o archivado para comparación.
* Añade comentarios en español en el código explicando tus decisiones.
* Documenta con `README.md` cada patrón aplicado.

---

## 🧰 Recursos Recomendados

* 📘 [Catálogo oficial de patrones GoF](https://refactoring.guru/design-patterns)
* 🎓 [Clean Code – Robert C. Martin](https://amzn.to/3qVMztd)
* 📙 [Refactoring – Martin Fowler](https://refactoring.com/)
* 🧠 [Principios SOLID explicados](https://solidprinciples.com/)

---

## 🎯 Criterios de Evaluación

Cada patrón aplicado correctamente vale **10% de la calificación**. Se valorará:

✅ Identificación correcta del problema
✅ Aplicación adecuada del patrón GoF
✅ Código claro, mantenible y comentado
✅ Documentación básica de cada solución
✅ Buen uso del lenguaje elegido

---

## 📌 Entrega

* Repositorio con tu solución estructurada.
* Documentación (`README.md`) indicando:

  * Qué patrón usaste
  * Por qué lo elegiste
  * Qué problema resolviste
* Capturas o evidencias de ejecución (CLI/Web/API según el caso).
* Entrega URL GIST via Idoceo

---

## 📚 Licencia

Uso educativo exclusivamente. Adaptado por el docente para prácticas de análisis y diseño orientado a objetos.
