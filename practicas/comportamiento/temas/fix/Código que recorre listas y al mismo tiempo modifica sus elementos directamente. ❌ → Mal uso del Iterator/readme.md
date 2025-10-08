# Refactoring Code Smell #26 - Patrones de Comportamiento (GoF)

## Información del Estudiante
**Nombre:** Santy Francisco Martinez Castellanos  
**Matrícula:** 21211989

---

## Tema Seleccionado
**Problema #26:** "Código que recorre listas y al mismo tiempo modifica sus elementos directamente"  
**Patrón de Diseño Aplicado:** Iterator Pattern (Patrón de Comportamiento - GoF)

---

## Objetivo
Refactorizar código que modifica elementos de una colección mientras la recorre, lo cual puede causar comportamientos inesperados, excepciones o resultados incorrectos. Aplicar el patrón Iterator para encapsular el recorrido de la colección y separar la lógica de iteración de la lógica de modificación.

---

## Código con Code Smell (Problema)

```csharp
using System;
using System.Collections.Generic;

// ❌ CODE SMELL: Modificación directa de elementos durante iteración
public class GestorProductos
{
    private List<Producto> productos = new List<Producto>();
    
    // ❌ Método que modifica la lista mientras la recorre
    public void ActualizarPreciosConDescuento(decimal porcentajeDescuento)
    {
        Console.WriteLine("=== ACTUALIZANDO PRECIOS CON DESCUENTO ===");
        
        // ❌ PROBLEMA: Modificar elementos mientras se itera
        for (int i = 0; i < productos.Count; i++)
        {
            var producto = productos[i];
            Console.WriteLine($"Procesando: {producto.Nombre} - Precio actual: ${producto.Precio}");
            
            // ❌ Modificación directa durante iteración
            producto.Precio = producto.Precio * (1 - porcentajeDescuento / 100);
            producto.Actualizado = true;
            
            // ❌ PROBLEMA: Eliminar elementos durante iteración puede causar errores
            if (producto.Precio < 10)
            {
                Console.WriteLine($"Eliminando producto barato: {producto.Nombre}");
                productos.RemoveAt(i); // ❌ Esto puede causar problemas de índice
                i--; // ❌ Solución manual y propensa a errores
            }
        }
    }
    
    // ❌ Otro método problemático
    public void MarcarProductosVencidos()
    {
        Console.WriteLine("\n=== MARCANDO PRODUCTOS VENCIDOS ===");
        
        // ❌ PROBLEMA: Usar foreach y modificar la colección
        foreach (var producto in productos)
        {
            if (producto.FechaVencimiento < DateTime.Now)
            {
                producto.Vencido = true;
                Console.WriteLine($"Producto vencido: {producto.Nombre}");
                
                // ❌ ERROR: No se puede modificar colección durante foreach
                // productos.Remove(producto); // Esto causaría excepción
            }
        }
    }
    
    // ❌ Método que agrega elementos durante iteración
    public void DuplicarProductosPopulares()
    {
        Console.WriteLine("\n=== DUPLICANDO PRODUCTOS POPULARES ===");
        var productosOriginales = new List<Producto>(productos); // Copia temporal
        
        // ❌ PROBLEMA: Agregar elementos mientras se itera
        foreach (var producto in productosOriginales)
        {
            if (producto.Ventas > 100)
            {
                var duplicado = new Producto
                {
                    Id = productos.Count + 1,
                    Nombre = $"{producto.Nombre} - Duplicado",
                    Precio = producto.Precio,
                    FechaVencimiento = producto.FechaVencimiento.AddDays(30),
                    Ventas = 0,
                    Actualizado = false,
                    Vencido = false
                };
                
                productos.Add(duplicado); // ❌ Modificar colección durante iteración
                Console.WriteLine($"Duplicado creado: {duplicado.Nombre}");
            }
        }
    }
    
    public void AgregarProducto(Producto producto)
    {
        productos.Add(producto);
    }
    
    public void MostrarProductos()
    {
        Console.WriteLine("\n=== LISTA DE PRODUCTOS ===");
        for (int i = 0; i < productos.Count; i++)
        {
            var p = productos[i];
            Console.WriteLine($"{i + 1}. {p.Nombre} - ${p.Precio:F2} - Vencido: {p.Vencido} - Actualizado: {p.Actualizado}");
        }
    }
}

// ❌ Clase Producto simple
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public int Ventas { get; set; }
    public bool Actualizado { get; set; }
    public bool Vencido { get; set; }
}

// ❌ Programa principal que demuestra el problema
public class Program
{
    public static void Main()
    {
        var gestor = new GestorProductos();
        
        // Agregar productos de prueba
        gestor.AgregarProducto(new Producto 
        { 
            Id = 1, 
            Nombre = "Laptop", 
            Precio = 15000, 
            FechaVencimiento = DateTime.Now.AddDays(30),
            Ventas = 150
        });
        
        gestor.AgregarProducto(new Producto 
        { 
            Id = 2, 
            Nombre = "Mouse", 
            Precio = 5, 
            FechaVencimiento = DateTime.Now.AddDays(15),
            Ventas = 200
        });
        
        gestor.AgregarProducto(new Producto 
        { 
            Id = 3, 
            Nombre = "Teclado", 
            Precio = 800, 
            FechaVencimiento = DateTime.Now.AddDays(-5), // Vencido
            Ventas = 80
        });
        
        gestor.AgregarProducto(new Producto 
        { 
            Id = 4, 
            Nombre = "Monitor", 
            Precio = 5000, 
            FechaVencimiento = DateTime.Now.AddDays(60),
            Ventas = 120
        });
        
        Console.WriteLine("=== ESTADO INICIAL ===");
        gestor.MostrarProductos();
        
        // ❌ Operaciones problemáticas
        gestor.ActualizarPreciosConDescuento(10);
        gestor.MarcarProductosVencidos();
        gestor.DuplicarProductosPopulares();
        
        Console.WriteLine("\n=== ESTADO FINAL ===");
        gestor.MostrarProductos();
    }
}
```

