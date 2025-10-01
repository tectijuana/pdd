# AI_GUIDANCE.md  
**Uso responsable y profesional de Inteligencia Artificial en el curso**

## 📘 Guía para estudiantes  
Este documento establece las pautas para el uso ético, reflexivo y técnicamente riguroso de herramientas de **Inteligencia Artificial (IA)** en el contexto del desarrollo de software y hardware embebido.

---

## 🎯 Objetivo

Aprovechar herramientas de IA como apoyo en el **aprendizaje técnico, la documentación y la exploración de código**, sin sustituir la **validación experimental**, el **razonamiento ingenieril** ni el **trabajo personal** sobre plataformas de hardware reales.

---

## ✅ Usos recomendados y valorados
- Solicitar explicaciones de conceptos clave: comunicación UART, I2C, SPI, interrupciones, timers, ADC, DMA.
- Generar **ejemplos de código de referencia** en C, C++ o ensamblador.
- Explorar variantes en la implementación de controladores, protocolos o rutinas de bajo nivel.
- Apoyarse en IA para generar **comentarios explicativos** o documentación técnica del código.
- Traducir o resumir secciones complejas de manuales técnicos o datasheets.

---

## 🚫 Usos no permitidos
- Entregar código generado por IA sin comprender su funcionamiento ni realizar pruebas en hardware.
- Utilizar IA para diseñar esquemas eléctricos o temporizaciones sin consultar **fuentes oficiales ni validar experimentalmente**.
- Delegar en IA la selección de componentes o estimación de consumo energético sin análisis ingenieril.

---

## 🧠 Buenas prácticas recomendadas

1. **Valida en hardware real**  
   La IA puede generar código que compila, pero solo tú puedes verificar su funcionamiento en un entorno físico.

2. **Consulta siempre el datasheet**  
   Usa la IA como apoyo complementario, pero **la fuente oficial es el fabricante**.

3. **Transparencia profesional**  
   Declara claramente qué parte de tu trabajo fue asistida por IA.

4. **Prompts técnicos y reflexión**  
   Formula preguntas específicas y registra tus *prompts*. Evalúa críticamente las respuestas.

5. **Explora con criterio múltiples herramientas**  
   Puedes usar ChatGPT, Copilot, Perplexity, etc., pero sé selectivo y consciente de sus limitaciones.

6. **Incluye reflexión final**  
   Comenta qué aprendiste, qué ajustaste y cómo validaste tus resultados.

---

## 📝 Formato obligatorio de declaración en prácticas o proyectos

```markdown
### Asistencia de Inteligencia Artificial

- **Prompts utilizados**:
  - "¿Cómo configurar el módulo ADC del PIC18F4550 en modo continuo con interrupciones?"
  - "Genera un ejemplo de manejo de SPI en STM32 con HAL."

- **Herramientas utilizadas**:
  - ChatGPT (GPT-4o)
  - GitHub Copilot

- **Cambios y validación**:
  - El código generado fue modificado para adaptarse al compilador XC8.
  - Se realizaron pruebas en protoboard con señales de entrada reales.
  - Verifiqué el funcionamiento usando lógica de test con LEDs y osciloscopio.

- **Reflexión personal**:
  La IA me ayudó a clarificar la configuración inicial, pero tuve que corregir errores de temporización. Esto reforzó mi entendimiento del ciclo de reloj y del manejo de interrupciones.

- **Fecha**: 2025-09-18  
- **Plataforma de hardware utilizada**: PIC18F4550 en protoboard, oscilador de 20 MHz  

---

Perfecto 👌. Aquí tienes la versión lista para integrar en tu AI_GUIDELINE.md, adaptada a cursos de Ingeniería en Sistemas y con un checklist crítico que los estudiantes deben aplicar cuando usen un LLM en sus prácticas:

⸻

🧠 Pensamiento Crítico y Uso Responsable de IA

Guía para Ingeniería en Sistemas

🎯 Objetivo

Orientar al estudiante en el uso crítico y reflexivo de LLMs (modelos de lenguaje como ChatGPT) en prácticas y proyectos académicos, asegurando que el contenido generado sea comprendido, verificado y mejorado antes de entregarlo.

⸻

🔹 Checklist de Preguntas Críticas

👤 QUIÉN
	•	¿Quién se beneficia de este diseño, código o propuesta?
	•	¿Quién sería responsable si falla este sistema?
	•	¿Quién falta en el análisis (usuarios finales, cliente, equipo de soporte)?
	•	¿Quién ya resolvió un problema similar (estándares, frameworks, bibliografía)?

📌 QUÉ
	•	¿Qué problema técnico estoy intentando resolver realmente?
	•	¿Qué parte de la respuesta de la IA son hechos comprobables y qué son suposiciones?
	•	¿Qué está asumiendo la IA sin que yo lo haya validado (plataforma, librerías, contexto)?
	•	¿Qué información o detalle falta (diagramas, dependencias, pruebas)?

🕒 CUÁNDO
	•	¿Cuándo debe tomarse esta decisión técnica?
	•	¿Cuándo en el ciclo de vida del software es más apropiado aplicar esta solución?
	•	¿Cuándo he visto errores similares en otros proyectos?
	•	¿Cuándo sería riesgoso implementar lo que propone la IA?

🌍 DÓNDE
	•	¿De dónde provienen los datos o ejemplos que usó la IA?
	•	¿Dónde se implementará este sistema (nube, local, IoT) y cambia eso la validez?
	•	¿Dónde puede fallar este diseño (rendimiento, seguridad, escalabilidad)?
	•	¿Dónde encuentro documentación oficial o pruebas que lo respalden?

❓ POR QUÉ
	•	¿Por qué este enfoque es mejor que otras alternativas?
	•	¿Por qué creo que la salida es correcta y no un error del modelo?
	•	¿Por qué otros podrían verlo distinto (otro lenguaje, paradigma, contexto)?
	•	¿Por qué no hemos resuelto esto con técnicas tradicionales ya conocidas?

⚙️ CÓMO
	•	¿Cómo mediré el éxito de implementar esta propuesta (tests, benchmarks, validación)?
	•	¿Cómo podría fallar este código en producción?
	•	¿Cómo pruebo la validez de lo que me dio la IA antes de usarlo?
	•	¿Cómo explicaré mi decisión de usar IA a mis compañeros, profesor o cliente?

⸻

📌 Ejemplos de aplicación en cursos
	•	Lenguajes de Interfaz (ARM/Assembly):
Si la IA genera un programa, preguntar:
“¿Qué registros preserva y dónde lo verifico en el ABI oficial de ARM?”
	•	Patrones de Diseño (GoF en C#):
Si la IA sugiere Singleton, cuestionar:
“¿Por qué elegir este patrón y no otro? ¿Dónde sería un antipatrón en sistemas distribuidos?”
	•	Bases de Datos:
Si la IA entrega un query SQL:
“¿Cómo afectará el rendimiento en tablas grandes? ¿Qué índices faltan?”
	•	Cultura Digital – IoT con micro:bit:
Si la IA genera un script:
“¿Cómo sé que maneja errores de hardware? ¿Dónde lo pruebo antes de cargarlo al dispositivo?”

⸻

📝 Responsabilidad académica
	1.	Documentar en ANEXO.md:
	•	Prompts utilizados.
	•	Cambios o mejoras realizadas tras usar pensamiento crítico.
	•	Referencias oficiales o pruebas adicionales consultadas.
	2.	Reflexionar:
	•	¿Qué sesgos, errores o vacíos encontré en la respuesta de la IA?
	•	¿Qué aprendí del proceso de revisión?