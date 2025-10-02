# 🎨 Práctica: Refactorización con Patrón Decorator

## 📌 Contexto
En el código base proporcionado existía un problema con el uso del patrón **Decorator**:  
algunos decoradores **no llamaban al componente base** al ejecutar sus operaciones, lo que generaba pérdida de funcionalidad y rompía la cadena de responsabilidades.

El objetivo de esta práctica fue **refactorizar** ese problema aplicando correctamente el patrón **Decorator** en **C# (.NET 8)**.

---

## 🧩 Problema Detectado
El **Code Smell** fue:

- **Decoradores que no llaman al componente base.**

Esto provocaba que, al usar varios decoradores encadenados, algunos comportamientos desaparecieran en lugar de sumarse.

### ❌ Ejemplo del código con error
```csharp
public interface INotificacion
{
    void Enviar(string mensaje);
}

public class NotificacionBase : INotificacion
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine($"Enviando notificación: {mensaje}");
    }
}

public class NotificacionEmail : INotificacion
{
    private INotificacion _base;

    public NotificacionEmail(INotificacion baseNotificacion)
    {
        _base = baseNotificacion;
    }

    public void Enviar(string mensaje)
    {
        Console.WriteLine("Enviando copia por Email"); 
        // ❌ No llama al _base.Enviar(mensaje)
    }
}


🔧 Refactor Aplicado

El refactor consistió en corregir los decoradores para que siempre invocaran al componente base antes o después de añadir su lógica extra.

public class NotificacionEmail : INotificacion
{
    private INotificacion _base;

    public NotificacionEmail(INotificacion baseNotificacion)
    {
        _base = baseNotificacion;
    }

    public void Enviar(string mensaje)
    {
        _base.Enviar(mensaje); // ✅ Se mantiene la funcionalidad base
        Console.WriteLine("Enviando copia por Email");
    }
}

public class NotificacionSMS : INotificacion
{
    private INotificacion _base;

    public NotificacionSMS(INotificacion baseNotificacion)
    {
        _base = baseNotificacion;
    }

    public void Enviar(string mensaje)
    {
        _base.Enviar(mensaje); // ✅ Cadena continua
        Console.WriteLine("Enviando notificación por SMS");
    }
}

📬 Justificación Técnica

Problema: Decoradores que no llamaban al componente base interrumpían la cadena de ejecución.

Solución aplicada: Patrón Decorator implementado correctamente.

Beneficios:

Se preserva el comportamiento original de la clase base.

Cada decorador añade funcionalidad de forma flexible.

Se cumple el principio de Open/Closed (abierto a extensión, cerrado a modificación).

Código más mantenible y escalable.

🎯 Conclusiones

Identifiqué y corregí un Code Smell estructural relacionado con el patrón Decorator.

Logré que la cadena de decoradores se ejecutara correctamente.

El refactor demuestra cómo un detalle (no invocar al componente base) puede romper la intención del patrón.

Esta práctica refuerza la importancia de aplicar patrones de diseño con responsabilidad y disciplina.
