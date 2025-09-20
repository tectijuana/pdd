# 📑 Estudio de Casos sobre Refactorización de Proyectos Legacy

**Alumno:** Rodrigo Sotelo Rubio  
**Matrícula:** 21212053

---

## 1. Introducción 

En este documento presento un análisis sobre la refactorización de proyectos legacy, explorando casos prácticos donde se aplicaron patrones de diseño para mejorar la calidad, mantenibilidad y escalabilidad del software.

Un sistema legacy, aunque funcional, suele tener problemas de deuda técnica ⚠️, acoplamiento excesivo o falta de pruebas automatizadas. La refactorización, apoyada en patrones de diseño, es una estrategia clave para extender la vida útil de estos sistemas y alinearlos con las necesidades actuales.

---

## 2. Estudio de Casos 🛠️

### Caso 1: Sistema bancario con código spaghetti 🍝

- **Problema:** el sistema tenía múltiples métodos duplicados y clases monolíticas difíciles de mantener.  
- **Refactorización aplicada:** introducción del patrón Facade para unificar accesos a subsistemas.  
- **Resultado:** el código se volvió más claro 📝, con menor acoplamiento y mayor facilidad para agregar nuevas funcionalidades.

---

### Caso 2: Aplicación web sin pruebas unitarias 🐞

- **Problema:** cualquier cambio en el código generaba errores en producción porque no había pruebas.  
- **Refactorización aplicada:** se aplicó Inyección de Dependencias y se implementó el patrón Strategy para manejar reglas de negocio sin condicionales extensos.  
- **Resultado:** se logró escribir pruebas unitarias ✅ y reducir drásticamente los errores en producción.

**Ejemplo en C# 💻**

**Código legacy con condicionales:**

```csharp
public class CalculadoraPrecio
{
    public double Calcular(string tipoCliente, double monto)
    {
        if (tipoCliente == "VIP")
        {
            return monto * 0.8; // 20% de descuento
        }
        else if (tipoCliente == "Regular")
        {
            return monto * 0.9; // 10% de descuento
        }
        else
        {
            return monto; // sin descuento
        }
    }
}
```

**Código refactorizado:**


```csharp
public interface IDescuentoStrategy
{
    double AplicarDescuento(double monto);
}

public class DescuentoVIP : IDescuentoStrategy
{
    public double AplicarDescuento(double monto) => monto * 0.8;
}

public class DescuentoRegular : IDescuentoStrategy
{
    public double AplicarDescuento(double monto) => monto * 0.9;
}

public class SinDescuento : IDescuentoStrategy
{
    public double AplicarDescuento(double monto) => monto;
}

public class CalculadoraPrecio
{
    private readonly IDescuentoStrategy _estrategia;

    public CalculadoraPrecio(IDescuentoStrategy estrategia)
    {
        _estrategia = estrategia;
    }

    public double Calcular(double monto) => _estrategia.AplicarDescuento(monto);
}

// Uso
class Program
{
    static void Main()
    {
        var calculadora = new CalculadoraPrecio(new DescuentoVIP());
        Console.WriteLine(calculadora.Calcular(1000)); // Imprime 800
    }
}

```

### Caso 3: Migración de un monolito a microservicios ⚡

- **Problema:** el monolito en Java no podía escalar adecuadamente y los tiempos de respuesta eran altos.
- **Refactorización aplicada:** se utilizó el patrón Strangler Fig, migrando módulos de forma gradual a microservicios.
- **Resultado:** se logró una arquitectura moderna 🏗️, más flexible y preparada para la escalabilidad.


## 3. Tabla Comparativa 📊
La siguiente tabla compara los casos estudiados, mostrando los problemas encontrados en los sistemas legacy, las refactorizaciones aplicadas, los patrones de diseño utilizados y los resultados obtenidos. Permite visualizar de manera resumida cómo cada estrategia contribuye a mejorar la calidad, mantenibilidad y escalabilidad del software.

| Caso | Problema              | Refactorización aplicada          | Patrón usado  | Resultado                                |
| ---- | --------------------- | --------------------------------- | ------------- | ---------------------------------------- |
| 1    | Código spaghetti      | Simplificación de accesos         | Facade        | Menor acoplamiento y mayor claridad      |
| 2    | Sin pruebas unitarias | Inyección de dependencias + tests | Strategy      | Reducción de errores y más confiabilidad |
| 3    | Monolito rígido       | Migración progresiva              | Strangler Fig | Arquitectura moderna y escalable         |

## 4. Análisis Crítico 🧐
La refactorización es más que un proceso técnico: es una estrategia de sostenibilidad del software.

### Ventajas: ✅

- **Reduce deuda técnica.**
- **Facilita el mantenimiento y escalabilidad.**
- **Permite aplicar pruebas automatizadas.**

### Limitaciones: ⚠️

- **Requiere inversión de tiempo y personal capacitado.**
- **Puede ser difícil en sistemas críticos con poca documentación.**
- **En los casos revisados, el uso de patrones como Facade, Strategy y Strangler Fig permitió transformar sistemas rígidos en arquitecturas más limpias y modernas.**

## 5. Conclusiones 🏁

Los sistemas legacy representan un reto, pero con una refactorización adecuada se pueden mantener competitivos.
Los patrones de diseño son aliados esenciales para guiar la modernización de estos proyectos.
Refactorizar no solo mejora la legibilidad, sino que establece una estrategia de evolución sostenible.














