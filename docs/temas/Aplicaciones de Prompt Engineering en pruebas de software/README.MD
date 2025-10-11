# Prompt Engineering en Pruebas de Software
Nombre: Luis Felipe Torres Coto Rodarte  
Num. Control: 21212368

## 🎯 Introducción
El **Prompt Engineering** es la práctica de diseñar y ajustar instrucciones para interactuar con modelos de lenguaje (LLMs) y obtener resultados útiles.  
En el área de **pruebas de software**, representa una herramienta innovadora para mejorar la calidad, automatización y eficiencia en los procesos de validación.

---

## ⚙️ Area de aplicacion

### 📝 Generación de Casos de Prueba
Genera casos de prueba a partir de requisitos, historias de usuarios o documentacion, se pueden crear prompts para generar casos de prueba de manera automatica.   
**Ejemplo:**   
> "Genera casos de prueba funcionales para un sistema de login con usuario y contraseña."

---

### 🧑‍💻 Automatización con PyTest
Con promps bien diseñados, se pueden generar scripts en herramientas de pruebas como Selenium, Cypress, JUnit, PyTest  
**Ejemplo:**  
> "Crea un test en Python usando PyTest para verificar que un formulario de registro rechaza correos sin el símbolo @."
---

### 🧑‍💻 Revisión de Requisitos
Se pueden usar modelos para analizar escritos del lenguaje natural y detectar ambigüedades. Por ejemplo: datos extremos, cadenas SQL maliciosas, inputs vacios o muy largos.  
**Ejemplo:**  
> "Detecta posibles inconsistencias en este documento de requisitos para una aplicación bancaria."

---

### 🧑‍💻 Automatización de Scripts de Prueba
Generar scripts automatizados en frameworks como Selenium, JUnit o PyTest a partir de descripciones.  
**Ejemplo:**  
> "Crea un test de Selenium que valide el registro de usuarios en esta página web."

---

## ✅ Beneficios
- **Ahorro de tiempo**
- **Mejor cobertura** 
- **Apoyo a testers juniors**

---

## ⚠️ Limitaciones
- **Dependencia de la calidad del prompt:** Si el prompt está mal diseñado, el resultado será incompleto, ambiguo o inútil.
- **Posibilidad de errores o invenciones:** Los modelos de lenguaje pueden “alucinar” información (falsos positivos o casos de prueba irreales).
- **Necesidad de validación humana:** Siempre se requiere que un tester revise y depure lo generado para asegurar su utilidad. 

--- 

## Reflexion
El Prompt Engineering en pruebas de software representa una evolución en la forma en que los ingenieros de calidad pueden diseñar y ejecutar validaciones. Su uso agiliza la generación de casos de prueba, fomenta la creatividad en la detección de escenarios no triviales y contribuye a una mayor cobertura en menos tiempo.
