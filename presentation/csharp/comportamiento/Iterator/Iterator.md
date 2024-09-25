# Patrón de Diseño Iterator

## ¿Qué es el patrón Iterator?

El **patrón Iterator** es uno de los patrones de diseño de comportamiento. Su propósito es proporcionar una forma de acceder secuencialmente a los elementos de una colección sin exponer su representación subyacente (por ejemplo, una lista, un árbol o un arreglo).

El patrón **Iterator** te permite recorrer elementos de una colección sin importar cómo están almacenados. Esto se logra separando la lógica de iteración de la colección misma, lo que también facilita cambiar la implementación de la colección sin modificar el código de iteración.

## Ventajas del Patrón Iterator

- **Abstracción de la colección**: El código que itera no necesita conocer los detalles internos de la colección.
- **Responsabilidad clara**: La iteración es responsabilidad de un objeto diferente al de la colección.
- **Flexibilidad**: Puedes tener múltiples iteradores para la misma colección.
- **Simplifica las colecciones**: Las colecciones no necesitan gestionar la lógica de iteración.

## Componentes del Patrón Iterator

1. **Iterator (Iterador)**: Define la interfaz necesaria para iterar sobre una colección.
2. **ConcreteIterator (Iterador Concreto)**: Implementa la interfaz del iterador y se encarga de la lógica de iteración.
3. **Aggregate (Agregado)**: Declara la interfaz para crear un iterador.
4. **ConcreteAggregate (Agregado Concreto)**: Implementa la interfaz para crear un iterador concreto.

## Ejemplo en C#
Este ejemplo muestra de forma simple cómo se puede aplicar el patrón Iterator para recorrer una colección de nombres, sin tener que acceder directamente a los detalles internos de la colección (como la lista de nombres). El iterador se encarga de manejar esa lógica.

```csharp
using System;
using System.Collections.Generic;

// Interfaz para el iterador
public interface IIterator
{
    // Devuelve el siguiente elemento en la colección
    string Next();

    // Verifica si hay más elementos en la colección
    bool HasNext();
}

// Implementación del iterador
public class NameIterator : IIterator
{
    private readonly List<string> _names; // Lista de nombres
    private int _index = 0;               // Índice para la posición actual

    // Constructor que recibe la lista de nombres
    public NameIterator(List<string> names)
    {
        _names = names;
    }

    // Verifica si aún quedan elementos por iterar
    public bool HasNext()
    {
        return _index < _names.Count;
    }

    // Devuelve el siguiente nombre y avanza el índice
    public string Next()
    {
        if (HasNext())
        {
            return _names[_index++];
        }
        return null; // Si no hay más elementos
    }
}

// Clase que contiene la colección de nombres y crea el iterador
public class NameCollection
{
    private readonly List<string> _names = new List<string>();

    // Método para agregar nombres a la colección
    public void AddName(string name)
    {
        _names.Add(name);
    }

    // Método para obtener un iterador de la colección
    public IIterator GetIterator()
    {
        return new NameIterator(_names);
    }
}

// Clase principal con el método Main para ejecutar el código
public class Program
{
    public static void Main()
    {
        // Crear una colección de nombres
        var nameCollection = new NameCollection();
        nameCollection.AddName("Ana");
        nameCollection.AddName("Luis");
        nameCollection.AddName("Pedro");
        nameCollection.AddName("María");

        // Obtener un iterador para la colección
        IIterator iterator = nameCollection.GetIterator();

        // Usar el iterador para recorrer los nombres
        Console.WriteLine("Nombres en la colección:");
        while (iterator.HasNext()) // Mientras haya más elementos
        {
            Console.WriteLine(iterator.Next()); // Imprimir el siguiente nombre
        }
    }
}
```

[Código Ejecutable en .NET Fiddle](https://dotnetfiddle.net/mslxXW)
