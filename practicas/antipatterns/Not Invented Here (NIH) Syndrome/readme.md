# 🧠 Síndrome "Not Invented Here" (NIH)

Realizado por:

Montaño Zaragoza Marcos Ulises 21211998

## 📘 Introducción

El **Síndrome "Not Invented Here" (NIH)** es un fenómeno organizacional y psicológico que describe la **resistencia o rechazo a utilizar ideas, productos o soluciones desarrolladas externamente**, prefiriendo crear una alternativa propia, aunque sea menos eficiente o más costosa.  
Este comportamiento suele observarse en empresas tecnológicas, instituciones académicas o equipos de desarrollo que valoran excesivamente la innovación interna.

---

## 📚 Definición

El término **"Not Invented Here"** (traducido como *“No inventado aquí”*) se refiere a la **actitud negativa hacia soluciones externas**.  
Quienes padecen este síndrome consideran que las ideas creadas fuera de su grupo carecen de calidad, compatibilidad o valor, aunque en la práctica puedan ser óptimas.

> “Si no fue creado por nosotros, no es lo suficientemente bueno.”

---

## 🧩 Origen del término

El término **surgió en el ámbito tecnológico y de ingeniería en los años 70**, cuando empresas de software y hardware comenzaron a desarrollar soluciones propias en lugar de adoptar estándares o tecnologías externas.  
Su popularización se atribuye a los ingenieros de **Xerox PARC** y posteriormente fue analizado en profundidad por autores de gestión como **Henry Chesbrough** en su teoría de la **Innovación Abierta** (*Open Innovation*).

---

## ⚙️ Definición del Antipatrón

El **antipatrón NIH** se refiere a la tendencia de los desarrolladores o empresas a **no aceptar tecnologías o ideas externas**, bajo la creencia de que **solo las soluciones propias son confiables o de calidad**.

> En lugar de reutilizar componentes probados, el equipo opta por recrear su propia versión “casera”.

Esto contradice los principios de **reutilización de código**, **modularidad** y **eficiencia**, promovidos por las buenas prácticas de desarrollo de software.

---

## ⚙️ Causas del síndrome NIH

| Causa | Descripción |
|-------|--------------|
| **Orgullo profesional** | Creencia de que el propio equipo es superior a los demás. |
| **Falta de confianza externa** | Temor a depender de tecnologías o proveedores ajenos. |
| **Proteccionismo interno** | Deseo de mantener el control total sobre los procesos. |
| **Desconocimiento de alternativas** | Falta de investigación o apertura hacia soluciones existentes. |
| **Cultura organizacional cerrada** | Políticas o líderes que desincentivan la colaboración externa. |

---

## 💼 Ejemplos en la industria

1. **Microsoft (años 90):** prefería desarrollar internamente muchas herramientas en lugar de usar estándares abiertos, lo que generó problemas de compatibilidad.
2. **Apple:** ha sido criticada por su ecosistema cerrado, aunque esta estrategia también le ha permitido mantener control y calidad.
3. **Proyectos académicos o gubernamentales:** a menudo rehúsan usar software libre existente y desarrollan versiones propias con funciones similares.

---

## 🚀 Consecuencias

| Consecuencia | Impacto |
|---------------|----------|
| **Retrasos en proyectos** | Se pierde tiempo reinventando soluciones ya existentes. |
| **Innovación limitada** | Al evitar la colaboración, se reducen oportunidades de mejora. |
| **Aislamiento organizacional** | La empresa o grupo se desconecta del ecosistema tecnológico global. |
| **Desmotivación del personal** | Los equipos pueden sentirse frustrados al repetir trabajo innecesario. |
| **Reinvención constante** | Se crean herramientas internas que ya existen públicamente. |
| **Aislamiento tecnológico** | Se evita colaborar con proyectos o comunidades externas. |
| **Lentitud en desarrollo** | Se desperdicia tiempo y recursos reimplementando funciones. |
| **Sobrecosto** | Mayor inversión en mantenimiento y pruebas. |
| **Dificultad de integración** | El software propio no es compatible con estándares externos. |

---

## 💡 Estrategias para evitar el síndrome NIH

1. **Fomentar la innovación abierta:** adoptar ideas externas y combinar con recursos internos.  
2. **Evaluar objetivamente las soluciones externas:** usar métricas de calidad y costo-beneficio.  
3. **Promover la colaboración y el aprendizaje cruzado:** trabajar con universidades, startups o comunidades open source.  
4. **Reconocer el valor del conocimiento externo:** entender que la innovación no depende exclusivamente de la autoría.  
5. **Adoptar cultura de mejora continua:** donde lo importante es el resultado, no el origen.

---
## ❌ Ejemplo de *Bad Code* (Antipatrón NIH)

En este ejemplo, un programador evita usar la biblioteca estándar de Python para realizar una operación común: calcular la media de una lista.

### Código con NIH

```python
# Ejemplo del antipatrón "Not Invented Here"
def calcular_media(lista):
    suma = 0
    for i in range(len(lista)):
        suma += lista[i]
    return suma / len(lista)

datos = [10, 20, 30, 40]
print(calcular_media(datos))
```
## ❌ Problemas

- Reimplementa una función básica ya existente en librerías estándar.  
- No maneja excepciones (por ejemplo, lista vacía).  
- Duplica trabajo y aumenta mantenimiento.  

---

## ✅ Refactorización (Eliminando el NIH)

```python
# Versión refactorizada (usando soluciones externas y probadas)
import statistics

datos = [10, 20, 30, 40]
print(statistics.mean(datos))
```
## 🔬 Relación con la Innovación Abierta

El concepto de **Innovación Abierta (Open Innovation)**, introducido por Henry Chesbrough en 2003, se opone directamente al NIH.  
Esta filosofía sostiene que **las empresas deben aprovechar tanto ideas internas como externas** para acelerar su desarrollo y competitividad.  
La innovación abierta busca transformar la mentalidad del “no inventado aquí” por un **“orgullosamente adoptado de fuera”**.

---

## 🧭 Conclusión

El **Síndrome “Not Invented Here”** es una barrera cultural más que técnica.  
Su superación requiere **humildad intelectual, apertura al cambio y cooperación externa**.  
En un mundo interconectado, donde la información y la innovación fluyen a gran velocidad, **rechazar lo externo no solo es ineficiente, sino contraproducente**.  
Las organizaciones que adoptan una mentalidad colaborativa logran **mayor agilidad, competitividad y sostenibilidad** en el largo plazo.

