# Asistencia de Inteligencia Artificial

## Datos del alumno
- **Nombre:** Jocelin Maribel Bernal Enciso
- **Número de control:** 21211919
- **Tema asignado:** #5 — Constructor con lógica pesada
- **Materia:** Patrones de Diseño de Software (DSF-2101 SC8A)
- **Docente:** René Solís Reyes

---

## Prompts representativos utilizados
> Prompts guía utilizados para idear y revisar la solución. Se presentan de forma sintética para trazabilidad académica.

1. **Generación de Bad Code (C# .NET 8):**  
   «Genera un ejemplo de consola en C# del dominio *vehículos* que muestre el **smell #5 (constructor con lógica pesada)**. El constructor debe cargar/normalizar datos, seleccionar tipo por `switch`, instanciar con `new` y producir efectos secundarios con `Console.WriteLine`. Incluye un Singleton **inseguro y mutable** para configuración.»

2. **Estrategia de refactor con patrones GoF:**  
   «Propón una **estrategia de refactor** aplicando **Builder** (separar construcción y validar invariantes), **Factory Method** (eliminar `switch/new`), y **Singleton thread-safe e inmutable** (configuración). Resume cómo cada patrón reduce acoplamiento y mejora extensibilidad.»


---

## Agentes y herramientas
- **Asistente principal:** ChatGPT (GPT-5 Thinking) — apoyo en análisis, estructuración y generación de ejemplos mínimos ejecutables.
- **Herramienta de verificación:** .NET Fiddle — compilación y ejecución de los ejemplos antes/después.
- **Entorno de entrega:** GitHub Web (edición de archivos, rama `fix/refactor-JocelinBernal`, Pull Request).


