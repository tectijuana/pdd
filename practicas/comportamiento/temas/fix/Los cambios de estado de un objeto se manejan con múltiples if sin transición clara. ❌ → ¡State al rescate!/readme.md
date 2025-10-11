# 🧩 Refactorización – Patrón de Comportamiento: STATE

## 💥 Problema Detectado
**"Los cambios de estado de un objeto se manejan con múltiples `if` sin transición clara."**

Este tipo de problema (code smell) ocurre cuando un objeto tiene varios estados y sus comportamientos se controlan mediante una serie de **condicionales anidados** (`if`, `else if`, `switch`).  
Esto hace que el código sea **difícil de mantener**, **poco escalable** y **confuso** al intentar agregar o modificar estados.

---

## 🎯 Objetivo del Refactor
Aplicar el **Patrón de Comportamiento “State” (GoF)** para eliminar los condicionales y **delegar el comportamiento a clases que representen cada estado del objeto**, permitiendo que el mismo cambie dinámicamente su comportamiento al cambiar su estado.

---

## ⚙️ Ejemplo Antes del Refactor

```csharp
using System;

public class Document
{
    public string State { get; set; } = "Draft";

    public void Publish()
    {
        if (State == "Draft")
        {
            Console.WriteLine("Publicando documento...");
            State = "Moderation";
        }
        else if (State == "Moderation")
        {
            Console.WriteLine("Documento ya enviado a moderación.");
        }
        else if (State == "Published")
        {
            Console.WriteLine("Documento ya publicado.");
        }
        else
        {
            Console.WriteLine("Estado desconocido.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        var doc = new Document();
        doc.Publish();
        doc.Publish();
        doc.Publish();
    }
}
```

### 🔴 Problemas detectados
- Múltiples `if` controlando los estados.
- Dificultad para agregar nuevos estados sin romper código existente.
- Comportamientos mezclados dentro de una sola clase.
- No hay transiciones claras ni encapsulamiento de la lógica por estado.

---

## ✅ Refactor Aplicando Patrón STATE

```csharp
using System;

// Interfaz común para todos los estados
public interface IState
{
    void Publish(Document doc);
}

// Estado: Borrador
public class DraftState : IState
{
    public void Publish(Document doc)
    {
        Console.WriteLine("Publicando documento...");
        doc.SetState(new ModerationState());
    }
}

// Estado: En moderación
public class ModerationState : IState
{
    public void Publish(Document doc)
    {
        Console.WriteLine("Documento ya enviado a moderación. Pasando a publicado...");
        doc.SetState(new PublishedState());
    }
}

// Estado: Publicado
public class PublishedState : IState
{
    public void Publish(Document doc)
    {
        Console.WriteLine("Documento ya publicado.");
    }
}

// Contexto principal
public class Document
{
    private IState _state;

    public Document()
    {
        _state = new DraftState(); // Estado inicial
    }

    public void SetState(IState state)
    {
        _state = state;
    }

    public void Publish()
    {
        _state.Publish(this);
    }
}

// Programa de prueba
public class Program
{
    public static void Main()
    {
        var doc = new Document();
        doc.Publish(); // Publica y pasa a Moderación
        doc.Publish(); // Pasa a Publicado
        doc.Publish(); // Ya está publicado
    }
}
```

---

## 🧠 Justificación del Patrón

El **patrón State** permite que un objeto altere su comportamiento cuando cambia su estado interno.  
En lugar de usar condicionales, el objeto delega su comportamiento a **clases concretas que representan cada estado**.  

### 💡 Beneficios del Patrón
| Aspecto | Antes del Refactor | Después del Refactor (State) |
|----------|-------------------|------------------------------|
| **Mantenibilidad** | Baja: muchos `if` | Alta: clases separadas por estado |
| **Escalabilidad** | Difícil agregar estados nuevos | Fácil: se crean nuevas clases |
| **Legibilidad** | Confusa y repetitiva | Clara y modular |
| **Principios SOLID** | Viola SRP y OCP | Cumple SRP (Responsabilidad Única) y OCP (Abierto/Cerrado) |

### 📚 Referencia GoF
> “Permite que un objeto altere su comportamiento cuando su estado interno cambia.  
> El objeto parecerá cambiar de clase.”  
> — *Design Patterns, Gamma, Helm, Johnson, Vlissides (GoF)*

---

## 🧩 Resultado del Refactor
- Se eliminan las estructuras condicionales repetitivas.
- El flujo de cambio de estado es más claro y extensible.
- Se cumple con los principios de **Clean Code** y **SOLID**.
- El código es más fácil de mantener, probar y entender.

---

## 💬 Reflexión Personal

Durante este ejercicio comprendí cómo los **patrones de comportamiento** ayudan a mejorar la forma en que los objetos interactúan y cambian su comportamiento.  
El patrón **State** me permitió eliminar estructuras condicionales innecesarias y entender la importancia de **delegar responsabilidades** a clases específicas.  

Aprendí que un código limpio no solo se trata de que funcione, sino de que **pueda evolucionar sin romper lo existente**.  
Además, este patrón fomenta el pensamiento modular, la reutilización y la claridad en las transiciones de estados.

---

## 🤖 Uso Ético de la Inteligencia Artificial

El uso de IA (como ChatGPT) en esta práctica fue únicamente con fines **educativos y de apoyo técnico**.  
El código fue analizado, comprendido y probado por el estudiante.  
La IA se utilizó para **apoyar la estructuración del documento**, la explicación teórica y el formato del README, manteniendo siempre la autoría y comprensión del alumno.

