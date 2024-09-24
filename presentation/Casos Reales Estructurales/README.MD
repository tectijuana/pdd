# Casos Reales Estructurales de PDD

Este repositorio contiene casos reales de problemas estructurales en el diseño de software, conocidos como **Problemas de Diseño Estructurales (PDD)**. 
Cada caso incluye una descripción del problema, la solución aplicada y el código en C# con explicaciones detalladas.

## ¿Qué son los Problemas de Diseño Estructurales (PDD)?

Los problemas de diseño estructural en el software ocurren cuando la arquitectura del sistema presenta defectos que dificultan su mantenimiento, evolución o eficiencia.
Estos problemas suelen surgir debido a una mala planificación inicial o al crecimiento descontrolado del código. 
Los PDD impactan negativamente la calidad del software, haciéndolo más difícil de escalar y modificar.

## Caso Real: **Duplicación de Lógica en Varios Módulos**

### Descripción del Problema

En un sistema de gestión de inventarios, se encontró que la lógica para calcular el total de productos en stock estaba duplicada en diferentes módulos del sistema. Esto llevó a problemas de mantenimiento, ya que cada vez que había que actualizar el cálculo, se debía modificar en múltiples lugares, lo cual aumentaba el riesgo de errores.

### Solución

Para resolver este problema, se optó por aplicar el patrón de diseño **"Single Responsibility Principle" (SRP)**.
Se centralizó el cálculo en una única clase especializada, permitiendo que otros módulos consumieran esta funcionalidad sin duplicar la lógica.

### Código en C#

Corrida en:
https://dotnetfiddle.net/pCXlpb

```csharp
// Clase responsable de manejar el cálculo de inventario
public class InventarioService
{
    public int CalcularTotalProductosEnStock(List<Producto> productos)
    {
        int total = 0;
        foreach (var producto in productos)
        {
            total += producto.CantidadEnStock;
        }
        return total;
    }
}

// Clase Producto
public class Producto
{
    public string Nombre { get; set; }
    public int CantidadEnStock { get; set; }
}

// Módulo que consume la lógica de InventarioService
public class ModuloVentas
{
    private readonly InventarioService _inventarioService;

    public ModuloVentas(InventarioService inventarioService)
    {
        _inventarioService = inventarioService;
    }

    public void MostrarTotalEnStock(List<Producto> productos)
    {
        int total = _inventarioService.CalcularTotalProductosEnStock(productos);
        Console.WriteLine($"Total en stock: {total}");
    }
}

// Uso en el programa principal
class Program
{
    static void Main(string[] args)
    {
        var productos = new List<Producto>
        {
            new Producto { Nombre = "Producto A", CantidadEnStock = 10 },
            new Producto { Nombre = "Producto B", CantidadEnStock = 20 },
        };

        var inventarioService = new InventarioService();
        var moduloVentas = new ModuloVentas(inventarioService);

        moduloVentas.MostrarTotalEnStock(productos);
    }
}
