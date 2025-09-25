# Refactor Creacional - Vehículos monolíticos y Logger global

## 🔍 Problemas detectados
1. **Clase `Vehiculo` viola SRP (Single Responsibility Principle)**  
   - Tiene múltiples responsabilidades: define atributos, lógica de construcción y validaciones.  
   - Esto genera dificultad para mantener y extender la clase.

2. **Instancias creadas con `new` directamente en controladores**  
   - Rompe la idea de Factory Method.  
   - Si se cambia la forma de instanciar un vehículo, hay que modificar todos los lugares donde se hace `new Vehiculo(...)`.

3. **Singleton `Logger` inseguro en entornos multihilo**  
   - Implementación actual no controla la concurrencia.  
   - Puede provocar múltiples instancias simultáneas y pérdida de trazas en logs.

---

## 🛠 Patrones aplicados
- ✅ **Builder**: Separa la construcción compleja de objetos `Vehiculo` (motor, color, puertas).  
- ✅ **Factory Method**: Permite crear instancias de vehículos sin depender de `new`.  
- ✅ **Singleton (thread-safe)**: Se refactoriza el `Logger` usando `Lazy<T>` para garantizar una única instancia segura en multihilo.

---

## 💡 Justificación del cambio
- Aumenta la **cohesión interna** al dividir responsabilidades.  
- Incrementa la **testabilidad**, ya que las dependencias pueden ser simuladas fácilmente.  
- Se gana **flexibilidad ante cambios futuros**, pues ahora se pueden extender las familias de vehículos sin modificar el código cliente.  

---

## 🔄 Impacto
- Cumplimiento del **Principio de Inversión de Dependencias (DIP)**.  
- Reducción del **acoplamiento** entre cliente y productos concretos.  
- Arquitectura lista para **pruebas unitarias** y **mantenimiento ágil**.  

---

## 📌 Ejemplo de Código Refactorizado

### 🚗 Builder para Vehículos
```csharp
// Producto
public class Vehiculo
{
    public string Motor { get; set; }
    public string Color { get; set; }
    public int Puertas { get; set; }

    public override string ToString()
        => $"Motor: {Motor}, Color: {Color}, Puertas: {Puertas}";
}

// Builder
public interface IVehiculoBuilder
{
    void SetMotor(string motor);
    void SetColor(string color);
    void SetPuertas(int puertas);
    Vehiculo Build();
}

// Implementación concreta del Builder
public class VehiculoBuilder : IVehiculoBuilder
{
    private Vehiculo _vehiculo = new Vehiculo();

    public void SetMotor(string motor) => _vehiculo.Motor = motor;
    public void SetColor(string color) => _vehiculo.Color = color;
    public void SetPuertas(int puertas) => _vehiculo.Puertas = puertas;

    public Vehiculo Build()
    {
        var result = _vehiculo;
        _vehiculo = new Vehiculo(); // reinicia para siguiente construcción
        return result;
    }
}

##🏭 Factory Method
public abstract class VehiculoFactory
{
    public abstract Vehiculo CrearVehiculo();
}

public class SedanFactory : VehiculoFactory
{
    public override Vehiculo CrearVehiculo()
    {
        var builder = new VehiculoBuilder();
        builder.SetMotor("1.6L");
        builder.SetColor("Azul");
        builder.SetPuertas(4);
        return builder.Build();
    }
}

👤 Singleton (thread-safe)
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
        VehiculoFactory factory = new SedanFactory();
        Vehiculo auto = factory.CrearVehiculo();

        Console.WriteLine(auto);

        Logger.Instancia.Log("Vehículo creado correctamente.");
    }
}

✅ Conclusión

El código inicial presentaba problemas de cohesión, acoplamiento y seguridad en el Singleton.
Con la aplicación de Builder, Factory Method y Singleton thread-safe, se logró una arquitectura más clara, reutilizable y mantenible, alineada con los principios SOLID.

