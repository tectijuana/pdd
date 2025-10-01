3🧪 Actividad de Cierre: Refactorizando Patrones Creacionales  
Nombre: Luis Felipe Torres Coto Rodarte
Num. Control: 21212368

## 🎯 Objetivo
Aplicar el patrón **Factory Method** para eliminar el uso de `if-else` anidados en la creación de objetos.

---

## 🔍 Problema Inicial
- El problema con el que haremos el codigo es `Uso de if-else anidados para selección de tipos`.

---

## 🛠 Patrón Aplicado
Se implementó el **Factory Method** mediante la clase `VehicleFactory`, que recibe un `VehicleType` y devuelve un objeto que implementa la interfaz `IVehicle`.

---

## 🔄 Impacto
-Se eliminó el uso de estructuras if-else anidadas, eliminando un code smell que dificultaba la escalabilidad del sistema.
-Se aplicó correctamente el patrón Factory Method, que permite encapsular la lógica de creación de objetos, cumpliendo con el principio abierto/cerrado (OCP): ahora es posible agregar nuevos tipos de vehículos sin modificar el código de la fábrica.
-El código también cumple con el principio de inversión de dependencias (DIP), ya que el cliente depende de una abstracción (IVehicle) y no de clases concretas.
-Esta refactorización hace que el sistema sea más mantenible, extensible y fácil de probar, ya que la lógica de creación está centralizada y desacoplada del resto del código.

---
## Codigo Inicial con error  
Con el problema anteriormente dado, cree un codigo super sencillo que crea carros con los `if-else`.
```csharp
public IVehicle CreateVehicle(string type)
{
    if (type == "car")
    {
        return new Car("Golf", "Rojo");
    }
    else if (type == "truck")
    {
        return new Truck("F-150", 6);
    }
    else if (type == "motorcycle")
    {
        return new Motorcycle("Ducati");
    }
    else
    {
        throw new ArgumentException("Tipo no válido");
    }
}
```

---

## 📦 Código Final con Factory Method

```csharp
public interface IVehicle
{
    string Model { get; }
}

public class Car : IVehicle
{
    public string Model { get; }
    public Car(string model, string color)
    {
        Model = $"{model} ({color})";
    }
}

public class Truck : IVehicle
{
    public string Model { get; }
    public Truck(string model, int axles)
    {
        Model = $"{model} con {axles} ejes";
    }
}

public class Motorcycle : IVehicle
{
    public string Model { get; }
    public Motorcycle(string model)
    {
        Model = model;
    }
}

public enum VehicleType
{
    Car,
    Truck,
    Motorcycle
}

public class VehicleFactory
{
    public IVehicle Create(VehicleType type)
    {
        switch (type)
        {
            case VehicleType.Car:
                return new Car("Golf", "Rojo");
            case VehicleType.Truck:
                return new Truck("F-150", 6);
            case VehicleType.Motorcycle:
                return new Motorcycle("Ducati");
            default:
                throw new ArgumentException("Tipo de vehículo no válido");
        }
    }
}
```
