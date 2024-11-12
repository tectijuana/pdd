
<img width="997" alt="Screenshot 2024-11-11 at 3 57 05 p m" src="https://github.com/user-attachments/assets/80318fd5-2197-497e-8832-36644ea2f40a">

Video:  https://youtu.be/YFUv4_lCBLY?si=JkYeOV-8IU6GrJbX

# Proyecto Cognify: Transformación de la Rehabilitación Criminal

## Introducción
El proyecto **Cognify** plantea una nueva perspectiva en la rehabilitación de criminales, utilizando tecnología avanzada para tratar a los delincuentes como pacientes. Este enfoque reduce la dependencia de las cárceles tradicionales mediante la implantación de recuerdos artificiales, lo que permite cumplir sentencias largas en solo minutos. Cognify no solo busca rehabilitar, sino también reimaginar el sistema de justicia penal.

---

## Objetivos del Proyecto

1. **Rehabilitación Eficiente:** Transformar las experiencias de los delincuentes mediante recuerdos artificiales diseñados para fomentar la empatía y el arrepentimiento.
2. **Reducción de Costos:** Minimizar los costos asociados con el encarcelamiento tradicional, como la construcción y mantenimiento de prisiones.
3. **Reinserción Social:** Permitir una reintegración rápida y efectiva de los criminales rehabilitados a la sociedad.
4. **Innovación en Justicia Penal:** Proporcionar una alternativa ética y tecnológica al castigo convencional, centrada en el aprendizaje y la rehabilitación.

---

## Descripción del Sistema Cognify

### Funcionamiento
1. **Elección del Delincuente:** 
   - El preso elige entre cumplir una sentencia tradicional o someterse a rehabilitación acelerada a través de Cognify.
2. **Mapeo Cerebral:**
   - Se realiza un escaneo cerebral de alta resolución para identificar regiones clave como el hipocampo, la corteza prefrontal y la amígdala, responsables de la memoria y el razonamiento.
3. **Implantación de Recuerdos:**
   - Basándose en la gravedad del crimen, se crean e implantan recuerdos diseñados para generar empatía, arrepentimiento y consciencia de las consecuencias.
4. **Tiempo Percibido:**
   - Los recuerdos son experimentados en tiempo ralentizado, simulando años de vivencias en pocos minutos.

### Personalización de Recuerdos
- **Ofensores Violentos:** Experiencias desde la perspectiva de la víctima, incluyendo su sufrimiento y el impacto en sus familias.
- **Crímenes Financieros:** Simulación de las consecuencias económicas y sociales de sus actos.
- **Crímenes de Odio:** Recuerdos que promuevan la comprensión y el respeto por la diversidad.

---

## Componentes Tecnológicos

1. **Sistema de Creación de Recuerdos:** 
   - Contenido generado por inteligencia artificial para ofrecer experiencias vívidas y realistas.
2. **Regulación Emocional:**
   - Modulación de neurotransmisores para inducir estados emocionales como el arrepentimiento.
3. **Monitorización en Tiempo Real:**
   - Análisis continuo de respuestas neuronales para ajustar y optimizar la terapia.
4. **Seguridad y Portabilidad:**
   - Diseño compacto y encriptado para garantizar la protección de datos y su uso en diversas ubicaciones.

---

## Impacto Social y Económico

### Reducción de Costos
- Menor necesidad de infraestructura carcelaria.
- Reducción en gastos de mantenimiento, alimentación y salud de los presos.

### Reasignación de Recursos
- Fondos redirigidos hacia educación, salud, infraestructura y programas sociales.

### Beneficios Económicos
- Reintegración más rápida de los criminales al mercado laboral.
- Aumento de la productividad y generación de ingresos fiscales.

### Mejora Social
- Comunidades más seguras.
- Reducción de la reincidencia y mejora del bienestar social.

---

## Aplicaciones Adicionales

- **Tratamiento de Pérdida de Memoria:** Ayuda a pacientes a recuperar recuerdos perdidos.
- **Tratamiento de PTSD:** Sustitución de recuerdos traumáticos por experiencias positivas.

---

Cognify redefine la justicia penal mediante la innovación tecnológica. Este sistema no solo ofrece una solución ética y eficiente para la rehabilitación de criminales, sino que también contribuye a la construcción de una sociedad más segura, inclusiva y próspera.

