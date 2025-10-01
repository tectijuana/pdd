# 📌 Declaración de Asistencia de IA

**Herramientas utilizadas:**  
- ChatGPT (modelo GPT-5, OpenAI)  
- BadCode Generator (para generar ejemplos de código con fallos intencionales)  

**Fecha de uso:** 24/09/2025  
**Plataforma de hardware:** PC con Windows 10  

---

## 📝 Solicitud al LLM
- Consulté cuál era el problema principal en el `OrdenBuilder` que permitía crear órdenes incompletas.  
- Pedí ejemplos de aplicación correcta del patrón Builder.  
- Solicité ejemplos con datos faltantes y cómo manejar errores con `try/catch`.  
- Requerí una justificación técnica clara para documentar el cambio.  

---

## 📦 Respuesta recibida
- Identificación del problema: el Builder no validaba campos obligatorios y permitía órdenes inconsistentes.  
- Código refactorizado con validación en el método `Build()`.  
- Ejemplos de ejecución con datos faltantes (cliente, productos, total).  
- Manejo de errores con `try/catch` para evitar que el programa se detenga.  
- Justificación técnica basada en SRP, robustez, testabilidad y flexibilidad.  

---

## 🔧 Adaptaciones realizadas
- Integré el bloque `try/catch` en `Main` para manejar varios casos de error.  
- Ajusté los mensajes de error a un formato más claro para consola.  
- Añadí ejemplos extra de fallos (ejemplo: total en cero).  
- Usé la justificación técnica como base para el documento de Pull Request.  
- Utilicé **BadCode Generator** para simular un escenario de código mal estructurado y luego refactorizarlo con la ayuda del LLM.  

---
