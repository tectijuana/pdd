# Para validar una orden, se usan 8 pasos consecutivos en el mismo método Validar(), sin posibilidad de extensión. ❌ → Candidate para Chain of Responsibility.
Ana Cristina Gutiérrez Martínez | 21211959
Fecha: 07/10/2025

## Code Smell
``` cs
using System;
using System.Collections.Generic;

public class ValidadorOrden
{
    public void Validar(Orden orden)
    {
        Console.WriteLine("Validando cliente...");
        // Paso 1
        if (orden.Cliente == null) throw new Exception("Cliente no válido.");

        Console.WriteLine("Validando productos...");
        // Paso 2
        if (orden.Productos.Count == 0) throw new Exception("Sin productos.");
        Console.WriteLine("Validando inventario...");
        // Paso 3
        // ... y así hasta el paso 8
        Console.WriteLine("Validación completa.");
    }
}
```
**Problemas detectados:**
- Método largo con múltiples pasos rígidos.
- No se puede extender sin modificar el código.
- Viola el principio OCP (Open/Closed Principle).

## Refactor: Parte Funcional

``` cs
using System;
using System.Collections.Generic;

namespace ValidacionOrdenRefactor
{
    // Clase base (Handler)
    public abstract class ValidadorBase
    {
        protected ValidadorBase Siguiente;
        public void EstablecerSiguiente(ValidadorBase siguiente)
        {
            Siguiente = siguiente;
        }

        public virtual void Validar(Orden orden)
        {
            if (Siguiente != null)
                Siguiente.Validar(orden);
        }
    }

    // Handlers concretos
    public class ValidadorCliente : ValidadorBase
    {
        public override void Validar(Orden orden)
        {
            if (string.IsNullOrEmpty(orden.Cliente))
                throw new Exception("Cliente no válido.");
            Console.WriteLine("Cliente validado correctamente.");

            base.Validar(orden);
        }
    }

    public class ValidadorProductos : ValidadorBase
    {
        public override void Validar(Orden orden)
        {
            if (orden.Productos == null || orden.Productos.Count == 0)
                throw new Exception("La orden no contiene productos.");
            Console.WriteLine("Productos validados correctamente.");

            base.Validar(orden);
        }
    }

    // Clase de datos
    public class Orden
    {
        public string Cliente { get; set; } = string.Empty;
        public List<string> Productos { get; set; } = new List<string>();
    }

    // Programa de prueba
    class Program
    {
        static void Main()
        {
            var orden = new Orden
            {
                Cliente = "Juan Pérez",
                Productos = new List<string> { "Producto A" }
            };

            var validadorCliente = new ValidadorCliente();
            var validadorProductos = new ValidadorProductos();

            validadorCliente.EstablecerSiguiente(validadorProductos);

            try
            {
                validadorCliente.Validar(orden);
                Console.WriteLine("✔ Validación completada exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✖ Validación fallida: {ex.Message}");
            }
        }
    }
}
```

### Resultados en Dotnetfiddle
<img width="431" height="165" alt="image" src="https://github.com/user-attachments/assets/5a1da3c9-b692-4a47-8313-e87a315ffc9c" />

### Justificación
Se aplicó el patrón Chain of Responsibility porque permite distribuir las validaciones en una secuencia de objetos que trabajan de manera independiente. Este enfoque mejora 
la flexibilidad del programa, ya que nuevos pasos pueden añadirse sin modificar el código existente. Así, el sistema sigue el principio de estar abierto a la extensión y cerrado 
a la modificación, logrando una solución más limpia y adaptable.

### Reflexión
El código original concentraba todos los pasos de validación en un solo método, lo que lo hacía difícil de mantener y entender. Al separarlos mediante una cadena de validadores, 
el proceso se vuelve más claro y ordenado. Cada parte cumple su función sin interferir con las demás, lo que facilita realizar cambios o ampliaciones sin afectar todo el sistema.
