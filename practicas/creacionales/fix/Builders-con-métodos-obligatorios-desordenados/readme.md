# Builders con métodos obligatorios desordenados
---

## Builders
Builder es un patrón de diseño creacional que nos permite construir objetos complejos paso a paso. El patrón nos permite producir distintos tipos y representaciones de un objeto empleando el mismo código de construcción.

---
## Problema

Este codigo simula el uso de un Builder para crear órdenes de compra en una tienda, pero con un mal diseño: los métodos obligatorios se pueden invocar en cualquier orden, dejando objetos incompletos o inválidos.

## Codigo
``` c#
using System;
using System.Collections.Generic;

namespace StoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new OrdenBuilder();
            var orden = builder
                .WithTotal(200) // ❌ se puede antes de productos
                .WithCliente("Juan")
                .WithProductos(new List<string> { "Laptop", "Mouse" })
                .Build();

            Console.WriteLine($"Orden creada: Cliente={orden.Cliente}, Total={orden.Total}");
        }
    }

    public class Orden
    {
        public string Id { get; set; }
        public string Cliente { get; set; }
        public List<string> Productos { get; set; }
        public decimal Total { get; set; }
    }

    public class OrdenBuilder
    {
        private Orden orden = new Orden();

        public OrdenBuilder WithId(string id)
        {
            orden.Id = id;
            return this;
        }

        public OrdenBuilder WithCliente(string cliente)
        {
            orden.Cliente = cliente;
            return this;
        }

        public OrdenBuilder WithProductos(List<string> productos)
        {
            orden.Productos = productos;
            return this;
        }

        public OrdenBuilder WithTotal(decimal total)
        {
            orden.Total = total;
            return this;
        }

        public Orden Build()
        {
            return orden; // ❌ sin validación, puede estar incompleta
        }
    }
}
```
El problema principal del código es que el `OrdenBuilder` permite construir objetos incompletos o inconsistentes, porque:

* No hay control de obligatoriedad ni orden

  * Se puede llamar a `WithTotal(200)` antes de definir cliente o productos.

  * Incluso se puede omitir algún campo importante (ej. no asignar `Cliente`).

* `Build()` no valida nada

  * Devuelve la instancia de `Orden` aunque falten datos obligatorios.

  * Esto rompe la idea de un Builder seguro y puede llevar a errores lógicos más adelante (ej. procesar una orden sin cliente o sin productos).

## Aplicacion del builder correctamente

``` C#
using System;
using System.Collections.Generic;

namespace StoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // ❌ Intento con datos faltantes (sin Cliente)
                var ordenInvalida = new OrdenBuilder()
                    .WithProductos(new List<string> { "Laptop", "Mouse" })
                    .WithTotal(200)
                    .Build();

                Console.WriteLine($"Orden creada: Cliente={ordenInvalida.Cliente}, Total={ordenInvalida.Total}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error al crear la orden: {ex.Message}");
            }

            try
            {
                // ❌ Intento con datos faltantes (sin Productos)
                var ordenInvalida2 = new OrdenBuilder()
                    .WithCliente("Ana")
                    .WithTotal(500)
                    .Build();

                Console.WriteLine($"Orden creada: Cliente={ordenInvalida2.Cliente}, Total={ordenInvalida2.Total}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error al crear la orden: {ex.Message}");
            }

            try
            {
                // ✅ Intento con datos correctos
                var ordenValida = new OrdenBuilder()
                    .WithCliente("Juan")
                    .WithProductos(new List<string> { "Laptop", "Mouse" })
                    .WithTotal(200)
                    .Build();

                Console.WriteLine($"Orden creada: Cliente={ordenValida.Cliente}, Total={ordenValida.Total}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error al crear la orden: {ex.Message}");
            }
        }
    }

    public class Orden
    {
        public string Id { get; set; }
        public string Cliente { get; set; }
        public List<string> Productos { get; set; }
        public decimal Total { get; set; }
    }

    public class OrdenBuilder
    {
        private Orden orden = new Orden();

        public OrdenBuilder WithId(string id)
        {
            orden.Id = id;
            return this;
        }

        public OrdenBuilder WithCliente(string cliente)
        {
            orden.Cliente = cliente;
            return this;
        }

        public OrdenBuilder WithProductos(List<string> productos)
        {
            orden.Productos = productos;
            return this;
        }

        public OrdenBuilder WithTotal(decimal total)
        {
            orden.Total = total;
            return this;
        }

        public Orden Build()
        {
            if (string.IsNullOrEmpty(orden.Cliente))
                throw new InvalidOperationException("El cliente es obligatorio.");
            if (orden.Productos == null || orden.Productos.Count == 0)
                throw new InvalidOperationException("Debe incluir al menos un producto.");
            if (orden.Total <= 0)
                throw new InvalidOperationException("El total debe ser mayor que cero.");

            return orden;
        }
    }
}

```
### 🔍 Problema corregido

* El builder ya no permite construir órdenes inválidas.

  <img width="465" height="57" alt="image" src="https://github.com/user-attachments/assets/8bce2815-e1aa-4e92-ab3e-f7929c590008" />


* Si faltan datos obligatorios, `Build()` lanza una excepción clara.
<img width="751" height="55" alt="image" src="https://github.com/user-attachments/assets/191abb7c-4916-4bfc-a5f7-2a7585b121e6" />

### 🛠 Solución aplicada

* Validación en el método `Build()`

  * Se añadieron validaciones para asegurar que los campos obligatorios estén completos antes de construir la instancia.

  * Si faltan datos, se lanza una excepción (`InvalidOperationException`).

* Manejo de errores con `try/catch`

  * Se encapsuló la construcción de órdenes dentro de bloques `try/catch`.

  * Esto evita que el programa se detenga al encontrar datos faltantes y permite mostrar mensajes de error claros.

### 💡 Justificación técnica

* Principio de Responsabilidad Única (SRP):
La validación de la construcción se concentra en el `Builder`, manteniendo la clase `Orden` libre de lógica extra.

* Robustez y resiliencia:
El uso de `try/catch` evita que el programa falle ante entradas inválidas, mejorando la estabilidad general.

* Testabilidad:
Es posible probar distintos escenarios (válidos e inválidos) de manera controlada, verificando que el builder se comporte de forma predecible.

* Flexibilidad ante cambios:
Si mañana se agregan nuevos campos obligatorios (ej. dirección de envío), solo se debe actualizar el `Builder` sin afectar la clase `Orden`.

<img width="442" height="772" alt="image" src="https://github.com/user-attachments/assets/966c880c-e5e6-489d-b565-401b0690aef8" />
