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
##Corrida del programa
```
-Compuesto 2
--Compuesto 1
----Hoja 1
----Hoja 2
```
## Explicación del Patrón Composite

El Patrón Composite permite que los objetos individuales y compuestos se traten de manera uniforme. En este ejemplo:

1. **Componente**: Es la clase abstracta `Componente`, que define la operación `Mostrar` que debe implementar cualquier hoja o compuesto.
   
2. **Hoja**: Representa los objetos simples (en este caso, las hojas). La clase `Hoja` implementa la operación `Mostrar` para imprimir su nombre con un nivel de indentación específico.

3. **Compuesto**: Representa objetos complejos, que pueden contener otros objetos (tanto hojas como compuestos). La clase `Compuesto` contiene una lista de hijos (`_hijos`) y también implementa la operación `Mostrar`, llamando al método `Mostrar` de cada hijo con un nivel de indentación mayor.

### Jerarquía resultante:

En este ejemplo, la jerarquía creada es la siguiente:
```
-Compuesto 2
--Compuesto 1
----Hoja 1
----Hoja 2
```


### Detalle del flujo:

1. Se crean dos hojas: `Hoja 1` y `Hoja 2`.
2. Se crean dos compuestos: `Compuesto 1` y `Compuesto 2`.
3. `Hoja 1` y `Hoja 2` se agregan a `Compuesto 1`.
4. `Compuesto 1` se agrega a `Compuesto 2`.
5. Cuando se llama al método `Mostrar` en `Compuesto 2`, este imprime su nombre, luego llama a `Mostrar` en sus hijos (en este caso, `Compuesto 1`), que a su vez llama a `Mostrar` en sus hojas (`Hoja 1` y `Hoja 2`).

Este patrón es útil cuando quieres representar estructuras jerárquicas como árboles de archivos, menús, o cualquier sistema de componentes que puedan contener otros componentes.

[Ejemplo funcional en C# del Patrón Composite](https://dotnetfiddle.net/FfUM6c) <br>
[Patrón Composite - Refactoring.Guru](https://refactoring.guru/es/design-patterns/composite)
