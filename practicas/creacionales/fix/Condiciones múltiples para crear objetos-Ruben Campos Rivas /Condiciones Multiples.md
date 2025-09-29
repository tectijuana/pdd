# Ejemplo de patrones creacionales GoF: Condiciones múltiples para crear objetos

# Nombre y numero de Control : Ruben Campos Rivas 21211926
# Tema : Condiciones múltiples para crear objetos  

# Tema
**Condiciones múltiples para crear objetos**:  
Detección de code smells relacionados con múltiples condicionales (`if/else` o `switch`) al instanciar objetos y refactorización usando patrones creacionales del GoF.

#  Objetivo
Aplicar lo aprendido sobre **patrones creacionales (GoF)** mediante la detección de **code smells** y propuestas de refactorización en código realista.  
Esta actividad simula una revisión de código en un **entorno de desarrollo profesional usando Pull Requests**, promoviendo buenas prácticas de programación, escalabilidad y mantenibilidad del código.

##  Problemas detectados
En el código original de gestión de vehículos, se observa que la clase `Program` asume múltiples responsabilidades, incluyendo la creación, configuración y despliegue de distintos tipos de vehículos. Esta situación representa una violación del **principio de responsabilidad única (SRP)**, ya que una misma clase se encarga de tareas que deberían estar separadas según su naturaleza.  

Asimismo, se detecta la creación directa de instancias de objetos mediante `new Auto()` o `new Moto()`, lo cual genera acoplamiento con clases concretas y dificulta la extensión del sistema. Esta práctica indica la ausencia de un patrón de **Factory**, que permitiría centralizar y estandarizar la creación de vehículos según su tipo, combustible y color.  

Finalmente, la implementación del **Singleton** para la clase `VehiculoManager` no contemplaba la seguridad en entornos multihilo, lo que podría provocar condiciones de carrera si múltiples hilos intentan acceder a la instancia simultáneamente. Esto evidencia un riesgo potencial en escenarios de ejecución concurrente.

---

##  Patrón aplicado
Para abordar los problemas detectados, se implementó el patrón **Builder**, lo que permitió separar la construcción compleja de los objetos `Vehiculo`, incluyendo propiedades como modelo, combustible y color. Este enfoque facilita la creación de vehículos con distintas configuraciones sin depender de múltiples condicionales ni de la lógica de inicialización interna de las clases concretas.  

Además, se sustituyó el uso directo de `new` por el patrón **Factory Method**, lo que garantiza que la responsabilidad de instanciar objetos específicos de `Auto` o `Moto` recae en clases especializadas. De este modo, la creación de vehículos se centraliza, se reduce el acoplamiento y se promueve la reutilización del código.

---

##  Justificación del cambio
La refactorización propuesta mejora significativamente la **cohesión interna** de las clases, al asignar responsabilidades claras y específicas. La implementación de Builder y Factory Method incrementa la **testabilidad**, dado que ahora se pueden simular distintos escenarios de creación de vehículos sin modificar la lógica central.  

Asimismo, se logra una mayor **flexibilidad ante cambios**, ya que la incorporación de nuevos modelos o tipos de vehículos puede realizarse sin afectar las clases existentes. La arquitectura resultante se adapta mejor a futuros requerimientos y facilita la evolución del sistema.



##  Impacto
El impacto de estos cambios se refleja en el cumplimiento del **principio de inversión de dependencias (DIP)**, ya que las clases de alto nivel dependen de abstracciones y no de implementaciones concretas.  

Además, la arquitectura resultante se encuentra preparada para **facilitar pruebas unitarias**, dado que las dependencias y la creación de objetos están claramente definidas y encapsuladas. En conjunto, estas mejoras incrementan la mantenibilidad, reducen el riesgo de errores en producción y preparan la base del código para escenarios de desarrollo profesional con Pull Requests y revisión de código.

## Código en C#

