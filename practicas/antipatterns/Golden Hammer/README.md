# Investigación sobre el antipatrón “Golden Hammer”

## Introducción

En ingeniería de software los *patrones de diseño* proponen soluciones probadas para problemas recurrentes.  
Su contraparte son los **antipatrones**, prácticas o soluciones que parecen atractivas en un principio, pero que conducen a una mala solución y generan deuda técnica.  
Uno de los más comunes es el **Golden Hammer** o *Martillo Dorado*, que consiste en aplicar una misma herramienta, tecnología o técnica para resolver cualquier tipo de problema, incluso cuando no es apropiada.

---

## 1. Comprensión del Antipatrón

El **Golden Hammer** ocurre cuando los desarrolladores confían excesivamente en una herramienta o enfoque que les resulta familiar, y lo utilizan como solución universal.  
El nombre proviene del dicho:  
> “Si todo lo que tienes es un martillo, todo te parecerá un clavo.”

Por ejemplo, un programador que domina un framework o lenguaje (como React, C# o SQL) intenta usarlo para resolver cualquier tipo de problema, ignorando alternativas más adecuadas.

### 💡 Razones comunes por las que ocurre
- Familiaridad y comodidad con una tecnología.  
- Falta de tiempo para evaluar otras soluciones.  
- Cultura organizacional que favorece “lo que ya funciona”.  
- Ausencia de arquitecturas modulares o desacopladas.  
- Presión por entregar rápido sin análisis técnico profundo.

---

## 2. Ejemplo Técnico (en C#)

### 🔴 Ejemplo de *Golden Hammer*
Un desarrollador usa **SQL “para todo”** incluso para datos jerárquicos o semiestructurados (p. ej. JSON), creando consultas frágiles y mapeo manual costoso.

```csharp
// Ejemplo de uso excesivo de ADO.NET + SQL para todo tipo de datos
using System.Data.SqlClient;

public sealed class ProductRepository
{
    private const string ConnString = "Server=.;Database=AppDb;Trusted_Connection=True;";

    public Product? FindById(int id)
    {
        using var conn = new SqlConnection(ConnString);
        conn.Open();

        // 🔴 Inyección de SQL potencial si concatenas (hammer: usar SQL crudo para todo)
        using var cmd = new SqlCommand($"SELECT Id, Name, Price, AttributesJson FROM Products WHERE Id = {id}", conn);
        using var reader = cmd.ExecuteReader();

        if (!reader.Read()) return null;

        // 🔴 Mapeo manual y parsing ad-hoc de JSON dentro de columnas
        var name = reader.GetString(reader.GetOrdinal("Name"));
        var price = reader.GetDecimal(reader.GetOrdinal("Price"));
        var attributesJson = reader.GetString(reader.GetOrdinal("AttributesJson"));
        // ... parsear JSON a mano, lógica dispersa, acoplamiento a la BD
        return new Product(name, price /*, attributesJson parseado a mano */);
    }
}

public record Product(string Name, decimal Price);
```

### ✅ Enfoques alternativos (elegir según el dominio)

**Opción A — MongoDB (documentos) para datos semiestructurados:**

```csharp
// Uso del driver oficial de MongoDB para documentos con estructura flexible
using MongoDB.Bson;
using MongoDB.Driver;

public sealed class ProductRepository
{
    private readonly IMongoCollection<BsonDocument> _products;

    public ProductRepository(IMongoDatabase db)
    {
        _products = db.GetCollection<BsonDocument>("products");
    }

    public Product? FindById(string id)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        var doc = _products.Find(filter).FirstOrDefault();

        return doc is null
            ? null
            : new Product(doc["name"].AsString, doc["price"].ToDecimal());
    }
}

public record Product(string Name, decimal Price);
```

**Opción B — EF Core (relacional) con entidades bien modeladas:**

```csharp
// Usar EF Core para evitar mapeo manual y concentrar lógica de acceso a datos
using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

public sealed class EfProductRepository
{
    private readonly AppDbContext _db;
    public EfProductRepository(AppDbContext db) => _db = db;

    public Task<Product?> FindByIdAsync(int id) => _db.Products.FindAsync(id).AsTask();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    // Puedes modelar Value Objects/Owned Types para atributos complejos
}
```

> La idea no es “usar MongoDB/EF siempre”, sino **elegir la herramienta que mejor encaje** con la naturaleza de los datos y los requisitos no funcionales.

---

## 3. Consecuencias

El uso del antipatrón **Golden Hammer** provoca múltiples problemas a mediano y largo plazo:

| Tipo de consecuencia | Descripción |
|----------------------|-------------|
| 🔧 **Mantenimiento** | El sistema se vuelve rígido; cualquier cambio en un módulo impacta a todo el sistema. |
| 🐢 **Rendimiento** | Las soluciones forzadas generan sobrecarga innecesaria y bajo desempeño. |
| 🧩 **Escalabilidad** | Es difícil adaptar el sistema a nuevos requerimientos o tecnologías. |
| 🧠 **Deuda técnica** | Se acumulan decisiones incorrectas que incrementan el costo futuro del proyecto. |

---

## 4. Solución Correctiva

### 🧭 Buenas prácticas para evitar el Golden Hammer
1. **Analizar cada problema de forma independiente.**  
   Evalúa si la herramienta favorita realmente se adapta al caso.
2. **Adoptar una mentalidad de arquitectura flexible.**  
   Diseña sistemas con separación de responsabilidades y bajo acoplamiento.
3. **Promover la capacitación continua.**  
   Un equipo que conoce múltiples tecnologías puede elegir con criterio.
4. **Documentar decisiones técnicas.**  
   Registra las razones detrás de la elección de herramientas o patrones.
5. **Revisar código y diseño periódicamente.**  
   Las revisiones arquitectónicas previenen el abuso de un único enfoque.

### 💎 Patrones alternativos recomendados
- **Strategy Pattern:** permite intercambiar algoritmos o comportamientos en tiempo de ejecución.  
- **Factory Method:** facilita crear objetos sin acoplar el código al tipo concreto.  
- **Adapter Pattern:** ayuda a integrar componentes sin modificar su estructura interna.  
- **Dependency Injection:** favorece la flexibilidad y el reemplazo de dependencias.

---

## 5. Conclusión

El antipatrón **Golden Hammer** es un error común impulsado por la comodidad y la falta de análisis.  
Reconocerlo permite desarrollar software más adaptable, mantenible y eficiente.  
La clave está en **no enamorarse de la herramienta**, sino **entender profundamente el problema** y seleccionar la mejor solución técnica disponible.

---

### 📚 Fuentes consultadas (lectura recomendada)

- DevIQ – Golden Hammer Antipattern  
- TechTarget – Signs of a Golden Hammer Antipattern  
- Codacy Blog – Code Smells and Anti‑Patterns  
- BairesDev Blog – Software Anti‑Patterns  
