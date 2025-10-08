# 📌 Refactorización parcial (Patrón Bridge)  
Problematica de la lista: Añadir métodos innecesarios a la interfaz implementadora

## 🎯 Contexto  
Se detectaron problemas en la implementación del patrón Bridge en la solución original (`IDevice`, `Television`, `Radio`, `RemoteControl`).  
El código presentaba **acoplamiento fuerte**, **métodos innecesarios en interfaces** y **falsa generalización** de dispositivos.  

El objetivo de este PR es aplicar un **refactor parcial** que mejore:  

- ✅ Legibilidad  
- ✅ Coherencia  
- ✅ Separación de responsabilidades  
- ✅ Uso idiomático de C# en .NET 8  

---

## 🔍 Problemas detectados (Code Smells)  

### 1. Interfaz inflada (ISP violado)  
- `IDevice` contenía `SetChannel()` y `SetVolume()`.  
- 🚨 Problema: **Radio** no usa canales, tenía método vacío.  
- ❌ Violación de **ISP (Interface Segregation Principle)**.  

---

### 2. Acoplamiento rígido entre abstracción e implementación  
- `RemoteControl` dependía directamente de `SetChannel()` y `SetVolume()`.  
- 🚨 Problema: la abstracción conocía detalles innecesarios de la implementación.  
- ❌ Se rompe la intención del **Bridge**.  

---

### 3. Falsa generalización (LSP violado)  
- Todos los dispositivos eran tratados como iguales.  
- 🚨 Problema: `RemoteControl.ChannelUp()` no aplica a **Radio**.  
- ❌ Violación de **LSP (Liskov Substitution Principle)**.  

---

## ✅ Soluciones aplicadas (Refactor Parcial)  

- **Interfaces segregadas**:  
  - `IAudioDevice` para dispositivos con volumen.  
  - `IVideoDevice` para dispositivos con canales.  
- **RemoteControl desacoplado**: solo maneja encendido/apagado (`PowerOn/PowerOff`).  
- **Implementaciones coherentes**:  
  - `Radio` implementa solo volumen.  
  - `Television` maneja volumen y canales.  

---

## 🛠 Patrones aplicados  

- 🌉 **Bridge (correcto)** → separación entre abstracción e implementación.  
- 🔎 **Interface Segregation (ISP)** → interfaces específicas según capacidades.  
- 🧩 **Dependency Injection** → evita acoplamiento fuerte y facilita pruebas.  

---

## 💡 Justificación técnica  

- 🧩 **Cohesión**: cada clase hace solo lo que corresponde.  
- 🧪 **Testabilidad**: fácil de mockear en pruebas unitarias.  
- 🔧 **Flexibilidad**: nuevos dispositivos sin modificar `RemoteControl`.  

---

## 🔄 Impacto  

- 🚫 Eliminación de métodos vacíos en implementaciones.  
- ✅ Cumplimiento de **ISP** y **LSP**.  
- 🌉 Uso correcto del patrón **Bridge**.  
- 🏗️ Arquitectura extensible y mantenible.  

---

## ❌ Código completo con problemas (antes)  

