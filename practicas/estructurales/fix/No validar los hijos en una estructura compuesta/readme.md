# No validar los hijos en una estructura compuesta
- Ana Cristina Gutiérrez Martínez | 21211959
- Fecha: 29 de Septiembre del 2025

## Code Smell

``` cs
using System;
using System.Collections.Generic;

class Folder
{
    public string Name { get; set; }
    public List<object> Items { get; set; } = new List<object>();

    public Folder(string name)
    {
        Name = name;
    }

    public void AddItem(object item)
    {
        // ❌ No hay validación, cualquier objeto puede agregarse
        Items.Add(item);
    }

    public void ShowItems()
    {
        foreach (var item in Items)
        {
            // Esto puede fallar si el objeto no tiene propiedad Name
            Console.WriteLine(((dynamic)item).Name);
        }
    }
}

class File
{
    public string Name { get; set; }
    public File(string name)
    {
        Name = name;
    }
}

// Uso
class Program
{
    static void Main()
    {
        Folder folder = new Folder("Mi Carpeta");
        folder.AddItem("No es un archivo ni carpeta"); // 🚨 Error
        folder.ShowItems();
    }
}

```

## Código Corregido con Composite

``` cs
using System;
using System.Collections.Generic;

// Componente base
abstract class Component
{
    public string Name { get; set; }
    public Component(string name)
    {
        Name = name;
    }

    public abstract void ShowItems();
}

// Hoja
class File : Component
{
    public File(string name) : base(name) { }

    public override void ShowItems()
    {
        Console.WriteLine(Name);
    }
}

// Compuesto
class Folder : Component
{
    private List<Component> items = new List<Component>();

    public Folder(string name) : base(name) { }

    public void AddItem(Component item)
    {
        if (item == null)
        {
            throw new ArgumentNullException("Solo se pueden agregar objetos de tipo File o Folder");
        }
        items.Add(item);
    }

    public override void ShowItems()
    {
        Console.WriteLine($"Carpeta: {Name}");
        foreach (var item in items)
        {
            item.ShowItems();
        }
    }
}

// Uso
class Program
{
    static void Main()
    {
        Folder folder = new Folder("Mi Carpeta");
        File file1 = new File("Archivo1.txt");
        folder.AddItem(file1);

        Folder subfolder = new Folder("Subcarpeta");
        folder.AddItem(subfolder);

        folder.ShowItems();
    }
}

```

## Comentarios y Reflexión

En este ejercicio identifiqué un problema común al trabajar con estructuras compuestas: no validar correctamente los elementos que se agregan a una colección. Inicialmente, 
cualquier objeto podía ser añadido a una carpeta, lo que podía generar errores o inconsistencias en el programa. Para solucionarlo, implementé el patrón Composite, que permite 
manejar de manera uniforme elementos simples y compuestos, asegurando que solo se puedan agregar objetos válidos.

Reflexionando sobre el tema, me doy cuenta de lo importante que es pensar en la integridad de las estructuras desde el diseño inicial. Validar correctamente los elementos no 
solo evita errores, sino que también facilita mantener y escalar el código en el futuro. Además, aplicar patrones de diseño no es solo una cuestión técnica, sino una forma de 
organizar nuestro pensamiento para que el software sea más claro, consistente y fácil de entender.
