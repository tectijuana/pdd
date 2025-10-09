# Una clase de "Controlador" tiene más de 500 líneas con lógica de múltiples módulos. ❌ → God Object, necesita Mediator y otros refactors
**Alumno:** Diego Huerta Espinoza  
**No. Control:** 20212411 

---

## Problema  
Una clase de "Controlador" tiene más de 500 líneas con lógica de múltiples módulos.  
❌ → God Object, necesita Mediator y otros refactors.

---

##  Contexto  
En un sistema académico, el controlador principal se encarga de coordinar directamente la interfaz de usuario, la lógica de negocio y el acceso a datos.  
Cada módulo interactúa de forma directa con los demás, generando un acoplamiento fuerte y dificultando la extensión del sistema.

Esto provoca:

- Duplicación de lógica de coordinación.
- Dependencia directa entre componentes.
- Baja cohesión y difícil mantenibilidad.
- Violación del principio de responsabilidad única (SRP).

---

##  Problema Detectado  
- El controlador actúa como un **God Object**, gestionando múltiples responsabilidades.  
- Los módulos están acoplados entre sí, sin una capa de mediación.  
- No existe un mecanismo centralizado para coordinar acciones.  
- Cualquier cambio en la lógica requiere modificar múltiples clases.

---

##  Consecuencias  
- Dificultad para escalar el sistema o agregar nuevos módulos.  
- Riesgo de errores al modificar flujos de interacción.  
- Código difícil de testear y mantener.  
- Violación de principios SOLID (SRP, OCP, DIP).

---

##  Patrón aplicado: Mediator  
El patrón **Mediator** permite desacoplar los componentes del sistema, centralizando la comunicación en un objeto mediador.  
Cada módulo se comunica con el mediador, sin conocer directamente a los demás.

---

##  Objetivo del Refactor  
- Eliminar el acoplamiento directo entre módulos.  
- Centralizar la lógica de coordinación.  
- Mejorar la extensibilidad y mantenibilidad del sistema.  
- Cumplir con los principios de diseño limpio.

---

## ❌  Código sin refactorizar

```csharp
class Controlador {
    public void Ejecutar() {
        var ui = new UI();
        var logica = new LogicaNegocio();
        var bd = new BaseDatos();

        ui.MostrarBoton();
        logica.Procesar();
        bd.Guardar();
    }
}
```
## ✅ Código refactorizado con Mediator
```csharp
using System;

// Interfaces
interface IMediator {
    void Notificar(object sender, string evento);
}

// Mediador concreto
class ControladorMediator : IMediator {
    public UI ui;
    public LogicaNegocio logica;
    public BaseDatos bd;

    public void Notificar(object sender, string evento) {
        if (evento == "clickBoton") {
            logica.Procesar();
        } else if (evento == "guardar") {
            bd.Guardar();
        }
    }
}

// Módulos desacoplados
class UI {
    private IMediator mediator;
    public UI(IMediator m) => mediator = m;
    public void MostrarBoton() {
        Console.WriteLine("Botón mostrado.");
        mediator.Notificar(this, "clickBoton");
    }
}

class LogicaNegocio {
    private IMediator mediator;
    public LogicaNegocio(IMediator m) => mediator = m;
    public void Procesar() {
        Console.WriteLine("Procesando lógica...");
        mediator.Notificar(this, "guardar");
    }
}

class BaseDatos {
    public void Guardar() {
        Console.WriteLine("Datos guardados en BD.");
    }
}

// Programa principal
class Program {
    static void Main() {
        var mediator = new ControladorMediator();
        mediator.ui = new UI(mediator);
        mediator.logica = new LogicaNegocio(mediator);
        mediator.bd = new BaseDatos();

        mediator.ui.MostrarBoton();
    }
}
```
## Solución
El patrón Mediator permite que los módulos se comuniquen sin depender directamente entre sí. Ahora, el controlador no coordina manualmente cada acción, sino que delega la lógica al mediador.

## Resultados  
La refactorización con Mediator mejora la arquitectura del sistema:

- Se elimina el God Object.  
- Cada clase tiene una única responsabilidad.  
- El sistema es más extensible y mantenible.  
- Se cumple con los principios SOLID.  
- El código es más claro, modular y reutilizable.

