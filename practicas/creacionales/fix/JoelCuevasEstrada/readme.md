# Falta de interfaz en los productos creados
## Joel Cuevas Estrada - 22210298

# Problema 1: Acoplamiento fuerte a clases concretas

Ejemplo en el código actual:
La clase VineyardManager instancia directamente objetos Vineyard.

Consecuencia:

Si quieres cambiar la implementación de Vineyard (ej. que los viñedos provengan de una base de datos o una API externa), deberías modificar todo el código cliente.

Rompe el principio OCP (Open/Closed Principle): cada cambio obliga a modificar código existente.

Posible solución / Hint:
Introducir una interfaz IVineyard y crear implementaciones concretas. Aquí encajaría el patrón Factory Method o Abstract Factory para encapsular la creación de productos.

## Bad Code
```csharp
using System;
using System.Collections.Generic;

namespace VineyardApp.BadCode
{
    // Clase concreta sin interfaz
    public class Vineyard
    {
        public string Name { get; set; }
        public string Location { get; set; }

        public void PrintDetails()
        {
            Console.WriteLine($"Vineyard: {Name}, Location: {Location}");
        }
    }

    public class VineyardManager
    {
        private List<Vineyard> vineyards = new List<Vineyard>();

        // 🚨 Problema: Se instancia directamente la clase concreta Vineyard
        public void AddVineyard(string name, string location)
        {
            var vineyard = new Vineyard { Name = name, Location = location };
            vineyards.Add(vineyard);
            vineyard.PrintDetails();
        }
    }
}

``` 
## Code Corregido 
```csharp
public interface IVineyard
{
    string Name { get; set; }
    string Location { get; set; }
    void PrintDetails();
}

public class StandardVineyard : IVineyard
{
    public string Name { get; set; }
    public string Location { get; set; }

    public void PrintDetails()
    {
        Console.WriteLine($"Vineyard: {Name}, Location: {Location}");
    }
}

public class VineyardManager
{
    private List<IVineyard> vineyards = new List<IVineyard>();

    public void AddVineyard(IVineyard vineyard)
    {
        vineyards.Add(vineyard);
        vineyard.PrintDetails();
    }
}

```

# Problema 2: Dificultad para probar (Testing)

Ejemplo en el código actual:
VineyardManager no puede recibir “dobles de prueba” (mocks o stubs), ya que depende de Vineyard concreto.

Consecuencia:

No se puede simular un Vineyard falso para probar lógicas sin necesidad de cargar datos reales.

Tests unitarios se vuelven costosos y frágiles.

Posible solución / Hint:
Introducir una interfaz IVineyard que permita crear implementaciones “mock” para pruebas. Esto es coherente con patrones como Strategy (para comportamiento dinámico) o Dependency Injection (para inyectar las dependencias).

## Bad Code
```csharp
using System.Collections.Generic;

namespace VineyardApp.BadCode
{
    // Clase concreta fija
    public class Vineyard
    {
        public string Name { get; set; }
        public List<string> Grapes { get; set; }

        public bool HasGrape(string grape)
        {
            return Grapes.Contains(grape);
        }
    }

    public class VineyardManager
    {
        private readonly Vineyard vineyard;

        // 🚨 Problema: No podemos pasar un Mock o Stub aquí
        public VineyardManager()
        {
            vineyard = new Vineyard
            {
                Name = "La Toscana",
                Grapes = new List<string> { "Merlot", "Cabernet" }
            };
        }

        public bool CheckForGrape(string grape)
        {
            return vineyard.HasGrape(grape);
        }
    }
}

``` 
## Code Corregido
```csharp
public interface IVineyard
{
    string Name { get; set; }
    List<string> Grapes { get; set; }
    bool HasGrape(string grape);
}

public class Vineyard : IVineyard
{
    public string Name { get; set; }
    public List<string> Grapes { get; set; }

    public bool HasGrape(string grape) => Grapes.Contains(grape);
}

public class VineyardManager
{
    private readonly IVineyard vineyard;

    // 🚀 Inyección de dependencia
    public VineyardManager(IVineyard vineyard)
    {
        this.vineyard = vineyard;
    }

    public bool CheckForGrape(string grape)
    {
        return vineyard.HasGrape(grape);
    }
}

``` 
# Problema 3: Falta de polimorfismo en los productos

Ejemplo en el código actual:
Actualmente solo existe un tipo de viñedo (Vineyard), pero ¿qué pasa si quieres tener viñedos orgánicos, industriales o simulaciones?

Consecuencia:

Cada vez que agregues un nuevo tipo de producto, deberías modificar VineyardManager.

Esto rompe el Principio de Sustitución de Liskov (LSP).

Posible solución / Hint:
Definir una interfaz IVineyard que pueda ser implementada por diferentes tipos de viñedos (OrganicVineyard, IndustrialVineyard, etc.). Aquí el patrón Abstract Factory o incluso Prototype sería muy útil para crear familias de productos sin tocar el código cliente.

## Bad Code
```csharp
using System;

namespace VineyardApp.BadCode
{
    // Clase única de viñedo
    public class Vineyard
    {
        public string Name { get; set; }

        public void PrintType()
        {
            Console.WriteLine("Viñedo estándar.");
        }
    }

    public class VineyardManager
    {
        // 🚨 Problema: Solo puede manejar un tipo de Vineyard
        public void ShowVineyardDetails(Vineyard vineyard)
        {
            vineyard.PrintType();
        }
    }

    // 🚨 Problema adicional:
    // Si queremos soportar OrganicVineyard o IndustrialVineyard,
    // deberíamos duplicar código o modificar VineyardManager.
}

``` 
## Code Corregido
```csharp
public interface IVineyard
{
    string Name { get; set; }
    void PrintType();
}

public class StandardVineyard : IVineyard
{
    public string Name { get; set; }
    public void PrintType() => Console.WriteLine("Viñedo estándar.");
}

public class OrganicVineyard : IVineyard
{
    public string Name { get; set; }
    public void PrintType() => Console.WriteLine("Viñedo orgánico.");
}

public class VineyardManager
{
    public void ShowVineyardDetails(IVineyard vineyard)
    {
        vineyard.PrintType();
    }
}

``` 
