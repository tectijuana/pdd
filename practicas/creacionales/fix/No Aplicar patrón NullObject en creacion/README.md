# 📦 Refactor Creacionales – Null Object Pattern en C#

## Nombre del problema
**No aplicar patrón NullObject en creación**

---

## 🔍 Problemas detectados
1. **Clase `Vehiculo` usada sin validar `null`**  
   - La fábrica retorna `null` cuando no reconoce un tipo de vehículo.  
   - Esto provoca que en el cliente siempre haya que validar con `if (v != null)`.

2. **Violación del Principio de Inversión de Dependencias**  
   - El cliente depende de la ausencia (`null`) en lugar de un comportamiento definido.

3. **Duplicación de lógica de validación**  
   - Cada consumidor debe implementar condicionales para manejar los casos inválidos.  

---

## 🛠 Patrón aplicado
Se aplicó el **Null Object Pattern**:  
- Se creó una clase `NullVehiculo` que hereda de `Vehiculo`.  
- La fábrica **nunca retorna `null`**, sino un objeto válido (real o nulo).  
- Se eliminan condicionales en el código cliente.  

---

## 💻 Código Antes (Anti-Patrón) y Código Después (Aplicando Null Object Pattern)

```csharp
public abstract class Vehiculo
{
    public abstract void Conducir();
}

public class Auto : Vehiculo
{
    public override void Conducir()
    {
        Console.WriteLine("Conduciendo un auto.");
    }
}

public class Moto : Vehiculo
{
    public override void Conducir()
    {
        Console.WriteLine("Conduciendo una moto.");
    }
}

public class VehiculoFactory
{
    public Vehiculo CrearVehiculo(string tipo)
    {
        if (tipo == "Auto") return new Auto();
        if (tipo == "Moto") return new Moto();
        return null; // ❌ Anti-patrón: retorna null
    }
}

// Uso en cliente ANTES
var factoryMala = new VehiculoFactory();
Vehiculo vMalo = factoryMala.CrearVehiculo("Camion");

if (vMalo != null)  // 👎 Condicional innecesario
{
    vMalo.Conducir();
}
else
{
    Console.WriteLine("Vehículo no disponible.");
}

// ✅ Implementación correcta con Null Object
public class NullVehiculo : Vehiculo
{
    public override void Conducir()
    {
        Console.WriteLine("Vehículo no disponible.");
    }
}

public class VehiculoFactoryBueno
{
    public Vehiculo CrearVehiculo(string tipo)
    {
        if (tipo == "Auto") return new Auto();
        if (tipo == "Moto") return new Moto();
        return new NullVehiculo(); // ✅ Nunca retorna null
    }
}

// Uso en cliente DESPUÉS
var factory = new VehiculoFactoryBueno();

Vehiculo v1 = factory.CrearVehiculo("Auto");
Vehiculo v2 = factory.CrearVehiculo("Camion"); // Tipo inválido

v1.Conducir(); // "Conduciendo un auto."
v2.Conducir(); // "Vehículo no disponible."