### Problemas Identificados:
1. **Modificación durante iteración**: Cambiar elementos mientras se recorre la colección
2. **Eliminación durante iteración**: Remover elementos puede causar errores de índice
3. **Adición durante iteración**: Agregar elementos puede causar comportamientos inesperados
4. **Violación del principio de responsabilidad única**: La clase maneja tanto iteración como modificación
5. **Código propenso a errores**: Soluciones manuales como `i--` son frágiles
6. **Dificultad para testing**: No se pueden probar iteración y modificación por separado

---

## Código Refactorizado (Solución con Iterator Pattern)

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

// ✅ INTERFAZ ITERATOR - Define operaciones de iteración
public interface IIterator<T>
{
    bool HasNext();
    T Next();
    void Remove();
    void Reset();
}

// ✅ INTERFAZ ITERABLE - Define cómo crear iteradores
public interface IIterable<T>
{
    IIterator<T> CreateIterator();
    int Count { get; }
}

// ✅ ITERATOR CONCRETO - Implementación segura para List<Producto>
public class ProductoIterator : IIterator<Producto>
{
    private List<Producto> productos;
    private int currentIndex;
    private List<int> indicesToRemove;
    
    public ProductoIterator(List<Producto> productos)
    {
        this.productos = productos;
        this.currentIndex = 0;
        this.indicesToRemove = new List<int>();
    }
    
    public bool HasNext()
    {
        return currentIndex < productos.Count;
    }
    
    public Producto Next()
    {
        if (!HasNext())
            throw new InvalidOperationException("No hay más elementos");
            
        var producto = productos[currentIndex];
        currentIndex++;
        return producto;
    }
    
    // ✅ MÉTODO SEGURO PARA ELIMINAR
    public void Remove()
    {
        if (currentIndex == 0)
            throw new InvalidOperationException("Debe llamar a Next() antes de Remove()");
            
        indicesToRemove.Add(currentIndex - 1);
    }
    
    public void Reset()
    {
        currentIndex = 0;
        indicesToRemove.Clear();
    }
    
    // ✅ MÉTODO PARA APLICAR ELIMINACIONES PENDIENTES
    public void ApplyRemovals()
    {
        // Ordenar índices en orden descendente para eliminar de atrás hacia adelante
        indicesToRemove.Sort((a, b) => b.CompareTo(a));
        
        foreach (var index in indicesToRemove)
        {
            if (index >= 0 && index < productos.Count)
            {
                productos.RemoveAt(index);
            }
        }
        
        indicesToRemove.Clear();
    }
}

// ✅ ITERATOR SEGURO PARA MODIFICACIONES
public class SafeModificationIterator : IIterator<Producto>
{
    private List<Producto> productos;
    private int currentIndex;
    private List<Producto> productosToAdd;
    private List<Producto> productosToRemove;
    
