# Refactor Creacional — Constructor con lógica pesada (#5)

**Alumno:** Jocelin Maribel Bernal Enciso  
**Número de control:** 21211919  
**Materia:** Patrones de Diseño de Software (DSF-2101 SC8A)  
**Docente:** René Solís Reyes  

---

## Objetivo
Aplicar lo aprendido sobre patrones creacionales (GoF) mediante la detección de code smells y su refactorización.  
Tema asignado: **Constructor con lógica pesada**.

---

## Problemas detectados

1. **Constructor con lógica pesada:** la clase `VehicleServiceBad` concentra múltiples tareas (carga de datos, validación, creación de objetos y salida a consola).  
2. **Violación del Principio de Responsabilidad Única (SRP):** el constructor asume responsabilidades de lógica de negocio, de entrada/salida y de construcción.  
3. **Instanciación directa:** se usan `switch` y `new` para acoplar al cliente con las clases concretas.  
4. **Singleton inseguro y mutable:** `AppSettingsBad` expone estado mutable y no controla concurrencia.  
5. **Testabilidad deficiente:** el acoplamiento impide realizar pruebas unitarias independientes.

---

## Código Antipatrón (BadCode.cs)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace Practica.Vehiculos.Bad
{
    public class AppSettingsBad
    {
        // Singleton inseguro y mutable
        public static AppSettingsBad Instance = new AppSettingsBad();
        public string ColorDefault = "gris";
        private AppSettingsBad() {}
    }

    public class AutoBad
    {
        public string Color;
        public int Puertas;
        public string Motor;
        public override string ToString() => $"Auto({Color},{Puertas},{Motor})";
    }

    public class MotoBad
    {
        public string Color;
        public string Motor;
        public override string ToString() => $"Moto({Color},{Motor})";
    }

    public class VehicleServiceBad
    {
        // Constructor pesado: hace múltiples tareas al mismo tiempo
        public VehicleServiceBad(string tipo, IEnumerable<string> entrada)
        {
            Console.WriteLine("[ctor] Cargando datos...");
            var raw = new List<string>(entrada);

            Console.WriteLine("[ctor] Normalizando...");
            for (int i = 0; i < raw.Count; i++)
                raw[i] = raw[i].Trim().ToLowerInvariant();

            Console.WriteLine("[ctor] Creando objeto...");
            object vehiculo;
            switch (tipo.ToLowerInvariant())
            {
                case "auto":
                    var partesA = raw[0].Split(',');
                    vehiculo = new AutoBad {
                        Color = partesA[0],
                        Puertas = int.Parse(partesA[1]),
                        Motor = partesA[2]
                    };
                    break;
                case "moto":
                    var partesM = raw[1].Split(',');
                    vehiculo = new MotoBad {
                        Color = partesM[0],
                        Motor = partesM[1]
                    };
                    break;
                default:
                    vehiculo = new AutoBad { Color = "negro", Puertas = 4, Motor = "gasolina" };
                    break;
            }

            Console.WriteLine("Vehículo generado: " + vehiculo);
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var datos = new List<string>{ "Rojo,4,gasolina", "Azul,electrica" };
            var servicio = new VehicleServiceBad("auto", datos);
        }
    }
}
```

---

## Refactor con Patrones Creacionales (Refactor.cs)

### Cambios principales
- **Builder:** separa la construcción compleja de `VehicleSpec`.  
- **Factory Method:** elimina el uso de `switch/new` en el cliente.  
- **Singleton seguro:** implementación thread-safe e inmutable de `AppConfig`.  
- **Servicio ligero:** constructor limpio y orientado a la responsabilidad única.  

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace Practica.Vehiculos.Refactor
{
    public sealed class AppConfig
    {
        private static readonly object _lock = new();
        private static AppConfig? _instance;
        public static AppConfig Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new AppConfig("memoria", "gris");
                }
            }
        }
        public string Fuente { get; }
        public string ColorDefault { get; }
        private AppConfig(string fuente, string colorDefault)
        { Fuente = fuente; ColorDefault = colorDefault; }
    }

    public class VehicleSpec
    {
        public string Tipo { get; init; } = "";
        public string Color { get; init; } = "";
        public int Puertas { get; init; } = 0;
        public string Motor { get; init; } = "";
        public override string ToString() => $"VehicleSpec({Tipo},{Color},{Puertas},{Motor})";
    }

    public interface IVehicleBuilder
    {
        void Reset();
        void SetTipo(string tipo);
        void SetColor(string color);
        void SetPuertas(int puertas);
        void SetMotor(string motor);
        VehicleSpec Build();
    }

    public class CarBuilder : IVehicleBuilder
    {
        private string _color = AppConfig.Instance.ColorDefault;
        private int _puertas = 4;
        private string _motor = "gasolina";
        public void Reset(){ _color="gris"; _puertas=4; _motor="gasolina"; }
        public void SetTipo(string tipo) { }
        public void SetColor(string color) => _color=color;
        public void SetPuertas(int puertas) => _puertas=puertas;
        public void SetMotor(string motor) => _motor=motor;
        public VehicleSpec Build()=> new(){Tipo="auto",Color=_color,Puertas=_puertas,Motor=_motor};
    }

    public class MotoBuilder : IVehicleBuilder
    {
        private string _color = AppConfig.Instance.ColorDefault;
        private string _motor = "gasolina";
        public void Reset(){ _color="gris"; _motor="gasolina"; }
        public void SetTipo(string tipo) { }
        public void SetColor(string color)=>_color=color;
        public void SetPuertas(int _){ }
        public void SetMotor(string motor)=>_motor=motor;
        public VehicleSpec Build()=> new(){Tipo="moto",Color=_color,Puertas=0,Motor=_motor};
    }

    public abstract class VehicleCreator
    { public abstract IVehicleBuilder CreateBuilder(); }
    public class CarCreator : VehicleCreator
    { public override IVehicleBuilder CreateBuilder()=> new CarBuilder(); }
    public class MotoCreator : VehicleCreator
    { public override IVehicleBuilder CreateBuilder()=> new MotoBuilder(); }

    public class VehicleService
    {
        public List<VehicleSpec> BuildFromRaw(IEnumerable<string> raw, VehicleCreator creator)
        {
            var builder = creator.CreateBuilder();
            var result = new List<VehicleSpec>();
            foreach (var csv in raw)
            {
                var tokens = csv.Split(',');
                builder.Reset();
                builder.SetColor(tokens[0]);
                if(tokens.Length>2) builder.SetPuertas(int.Parse(tokens[1]));
                builder.SetMotor(tokens.Last());
                result.Add(builder.Build());
            }
            return result;
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var raw = new List<string>{ "Rojo,4,gasolina", "Azul,electrica" };
            var service = new VehicleService();
            var autos = service.BuildFromRaw(new[]{raw[0]}, new CarCreator());
            var motos = service.BuildFromRaw(new[]{raw[1]}, new MotoCreator());
            Console.WriteLine(string.Join(" | ", autos));
            Console.WriteLine(string.Join(" | ", motos));
        }
    }
}
```

