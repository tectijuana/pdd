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

```csharp
using System;
using System.Collections;

public class Program
{
    public static void Main()
    {
        // Crear una colección
        var collection = new CustomCollection();
        
        // Asignar elementos a la colección usando el indexador
        collection[0] = "Elemento 1";
        collection[1] = "Elemento 2";
        collection[2] = "Elemento 3";
        
        // Obtener el iterador de la colección
        Iterator iterator = collection.CreateIterator();
        
        // Imprimir los elementos de la colección usando el iterador
        Console.WriteLine("Iterando sobre la colección:");
        while (!iterator.IsDone) // Mientras no hayamos terminado de iterar
        {
            // Mostrar el elemento actual
            Console.WriteLine(iterator.CurrentItem);
            // Pasar al siguiente elemento
            iterator.Next();
        }
    }
}

// Interfaz Iterator
// Define la estructura básica que todo iterador debe seguir
public interface Iterator
{
    // Propiedad que indica si la iteración ha llegado al final
    bool IsDone { get; }
    
    // Propiedad que devuelve el elemento actual de la iteración
    object CurrentItem { get; }
    
    // Método para movernos al siguiente elemento
    void Next();
}

// Implementación concreta del Iterator
// Esta clase realiza la lógica de iteración sobre la colección
public class CustomIterator : Iterator
{
    // Almacena la colección sobre la que iterar
    private readonly CustomCollection _collection;
    
    // Índice actual de la iteración
    private int _currentIndex = 0;
    
    // Constructor que toma la colección como parámetro
    public CustomIterator(CustomCollection collection)
    {
        _collection = collection;
    }
    
    // Verifica si hemos llegado al final de la colección
    public bool IsDone => _currentIndex >= _collection.Count;
    
    // Devuelve el elemento actual en la posición del índice
    public object CurrentItem => _collection[_currentIndex];
    
    // Avanza al siguiente elemento incrementando el índice
    public void Next()
    {
        _currentIndex++;
    }
}

// Interfaz Aggregate (Agregado)
// Define el método para crear un iterador que recorrerá la colección
public interface IAggregate
{
    // Método que debe implementar cualquier colección para devolver un iterador
    Iterator CreateIterator();
}

// Implementación concreta del Aggregate (Colección)
// Define la estructura interna de la colección y cómo crear su iterador
public class CustomCollection : IAggregate
{
    // Arreglo dinámico para almacenar los elementos de la colección
    private readonly ArrayList _items = new ArrayList(10);
    
    // Implementa el método CreateIterator para devolver un iterador concreto
    public Iterator CreateIterator()
    {
        return new CustomIterator(this);
    }
    
    // Propiedad para obtener el número de elementos en la colección
    public int Count => _items.Count;
    
    // Indexador que permite obtener o establecer elementos por su índice
    public object this[int index]
    {
        get => _items.Count > index ? _items[index] : null; // Devuelve el elemento si existe
        set
        {
            if (_items.Count > index)
            {
                _items[index] = value; // Si el índice ya existe, sobrescribe el valor
            }
            else
            {
                _items.Add(value); // Si el índice no existe, agrega el valor a la colección
            }
        }
    }
}