    public SafeModificationIterator(List<Producto> productos)
    {
        this.productos = productos;
        this.currentIndex = 0;
        this.productosToAdd = new List<Producto>();
        this.productosToRemove = new List<Producto>();
    }
    
    public bool HasNext()
    {
        return currentIndex < productos.Count;
    }
    
    public Producto Next()
    {
        if (!HasNext())
            throw new InvalidOperationException("No hay más elementos");
            
        var producto = productos[currentIndex];
        currentIndex++;
        return producto;
    }
    
    public void Remove()
    {
        if (currentIndex == 0)
            throw new InvalidOperationException("Debe llamar a Next() antes de Remove()");
            
        productosToRemove.Add(productos[currentIndex - 1]);
    }
    
    public void Add(Producto producto)
    {
        productosToAdd.Add(producto);
    }
    
    public void Reset()
    {
        currentIndex = 0;
        productosToAdd.Clear();
        productosToRemove.Clear();
    }
    
    // ✅ APLICAR TODAS LAS MODIFICACIONES DE FORMA SEGURA
    public void ApplyModifications()
    {
        // Aplicar eliminaciones
        foreach (var producto in productosToRemove)
        {
            productos.Remove(producto);
        }
        
        // Aplicar adiciones
        foreach (var producto in productosToAdd)
        {
            productos.Add(producto);
        }
        
        // Limpiar listas temporales
        productosToAdd.Clear();
        productosToRemove.Clear();
    }
}

// ✅ GESTOR REFACTORIZADO - Usa Iterator Pattern
public class GestorProductosRefactorizado : IIterable<Producto>
{
    private List<Producto> productos = new List<Producto>();
    
    public int Count => productos.Count;
    
    // ✅ MÉTODO SEGURO PARA ACTUALIZAR PRECIOS
    public void ActualizarPreciosConDescuento(decimal porcentajeDescuento)
    {
        Console.WriteLine("=== ACTUALIZANDO PRECIOS CON DESCUENTO (SEGURO) ===");
        
        var iterator = new SafeModificationIterator(productos);
        
        while (iterator.HasNext())
        {
            var producto = iterator.Next();
            Console.WriteLine($"Procesando: {producto.Nombre} - Precio actual: ${producto.Precio}");
            
            // ✅ Modificación segura durante iteración
            producto.Precio = producto.Precio * (1 - porcentajeDescuento / 100);
            producto.Actualizado = true;
            
            // ✅ Eliminación segura (marcada para después)
            if (producto.Precio < 10)
            {
                Console.WriteLine($"Marcando para eliminar: {producto.Nombre}");
                iterator.Remove();
            }
        }
        
        // ✅ Aplicar eliminaciones de forma segura
        iterator.ApplyModifications();
    }
    
    // ✅ MÉTODO SEGURO PARA MARCAR VENCIDOS
    public void MarcarProductosVencidos()
    {
        Console.WriteLine("\n=== MARCANDO PRODUCTOS VENCIDOS (SEGURO) ===");
        
        var iterator = CreateIterator();
        
        while (iterator.HasNext())
        {
            var producto = iterator.Next();
            
            if (producto.FechaVencimiento < DateTime.Now)
            {
                producto.Vencido = true;
                Console.WriteLine($"Producto vencido: {producto.Nombre}");
            }
        }
    }
    
    // ✅ MÉTODO SEGURO PARA DUPLICAR PRODUCTOS
    public void DuplicarProductosPopulares()
    {
        Console.WriteLine("\n=== DUPLICANDO PRODUCTOS POPULARES (SEGURO) ===");
        
        var iterator = new SafeModificationIterator(productos);
        
        while (iterator.HasNext())
        {
            var producto = iterator.Next();
            
            if (producto.Ventas > 100)
            {
                var duplicado = new Producto
                {
                    Id = productos.Count + 1,
                    Nombre = $"{producto.Nombre} - Duplicado",
                    Precio = producto.Precio,
                    FechaVencimiento = producto.FechaVencimiento.AddDays(30),
                    Ventas = 0,
                    Actualizado = false,
                    Vencido = false
                };
                
                // ✅ Adición segura (marcada para después)
                iterator.Add(duplicado);
                Console.WriteLine($"Duplicado creado: {duplicado.Nombre}");
            }
        }
        
        // ✅ Aplicar adiciones de forma segura
        iterator.ApplyModifications();
    }
    
