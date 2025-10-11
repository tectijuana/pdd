# 📄 Declaración de Asistencia de Inteligencia Artificial.

Completa esta sección de forma honesta y reflexiva. Esta declaración forma parte de la evaluación del trabajo.

---

## 📌 Prompts utilizados
A continuación se enumeran los prompts principales que se enviaron a la herramienta de IA durante el desarrollo del proyecto:

- "📖 Refactorización de Componentes Gráficos con Composite, dame un PR completo con explicación y justificación."  
- "Dame el código C# con malas prácticas usando if/switch para distinguir tipos y el código correcto aplicando Composite y polimorfismo."  
- "Pon todo en formato Markdown, incluyendo explicación, problemas detectados, patrones aplicados y código."  
- "Agrega comentarios en el código sobre los errores de diseño y la solución aplicada con Composite."  
- "Crea un ejemplo de declaración de asistencia de IA en Markdown adaptado a este ejercicio."  

---

## 🧠 Agentes o herramientas utilizadas
*ChatGPT (GPT-5)* – para:  

- Revisar ejemplos de implementación del patrón Composite en C#.  
- Identificar problemas comunes al usar estructuras condicionales en lugar de polimorfismo.  
- Explicar cómo el patrón Composite resuelve la extensibilidad y reduce el acoplamiento.  
- Generar un formato de Pull Request en Markdown con justificación y reflexión.  
- Incluir comentarios aclaratorios en el código sobre las malas prácticas y su refactorización.  

No se utilizaron otras herramientas de IA como GitHub Copilot o Perplexity en este ejercicio.  

---

## 🔍 Cambios realizados y evaluación crítica
- Reemplacé el uso de `if/switch` basado en el atributo `Type` por una jerarquía de clases con polimorfismo.  
- Apliqué el patrón Composite para que los objetos `Window` puedan contener otros componentes gráficos de manera recursiva.  
- Comenté el código para señalar claramente las diferencias entre el diseño rígido (con condicionales) y el diseño extensible con Composite.  
- Validé el diseño bajo principios SOLID, en especial *OCP (Open/Closed)* y *SRP (Single Responsibility)*.  
- Preparé la solución para facilitar la inclusión de nuevos componentes sin modificar el código existente.  

---

## ✍ Reflexión personal
- Aprendí a reconocer por qué usar `if/switch` para distinguir tipos es una mala práctica que rompe la extensibilidad.  
- La IA me ayudó a comprender cómo el polimorfismo y el patrón Composite eliminan la necesidad de condicionales y permiten manejar estructuras jerárquicas más limpias.  
- Ahora tengo más criterio para aplicar Composite en escenarios donde hay elementos que pueden agruparse o contener otros del mismo tipo.  
- Me quedó claro que este patrón no solo organiza el código, sino que facilita el mantenimiento y la escalabilidad de la aplicación.  

---

## 📅 Datos finales
- *Fecha de la asistencia IA:* 30 de septiembre de 2025  
- *Versión de entrega/práctica:* Refactorización de Componentes Gráficos aplicando Composite en lugar de if/switch  
- *Herramientas:* ChatGPT (GPT-5)  

---
