#  Caso #41 — Eliminación de condicionales por tipo con Strategy Pattern
Jesus Antonio Triana Corvera - C20212681

## Descripción general
Este ejercicio forma parte de la práctica de **Patrones de Comportamiento (GoF)**.  
El objetivo fue refactorizar un código con múltiples condicionales (`if/switch`) que decidían la configuración a aplicar según el tipo de objeto (`config.Tipo`), aplicando el patrón **Strategy** para mejorar extensibilidad y mantener el código limpio.

---

## Identificación del Code Smell

**Problema original:**
```csharp
if (config.Tipo == "A") { ... }
else if (config.Tipo == "B") { ... }
else if (config.Tipo == "C") { ... }
```

### 🔍 Problemas detectados
- Uso excesivo de condicionales por tipo (*Type Checking*).
- Violación del **Principio Abierto/Cerrado (OCP)**.
- Código difícil de mantener y extender.
- Repetición y baja cohesión.
- Dificultad para probar individualmente cada comportamiento.

---

## Patrón GoF aplicado

**Patrón:**  **Strategy**

### Justificación
- Encapsula comportamientos intercambiables (estrategias) detrás de una interfaz común (`ISetupStrategy`).
- Elimina condicionales al delegar el comportamiento a objetos específicos.
- Facilita agregar nuevas estrategias sin modificar el contexto principal.
- Mejora la cohesión, testabilidad y mantenibilidad.

> Alternativas consideradas:
> - **State:** aplica cuando el comportamiento cambia por transición de estados internos.  
> - **Template Method:** útil cuando hay pasos fijos y pasos variables dentro de un mismo flujo.  
> En este caso, el comportamiento depende del tipo de configuración, por lo que **Strategy** es el patrón ideal.

---


## Código original (antes del refactor)

```csharp
public class Configuracion {
    public string Tipo { get; set; }
    public string Nombre { get; set; }
}

public static class Program {
    public static void Main() {
        var cfg = new Configuracion { Tipo = "A", Nombre = "ModuloVentas" };

        if (cfg.Tipo == "A") {
            Console.WriteLine($"Config A para {cfg.Nombre}: habilitar cache y logs.");
        } else if (cfg.Tipo == "B") {
            Console.WriteLine($"Config B para {cfg.Nombre}: modo seguro y validaciones extra.");
        } else if (cfg.Tipo == "C") {
            Console.WriteLine($"Config C para {cfg.Nombre}: compresión y métricas.");
        } else {
            Console.WriteLine($"Tipo {cfg.Tipo} no soportado.");
        }
    }
}
```

![alt text](image-3.png)

---

## Código refactorizado (Strategy aplicado)

```csharp
using System;
using System.Collections.Generic;

public interface ISetupStrategy {
    void Configure(string nombre);
}

public class TipoA : ISetupStrategy {
    public void Configure(string nombre) =>
        Console.WriteLine($"[A] {nombre}: cache + logs.");
}

public class TipoB : ISetupStrategy {
    public void Configure(string nombre) =>
        Console.WriteLine($"[B] {nombre}: modo seguro.");
}

public class Configurator {
    private readonly Dictionary<string, ISetupStrategy> _map = new() {
        ["A"] = new TipoA(),
        ["B"] = new TipoB()
    };

    public void Apply(string tipo, string nombre) {
        if (_map.TryGetValue(tipo, out var strat))
            strat.Configure(nombre);
        else
            Console.WriteLine($"Tipo {tipo} no soportado.");
    }
}

public class Program {
    public static void Main() {
        var cfg = new Configurator();
        cfg.Apply("A", "ModuloVentas");
        cfg.Apply("B", "ModuloPagos");
        cfg.Apply("X", "Desconocido");
    }
}

```
![alt text](image.png)

---

## Reflexión del estudiante

> En esta práctica aprendí que el patrón **Strategy** es una forma eficiente de eliminar condicionales extensos, sustituyéndolos por polimorfismo.  
> Comprendí cómo aplicar el **principio abierto/cerrado**, cómo delegar responsabilidades de forma clara y cómo facilitar la extensión del sistema sin romper su estructura.  
> También me di cuenta de que los patrones de comportamiento no solo mejoran el diseño, sino que hacen que el código sea más fácil de probar y mantener.

---

**Conclusión:**  
El patrón **Strategy** facilita el mantenimiento del software al permitir agregar comportamientos sin modificar código existente.  
Con este refactor, se reemplazan estructuras condicionales rígidas por un diseño limpio, flexible y extensible.
