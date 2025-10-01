# Refactorización de Vehículos con Patrones Creacionales

## 🪪 Barboza Noriega Jesús Enrique - 21211913

Este documento analiza los problemas encontrados en la versión inicial (*BadCode*) del programa de vehículos y describe los patrones aplicados en la refactorización para mejorar la arquitectura.

---

🔍 **Problemas detectados**  
- La clase `Vehicle` viola el principio de responsabilidad única al intentar modelar **todos los tipos de vehículos** (eléctricos, gasolina, motos, camiones) en una sola estructura con *flags* (`IsElectric`) y campos mutuamente excluyentes (`BatteryKWh` vs `TankLiters`).  
- Uso de **constructores telescópicos** con más de 10 parámetros posicionales, lo que genera baja legibilidad, propensión a errores y dificulta el mantenimiento.  
- Se detecta la **instanciación directa con `new`** en `FleetManager.Seed`, lo que acopla la creación de objetos al cliente y dificulta la extensión.  
- **Primitivismo excesivo**: booleanos y enteros representan paquetes de seguridad y conveniencia, en lugar de objetos de valor.  
- Baja reutilización: para crear una variante (ej. mismo modelo en otro color), se repiten todos los parámetros en el constructor.

---

🛠 **Patrones aplicados**  
- **Builder** para separar la construcción compleja de un objeto `Vehicle` en pasos más claros y con validaciones de invariante.  
- **Factory Method** para centralizar la creación de *builders* según el tipo de vehículo (`ElectricCarFactory`, `GasCarFactory`).  
- **Prototype (ligero)** para clonar vehículos existentes y crear variantes seguras (ej. mismo modelo con distinto color).  
- **Objetos de valor** (`SafetyPackage`, `ConveniencePackage`) en lugar de primitivos dispersos.

---

💡 **Justificación del cambio**  
Con la refactorización se mejora:  
- **Cohesión interna**: cada clase y builder tiene una única responsabilidad clara.  
- **Legibilidad**: en vez de 13 parámetros en un constructor, se usan métodos encadenados con nombres expresivos.  
- **Testabilidad**: los builders permiten probar la construcción en pasos, y las fábricas aíslan dependencias.  
- **Extensibilidad**: agregar un nuevo tipo de vehículo (ej. híbrido) solo requiere implementar un nuevo `Factory` y `Builder`.  
- **Reutilización**: con Prototype se evita repetir largas construcciones para variantes simples.

---

🔄 **Impacto**  
- Se asegura el cumplimiento de principios SOLID, especialmente **Responsabilidad Única (SRP)** y **Inversión de Dependencias (DIP)**.  
- La arquitectura queda preparada para **pruebas unitarias** y escenarios de extensión (nuevos tipos de vehículos, paquetes adicionales).  
- Se elimina el riesgo de errores humanos por orden incorrecto de parámetros, mejorando la **robustez y mantenibilidad** del sistema.

---

📌 **Código con malas prácticas (BadCode)**

