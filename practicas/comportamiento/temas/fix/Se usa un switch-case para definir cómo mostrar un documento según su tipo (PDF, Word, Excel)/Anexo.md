# 📄 Declaración de Asistencia de Inteligencia Artificial

Esta sección refleja el uso de IA durante el desarrollo del proyecto de refactorización y mejora del sistema de visualización de documentos en C#.  

---

## 📌 Prompts utilizados

Durante el desarrollo se consultó la herramienta de IA con prompts clave como:

- "Generame un código espagueti con switch-case para mostrar PDF, Word y Excel y refactorizarlo usando Template Method."  
- "Explica cuál es el patrón ausente y justifica su elección."  
- "Muestra cómo implementar una parte funcional (PDF) usando Template Method en C#."  
- "Entrega el código final refactorizado listo para correr en .NET Fiddle."  
- "Formatea todos los problemas detectados y explicaciones en Markdown para el informe."  

---

## 🧠 Herramientas utilizadas

- **ChatGPT (GPT-5 mini)** – para:  
  - Revisar y refactorizar el código inicial con malas prácticas (switch-case repetitivo).  
  - Identificar el patrón Template Method como solución y justificarlo.  
  - Generar ejemplos funcionales en C# de cada tipo de documento siguiendo la estructura del patrón.  
  - Formatear el contenido, explicaciones y código en Markdown listo para entrega.  

- **No se utilizaron otras herramientas de IA** durante este proyecto.  

---

## 🔍 Cambios realizados y evaluación crítica

- Transformé el código espagueti inicial en un **Template Method** limpio y extensible.  
- Implementé correctamente la parte funcional para PDF y extendí a Word y Excel.  
- Centralicé el flujo común (`Abrir → Cargar → Mostrar`) en la clase base, reduciendo duplicación de código.  
- Aseguré el cumplimiento del **principio Open/Closed (OCP)** y mejoré la mantenibilidad del código.  
- Revisé manualmente la ejecución en C# y eliminé prácticas poco seguras o repetitivas.  

---

## ✍️ Reflexión personal

- Aprendí a identificar **code smells** y cómo los patrones de diseño solucionan problemas de duplicación y mantenimiento.  
- La IA facilitó la comprensión de **Template Method** y cómo separar la lógica común de las variaciones específicas de cada tipo de documento.  
- Me permitió entender mejor la importancia de estructurar el código para que sea extensible y fácil de probar.  

---

## 📅 Datos finales

- **Fecha de asistencia IA:** 7 de octubre de 2025  
- **Versión de entrega/práctica:** Refactorización de sistema de visualización de documentos con Template Method  
- **Herramientas:** ChatGPT (GPT-5 mini)  
