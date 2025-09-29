# 📖 Refactorización de Vehículos con Patrones GoF

## 📝 Formato del Pull Request

### 🔍 Problemas detectados
- La clase `VehiculoManager` viola el principio de responsabilidad única (SRP) al encargarse tanto de la gestión como de la creación de vehículos.
- Se detecta una instancia directa (`new`) que debería ser gestionada a través de un **Factory Method** para mejorar la extensibilidad.
- El **Singleton** actual es inseguro en entornos multihilo y mantiene un estado mutable compartido, lo cual introduce riesgos de concurrencia.

### 🛠 Patrones aplicados
- **Factory Method** → para encapsular la creación de objetos `IVehiculo` y permitir extender el sistema sin modificar la clase principal.
- **Builder** → para separar la construcción compleja de vehículos (ejemplo: color, motor, ruedas) de su representación final.
- Se eliminó el **Singleton mutable**, migrando la gestión hacia clases que reciben dependencias (**Inversión de Dependencias - DIP**).

### 💡 Justificación del cambio
Con este refactor:
- **Cohesión interna**: cada clase tiene una única responsabilidad (SRP).
- **Testabilidad**: al reemplazar el Singleton por inyección de dependencias, es posible probar con mocks y stubs fácilmente.
- **Flexibilidad ante cambios**: agregar un nuevo tipo de vehículo ya no requiere modificar la lógica central, solo crear una nueva Factory o extender el Builder.

### 🔄 Impacto
- Se asegura el cumplimiento del principio de inversión de dependencias (DIP).
- La arquitectura queda preparada para pruebas unitarias automatizadas.
- Se reducen riesgos en entornos concurrentes al remover el estado global mutable.
- Se logra un diseño más extensible y abierto a nuevos tipos de vehículos sin tocar código existente.

### 📌 Próximos pasos sugeridos
- Extender el Builder para permitir configurar más atributos del vehículo (ejemplo: tipo de motor, transmisión, color).
- Explorar la integración del patrón **Prototype** para clonar vehículos con configuraciones base.

---

## 💻 Código de ejemplo

### 🚨 Código con malas prácticas
```csharp
using System;
using System.Collections.Generic;

namespace VehiculosApp
{
    // Singleton con estado mutable (peligroso en sistemas multihilo o compartidos)
    public class VehiculoManager
    {
        private static VehiculoManager instancia;
        public List<string> Vehiculos = new List<string>(); // Estado mutable compartido

        private VehiculoManager() { }

        public static VehiculoManager GetInstance()
        {
            if (instancia == null)
            {
                instancia = new VehiculoManager();
            }
            return instancia;
        }

        public void AgregarVehiculo(string tipo)
        {
            Vehiculos.Add(tipo);
        }

        public void MostrarVehiculos()
        {
            Console.WriteLine("Vehículos registrados:");
            foreach (var v in Vehiculos)
            {
                Console.WriteLine("- " + v);
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var gestor1 = VehiculoManager.GetInstance();
            gestor1.AgregarVehiculo("Auto");

            var gestor2 = VehiculoManager.GetInstance();
            gestor2.AgregarVehiculo("Moto");

            gestor1.MostrarVehiculos();

            // 🚨 gestor1 y gestor2 son la misma instancia,
            // y su estado compartido es mutable → bugs en sistemas concurrentes.
        }
    }
}
```
### Código con Buenas prácticas

```csharp
using System;
using System.Collections.Generic;

namespace VehiculosApp
{
    // 🚀 Abstracción de un Vehículo (Interfaz)
    public interface IVehiculo
    {
        void MostrarInfo();
    }

    // 🏎️ Clases concretas de vehículos
    public class Auto : IVehiculo
    {
        public void MostrarInfo() => Console.WriteLine("Soy un Auto 🚗");
    }

    public class Moto : IVehiculo
    {
        public void MostrarInfo() => Console.WriteLine("Soy una Moto 🏍️");
    }

    // 🏭 Factory Method: encapsula la creación de vehículos
    public abstract class VehiculoFactory
    {
        public abstract IVehiculo CrearVehiculo();
    }

    public class AutoFactory : VehiculoFactory
    {
        public override IVehiculo CrearVehiculo() => new Auto();
    }

    public class MotoFactory : VehiculoFactory
    {
        public override IVehiculo CrearVehiculo() => new Moto();
    }

    // 📋 Gestor de vehículos: elimina Singleton y recibe dependencias
    public class VehiculoManager
    {
        private readonly List<IVehiculo> vehiculos = new List<IVehiculo>();

        // Agrega un vehículo usando la Factory correspondiente
        public void AgregarVehiculo(VehiculoFactory factory)
        {
            var vehiculo = factory.CrearVehiculo();
            vehiculos.Add(vehiculo);
        }

        // Muestra todos los vehículos registrados
        public void MostrarVehiculos()
        {
            Console.WriteLine("Vehículos registrados:");
            foreach (var v in vehiculos)
            {
                v.MostrarInfo();
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var gestor = new VehiculoManager();

            // ✅ Uso del Factory Method para crear vehículos
            gestor.AgregarVehiculo(new AutoFactory());
            gestor.AgregarVehiculo(new MotoFactory());

            gestor.MostrarVehiculos();
        }
    }
}

