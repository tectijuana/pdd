# Asistencia de Inteligencia Artificial

## Alumna: Sanchez Hernandez Evelyn Belen
- **Número de control:** 21212047
- **Tema asignado:** #38 — Tener una clase CreatorFactoryBuilder

---

## Prompts utilizados
1. "Genera en C# un ejemplo de **clase CreatorFactoryBuilder** que combine Factory y Builder en una sola clase como antipatrón en el dominio de usuarios."  
2. "Identifica y explica **al menos 3 problemas creacionales graves** en ese antipatrón (violación SRP, acoplamiento, baja testabilidad)."  
3. "Refactoriza el código separando responsabilidades con **Factory Method** y **Builder**, con justificación técnica."  

---

## Agentes y herramientas utilizadas
- **ChatGPT (GPT-5)** — apoyo en la generación de ejemplos, detección de problemas y propuesta de refactorización.  
- **GitHub Web** — para la gestión de ramas, subida de archivos (`readme.md` y `anexo.md`) y creación del Pull Request.  

---

## Cambios y evaluación de la refactorización
- Se eliminó la clase híbrida **CreatorFactoryBuilder**, la cual centralizaba demasiadas responsabilidades.  
- Se aplicó el **Factory Method** para encapsular la creación de usuarios por tipo (Admin, Reader).  
- Se utilizó un **Builder** para estructurar la construcción de instancias de `User` paso a paso y con validaciones.  
- Se mejoró la **cohesión interna**, reduciendo acoplamiento y facilitando la prueba unitaria.  
- La solución cumple con principios **SOLID (SRP y OCP)** y es extensible para futuros roles de usuario.  


---

## Metadatos
- **Fecha:** 2025-09-24  
- **Versión del trabajo:** `practica_creacionales_tema38_v1`  
- **Ruta sugerida de entrega:** `practicas/creacionales/fix/Tener una clase CreatorFactoryBuilder/anexo.md`
