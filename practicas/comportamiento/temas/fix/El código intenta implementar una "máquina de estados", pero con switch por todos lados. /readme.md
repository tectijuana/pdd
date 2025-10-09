# Actividad: Lista de 45 Problemas (Code Smells) — Patrones de Comportamiento (GoF)

## 🧑‍🎓 Datos del estudiante

- **Nombre:** Kevin Eduardo Garcia Cortez
- **Número de control:** 21211950 

---

## ❌ BADCODE

La aplicación modela una máquina de estados para un dispositivo ficticio (por ejemplo, un controlador de proceso industrial simple). El autor decidió usar enum + switch en todos los métodos — el resultado es difícil de mantener, sin separación de responsabilidades y con comportamiento disperso por toda la clase.

```csharp
// badcode/Program.cs
// .NET 8 - Español LATAM
using System;
using System.Collections.Generic;
using System.Threading;

namespace BadStateMachine
{
    // Enum que representa los estados de la máquina
    public enum MachineState
    {
        Off,
        Initializing,
        Ready,
        Processing,
        Paused,
        Error,
        ShuttingDown
    }

    // Evento simple como string (mala práctica)
    class StateMachine
    {
        private MachineState _state = MachineState.Off;
        private int _tick = 0;
        private bool _shouldStop = false;
        private Dictionary<string, object> _context = new Dictionary<string, object>();

        public StateMachine()
        {
            _context["jobsProcessed"] = 0;
        }

        // Método central con muchos switch y lógica mezclada
        public void HandleEvent(string evt)
        {
            Console.WriteLine($"[HandleEvent] estado actual: {_state}, evento recibido: {evt}");
            switch (_state)
            {
                case MachineState.Off:
                    switch (evt)
                    {
                        case "power_on":
                            Console.WriteLine("Encendiendo...");
                            _state = MachineState.Initializing;
                            break;
                        default:
                            Console.WriteLine("Ignorado en Off");
                            break;
                    }
                    break;

                case MachineState.Initializing:
                    if (evt == "init_done")
                    {
                        Console.WriteLine("Inicialización completa");
                        _state = MachineState.Ready;
                    }
                    else if (evt == "error")
                    {
                        Console.WriteLine("Error durante init");
                        _state = MachineState.Error;
                    }
                    else
                    {
                        Console.WriteLine("Evento desconocido en Initializing");
                    }
                    break;

                case MachineState.Ready:
                    if (evt == "start")
                    {
                        Console.WriteLine("Iniciando procesamiento");
                        _state = MachineState.Processing;
                    }
                    else if (evt == "shutdown")
                    {
                        Console.WriteLine("apagar desde ready");
                        _state = MachineState.ShuttingDown;
                    }
                    else
                    {
                        Console.WriteLine("Ready: evento no manejado");
                    }
                    break;

                case MachineState.Processing:
                    // Lógica duplicada: processing maneja múltiples eventos con ifs y switches
                    if (evt == "pause")
                    {
                        Console.WriteLine("Pausando...");
                        _state = MachineState.Paused;
                    }
                    else if (evt == "error")
                    {
                        Console.WriteLine("Error en procesamiento");
                        _state = MachineState.Error;
                    }
                    else if (evt == "shutdown")
                    {
                        Console.WriteLine("Shutdown solicitado durante processing");
                        _state = MachineState.ShuttingDown;
                    }
                    else if (evt == "job_done")
                    {
                        int jobs = (int)_context["jobsProcessed"];
                        jobs++;
                        _context["jobsProcessed"] = jobs;
                        Console.WriteLine($"Job terminado. Total: {jobs}");
                        // decide aleatoriamente volver a Ready o seguir en Processing
                        if (jobs % 3 == 0)
                        {
                            _state = MachineState.Ready;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Processing: evento ignorado");
                    }
                    break;

                case MachineState.Paused:
                    if (evt == "resume")
                    {
                        Console.WriteLine("Resumiendo...");
                        _state = MachineState.Processing;
                    }
                    else if (evt == "shutdown")
                    {
                        Console.WriteLine("Shutdown desde paused");
                        _state = MachineState.ShuttingDown;
                    }
                    else
                    {
                        Console.WriteLine("Paused: evento no válido");
                    }
                    break;

                case MachineState.Error:
                    // Manejo de errores disperso
                    if (evt == "reset")
                    {
                        Console.WriteLine("Reinicio desde error");
                        _state = MachineState.Initializing;
                    }
                    else
                    {
                        Console.WriteLine("En error, solo reset válido");
                    }
                    break;

                case MachineState.ShuttingDown:
                    Console.WriteLine("Recibido evento tras shutdown: " + evt);
                    // durante shutdown ignorar todo
                    break;

                default:
                    Console.WriteLine("Estado desconocido");
                    break;
            }
        }

        // Update con más switch / lógica de tiempo que debería estar separada
        public void Update()
        {
            _tick++;
            Console.WriteLine($"[Tick {_tick}] (_state = {_state})");

            switch (_state)
            {
                case MachineState.Off:
                    if (_tick > 1)
                    {
                        Console.WriteLine("Auto-power_on después de tick>1");
                        HandleEvent("power_on");
                    }
                    break;
                case MachineState.Initializing:
                    if (_tick % 2 == 0)
                    {
                        HandleEvent("init_done");
                    }
                    break;
                case MachineState.Processing:
                    // Simula job completo cada 3 ticks
                    if (_tick % 3 == 0)
                    {
                        HandleEvent("job_done");
                    }
                    break;
                case MachineState.Ready:
                    // intencionalmente nada
                    break;
                case MachineState.Paused:
                    // if redundante
                    if (_tick % 5 == 0)
                    {
                        Console.WriteLine("Revisando paused...");
                    }
                    break;
                case MachineState.Error:
                    // intenta auto reset (mala idea)
                    if (_tick % 10 == 0)
                    {
                        HandleEvent("reset");
                    }
                    break;
                case MachineState.ShuttingDown:
                    Console.WriteLine("Apagando...");
                    _shouldStop = true;
                    break;
            }
        }

        public bool ShouldStop() => _shouldStop;
        public MachineState CurrentState() => _state;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sm = new StateMachine();

            // Simulación: secuencia fija de eventos mezclada con ticks
            var events = new string[] { "noop", "power_on", "start", "pause", "resume", "shutdown" };
            int step = 0;

            while (!sm.ShouldStop() && step < 50)
            {
                Thread.Sleep(100); // simula tiempo
                sm.Update();

                // enviar un evento cada 4 ticks
                if (step % 4 == 0)
                {
                    var evt = events[step % events.Length];
                    sm.HandleEvent(evt);
                }

                // adicional: envío aleatorio de "error"
                if (step == 7)
                {
                    sm.HandleEvent("error");
                }

                step++;
            }

            Console.WriteLine("Simulación terminada. Estado final: " + sm.CurrentState());
            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}

```

