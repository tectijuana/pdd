# Investigación: Comparativa de Herramientas de Refactorización Asistida

### Alumno: Gonzalez Carrillo Valeri Alexandra - 21211955

## 1. Introducción

La **refactorización de código** es la práctica de reestructurar el código para mejorar su diseño interno sin cambiar su comportamiento externo. Es un proceso fundamental para reducir la **deuda técnica**, mejorar la **mantenibilidad** y facilitar la **escalabilidad** del software. Las **herramientas de refactorización asistida** automatizan este proceso, permitiendo a los desarrolladores realizar cambios complejos con rapidez y seguridad.

Esta investigación compara tres de las plataformas más populares con potentes capacidades de refactorización asistida:
- **JetBrains** (IntelliJ IDEA, PyCharm, etc.)
- **Visual Studio Code** (VS Code)
- **Eclipse**

---

## 2. Criterios de Evaluación

Para esta comparativa, se analizarán las herramientas basándose en los siguientes criterios:

1.  **Funcionalidad:** La variedad y el alcance de las refactorizaciones disponibles.
2.  **Rendimiento:** La velocidad y el consumo de recursos de la herramienta en proyectos de diferentes tamaños.
3.  **Seguridad:** La capacidad de la herramienta para prevenir errores y efectos secundarios no deseados.
4.  **Experiencia de Usuario (UX):** La facilidad de uso y la intuición de la interfaz.
5.  **Costo y Flexibilidad:** El modelo de licenciamiento y la capacidad de personalización.

---

## 3. Análisis Comparativo Detallado

| Característica | JetBrains | Visual Studio Code | Eclipse |
| :--- | :--- | :--- | :--- |
| **Enfoque** | IDE inteligente, integrado y proactivo. | Editor de código ligero y modular. | IDE maduro, de código abierto y robusto. |
| **Funcionalidad** | Amplia gama de refactorizaciones avanzadas. Usa un motor de análisis de código para entender la sintaxis y relaciones entre componentes. | Depende de extensiones (Language Server Protocol). Las funciones varían en calidad según la extensión. | Fuerte en refactorización para Java. Su motor ha sido perfeccionado durante décadas y es muy confiable. |
| **Rendimiento** | Puede ser exigente con los recursos en proyectos grandes debido a su análisis en tiempo real. | Generalmente rápido y eficiente, ideal para máquinas con recursos limitados. | Rendimiento intermedio. Puede ser lento en arrancar, pero estable en uso. |
| **Seguridad** | **Alta.** Su motor de análisis avanzado minimiza errores y ofrece vistas previas detalladas de los cambios. | **Variable.** La seguridad depende de la calidad de la extensión y del servidor de lenguaje. | **Alta.** Especialmente para Java, sus refactorizaciones son probadas y confiables. |
| **Costo** | De pago (suscripción anual) para la versión completa. | **Gratuito** y de código abierto. | **Gratuito** y de código abierto. |
| **Experiencia de Usuario** | Interfaz intuitiva y consistente. Las refactorizaciones tienen atajos de teclado y vistas previas. | Interfaz minimalista y personalizable. Las refactorizaciones se realizan a través de menús contextuales o atajos. | Puede tener una curva de aprendizaje más pronunciada debido a su interfaz densa en opciones. |

---

## 4. Relación con la Calidad del Código y Patrones de Diseño

El uso de estas herramientas es un pilar fundamental para mejorar la **calidad del código**. Una refactorización asistida permite:

* **Reducir la complejidad (Ciclomática):** Al extraer métodos, se descomponen funciones largas y complejas en unidades más pequeñas y fáciles de entender.
* **Facilitar la aplicación de patrones:** Las refactorizaciones como `Extract Method` o `Extract Interface` son pasos directos para implementar patrones de diseño como el "Strategy" o el "Adapter". El uso de la herramienta garantiza que estos cambios no introduzcan nuevos errores.
* **Mejorar la legibilidad y la mantenibilidad:** Un código bien refactorizado es más fácil de leer, lo que reduce el tiempo de adaptación de nuevos miembros del equipo y simplifica la detección de fallos.

---

## 4. Ejemplo Práctico: Refactorización "Extraer Método"
Ejemplo en Java de un problema común en el código: un cálculo se repite en dos métodos diferentes. El objetivo de la refactorización es "extraer" este cálculo a un nuevo método reutilizable.
```java
public class CarritoDeCompras {

    public double procesarCompra() {
        double precioBase = 150.0;
        double impuesto = precioBase * 0.16;
        double descuento = precioBase > 100 ? precioBase * 0.10 : 0;
        double precioFinal = precioBase + impuesto - descuento;
        return precioFinal;
    }

    public double procesarCompraConEnvioGratis() {
        double precioBase = 150.0;
        double impuesto = precioBase * 0.16;
        double descuento = precioBase > 100 ? precioBase * 0.10 : 0;
        double precioFinal = precioBase + impuesto - descuento;
        return precioFinal;
    }
}
```
---

## 5. Reflexión Crítica

La elección entre estas herramientas refleja una decisión filosófica sobre el desarrollo. **JetBrains** ofrece un enfoque "de caja", donde la potencia y la seguridad son máximas, a un costo monetario y de recursos. **Visual Studio Code** representa la flexibilidad y la personalización, ideal para quienes construyen su propio entorno de trabajo. **Eclipse**, por su parte, es la opción robusta y probada para el desarrollo empresarial en Java, respaldada por una comunidad de larga trayectoria.

Cada herramienta tiene un público y un propósito definidos, y la "mejor" opción dependerá siempre de las necesidades específicas del proyecto y las preferencias del equipo.

---
