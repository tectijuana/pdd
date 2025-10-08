# 🧩 Refactor de Code Smell - Lista 38

## 🔍 Problema detectado

### ❌ Code Smell:

Se crea una clase por cada algoritmo de búsqueda sin reutilización de lógica común.

### 🔎 ¿Por qué es un problema?

- Se repite código en cada clase de búsqueda.
- No hay una estructura común que organice los pasos del algoritmo.
- Violación del principio DRY (Don’t Repeat Yourself).
- Dificulta el mantenimiento y la extensibilidad.

---

## 🎯 Patrón a aplicar

### ✅ Patrón recomendado:

**Template Method**

---

## 📚 Justificación del patrón (GoF)

### 🧠 Template Method (Método Plantilla)

> **Intención (GoF):**  
> “Define el esqueleto de un algoritmo en una operación, dejando algunos pasos a las subclases. Permite que las subclases redefinan ciertos pasos de un algoritmo sin cambiar su estructura.”

### ¿Por qué es ideal aquí?

- Permite definir los pasos comunes del algoritmo de búsqueda en una clase base.
- Cada tipo de búsqueda puede personalizar solo los pasos que cambian.
- Evita duplicar código.
- Mejora la legibilidad y mantenibilidad del sistema.

---

## 🧪 Escenario de ejemplo

Supongamos que tienes distintos algoritmos de búsqueda sobre listas de enteros:

- `BusquedaLineal`
- `BusquedaBinaria`
- `BusquedaPorMultiplo`

Cada uno está implementado en su propia clase, repitiendo lógica común como iteración o manejo de excepciones.

## ❌ Ejemplo de implementación mala (sin Template Method)

```csharp
public class BusquedaLineal
{
    public int Buscar(int[] datos, int objetivo)
    {
        for (int i = 0; i < datos.Length; i++)
        {
            if (datos[i] == objetivo)
                return i;
        }
        return -1;
    }
}

public class BusquedaPorMultiplo
{
    public int Buscar(int[] datos, int multiplo)
    {
        for (int i = 0; i < datos.Length; i++)
        {
            if (datos[i] % multiplo == 0)
                return i;
        }
        return -1;
    }
}
```

## ✅ Refactor usando Template Method
```csharp
using System;

public abstract class BusquedaTemplate
{
    public int Buscar(int[] datos)
    {
        for (int i = 0; i < datos.Length; i++)
        {
            if (Condicion(datos[i]))
                return i;
        }
        return -1;
    }

    // Método abstracto que define la condición de búsqueda
    protected abstract bool Condicion(int valor);
}

public class BusquedaLineal : BusquedaTemplate
{
    private int objetivo;
    public BusquedaLineal(int objetivo)
    {
        this.objetivo = objetivo;
    }

    protected override bool Condicion(int valor)
    {
        return valor == objetivo;
    }
}

public class BusquedaPorMultiplo : BusquedaTemplate
{
    private int multiplo;
    public BusquedaPorMultiplo(int multiplo)
    {
        this.multiplo = multiplo;
    }

    protected override bool Condicion(int valor)
    {
        return valor % multiplo == 0;
    }
}

// Simulación de uso
public class Programa
{
    public static void Main()
    {
        int[] datos = { 3, 5, 7, 10, 15 };

        var busqueda1 = new BusquedaLineal(10);
        Console.WriteLine("Índice (Lineal): " + busqueda1.Buscar(datos));

        var busqueda2 = new BusquedaPorMultiplo(5);
        Console.WriteLine("Índice (Multiplo de 5): " + busqueda2.Buscar(datos));
    }
}
```

# Link en http://dotnetfiddle.net 
---
https://dotnetfiddle.net/q0oUYg
---

# 🧩 ¿Qué se refactorizó?

| Aspecto           | Antes                  | Después                    |
|-------------------|------------------------|----------------------------|
| Estructura común  | Ausente                | Centralizada en clase base |
| Lógica repetida   | Sí                     | No                         |
| Flexibilidad      | Baja                   | Alta                       |
| Patrón aplicado   | Ninguno                | ✅ Template Method          |


---

## ✅ Conclusión

Usar **Template Method** en este escenario permite:

- Evitar la repetición de código.
- Organizar mejor la lógica de los algoritmos.
- Aplicar el principio **"Open/Closed"**: abierto para extensión, cerrado para modificación.
- Aumentar la claridad del código.