```csharp
using System;
using System.Collections.Generic;

namespace BadVehicles
{
    public class Vehicle
    {
        public string Type;
        public string Brand;
        public string Model;
        public int Year;
        public string Color;
        public int Wheels;
        public bool IsElectric;
        public int BatteryKWh;
        public int TankLiters;
        public bool HasABS;
        public bool HasAirbags;
        public bool Gps;
        public bool SportPackage;

        public Vehicle(
            string type,
            string brand,
            string model,
            int year,
            string color,
            int wheels,
            bool isElectric,
            int batteryKWh,
            int tankLiters,
            bool hasABS,
            bool hasAirbags,
            bool gps,
            bool sportPackage)
        {
            Type = type;
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Wheels = wheels;
            IsElectric = isElectric;
            BatteryKWh = batteryKWh;
            TankLiters = tankLiters;
            HasABS = hasABS;
            HasAirbags = hasAirbags;
            Gps = gps;
            SportPackage = sportPackage;
        }

        public override string ToString()
        {
            return $"{Year} {Brand} {Model} [{Type}] - Color:{Color}, Wheels:{Wheels}, " +
                   (IsElectric ? $"Battery:{BatteryKWh}kWh" : $"Tank:{TankLiters}L") +
                   $", ABS:{HasABS}, Airbags:{HasAirbags}, GPS:{Gps}, Sport:{SportPackage}";
        }
    }

    public static class FleetManager
    {
        public static List<Vehicle> Seed()
        {
            var list = new List<Vehicle>();
            list.Add(new Vehicle("Car", "Voltaro", "ZXE", 2024, "Azul", 4, true, 65, 0, true, true, true, false));
            list.Add(new Vehicle("Car", "Voltaro", "ZXE", 2024, "Rojo", 4, true, 65, 0, true, true, false, true));
            list.Add(new Vehicle("Truck", "Camio", "T900", 2020, "Blanco", 6, false, 0, 120, true, false, false, false));
            list.Add(new Vehicle("Moto", "Rayo", "200R", 2022, "Negro", 2, false, 0, 14, false, false, false, false));
            list.Add(new Vehicle("Car", "Genio", "Sport", 2023, "Gris", 4, false, 0, 55, true, true, true, true));
            return list;
        }
    }

    class Program
    {
        static void Main()
        {
            var fleet = FleetManager.Seed();
            Console.WriteLine("=== BAD CODE - Fleet ===");
            foreach (var v in fleet)
                Console.WriteLine(v);
        }
    }
}
```

---

✅ **Código refactorizado con patrones (Builder + Factory Method + Prototype)**

