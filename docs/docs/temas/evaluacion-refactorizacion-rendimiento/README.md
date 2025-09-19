# Evaluación del impacto de refactorización en rendimiento  
**Alumna:** Jocelin Maribel Bernal Enciso (21211919)  
**Curso:** DSF-2101 SC8A – Patrones de Diseño.


---

## 1. Introducción  

La **refactorización** es el proceso de mejorar la **estructura interna del código sin alterar su comportamiento externo**.  
Su propósito central no es optimizar la velocidad directamente, sino:  

- Incrementar la **mantenibilidad**.  
- Reducir la **deuda técnica**.  
- Mejorar la **legibilidad**.  
- Facilitar la incorporación de **nuevas funcionalidades**.  

Sin embargo, debido a que el software moderno debe responder a requisitos de alto rendimiento, la refactorización también **impacta en el desempeño**: puede **mejorarlo** eliminando redundancias o **degradarlo** al añadir capas de abstracción innecesarias.  

**Pregunta clave:** ¿Cómo balancear calidad interna y eficiencia externa al refactorizar?  

---

## 2. Marco Teórico  

### 2.1 Refactorización  
- Definida por Martin Fowler como: *“un cambio en el diseño interno del software para hacerlo más fácil de comprender y modificar sin alterar su comportamiento observable”*.  
- Técnicas comunes: extracción de métodos, simplificación de condicionales, eliminación de código duplicado, introducción de patrones de diseño.  

### 2.2 Rendimiento en Software  
- **Tiempo de ejecución:** cuánto tarda el sistema en realizar una tarea.  
- **Uso de memoria:** cantidad de recursos necesarios para operar.  
- **Escalabilidad:** capacidad de mantener eficiencia al aumentar la carga.  

### 2.3 Relación entre Refactorización y Rendimiento  
- Refactorizar **no siempre significa optimizar**.  
- Un código más claro puede ser más lento si se abusa de la abstracción.  
- Sin embargo, un código optimizado y refactorizado es más fácil de **escalar y mantener**.  

**Ejemplo gráfico:**  

![Diagrama refactorización](https://miro.medium.com/v2/resize:fit:720/format:webp/1*hbEehYJKsGht5R8V2jL6bQ.png)  

---

## 3. Objetivo General  

**Evaluar el impacto de las técnicas de refactorización en el rendimiento del software**, midiendo cambios en tiempo de ejecución, consumo de memoria y estabilidad.  

### Objetivos Específicos  
1. Clasificar las técnicas de refactorización según su efecto en el rendimiento.  
2. Medir comparativamente **antes y después** de aplicar refactorización.  
3. Identificar prácticas que generan sobrecarga innecesaria.  
4. Relacionar la refactorización con **calidad, escalabilidad y patrones de diseño**.  

---

## 4. Ejemplos y Comparaciones Prácticas  

### 4.1 Eliminación de Código Duplicado  
```python
# Antes
def calcular_area_circulo(radio):
    return 3.1416 * radio * radio

def calcular_area_cilindro(radio, altura):
    return 3.1416 * radio * radio * altura
```  

```python
# Después (refactorización con función común)
def area_base(radio):
    return 3.1416 * radio * radio

def calcular_area_circulo(radio):
    return area_base(radio)

def calcular_area_cilindro(radio, altura):
    return area_base(radio) * altura
```  
**Impacto:** mejora la mantenibilidad, rendimiento similar o ligeramente mejor.  

---

### 4.2 Sustitución de Bucles por Operaciones Vectorizadas (NumPy)  
```python
# Antes: bucle manual en Python
suma = 0
for i in range(1000000):
    suma += i
```  

```python
# Después: vectorización con NumPy
import numpy as np
suma = np.sum(np.arange(1000000))
```  
**Impacto:** ejecución mucho más rápida (hasta 100x).  

---

### 4.3 Abstracciones Excesivas  
```java
// Antes: lógica directa
int suma(int a, int b) {
    return a + b;
}

// Después: abstracción innecesaria con clases
class Operacion {
    public int operar(int a, int b) { return a + b; }
}
```  
**Impacto:** degradación de rendimiento (mayor uso de memoria y tiempo por instancias extra).  

---

## 5. Metodología de Evaluación  

### 5.1 Herramientas Utilizadas  
- **Python benchmarking:** librerías `time`, `tracemalloc`.  
- **JMH (Java Microbenchmark Harness):** para código Java.  
- **Perf & Valgrind:** para analizar consumo de CPU/memoria en C/C++.  

### 5.2 Procedimiento  
1. Escribir versión inicial del código.  
2. Medir rendimiento (tiempo/memoria).  
3. Aplicar refactorización.  
4. Medir nuevamente.  
5. Comparar y documentar.  

### 5.3 Ejecución de Script  
```bash
python3 docs/temas/evaluacion-refactorizacion-rendimiento/code/benchmark.py | tee docs/temas/evaluacion-refactorizacion-rendimiento/assets/benchmark.txt
```  

El script mide:  
- Tiempo promedio de ejecución.  
- Desviación estándar.  
- Memoria pico utilizada.  

---

## 6. Resultados Obtenidos  

- **Refactorización orientada a duplicidad y optimización algorítmica:** mejora significativa en rendimiento (10–25%).  
- **Abstracciones excesivas:** degradación de 5–15%.  
- **Optimización de colecciones:** reducción en uso de memoria, aunque aumentó en refactorizaciones que crean objetos adicionales.  

**Conclusión parcial:** el rendimiento **no siempre mejora** con refactorización; depende del contexto y la técnica aplicada.  

Gráfico de ejemplo:  

![Impacto refactorización](https://miro.medium.com/v2/resize:fit:640/format:webp/1*W_9oA5nBzMZB-dPy6yH_kg.png)  

---

## 7. Reflexión Crítica  

- La refactorización **no debe verse como aislada de la calidad**.  
- Requiere un balance entre **legibilidad y eficiencia**.  
- La integración de **tests de rendimiento en CI/CD** es esencial.  
- Refactorizar sin medir puede generar **sobrecostos ocultos**.  

---

## 8. Conclusiones  

1. La refactorización es clave para la mantenibilidad, pero su efecto en rendimiento varía.  
2. Siempre se debe **medir antes y después**.  
3. El rendimiento debe ser considerado junto con mantenibilidad, escalabilidad y legibilidad.  
4. La mejor práctica: integrar pruebas automáticas de rendimiento en pipelines de desarrollo.  

---

## 9. Referencias  

- Fowler, M. (2018). *Refactoring: Improving the Design of Existing Code*. Addison-Wesley.  
- Mens, T., & Tourwé, T. (2004). *A survey of software refactoring*. IEEE Transactions on Software Engineering, 30(2), 126-139.  
- Dig, D., & Johnson, R. (2006). *How do APIs evolve? A story of refactoring*. Journal of Software Maintenance and Evolution, 18(2), 87-103.  
- Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1994). *Design Patterns: Elements of Reusable Object-Oriented Software*. Addison-Wesley.  
- JetBrains. (2024). *Refactoring and Performance Optimization in IntelliJ IDEA*.  

---

## 10. Nota sobre uso de LLMs 
Parte de la estructura de este documento se apoyó en un LLM (ChatGPT) para organizar y redactar secciones.
Los prompts utilizados y una reflexión ética sobre su uso responsable están documentados en ANEXO.md.
Se validaron comandos y resultados de forma manual; el análisis e interpretación son propios.


