# 🛠️ Refactorización de Patrones Estructurales (GoF)

Alumno:  Luis Felipe Torres Coto Rodarte
Num. Control: 21212368  

---

## 📌 Contexto
El patrón Decorador (GoF) se utiliza para añadir responsabilidades a objetos de manera dinámica, evitando herencia rígida y favoreciendo la composición. Sin embargo, cuando se encadenan múltiples decoradores similares o con responsabilidades repetidas, aparece un problema: comportamiento duplicado.

---

## ✅ Code Smells detectados
1. **Repeticion involuntaria:** Se aplica ExclamationMessage dos veces obteniendo !!! duplicados. 
2. **Acopilacion por orden:** El resultado depende del orden de aplicación de decoradores (UpperCase luego Exclamation ≠ Exclamation luego UpperCase en transformaciones más complejas). 
3. **Falta de una abstraccion base para decoradores:** Cada decorador repite la envoltura de IMessage (private readonly IMessage _inner; constructor). 

---

## 🏗️ Patrones aplicados
- **Composite**  
  Permite reemplazar la cadena de `if/else` con una jerarquía flexible de handlers.  
  👉 Beneficio: fácil agregar nuevos handlers sin modificar el código existente.

- **Adapter**  
  Integra `LegacyLogger` con la interfaz `ILogger` del sistema moderno.  
  👉 Beneficio: se mantiene el código legado sin modificarlo, habilitando inyección de dependencias y pruebas.

---

## 🔄 Refactor realizado

### 🔴 Antes
```csharp
public interface IMessage
{
    string GetContent();
}

public class SimpleMessage : IMessage
{
    public string GetContent() => "Hola mundo";
}

public class UpperCaseMessage : IMessage
{
    private readonly IMessage _inner;
    public UpperCaseMessage(IMessage inner) => _inner = inner;
    public string GetContent() => _inner.GetContent().ToUpper();
}

public class ExclamationMessage : IMessage
{
    private readonly IMessage _inner;
    public ExclamationMessage(IMessage inner) => _inner = inner;
    public string GetContent() => _inner.GetContent() + "!!!";
}

```
Este codigo tiene como problema el tener varios decoradores repetidos (UpperCaseMessge) el cual se repite 2 veces.  

### 🟢 Después
```csharp
public interface IMessage
{
    string GetContent();
}

public class SimpleMessage : IMessage
{
    public string GetContent() => "Hola mundo";
}

// Decorador base
public abstract class MessageDecorator : IMessage
{
    protected readonly IMessage _inner;
    public MessageDecorator(IMessage inner) => _inner = inner;
    public abstract string GetContent();
}

// Decoradores concretos
public class UpperCaseDecorator : MessageDecorator
{
    public UpperCaseDecorator(IMessage inner) : base(inner) { }
    public override string GetContent() => _inner.GetContent().ToUpper();
}

public class ExclamationDecorator : MessageDecorator
{
    public ExclamationDecorator(IMessage inner) : base(inner) { }
    public override string GetContent() => _inner.GetContent() + "!!!";
}
```

---

## 📂 Estructura del proyecto

```
src/
 ├── Handlers/
 │   ├── IHandler.cs
 │   ├── ConcreteHandler.cs
 │   └── CompositeHandler.cs
 ├── Logging/
 │   ├── ILogger.cs
 │   ├── LegacyLogger.cs
 │   └── LegacyLoggerAdapter.cs
 └── Program.cs
```

---

## 📜 Justificación técnica (para PR)

1. **Problema:** cadena de `if/else` en `HandlerService`.  
   **Solución:** aplicar **Composite** → elimina condicionales, facilita extensibilidad.  

2. **Problema:** `LegacyLogger` no implementa `ILogger`.  
   **Solución:** aplicar **Adapter** → desacopla API legacy de la nueva interfaz, habilita DI.  

3. **Beneficio global:**  
   - Código más **mantenible y extensible**.  
   - Facilita **pruebas unitarias**.  
   - Evita **modificar código legacy**.  

---

## 🟢 Conclusión
La refactorización aplica correctamente patrones **Composite** y **Adapter** para resolver problemas estructurales comunes, asegurando un código más limpio, extensible y mantenible en .NET 8.
