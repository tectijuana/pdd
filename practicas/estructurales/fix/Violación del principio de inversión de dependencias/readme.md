# Refactorización: Patrón Bridge y Principio de Inversión de Dependencias (DIP)

---

## Contexto

El código original viola el **Principio de Inversión de Dependencias (DIP)** al hacer que la clase de alto nivel (`RemoteControl`) dependa directamente de una clase concreta (`SonyTV`). Esto afecta la flexibilidad y escalabilidad del sistema, además de dificultar las pruebas y el mantenimiento.

---

## Código Original (Antes de la refactorización)

```csharp
// Abstracción
public class RemoteControl
{
    private TV _tv;

    public RemoteControl()
    {
        _tv = new SonyTV(); // Violación del DIP: dependencia directa de implementación concreta
    }

    public void TurnOn()
    {
        _tv.TurnOn();
    }
}

// Implementación concreta
public class SonyTV : TV
{
    public void TurnOn()
    {
        Console.WriteLine("Sony TV encendida");
    }
}

// Interface
public interface TV
{
    void TurnOn();
}
```
## Problema Identificado

- `RemoteControl` depende directamente de una clase concreta (`SonyTV`) en lugar de depender de una abstracción.
- Se rompe el **Principio de Inversión de Dependencias (DIP)**.
- El sistema es menos flexible para extender nuevas implementaciones de TV o controles remotos sin modificar clases existentes.

---

## Refactorización Propuesta (Aplicando Patrón Bridge y DIP)

```csharp
// Interface implementadora (Implementor)
public interface ITV
{
    void TurnOn();
}

// Implementaciones concretas (ConcreteImplementor)
public class SonyTV : ITV
{
    public void TurnOn()
    {
        Console.WriteLine("Sony TV encendida");
    }
}

public class SamsungTV : ITV
{
    public void TurnOn()
    {
        Console.WriteLine("Samsung TV encendida");
    }
}

// Abstracción (Abstraction)
public class RemoteControl
{
    protected ITV _tv;

    public RemoteControl(ITV tv)
    {
        _tv = tv; // Dependencia en abstracción (ITV)
    }

    public virtual void TurnOn()
    {
        _tv.TurnOn();
    }
}

// Abstracción refinada (Refined Abstraction)
public class AdvancedRemoteControl : RemoteControl
{
    public AdvancedRemoteControl(ITV tv) : base(tv) {}

    public void Mute()
    {
        Console.WriteLine("TV en silencio");
    }
}
```
Uso
```
var sonyRemote = new RemoteControl(new SonyTV());
sonyRemote.TurnOn();

var samsungRemote = new AdvancedRemoteControl(new SamsungTV());
samsungRemote.TurnOn();
samsungRemote.Mute();
```
Justificación

Se ha invertido la dependencia para que RemoteControl dependa de la abstracción ITV y no de una implementación concreta.

Esto permite desacoplar la abstracción de la implementación, aplicando correctamente el patrón Bridge.

El código es más extensible: se pueden añadir nuevas implementaciones de ITV sin modificar RemoteControl.

Facilita la prueba y mantenimiento del código.

Se cumple el principio SOLID de Inversión de Dependencias.
