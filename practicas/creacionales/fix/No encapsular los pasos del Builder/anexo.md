# Anexo – Refactorización de patrón Builder en C# (Carrocería)  
**Autor:** Diego Huerta Espinoza – 20212411  

Herramienta: ChatGPT  
Asistencia de IA: Consulté a ChatGPT para que me ayudara a estructurar un ejemplo de **antes y después** en C# sobre el uso correcto del patrón **Builder**, aplicado al caso de la carrocería de un vehículo. Además, organicé la documentación en formato markdown para incluirla en el Pull Request.  
Fecha: 2025-09-24  

### Prompts utilizados
- "necesito mostrar un codigo de ejemplo donde de note el cambio al solucionar el problema que te di"  
- "ayudame a organizar para hacer mi readme con el formato que necesito"  
---

### Mejoras aplicadas
- Se eliminó la dependencia del cliente de llamar manualmente los métodos del `Builder`.  
- Se implementó la clase **Director** para encapsular los pasos de construcción.  
- Se mejoró la **consistencia** y se evitó que el cliente genere objetos incompletos.  
