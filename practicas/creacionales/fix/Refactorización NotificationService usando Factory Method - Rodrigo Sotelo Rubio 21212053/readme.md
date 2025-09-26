# 📉 Código Mal Diseñado en C# .NET 8
Alumno: Rodrigo Sotelo Rubio

Matricula: 21212053


# 🧪 Actividad de Cierre: Refactorizando Patrones Creacionales

## 🎯 Objetivo

Aplicar lo aprendido sobre **patrones creacionales (GoF)** mediante la detección de **code smells** y propuestas de refactorización en código realista. Esta actividad simula una revisión de código en un entorno de desarrollo profesional usando Pull Requests.

---

## 📦 Proyecto Base

El repositorio contiene clases implementadas incorrectamente con:
- Singleton
- Factory Method
- Abstract Factory
- Builder
- Prototype

El código presenta **malas prácticas intencionadas**, errores comunes, y anti-patrones frecuentes.

## 🛑 Contexto del problema generado
Una aplicación que crea diferentes tipos de notificaciones (Email, SMS, Push), pero todo está mal implementado y mezclado. Se viola la responsabilidad única, el código es difícil de mantener y extender, y no se aplican patrones creacionales adecuados.

## 📉 Código Mal Diseñado en C# .NET 8 
```csharp
public class NotificationService
{
    public void SendNotification(string type, string message, string recipient)
    {
        if (type == "Email")
        {
            // Creación y envío de email directamente aquí (violación SRP)
            Console.WriteLine($"Enviando Email a {recipient}: {message}");
        }
        else if (type == "SMS")
        {
            // Creación y envío SMS también aquí, mezcla lógica de creación y uso
            Console.WriteLine($"Enviando SMS a {recipient}: {message}");
        }
        else if (type == "Push")
        {
            // Lógica de notificación Push mezclada en el mismo método
            Console.WriteLine($"Enviando Push a {recipient}: {message}");
        }
        else
        {
            throw new ArgumentException("Tipo de notificación no soportado");
        }
    }
}

// Uso:
var service = new NotificationService();
service.SendNotification("Email", "Hola Mundo!", "juan@correo.com");
service.SendNotification("SMS", "Hola Mundo!", "+123456789");
service.SendNotification("Push", "Hola Mundo!", "device_token");
```
## 🔍 Problemas detectados
- La clase NotificationService viola el **Principio de Responsabilidad Única** (SRP) al mezclar creación y envío de notificaciones.
- Uso de condicionales (if/else) para decidir qué tipo de notificación crear, dificultando la extensión.
- La instancia directa y creación de objetos debería estar encapsulada en una Factory para mejorar mantenimiento y testabilidad.
- La estructura actual dificulta la adición de nuevos tipos de notificación sin modificar código existente (violación de OCP).

## 🛠 Patrón aplicado

- Se implementa el **Factory Method** para separar la creación de objetos **INotification** del uso.
- Cada tipo de notificación implementa una interfaz común **INotification.**
- **NotificationFactory** se encarga de instanciar el tipo correcto de notificación según el parámetro recibido.
- **NotificationService** usa la fábrica para obtener las notificaciones, manteniendo solo la lógica de envío.

## 💡 Justificación del cambio
- Mejoramos la **cohesión interna** al que cada clase tenga una única responsabilidad.
- Aumentamos la **testabilidad**, pues ahora se puede mockear la fábrica o las notificaciones individualmente.
- **Incrementamos la flexibilidad ante cambios** agregar nuevas notificaciones solo implica añadir nuevas clases y extender la fábrica.
- Se **cumple el Principio de Inversión de Dependencias**, desacoplando la creación del uso.

## 🔄 Impacto

- Facilita la **extensión y mantenimiento** del código.
- Prepara la arquitectura para facilitar la **automatización de pruebas unitarias.**
- Aumenta la **legibilidad** y separación de preocupaciones.

## Codigo refactorizado
```csharp
// 🚩 Interfaz común que representa la abstracción de la notificación.
// Define el contrato que todas las notificaciones deben implementar.
// Esto cumple con el principio de programación hacia interfaces, no implementaciones.
public interface INotification
{
    // Método para enviar la notificación, implementado de forma distinta por cada tipo.
    void Send(string message, string recipient);
}

// 📨 Implementación concreta de la notificación Email.
// Encapsula la lógica específica para enviar un email.
public class EmailNotification : INotification
{
    public void Send(string message, string recipient)
    {
        Console.WriteLine($"📧 Enviando Email a {recipient}: {message}");
    }
}

// 📱 Implementación concreta de la notificación SMS.
public class SmsNotification : INotification
{
    public void Send(string message, string recipient)
    {
        Console.WriteLine($"📱 Enviando SMS a {recipient}: {message}");
    }
}

// 🔔 Implementación concreta de la notificación Push.
public class PushNotification : INotification
{
    public void Send(string message, string recipient)
    {
        Console.WriteLine($"🔔 Enviando Push a {recipient}: {message}");
    }
}

// 🏭 Factory Method: clase estática que centraliza la creación de objetos INotification.
// Aquí se encapsula el conocimiento sobre qué clase concreta se instancia según el parámetro "type".
// Esto permite separar la creación (Factory) de la lógica de uso (NotificationService).
public static class NotificationFactory
{
    public static INotification CreateNotification(string type)
    {
        // Patrón "switch" moderno para decidir qué objeto crear.
        // Centraliza el código de instanciación y evita condicionales dispersos.
        // Esta fábrica cumple el rol de "Creator" en el Factory Method.
        return type.ToLower() switch
        {
            "email" => new EmailNotification(),
            "sms" => new SmsNotification(),
            "push" => new PushNotification(),
            _ => throw new ArgumentException("Tipo de notificación no soportado")
        };
    }
}

// 🎯 Servicio que utiliza la fábrica para obtener la instancia adecuada de notificación y enviar el mensaje.
// Se encarga sólo de la lógica de negocio, no de la creación de objetos (separa responsabilidades, SRP).
public class NotificationService
{
    public void SendNotification(string type, string message, string recipient)
    {
        // Se delega la creación de la notificación al Factory Method.
        INotification notification = NotificationFactory.CreateNotification(type);

        // Se utiliza el objeto creado para enviar la notificación.
        notification.Send(message, recipient);
    }
}

// 👇 Código de ejemplo para usar el servicio refactorizado
var service = new NotificationService();
service.SendNotification("email", "Hola Mundo!", "juan@correo.com");
service.SendNotification("sms", "Hola Mundo!", "+123456789");
service.SendNotification("push", "Hola Mundo!", "device_token");

```
