# Alvarado Cardona Antonio 22210279
# 🧩 Refactorización: Lógica de Descuentos (Código Espagueti → Strategy Pattern)

##  Código Espagueti Original

El siguiente código calcula descuentos para diferentes tipos de clientes utilizando **siete `if` anidados**, haciendo que el código sea difícil de leer, mantener y extender.

```csharp
using System;

namespace TiendaDescuentos
{
    public class CalculadoraDescuentos
    {
        public double CalcularDescuento(string tipoCliente, double totalCompra)
        {
            double descuento = 0;

            if (tipoCliente == "REGULAR")
            {
                if (totalCompra > 500)
                    descuento = totalCompra * 0.05;
                else
                    descuento = 0;
            }
            else if (tipoCliente == "PLATA")
            {
                if (totalCompra > 300)
                    descuento = totalCompra * 0.10;
                else
                    descuento = totalCompra * 0.05;
            }
            else if (tipoCliente == "ORO")
            {
                if (totalCompra > 500)
                    descuento = totalCompra * 0.15;
                else
                    descuento = totalCompra * 0.10;
            }
            else if (tipoCliente == "PLATINO")
            {
                if (totalCompra > 700)
                    descuento = totalCompra * 0.20;
                else
                    descuento = totalCompra * 0.15;
            }
            else if (tipoCliente == "VIP")
            {
                if (totalCompra > 1000)
                    descuento = totalCompra * 0.25;
                else
                    descuento = totalCompra * 0.20;
            }
            else if (tipoCliente == "NUEVO")
            {
                if (totalCompra > 200)
                    descuento = totalCompra * 0.02;
                else
                    descuento = 0;
            }
            else if (tipoCliente == "EMPLEADO")
            {
                descuento = totalCompra * 0.30;
            }
            else
            {
                descuento = 0;
            }

            return descuento;
        }
    }
}
```
# Problemas Detectados
| Tipo de Problema                             | Descripción                                                                  |
| -------------------------------------------- | ---------------------------------------------------------------------------- |
| **Código Espagueti**                         | Exceso de `if` y `else if` anidados. Difícil de leer y modificar.            |
| **Violación de OCP (Open/Closed Principle)** | Cada vez que se agrega un nuevo tipo de cliente, se debe modificar la clase. |
| **Falta de Cohesión**                        | La clase tiene múltiples comportamientos mezclados.                          |
| **Baja Mantenibilidad**                      | Pequeños cambios pueden generar errores en otras condiciones.                |
| **Difícil de Testear**                       | No hay separación entre las reglas de negocio.                               |

# Refactorización Usando Strategy Pattern

El Patrón Strategy permite definir una familia de algoritmos (en este caso, estrategias de descuento) y hacerlas intercambiables fácilmente, eliminando la dependencia de múltiples condicionales.

### 1. Interfaz Estrategia de Descuento
``` csharp
public interface IDescuentoStrategy
{
    double Calcular(double totalCompra);
}
```

### 2. Estrategias Concretas
``` csharp
public class DescuentoRegular : IDescuentoStrategy
{
    public double Calcular(double totalCompra)
    {
        return totalCompra > 500 ? totalCompra * 0.05 : 0;
    }
}

public class DescuentoPlata : IDescuentoStrategy
{
    public double Calcular(double totalCompra)
    {
        return totalCompra > 300 ? totalCompra * 0.10 : totalCompra * 0.05;
    }
}

public class DescuentoOro : IDescuentoStrategy
{
    public double Calcular(double totalCompra)
    {
        return totalCompra > 500 ? totalCompra * 0.15 : totalCompra * 0.10;
    }
}

public class DescuentoPlatino : IDescuentoStrategy
{
    public double Calcular(double totalCompra)
    {
        return totalCompra > 700 ? totalCompra * 0.20 : totalCompra * 0.15;
    }
}

public class DescuentoVIP : IDescuentoStrategy
{
    public double Calcular(double totalCompra)
    {
        return totalCompra > 1000 ? totalCompra * 0.25 : totalCompra * 0.20;
    }
}

public class DescuentoNuevo : IDescuentoStrategy
{
    public double Calcular(double totalCompra)
    {
        return totalCompra > 200 ? totalCompra * 0.02 : 0;
    }
}

public class DescuentoEmpleado : IDescuentoStrategy
{
    public double Calcular(double totalCompra)
    {
        return totalCompra * 0.30;
    }
}

public class SinDescuento : IDescuentoStrategy
{
    public double Calcular(double totalCompra) => 0;
}
```
### 3. Contexto (CalculadoraDescuentos)
``` csharp
using System;
using System.Collections.Generic;

namespace TiendaDescuentos
{
    public class CalculadoraDescuentos
    {
        private readonly Dictionary<string, IDescuentoStrategy> _estrategias;

        public CalculadoraDescuentos()
        {
            _estrategias = new Dictionary<string, IDescuentoStrategy>(StringComparer.OrdinalIgnoreCase)
            {
                { "REGULAR", new DescuentoRegular() },
                { "PLATA", new DescuentoPlata() },
                { "ORO", new DescuentoOro() },
                { "PLATINO", new DescuentoPlatino() },
                { "VIP", new DescuentoVIP() },
                { "NUEVO", new DescuentoNuevo() },
                { "EMPLEADO", new DescuentoEmpleado() }
            };
        }

        public double CalcularDescuento(string tipoCliente, double totalCompra)
        {
            var estrategia = _estrategias.ContainsKey(tipoCliente)
                ? _estrategias[tipoCliente]
                : new SinDescuento();

            return estrategia.Calcular(totalCompra);
        }
    }
}
```
### 4. Ejemplo de Uso
``` csharp
class Program
{
    static void Main()
    {
        var calculadora = new CalculadoraDescuentos();

        Console.WriteLine($"Cliente REGULAR: {calculadora.CalcularDescuento("REGULAR", 600)}");
        Console.WriteLine($"Cliente ORO: {calculadora.CalcularDescuento("ORO", 800)}");
        Console.WriteLine($"Cliente EMPLEADO: {calculadora.CalcularDescuento("EMPLEADO", 1000)}");
        Console.WriteLine($"Cliente DESCONOCIDO: {calculadora.CalcularDescuento("DESCONOCIDO", 400)}");
    }
}
```
# Beneficios del Patrón Strategy
Beneficio	Descripción
Extensible	Agregar un nuevo tipo de cliente solo requiere una nueva clase que implemente IDescuentoStrategy.
Mantenible	Cada lógica de descuento está aislada.
Cumple con OCP	No es necesario modificar el código existente para añadir nuevos descuentos.
Reutilizable y testeable	Las estrategias pueden probarse individualmente.
# Alternativa: Chain of Responsibility

Si las condiciones fueran dependientes (por ejemplo, varios descuentos acumulables o verificaciones en cascada), Chain of Responsibility sería más adecuado, permitiendo que varias reglas se apliquen secuencialmente.

En este caso, como solo se aplica una regla por tipo de cliente, Strategy Pattern es la mejor elección.

# Conclusión

- Antes: Código rígido, difícil de mantener, con 7 if anidados.
- Después: Sistema flexible basado en estrategias intercambiables.
- Patrón aplicado: Strategy (aunque Chain of Responsibility sería viable en otros escenarios).