    // ✅ IMPLEMENTACIÓN DE IIterable
    public IIterator<Producto> CreateIterator()
    {
        return new ProductoIterator(productos);
    }
    
    public void AgregarProducto(Producto producto)
    {
        productos.Add(producto);
    }
    
    public void MostrarProductos()
    {
        Console.WriteLine("\n=== LISTA DE PRODUCTOS ===");
        var iterator = CreateIterator();
        int index = 1;
        
        while (iterator.HasNext())
        {
            var p = iterator.Next();
            Console.WriteLine($"{index}. {p.Nombre} - ${p.Precio:F2} - Vencido: {p.Vencido} - Actualizado: {p.Actualizado}");
            index++;
        }
    }
}

// ✅ CLASE PRODUCTO (sin cambios)
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public int Ventas { get; set; }
    public bool Actualizado { get; set; }
    public bool Vencido { get; set; }
}

// ✅ PROGRAMA PRINCIPAL REFACTORIZADO
public class Program
{
    public static void Main()
    {
        var gestor = new GestorProductosRefactorizado();
        
        // Agregar productos de prueba
        gestor.AgregarProducto(new Producto 
        { 
            Id = 1, 
            Nombre = "Laptop", 
            Precio = 15000, 
            FechaVencimiento = DateTime.Now.AddDays(30),
            Ventas = 150
        });
        
        gestor.AgregarProducto(new Producto 
        { 
            Id = 2, 
            Nombre = "Mouse", 
            Precio = 5, 
            FechaVencimiento = DateTime.Now.AddDays(15),
            Ventas = 200
        });
        
        gestor.AgregarProducto(new Producto 
        { 
            Id = 3, 
            Nombre = "Teclado", 
            Precio = 800, 
            FechaVencimiento = DateTime.Now.AddDays(-5), // Vencido
            Ventas = 80
        });
        
        gestor.AgregarProducto(new Producto 
        { 
            Id = 4, 
            Nombre = "Monitor", 
            Precio = 5000, 
            FechaVencimiento = DateTime.Now.AddDays(60),
            Ventas = 120
        });
        
        Console.WriteLine("=== ESTADO INICIAL ===");
        gestor.MostrarProductos();
        
        // ✅ Operaciones seguras con Iterator Pattern
        gestor.ActualizarPreciosConDescuento(10);
        gestor.MarcarProductosVencidos();
        gestor.DuplicarProductosPopulares();
        
        Console.WriteLine("\n=== ESTADO FINAL ===");
        gestor.MostrarProductos();
        
        // ✅ Demostración de iteración segura
        Console.WriteLine("\n=== ITERACIÓN SEGURA ===");
        var iterator = gestor.CreateIterator();
        while (iterator.HasNext())
        {
            var producto = iterator.Next();
            Console.WriteLine($"Iterando: {producto.Nombre}");
        }
    }
}
```
## Justificación del Patrón Iterator

### ¿Por qué Iterator Pattern?

El **Iterator Pattern** es la solución ideal para este code smell porque:

1. **Encapsula el recorrido de una colección**: Oculta la estructura interna de la colección y proporciona una interfaz uniforme para acceder a sus elementos.

2. **Separa la lógica de iteración de la lógica de negocio**: Permite que el cliente se enfoque en procesar elementos sin preocuparse por cómo recorrer la colección.

3. **Permite diferentes tipos de iteración**: Se pueden crear iteradores especializados (seguros, reversos, filtrados, etc.) sin modificar la colección.

4. **Evita modificaciones durante iteración**: Proporciona mecanismos seguros para manejar modificaciones de la colección.

5. **Mejora la reutilización**: Los iteradores pueden ser reutilizados en diferentes contextos.

### Alternativas Consideradas:

- **Strategy Pattern**: No aplica porque no se trata de intercambiar algoritmos de iteración.
- **Command Pattern**: No es apropiado porque no se trata de encapsular operaciones.
- **Template Method**: No aplica porque no hay un algoritmo común con pasos variables.
---

## Conclusión

### Problema Resuelto

El refactoring exitosamente eliminó el code smell de **modificación directa de elementos durante iteración** utilizando el **Iterator Pattern**. La solución original era propensa a errores, excepciones y comportamientos inesperados al modificar colecciones mientras se iteraban.

## Resultado 
<img width="1246" height="1169" alt="image" src="https://github.com/user-attachments/assets/f9b0bfad-1ccc-4831-9983-63bcaddd6ec3" />


