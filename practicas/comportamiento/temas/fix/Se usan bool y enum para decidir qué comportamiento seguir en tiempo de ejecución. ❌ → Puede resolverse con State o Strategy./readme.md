# 🧩 Problema:

**Se usan bool y enum para decidir qué comportamiento seguir en tiempo de ejecución. ❌ → Puede resolverse con State o Strategy.**

---

## 🔍 Descripción del problema

En el siguiente ejemplo de código, se modela un **inventario de una bodega de vinos**, pero se cometen varios errores comunes de diseño.
El código utiliza **banderas booleanas (`bool`)** y **enumeraciones (`enum`)** para tomar decisiones sobre el comportamiento en tiempo de ejecución.

Esto genera una serie de problemas de diseño:

* **Violación del principio OCP (Open/Closed Principle):**
  Cada vez que se agrega un nuevo tipo de vino o descuento, se debe modificar el código existente.

* **Violación del principio SRP (Single Responsibility Principle):**
  Las clases hacen demasiadas cosas: calculan precios, registran logs y definen reglas de almacenamiento.

* **Violación del principio DIP (Dependency Inversion Principle):**
  La clase `InventoryService` depende directamente de implementaciones concretas y no de abstracciones.

---

## 💀 Código Malo (Versión Original)

```csharp
/*
  BadCode_Inventory.cs
  Problema: Se usan bool y enum para decidir qué comportamiento seguir en tiempo de ejecución.
  ❌ → Puede resolverse con State o Strategy.
*/

using System;
using System.Collections.Generic;

namespace VineyardInventoryBad
{
    public enum WineType
    {
        Red,
        White,
        Sparkling,
        Rose
    }

    public enum DiscountType
    {
        None,
        Seasonal,
        Volume
    }

    public class Wine
    {
        public string Name { get; set; }
        public WineType Type { get; set; }
        public double Price { get; set; }
        public bool IsImported { get; set; }
        public bool IsLimitedEdition { get; set; }
    }

    public class InventoryService
    {
        private List<Wine> wines = new();

        public void AddWine(Wine wine, bool notifyCustomers)
        {
            wines.Add(wine);
            Console.WriteLine($"Agregado vino: {wine.Name}");

            if (notifyCustomers)
            {
                Console.WriteLine("📢 Notificando clientes sobre nuevo vino...");
            }
        }

        public double CalculatePrice(Wine wine, DiscountType discountType, bool includeTax)
        {
            double finalPrice = wine.Price;

            switch (discountType)
            {
                case DiscountType.None:
                    break;
                case DiscountType.Seasonal:
                    finalPrice -= wine.Price * 0.10;
                    Console.WriteLine($"Descuento de temporada aplicado a {wine.Name}");
                    break;
                case DiscountType.Volume:
                    if (wine.IsLimitedEdition)
                        Console.WriteLine($"No se puede aplicar descuento de volumen a {wine.Name}");
                    else
                        finalPrice -= wine.Price * 0.20;
                    break;
            }

            if (wine.IsImported)
            {
                finalPrice += 15;
            }

            if (includeTax)
            {
                finalPrice *= 1.16;
            }

            Console.WriteLine($"Precio final de {wine.Name}: {finalPrice:C2}");
            return finalPrice;
        }

        public void StoreWine(Wine wine, bool logEnabled)
        {
            switch (wine.Type)
            {
                case WineType.Red:
                    Console.WriteLine($"Guardando {wine.Name} en bodega fresca (12°C)");
                    break;
                case WineType.White:
                    Console.WriteLine($"Guardando {wine.Name} en refrigeración (8°C)");
                    break;
                case WineType.Sparkling:
                    Console.WriteLine($"Guardando {wine.Name} en cava presurizada");
                    break;
                case WineType.Rose:
                    Console.WriteLine($"Guardando {wine.Name} en temperatura controlada (10°C)");
                    break;
            }

            if (logEnabled)
            {
                Console.WriteLine($"[LOG] {wine.Name} almacenado como {wine.Type}");
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var inventory = new InventoryService();

            var wines = new List<Wine>
            {
                new Wine { Name = "Cabernet Sauvignon", Type = WineType.Red, Price = 200, IsImported = false, IsLimitedEdition = true },
                new Wine { Name = "Chardonnay", Type = WineType.White, Price = 180, IsImported = true, IsLimitedEdition = false },
                new Wine { Name = "Rosé Provence", Type = WineType.Rose, Price = 220, IsImported = true, IsLimitedEdition = true },
            };

            foreach (var w in wines)
            {
                inventory.AddWine(w, notifyCustomers: true);
                inventory.CalculatePrice(w, DiscountType.Seasonal, includeTax: true);
                inventory.StoreWine(w, logEnabled: true);
                Console.WriteLine("---------------------------------------------------");
            }
        }
    }
}
```

---

## ⚙️ Análisis de los Problemas