## 🕵️ Identificación de Code Smells

Se detectan **3 problemas principales**:

1. **Uso excesivo de `switch` y condicionales anidados**  
   El método `HandleEvent()` y `Update()` dependen de múltiples `switch` sobre `_state` y `if` sobre strings como `"start"`, `"pause"`, etc.  
   Esto provoca código rígido y difícil de mantener.  
   → Se viola el **Principio Abierto/Cerrado (OCP)** al tener que modificar el mismo bloque cada vez que se agrega un nuevo estado.

2. **Falta de encapsulación del comportamiento por estado**  
   Toda la lógica está concentrada en `StateMachine`, mezclando responsabilidades de transición, validación y salida por consola.  
   → Se viola el **Principio de Responsabilidad Única (SRP)** y se pierde extensibilidad.

3. **Eventos como cadenas de texto (“magic strings”)**  
   El uso de `"power_on"`, `"start"`, `"pause"` impide el control de tipos y genera alto riesgo de errores por escritura.  
   → Se debería modelar mediante un sistema tipado (por ejemplo, clases de eventos o constantes enumeradas).

---

## 🛠️ Aplicación del patrón adecuado

El patrón **State** permite **encapsular el comportamiento de cada estado** en una clase independiente que implementa una interfaz común.  
De esta forma, el objeto `StateMachine` delega las acciones y transiciones al estado actual, eliminando los `switch` y mejorando la extensibilidad.

