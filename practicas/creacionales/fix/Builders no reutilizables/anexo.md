Anexo.md

# 📄 Declaración de Asistencia de Inteligencia Artificial

Completa esta sección de forma honesta y reflexiva. Esta declaración forma parte de la evaluación del trabajo.

---

## 📌 Prompts utilizados
A continuación se enumeran los prompts principales que se enviaron a la herramienta de IA durante el desarrollo del proyecto:

- "📖 Refactorización de Vehículos con Builders no reutilizables, dame un PR completo con explicación y justificación."  
- "Dame el código C# con malas prácticas de Builder que comparte estado interno y el código correcto aplicando un Builder reutilizable."  
- "Pon todo en formato Markdown, incluyendo explicación, problemas detectados, patrones aplicados y código."  
- "Agrega comentarios en el código sobre los errores del Builder no reutilizable y la solución aplicada."  
- "Crea un ejemplo de declaración de asistencia de IA en Markdown basado en el trabajo realizado."  

---

## 🧠 Agentes o herramientas utilizadas
*ChatGPT (GPT-5)* – para:  

- Revisar ejemplos de implementación del patrón Builder en C#.  
- Identificar problemas de reutilización de estado en Builders.  
- Generar explicaciones claras sobre las malas prácticas y su refactorización.  
- Transformar el contenido en formato Markdown listo para un Pull Request.  
- Añadir comentarios explicativos en el código sobre los patrones aplicados.  

No se utilizaron otras herramientas de IA como GitHub Copilot o Perplexity en este ejercicio.  

---

## 🔍 Cambios realizados y evaluación crítica
- Refactoricé el Builder para que cada llamada a Build() genere un *nuevo objeto limpio*, evitando que se compartan configuraciones previas.  
- Implementé un *método Reset interno* en el Builder para reiniciar su estado tras cada construcción.  
- Añadí un *Director* para centralizar configuraciones frecuentes (ejemplo: Auto estándar, Moto deportiva).  
- Comenté el código para dejar clara la diferencia entre un Builder inseguro y uno reutilizable.  
- Validé el diseño bajo principios SOLID, en especial el *SRP* y el *principio de construcción segura*.  

---

## ✍ Reflexión personal
- Aprendí a detectar un error común en la aplicación del patrón Builder: el problema de los *Builders no reutilizables* que comparten estado interno.  
- La IA me ayudó a estructurar la explicación y entender cómo aplicar un *Reset* en la construcción para evitar inconsistencias.  
- Pude reforzar la idea de separar la *lógica de construcción* de la *representación final del objeto*.  
- Este ejercicio me dio más criterio para decidir cuándo conviene aplicar Builder con Director y cuándo no es necesario.  

---

## 📅 Datos finales
- *Fecha de la asistencia IA:* 29 de septiembre de 2025  
- *Versión de entrega/práctica:* Refactorización de VehiculosApp corrigiendo Builders no reutilizables  
- *Herramientas:* ChatGPT (GPT-5)  

---