| Tipo de problema | Descripción                                                                    |
| ---------------- | ------------------------------------------------------------------------------ |
| Uso de `enum`    | Cada nuevo tipo de vino o descuento obliga a modificar `switch`, violando OCP. |
| Uso de `bool`    | Causa comportamientos condicionales difíciles de mantener.                     |
| SRP              | `InventoryService` maneja lógica de negocio, presentación y logging.           |
| DIP              | No hay interfaces, todo depende de implementaciones concretas.                 |

---

## ✅ Solución: Uso del Patrón **Strategy**

### 🧠 Idea principal

* Crear **estrategias de descuento** (`IDiscountStrategy`).
* Crear **estrategias de almacenamiento** (`IStorageStrategy`).
* Cada tipo de vino y descuento define su propio comportamiento.
* Se eliminan los `enum` y los `bool`.

---

## 💡 Código Bueno (Versión Refactorizada con Strategy)

```csharp
using System;
using System.Collections.Generic;

namespace VineyardInventoryGood
{
    // Estrategia para descuentos
    public interface IDiscountStrategy
    {
        double ApplyDiscount(Wine wine);
    }

    public class NoDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(Wine wine) => wine.Price;
    }

    public class SeasonalDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(Wine wine)
        {
            Console.WriteLine($"Descuento de temporada aplicado a {wine.Name}");
            return wine.Price * 0.9;
        }
    }

    public class VolumeDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(Wine wine)
        {
            if (wine.IsLimitedEdition)
            {
                Console.WriteLine($"No se puede aplicar descuento de volumen a {wine.Name}");
                return wine.Price;
            }

            Console.WriteLine($"Descuento por volumen aplicado a {wine.Name}");
            return wine.Price * 0.8;
        }
    }

    // Estrategia para almacenamiento
    public interface IStorageStrategy
    {
        void Store(Wine wine);
    }

    public class RedWineStorage : IStorageStrategy
    {
        public void Store(Wine wine) => Console.WriteLine($"Guardando {wine.Name} en bodega fresca (12°C)");
    }

    public class WhiteWineStorage : IStorageStrategy
    {
        public void Store(Wine wine) => Console.WriteLine($"Guardando {wine.Name} en refrigeración (8°C)");
    }

    public class SparklingWineStorage : IStorageStrategy
    {
        public void Store(Wine wine) => Console.WriteLine($"Guardando {wine.Name} en cava presurizada");
    }

    public class RoseWineStorage : IStorageStrategy
    {
        public void Store(Wine wine) => Console.WriteLine($"Guardando {wine.Name} en temperatura controlada (10°C)");
    }

    // Clase Wine
    public class Wine
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsImported { get; set; }
        public bool IsLimitedEdition { get; set; }
        public IStorageStrategy StorageStrategy { get; set; }
    }

    // Servicio de Inventario
    public class InventoryService
    {
        private List<Wine> wines = new();

        public void AddWine(Wine wine)
        {
            wines.Add(wine);
            Console.WriteLine($"Agregado vino: {wine.Name}");
        }

        public double CalculatePrice(Wine wine, IDiscountStrategy discountStrategy)
        {
            double price = discountStrategy.ApplyDiscount(wine);

            if (wine.IsImported)
                price += 15;

            price *= 1.16; // IVA
            Console.WriteLine($"Precio final de {wine.Name}: {price:C2}");
            return price;
        }

        public void StoreWine(Wine wine)
        {
            wine.StorageStrategy.Store(wine);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var inventory = new InventoryService();

            var wines = new List<Wine>
            {
                new Wine { Name = "Cabernet Sauvignon", Price = 200, IsImported = false, IsLimitedEdition = true, StorageStrategy = new RedWineStorage() },
                new Wine { Name = "Chardonnay", Price = 180, IsImported = true, IsLimitedEdition = false, StorageStrategy = new WhiteWineStorage() },
                new Wine { Name = "Rosé Provence", Price = 220, IsImported = true, IsLimitedEdition = true, StorageStrategy = new RoseWineStorage() },
            };

            foreach (var wine in wines)
            {
                inventory.AddWine(wine);
                inventory.CalculatePrice(wine, new SeasonalDiscount());
                inventory.StoreWine(wine);
                Console.WriteLine("---------------------------------------------------");
            }
        }
    }
}
```

---

## 🧾 Beneficios del Refactor

| Aspecto              | Antes                                    | Después                                                       |
| -------------------- | ---------------------------------------- | ------------------------------------------------------------- |
| **Flexibilidad**     | Cada nuevo tipo requería editar `switch` | Se agrega una nueva estrategia sin modificar código existente |
| **Mantenibilidad**   | Código acoplado y con lógica condicional | Código desacoplado y extensible                               |
| **Principios SOLID** | Violados                                 | Cumplidos (SRP, OCP, DIP)                                     |
| **Legibilidad**      | Dificultad para seguir la lógica         | Estructura clara y organizada                                 |

---

## 🏁 Conclusión

El problema original de depender de **`bool` y `enum`** para tomar decisiones en tiempo de ejecución se resuelve aplicando **Strategy** (o alternativamente **State**, si el comportamiento varía con el tiempo).
Con este patrón, el sistema puede extenderse fácilmente, eliminando la necesidad de condicionales complejas y mejorando la mantenibilidad.
