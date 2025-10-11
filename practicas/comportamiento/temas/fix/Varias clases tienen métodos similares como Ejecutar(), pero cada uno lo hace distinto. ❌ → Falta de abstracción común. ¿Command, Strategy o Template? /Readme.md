Ricardo Rodriguez Carreras Ricardo 21212360
# 💡 Solución  — Patrones de Comportamiento (GoF)

## Descripción del Problema
En el código actual, varias clases tienen métodos similares llamados `Ejecutar()`, pero cada uno realiza tareas diferentes. Esto genera **code smells** como:

- Falta de abstracción común
- Duplicación de lógica
- Dificultad para extender o modificar comportamientos sin alterar otras clases

Este escenario requiere un refactor que permita **unificar la interfaz de ejecución** y mantener la flexibilidad de los distintos comportamientos.

---

## Patrón Recomendado
**Strategy (GoF)**

### Justificación
- Permite encapsular distintos algoritmos o comportamientos en clases separadas.  
- Facilita que el **Contexto** cambie el comportamiento en tiempo de ejecución sin modificar las clases existentes.  
- Mejora la extensibilidad y reduce duplicación de código.

### Alternativas
- **Command**: si se necesitan operaciones que se traten como objetos (historial, undo/redo, colas).  
- **Template Method**: si existe un flujo común con pasos fijos y algunas variaciones.

> ✅ En este caso, **Strategy es la opción más apropiada** porque todas las clases comparten la firma `Ejecutar()`, pero la lógica interna varía.
---
Breve Reflexión
---

Con este patrón aprendí que encapsular comportamientos en clases separadas permite mantener un código más limpio, flexible y fácil de extender. Antes, cada clase duplicaba lógica y cualquier cambio requería modificar varias clases; ahora, puedo agregar o cambiar comportamientos sin afectar el resto del sistema.

Conclusión
---

Aplicando Strategy se mejora la flexibilidad del código, se reduce duplicación y se facilita la incorporación de nuevas acciones. La solución es clara, extensible y resuelve el code smell identificado de manera efectiva.

Resultado
---
<img width="552" height="642" alt="image" src="https://github.com/user-attachments/assets/070ed73f-9b84-450d-b2cb-7d89728d53c1" />


## Ejemplo Implementación — Strategy en C#

```csharp
using System;

// Interfaz común (Strategy)
public interface IEjecutor
{
    void Ejecutar();
}

// Implementaciones distintas
public class EjecutarImpresion : IEjecutor
{
    public void Ejecutar()
    {
        Console.WriteLine("Ejecución: imprimir documento...");
    }
}

public class EjecutarGuardado : IEjecutor
{
    public void Ejecutar()
    {
        Console.WriteLine("Ejecución: guardar cambios en la base de datos...");
    }
}

public class EjecutarEnvio : IEjecutor
{
    public void Ejecutar()
    {
        Console.WriteLine("Ejecución: enviar notificación por correo...");
    }
}

// Contexto que utiliza una estrategia
public class ContextoDeAccion
{
    private IEjecutor _ejecutor;

    public ContextoDeAccion(IEjecutor ejecutor)
    {
        _ejecutor = ejecutor;
    }

    public void SetStrategy(IEjecutor ejecutor)
    {
        _ejecutor = ejecutor;
    }

    public void EjecutarAccion()
    {
        Console.WriteLine("Contexto: iniciando acción...");
        _ejecutor.Ejecutar();
        Console.WriteLine("Contexto: acción finalizada.\n");
    }
}

public class Program
{
    public static void Main()
    {
        var contexto = new ContextoDeAccion(new EjecutarImpresion());
        contexto.EjecutarAccion();

        contexto.SetStrategy(new EjecutarGuardado());
        contexto.EjecutarAccion();

        contexto.SetStrategy(new EjecutarEnvio());
        contexto.EjecutarAccion();
    }
}
