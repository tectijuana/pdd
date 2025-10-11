# Actividad de Cierre: Refactorizando Patrones Creacionales

***Nombre:*** Martinez Castellanos Santy Francisco 

***Numero de Control:*** 21211989

***Tema:*** Constructor que accede a base de datos

***Objetivo:*** Aplicar lo aprendido sobre patrones creacionales (GoF) mediante la detección de code smells y propuestas de refactorización en código realista. Esta actividad simula una revisión de código en un entorno de desarrollo profesional usando Pull Requests.


# Codigo Smell - Incorrecto

Version Incorrecta del tema Constructor que accede a base de datos.

```csharp
using System;
using System.Data.SqlClient;

// Singleton mal
public class ConexionBD
{
    private static ConexionBD instancia;
    private SqlConnection conn;

    private ConexionBD()
    {
        conn = new SqlConnection("Server=localhost;Database=TiendaCelulares;User Id=sa;Password=1234;");
        conn.Open(); // Error: acceso a BD en el constructor
    }

    public static ConexionBD Instancia
    {
        get
        {
            if (instancia == null)
                instancia = new ConexionBD();
            return instancia;
        }
    }

    public SqlConnection GetConexion() => conn;
}

// Clase dominio
public class Celular
{
    public int Id { get; }
    public string Marca { get; }
    public string Modelo { get; }
    public decimal Precio { get; }

    public Celular(int id)
    {
        var cmd = new SqlCommand("SELECT Marca, Modelo, Precio FROM Celulares WHERE Id=@id",
            ConexionBD.Instancia.GetConexion());
        cmd.Parameters.AddWithValue("@id", id);

        var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Marca = reader.GetString(0);
            Modelo = reader.GetString(1);
            Precio = reader.GetDecimal(2);
        }
        reader.Close();

        Id = id;
    }
}

//Factory Method mal
public abstract class CelularFactory
{
    public abstract Celular CrearCelular(int id);
}

public class CelularDBFactory : CelularFactory
{
    public override Celular CrearCelular(int id) => new Celular(id);
}

//Abstract Factory mal
public interface IAbstractFactory
{
    Celular CrearCelular(int id);
}

public class SamsungFactory : IAbstractFactory
{
    public Celular CrearCelular(int id) => new Celular(id);
}

// Builder mal
public class CelularBuilder
{
    private Celular celular;

    public CelularBuilder(int id)
    {
        celular = new Celular(id);
    }

    public Celular Build() => celular;
}

// Prototype mal:
public class CelularPrototype : ICloneable
{
    private Celular celular;

    public CelularPrototype(int id)
    {
        celular = new Celular(id);
    }

    public object Clone()
    {
        return new Celular(celular.Id); 
    }
}

```

# Codigo Corregido 

```csharp
using System;
using System.Data.SqlClient;

// Singleton: solo gestiona la conexión, no carga datos de dominio
public class ConexionBD
{
    private static ConexionBD instancia;
    private SqlConnection conn;

    private ConexionBD()
    {
        conn = new SqlConnection("Server=localhost;Database=TiendaCelulares;User Id=sa;Password=1234;");
        conn.Open();
    }

    public static ConexionBD Instancia
    {
        get
        {
            if (instancia == null)
                instancia = new ConexionBD();
            return instancia;
        }
    }

    public SqlConnection GetConexion() => conn;
}

// Clase dominio: constructor limpio
public class Celular
{
    public int Id { get; }
    public string Marca { get; }
    public string Modelo { get; }
    public decimal Precio { get; }

    public Celular(int id, string marca, string modelo, decimal precio)
    {
        Id = id;
        Marca = marca;
        Modelo = modelo;
        Precio = precio;
    }

    public override string ToString() => $"{Marca} {Modelo} - ${Precio}";
}

// Factory Method: ahora se encarga de la consulta
public abstract class CelularFactory
{
    public abstract Celular CrearCelular(int id);
}

public class CelularDBFactory : CelularFactory
{
    public override Celular CrearCelular(int id)
    {
        var cmd = new SqlCommand("SELECT Marca, Modelo, Precio FROM Celulares WHERE Id=@id",
            ConexionBD.Instancia.GetConexion());
        cmd.Parameters.AddWithValue("@id", id);

        var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var celular = new Celular(
                id,
                reader.GetString(0),
                reader.GetString(1),
                reader.GetDecimal(2)
            );
            reader.Close();
            return celular;
        }
        reader.Close();
        return null;
    }
}

// Abstract Factory: crea celulares por marca
public interface IAbstractFactory
{
    Celular CrearCelular(int id);
}

public class SamsungFactory : IAbstractFactory
{
    private CelularFactory factory = new CelularDBFactory();

    public Celular CrearCelular(int id) => factory.CrearCelular(id);
}

// Builder: construye celulares paso a paso en memoria
public class CelularBuilder
{
    private int id;
    private string marca;
    private string modelo;
    private decimal precio;

    public CelularBuilder SetId(int id) { this.id = id; return this; }
    public CelularBuilder SetMarca(string marca) { this.marca = marca; return this; }
    public CelularBuilder SetModelo(string modelo) { this.modelo = modelo; return this; }
    public CelularBuilder SetPrecio(decimal precio) { this.precio = precio; return this; }

    public Celular Build() => new Celular(id, marca, modelo, precio);
}

// Prototype: clona sin tocar BD
public class CelularPrototype : ICloneable
{
    private Celular celular;

    public CelularPrototype(Celular celular)
    {
        this.celular = celular;
    }

    public object Clone()
    {
        return new Celular(celular.Id, celular.Marca, celular.Modelo, celular.Precio);
    }
}

```

