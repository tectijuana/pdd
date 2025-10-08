# Refactorización de Patrones Estructurales (GoF)

## Contexto
- **Actividad:** Refactorización de Patrones Estructurales (GoF)  
- **Lenguaje:** C# (.NET 8)  
- **Modalidad:** Individual  
- **Formato de entrega:** Pull Request en Git  
- **Duración estimada:** 50 minutos  
- **Alumno:** Martinez Castellanos Santy Francisco

---

## Code Smell detectado
**No unificar el tratamiento de hojas y compuestos**.  
En el código original, el cliente debía distinguir entre objetos tipo `File` y `Folder`, repitiendo lógica y rompiendo el principio de polimorfismo.  

Esto es un problema porque:
- Se genera **código condicional repetido** en el cliente.  
- Se rompe la idea de **abstracción común** entre hojas y compuestos.  
- La solución no es extensible si mañana se agregan nuevos tipos (por ejemplo, accesos directos o archivos comprimidos).  

---

## 🔴 Código original con Code Smell
```csharp
using System;
using System.Collections.Generic;

class File
{
    public string Name { get; }

    public File(string name)
    {
        Name = name;
    }

    public void Open()
    {
        Console.WriteLine($"Abriendo archivo: {Name}");
    }
}

class Folder
{
    public string Name { get; }
    private List<File> files = new List<File>();

    public Folder(string name)
    {
        Name = name;
    }

    public void Add(File file)
    {
        files.Add(file);
    }

    public void OpenAll()
    {
        Console.WriteLine($"Abriendo carpeta: {Name}");
        foreach (var file in files)
        {
            file.Open();
        }
    }
}

class Program
{
    static void Main()
    {
        var file1 = new File("doc1.txt");
        var file2 = new File("doc2.txt");

        var folder = new Folder("Mis Documentos");
        folder.Add(file1);
        folder.Add(file2);

        // El cliente debe distinguir entre abrir archivo y abrir carpeta
        file1.Open();
        folder.OpenAll();
    }
}
```

## 🟢 Refactorización con Patrón Composite
```csharp
// Interfaz común
interface IFileSystemComponent
{
    void Open();
}

// Clase hoja
class File : IFileSystemComponent
{
    public string Name { get; }

    public File(string name)
    {
        Name = name;
    }

    public void Open()
    {
        Console.WriteLine($"Abriendo archivo: {Name}");
    }
}

// Clase compuesta
class Folder : IFileSystemComponent
{
    public string Name { get; }
    private List<IFileSystemComponent> children = new List<IFileSystemComponent>();

    public Folder(string name)
    {
        Name = name;
    }

    public void Add(IFileSystemComponent component)
    {
        children.Add(component);
    }

    public void Open()
    {
        Console.WriteLine($"Abriendo carpeta: {Name}");
        foreach (var child in children)
        {
            child.Open();
        }
    }
}

class Program
{
    static void Main()
    {
        var file1 = new File("doc1.txt");
        var file2 = new File("doc2.txt");

        var folder = new Folder("Mis Documentos");
        folder.Add(file1);
        folder.Add(file2);

        //El cliente trata igual a archivos y carpetas
        IFileSystemComponent root = folder;
        root.Open();
    }
}

```
## 🧾 Justificación de la Refactorización

En el código original se presentaba un **Code Smell** debido a que no existía un tratamiento unificado para los elementos del sistema de archivos.  
- Los **archivos (`File`)** y las **carpetas (`Folder`)** tenían interfaces diferentes.  
- El cliente debía distinguir explícitamente entre llamar a `Open()` para archivos o `OpenAll()` para carpetas.  
- Esto generaba **duplicidad de lógica**, alta dependencia del cliente y una baja extensibilidad al sistema.

Para resolver este problema se aplicó el **Patrón Composite (GoF)**, cuyo objetivo es tratar de manera uniforme a los objetos individuales (hojas) y a las composiciones de objetos (compuestos).  

Con la refactorización:  
- Se definió una **interfaz común (`IFileSystemComponent`)** que unifica el comportamiento de archivos y carpetas.  
- Tanto `File` como `Folder` implementan la misma interfaz, eliminando la necesidad de condicionales en el cliente.  
- El cliente ahora trabaja de manera uniforme, llamando simplemente a `Open()` sin importar si se trata de un archivo o de una carpeta.  
- Se obtuvo un diseño **más flexible, extensible y mantenible**, alineado con los principios SOLID (particularmente el Principio de Sustitución de Liskov y el Principio de Abierto/Cerrado).

El uso del **Patrón Composite** permitió unificar el tratamiento de hojas y compuestos, reduciendo la complejidad en el cliente y mejorando la cohesión del sistema.
