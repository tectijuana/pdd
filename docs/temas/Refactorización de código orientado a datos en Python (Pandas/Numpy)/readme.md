# Refactorización de código orientado a datos en Python (Pandas/Numpy)

## 1. Presentación clara del tema asignado

La **refactorización de código orientado a datos** en Python consiste en mejorar la estructura interna del código sin cambiar su comportamiento externo. Este proceso es crucial cuando se trabaja con bibliotecas como **Pandas** y **NumPy**, que son ampliamente utilizadas en análisis de datos y cómputo numérico.

El objetivo principal es hacer que el código sea más **legible, eficiente y mantenible**, garantizando al mismo tiempo la consistencia de los resultados.

---

## 2. Uso de ejemplos o comparaciones prácticas

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

## 3. Relación directa con refactorización, calidad o patrones

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

---

## 4. Originalidad, análisis y reflexión crítica (enfocado a PDD)

En el curso de **PDD (Patrones de Diseño y Desarrollo)**, la refactorización con Pandas/NumPy se puede analizar bajo dos ángulos:

1. **Diseño orientado a eficiencia**: usar operaciones vectorizadas no es solo una optimización, sino un patrón recurrente que prioriza la legibilidad y el rendimiento.  
2. **Diseño para la colaboración**: equipos que trabajan con grandes volúmenes de datos se benefician al tener código más declarativo y entendible.  
3. **Crítica**: muchas veces los estudiantes aprenden a programar con bucles y estructuras básicas, pero no se fomenta desde el inicio el pensamiento vectorizado y orientado a datos, lo que puede retrasar su madurez como desarrolladores de software.  

Esto sugiere que la enseñanza debería incorporar más ejemplos de **patrones de refactorización en análisis de datos**, no solo en programación orientada a objetos.

---

## 5. Referencias (formato APA)

- McKinney, W. (2017). *Python for Data Analysis: Data Wrangling with Pandas, NumPy, and IPython*. O’Reilly Media. (pp. 45-60)  
- Van Rossum, G., & Drake, F. L. (2009). *The Python Language Reference Manual*. Network Theory Ltd.  
- Fowler, M. (2018). *Refactoring: Improving the Design of Existing Code*. Addison-Wesley Professional. (pp. 35-55)  
- Oliphant, T. E. (2015). *A Guide to NumPy*. Continuum Press. (pp. 102-120)  
