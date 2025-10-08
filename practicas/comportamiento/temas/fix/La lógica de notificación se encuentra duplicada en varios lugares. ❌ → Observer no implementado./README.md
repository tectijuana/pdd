# Refactorización con Patrón Observer (GoF)

### Alumno:
Luis Felipe Torres Coto Rodarte

### Problema:   
La lógica de notificación se encuentra duplicada en varios lugares. ❌ → Observer no implementado.

---

## Contexto
En un sistema de software, la **lógica de notificación** se repite en múltiples clases.  
Cada módulo que genera un evento (por ejemplo, un cambio de estado, un nuevo mensaje o actualización de datos) se encarga manualmente de avisar a las demás partes interesadas.  

Esto genera **duplicación de código**, **alto acoplamiento** y **baja flexibilidad**.  
Cuando se requiere modificar la forma en que se notifican los cambios, es necesario intervenir en distintos puntos del sistema.

---

## Problema Detectado
La lógica de notificación se encuentra duplicada en varios lugares.  
No se ha implementado ningún mecanismo centralizado de observación o suscripción.

### Consecuencias
- Duplicación de funciones que hacen lo mismo (enviar mensajes, actualizar vistas, registrar eventos).  
- Dependencia directa entre emisores y receptores.  
- Dificultad para añadir nuevas notificaciones o cambiar la forma en que se comunican los módulos.  
- Riesgo de errores al olvidar actualizar una de las copias del código repetido.

---

## Patrón: **Observer**

El patrón **Observer** sirve para notificar automáticamente a un conjunto de objetos (observadores) cuando el estado de otro objeto (sujeto o publicador) cambia, sin que el sujeto necesite conocer qué observadores específicos están interesados en los cambios.

### Objetivo del Refactor
Eliminar la duplicación de lógica de notificación, reducir el acoplamiento entre las clases emisoras y receptoras, y facilitar la extensión y mantenimiento del sistema.

---
## Codigo

### Codigo sin refactorizar
```csharp
using System;
using System.Collections.Generic;

class User
{
    public string Name { get; }
    public User(string name) => Name = name;

    public void ReceiveNotification(string message)
    {
        Console.WriteLine($"{Name} recibió la notificación: {message}");
    }
}

class Notifier
{
    private List<User> users = new();

    public void AddUser(User user)
    {
        users.Add(user);
    }

    // Problema: el método de notificación está acoplado directamente a la lista de usuarios
    public void SendNotification(string message)
    {
        foreach (var user in users)
        {
            // Duplicación de lógica si luego se agregan más tipos de receptores
            user.ReceiveNotification(message);
        }
    }
}

class Program
{
    static void Main()
    {
        var notifier = new Notifier();

        var user1 = new User("Luis");
        var user2 = new User("Felipe");

        notifier.AddUser(user1);
        notifier.AddUser(user2);

        // Enviar notificación
        notifier.SendNotification("Nuevo mensaje en el sistema");

        // Problema: Si queremos agregar otro tipo de observador (ej. Logger o Email),
        // tendríamos que modificar la clase Notifier directamente,
        // rompiendo el principio de abierto/cerrado (OCP).
    }
}
```
**Problemas:**  
-Duplicación de lógica de notificación.
-Acoplamiento fuerte: Notifier depende directamente de User.
-Difícil de extender: agregar otro tipo de receptor (por ejemplo Admin o Logger) requeriría modificar Notifier.
-Rompe el principio OCP (Open/Closed Principle).

### Codigo realizado con Observer

```csharp
using System;
using System.Collections.Generic;

// --- Interfaces ---
interface IObserver
{
    void Update(string message);
}

interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(string message);
}

// --- Implementación del Sujeto ---
class Notifier : ISubject
{
    private List<IObserver> observers = new();

    public void Attach(IObserver observer) => observers.Add(observer);
    public void Detach(IObserver observer) => observers.Remove(observer);

    public void Notify(string message)
    {
        foreach (var obs in observers)
            obs.Update(message);
    }
}

// --- Observadores concretos ---
class User : IObserver
{
    public string Name { get; }
    public User(string name) => Name = name;

    public void Update(string message)
    {
        Console.WriteLine($"{Name} recibió la notificación: {message}");
    }
}

// --- Programa principal ---
class Program
{
    static void Main()
    {
        var notifier = new Notifier();
        var user1 = new User("Luis");
        var user2 = new User("Felipe");

        notifier.Attach(user1);
        notifier.Attach(user2);

        notifier.Notify("Nuevo mensaje en el sistema");
    }
}
```
**Solucion:**
El patrón Observer permite que varios objetos “observen” a otro sin depender directamente de su implementación. Ahora, Notifier no sabe quién recibe las notificaciones, solo invoca Update() sobre cada observador

### Resultados
El patrón Observer demuestra cómo desacoplar la lógica de notificación mejora la flexibilidad y mantenibilidad del sistema.
Antes, cada clase debía manejar sus propios avisos, generando código repetido.
Después, el sistema notifica automáticamente a todos los interesados sin conocerlos directamente, cumpliendo los principios de diseño limpio y reutilizable.