# Refactor Creacional - Tienda de Celulares 📱

## 🔍 Problemas detectados
- **`Celular` (constructor)** viola el principio de responsabilidad única: el constructor accede directamente a la base de datos para cargar datos del dominio en lugar de limitarse a construir el objeto.  
- **`ConexionBD`** utiliza el patrón Singleton con apertura de conexión en el constructor y una implementación lazy sin control de concurrencia, por lo que es **inseguro en entornos multihilo** y realiza inicialización pesada en la creación de la instancia.  
- **Instancias directas (`new Celular(id)`)** se crean desde `CelularDBFactory`, `SamsungFactory`, `CelularBuilder` y `CelularPrototype`, lo que demuestra que la **creación está acoplada al uso** y debería delegarse a un Factory para facilitar la inversión de dependencias y el testeo.  
- **`CelularBuilder`** mezcla responsabilidades al construir el objeto consultando la base de datos en lugar de ensamblar datos en memoria; esto provoca una construcción no determinista y difícil de probar.  
- **`CelularPrototype`** implementa clonación creando nuevas instancias que vuelven a invocar la lógica de carga (BD) en lugar de clonar el estado en memoria, produciendo llamadas redundantes a la capa de persistencia.  

## 🛠 Patrón aplicado
- **Factory Method**: se centraliza la lógica de acceso a datos en `CelularDBFactory`, que realiza la consulta SQL y construye objetos `Celular` con los datos resultantes; de esta forma se evita que el dominio realice operaciones de persistencia.  
- **Abstract Factory**: se incorpora `IAbstractFactory` (ej. `SamsungFactory`) para encapsular la creación por familia (marca) y delegar en el Factory Method apropiado, promoviendo polimorfismo y separación de familias de productos.  
- **Builder**: se implementa `CelularBuilder` para componer objetos `Celular` en memoria mediante métodos encadenados (`SetMarca`, `SetModelo`, `SetPrecio`) y finalmente `Build()`, separando la construcción del origen de datos.  
- **Prototype**: se refactoriza `CelularPrototype` para clonar el estado de un `Celular` en memoria (nueva instancia con mismas propiedades) sin volver a consultar la base de datos.  
- **Singleton**: se limita la responsabilidad de `ConexionBD` a la gestión de la conexión; se recomienda asegurar su seguridad en entornos concurrentes (por ejemplo, usando doble verificación o `Lazy<T>` en C#) para evitar condiciones de carrera en la inicialización.

## 💡 Justificación del cambio
Mejoras observadas:
- **Cohesión interna:** cada clase ahora tiene una responsabilidad única — el modelo (`Celular`) representa datos, las factories manejan persistencia y el singleton gestiona la conexión.  
- **Testabilidad:** la separación de la creación y la persistencia permite sustituir factories por mocks/stubs en pruebas unitarias; los objetos de dominio se pueden instanciar directamente sin depender de una base de datos.  
- **Flexibilidad ante cambios:** cambios en la fuente de datos (por ejemplo, migrar de SQL a un servicio HTTP o archivo) quedan encapsulados en las factories sin impactar el modelo ni los consumidores del objeto.  
- **Reducción de efectos secundarios:** al eliminar consultas en constructores se evitan llamadas inesperadas a la BD durante la creación de objetos, reduciendo latencia inesperada y efectos colaterales difíciles de depurar.

## 🔄 Impacto
- **Principio de inversión de dependencias:** la capa de dominio deja de depender de detalles de infraestructura (BD); los detalles quedan encapsulados en factories, facilitando la inversión y la sustitución de dependencias.  
- **Facilidad para pruebas unitarias y CI:** los objetos `Celular` pueden crearse en pruebas sin necesidad de iniciar o mockear la BD; las factories pueden mockearse para controlar escenarios y excepciones.  
- **Preparación para concurrencia y escalabilidad:** al minimizar la inicialización pesada en constructores y concentrarla (de forma controlada) en un punto, el sistema queda más fácil de adaptar a mecanismos de inicialización segura para entornos multihilo.  
- **Riesgos residuales y recomendaciones:** la implementación actual de `ConexionBD` en la versión corregida **se ha limitado** a gestionar la conexión pero **queda la recomendación explícita** de hacer su inicialización thread-safe (ej.: `double-check locking` o usar `Lazy<ConexionBD>` en C#) para entornos con múltiples hilos.  

---

### Resumen
- Se elimina el acceso a la base de datos desde constructores, se centraliza la persistencia en factories, se introduce un Builder para construcción en memoria y un Prototype que clona en memoria. Se limita la responsabilidad del Singleton a la gestión de la conexión y se recomienda su aseguramiento para concurrencia.  