```csharp
using System;

namespace BridgeBadExample
{
    public interface IDevice
    {
        void PowerOn();
        void PowerOff();
        void SetVolume(int volume);   // 🚨 No aplica a todos
        void SetChannel(int channel); // 🚨 No aplica a todos
    }

    public class Television : IDevice
    {
        public void PowerOn() => Console.WriteLine("Televisión encendida");
        public void PowerOff() => Console.WriteLine("Televisión apagada");

        public void SetVolume(int volume) =>
            Console.WriteLine($"Televisión volumen: {volume}");

        public void SetChannel(int channel) =>
            Console.WriteLine($"Televisión canal: {channel}");
    }

    public class Radio : IDevice
    {
        public void PowerOn() => Console.WriteLine("Radio encendida");
        public void PowerOff() => Console.WriteLine("Radio apagada");

        public void SetVolume(int volume) =>
            Console.WriteLine($"Radio volumen: {volume}");

        public void SetChannel(int channel)
        {
            // 🚨 No aplica en Radio → método vacío
        }
    }

    public class RemoteControl
    {
        private readonly IDevice _device;

        public RemoteControl(IDevice device)
        {
            _device = device;
        }

        public void TurnOn()
        {
            _device.PowerOn();
            _device.SetVolume(10);    // 🚨 Acoplamiento fuerte
            _device.SetChannel(5);    // 🚨 No todos los dispositivos tienen canal
        }

        public void TurnOff()
        {
            _device.PowerOff();
        }
    }

    public class Program
    {
        public static void Main()
        {
            var tv = new Television();
            var remoteTv = new RemoteControl(tv);
            remoteTv.TurnOn();  // Funciona bien

            var radio = new Radio();
            var remoteRadio = new RemoteControl(radio);
            remoteRadio.TurnOn(); // 🚨 Problema: SetChannel no aplica
        }
    }
}
```
## ✅ Código completo corregido (después)
```csharp
using System;

namespace BridgeRefactor
{
    // Abstracción mínima: todo dispositivo puede encenderse y apagarse
    public interface IDevice
    {
        void PowerOn();
        void PowerOff();
    }

    // Interfaces segregadas según capacidades
    public interface IAudioDevice : IDevice
    {
        void SetVolume(int volume);
    }

    public interface IVideoDevice : IDevice
    {
        void SetChannel(int channel);
    }

    // Implementación concreta: Televisión (audio + video)
    public class Television : IAudioDevice, IVideoDevice
    {
        public void PowerOn() => Console.WriteLine("Televisión encendida");
        public void PowerOff() => Console.WriteLine("Televisión apagada");

        public void SetVolume(int volume) =>
            Console.WriteLine($"Televisión volumen: {volume}");

        public void SetChannel(int channel) =>
            Console.WriteLine($"Televisión canal: {channel}");
    }

    // Implementación concreta: Radio (solo audio)
    public class Radio : IAudioDevice
    {
        public void PowerOn() => Console.WriteLine("Radio encendida");
        public void PowerOff() => Console.WriteLine("Radio apagada");

        public void SetVolume(int volume) =>
            Console.WriteLine($"Radio volumen: {volume}");
    }

    // Abstracción: control remoto básico
    public class RemoteControl
    {
        protected IDevice Device { get; }

        public RemoteControl(IDevice device)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device));
        }

        public void TurnOn() => Device.PowerOn();
        public void TurnOff() => Device.PowerOff();
    }

    // Programa de prueba
    public class Program
    {
        public static void Main()
        {
            // Televisión con control remoto
            var tv = new Television();
            var remoteTv = new RemoteControl(tv);
            remoteTv.TurnOn();
            tv.SetVolume(15);
            tv.SetChannel(7);

            // Radio con control remoto
            var radio = new Radio();
            var remoteRadio = new RemoteControl(radio);
            remoteRadio.TurnOn();
            radio.SetVolume(8);
        }
    }
}
```
<img width="801" height="622" alt="image" src="https://github.com/user-attachments/assets/df6b0609-f4cd-49ff-b190-e3a0acd74de2" />


### Reflexión
En este ejercicio pude darme cuenta de lo importante que es detectar los problemas de diseño antes de codificar más funcionalidad. Al principio parecía que la interfaz IDevice era suficiente, pero terminó siendo demasiado rígida y forzada para algunos casos, como el de la radio.

Con el refactor entendí que los patrones de diseño, como Bridge y los principios de SOLID (especialmente ISP y LSP), no son teoría abstracta, sino que realmente ayudan a tener un código más flexible, claro y fácil de mantener.

También confirmé que un refactor parcial bien justificado es más valioso que un cambio enorme sin explicación, porque demuestra que se entendió el problema y se aplicó la solución adecuada.

En resumen, la tarea me ayudó a reforzar que:

- Detectar code smells es tan importante como escribir el código.
- Un patrón bien aplicado evita acoplamientos innecesarios.
- Documentar el porqué del cambio es parte esencial del trabajo profesional de un desarrollador.
