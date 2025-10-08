# 🧩 1. Contexto del problema

Cuando un código ejecuta una secuencia de pasos (por ejemplo, un proceso, algoritmo o flujo de trabajo), pero:

* Algunos pasos siempre deben ejecutarse (obligatorios),

* Otros pasos pueden cambiar o no siempre aplican (opcionales),

* Y no hay una estructura clara que separe qué partes son fijas y cuáles son personalizables...

👉 se genera código **confuso, difícil de mantener y extender.**

Este es un síntoma clásico de que podría aplicarse el Patrón Template Method.

---

# 🧠 2. Qué es el Patrón Template Method

El Template Method es un patrón de diseño comportamental (behavioral pattern) que:

Define el esqueleto de un algoritmo en una clase base, permitiendo que las subclases redefinan ciertos pasos sin cambiar la estructura general del algoritmo.

En otras palabras:

* La clase base establece qué pasos hay y en qué orden.

* Las subclases definen cómo se implementan los pasos personalizables.

---

# ⚙️ 3. Estructura general

```C#
abstract class ProcesoBase
{
    // Template Method → define la secuencia general
    public void Ejecutar()
    {
        PasoObligatorio1();
        PasoObligatorio2();
        if (PasoOpcional()) // opcional
            PasoAdicional();
        PasoFinal();
    }

    protected abstract void PasoObligatorio1();
    protected abstract void PasoObligatorio2();

    protected virtual bool PasoOpcional() => true; // gancho (hook)
    protected virtual void PasoAdicional() { }

    protected void PasoFinal()
    {
        Console.WriteLine("Finalizando proceso...");
    }
}
```

Y luego cada subclase define su propia versión de los pasos:
```C#
class ProcesoConcretoA : ProcesoBase
{
    protected override void PasoObligatorio1() => Console.WriteLine("Paso 1 - Versión A");
    protected override void PasoObligatorio2() => Console.WriteLine("Paso 2 - Versión A");
    protected override bool PasoOpcional() => false; // desactiva el paso opcional
}
```
# 🧩 4. Cuándo aplicarlo

Usa Template Method cuando:

* Situación	Señal de uso
* Hay un proceso con varios pasos definidos	✔️
* Algunos pasos deben ser fijos y otros personalizables	✔️
* Quieres evitar duplicación en algoritmos similares	✔️
* El código actual tiene if/else o flags para decidir qué hacer	🚨 Indica mala estructura

---

# 📉 5. Problema común (sin Template Method)

Ejemplo de código malo:
```C#
public class Reporte
{
    public void GenerarReporte(bool incluirGrafica, bool exportarPDF)
    {
        ObtenerDatos();
        ProcesarDatos();
        if (incluirGrafica) AgregarGrafica();
        if (exportarPDF) ExportarPDF();
        Console.WriteLine("Reporte generado.");
    }
}
```

Este código mezcla pasos fijos (obtener/procesar) con opcionales (gráfica, exportar PDF).
Cada vez que haya una nueva variante del reporte, se duplicará o llenará de condicionales.


## Codigo completo para la ejecucion
```C#
using System;

class Reporte
{
    // Método que mezcla pasos obligatorios y opcionales
    public void GenerarReporte(bool incluirGrafica, bool exportarPDF)
    {
        ObtenerDatos();
        ProcesarDatos();

        // Lógica opcional mezclada directamente en el flujo principal
        if (incluirGrafica)
            AgregarGrafica();

        if (exportarPDF)
            ExportarPDF();

        Console.WriteLine("Reporte generado.\n");
    }

    private void ObtenerDatos()
    {
        Console.WriteLine("Obteniendo datos...");
    }

    private void ProcesarDatos()
    {
        Console.WriteLine("Procesando información...");
    }

    private void AgregarGrafica()
    {
        Console.WriteLine("Agregando gráfica al reporte...");
    }

    private void ExportarPDF()
    {
        Console.WriteLine("Exportando reporte a PDF...");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Generando reporte simple ===");
        Reporte reporte = new Reporte();
        reporte.GenerarReporte(false, false);

        Console.WriteLine("=== Generando reporte con gráfica ===");
        reporte.GenerarReporte(true, false);

        Console.WriteLine("=== Generando reporte completo ===");
        reporte.GenerarReporte(true, true);

        Console.WriteLine("Presiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
```

## Salida
<img width="333" height="263" alt="image" src="https://github.com/user-attachments/assets/19279c91-50be-4861-8131-2f18141e8f3b" />


# ✅ 6. Solución con Template Method