----

### Misión del Proyecto Final: Implementación del Patrón MVC para Cognify

**Misión:** Diseñar e implementar un sistema funcional utilizando el patrón MVC (Modelo-Vista-Controlador) que soporte las operaciones del sistema Cognify, alineándose con sus objetivos tecnológicos, sociales y éticos. El proyecto debe reflejar la aplicación de patrones de diseño para garantizar modularidad, mantenibilidad y escalabilidad.

---

### **Objetivos del Proyecto y Evaluación**

| **Objetivo**                                                                 | **Descripción**                                                                                                                                                          | **Ponderación (%)** |
|------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------|
| **1. Implementación del Modelo:**                                            | Diseñar las clases y estructuras de datos necesarias para representar los componentes del sistema Cognify, incluyendo delincuentes, recuerdos y configuraciones personalizadas. | 20%                 |
| **2. Diseño de la Vista:**                                                   | Crear una interfaz gráfica o consola que permita interactuar con el sistema, incluyendo la selección de opciones (rehabilitación acelerada o sentencia tradicional) y la personalización de recuerdos. | 20%                 |
| **3. Controlador Funcional:**                                                | Desarrollar el controlador que gestione las operaciones principales: entrada de datos, comunicación entre el modelo y la vista, y ejecución del mapeo cerebral y simulación. | 20%                 |
| **4. Integración de Patrones Creacionales:**                                 | Aplicar patrones creacionales (Factory Method, Singleton o Builder) para gestionar la creación de recuerdos artificiales y la personalización según el tipo de delito. | 20%                 |
| **5. Evaluación de Impacto:**                                                | Simular escenarios de uso del sistema, evaluando su impacto en términos de eficiencia, reducción de costos y reinserción social. Documentar los resultados.                 | 20%                 |

---

### **Detalles Técnicos y Entregables**

1. **Modelo:**  
   - Clases principales: `Criminal`, `Memory`, `Simulation`, `CrimeType`.  
   - Relación entre clases y uso de patrones como Singleton para gestionar la instancia global de configuración.

2. **Vista:**  
   - Interfaz interactiva que permita seleccionar las opciones descritas en el sistema Cognify (elección del método, personalización de recuerdos, etc.).  
   - Puede ser implementada como una aplicación de consola o gráfica.

3. **Controlador:**  
   - Coordinación de las acciones de entrada del usuario con el modelo y la vista.  
   - Gestión de flujos: lectura de datos, creación de recuerdos, y simulación.

4. **Patrones Creacionales:**  
   - **Factory Method:** Para la creación de recuerdos basados en el tipo de crimen.  
   - **Singleton:** Para el gestor de configuración de simulaciones.  
   - **Builder:** Para la construcción de recuerdos personalizados.

5. **Evaluación Final:**  
   - Documentación que incluya escenarios de prueba, análisis de eficiencia y diagramas UML de la implementación del patrón MVC y patrones creacionales.

---

### **Formato de Evaluación**

| **Criterio**                          | **Aspecto Evaluado**                                                   | **Puntos Máximos** |
|---------------------------------------|------------------------------------------------------------------------|--------------------|
| **Correctitud Técnica:**              | Uso apropiado del patrón MVC y patrones creacionales en el proyecto.   | 40                 |
| **Interfaz de Usuario:**              | Funcionalidad y usabilidad de la interfaz desarrollada.                | 20                 |
| **Complejidad de Escenarios:**        | Inclusión de casos de uso complejos y simulaciones significativas.     | 20                 |
| **Documentación:**                    | Diagrama UML, justificación técnica y reporte de evaluación de impacto.| 10                 |
| **Creatividad e Innovación:**         | Soluciones innovadoras y extensibles al sistema Cognify.               | 10                 |

**Total:** 100 puntos

---

### **Entrega Final**

1. Código fuente funcional (repositorio ).  
2. Documento técnico con:
   - Diagramas UML del modelo, vista y controlador.
   - Análisis del impacto social y técnico del sistema.  
   - Pruebas y resultados simulados.  
3. Video LOOM de demostración del sistema en funcionamiento. 

Este enfoque integrador evalúa habilidades técnicas y la capacidad de aplicar patrones de diseño en un contexto realista e innovador.
