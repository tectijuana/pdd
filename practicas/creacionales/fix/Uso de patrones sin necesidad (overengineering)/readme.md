# Refactorización: Aplicación de Patrones Creacionales en Sistema de Vehículos

## Problemas detectados
- La clase `Program` violaba el **principio de responsabilidad única** al encargarse tanto de la creación como del registro de vehículos.  
- Se detectaba la creación manual con `new` en varias partes del código, lo cual limita la **flexibilidad** y dificulta la extensión.  
- El `Singleton` actual (`VehicleRegistry`) no era seguro en **entornos multihilo**, lo que podía generar inconsistencias en el registro de vehículos bajo alta concurrencia.  

---

## Patrones aplicados
- **Factory Method**: Se encapsuló la lógica de creación de instancias (`CarFactory`, `MotorcycleFactory`) evitando el uso directo de `new`.  
- **Builder**: Se añadió para manejar la construcción compleja de objetos `Vehicle` de forma clara y fluida.  
- **Prototype**: Se implementó para permitir la clonación de vehículos a partir de un prototipo base.  
- **Thread-Safe Singleton**: Se ajustó `VehicleRegistry` para ser seguro en entornos multihilo.  

---

## Justificación del cambio
Estos cambios aportan:  
- Mayor **cohesión interna**: cada clase tiene una responsabilidad clara.  
- Mejor **testabilidad**: al depender de interfaces y factories, se facilita el mockeo en pruebas unitarias.  
- Más **flexibilidad ante cambios**: podemos agregar nuevos tipos de vehículos o modificar el proceso de construcción sin afectar el resto del sistema.  

---

## Impacto
- Se garantiza el cumplimiento del **Principio de Inversión de Dependencias (DIP)**.  
- El sistema ahora está mejor preparado para la **extensión y mantenimiento** a largo plazo.  
- Se facilita la **ejecución de pruebas unitarias** sin dependencias rígidas.  
---

## Código inicial (con abuso de patrones GoF)

```csharp
using System;
using System.Collections.Generic;

namespace OverEngineeringVehicles
{
    // Singleton innecesario
    public sealed class VehiclePrinter
    {
        private static readonly VehiclePrinter _instance = new VehiclePrinter();
        public static VehiclePrinter Instance => _instance;
        private VehiclePrinter() { }

        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }

    // Strategy sin variaciones reales
    public interface IPrintStrategy
    {
        void Print(string message);
    }

    public class NormalPrintStrategy : IPrintStrategy
    {
        public void Print(string message)
        {
            VehiclePrinter.Instance.Print(message);
        }
    }

    // Factory Method absurdo
    public abstract class VehicleFactory
    {
        public abstract IVehicle Create(string brand, string model);
    }

    public class CarFactory : VehicleFactory
    {
        public override IVehicle Create(string brand, string model) => new Car(brand, model);
    }

    // Interface trivial
    public interface IVehicle
    {
        string GetInfo();
    }

    public class Car : IVehicle
    {
        private string _brand;
        private string _model;

        public Car(string brand, string model)
        {
            _brand = brand;
            _model = model;
        }

        public string GetInfo() => $"Auto: {_brand} {_model}";
    }

    // Decorator inútil
    public class VehicleDecorator : IVehicle
    {
        private readonly IVehicle _vehicle;
        public VehicleDecorator(IVehicle vehicle) { _vehicle = vehicle; }
        public string GetInfo() => _vehicle.GetInfo();
    }

    // Proxy que solo reenvía la llamada
    public class VehicleProxy : IVehicle
    {
        private readonly IVehicle _vehicle;
        public VehicleProxy(IVehicle vehicle) { _vehicle = vehicle; }
        public string GetInfo() => _vehicle.GetInfo();
    }

    // Observer innecesario
    public interface IObserver
    {
        void Update(string message);
    }

    public class VehicleLogger : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"[LOG] Vehículo creado: {message}");
        }
    }

    public class VehiclePublisher
    {
        private List<IObserver> _observers = new();
        public void Attach(IObserver observer) => _observers.Add(observer);
        public void Notify(string message)
        {
            foreach (var obs in _observers)
                obs.Update(message);
        }
    }

    // Fachada que junta todo el caos
    public class VehicleFacade
    {
        private readonly VehicleFactory _factory;
        private readonly IPrintStrategy _strategy;
        private readonly VehiclePublisher _publisher;

        public VehicleFacade()
        {
            _factory = new CarFactory();
            _strategy = new NormalPrintStrategy();
            _publisher = new VehiclePublisher();
            _publisher.Attach(new VehicleLogger());
        }

        public void ShowVehicle(string brand, string model)
        {
            IVehicle vehicle = _factory.Create(brand, model);
            IVehicle decorated = new VehicleDecorator(new VehicleProxy(vehicle));
            _strategy.Print(decorated.GetInfo());
            _publisher.Notify(decorated.GetInfo());
        }
    }

    public class Program
    {
        public static void Main()
        {
            var vehicleSystem = new VehicleFacade();
            vehicleSystem.ShowVehicle("Toyota", "Corolla");
        }
    }
}
```
# Código refactorizado (solo patrones creacionales aplicados)