---

## Justificación técnica

### Problemas detectados
- La clase `VehicleServiceBad` violaba el principio de responsabilidad única.  
- El uso de `switch/new` obligaba al cliente a depender de implementaciones concretas.  
- El Singleton era inseguro, mutable y no garantizaba consistencia en entornos concurrentes.  

### Patrones aplicados
- **Builder:** organiza la construcción paso a paso de los objetos complejos.  
- **Factory Method:** encapsula la creación y elimina los condicionales de selección de tipo.  
- **Singleton seguro:** garantiza una única instancia inmutable, segura en concurrencia y reutilizable.  

### Beneficios obtenidos
- Mayor cohesión y claridad en las responsabilidades de cada clase.  
- Facilidad para realizar pruebas unitarias independientes.  
- Flexibilidad para extender el sistema con nuevos tipos de vehículos sin modificar código existente.  

---

## Impacto

- Cumplimiento del **principio de inversión de dependencias (DIP)**.  
- Objetos creados consistentes y listos para ser utilizados.  
- Código preparado para pruebas unitarias, extensibilidad y mantenimiento a largo plazo.  

---

## Evidencia de ejecución

A continuación se muestran capturas de pantalla del código ejecutado en el compilador en línea:

### Código con antipatrón (BadCode.cs)
El sistema genera un vehículo directamente desde el constructor, con lógica pesada y sin separación de responsabilidades:

<img width="1332" height="944" alt="image" src="https://github.com/user-attachments/assets/6e6dd7f7-051b-48be-a596-dce9a6b2f9e5" />


### Código refactorizado (GoodCode.cs)
El sistema utiliza `Builder`, `Factory Method` y un `Singleton` seguro para crear objetos consistentes y extensibles:

<img width="1288" height="1012" alt="image" src="https://github.com/user-attachments/assets/1e432bbc-618c-4e9a-9975-f343499c83d8" />


---

## Conclusión

El antipatrón surge cuando un constructor concentra demasiadas responsabilidades, generando acoplamiento, baja cohesión y dificultad para realizar pruebas.  
La refactorización con Builder, Factory Method y Singleton seguro devuelve claridad, reutilización y calidad al diseño, asegurando una arquitectura extensible y preparada para cambios futuros.
