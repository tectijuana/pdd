# Refactorización con Facade: evitar exponer detalles internos del sistema

**Tema:** Hacer que el Facade exponga detalles internos del sistema  
Jesus Antonio Triana Corvera - C20212681

---

## 1. Identificación de Code Smells 

**Problemas identificados:**

1. **Exposición directa de parámetros internos**  
   - El Facade obliga al cliente a pasar `parametroInterno` y `valorInterno`, que pertenecen al subsistema.  
   - Esto rompe la **encapsulación** y filtra detalles internos.  

2. **Violación de la Ley de Deméter**  
   - El cliente debe saber qué métodos invocar y en qué orden.  
   - Esto convierte al Facade en un simple pasacables en lugar de simplificar la interacción.  

3. **Acoplamiento fuerte con clases concretas**  
   - Si cambian las operaciones internas, también debe cambiar el cliente.  
   - Se viola el principio de inversión de dependencias (**DIP**), haciendo el sistema frágil.  

*(Señalados en el código ANTES con comentarios `// ERROR`)*  

---

## 2. Aplicación del patrón adecuado 

Patrón aplicado: **Facade (GoF)**.  

Objetivo: simplificar el acceso al sistema y **ocultar los detalles internos** de los subsistemas.  

Medidas tomadas:  
- Se definieron **DTOs (request/response)** como contratos estables.  
- Se encapsuló la lógica interna y el orden de ejecución dentro del Facade.  
- Se expone al cliente un método de alto nivel (`ProcesarAltaCliente`) en lugar de parámetros crudos.  

---

## 3. Refactor funcional

### Código ANTES (con problemas estructurales):

```csharp
public class SubsistemaA
{
    public void OperacionA(string parametroInterno /* ERROR: detalle interno expuesto */)
    {
        Console.WriteLine($"OperacionA con {parametroInterno}");
    }
}

public class SubsistemaB
{
    public void OperacionB(int valorInterno /* ERROR: detalle interno filtrado */)
    {
        Console.WriteLine($"OperacionB con {valorInterno}");
    }
}

public class SistemaFacade
{
    private readonly SubsistemaA _subA = new();
    private readonly SubsistemaB _subB = new();

    public void EjecutarOperacionA(string parametroInterno)
    {
        _subA.OperacionA(parametroInterno); // ERROR
    }

    public void EjecutarOperacionB(int valorInterno)
    {
        _subB.OperacionB(valorInterno); // ERROR
    }
}

public static class Program_Before
{
    public static void Main()
    {
        var facade = new SistemaFacade();
        facade.EjecutarOperacionA("config-123"); // ERROR: detalle interno filtrado
        facade.EjecutarOperacionB(42);           // ERROR: detalle interno filtrado
    }
}
```

### Código DESPUÉS (con refactor correcto):

```csharp
// DTOs públicos
public sealed class ProcesoRequest
{
    public required string NombreCliente { get; init; }
    public required string Email { get; init; }
}

public sealed class ProcesoResponse
{
    public required string CodigoResultado { get; init; }
    public required string Mensaje { get; init; }
    public DateTimeOffset Fecha { get; init; } = DateTimeOffset.UtcNow;
}

// Subsistemas internos ocultos
internal class SubsistemaA
{
    public void OperacionA(string parametroInterno)
    {
        Console.WriteLine($"OperacionA con {parametroInterno}");
    }
}

internal class SubsistemaB
{
    public void OperacionB(int valorInterno)
    {
        Console.WriteLine($"OperacionB con {valorInterno}");
    }
}

// Facade correcto
public class SistemaFacade
{
    private readonly SubsistemaA _subA;
    private readonly SubsistemaB _subB;

    public SistemaFacade()
    {
        _subA = new SubsistemaA();
        _subB = new SubsistemaB();
    }

    public ProcesoResponse ProcesarAltaCliente(ProcesoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NombreCliente))
            return new ProcesoResponse { CodigoResultado = "ERR", Mensaje = "Nombre requerido" };

        if (string.IsNullOrWhiteSpace(request.Email))
            return new ProcesoResponse { CodigoResultado = "ERR", Mensaje = "Email requerido" };

        // Parámetros internos controlados por el Facade
        string parametroInterno = "config-interna";
        int valorInterno = 100;

        _subA.OperacionA(parametroInterno);
        _subB.OperacionB(valorInterno);

        return new ProcesoResponse
        {
            CodigoResultado = "OK",
            Mensaje = $"Cliente {request.NombreCliente} registrado con éxito."
        };
    }
}

// Cliente externo simplificado
public static class Program_After
{
    public static void Main()
    {
        var facade = new SistemaFacade();
        var response = facade.ProcesarAltaCliente(new ProcesoRequest
        {
            NombreCliente = "Ana López",
            Email = "ana@example.com"
        });
        Console.WriteLine($"{response.CodigoResultado} - {response.Mensaje}");
    }
}
```

*(Señalados en el código DESPUÉS con comentarios `// SOLUCIÓN`)*  

---

## 4. Justificación técnica

**Problema:** el Facade exponía detalles internos, obligando al cliente a conocer configuraciones internas.  
**Patrón aplicado:** Facade correcto, que encapsula los subsistemas y provee una API de alto nivel con DTOs.  
**Beneficios:**  
- Encapsulación real de subsistemas.  
- Contratos estables y claros.  
- Cliente desacoplado.  
- Mayor mantenibilidad y claridad del código.  



## Conclusión

Se corrigió el *code smell* **“Hacer que el Facade exponga detalles internos del sistema”**.  
El nuevo diseño aplica correctamente el patrón Facade, brindando una API pública estable y clara, oculta la complejidad de los subsistemas y reduce el acoplamiento.  
