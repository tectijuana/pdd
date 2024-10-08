## Singleton
Caso real: Manejo de conexión de bases de datos en aplicaciones empresariales.

Descripción: En muchas aplicaciones, se necesita que solo exista una única instancia de la conexión a la base de datos para evitar múltiples conexiones simultáneas
que podrían saturar el servidor o causar inconsistencias.

Aplicación del patrón: Al usar Singleton, se garantiza que solo haya una instancia de la clase que gestiona la conexión y que cualquier clase que necesite acceder 
a la base de datos use esa única instancia.

## Código en C#
```C#
using System;

public class Database
{
    private static Database _instance;

    // Constructor privado para evitar la creación de nuevas instancias
    private Database()
    {
        Console.WriteLine("Conectado a la base de datos.");
    }

    public static Database GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Database();
        }
        return _instance;
    }
}

public class Program
{
    public static void Main()
    {
        Database db1 = Database.GetInstance();
        Database db2 = Database.GetInstance();
        
        // Verificamos que ambas referencias son a la misma instancia
        Console.WriteLine(ReferenceEquals(db1, db2));  // True
    }
}
