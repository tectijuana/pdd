# Refactorización de código orientado a datos en Python (Pandas/Numpy)

>**Mendoza Vilchis Isai**
>**21211996**

##  Presentación clara del tema asignado

La **refactorización de código orientado a datos** en Python consiste en mejorar la estructura interna del código sin cambiar su comportamiento externo. Este proceso es crucial cuando se trabaja con bibliotecas como **Pandas** y **NumPy**, que son ampliamente utilizadas en análisis de datos y cómputo numérico.

El objetivo principal es hacer que el código sea más **legible, eficiente y mantenible**, garantizando al mismo tiempo la consistencia de los resultados.

---

##  Uso de ejemplos o comparaciones prácticas

### Ejemplo inicial (sin refactorización)
```python
import pandas as pd

# Calcular el promedio de edades mayores de 30
df = pd.DataFrame({"nombre": ["Ana", "Luis", "Pedro"], "edad": [25, 35, 40]})
edades = df[df["edad"] > 30]["edad"]
promedio = sum(edades) / len(edades)
print(promedio)
```

### Ejemplo refactorizado
```python
import pandas as pd

df = pd.DataFrame({"nombre": ["Ana", "Luis", "Pedro"], "edad": [25, 35, 40]})
promedio = df.loc[df["edad"] > 30, "edad"].mean()
print(promedio)
```

En el segundo ejemplo, se usan directamente los métodos de **Pandas** (`.loc` y `.mean()`), lo que hace el código más claro, eficiente y menos propenso a errores.

---

##  Relación directa con refactorización, calidad o patrones

La refactorización en el contexto de Pandas/NumPy está directamente ligada a la **calidad del software** porque:

- Promueve **código limpio** siguiendo principios como *DRY (Don't Repeat Yourself)*.  
- Se apoya en **patrones de diseño** orientados a datos como *Vectorization*, que evita bucles explícitos.  
- Mejora la **mantenibilidad** y **escalabilidad** del proyecto, facilitando el trabajo en equipo.  

Ejemplo de vectorización en NumPy:

```python
import numpy as np

# Sin vectorización (menos eficiente)
valores = [1, 2, 3, 4, 5]
cuadrados = []
for v in valores:
    cuadrados.append(v**2)

# Con vectorización (más eficiente)
arr = np.array([1, 2, 3, 4, 5])
cuadrados_vec = arr**2
```
##  Conclusión

La refactorización de código orientado a datos en Python, especialmente con bibliotecas como **Pandas** y **NumPy**, es una práctica esencial para mejorar la **calidad, eficiencia y mantenibilidad** del software.  
Al aplicar patrones como la **vectorización** y el uso de métodos integrados, los desarrolladores pueden reducir la complejidad del código, minimizar errores y trabajar de manera más colaborativa.  

En el contexto del curso de **PDD (Patrones de Diseño y Desarrollo)**, esta práctica no solo optimiza el rendimiento técnico, sino que también refleja un enfoque disciplinado hacia el diseño de software.  
La refactorización, entonces, no es un proceso opcional, sino un componente clave en la formación de profesionales capaces de enfrentar proyectos de **gran escala** y **alta demanda de datos**, promoviendo buenas prácticas de programación y un pensamiento crítico en el desarrollo orientado a datos.
