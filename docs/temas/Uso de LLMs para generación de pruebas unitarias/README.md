# Uso de LLMs para la generación de pruebas unitarias
# Daniel Omar Gonzalez Martinez 21212342

La ingeniería de software moderna enfrenta un reto constante: garantizar la calidad del código mediante **pruebas unitarias** que aseguren el correcto funcionamiento de los módulos individuales, la escritura manual de estas pruebas suele ser una de las tareas más tediosas y costosas en tiempo para los desarrolladores. En este contexto surge una pregunta: ¿podemos utilizar **LLMs (Large Language Models)**, como ChatGPT o similares, para **automatizar la generación de pruebas unitarias** y mejorar la productividad sin perder calidad?  

---

## Presentación clara del tema

Un **LLM** es un modelo de lenguaje entrenado con grandes cantidades de datos de texto y código, capaz de **entender, generar y transformar fragmentos de software**. En el ámbito de las pruebas unitarias, los LLMs pueden:

- Leer un archivo de código fuente.
- Detectar clases, métodos y dependencias.
- Sugerir o generar automáticamente pruebas unitarias en frameworks como **JUnit (Java)**, **Pytest (Python)**, **xUnit (C#)**, entre otros.
- Optimizar pruebas existentes proponiendo mejores coberturas.

La generación automática de pruebas no es un concepto nuevo: existen herramientas como **EvoSuite** o **PITest**, pero los LLMs marcan la diferencia porque **razonan sobre la intención del código**, no solo sobre firmas de métodos o análisis estático. Esto abre la puerta a que los LLMs no solo sugieran pruebas, sino que incluso **expliquen por qué una prueba es necesaria**, lo que puede ayudar en la enseñanza y formación de estudiantes.  

Al integrarse en pipelines de CI/CD, los LLMs podrían convertirse en un **asistente continuo de calidad**, reduciendo el riesgo de fallas en producción.

---

## Ejemplos prácticos

Supongamos la siguiente clase sencilla en Java:

```java
public class Calculadora {
    public int suma(int a, int b) {
        return a + b;
    }
    
    public int divide(int a, int b) {
        if (b == 0) throw new IllegalArgumentException("División por cero");
        return a / b;
    }
}
```

Un LLM como ChatGPT puede generar automáticamente una prueba unitaria usando JUnit:

```java
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class CalculadoraTest {

    @Test
    void testSuma() {
        Calculadora calc = new Calculadora();
        assertEquals(5, calc.suma(2, 3));
    }

    @Test
    void testDivide() {
        Calculadora calc = new Calculadora();
        assertEquals(2, calc.divide(10, 5));
    }

    @Test
    void testDividePorCero() {
        Calculadora calc = new Calculadora();
        assertThrows(IllegalArgumentException.class, () -> calc.divide(10, 0));
    }
}
```

Lo interesante es que el LLM **no solo genera las pruebas** pues puede sugerir casos de borde
En comparación con una generación tradicional (basada en mutación o aleatoriedad), el LLM “comprende” las excepciones y el contexto del código.

---

## Relación con refactorización, calidad y patrones

- **Refactorización:**  
  Cuando se refactoriza un código (por ejemplo, aplicar el patrón **Singleton**) es necesario asegurar que los cambios no rompen la funcionalidad existente. Aquí, los LLMs pueden regenerar pruebas unitarias adaptadas a la nueva estructura del código.  

- **Calidad del software:**  
  La calidad se mide en parte por la **cobertura de pruebas**. Pueden sugerir pruebas adicionales para aumentar esa cobertura y detectar rutas de ejecución no consideradas por el programador.  

- **Patrones de diseño:**  
   Los patrones como **Factory**, **Observer** o **Decorator** requieren pruebas que validen las interacciones entre objetos. Un LLM puede analizar estas dependencias y proponer pruebas más ricas que verifiquen no solo valores de retorno, sino también **colaboración entre objetos**.  

Ejemplo:  
Si implementamos un **Observer**, el LLM puede sugerir pruebas donde varios observadores reaccionan a un mismo evento, algo que un programador que apenas empieza  podría olvidar.

---

## Originalidad y análisis crítico


- **Ventajas**  
  - Ahorra tiempo en la escritura de pruebas repetitivas.  
  - Sugiere casos de borde difíciles de anticipar.  
  - Facilita la enseñanza de buenas prácticas a estudiantes y juniors.  
  - Puede integrarse en pipelines CI/CD como soporte continuo.  
  - Mejora la documentación del proceso de pruebas, ya que puede **explicar las pruebas en lenguaje natural**.  

- **Limitaciones**  
  - Puede generar código incorrecto o no compilable (alucinaciones).  
  - Depende en gran medida de cómo se formule el *prompt*.  
  - Riesgo de sobreconfianza: los desarrolladores pueden confiar en exceso sin validar manualmente.  
  - Riesgos de propiedad intelectual: si el modelo fue entrenado con código público, puede generar fragmentos similares a repositorios existentes.  

Los LLMs no deben reemplazar al programador, sino **complementarlo**. La responsabilidad final de validar la calidad del software sigue recayendo en el humano.  

El valor de esta tecnología está en **mostrar cómo patrones y refactorizaciones pueden ser acompañados por pruebas generadas automáticamente**, agilizando el aprendizaje y la práctica. Además, enseña a los estudiantes a ser críticos frente a la tecnología: usarla, pero también detectar sus limitaciones.  

---

## Reflexión ética sobre el uso de LLMs

El uso de LLMs plantea preguntas éticas y profesionales:  
- ¿Debemos citar las herramientas cuando nos ayudan a escribir código? Sí, por transparencia académica.  
- ¿Es correcto confiar en un modelo sin validar? No, siempre se debe revisar críticamente.  
- ¿Podrían reemplazar el trabajo humano? Su mejor papel es como **asistente**.  
- ¿Qué pasa con la privacidad y seguridad? Si se sube código sensible a un LLM en la nube, puede haber riesgos de exposición.  

---

## Conclusión

Los **LLMs aplicados a pruebas unitarias** representan un avance  en la ingeniería de software. Son especialmente útiles en etapas de refactorización y al aplicar patrones de diseño, pues permiten mantener la calidad y reducir el tiempo invertido en tareas repetitivas.  

Deben usarse con sentido crítico: **revisar, validar y complementar**. Esta tecnología no solo facilita la práctica, sino que también introduce al estudiante a los debates actuales sobre ética, automatización y el futuro del desarrollo de software. Los LLMs invitan a reflexionar sobre el futuro del rol del ingeniero de software: ¿seguiremos escribiendo pruebas de forma manual dentro de 10 años, o serán completamente generadas y validadas por inteligencia artificial? La respuesta probablemente se encuentre en un punto intermedio: la colaboración entre humanos y máquinas.  
