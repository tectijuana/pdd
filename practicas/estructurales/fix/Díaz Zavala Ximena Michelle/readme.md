# Acoplar fuertemente la abstracción con la implementación
## Análisis de Código y Refactorización con Bridge
### Creado por Díaz Zavala Ximena Michelle
---
## Código malo
```csharp
// Ejemplo en .NET 8 
// Contexto: Queremos representar dispositivos y controles remotos (similar a TV / Radio).
// Pero aquí hay errores graves de diseño.

using System;

namespace BridgeBadExample
{
    // Abstracción fuertemente acoplada a la implementación 
    public class RemoteControl
    {
        private TV _tv; //  Problema: Está fuertemente acoplado a una implementación concreta.

        public RemoteControl(TV tv)
        {
            _tv = tv;
        }

        public void TogglePower()
        {
            if (_tv.IsEnabled)
                _tv.Disable();
            else
                _tv.Enable();
        }

        public void VolumeUp()
        {
            _tv.SetVolume(_tv.Volume + 10);
        }

        public void VolumeDown()
        {
            _tv.SetVolume(_tv.Volume - 10);
        }
    }

    // Implementación concreta única
    public class TV
    {
        public bool IsEnabled { get; private set; }
        public int Volume { get; private set; }

        public void Enable()
        {
            IsEnabled = true;
            Console.WriteLine("TV encendida");
        }

        public void Disable()
        {
            IsEnabled = false;
            Console.WriteLine("TV apagada");
        }

        public void SetVolume(int volume)
        {
            Volume = volume;
            Console.WriteLine($"Volumen ajustado a {Volume}");
        }
    }

    // Clase que intenta extender, pero sin puente ni generalización.
    public class AdvancedRemoteControl : RemoteControl
    {
        private TV _tv; //  Problema repetido: vuelve a acoplarse a la implementación concreta.

        public AdvancedRemoteControl(TV tv) : base(tv)
        {
            _tv = tv;
        }

        public void Mute()
        {
            _tv.SetVolume(0);
            Console.WriteLine("TV en mute");
        }
    }

    class Program
    {
        static void Main()
        {
            var tv = new TV();
            var remote = new AdvancedRemoteControl(tv);

            remote.TogglePower();
            remote.VolumeUp();
            remote.Mute();
        }
    }
}

```
---
## 🚨 Problemas estructurales detectados

### 1. Acoplamiento fuerte entre abstracción e implementación
La clase `RemoteControl` depende directamente de la clase `TV`.  
Esto rompe el principio de **abierto/cerrado** y hace imposible reutilizar el control remoto con otros dispositivos (ej. `Radio`).

### 2. Falta de una jerarquía de interfaces/abstracciones
No existe una interfaz común (`IDevice`) que generalice a los dispositivos.  
Cada clase remota necesita reescribir código para cada implementación concreta.

### 3. Violación del principio de extensión sin modificación
La clase `AdvancedRemoteControl` vuelve a acoplarse directamente a `TV`, repitiendo el problema inicial y rompiendo el beneficio de la herencia.  
No hay una separación clara entre **abstracción** (controles) e **implementación** (dispositivos).

---

## ✅ Por qué usar el patrón Bridge

El patrón **Bridge** es apropiado porque:

- Desacopla la **abstracción** de su **implementación**, permitiendo que cambien de forma independiente.  
- Facilita agregar nuevos **controles remotos** o nuevos **dispositivos** sin modificar el código existente.  
- Hace el sistema más **extensible** y **mantenible**, respetando el principio **OCP** (Open/Closed Principle).  


---
## Código corregido con Bridge
```csharp
// Ejemplo corregido con Bridge en .NET 8

using System;

namespace BridgeGoodExample
{
    // Implementación genérica (Implementor)
    public interface IDevice
    {
        bool IsEnabled { get; }
        int Volume { get; }
        void Enable();
        void Disable();
        void SetVolume(int volume);
    }

    // Implementación concreta 1
    public class TV : IDevice
    {
        public bool IsEnabled { get; private set; }
        public int Volume { get; private set; }

        public void Enable()
        {
            IsEnabled = true;
            Console.WriteLine("TV encendida");
        }

        public void Disable()
        {
            IsEnabled = false;
            Console.WriteLine("TV apagada");
        }

        public void SetVolume(int volume)
        {
            Volume = Math.Max(0, Math.Min(100, volume)); // Validación básica
            Console.WriteLine($"Volumen de TV ajustado a {Volume}");
        }
    }

    // Implementación concreta 2
    public class Radio : IDevice
    {
        public bool IsEnabled { get; private set; }
        public int Volume { get; private set; }

        public void Enable()
        {
            IsEnabled = true;
            Console.WriteLine("Radio encendida");
        }

        public void Disable()
        {
            IsEnabled = false;
            Console.WriteLine("Radio apagada");
        }

        public void SetVolume(int volume)
        {
            Volume = Math.Max(0, Math.Min(100, volume));
            Console.WriteLine($"Volumen de Radio ajustado a {Volume}");
        }
    }

    // Abstracción
    public class RemoteControl
    {
        protected IDevice _device;

        public RemoteControl(IDevice device)
        {
            _device = device;
        }

        public void TogglePower()
        {
            if (_device.IsEnabled)
                _device.Disable();
            else
                _device.Enable();
        }

        public void VolumeUp() => _device.SetVolume(_device.Volume + 10);
        public void VolumeDown() => _device.SetVolume(_device.Volume - 10);
    }

```
---
