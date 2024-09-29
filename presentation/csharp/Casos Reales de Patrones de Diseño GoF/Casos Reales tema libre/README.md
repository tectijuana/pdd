
### Ejemplo de Singleton en un Sistema de Gestión de Base de Datos:

#### Caso: Sistema de Conexión a una Base de Datos

Supongamos que tienes una aplicación que necesita conectarse a una base de datos para realizar operaciones (insertar, consultar, modificar datos). En este caso, no sería eficiente crear una nueva conexión a la base de datos cada vez que se necesite acceder a ella, ya que esto consumiría demasiados recursos y afectaría el rendimiento. Aquí es donde el patrón Singleton es útil.

#### Aplicación del Patrón Singleton:

- **Problema**: Necesitas asegurarte de que solo haya una instancia de la conexión a la base de datos para toda la aplicación.
- **Solución**: Utilizar el patrón Singleton para que solo se cree una instancia de la conexión a la base de datos y que todas las demás partes de la aplicación accedan a esa misma instancia.

### Implementación del Singleton en C#:

```csharp
using System;

public class DatabaseConnection
{
    // Instancia única estática de la clase (inicialización perezosa)
    private static DatabaseConnection _instance;

    // Constructor privado para evitar la instanciación directa
    private DatabaseConnection()
    {
        Console.WriteLine("Estableciendo nueva conexión a la base de datos...");
    }

    // Método estático que devuelve la única instancia de la clase
    public static DatabaseConnection GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DatabaseConnection();
        }
        return _instance;
    }
}

// Clase de prueba para utilizar el Singleton
public class Program
{
    public static void Main(string[] args)
    {
        // Se intenta obtener la conexión a la base de datos
        DatabaseConnection conexion1 = DatabaseConnection.GetInstance();
        DatabaseConnection conexion2 = DatabaseConnection.GetInstance();

        // Se verifica que ambas conexiones son la misma instancia
        if (conexion1 == conexion2)
        {
            Console.WriteLine("Ambas conexiones son la misma instancia.");
        }
    }
}
```

### Explicación del código:

1. **Propiedad estática `DatabaseConnection _instance`**: Esta es la variable que almacena la única instancia de la clase. Está marcada como `static` para asegurar que solo haya una para toda la aplicación.

2. **Constructor privado**: Al declarar el constructor como privado, evitamos que otros objetos creen instancias directamente usando `new DatabaseConnection()`. Solo se puede obtener la instancia a través del método `GetInstance()`.

3. **Método `GetInstance()`**: Este método es el encargado de controlar el acceso a la instancia única de la clase. Si aún no existe una instancia (`_instance == null`), crea una nueva. Si ya existe, devuelve la que fue creada previamente.

4. **Comparación de instancias**: En el `Main()`, se crean dos variables que aparentemente almacenan dos instancias diferentes (`conexion1` y `conexion2`). Sin embargo, como es un Singleton, ambas apuntan a la misma instancia. 

### Salida esperada:

```plaintext
Estableciendo nueva conexión a la base de datos...
Ambas conexiones son la misma instancia.
```

Este patrón se asegura de que siempre utilices la misma conexión a la base de datos, optimizando el uso de recursos y evitando problemas como duplicidad de conexiones en una aplicación grande.

### Variaciones del Patrón Singleton en C#:
En C# hay variaciones como el uso de la **inicialización perezosa con `Lazy<T>`** o asegurando que el patrón funcione en entornos con múltiples hilos (multithreading), pero este ejemplo básico es funcional para muchos casos comunes.

### Corrida del codigo:
https://dotnetfiddle.net/L7VUt4
