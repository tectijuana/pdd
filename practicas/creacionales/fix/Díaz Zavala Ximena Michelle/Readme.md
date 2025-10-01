# 📌 Pull Request – Mejora de Prototype en Tienda

## 🔍 Problemas detectados
1. **El método `Clone()` está definido pero no implementado** → rompe la intención del patrón Prototype.  
2. **Clases concretas (`Ropa` y `Electronico`) no implementan su propia copia** → violan la responsabilidad del patrón creacional.  
3. **Uso forzado de `new` en lugar de reutilizar objetos existentes** → baja cohesión, poca reutilización y código duplicado.  

---

## 🛠 Patrón aplicado
- **Prototype Pattern**:  
  Se implementa correctamente el método `Clone()` en cada clase concreta (`Ropa`, `Electronico`).  
- Se elimina la dependencia de `new` al permitir la clonación de objetos ya configurados.  

---

## 💡 Justificación del cambio
- **Cohesión interna**: cada clase sabe clonarse a sí misma sin depender de código externo.  
- **Testabilidad**: podemos generar copias de prueba sin alterar los objetos originales.  
- **Flexibilidad**: facilita crear variaciones de productos a partir de prototipos existentes.  
- **Reutilización**: evitamos repetir lógica de inicialización.  

---

## 🔄 Impacto
- Cumplimiento del principio **Open/Closed (OCP)** → nuevas clases de producto pueden añadirse sin modificar las existentes.  
- Cumplimiento del principio **Single Responsibility (SRP)** → cada clase maneja su propia clonación.  
- Mejora en **Inversión de Dependencias (DIP)** → el cliente (`Tienda`) no depende de la creación directa con `new`.  
- Arquitectura más preparada para **pruebas unitarias** y escalabilidad.  

---

## 🚨 Código de "Mala Calidad" (sin Clone implementado)
```csharp
// Ejemplo de código .NET 8 en estado "incompleto" para Prototype
using System;
using System.Collections.Generic;

namespace TiendaPrototype
{
    // Producto base en la tienda
    public abstract class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        // 🚨 Aquí debería ir Clone, pero está ausente
        public abstract Producto Clone(); // ← no implementado correctamente todavía
    }

    // Producto concreto: Ropa
    public class Ropa : Producto
    {
        public string Talla { get; set; }
        public string Color { get; set; }

        // 🚧 Falta implementar Clone
        public override Producto Clone()
        {
            throw new NotImplementedException("Aquí deberías clonar el objeto Ropa...");
        }
    }

    // Producto concreto: Electrónico
    public class Electronico : Producto
    {
        public string Marca { get; set; }
        public int GarantiaMeses { get; set; }

        // 🚧 Falta implementar Clone
        public override Producto Clone()
        {
            throw new NotImplementedException("Aquí deberías clonar el objeto Electronico...");
        }
    }

    // Cliente que usa los productos
    public class Tienda
    {
        private List<Producto> _productos = new List<Producto>();

        public void AgregarProducto(Producto producto)
        {
            _productos.Add(producto);
        }

        public void MostrarInventario()
        {
            foreach (var p in _productos)
            {
                Console.WriteLine($"Producto: {p.Nombre}, Precio: {p.Precio}");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var tienda = new Tienda();

            // Creación manual (sin clon todavía)
            var camisa = new Ropa { Nombre = "Camisa", Precio = 300, Talla = "M", Color = "Azul" };
            tienda.AgregarProducto(camisa);

            var laptop = new Electronico { Nombre = "Laptop", Precio = 15000, Marca = "Dell", GarantiaMeses = 24 };
            tienda.AgregarProducto(laptop);

            tienda.MostrarInventario();

            // 🚨 Reto: ahora intenta clonar estos productos en vez de repetir creación manual
        }
    }
}

```
---

## ✅ Código Mejorado (.NET 8 con Prototype implementado)
```csharp
using System;
using System.Collections.Generic;

namespace TiendaPrototype
{
    // Producto base en la tienda
    public abstract class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        // Método abstracto Clone → obligatorio para cada producto concreto
        public abstract Producto Clone();
    }

    // Producto concreto: Ropa
    public class Ropa : Producto
    {
        public string Talla { get; set; }
        public string Color { get; set; }

        // Implementación de clonación profunda
        public override Producto Clone()
        {
            return new Ropa
            {
                Nombre = this.Nombre,
                Precio = this.Precio,
                Talla = this.Talla,
                Color = this.Color
            };
        }
    }

    // Producto concreto: Electrónico
    public class Electronico : Producto
    {
        public string Marca { get; set; }
        public int GarantiaMeses { get; set; }

        // Implementación de clonación profunda
        public override Producto Clone()
        {
            return new Electronico
            {
                Nombre = this.Nombre,
                Precio = this.Precio,
                Marca = this.Marca,
                GarantiaMeses = this.GarantiaMeses
            };
        }
    }

    // Cliente que usa los productos
    public class Tienda
    {
        private readonly List<Producto> _productos = new List<Producto>();

        public void AgregarProducto(Producto producto)
        {
            _productos.Add(producto);
        }

        public void MostrarInventario()
        {
            Console.WriteLine("📦 Inventario de Tienda:");
            foreach (var p in _productos)
            {
                Console.WriteLine($"Producto: {p.Nombre}, Precio: {p.Precio}");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var tienda = new Tienda();

            // Creación inicial
            var camisa = new Ropa { Nombre = "Camisa", Precio = 300, Talla = "M", Color = "Azul" };
            tienda.AgregarProducto(camisa);

            var laptop = new Electronico { Nombre = "Laptop", Precio = 15000, Marca = "Dell", GarantiaMeses = 24 };
            tienda.AgregarProducto(laptop);

            // Clonando productos sin necesidad de new
            var camisaClonada = camisa.Clone();
            camisaClonada.Nombre = "Camisa clonada"; // se puede modificar sin afectar el original
            tienda.AgregarProducto(camisaClonada);

            var laptopClonada = laptop.Clone();
            laptopClonada.Nombre = "Laptop clonada";
            tienda.AgregarProducto(laptopClonada);

            // Mostrar resultados
            tienda.MostrarInventario();
        }
    }
}
