# Patrón Estructural Composite

## ¿Qué es el Patrón Composite?

El **Patrón Composite** es un patrón de diseño estructural que permite tratar a objetos individuales y a composiciones de objetos de manera uniforme. Se utiliza cuando tenemos una estructura de árbol de objetos, donde los objetos individuales y sus contenedores pueden ser tratados de la misma manera.

Este patrón es útil en aplicaciones que requieren representar jerarquías de objetos, como archivos y carpetas en un sistema de archivos, menús en una aplicación o gráficos en una interfaz de usuario.

## Intención

El objetivo del **Patrón Composite** es permitir que los clientes traten de manera uniforme tanto a los objetos individuales como a grupos de objetos. Esto significa que puedes ejecutar las mismas operaciones tanto en un objeto individual como en una composición de objetos sin preocuparte por las diferencias entre ellos.

## Estructura

El patrón incluye los siguientes componentes:

1. **Componente**: Una interfaz común para todos los objetos en la composición. Define las operaciones que serán implementadas tanto por los objetos hoja como por los compuestos. Este componente asegura que las operaciones como `agregar`, `eliminar` y `obtener` hijos puedan ser uniformemente aplicadas.
   
2. **Hoja**: Representa los objetos finales de la estructura, aquellos que no contienen otros objetos. Las hojas no pueden tener hijos y, a menudo, implementan las operaciones del componente con funcionalidad concreta.
   
3. **Compuesto**: Un objeto que puede contener otros objetos (hojas o compuestos). Implementa las operaciones para gestionar los objetos hijos, como `agregar` o `eliminar`. Los compuestos permiten crear estructuras más complejas al agrupar hojas u otros compuestos.
   
4. **Cliente**: Manipula los objetos a través de la interfaz del Componente, sin saber si está interactuando con una hoja o con un compuesto. Esto da flexibilidad al sistema y simplifica el código del cliente.

## Diagrama UML

```plaintext
         +----------------+
         |  Componente     |
         +----------------+
         | + Operación()   |
         +----------------+
                ▲
                |
       +------------------+
       |                  |
+--------------+   +----------------+
|    Hoja      |   |    Compuesto    |
+--------------+   +----------------+
| + Operación()|   | + Agregar()     |
|              |   | + Eliminar()    |
+--------------+   +----------------+
                           ▲
                           |
                     +----------------+
                     |    Cliente     |
                     +----------------+
                     |  Usa Componente |
                     +----------------+
```
## Ejemplo del Patrón Composite en C#

A continuación se muestra un ejemplo sencillo del Patrón Composite implementado en C#.

### Código

```csharp
using System;
using System.Collections.Generic;

// Componente
public abstract class Componente
{
    public abstract void Mostrar(int nivel);
}

// Hoja
public class Hoja : Componente
{
    private string _nombre;

    public Hoja(string nombre)
    {
        _nombre = nombre;
    }

    public override void Mostrar(int nivel)
    {
        Console.WriteLine(new String('-', nivel) + _nombre);
    }
}

// Compuesto
public class Compuesto : Componente
{
    private List<Componente> _hijos = new List<Componente>();
    private string _nombre;

    public Compuesto(string nombre)
    {
        _nombre = nombre;
    }

    public void Agregar(Componente componente)
    {
        _hijos.Add(componente);
    }

    public void Eliminar(Componente componente)
    {
        _hijos.Remove(componente);
    }

    public override void Mostrar(int nivel)
    {
        Console.WriteLine(new String('-', nivel) + _nombre);

        foreach (var hijo in _hijos)
        {
            hijo.Mostrar(nivel + 2);
        }
    }
}

// Cliente
public class Program
{
    public static void Main()
    {
        // Crear hojas
        Componente hoja1 = new Hoja("Hoja 1");
        Componente hoja2 = new Hoja("Hoja 2");

        // Crear compuestos
        Compuesto compuesto1 = new Compuesto("Compuesto 1");
        Compuesto compuesto2 = new Compuesto("Compuesto 2");

        // Agregar hojas a compuestos
        compuesto1.Agregar(hoja1);
        compuesto1.Agregar(hoja2);
        compuesto2.Agregar(compuesto1);

        // Mostrar la estructura
        compuesto2.Mostrar(1);
    }
}
```

[Ejemplo funcional en C# del Patrón Composite](https://dotnetfiddle.net/FfUM6c)