```csharp
using System;
using System.Collections.Generic;

// ======== Producto ========
public abstract class Vehiculo
{
    public string Modelo { get; set; }
    public string Combustible { get; set; }
    public string Color { get; set; }

    public abstract void MostrarInfo();
    public abstract Vehiculo Clonar(); // Prototype
}

// ======== Productos concretos ========
public class Auto : Vehiculo
{
    public override void MostrarInfo() => Console.WriteLine($"Auto Modelo: {Modelo}, Combustible: {Combustible}, Color: {Color}");
    public override Vehiculo Clonar() => (Vehiculo)this.MemberwiseClone(); // Prototype
}

public class Moto : Vehiculo
{
    public override void MostrarInfo() => Console.WriteLine($"Moto Modelo: {Modelo}, Combustible: {Combustible}, Color: {Color}");
    public override Vehiculo Clonar() => (Vehiculo)this.MemberwiseClone(); // Prototype
}

// ======== Builder ========
public class VehiculoBuilder
{
    private Vehiculo vehiculo;

    public VehiculoBuilder CrearAuto() { vehiculo = new Auto(); return this; }
    public VehiculoBuilder CrearMoto() { vehiculo = new Moto(); return this; }
    public VehiculoBuilder ConModelo(string modelo) { vehiculo.Modelo = modelo; return this; }
    public VehiculoBuilder ConCombustible(string combustible) { vehiculo.Combustible = combustible; return this; }
    public VehiculoBuilder ConColor(string color) { vehiculo.Color = color; return this; }
    public Vehiculo Construir() { return vehiculo; }
}

// ======== Factory Method ========
public abstract class VehiculoFactory
{
    public abstract Vehiculo CrearVehiculo();
}

public class AutoToyotaGasolinaFactory : VehiculoFactory
{
    public override Vehiculo CrearVehiculo() => new VehiculoBuilder()
        .CrearAuto()
        .ConModelo("Toyota Corolla")
        .ConCombustible("Gasolina")
        .ConColor("Rojo")
        .Construir();
}

public class AutoTeslaElectricoFactory : VehiculoFactory
{
    public override Vehiculo CrearVehiculo() => new VehiculoBuilder()
        .CrearAuto()
        .ConModelo("Tesla Model 3")
        .ConCombustible("Electrico")
        .ConColor("Blanco")
        .Construir();
}

public class MotoHarleyGasolinaFactory : VehiculoFactory
{
    public override Vehiculo CrearVehiculo() => new VehiculoBuilder()
        .CrearMoto()
        .ConModelo("Harley Davidson")
        .ConCombustible("Gasolina")
        .ConColor("Negro")
        .Construir();
}

public class MotoZeroElectricoFactory : VehiculoFactory
{
    public override Vehiculo CrearVehiculo() => new VehiculoBuilder()
        .CrearMoto()
        .ConModelo("Zero SR/F")
        .ConCombustible("Electrico")
        .ConColor("Azul")
        .Construir();
}

// ======== Abstract Factory ========
public interface IFabricaVehiculo
{
    Vehiculo CrearAuto();
    Vehiculo CrearMoto();
}

public class FabricaVehiculoConcreta : IFabricaVehiculo
{
    public Vehiculo CrearAuto() => new VehiculoBuilder()
        .CrearAuto()
        .ConModelo("BMW i8")
        .ConCombustible("Híbrido")
        .ConColor("Gris")
        .Construir();

    public Vehiculo CrearMoto() => new VehiculoBuilder()
        .CrearMoto()
        .ConModelo("Ducati Monster")
        .ConCombustible("Gasolina")
        .ConColor("Rojo")
        .Construir();
}

// ======== Singleton ========
public class VehiculoManager
{
    private static VehiculoManager _instance;
    private VehiculoManager() { }
    public static VehiculoManager Instance => _instance ??= new VehiculoManager();
}

// ======== Uso ========
class Program
{
    static void Main()
    {
        // Factory Method
        VehiculoFactory f1 = new AutoToyotaGasolinaFactory();
        Vehiculo auto1 = f1.CrearVehiculo();
        auto1.MostrarInfo();

        VehiculoFactory f2 = new AutoTeslaElectricoFactory();
        Vehiculo auto2 = f2.CrearVehiculo();
        auto2.MostrarInfo();

        VehiculoFactory f3 = new MotoHarleyGasolinaFactory();
        Vehiculo moto1 = f3.CrearVehiculo();
        moto1.MostrarInfo();

        VehiculoFactory f4 = new MotoZeroElectricoFactory();
        Vehiculo moto2 = f4.CrearVehiculo();
        moto2.MostrarInfo();

        // Abstract Factory
        IFabricaVehiculo fabrica = new FabricaVehiculoConcreta();
        Vehiculo auto3 = fabrica.CrearAuto();
        auto3.MostrarInfo();
        Vehiculo moto3 = fabrica.CrearMoto();
        moto3.MostrarInfo();

        // Builder directo
        Vehiculo auto4 = new VehiculoBuilder()
            .CrearAuto()
            .ConModelo("Ford Mustang")
            .ConCombustible("Gasolina")
            .ConColor("Amarillo")
            .Construir();
        auto4.MostrarInfo();

        // Prototype
        Vehiculo clonAuto = auto4.Clonar();
        clonAuto.Color = "Negro";
        clonAuto.MostrarInfo();

        // Singleton
        VehiculoManager manager = VehiculoManager.Instance;
        Console.WriteLine("Manager singleton instanciado");
    }
}
