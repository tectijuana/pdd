# 🧪 Actividad de Cierre: Refactorizando Patrones Creacionales

### Codigo con errores

```csharp
using System;
using System.Collections.Generic;

namespace CreationalBadCode
{
    // 1) Problema: Dependencia fuerte con clases concretas en lugar de interfaces
    // Violation: No se sigue DIP (Principio de Inversión de Dependencias)
    public class ReportGenerator
    {
        private PdfReport report = new PdfReport(); // Acoplamiento fuerte

        public void Generate()
        {
            report.BuildHeader();
            report.BuildContent();
            report.BuildFooter();
        }
    }

    public class PdfReport
    {
        public void BuildHeader() => Console.WriteLine("Encabezado PDF");
        public void BuildContent() => Console.WriteLine("Contenido PDF");
        public void BuildFooter() => Console.WriteLine("Pie de página PDF");
    }

    // 2) Problema con Builder: pasos sin orden y posibilidad de estado inconsistente
    public class PizzaBuilder
    {
        private List<string> ingredients = new List<string>();

        public void AddSauce(string sauce) => ingredients.Add(sauce);
        public void AddCheese(string cheese) => ingredients.Add(cheese);
        public void Bake() => Console.WriteLine($"Horneando pizza con: {string.Join(", ", ingredients)}");
    }

    // Cliente llama sin orden → objeto inconsistente
    public class PizzaOrder
    {
        public void CreatePizza()
        {
            var builder = new PizzaBuilder();
            builder.Bake(); // Problema: hornear sin ingredientes
            builder.AddCheese("Mozzarella");
        }
    }

    // 3) Problema con Singleton: rompe SRP y es difícil de probar
    public class DatabaseConnection
    {
        private static DatabaseConnection instance;
        private DatabaseConnection() { }

        public static DatabaseConnection GetInstance()
        {
            if (instance == null)
                instance = new DatabaseConnection();

            return instance;
        }

        public void Query(string sql)
        {
            Console.WriteLine($"Ejecutando: {sql}");
        }
    }
}
```

---

## 📝 Formato del Pull Request


### 🔍 Problemas detectados
- Violación del Principio de Responsabilidad Única (SRP):
  - `ReportGenerator` depende directamente de `PdfReport`, rompiendo la inversión de dependencias.

- Builder mal aplicado:
  - `PizzaBuilder` permite construir en cualquier orden y genera objetos inconsistentes.

- Singleton inseguro en entornos multihilo y difícil de probar:
  * `DatabaseConnection` implementa un Singleton sin `lock`, lo que puede crear instancias múltiples.

### 🛠 Patrón aplicado
- Factory Method para eliminar el acoplamiento directo en `ReportGenerator`.
- Builder con un `Director` que asegura el orden de construcción en `Pizza`.
- Singleton thread-safe con `Lazy<T>` para manejar `DatabaseConnection`.

### 💡 Justificación del cambio
Mejoramos:
- Mejor cohesión interna: cada clase tiene una única responsabilidad.
- Testabilidad: se inyectan dependencias mediante interfaces.
- Flexibilidad: fácil extensión para nuevos tipos de reportes, pizzas o conexiones.

### 🔄 Impacto
- Cumplimiento del Principio de Inversión de Dependencias (DIP).
- Arquitectura preparada para unit testing.
- Eliminación de inconsistencias en la creación de objetos.

---

## Codigo Arreglado

```csharp
using System;
using System.Collections.Generic;

namespace CreationalGoodCode
{
    // ==========================
    // FACTORY METHOD
    // ==========================

    public interface IReport
    {
        void BuildHeader();
        void BuildContent();
        void BuildFooter();
    }

    public class PdfReport : IReport
    {
        public void BuildHeader() => Console.WriteLine("Encabezado PDF");
        public void BuildContent() => Console.WriteLine("Contenido PDF");
        public void BuildFooter() => Console.WriteLine("Pie de página PDF");
    }

    public abstract class ReportGenerator
    {
        protected abstract IReport CreateReport();

        public void Generate()
        {
            var report = CreateReport();
            report.BuildHeader();
            report.BuildContent();
            report.BuildFooter();
        }
    }

    public class PdfReportGenerator : ReportGenerator
    {
        protected override IReport CreateReport() => new PdfReport();
    }

    // ==========================
    // BUILDER + DIRECTOR
    // ==========================

    public class Pizza
    {
        public List<string> Ingredients { get; } = new List<string>();
        public void ShowPizza() => Console.WriteLine($"Pizza con: {string.Join(", ", Ingredients)}");
    }

    public interface IPizzaBuilder
    {
        void AddSauce();
        void AddCheese();
        void AddToppings();
        Pizza GetResult();
    }

    public class MargheritaPizzaBuilder : IPizzaBuilder
    {
        private Pizza pizza = new Pizza();

        public void AddSauce() => pizza.Ingredients.Add("Salsa de tomate");
        public void AddCheese() => pizza.Ingredients.Add("Mozzarella");
        public void AddToppings() => pizza.Ingredients.Add("Albahaca");
        public Pizza GetResult() => pizza;
    }

    public class PizzaDirector
    {
        private readonly IPizzaBuilder _builder;
        public PizzaDirector(IPizzaBuilder builder) => _builder = builder;

        public Pizza Construct()
        {
            _builder.AddSauce();
            _builder.AddCheese();
            _builder.AddToppings();
            return _builder.GetResult();
        }
    }

    // ==========================
    // SINGLETON THREAD-SAFE
    // ==========================

    public class DatabaseConnection
    {
        private static readonly Lazy<DatabaseConnection> instance =
            new Lazy<DatabaseConnection>(() => new DatabaseConnection());

        private DatabaseConnection() { }

        public static DatabaseConnection Instance => instance.Value;

        public void Query(string sql)
        {
            Console.WriteLine($"Ejecutando: {sql}");
        }
    }

    // ==========================
    // CLIENTE DE PRUEBA
    // ==========================
    class Program
    {
        static void Main()
        {
            // Factory Method
            ReportGenerator generator = new PdfReportGenerator();
            generator.Generate();

            // Builder
            var director = new PizzaDirector(new MargheritaPizzaBuilder());
            var pizza = director.Construct();
            pizza.ShowPizza();

            // Singleton
            DatabaseConnection.Instance.Query("SELECT * FROM Users");
        }
    }
}
```

## Kevin Eduardo Garcia Cortez 
### 21211950