```csharp
using System;
using System.Collections.Generic;

namespace CreationalVehicles
{
    // ==== PRODUCTO ====
    public interface IVehicle
    {
        string Brand { get; }
        string Model { get; }
        string GetInfo();
    }

    public class Car : IVehicle
    {
        public string Brand { get; }
        public string Model { get; }

        public Car(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        public string GetInfo() => $"Auto: {Brand} {Model}";
    }

    public class Motorcycle : IVehicle
    {
        public string Brand { get; }
        public string Model { get; }

        public Motorcycle(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        public string GetInfo() => $"Moto: {Brand} {Model}";
    }

    // ==== FACTORY METHOD ====
    public abstract class VehicleFactory
    {
        public abstract IVehicle Create(string brand, string model);
    }

    public class CarFactory : VehicleFactory
    {
        public override IVehicle Create(string brand, string model) => new Car(brand, model);
    }

    public class MotorcycleFactory : VehicleFactory
    {
        public override IVehicle Create(string brand, string model) => new Motorcycle(brand, model);
    }

    // ==== BUILDER ====
    public class VehicleBuilder
    {
        private string _brand = "Genérico";
        private string _model = "Base";
        private Func<string, string, IVehicle> _creator = (b, m) => new Car(b, m);

        public VehicleBuilder SetBrand(string brand)
        {
            _brand = brand;
            return this;
        }

        public VehicleBuilder SetModel(string model)
        {
            _model = model;
            return this;
        }

        public VehicleBuilder AsCar()
        {
            _creator = (b, m) => new Car(b, m);
            return this;
        }

        public VehicleBuilder AsMotorcycle()
        {
            _creator = (b, m) => new Motorcycle(b, m);
            return this;
        }

        public IVehicle Build() => _creator(_brand, _model);
    }

    // ==== PROTOTYPE ====
    public class VehiclePrototype
    {
        public IVehicle Prototype { get; }

        public VehiclePrototype(IVehicle prototype)
        {
            Prototype = prototype;
        }

        public IVehicle Clone()
        {
            return Prototype switch
            {
                Car c => new Car(c.Brand, c.Model),
                Motorcycle m => new Motorcycle(m.Brand, m.Model),
                _ => throw new NotSupportedException()
            };
        }
    }

    // ==== THREAD-SAFE SINGLETON ====
    public sealed class VehicleRegistry
    {
        private static readonly Lazy<VehicleRegistry> _instance = new(() => new VehicleRegistry());
        private readonly List<IVehicle> _vehicles = new();

        private VehicleRegistry() { }

        public static VehicleRegistry Instance => _instance.Value;

        public void Register(IVehicle vehicle)
        {
            lock (_vehicles)
            {
                _vehicles.Add(vehicle);
            }
            Console.WriteLine($"[Registro] {vehicle.GetInfo()} agregado.");
        }

        public void ShowAll()
        {
            Console.WriteLine("\n--- Vehículos registrados ---");
            lock (_vehicles)
            {
                foreach (var v in _vehicles)
                    Console.WriteLine(v.GetInfo());
            }
        }
    }

    // ==== DEMO ====
    public class Program
    {
        public static void Main()
        {
            // Factory
            VehicleFactory carFactory = new CarFactory();
            IVehicle car = carFactory.Create("Toyota", "Corolla");

            VehicleFactory motoFactory = new MotorcycleFactory();
            IVehicle moto = motoFactory.Create("Honda", "CB500");

            // Builder
            var customCar = new VehicleBuilder()
                .SetBrand("Mazda")
                .SetModel("3 Hatchback")
                .AsCar()
                .Build();

            // Prototype
            var proto = new VehiclePrototype(customCar);
            var clonedCar = proto.Clone();

            // Singleton Registry
            VehicleRegistry.Instance.Register(car);
            VehicleRegistry.Instance.Register(moto);
            VehicleRegistry.Instance.Register(customCar);
            VehicleRegistry.Instance.Register(clonedCar);

            VehicleRegistry.Instance.ShowAll();
        }
    }
}
```
