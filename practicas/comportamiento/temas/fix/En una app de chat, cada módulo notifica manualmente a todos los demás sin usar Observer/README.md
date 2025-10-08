# Refactorización de App de Chat usando Patrones de Comportamiento (GoF)
# Daniel Omar Gonzalez Martinez 21212342

## Contexto
En nuestra aplicación de chat, actualmente cada módulo tiene que notificar manualmente a todos los demás cuando ocurre un evento (por ejemplo, cuando llega un mensaje).  
Esto genera varios problemas:

- Los módulos quedan muy acoplados entre sí.
- El código se vuelve difícil de mantener o escalar.
- Agregar nuevos módulos o eventos puede causar errores fácilmente.

Un ejemplo simplificado de cómo se hace ahora:

```csharp
chatModule1.SendMessage("Hola");
chatModule2.ReceiveMessage("Hola");
chatModule3.ReceiveMessage("Hola");
```

Aquí cada módulo tiene que avisar a todos los demás manualmente, lo que no es nada práctico.

---

## Problema Identificado
**Problema:** Cada módulo conoce a todos los demás y se encarga de notificar.  
**Consecuencia:** Acoplamiento muy fuerte entre módulos y código difícil de mantener.

**Recomendación GoF:** Usar un patrón de **comportamiento** que desacople al emisor del receptor: **Observer**.

---

## Patrón Aplicable: Observer

### Definición
El patrón **Observer** permite que un objeto (el **sujeto**) mantenga una lista de otros objetos (**observadores**) y los notifique automáticamente cuando cambie su estado.  

- **Sujeto (Subject):** Mantiene la lista de observadores y les avisa cuando hay cambios.  
- **Observador (Observer):** Recibe las actualizaciones del sujeto.

### Beneficios
- Desacopla el módulo que genera eventos de los que los consumen.  
- Permite agregar o quitar observadores sin tocar el sujeto.  
- Facilita mantener y escalar la aplicación sin romper otras partes.

---

## Implementación Parcial (Ejemplo)

```csharp
using System;
using System.Collections.Generic;

// Interfaz Observer
interface IChatObserver
{
    void Update(string message);
}

// Interfaz Subject
interface IChatSubject
{
    void Attach(IChatObserver observer);
    void Detach(IChatObserver observer);
    void Notify(string message);
}

// Sujeto concreto
class ChatModule : IChatSubject
{
    private List<IChatObserver> observers = new List<IChatObserver>();

    public void Attach(IChatObserver observer) => observers.Add(observer);
    public void Detach(IChatObserver observer) => observers.Remove(observer);

    public void Notify(string message)
    {
        foreach (var observer in observers)
            observer.Update(message);
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"Mensaje enviado: {message}");
        Notify(message);
    }
}

// Observador concreto
class ChatUser : IChatObserver
{
    private string name;

    public ChatUser(string name) => this.name = name;

    public void Update(string message) => Console.WriteLine($"{name} recibió: {message}");
}

// Uso parcial
class Program
{
    static void Main()
    {
        ChatModule chat = new ChatModule();

        ChatUser alice = new ChatUser("Alice");
        ChatUser bob = new ChatUser("Bob");

        chat.Attach(alice);
        chat.Attach(bob);

        chat.SendMessage("Hola a todos!");
    }
}
```

### Resultado
Cuando llamamos `chat.SendMessage()`, todos los observadores reciben automáticamente el mensaje.  
Ya no es necesario que cada módulo conozca a los demás.

Aplicar **Observer** en este escenario permite:

- Reducir el acoplamiento entre módulos.  
- Facilitar la extensión de la app si agregamos más usuarios o módulos.  
- Mantener un diseño limpio y escalable.  

---
