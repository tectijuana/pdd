# Uso de LLMs para revisión de código automatizada

Los Modelos de Lenguaje de Código Grande (LLM) demuestran una gran versatilidad para adaptarse a diversas tareas posteriores, como la generación y finalización de código, así como la detección y corrección de errores. Sin embargo, los LLM de Código a menudo no logran capturar los estándares de codificación existentes, lo que genera código que entra en conflicto con los patrones de diseño requeridos para un proyecto determinado. Como resultado, los desarrolladores deben realizar un posprocesamiento para adaptar el código generado a las normas de diseño del proyecto.

##  Introducción 
Los Modelos de Lenguaje de Gran Escala (LLMs) están transformando los paradigmas tradicionales en múltiples campos. En particular, los LLMs de código, entrenados con grandes volúmenes de datos, muestran una gran versatilidad en tareas relacionadas con el desarrollo de software. Pueden asistir en la generación y finalización de código, la localización y corrección de errores, así como en la detección y mejora de aspectos de seguridad, lo que incrementa de manera notable la productividad de los desarrolladores.

No obstante, todavía existen desafíos importantes para su aplicación en escenarios reales de ingeniería de software. En proyectos con repositorios complejos, resulta difícil extraer los conocimientos relevantes de una gran cantidad de información y adaptarse a las convenciones específicas de cada equipo. Además, estos modelos suelen mostrar limitaciones para comprender patrones de diseño y estilos de codificación propios de un proyecto, lo que puede producir código inconsistente o poco alineado con los requisitos, generando una carga adicional en revisiones y correcciones.

La mayoría de estudios recientes se han centrado en evaluar a los LLMs de código en tareas como generación, detección de errores o resumen, empleando conjuntos de referencia como CodeXGLUE. Si bien estos análisis han permitido conocer la precisión y el entendimiento de contexto de los modelos, han dejado en segundo plano el papel de los patrones de diseño, fundamentales para mantener la calidad del software en el desarrollo orientado a objetos. En este sentido, resulta esencial considerar también los posibles sesgos que los LLMs pueden presentar al trabajar con distintos patrones, pues estos influyen directamente en la fiabilidad de los resultados.

Ante esta situación, un enfoque de investigación clave es evaluar la capacidad de los LLMs para reconocer y generar código siguiendo patrones de diseño establecidos, como Singleton o Factory. Esto implica probar su rendimiento en clasificación, generación y comprensión de patrones, mediante experimentos que van desde la identificación de patrones en archivos de código hasta la generación de funciones con o sin información previa del patrón. A partir de estas evaluaciones, es posible analizar tanto las fortalezas como las limitaciones actuales de los LLMs en el manejo de patrones de diseño dentro de la ingeniería de software.
 

---

##  La llegada de los LLMs 
- **Large Language Models (LLMs)** entrenados en código y lenguaje natural.
- Ventajas sobre herramientas tradicionales:
  - Analizan el **contexto** del código, no solo la sintaxis.
  - Detectan **errores lógicos y de seguridad**.
  - Sugieren **optimizaciones** y mejores prácticas.
  - Explican sus recomendaciones, favoreciendo el aprendizaje.

---

##  Beneficios de la Revisión con LLMs 
1. **Velocidad** → Retroalimentación en segundos.
2. **Consistencia** → Eliminan sesgos y cansancio humano.
3. **Escalabilidad** → Revisan múltiples MR/PR al mismo tiempo.
4. **Mejor experiencia de desarrollador** → Menos tareas repetitivas, más enfoque en resolver problemas complejos.

---

## Funcionamiento 
1. **Integración con CI/CD** (GitHub, GitLab, Bitbucket).
2. **Activación automática** al crear o actualizar un Merge Request.
3. **Análisis del código**:
   - Calidad
   - Seguridad
   - Rendimiento
   - Estilo y buenas prácticas
4. **Retroalimentación en línea** dentro del MR/PR.

---

## Ventajas
- **Precisión y uniformidad** → Reducción de errores humanos.
- **Soporte multilenguaje** → JS, Python, Java, Go, C#, etc.
- **Ciclos de desarrollo más rápidos** → Menos tiempo de espera.
- **Mejor colaboración** → Feedback objetivo y neutral.
- **Menos deuda técnica** → Detección temprana de problemas.

---

## Impacto en los Equipos 
- Mayor satisfacción de los desarrolladores.
- Reducción del **burnout**.
- Trabajo más colaborativo y menos fricción.
- Cultura de desarrollo más ágil y eficiente.

---

## Comparación práctica

