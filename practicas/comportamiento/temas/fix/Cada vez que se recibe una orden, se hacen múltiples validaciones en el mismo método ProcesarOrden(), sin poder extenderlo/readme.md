## Datos del Alumno
#### Nombre: Emmanuel Isai Chavez Hernandez
#### No. Control: 23211005
#### Materia: Patrones de Diseño
#### Fecha: 7/10/2025

# Cada vez que se recibe una orden, se hacen múltiples validaciones en el mismo método ProcesarOrden(), sin poder extenderlo. ❌ → ¡Ideal para Template Method!

## Código mal estructurado (BadCode)
```
// Proyecto .NET 8 - Ejemplo intencional de mal diseño
// ⚠️ Este código es un anti-ejemplo: múltiples responsabilidades en un solo método.

using System;
using System.Collections.Generic;

namespace TiendaApp
{
    public class Orden
    {
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public string MetodoPago { get; set; }
        public string DireccionEnvio { get; set; }
    }

    public class ProcesadorDeOrden
    {
        private List<string> productosDisponibles = new() { "Laptop", "Mouse", "Teclado" };
        private Dictionary<string, int> stock = new() { { "Laptop", 3 }, { "Mouse", 10 }, { "Teclado", 5 } };

        public void ProcesarOrden(Orden orden)
        {
            Console.WriteLine("=== Procesando orden ===");

            // 1. Validar cliente
            if (string.IsNullOrEmpty(orden.Cliente))
            {
                Console.WriteLine("❌ Error: cliente no válido.");
                return;
            }

            // 2. Validar producto
            if (!productosDisponibles.Contains(orden.Producto))
            {
                Console.WriteLine("❌ Error: producto no disponible.");
                return;
            }

            // 3. Validar stock
            if (stock[orden.Producto] < orden.Cantidad)
            {
                Console.WriteLine("❌ Error: stock insuficiente.");
                return;
            }

            // 4. Validar pago
            if (orden.MetodoPago != "Tarjeta" && orden.MetodoPago != "PayPal")
            {
                Console.WriteLine("❌ Error: método de pago no aceptado.");
                return;
            }

            // 5. Validar dirección
            if (string.IsNullOrWhiteSpace(orden.DireccionEnvio))
            {
                Console.WriteLine("❌ Error: dirección no válida.");
                return;
            }

            // 6. Procesar orden (lógica mezclada con validaciones)
            stock[orden.Producto] -= orden.Cantidad;
            Console.WriteLine($"✅ Orden completada: {orden.Cantidad} x {orden.Producto} para {orden.Cliente}");
        }
    }

    class Program
    {
        static void Main()
        {
            var orden = new Orden
            {
                Cliente = "Carlos",
                Producto = "Laptop",
                Cantidad = 1,
                MetodoPago = "Tarjeta",
                DireccionEnvio = "Av. Central 123"
            };

            var procesador = new ProcesadorDeOrden();
            procesador.ProcesarOrden(orden);
        }
    }
}

```
## Problemas Detectados

- Violación del Principio de Responsabilidad Única (SRP):
ProcesarOrden() hace todo: validación, lógica de negocio, actualización de inventario.

- Difícil de extender:
Si mañana hay una nueva validación (p. ej. “cliente VIP”), hay que modificar el método.

- Falta de separación de preocupaciones:
Las reglas de negocio y de infraestructura están mezcladas.

- No reutilizable:
Las validaciones no pueden usarse en otros contextos (como una API o microservicio).

- No hay manejo de excepciones controlado.

- Demasiadas dependencias internas (productos, stock).

- Método demasiado largo y secuencial.

- Difícil de testear unitariamente.

- No cumple el principio Open/Closed.

- No usa abstracciones o interfaces.
---

## Código Corregido 

