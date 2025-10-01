# Asistencia de Inteligencia Artificial

## 🧑‍🎓 Datos del estudiante

- **Nombre:** Jesus Enrique Barboza Noriega
- **Número de control:** 21211913 

## Prompts utilizados (paso a paso)

1. "Dame un código **malo** en C# con temática de teléfonos que tenga el problema: **No seguir la interfaz esperada por el cliente**."
2. "¿Con qué **patrón GoF estructural** puedo resolver esta **incompatibilidad de interfaces** y por qué?"
3. "Dame los **pasos concretos** para aplicar **Adapter** en este caso (Target, Adaptee, Adapter, cómo inyectarlo en el cliente)."
4. "Refactoriza el código usando **Adapter**: crea `IPhone`, un `OldPhoneAdapter` que implemente `IPhone` y traduzca a `OldPhone`."
5. "Agrega **comentarios en el código** para hacer obvio dónde se aplica el patrón y qué problema resuelve."
6. "Escribe una **justificación técnica** tipo Pull Request (problema, solución, beneficios, impacto, riesgos/mitigación)."
7. "Propón **pruebas mínimas** (unidad/integración) para validar llamadas y envío de mensajes con números E.164."
8. "Incluye un **diagrama UML en Mermaid** mostrando Target (`IPhone`), Adaptee (`OldPhone`) y Adapter (`OldPhoneAdapter`)."
9. "Enumera **code smells** detectados (mínimo 3) relacionados con patrones GoF y mapea por qué Adapter es el adecuado."
10. "Revisa la **calidad del código** según criterios (legibilidad, nombres, SRP/DIP, manejo de errores básico)."

## Agentes usados
- ChatGPT (GPT-5 Thinking)
- GitHub Copilot (Visual Studio Code)

## Cambios y evaluación
- Se reemplazó la **dependencia concreta** `OldPhone` en el cliente por una **abstracción** `IPhone` (DIP).
- Se encapsuló la **traducción de interfaz** (E.164 → `area/local`) dentro de `OldPhoneAdapter`, evitando duplicación y acoplamiento.
- Se añadieron **comentarios** explicando el rol del Adapter y por qué resuelve la incompatibilidad de firmas.
- Se propuso una **demo mínima** y pruebas para verificar `Call` y `SendMessage` con el mismo contrato `IPhone`.
- Se documentó un **PR** con problema, solución, beneficios, impacto y riesgos (limpieza y parsing defensivo).

## Reflexión personal
- Comprendí mejor la diferencia entre **Adapter** (reconciliar **interfaces incompatibles**) y **Bridge/Facade** (abstracción/encapsulamiento sin cambiar firmas).
- Validé el valor de **programar contra interfaces** y aplicar **DIP/SRP** para mejorar testabilidad y mantenibilidad.
- Confirmé que centralizar conversión/formateo en el Adapter reduce errores y facilita reemplazar la librería legacy.

**Fecha**: 2025-09-29  
