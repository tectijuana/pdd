<div align="center">
<img width="500" height="500"  alt="image" src="https://github.com/user-attachments/assets/318d860e-2b7f-49d7-9b89-964198fb0507" />
</div>

## 🐄 Práctica Integral: **Sistema de Alimentación Automatizada para Granja Inteligente**

### 🎯 Objetivo General:

Refactorizar un sistema de alimentación de ganado que actualmente usa lógica rígida y mal estructurada. El estudiante debe aplicar **todos los patrones creacionales GoF** para mejorar la mantenibilidad, extensibilidad y modularidad del código.

---

## 🧩 Contexto Temático

Una granja tiene un sistema automatizado que:

* Detecta animales (vacas, cerdos, gallinas)
* Les sirve **comidas específicas**
* Les provee **bebidas y suplementos**
* Genera **rutinas completas de alimentación**
* Clona rutinas pasadas si funcionan bien (por eficiencia)
* Usa un único **registro central** para control y monitoreo (donde se aplica Singleton)

---

## 🧨 Código Inicial (Malo)

```csharp
public class FeedingSystem
{
    public void Feed(string animal)
    {
        if (animal == "Cow")
            Console.WriteLine("Feeding cow: hay, water, salt block.");
        else if (animal == "Pig")
            Console.WriteLine("Feeding pig: grains, juice.");
        else if (animal == "Chicken")
            Console.WriteLine("Feeding chicken: seeds, water.");
    }
}
```

---

## 🧠 Retos del Estudiante

| Patrón GoF              | Aplicación esperada                                                         |
| ----------------------- | --------------------------------------------------------------------------- |
| 🧪 **Factory Method**   | Crear instancias de `Dieta` para cada tipo de animal según una fábrica.     |
| 🧪 **Abstract Factory** | Crear familias: `Alimento`, `Bebida` y `Suplemento` por tipo de animal.     |
| 🧪 **Builder**          | Construir `RutinaAlimentacionCompleta` paso a paso (desayuno, snack, cena). |
| 🧪 **Prototype**        | Clonar rutinas anteriores para usarlas como base para nuevos animales.      |
| 🧪 **Singleton**        | Clase `RegistroGlobalAlimentacion` centraliza la auditoría del sistema.     |

---

## 🔍 Actividades

1. Identifica en qué parte del código mal diseñado se puede aplicar cada patrón.
2. Refactoriza sin romper la lógica de negocio.
3. Asegúrate de cumplir con los principios SOLID.
4. Documenta los cambios con comentarios en español.
5. Documenta en un GIST para la entrega

---

## 🧪 Estructura del Código Mal Diseñado (`Program.cs`)

Este es el código que los estudiantes deben refactorizar, integrando los patrones **Factory Method**, **Abstract Factory**, **Builder**, **Prototype** y **Singleton**:

```csharp
using System;
using System.Collections.Generic;

namespace GranjaInteligente
{
    public class FeedingSystem
    {
        public void Alimentar(string animal)
        {
            if (animal == "Vaca")
                Console.WriteLine("Dando heno, agua y sal mineral a la vaca.");
            else if (animal == "Cerdo")
                Console.WriteLine("Dando granos y jugo al cerdo.");
            else if (animal == "Gallina")
                Console.WriteLine("Dando semillas y agua a la gallina.");
            else
                Console.WriteLine("Animal desconocido.");
        }
    }

    public class RegistroAlimentacion
    {
        private static RegistroAlimentacion instancia;
        private RegistroAlimentacion() {}

        public static RegistroAlimentacion ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new RegistroAlimentacion();

            return instancia;
        }

        public void Registrar(string mensaje)
        {
            Console.WriteLine($"[REGISTRO] {mensaje}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var sistema = new FeedingSystem();
            sistema.Alimentar("Vaca");
            sistema.Alimentar("Cerdo");

            var registro = RegistroAlimentacion.ObtenerInstancia();
            registro.Registrar("Ciclo de alimentación completado.");
        }
    }
}
```

---

## 🧰 ¿Qué debe hacer el estudiante?

