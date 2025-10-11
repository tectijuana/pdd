# Revisión de Código - Patrones Estructurales (GoF)

**👤 Revisor:** Gutiérrez Martínez Ana Cristina 

**📌 PR Revisado**

**Rama:** practicas/comportamiento/temas/fix/Una misma clase ejecuta los comandos y también los define. ❌ → Falta de separación clara de responsabilidades. ¿Command?/readme.md

**Autor:** Eduardo Gallardo Dueñas (07/10/25)

**Checklist Técnica**

| Ítem	| ¿Cumple?	| Comentarios |
|-------|-----------|-------------|
|1. Identifica al menos un code smell estructural real	| ☑️ Sí	| Reconoce correctamente la falta de separación de responsabilidades (SRP) como un problema real del código original. |
|2. Aplica un patrón estructural adecuado	| ☑️ Sí	| Utiliza el patrón Command, que es el más apropiado para separar la definición y ejecución de acciones. |
|3. La solución es coherente y mejora el diseño	| ☑️ Sí	| La refactorización elimina la dependencia directa entre el invocador y las acciones concretas, haciendo el código más flexible y extensible. |
|4. El código es legible y está bien estructurado	| ☑️ Sí	 | Buen uso de nombres descriptivos, convenciones y comentarios. La estructura por clases es clara y coherente. |
|5. El PR está bien documentado y argumentado |	☑️  Sí	| Explica correctamente la motivación del cambio y su relación con los principios SOLID y el patrón Command. |

**🧠 Observaciones Técnicas**
- Se aplicó correctamente el patrón "Command" para resolver un problema de diseño: mezcla entre la definición y ejecución de comandos.
- El código refactorizado separa los roles en Invoker (RemoteControl), Command (ICommand y sus implementaciones) y Receiver (Light), alineándose con la estructura clásica GoF,
permitiendo que el sistema sea extensible sin modificar las clases existentes.

**🛠️ Sugerencias de Mejora**
- Podría incluir un ejemplo de macrocomando o historial de comandos para mostrar el potencial del patrón Command en escenarios más complejos.
- Sería recomendable agregar manejo de errores o validaciones al momento de asignar comandos nulos al RemoteControl.

**🎯 Entrega Final**

“Excelente aplicación del patrón Command. El código refactorizado demuestra comprensión del diseño estructural y elimina las dependencias innecesarias entre clases. 
El trabajo cumple plenamente con los criterios técnicos.”
