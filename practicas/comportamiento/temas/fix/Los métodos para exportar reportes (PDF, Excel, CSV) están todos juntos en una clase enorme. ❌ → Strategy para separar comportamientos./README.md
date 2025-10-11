# 🧩 Tema 37 — Code Smell: Métodos para exportar reportes (PDF, Excel, CSV) en una sola clase

## 🔎 Descripción del Problema

En el sistema actual, existe una **clase enorme** llamada `ExportadorReportes` que contiene **métodos para exportar reportes en distintos formatos**: PDF, Excel y CSV.

Esto genera varios problemas:

- La clase **viola el Principio de Responsabilidad Única (SRP)**.
- Es **difícil de mantener o extender**: si se agrega un nuevo formato, hay que **modificar la misma clase**.
- El código **rompe el principio Open/Closed (OCP)** porque no está abierto a extensión sin modificación.

---
## Imagen
<img width="914" height="786" alt="image" src="https://github.com/user-attachments/assets/7b4fdbbf-685b-430a-b83a-49ca7f35ad9e" />

---

## 💣 Code Smell Original

```csharp
public class ExportadorReportes
{
    public void Exportar(string tipo)
    {
        if (tipo == "PDF")
        {
            Console.WriteLine("Exportando reporte a PDF...");
        }
        else if (tipo == "EXCEL")
        {
            Console.WriteLine("Exportando reporte a Excel...");
        }
        else if (tipo == "CSV")
        {
            Console.WriteLine("Exportando reporte a CSV...");
        }
        else
        {
            Console.WriteLine("Formato no soportado.");
        }
    }
}

// Uso
var exportador = new ExportadorReportes();
exportador.Exportar("PDF");

---
✅ Refactorización con Strategy
using System;

// Estrategia base
public interface IExportStrategy
{
    void Exportar();
}

// Estrategias concretas
public class ExportarPDF : IExportStrategy
{
    public void Exportar() => Console.WriteLine("📄 Exportando reporte en formato PDF...");
}

public class ExportarExcel : IExportStrategy
{
    public void Exportar() => Console.WriteLine("📊 Exportando reporte en formato Excel...");
}

public class ExportarCSV : IExportStrategy
{
    public void Exportar() => Console.WriteLine("📋 Exportando reporte en formato CSV...");
}

// Contexto que usa la estrategia
public class ExportadorContexto
{
    private IExportStrategy _estrategia;

    public ExportadorContexto(IExportStrategy estrategia)
    {
        _estrategia = estrategia;
    }

    public void SetEstrategia(IExportStrategy nuevaEstrategia)
    {
        _estrategia = nuevaEstrategia;
    }

    public void Exportar()
    {
        _estrategia.Exportar();
    }
}

// Ejemplo de uso
public class Program
{
    public static void Main()
    {
        // Exportar a PDF
        var contexto = new ExportadorContexto(new ExportarPDF());
        contexto.Exportar();

        // Cambiar a Excel
        contexto.SetEstrategia(new ExportarExcel());
        contexto.Exportar();

        // Cambiar a CSV
        contexto.SetEstrategia(new ExportarCSV());
        contexto.Exportar();
    }
}

🧾 Justificación del Patrón Elegido

Strategy separa los comportamientos relacionados con la exportación, eliminando condicionales y favoreciendo la extensión.

Permite agregar nuevos formatos (por ejemplo, JSON o XML) sin modificar el código del contexto.

Cumple con los principios OCP y SRP.

| Problema Original             | Solución con Strategy                                               |
| ----------------------------- | ------------------------------------------------------------------- |
| Muchos `if` para cada formato | Cada formato es una clase independiente                             |
| Dificultad para extender      | Se pueden agregar nuevas estrategias sin modificar código existente |
| Clase grande y rígida         | Código modular y limpio                                             |
| Violación de SRP              | Cada clase tiene una sola responsabilidad                           |

🧩 Conclusión

El uso del patrón Strategy permite reducir la complejidad del sistema, aumentar la mantenibilidad y mejorar la escalabilidad del módulo de exportación.
Cada tipo de exportación se convierte en una estrategia intercambiable, eliminando los condicionales innecesarios y favoreciendo la extensibilidad del sistema.
