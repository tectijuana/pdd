# Patrón de Diseño Singleton en C#

## 1. ¿Qué es el Patrón Singleton?

El **Patrón Singleton** es un patrón de diseño que restringe la creación de múltiples instancias de una clase y asegura que solo exista una única instancia disponible durante la ejecución del programa. Este patrón es útil cuando se necesita un control centralizado de ciertos recursos, como la conexión a una base de datos.

## 2. ¿Cuándo Usar el Patrón Singleton?

El patrón Singleton es ideal en situaciones como:
- Controlar el acceso a un recurso compartido, como una **conexión a una base de datos**.
- Controlar **logs globales** o configuraciones centralizadas que deben ser consistentes.
- Garantizar que solo exista una **instancia de un servicio o componente crítico** durante la ejecución de la aplicación.

## 3. Ejemplo del Mundo Real

**Contexto:**
En un sistema de gestión de mesas para un restaurante, se necesita asegurar que solo exista **una única conexión a la base de datos** para evitar problemas de rendimiento causados por conexiones múltiples innecesarias.

**Solución:**
Usar el patrón Singleton para crear y mantener una sola instancia de la conexión a la base de datos durante toda la ejecución de la aplicación.

## 4. Implementación en C#

```csharp
using System;

public class DatabaseConnection
{
    // Variable privada y estática que almacena la única instancia de la clase
    private static DatabaseConnection _instance;

    // Variable que simula la conexión a la base de datos
    public string ConnectionString { get; private set; }

    // Constructor privado para prevenir instanciación directa
    private DatabaseConnection()
    {
        ConnectionString = "Conectado a la base de datos del restaurante";
    }

    // Método público y estático que devuelve la única instancia de la clase
    public static DatabaseConnection GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DatabaseConnection();
            Console.WriteLine("Se ha creado una nueva conexión a la base de datos.");
        }
        else
        {
            Console.WriteLine("Ya existe una conexión a la base de datos.");
        }

        return _instance;
    }

    public void ExecuteQuery(string query)
    {
        Console.WriteLine("Ejecutando consulta: " + query + " usando " + ConnectionString);
    }
}

public class Program
{
    public static void Main()
    {
        var db1 = DatabaseConnection.GetInstance();
        db1.ExecuteQuery("SELECT * FROM Mesas");

        var db2 = DatabaseConnection.GetInstance();
        db2.ExecuteQuery("SELECT * FROM Ordenes");
        
        Console.WriteLine("¿db1 y db2 son la misma instancia? " + (db1 == db2));
    }
}

```
## 5. Explicación del Código

- **Clase `DatabaseConnection`**:
    - Tiene una variable estática `_instance` que almacena la única instancia de la clase.
    - El constructor es privado, impidiendo la creación de nuevas instancias fuera de la clase.
    - El método `GetInstance()` devuelve la única instancia de `DatabaseConnection`. Si no existe, la crea.
    - El método `ExecuteQuery()` simula la ejecución de una consulta en la base de datos.

- **Programa Principal (`Main`)**:
    - Se obtienen dos referencias (`db1` y `db2`) a la conexión de base de datos.
    - Ambas referencias apuntan a la misma instancia, lo que se verifica imprimiendo el resultado de la comparación.

## 6. Beneficios del Patrón Singleton

- **Consistencia**: Garantiza que solo haya una instancia de un recurso compartido.
- **Eficiencia**: Evita la creación innecesaria de múltiples instancias, optimizando el uso de recursos.
- **Control Centralizado**: Facilita el manejo de un recurso crítico desde un solo punto.

## [Ver código en DotNetFiddle](https://dotnetfiddle.net/DrIai7)

