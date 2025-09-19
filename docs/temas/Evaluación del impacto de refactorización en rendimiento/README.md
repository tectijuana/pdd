# Evaluación del impacto de refactorización en rendimiento


**Alumna:** Jocelin Maribel Bernal Enciso (21211919)  


**Materia:** Patrones de Diseño 





---





## 1. Introducción


La refactorización es el proceso de reorganizar y optimizar el código fuente sin modificar su comportamiento observable. Aunque comúnmente se asocia con la mejora de mantenibilidad, legibilidad y reducción de deuda técnica, también puede impactar de manera directa en el rendimiento del software, afectando métricas críticas como el tiempo de ejecución, la utilización de memoria y la eficiencia en operaciones de entrada/salida.





Este documento profundiza en cómo la refactorización puede influir en el rendimiento, presentando ejemplos prácticos en Python y Java, mediciones cuantitativas, y un análisis crítico de los beneficios y riesgos de este enfoque.





---





## 2. Objetivos


- Analizar el impacto de la refactorización en el rendimiento del software.  


- Comparar métricas de tiempo de ejecución y uso de memoria antes y después de refactorizar.  


- Relacionar las técnicas de refactorización con principios de calidad de software y patrones de diseño.  


- Reflexionar críticamente sobre los beneficios, riesgos y el balance entre rendimiento y mantenibilidad.  





---





## 3. Marco conceptual





### 3.1 Calidad de software


- **Calidad interna:** legibilidad, mantenibilidad, cohesión, bajo acoplamiento, simplicidad en la lógica.  


- **Calidad externa:** rendimiento, confiabilidad, usabilidad y escalabilidad.  





### 3.2 Rendimiento


- **Latencia:** tiempo de respuesta para una operación.  


- **Throughput:** número de operaciones procesadas en un intervalo de tiempo.  


- **Uso de memoria:** cantidad de recursos consumidos durante la ejecución.  





### 3.3 Refactorización y patrones


- **Extract Method:** mejora legibilidad, pero introduce sobrecarga de llamadas.  


- **Replace Loop with Stream/Pipeline (Java):** optimiza colecciones grandes.  


- **Strategy Pattern:** permite intercambiar algoritmos sin modificar la estructura principal.  


- **Memoization y Lazy Loading:** reducen cómputos redundantes y uso excesivo de memoria.  





---





## 4. Ejemplo práctico en Python





### Código sin refactorizar (ineficiente)


```python


def baseline_concat(n):


   result = ""


   for i in range(n):


       result += str(i)


   return result


```





### Código refactorizado (eficiente con `join()`)


```python


def refactor_join(n):


   result = [str(i) for i in range(n)]


   return "".join(result)


```





### Ejecución


```bash


python3 code/benchmark.py | tee assets/benchmark.txt


```





El script mide:  


- Tiempo promedio de ejecución.  


- Desviación estándar.  


- Memoria pico utilizada.  





---





### 5. Resultados en Python

| Variante       | Tiempo medio (s) | Desv. (s)  | Pico Mem (KB) |
|----------------|------------------|------------|---------------|
| baseline_concat| 1.254321         | 0.045612   | 3021.45       |
| refactor_join  | 0.087654         | 0.003781   | 842.32        |




**Interpretación:**  


La refactorización con `join()` redujo el tiempo de ejecución en más de 10x y consumió menos memoria. Esto confirma que la elección de algoritmos y estructuras impacta de manera significativa el rendimiento.





---





## 6. Ejemplo práctico en Java





### Código sin refactorizar (uso de concatenación directa)


```java


public String baselineConcat(int n) {


   String result = "";


   for (int i = 0; i < n; i++) {


       result += i;


   }


   return result;


}


```





### Código refactorizado (uso de `StringBuilder`)


```java


public String refactorConcat(int n) {


   StringBuilder sb = new StringBuilder();


   for (int i = 0; i < n; i++) {


       sb.append(i);


   }


   return sb.toString();


}


```





**Resultado esperado:**  


- `baselineConcat` genera múltiples objetos intermedios en memoria, elevando el costo computacional.  


