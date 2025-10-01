# 📝 Refactorización Tema 37 – Confundir Decorator con Proxy o Adapter - Joshua Ruiz Lopez 21212363

## 📌 Nombre del problema
Confusión entre Decorator, Proxy y Adapter al extender funcionalidad de un servicio

## 🔍 Identificación de Code Smells
En el módulo `ServicioNotificaciones` se encontraron los siguientes problemas:
- Se implementó un “decorador” que en realidad actúa como un Proxy (controla acceso y crea la instancia interna).
- Mezcla de responsabilidades: el “decorador” valida y adapta la interfaz (propio de un Adapter), además de añadir funcionalidad.
- El cliente no sabe si está usando un Decorator real o un Proxy, rompiendo el principio de sustitución.

## 🛠️ Patrón aplicado
Se refactorizó usando **Decorator** verdadero:  
- Mantener la **misma interfaz** del componente decorado.  
- Delegar siempre al componente interno.  
- Añadir comportamiento adicional sin controlar acceso ni adaptar interfaces (función del Proxy/Adapter).  

## 💻 Código Antes (Confusión Decorator/Proxy/Adapter)
```csharp
// Interfaz original
public interface IServicio
{
    void Enviar(string mensaje);
}

// Implementación concreta
public class ServicioCorreo : IServicio
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine("Enviando correo: " + mensaje);
    }
}

// “Decorador” mal implementado (es Proxy + Adapter)
public class ServicioDecoradorCorreo : IServicio
{
    private ServicioCorreo _servicio; // dependencia concreta

    public void Enviar(string mensaje)
    {
        if (_servicio == null) _servicio = new ServicioCorreo(); // Proxy creando instancia
        if (mensaje.Length > 160) mensaje = mensaje.Substring(0, 160); // Adaptación
        Console.WriteLine("[Decorador] Validando mensaje");
        _servicio.Enviar(mensaje);
    }
}

// Uso en cliente
IServicio servicio = new ServicioDecoradorCorreo();
servicio.Enviar("Mensaje largo...");

## 💻 Código Después (Decorator Correcto)
// Interfaz original
public interface IServicio
{
    void Enviar(string mensaje);
}

// Implementación concreta
public class ServicioCorreo : IServicio
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine("Enviando correo: " + mensaje);
    }
}

// Decorator base correcto
public abstract class ServicioDecorator : IServicio
{
    protected readonly IServicio _servicio;

    protected ServicioDecorator(IServicio servicio)
    {
        _servicio = servicio;
    }

    public virtual void Enviar(string mensaje)
    {
        _servicio.Enviar(mensaje);
    }
}

// Decorator concreto que añade comportamiento
public class ServicioConLog : ServicioDecorator
{
    public ServicioConLog(IServicio servicio) : base(servicio) { }

    public override void Enviar(string mensaje)
    {
        Console.WriteLine("[Log] Enviando mensaje...");
        base.Enviar(mensaje);
    }
}

// Uso en cliente
IServicio servicio = new ServicioConLog(new ServicioCorreo());
servicio.Enviar("Hola mundo");
