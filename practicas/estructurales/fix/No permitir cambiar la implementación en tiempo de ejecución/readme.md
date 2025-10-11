# Actividad: Refactorización de Patrones Estructurales (GoF)

## 🧑‍🎓 Datos del estudiante

- **Nombre:** Kevin Eduardo Garcia Cortez
- **Número de control:** 21211950 

---

## ❌ Código sin refactorizar

En este escenario, un servicio de **almacenamiento de archivos** permite cambiar de implementación en tiempo de ejecución entre `LocalStorage` y `CloudStorage`.  
Esto genera **inestabilidad** porque el cliente depende de **detalles concretos** y no de una interfaz estable.  

```csharp
// Interfaces de almacenamiento
public interface IStorage
{
    void Save(string fileName, string content);
    string Load(string fileName);
}

// Implementaciones concretas
public sealed class LocalStorage : IStorage
{
    public void Save(string fileName, string content)
        => System.Console.WriteLine($"[Local] Guardando {fileName}");
    public string Load(string fileName)
        => $"[Local] Cargando {fileName}";
}

public sealed class CloudStorage : IStorage
{
    public void Save(string fileName, string content)
        => System.Console.WriteLine($"[Cloud] Guardando {fileName}");
    public string Load(string fileName)
        => $"[Cloud] Cargando {fileName}";
}

// 🚨 MAL: el cliente cambia implementación en runtime
public sealed class FileService
{
    private IStorage _storage;

    public FileService()
    {
        _storage = new LocalStorage(); // inicial fijo
    }

    // Cambia almacenamiento dinámicamente (error conceptual)
    public void SwitchToCloud() => _storage = new CloudStorage();

    public void SaveFile(string file, string content) => _storage.Save(file, content);
}
```

## 🕵️ Identificación de Code Smells

Se detectan **3 problemas estructurales**:

1. **Cambio de implementación en tiempo de ejecución**  
   El cliente (`FileService`) permite cambiar entre `LocalStorage` y `CloudStorage`. Esto genera **inconsistencia** en el flujo de negocio.  
   → Viola la regla de **consistencia en contratos**.

2. **Acoplamiento fuerte a clases concretas**  
   El servicio instancia directamente `LocalStorage` y `CloudStorage`.  
   → Rompe el principio de inversión de dependencias (DIP).

3. **Falta de control de configuración**  
   El mecanismo de selección de estrategia de almacenamiento no está centralizado.  
   → La lógica de configuración invade la capa de negocio.

---

## 🛠️ Aplicación del patrón adecuado

El patrón **Bridge** es el más apropiado porque:  

- Define un **abstracción estable** (`FileService`) separada de la **implementación variable** (`IStorage`).  
- La implementación se decide en la **configuración** o en la **inyección de dependencias**, no en tiempo de ejecución arbitrario.  
- Impide que el cliente cambie el detalle técnico sin control.  

---

## 💡 Código refactorizado con Bridge

```csharp
// Abstracción estable
public interface IStorage
{
    void Save(string fileName, string content);
    string Load(string fileName);
}

// Implementaciones concretas (pueden crecer sin afectar al cliente)
public sealed class LocalStorage : IStorage
{
    public void Save(string fileName, string content)
        => System.Console.WriteLine($"[Local] Guardando {fileName}");
    public string Load(string fileName)
        => $"[Local] Cargando {fileName}";
}

public sealed class CloudStorage : IStorage
{
    public void Save(string fileName, string content)
        => System.Console.WriteLine($"[Cloud] Guardando {fileName}");
    public string Load(string fileName)
        => $"[Cloud] Cargando {fileName}";
}

// Abstracción de alto nivel que usa el Bridge
public sealed class FileService
{
    private readonly IStorage _storage; // no puede cambiar en runtime

    public FileService(IStorage storage) => _storage = storage;

    public void SaveFile(string file, string content) => _storage.Save(file, content);
    public string LoadFile(string file) => _storage.Load(file);
}

// 🚀 Demo de uso
public static class Demo
{
    public static void Main()
    {
        // Elección fija por configuración (no runtime)
        IStorage storage = new CloudStorage();
        var service = new FileService(storage);

        service.SaveFile("pedido.txt", "Tu pedido está en camino");
        System.Console.WriteLine(service.LoadFile("pedido.txt"));
    }
}
```

## 📋 Justificación técnica

**Problema:**  
El cliente (`FileService`) permitía cambiar la implementación de almacenamiento en **tiempo de ejecución**, lo cual rompía la consistencia y generaba acoplamiento fuerte a detalles técnicos.

**Patrón aplicado:**  
Se aplicó el patrón **Bridge**, donde `FileService` delega en una interfaz `IStorage` y la implementación se decide en la **configuración**.  
El cliente nunca cambia dinámicamente de `LocalStorage` a `CloudStorage`, garantizando estabilidad.

**Beneficios esperados:**  
- ✅ **Consistencia:** la implementación no cambia en runtime de manera arbitraria.  
- ✅ **Desacoplamiento:** el cliente desconoce las clases concretas.  
- ✅ **Escalabilidad:** se pueden agregar nuevos tipos de `IStorage` sin modificar `FileService`.  
- ✅ **Configuración centralizada:** la selección de implementación se hace una sola vez (inyección o configuración).  