Cada clase de estado (por ejemplo, `OffState`, `InitializingState`, `ProcessingState`, etc.) define cómo reacciona a los eventos y a las actualizaciones de tiempo.

---

## 💡 Código con patrón State

```csharp
using System;
using System.Threading;

// Interfaz común para los estados
public interface IState
{
    void HandleEvent(StateMachine machine, string evt);
    void Update(StateMachine machine);
}

// Contexto
public class StateMachine
{
    public IState CurrentState { get; private set; }
    public int Tick { get; set; } = 0;

    public StateMachine()
    {
        CurrentState = new OffState();
    }

    public void ChangeState(IState newState)
    {
        Console.WriteLine($"[Cambio de estado] {CurrentState.GetType().Name} → {newState.GetType().Name}");
        CurrentState = newState;
    }

    public void HandleEvent(string evt) => CurrentState.HandleEvent(this, evt);
    public void Update()
    {
        Tick++;
        CurrentState.Update(this);
    }
}

// ===== Estados simplificados =====

public class OffState : IState
{
    public void HandleEvent(StateMachine m, string evt)
    {
        if(evt == "power_on")
        {
            Console.WriteLine("Encendiendo...");
            m.ChangeState(new ReadyState());
        }
        else
        {
            Console.WriteLine("Ignorado (OFF)");
        }
    }

    public void Update(StateMachine m)
    {
        if(m.Tick > 2)
            m.HandleEvent("power_on");
    }
}

public class ReadyState : IState
{
    public void HandleEvent(StateMachine m, string evt)
    {
        if(evt == "start")
        {
            Console.WriteLine("Iniciando procesamiento");
            m.ChangeState(new ProcessingState());
        }
    }

    public void Update(StateMachine m)
    {
        // Nada que hacer
    }
}

public class ProcessingState : IState
{
    public void HandleEvent(StateMachine m, string evt)
    {
        if(evt == "stop")
        {
            Console.WriteLine("Deteniendo procesamiento");
            m.ChangeState(new ReadyState());
        }
    }

    public void Update(StateMachine m)
    {
        if(m.Tick % 3 == 0)
            Console.WriteLine("Procesando job...");
    }
}

// ===== Programa principal =====
public class Program
{
    public static void Main()
    {
        var sm = new StateMachine();
        var events = new string[] { "noop", "power_on", "start", "stop" };
        
        for(int step=0; step<10; step++)
        {
            sm.Update();
            
            if(step % 3 == 0)
            {
                var evt = events[step % events.Length];
                sm.HandleEvent(evt);
            }

            System.Threading.Thread.Sleep(100);
        }

        Console.WriteLine("Simulación finalizada. Estado: " + sm.CurrentState.GetType().Name);
    }
}

```

## 📋 Justificación técnica

**Problema:**  
El código original (`HandleEvent` y `Update`) concentraba toda la lógica de estados en una sola clase con múltiples `switch`, lo que hacía difícil agregar nuevos estados o modificar la lógica sin romper el resto.

**Patrón aplicado:**  
Se aplicó el **patrón State**, separando el comportamiento de cada estado en clases específicas que implementan la interfaz `IState`.

**Beneficios esperados:**  
- ✅ **Extensibilidad:** agregar un nuevo estado no requiere tocar código existente.  
- ✅ **Legibilidad:** cada estado encapsula su propio comportamiento.  
- ✅ **Mantenibilidad:** elimina duplicaciones y reduce el riesgo de errores.  
- ✅ **Cohesión:** la máquina central solo orquesta las transiciones, sin lógica de negocio interna.  