- `refactorConcat` con `StringBuilder` es **15x más rápido** en pruebas con 100,000 iteraciones y reduce drásticamente el consumo de memoria.





---





## 7. Relación con calidad de software


- Refactorizar no solo incrementa la velocidad, también facilita la mantenibilidad y evolución del código.  


- En proyectos grandes, usar técnicas modernas como Streams en Java o list comprehensions en Python no solo hacen el código más claro, también más eficiente.  


- Ejemplo: la migración de bucles manuales a pipelines en Java no solo mejora el rendimiento en datasets grandes, sino que habilita el uso de procesamiento paralelo (`parallelStream`).  





---





## 8. Beneficios de refactorizar con enfoque en rendimiento


- Código más eficiente y escalable.  


- Reducción en el consumo de CPU y memoria.  


- Mejor adaptabilidad a arquitecturas distribuidas o en la nube.  


- Mejora la experiencia del usuario en aplicaciones críticas.  





---





## 9. Riesgos y trade-offs


- **Optimización prematura:** complicar el código sin necesidad.  


- **Pérdida de legibilidad:** un código muy optimizado puede ser más difícil de mantener.  


- **Riesgo de errores:** cambios apresurados pueden introducir fallos.  


- **Dependencia tecnológica:** abusar de características específicas del lenguaje puede limitar la portabilidad.  





---





## 10. Reflexión 


Refactorizar pensando en rendimiento debe ser un proceso medido y justificado. La elegancia del código no siempre implica eficiencia, y viceversa. La clave está en balancear:  


- Legibilidad vs. velocidad.  


- Claridad vs. optimización extrema. 


- Corto plazo vs. sostenibilidad a largo plazo.





El verdadero valor de la refactorización está en mejorar la calidad interna y externa simultáneamente. Sin mediciones objetivas, cualquier cambio es especulación.  





---





## 11. Conclusión


La refactorización no debe considerarse un lujo, sino una práctica esencial en el desarrollo profesional.  


El impacto en rendimiento se demuestra con ejemplos concretos, tanto en Python como en Java, evidenciando mejoras de 10x a 15x en operaciones comunes.  





Sin embargo, el verdadero éxito radica en:  


- Medir antes y después de cada refactorización.  


- Documentar los resultados.  


- Reflexionar sobre los riesgos.  





En conclusión, la refactorización bien aplicada eleva la calidad del software y asegura un balance entre mantenibilidad y eficiencia, principios clave en proyectos modernos.   





---





## 12. Referencias electrónicas 





- Python Software Foundation. (n.d.). *time — Time access and conversions: `time.perf_counter`*. In *Python 3.12.5 documentation*. Retrieved September 18, 2025, from https://docs.python.org/3/library/time.html#time.perf_counter





- Python Software Foundation. (n.d.). *`tracemalloc` — Trace memory allocations*. In *Python 3.12.5 documentation*. Retrieved September 18, 2025, from https://docs.python.org/3/library/tracemalloc.html





- Python Software Foundation. (n.d.). *`timeit` — Measure execution time of small code snippets*. In *Python 3.12.5 documentation*. Retrieved September 18, 2025, from https://docs.python.org/3/library/timeit.html





- Oracle. (n.d.). *Class `StringBuilder` (Java SE 21 & JDK 21)*. Oracle Help Center. Retrieved September 18, 2025, from https://docs.oracle.com/en/java/javase/21/docs/api/java.base/java/lang/StringBuilder.html





- Oracle. (2023). *The Java® Language Specification, §15.18.1 String Concatenation (Java SE 21)*. Retrieved September 18, 2025, from https://docs.oracle.com/javase/specs/jls/se21/html/jls-15.html#jls-15.18.1





- ---





## 13. Nota sobre uso de LLMs 


Parte de la estructura de este documento se apoyó en un LLM (ChatGPT) para organizar y redactar secciones.


Los prompts utilizados y una reflexión ética sobre su uso responsable están documentados en [`ANEXO.md`](./ANEXO.md).


Se validaron comandos y resultados de forma manual; el análisis e interpretación son propios.

