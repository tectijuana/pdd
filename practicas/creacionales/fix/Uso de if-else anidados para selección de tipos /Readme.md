# 🧪 Actividad de Cierre: Refactorizando Patrones Creacionales  
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
- El código ahora cumple con el **principio de inversión de dependencias (DIP)**.
- La solución es más **fácil de mantener y probar**.
- Se elimina el **code smell** del `if-else` anidado.

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
Footer