```csharp
using System;
using System.Collections.Generic;

namespace GoodVehicles
{
    // === Base ===
    public abstract class Vehicle
    {
        public string Kind { get; protected set; }
        public string Brand { get; protected set; }
        public string Model { get; protected set; }
        public int Year { get; protected set; }
        public string Color { get; protected set; }
        public int Wheels { get; protected set; }

        public SafetyPackage Safety { get; protected set; }
        public ConveniencePackage Convenience { get; protected set; }

        protected abstract string DescribePowertrain();

        public override string ToString()
        {
            return $"{Year} {Brand} {Model} [{Kind}] - Color:{Color}, Wheels:{Wheels}, " +
                   $"{DescribePowertrain()}, {Safety}, {Convenience}";
        }

        public Vehicle WithColor(string newColor)
        {
            var clone = (Vehicle)this.MemberwiseClone();
            clone.Color = newColor;
            return clone;
        }
    }

    public class ElectricCar : Vehicle
    {
        public int BatteryKWh { get; private set; }

        public ElectricCar() { Kind = "ElectricCar"; }
        internal void SetBattery(int kWh) => BatteryKWh = kWh;
        protected override string DescribePowertrain() => $"Battery:{BatteryKWh}kWh";
    }

    public class GasCar : Vehicle
    {
        public int TankLiters { get; private set; }

        public GasCar() { Kind = "GasCar"; }
        internal void SetTank(int liters) => TankLiters = liters;
        protected override string DescribePowertrain() => $"Tank:{TankLiters}L";
    }

    public class SafetyPackage
    {
        public bool ABS { get; }
        public bool Airbags { get; }
        public SafetyPackage(bool abs, bool airbags) { ABS = abs; Airbags = airbags; }
        public override string ToString() => $"Safety(ABS:{ABS}, Airbags:{Airbags})";
    }

    public class ConveniencePackage
    {
        public bool GPS { get; }
        public bool Sport { get; }
        public ConveniencePackage(bool gps, bool sport) { GPS = gps; Sport = sport; }
        public override string ToString() => $"Conv(GPS:{GPS}, Sport:{Sport})";
    }

    // === Builder Interface ===
    public interface IVehicleBuilder
    {
        IVehicleBuilder Brand(string brand);
        IVehicleBuilder Model(string model);
        IVehicleBuilder Year(int year);
        IVehicleBuilder Color(string color);
        IVehicleBuilder Wheels(int wheels);
        IVehicleBuilder Safety(bool abs, bool airbags);
        IVehicleBuilder Convenience(bool gps, bool sport);
        Vehicle Build();
    }

    public class ElectricCarBuilder : IVehicleBuilder
    {
        private readonly ElectricCar _car = new ElectricCar();
        public ElectricCarBuilder Battery(int kWh) { _car.SetBattery(kWh); return this; }
        public IVehicleBuilder Brand(string brand) { _car.Brand = brand; return this; }
        public IVehicleBuilder Model(string model) { _car.Model = model; return this; }
        public IVehicleBuilder Year(int year) { _car.Year = year; return this; }
        public IVehicleBuilder Color(string color) { _car.Color = color; return this; }
        public IVehicleBuilder Wheels(int wheels) { _car.Wheels = wheels; return this; }
        public IVehicleBuilder Safety(bool abs, bool airbags) { _car.Safety = new SafetyPackage(abs, airbags); return this; }
        public IVehicleBuilder Convenience(bool gps, bool sport) { _car.Convenience = new ConveniencePackage(gps, sport); return this; }
        public Vehicle Build() => _car;
    }

    public class GasCarBuilder : IVehicleBuilder
    {
        private readonly GasCar _car = new GasCar();
        public GasCarBuilder Tank(int liters) { _car.SetTank(liters); return this; }
        public IVehicleBuilder Brand(string brand) { _car.Brand = brand; return this; }
        public IVehicleBuilder Model(string model) { _car.Model = model; return this; }
        public IVehicleBuilder Year(int year) { _car.Year = year; return this; }
        public IVehicleBuilder Color(string color) { _car.Color = color; return this; }
        public IVehicleBuilder Wheels(int wheels) { _car.Wheels = wheels; return this; }
        public IVehicleBuilder Safety(bool abs, bool airbags) { _car.Safety = new SafetyPackage(abs, airbags); return this; }
        public IVehicleBuilder Convenience(bool gps, bool sport) { _car.Convenience = new ConveniencePackage(gps, sport); return this; }
        public Vehicle Build() => _car;
    }

    // === Factory Method ===
    public enum VehicleKind { ElectricCar, GasCar }
    public abstract class VehicleFactory { public abstract IVehicleBuilder CreateBuilder(); }
    public class ElectricCarFactory : VehicleFactory { public override IVehicleBuilder CreateBuilder() => new ElectricCarBuilder(); }
    public class GasCarFactory : VehicleFactory { public override IVehicleBuilder CreateBuilder() => new GasCarBuilder(); }

    // === Presets / Director ===
    public static class Presets
    {
        public static Vehicle BasicEV(string color)
        {
            return ((ElectricCarBuilder)new ElectricCarFactory().CreateBuilder())
                .Brand("Voltaro").Model("ZXE").Year(2024).Color(color).Wheels(4)
                .Safety(true, true).Convenience(true, false).Battery(65).Build();
        }

        public static Vehicle GasSedan(string color)
        {
            return ((GasCarBuilder)new GasCarFactory().CreateBuilder())
                .Brand("Genio").Model("Sedan").Year(2023).Color(color).Wheels(4)
                .Safety(true, true).Convenience(true, false).Tank(55).Build();
        }
    }

    class Program
    {
        static void Main()
        {
            var ev = Presets.BasicEV("Azul");
            var gas = Presets.GasSedan("Gris");
            var customEv = ((ElectricCarBuilder)new ElectricCarFactory().CreateBuilder())
                .Brand("Voltaro").Model("ZXE Performance").Year(2024).Color("Negro").Wheels(4)
                .Safety(true, true).Convenience(true, true).Battery(78).Build();

            var showCar = customEv.WithColor("Amarillo");

            var fleet = new List<Vehicle> { ev, gas, customEv, showCar };
            Console.WriteLine("=== GOOD CODE - Fleet ===");
            foreach (var v in fleet) Console.WriteLine(v);
        }
    }
}
```
