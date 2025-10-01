RICARDO RODRIGUEZ CARRERAS 21212360
# Refactor Creacional - Crear objetos sin validar estado

## 🔍 Problemas detectados
1. **Clase `Celular` permite instancias inválidas**
   - El constructor acepta parámetros nulos o vacíos (`marca`, `modelo`, `sistema operativo`).
   - Esto provoca objetos en estado inconsistente.

2. **Uso de `new` directamente en el cliente**
   - Se crean objetos `Celular` sin ningún mecanismo de validación.
   - Esto rompe el encapsulamiento y genera duplicación de lógica de validación en distintos lugares.

3. **Falta de separación entre creación y uso**
   - El cliente decide qué valores asignar y cómo construir el objeto.
   - Esto viola **SRP**, ya que la validación de estado está mezclada con la lógica de negocio.

---

## 🛠 Patrones aplicados
- ✅ **Builder con validación interna**: Garantiza que un `Celular` solo se cree si tiene un estado válido.  
- ✅ **Factory Method**: Centraliza la creación de celulares evitando el uso de `new` disperso.  
- ✅ **Singleton (Logger seguro en multihilo)**: Registra errores o intentos de creación inválidos.  

---

## 💡 Justificación del cambio
- Se asegura que **ningún objeto inválido pueda existir** en el sistema.  
- Se mejora la **cohesión** al centralizar la lógica de construcción.  
- Se aumenta la **robustez y mantenibilidad** evitando validaciones duplicadas.  


---

## 🔄 Impacto
- Los objetos `Celular` ahora siempre son válidos por construcción.  
- El código cliente queda más limpio y desacoplado del proceso de validación.  
- Se prepara la arquitectura para **pruebas unitarias** y extensión futura (ejemplo: distintos tipos de celulares).  

---

## 📌 Ejemplo de Código Refactorizado

### 📱 Clase Producto

public class Celular
{
    public string Marca { get; private set; }
    public string Modelo { get; private set; }
    public string SistemaOperativo { get; private set; }

    internal Celular(string marca, string modelo, string sistemaOperativo)
    {
        Marca = marca;
        Modelo = modelo;
        SistemaOperativo = sistemaOperativo;
    }

    public override string ToString()
        => $"Celular {Marca} {Modelo} con {SistemaOperativo}";
}

🏗️ Builder con Validación
public class CelularBuilder
{
    private string _marca;
    private string _modelo;
    private string _sistemaOperativo;

    public CelularBuilder ConMarca(string marca)
    {
        _marca = marca;
        return this;
    }

    public CelularBuilder ConModelo(string modelo)
    {
        _modelo = modelo;
        return this;
    }

    public CelularBuilder ConSistemaOperativo(string so)
    {
        _sistemaOperativo = so;
        return this;
    }

    public Celular Build()
    {
        if (string.IsNullOrWhiteSpace(_marca) ||
            string.IsNullOrWhiteSpace(_modelo) ||
            string.IsNullOrWhiteSpace(_sistemaOperativo))
        {
            Logger.Instancia.Log("Intento de crear celular inválido.");
            throw new InvalidOperationException("El celular debe tener marca, modelo y sistema operativo válidos.");
        }

        return new Celular(_marca, _modelo, _sistemaOperativo);
    }
}

🏭 Factory Method
public abstract class CelularFactory
{
    public abstract Celular CrearCelular(string marca, string modelo, string so);
}

public class AndroidFactory : CelularFactory
{
    public override Celular CrearCelular(string marca, string modelo, string so)
    {
        return new CelularBuilder()
            .ConMarca(marca)
            .ConModelo(modelo)
            .ConSistemaOperativo(so)
            .Build();
    }
}

👤 Singleton (Logger thread-safe)
public sealed class Logger
{
    private static readonly Lazy<Logger> _instancia =
        new Lazy<Logger>(() => new Logger());

    public static Logger Instancia => _instancia.Value;

    private Logger() { }

    public void Log(string mensaje)
        => Console.WriteLine($"[LOG] {DateTime.Now}: {mensaje}");
}

▶️ Uso en Programa
class Program
{
    static void Main()
    {
        CelularFactory factory = new AndroidFactory();

        try
        {
            // ✅ Objeto válido
            Celular celular = factory.CrearCelular("Samsung", "Galaxy S24", "Android 14");
            Console.WriteLine(celular);

            // ❌ Objeto inválido (lanza excepción y se registra en Logger)
            Celular invalido = factory.CrearCelular("", "", "");
        }
        catch (Exception ex)
        {
            Logger.Instancia.Log($"Error: {ex.Message}");
        }
    }
}

✅ Conclusión

El código inicial permitía crear objetos inconsistentes, lo cual representaba un grave riesgo de diseño.

Con la aplicación de Builder con validación, Factory Method y un Logger Singleton thread-safe:

Se garantiza que cada Celular creado siempre esté en un estado válido.

Se mejora la cohesión y la separación de responsabilidades.

Se refuerza la mantenibilidad y testabilidad del sistema.