```
// Proyecto .NET 8 - Refactorizado con Template Method
// Mejor diseño: separación de responsabilidades y estructura clara

using System;
using System.Collections.Generic;

namespace TiendaApp
{
    public class Orden
    {
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public string MetodoPago { get; set; }
        public string DireccionEnvio { get; set; }
    }

    public abstract class ProcesadorDeOrdenBase
    {
        protected List<string> productosDisponibles = new() { "Laptop", "Mouse", "Teclado" };
        protected Dictionary<string, int> stock = new() { { "Laptop", 3 }, { "Mouse", 10 }, { "Teclado", 5 } };

        // Template Method - Define el esqueleto del algoritmo
        public void ProcesarOrden(Orden orden)
        {
            Console.WriteLine("=== Procesando orden ===");

            if (!ValidarOrden(orden))
                return;

            EjecutarProcesamiento(orden);
            FinalizarProcesamiento(orden);
        }

        // Método que contiene las validaciones (puede ser override si es necesario)
        protected virtual bool ValidarOrden(Orden orden)
        {
            return ValidarCliente(orden) &&
                   ValidarProducto(orden) &&
                   ValidarStock(orden) &&
                   ValidarPago(orden) &&
                   ValidarDireccion(orden);
        }

        // Métodos de validación específicos
        protected bool ValidarCliente(Orden orden)
        {
            if (string.IsNullOrEmpty(orden.Cliente))
            {
                Console.WriteLine("Error: cliente no válido.");
                return false;
            }
            return true;
        }

        protected bool ValidarProducto(Orden orden)
        {
            if (!productosDisponibles.Contains(orden.Producto))
            {
                Console.WriteLine("Error: producto no disponible.");
                return false;
            }
            return true;
        }

        protected bool ValidarStock(Orden orden)
        {
            if (stock[orden.Producto] < orden.Cantidad)
            {
                Console.WriteLine("Error: stock insuficiente.");
                return false;
            }
            return true;
        }

        protected virtual bool ValidarPago(Orden orden)
        {
            if (orden.MetodoPago != "Tarjeta" && orden.MetodoPago != "PayPal")
            {
                Console.WriteLine("Error: método de pago no aceptado.");
                return false;
            }
            return true;
        }

        protected bool ValidarDireccion(Orden orden)
        {
            if (string.IsNullOrWhiteSpace(orden.DireccionEnvio))
            {
                Console.WriteLine("Error: dirección no válida.");
                return false;
            }
            return true;
        }

        // Método abstracto para el procesamiento específico
        protected abstract void EjecutarProcesamiento(Orden orden);

        // Hook method - puede ser override para agregar funcionalidad adicional
        protected virtual void FinalizarProcesamiento(Orden orden)
        {
            Console.WriteLine($"Orden completada: {orden.Cantidad} x {orden.Producto} para {orden.Cliente}");
        }
    }

    // Implementación concreta para procesamiento estándar
    public class ProcesadorDeOrdenStandard : ProcesadorDeOrdenBase
    {
        protected override void EjecutarProcesamiento(Orden orden)
        {
            // Lógica específica de procesamiento
            stock[orden.Producto] -= orden.Cantidad;
            Console.WriteLine($"Procesando orden estándar...");
        }
    }

    // Implementación concreta para procesamiento express
    public class ProcesadorDeOrdenExpress : ProcesadorDeOrdenBase
    {
        protected override void EjecutarProcesamiento(Orden orden)
        {
            // Lógica específica para envío express
            stock[orden.Producto] -= orden.Cantidad;
            Console.WriteLine($"Procesando orden express...");
        }

        protected override void FinalizarProcesamiento(Orden orden)
        {
            base.FinalizarProcesamiento(orden);
            Console.WriteLine($"Envío express configurado para: {orden.DireccionEnvio}");
        }
    }

    class Program
    {
        static void Main()
        {
            var orden = new Orden
            {
                Cliente = "Carlos",
                Producto = "Laptop",
                Cantidad = 1,
                MetodoPago = "Tarjeta",
                DireccionEnvio = "Av. Central 123"
            };

            Console.WriteLine("=== Procesador Standard ===");
            var procesadorStandard = new ProcesadorDeOrdenStandard();
            procesadorStandard.ProcesarOrden(orden);

            Console.WriteLine("\n=== Procesador Express ===");
            var procesadorExpress = new ProcesadorDeOrdenExpress();
            procesadorExpress.ProcesarOrden(orden);
        }
    }
}
```
## Mejoras implementadas:

### Template Method Pattern
- ProcesarOrden() es el método plantilla que define el esqueleto del algoritmo
- Las validaciones y el procesamiento están separados en métodos específicos

### Separación de responsabilidades
- Cada validación en su propio método
- Procesamiento específico en EjecutarProcesamiento()
- Finalización opcional en FinalizarProcesamiento()

### Extensibilidad
- ProcesadorStandard: Implementación básica
- ProcesadorExpress: Agrega funcionalidad de envío express

### Flexibilidad
- ValidarOrden() puede ser override para cambiar el orden de validaciones
- ValidarPago() puede ser extendido para nuevos métodos de pago
- FinalizarProcesamiento() es un hook para agregar lógica adicional

### Mantenibilidad
- Código más limpio y organizado
- Fácil de testear cada componente por separado
- Nuevos tipos de procesamiento se agregan creando nuevas clases

### Imprimr en pantalla los resultados
https://dotnetfiddle.net/QfNxnB
