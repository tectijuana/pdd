# 🧩 Refactorización: Observer mal implementado

## 💭 Descripción del problema
En el sistema actual, cada vez que se recibe una notificación, se evalúan múltiples condiciones para decidir a quién avisar (correo, SMS, push).  
Esto genera código rígido y difícil de mantener, con estructuras como:

```csharp
if (mensaje.Contains("correo"))
    Console.WriteLine("Enviando correo...");

if (mensaje.Contains("sms"))
    Console.WriteLine("Enviando SMS...");

if (mensaje.Contains("push"))
    Console.WriteLine("Enviando notificación push...");
```
## 🔴 Problemas detectados

- **Violación del principio Abierto/Cerrado (OCP):** hay que modificar el código para agregar nuevos canales.  
- **Alto acoplamiento** entre el componente principal y los canales de notificación.  
- **Dificultad para escalar o probar** los componentes por separado.  

---

## ✅ Solución: Aplicar el Patrón Observer (GoF)

El patrón **Observer** permite que varios objetos (**observadores**) se suscriban a un evento del **sujeto**.  
Cuando ocurre una nueva notificación, el sujeto **notifica automáticamente** a todos los observadores registrados.

---

## 💻 Código Refactorizado (C#)
```csharp
using System;
using System.Collections.Generic;

// --- Interfaz del Observer ---
public interface IObserver
{
    void Update(string mensaje);
}

// --- Sujeto (Subject) ---
public class Notificador
{
    private List<IObserver> observadores = new List<IObserver>();

    public void Agregar(IObserver obs)
    {
        observadores.Add(obs);
    }

    public void Quitar(IObserver obs)
    {
        observadores.Remove(obs);
    }

    public void NuevaNotificacion(string mensaje)
    {
        Console.WriteLine($"[Sistema] Nueva notificación: {mensaje}");
        NotificarObservadores(mensaje);
    }

    private void NotificarObservadores(string mensaje)
    {
        foreach (var obs in observadores)
            obs.Update(mensaje);
    }
}

// --- Observadores concretos ---
public class NotificadorCorreo : IObserver
{
    public void Update(string mensaje)
    {
        Console.WriteLine($"Enviando correo con mensaje: {mensaje}");
    }
}

public class NotificadorSMS : IObserver
{
    public void Update(string mensaje)
    {
        Console.WriteLine($"Enviando SMS con mensaje: {mensaje}");
    }
}

public class NotificadorPush : IObserver
{
    public void Update(string mensaje)
    {
        Console.WriteLine($"Enviando notificación Push con mensaje: {mensaje}");
    }
}

// --- Programa principal ---
public class Program
{
    public static void Main()
    {
        var notificador = new Notificador();

        // Se agregan observadores (suscriptores)
        notificador.Agregar(new NotificadorCorreo());
        notificador.Agregar(new NotificadorSMS());
        notificador.Agregar(new NotificadorPush());

        // Se envía una nueva notificación
        notificador.NuevaNotificacion("Alerta de seguridad en el sistema");
    }
}

```
---
## 🧩 Justificación del patrón GoF aplicado

| **Aspecto** | **Detalle** |
|--------------|-------------|
| **Patrón aplicado** | Observer (GoF) |
| **Problema original** | Evaluación manual de múltiples condiciones para enviar avisos. |
| **Motivo de uso** | Permite desacoplar el emisor (sujeto) de los receptores (observadores). |
| **Ventajas** | Escalabilidad, bajo acoplamiento, cumplimiento de OCP y SRP. |
| **Extensibilidad** | Se pueden agregar nuevos observadores (WhatsApp, Telegram, Slack, etc.) sin modificar el código base. |

---

## 🧠 Conclusión

Se refactorizó el código aplicando el patrón **Observer (GoF)** para eliminar las evaluaciones condicionales y permitir que los canales de notificación se suscriban dinámicamente.  
De esta manera, el sistema se vuelve **más escalable, mantenible y extensible**, cumpliendo con los principios de diseño orientado a objetos.

---
## 💭 Reflexión

La aplicación del patrón **Observer** demuestra cómo un buen diseño orientado a objetos puede transformar un sistema rígido en uno flexible y extensible.  
Este ejercicio refuerza la importancia de **identificar los code smells** y aplicar los **patrones de comportamiento (GoF)** adecuados para mejorar la mantenibilidad del software.  
Implementar este patrón no solo resuelve un problema técnico, sino que también promueve una **mentalidad de diseño modular y desacoplado**, esencial para el desarrollo de proyectos escalables y sostenibles en el tiempo.