```C#
abstract class ReporteTemplate
{
    public void Generar()
    {
        ObtenerDatos();
        ProcesarDatos();
        if (IncluirGrafica()) AgregarGrafica();
        if (ExportarPDF()) ExportarPDF();
        Finalizar();
    }

    protected abstract void ObtenerDatos();
    protected abstract void ProcesarDatos();
    protected virtual bool IncluirGrafica() => false;
    protected virtual bool ExportarPDF() => false;
    protected virtual void AgregarGrafica() { }
    protected virtual void ExportarPDF() { }
    protected void Finalizar() => Console.WriteLine("Reporte completado.");
}

class ReporteVentas : ReporteTemplate
{
    protected override void ObtenerDatos() => Console.WriteLine("Obteniendo datos de ventas...");
    protected override void ProcesarDatos() => Console.WriteLine("Procesando ventas...");
    protected override bool IncluirGrafica() => true;
    protected override void AgregarGrafica() => Console.WriteLine("Agregando gráfica de ventas...");
}
```

👉 Así, el flujo está claramente definido, y las variantes solo cambian los pasos que necesitan.

## Codigo completo para la ejecucion
```C#
using System;

// ============================
// Patrón Template Method
// Ejemplo: Generación de Reportes
// ============================

// Clase abstracta: define el esqueleto del proceso
abstract class ReporteTemplate
{
    // Método plantilla: define la secuencia general
    public void Generar()
    {
        ObtenerDatos();          // Paso obligatorio
        ProcesarDatos();         // Paso obligatorio

        // Pasos opcionales (hooks)
        if (IncluirGrafica()) AgregarGrafica();
        if (ExportarAPDF()) ExportarPDF();

        Finalizar();             // Paso fijo
    }

    // Métodos abstractos → deben implementarse en las subclases
    protected abstract void ObtenerDatos();
    protected abstract void ProcesarDatos();

    // Métodos virtuales (hooks) → opcionales
    protected virtual bool IncluirGrafica() => false;
    protected virtual bool ExportarAPDF() => false;

    protected virtual void AgregarGrafica() { }
    protected virtual void ExportarPDF() { }

    // Método final fijo (no se sobreescribe)
    protected void Finalizar()
    {
        Console.WriteLine("Reporte completado.\n");
    }
}

// ============================
// Subclase concreta: Reporte de Ventas
// ============================

class ReporteVentas : ReporteTemplate
{
    protected override void ObtenerDatos()
    {
        Console.WriteLine("Obteniendo datos de ventas...");
    }

    protected override void ProcesarDatos()
    {
        Console.WriteLine("Procesando información de ventas...");
    }

    protected override bool IncluirGrafica() => true;

    protected override void AgregarGrafica()
    {
        Console.WriteLine("Generando gráfica de ventas...");
    }

    protected override bool ExportarAPDF() => true;

    protected override void ExportarPDF()
    {
        Console.WriteLine("Exportando reporte de ventas a PDF...");
    }
}

// ============================
// Subclase concreta: Reporte de Inventario
// ============================

class ReporteInventario : ReporteTemplate
{
    protected override void ObtenerDatos()
    {
        Console.WriteLine("Obteniendo datos de inventario...");
    }

    protected override void ProcesarDatos()
    {
        Console.WriteLine("Analizando niveles de stock...");
    }

    // Este no incluye gráfica ni exporta a PDF
}

// ============================
// Programa principal
// ============================

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Generando Reporte de Ventas ===");
        ReporteTemplate reporteVentas = new ReporteVentas();
        reporteVentas.Generar();

        Console.WriteLine("=== Generando Reporte de Inventario ===");
        ReporteTemplate reporteInventario = new ReporteInventario();
        reporteInventario.Generar();

        Console.WriteLine("Presiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}

```

## Salida
<img width="358" height="171" alt="image" src="https://github.com/user-attachments/assets/b1467c4c-bc82-4a66-b9fa-45bf60c516e7" />




# 🧮 7. Ventajas y desventajas
## ✅ Ventajas

* Claridad estructural (qué es fijo y qué se puede cambiar)

* Reutilización de código

* Facilita la extensión del comportamiento

* Reduce condicionales repetidos

## ❌ Desventajas

* Puede crear demasiadas clases si hay muchas variaciones

* Las subclases dependen fuertemente del comportamiento de la clase base (acoplamiento)

## 📚 8. Ejemplos del mundo real

* Frameworks (como Spring, Django, ASP.NET) usan Template Method para definir flujos de inicialización o procesamiento.

* Juegos: flujo de turno (iniciar turno → acción → finalizar turno).

* Procesamiento de archivos: abrir → leer → procesar → cerrar.

🧾 9. En resumen

| Aspecto               | Descripción                                                        |
| --------------------- | ------------------------------------------------------------------ |
| Patrón                | Template Method                                                    |
| Tipo                  | Comportamental                                                     |
| Problema que resuelve | Código con pasos fijos y opcionales mezclados sin estructura       |
| Solución              | Definir un algoritmo base con métodos abstractos y ganchos (hooks) |
| Beneficio             | Claridad, extensibilidad y reducción de duplicación                |

