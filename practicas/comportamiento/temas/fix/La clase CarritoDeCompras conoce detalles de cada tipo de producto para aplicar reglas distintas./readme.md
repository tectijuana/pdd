#
Autor: Rojas Garcia Kevin Argenis
Fecha: 2025-10-08
Descripción: Practica patrones comportamiento bad code
 ============================================

## Bad Code

 ```
using System;
using System.Collections.Generic;

namespace TiendaApp
{
    public class CarritoDeCompras
    {
        private List<object> productos = new List<object>();

        public void AgregarProducto(object producto)
        {
            productos.Add(producto);
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var producto in productos)
            {
                if (producto is Libro libro)
                {
                    if (libro.EsImportado)
                        total += libro.Precio * 1.10m;
                    else
                        total += libro.Precio;
                }
                else if (producto is Comida comida)
                {
                    total += comida.Precio * 1.05m;
                }
                else if (producto is Electronico elec)
                {
                    total += elec.Precio * 1.20m + 15;
                }
                else
                {
                    Console.WriteLine("Producto desconocido");
                }
            }
            return total;
        }
    }

    public class Libro
    {
        public decimal Precio { get; set; }
        public bool EsImportado { get; set; }
    }

    public class Comida
    {
        public decimal Precio { get; set; }
    }

    public class Electronico
    {
        public decimal Precio { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            var carrito = new CarritoDeCompras();
            carrito.AgregarProducto(new Libro { Precio = 100, EsImportado = true });
            carrito.AgregarProducto(new Comida { Precio = 50 });
            carrito.AgregarProducto(new Electronico { Precio = 200 });

            Console.WriteLine($"Total: {carrito.CalcularTotal()}");
        }
    }
}

```

## Codigo Corregido

 ```
using System;
using System.Collections.Generic;

namespace TiendaApp
{
    // Interfaz para productos
    public interface IProducto
    {
        decimal Precio { get; }
        void Aceptar(IVisitor visitor);
    }

    // Interfaz del visitante
    public interface IVisitor
    {
        void Visitar(Libro libro);
        void Visitar(Comida comida);
        void Visitar(Electronico electronico);
    }

    // Implementación del visitante concreto (calcula totales)
    public class CalculadoraTotal : IVisitor
    {
        public decimal Total { get; private set; }

        public void Visitar(Libro libro)
        {
            if (libro.EsImportado)
                Total += libro.Precio * 1.10m;
            else
                Total += libro.Precio;
        }

        public void Visitar(Comida comida)
        {
            Total += comida.Precio * 1.05m;
        }

        public void Visitar(Electronico electronico)
        {
            Total += electronico.Precio * 1.20m + 15;
        }
    }

    // Clases de productos concretos
    public class Libro : IProducto
    {
        public decimal Precio { get; set; }
        public bool EsImportado { get; set; }

        public void Aceptar(IVisitor visitor)
        {
            visitor.Visitar(this);
        }
    }

    public class Comida : IProducto
    {
        public decimal Precio { get; set; }

        public void Aceptar(IVisitor visitor)
        {
            visitor.Visitar(this);
        }
    }

    public class Electronico : IProducto
    {
        public decimal Precio { get; set; }

        public void Aceptar(IVisitor visitor)
        {
            visitor.Visitar(this);
        }
    }

    // Clase Carrito que ya no conoce los detalles de cada producto
    public class CarritoDeCompras
    {
        private List<IProducto> productos = new List<IProducto>();

        public void AgregarProducto(IProducto producto)
        {
            productos.Add(producto);
        }

        public decimal CalcularTotal()
        {
            var visitor = new CalculadoraTotal();

            foreach (var producto in productos)
                producto.Aceptar(visitor);

            return visitor.Total;
        }
    }

    public class Program
    {
        public static void Main()
        {
            var carrito = new CarritoDeCompras();
            carrito.AgregarProducto(new Libro { Precio = 100, EsImportado = true });
            carrito.AgregarProducto(new Comida { Precio = 50 });
            carrito.AgregarProducto(new Electronico { Precio = 200 });

            Console.WriteLine($"Total: {carrito.CalcularTotal()}");
        }
    }
}

```
### El patrón Visitor es el más adecuado para este caso porque:

- El principio de abierto/cerrado (OCP) se cumple:
La clase CarritoDeCompras ya no necesita modificarse cada vez que se agregue un nuevo tipo de producto.
En su lugar, simplemente se crea una nueva clase que implemente IProducto y se añade el comportamiento correspondiente dentro del visitante.

- Desacopla las operaciones del objeto:
CarritoDeCompras no conoce los detalles internos de Libro, Comida o Electronico.
El cálculo del precio total se traslada a un objeto externo (CalculadoraTotal), lo que reduce dependencias.

- Facilita agregar nuevas operaciones:
Si en el futuro se desea calcular descuentos, impuestos o aplicar una promoción, solo se agrega un nuevo visitante (por ejemplo, CalculadoraDescuento) sin modificar las clases de los productos.

- Evita múltiples condicionales:
El uso del método Aceptar(visitor) junto con la sobrecarga de Visitar() elimina los if (producto is ...), logrando un código más limpio y extensible.

## En resumen:
El patrón Visitor es el correcto porque permite extender comportamientos sobre objetos de distintas clases sin modificarlas, promoviendo la extensibilidad y mantenibilidad del sistema, en línea con los principios de diseño SOLID definidos por GoF.