Ejemplo: “Un revisor humano puede pasar por alto un método duplicado en diferentes clases, mientras que un LLM puede sugerir extraer un patrón Factory o aplicar refactorización Extract Method para reducir duplicidad.”

Ejemplo: “Al detectar un if-else muy complejo, el LLM puede sugerir reemplazarlo por el patrón Strategy, mejorando legibilidad y mantenimiento.”

### Refactorización de Procesador de Pagos con Patrón Strategy

### Código Inicial (sin refactorizar)

Un desarrollador implementa los pagos usando `if-else`:

```csharp
public class ProcesadorPagos
{
    public void Procesar(string tipoPago, double monto)
    {
        if (tipoPago == "Tarjeta")
        {
            Console.WriteLine($"Procesando pago con tarjeta por ${monto}");
        }
        else if (tipoPago == "Efectivo")
        {
            Console.WriteLine($"Procesando pago en efectivo por ${monto}");
        }
        else if (tipoPago == "Transferencia")
        {
            Console.WriteLine($"Procesando transferencia por ${monto}");
        }
        else if (tipoPago == "QR")
        {
            Console.WriteLine($"Procesando pago con código QR por ${monto}");
        }
        else
        {
            Console.WriteLine("Método de pago no soportado");
        }
    }
}
```
### Refactorización de Métodos de Pago usando Patrón Strategy

### Problemas detectados

- Uso excesivo de `if-else`.
- Difícil de mantener: cada nuevo método de pago obliga a modificar la clase.
- No aplica principios SOLID ni patrones de diseño.

### Refactorización sugerida por un LLM → Patrón Strategy

Un LLM analiza el código y propone aplicar **Strategy**:

- Crear una interfaz `IPago`.
- Implementar una clase por cada tipo de pago.
- Usar un contexto (`ProcesadorPagos`) que delegue la acción.

---

## Interfaz

```csharp
public interface IPago
{
    void Procesar(double monto);
}
```
# Implementaciones concretas

```csharp
public class PagoTarjeta : IPago
{
    public void Procesar(double monto)
    {
        Console.WriteLine($"Procesando pago con tarjeta por ${monto}");
    }
}

public class PagoEfectivo : IPago
{
    public void Procesar(double monto)
    {
        Console.WriteLine($"Procesando pago en efectivo por ${monto}");
    }
}

public class PagoTransferencia : IPago
{
    public void Procesar(double monto)
    {
        Console.WriteLine($"Procesando transferencia por ${monto}");
    }
}

public class PagoQR : IPago
{
    public void Procesar(double monto)
    {
        Console.WriteLine($"Procesando pago con código QR por ${monto}");
    }
}
```

### Clase Contexto

```csharp
public class ProcesadorPagos
{
    private IPago _metodoPago;

    public ProcesadorPagos(IPago metodoPago)
    {
        _metodoPago = metodoPago;
    }

    public void ProcesarPago(double monto)
    {
        _metodoPago.Procesar(monto);
    }
}
```
### Uso
```csharp
class Program
{
    static void Main(string[] args)
    {
        ProcesadorPagos procesador1 = new ProcesadorPagos(new PagoTarjeta());
        procesador1.ProcesarPago(100);

        ProcesadorPagos procesador2 = new ProcesadorPagos(new PagoEfectivo());
        procesador2.ProcesarPago(50);

        ProcesadorPagos procesador3 = new ProcesadorPagos(new PagoQR());
        procesador3.ProcesarPago(75);
    }
}
```
### Resultado

- El código es más limpio y mantenible.
- Se cumple con el principio abierto/cerrado (OCP): se pueden añadir nuevos métodos de pago sin modificar la clase principal.
- Se aplica un patrón de diseño (Strategy).
- Un LLM podría detectar el mal uso de `if-else` y sugerir este cambio automáticamente, acelerando la refactorización y mejorando la calidad del software.

##  Casos de Éxito 
- **Fintech**: -40% en defectos de seguridad.
- **SaaS startup**: -30% en tiempo de refactorización.
- **DevOps enterprise**: -50% en bugs post-lanzamiento.

---

##  Futuro de la Revisión con LLMs 
- Comprensión más profunda del contexto del proyecto.
- Feedback **personalizado** para cada desarrollador.
- Análisis **predictivo y preventivo** (detectar problemas antes de que ocurran).
- Integración directa con editores para sugerencias en tiempo real.

---

##  Conclusión 
El uso de LLMs para la revisión de código automatizada representa un avance significativo en la forma en que los equipos de desarrollo garantizan la calidad de sus proyectos. Estas herramientas no buscan reemplazar la revisión humana, sino complementarla, aportando velocidad, consistencia y escalabilidad en la detección de errores y la aplicación de buenas prácticas

---