1. Reemplazar `FeedingSystem` usando **Factory Method** para crear dietas.
2. Agregar una **Abstract Factory** para generar familias de objetos: `Alimento`, `Bebida`, `Suplemento`.
3. Aplicar **Builder** para generar rutinas de alimentación diaria.
4. Implementar **Prototype** para clonar rutinas exitosas y ahorrar configuración.
5. Corregir el uso **deficiente de Singleton** (hacerlo thread-safe).

---
## 🧪 Código Corregido
```c sharp
using System;
using System.Collections.Generic;

namespace GranjaInteligente
{
    // ====================================================================
    // 1. PATRÓN SINGLETON 🔒 (Implementación Segura y Centralizada)
    // ====================================================================

    /// <summary>
    /// Patrón Singleton: Garantiza una única instancia global para el registro
    /// de auditoría. Se utiliza inicialización estática para ser 'thread-safe'.
    /// </summary>
    public sealed class RegistroGlobalAlimentacion
    {
        // Instancia estática y de solo lectura. Se crea solo una vez al cargar la clase.
        private static readonly RegistroGlobalAlimentacion instancia = new RegistroGlobalAlimentacion();

        // Constructor privado para evitar la instanciación externa.
        private RegistroGlobalAlimentacion() => Console.WriteLine("[SINGLETON] Instancia de Registro Global creada.");

        /// <summary>
        /// Punto de acceso global al Singleton.
        /// </summary>
        public static RegistroGlobalAlimentacion ObtenerInstancia() 
        {
            return instancia;
        }

        /// <summary>
        /// Registra un evento en el sistema central.
        /// </summary>
        public void Registrar(string mensaje) => Console.WriteLine($"[REGISTRO] {DateTime.Now:HH:mm:ss} | {mensaje}");
    }

    // ====================================================================
    // 2. INTERFACES BASE (Para Abstract Factory y Factory Method)
    // ====================================================================

    // Producto 1: Alimento
    public interface IAlimento
    {
        string Servir();
    }

    // Producto 2: Bebida
    public interface IBebida
    {
        string Servir();
    }

    // Producto 3: Suplemento
    public interface ISuplemento
    {
        string Servir();
    }

    // Interfaz para la Dieta (El producto que crea Factory Method)
    public interface IDieta
    {
        void MostrarDieta();
    }

    // ====================================================================
    // 3. PATRÓN ABSTRACT FACTORY 🏭 (Familias de Productos)
    // ====================================================================

    /// <summary>
    /// Fábrica Abstracta: Define métodos para crear la familia de productos
    /// (Alimento, Bebida, Suplemento) por tipo de animal.
    /// </summary>
    public interface IDietaFactory
    {
        IAlimento CrearAlimento();
        IBebida CrearBebida();
        ISuplemento CrearSuplemento();
    }

    // ---------------------- Familia Vaca ------------------------
    public class Heno : IAlimento { public string Servir() => "Heno fibroso premium"; }
    public class Agua : IBebida { public string Servir() => "Agua fresca de pozo"; }
    public class SalMineral : ISuplemento { public string Servir() => "Bloque de sal mineral"; }

    public class DietaVacaFactory : IDietaFactory
    {
        public IAlimento CrearAlimento() => new Heno();
        public IBebida CrearBebida() => new Agua();
        public ISuplemento CrearSuplemento() => new SalMineral();
    }

    // ---------------------- Familia Cerdo ------------------------
    public class Granos : IAlimento { public string Servir() => "Granos proteicos enriquecidos"; }
    public class Jugo : IBebida { public string Servir() => "Jugo de verduras y suero"; }
    public class Vitamina : ISuplemento { public string Servir() => "Suplemento de vitaminas B y E"; }

    public class DietaCerdoFactory : IDietaFactory
    {
        public IAlimento CrearAlimento() => new Granos();
        public IBebida CrearBebida() => new Jugo();
        public ISuplemento CrearSuplemento() => new Vitamina();
    }

    // ====================================================================
    // 4. DIETAS CONCRETAS (Productos de Factory Method)
    // ====================================================================

    /// <summary>
    /// DietaVaca es el producto. Utiliza la Abstract Factory inyectada
    /// para asegurar la consistencia de los componentes de su dieta.
    /// </summary>
    public class DietaVaca : IDieta
    {
        private readonly IAlimento _alimento;
        private readonly IBebida _bebida;
        private readonly ISuplemento _suplemento;

        public DietaVaca(IDietaFactory factory)
        {
            _alimento = factory.CrearAlimento();
            _bebida = factory.CrearBebida();
            _suplemento = factory.CrearSuplemento();
            RegistroGlobalAlimentacion.ObtenerInstancia().Registrar("Dieta de Vaca lista.");
        }

        public void MostrarDieta() => Console.WriteLine($"VACA: Consumiendo {_alimento.Servir()}, {_bebida.Servir()} y {_suplemento.Servir()}.");
    }

    public class DietaCerdo : IDieta
    {
        private readonly IAlimento _alimento;
        private readonly IBebida _bebida;
        private readonly ISuplemento _suplemento;

        public DietaCerdo(IDietaFactory factory)
        {
            _alimento = factory.CrearAlimento();
            _bebida = factory.CrearBebida();
            _suplemento = factory.CrearSuplemento();
            RegistroGlobalAlimentacion.ObtenerInstancia().Registrar("Dieta de Cerdo lista.");
        }

        public void MostrarDieta() => Console.WriteLine($"CERDO: Consumiendo {_alimento.Servir()}, {_bebida.Servir()} y {_suplemento.Servir()}.");
    }

    // ====================================================================
    // 5. PATRÓN FACTORY METHOD 🧪 (Creadores de Dieta)
    // ====================================================================

    /// <summary>
    /// Creador Abstracto. Contiene la lógica de negocio central (IniciarAlimentacion)
    /// que es independiente del producto concreto que se crea (IDieta).
    /// </summary>
    public abstract class CreadorDieta
    {
        // El Factory Method: Lo implementan las subclases.
        public abstract IDieta CrearDieta();

        // Operación central del sistema que utiliza la Dieta.
        public void IniciarAlimentacion()
        {
            var dieta = CrearDieta(); // Aquí se usa el Factory Method
            dieta.MostrarDieta();
            RegistroGlobalAlimentacion.ObtenerInstancia().Registrar("Alimentación completada para el animal.");
        }
    }

    /// <summary>
    /// Creador Concreto Vaca: Implementa el Factory Method para crear DietaVaca.
    /// </summary>
    public class CreadorDietaVaca : CreadorDieta
    {
        public override IDieta CrearDieta()
        {
            return new DietaVaca(new DietaVacaFactory());
        }
    }

    /// <summary>
    /// Creador Concreto Cerdo: Implementa el Factory Method para crear DietaCerdo.
    /// </summary>
    public class CreadorDietaCerdo : CreadorDieta
    {
        public override IDieta CrearDieta()
        {
            return new DietaCerdo(new DietaCerdoFactory());
        }
    }

    // ====================================================================
    // 6. PATRÓN BUILDER 🧱 & PROTOTYPE 🧬 (Rutinas Complejas)
    // ====================================================================

    /// <summary>
    /// Producto Complejo que también implementa Prototype (ICloneable).
    /// </summary>
    public class RutinaAlimentacionCompleta : ICloneable
    {
        public List<string> Pasos { get; set; } = new List<string>();
        public string NombreRutina { get; set; } = "Rutina Base";

        public void AgregarPaso(string paso) => Pasos.Add(paso);

        public void Mostrar()
        {
            Console.WriteLine($"\n*** Rutina: {NombreRutina} ***");
            Pasos.ForEach(p => Console.WriteLine($"- {p}"));
            Console.WriteLine("******************************");
        }

        /// <summary>
        /// Implementación de Prototype: Clonación.
        /// Realiza una copia superficial y luego una copia de la lista (Deep Copy de Pasos).
        /// </summary>
        public object Clone()
        {
            RutinaAlimentacionCompleta clon = (RutinaAlimentacionCompleta)this.MemberwiseClone();
            // Aseguramos una copia profunda de la lista de Pasos para que las rutinas sean independientes.
            clon.Pasos = new List<string>(this.Pasos);

            RegistroGlobalAlimentacion.ObtenerInstancia().Registrar($"Rutina '{this.NombreRutina}' clonada exitosamente.");
            return clon;
        }
    }

    // Interfaz del Builder
    public interface IRutinaBuilder
    {
        void Reset();
        void ConstruirDesayuno();
        void ConstruirSnack();
        void ConstruirCena();
        RutinaAlimentacionCompleta ObtenerRutina();
    }

    // Builder Concreto
    public class RutinaNormalBuilder : IRutinaBuilder
    {
        private RutinaAlimentacionCompleta _rutina;

        public RutinaNormalBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._rutina = new RutinaAlimentacionCompleta();
        }

        public void ConstruirDesayuno()
        {
            this._rutina.AgregarPaso("Paso 1 (Desayuno): Servir ración alta en fibra.");
        }

        public void ConstruirSnack()
        {
            this._rutina.AgregarPaso("Paso 2 (Snack): Suplemento vitamínico de medio día.");
        }

        public void ConstruirCena()
        {
            this._rutina.AgregarPaso("Paso 3 (Cena): Ración final de proteínas y bebida.");
        }

        public RutinaAlimentacionCompleta ObtenerRutina()
        {
            var resultado = this._rutina;
            this.Reset(); // Limpiar el Builder para el siguiente uso
            return resultado;
        }
    }

    // Director: Define el orden de construcción
    public class DirectorRutina
    {
        private IRutinaBuilder _builder;

        public IRutinaBuilder Builder
        {
            set { _builder = value; }
        }

        // Método para construir una rutina completa
        public void ConstruirRutinaEstandar()
        {
            _builder.ConstruirDesayuno();
            _builder.ConstruirSnack();
            _builder.ConstruirCena();
        }
    }

    // ====================================================================
    // 7. PROGRAMA PRINCIPAL (DEMOSTRACIÓN DE USO)
    // ====================================================================

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine(" DEMOSTRACIÓN DEL SISTEMA DE ALIMENTACIÓN REFACTORIZADO ");

            // 🔒 USO DE SINGLETON
            var registro = RegistroGlobalAlimentacion.ObtenerInstancia();
            registro.Registrar("Inicio del proceso de alimentación.");
            Console.WriteLine("==================================================");

            // --- USO DEL FACTORY METHOD Y ABSTRACT FACTORY ---
            Console.WriteLine("\n--- 1. Alimentación (Factory Method + Abstract Factory) ---");

            CreadorDieta creadorVaca = new CreadorDietaVaca();
            creadorVaca.IniciarAlimentacion();

            CreadorDieta creadorCerdo = new CreadorDietaCerdo();
            creadorCerdo.IniciarAlimentacion();

            registro.Registrar("Dietas servidas usando patrones de creación.");

            // --- USO DEL BUILDER Y DIRECTOR ---
            Console.WriteLine("\n--- 2. Construcción de Rutina (Builder) ---");
            var director = new DirectorRutina();
            var builder = new RutinaNormalBuilder();
            director.Builder = builder;

            // Construcción controlada paso a paso
            director.ConstruirRutinaEstandar();
            var rutinaExitosaVaca = builder.ObtenerRutina();
            rutinaExitosaVaca.NombreRutina = "Rutina Vaca Élite (Exitosa)";
            rutinaExitosaVaca.Mostrar();

            // --- USO DEL PROTOTYPE ---
            Console.WriteLine("\n--- 3. Clonación de Rutinas (Prototype) ---");

            // Clonamos la rutina exitosa para un nuevo grupo de cerdos, ahorrando configuración.
            var rutinaClonadaCerdo = (RutinaAlimentacionCompleta)rutinaExitosaVaca.Clone();
            rutinaClonadaCerdo.NombreRutina = "Rutina Cerdo Nuevo Grupo (Clonada)";

            // Personalizamos la rutina clonada (demostrando que es independiente)
            rutinaClonadaCerdo.Pasos[1] = "Paso 2 (Snack): Snack de vitaminas C y Jugo.";
            rutinaClonadaCerdo.Mostrar();

            registro.Registrar("Fin del ciclo diario.");
            Console.WriteLine("==================================================");
        }
    }
}
```


