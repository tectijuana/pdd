## Uso de IA (Asistencia de LLM)
Se utilizó asistencia de un modelo de lenguaje (LLM) para apoyar el proceso de refactor y la redacción del Pull Request. A continuación se describe cómo se empleó y las buenas prácticas seguidas:

**Modelo utilizado:** ChatGPT (GPT-5 Thinking mini).

**Qué se le pidió a la IA (prompts resumidos):**
- Detectar problemas estructurales y code smells en el código original del patrón Decorator.
- Proponer una implementación refactorizada que cumpla la interfaz `IEstudiante` y permita encadenar decoradores.
- Generar código C# corregido y un ejemplo de ejecución en `Main()` con salida de consola esperada.
- Redactar la justificación técnica y la sección para incluir en el Pull Request (rubrica, beneficios, uso de Git, etc.).

**Cómo se verificó la salida del LLM por el autor humano:**
- Se revisó manualmente el código generado para asegurar que compila y respeta buenas prácticas de C#/.NET.  
- Se ejecutaron pruebas básicas (compilación y ejecución local) para validar la salida esperada.  
- Se ajustaron nombres y documentación para mantener coherencia con estándares del proyecto.

**Limitaciones y responsabilidad:**  
- La IA **asistió** en la generación de código y documentación; el autor humano es responsable de validar, adaptar y aprobar los cambios finales.  
- Cualquier decisión de diseño final fue tomada y validada por el desarrollador responsable del PR.  
- Se recomienda ejecutar pruebas automatizadas y revisiones de seguridad adicionales antes de mergear a la rama principal.

**Sugerencia para la inclusión en el PR:**  
Agregar una línea de atribución en el PR, por ejemplo:  
> "Asistencia de IA: código y redacción inicial generados con ChatGPT (GPT-5 Thinking mini); revisado y adaptado por Thejimmy246."
