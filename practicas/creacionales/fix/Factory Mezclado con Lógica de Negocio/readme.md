# Refactor Creacional
## Factory mezclado con lógica de negocio
- Ana Cristina Gutiérrez Martínez | 21211959
- 24 de Septiembre del 2025

**Código "Malo"**

``` cs 

using System;

namespace FactoryConLogicaIncorrecta
{
    public interface IVehiculo
    {
        string GetDescripcion();
    }

    public class Auto : IVehiculo
    {
        public string GetDescripcion() => "Soy un Auto";
    }

    public class Moto : IVehiculo
    {
        public string GetDescripcion() => "Soy una Moto";
    }

    public class VehiculoFactory
    {
        public IVehiculo CrearVehiculo(string tipo, int pasajeros)
        {
            if (tipo == "Auto")
            {
                if (pasajeros > 5)
                    throw new Exception("Demasiados pasajeros para un Auto");
                return new Auto();
            }
            else if (tipo == "Moto")
            {
                if (pasajeros > 2)
                    throw new Exception("Demasiados pasajeros para una Moto");
                return new Moto();
            }
            else
            {
                return null; // ❌ Mal diseño
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var factory = new VehiculoFactory();

            try
            {
                IVehiculo auto = factory.CrearVehiculo("Auto", 4);
                Console.WriteLine(auto.GetDescripcion());

                IVehiculo moto = factory.CrearVehiculo("Moto", 2);
                Console.WriteLine(moto.GetDescripcion());

                IVehiculo autoGrande = factory.CrearVehiculo("Auto", 6);
                Console.WriteLine(autoGrande.GetDescripcion());
            }
            catch (Exception e)
            {
                Console.WriteLine("⚠️ Error: " + e.Message);
            }
        }
    }
}
```

**Resultados del Código "Malo"** 

Soy un Auto

Soy una Moto

Error: Demasiados pasajeros para un Auto

**Código Refactorizado: Código Correcto**

``` cs
using System;

namespace FactoryRefactorizada
{
    public interface IVehiculo
    {
        string GetDescripcion();
    }

    public class Auto : IVehiculo
    {
        public string GetDescripcion() => "Soy un Auto";
    }

    public class Moto : IVehiculo
    {
        public string GetDescripcion() => "Soy una Moto";
    }

    // Factory: solo creación
    public class VehiculoFactory
    {
        public IVehiculo CrearVehiculo(string tipo)
        {
            switch (tipo)
            {
                case "Auto": return new Auto();
                case "Moto": return new Moto();
                default: throw new ArgumentException("Tipo de vehículo no válido");
            }
        }
    }

    // Service: lógica de negocio separada
    public class VehiculoService
    {
        private readonly VehiculoFactory _factory = new VehiculoFactory();

        public IVehiculo RegistrarVehiculo(string tipo, int pasajeros)
        {
            IVehiculo vehiculo = _factory.CrearVehiculo(tipo);

            if (vehiculo is Auto && pasajeros > 5)
                throw new Exception("Demasiados pasajeros para un Auto");

            if (vehiculo is Moto && pasajeros > 2)
                throw new Exception("Demasiados pasajeros para una Moto");

            return vehiculo;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var service = new VehiculoService();

            try
            {
                IVehiculo auto = service.RegistrarVehiculo("Auto", 4);
                Console.WriteLine(auto.GetDescripcion());

                IVehiculo moto = service.RegistrarVehiculo("Moto", 2);
                Console.WriteLine(moto.GetDescripcion());

                IVehiculo autoGrande = service.RegistrarVehiculo("Auto", 6);
                Console.WriteLine(autoGrande.GetDescripcion());
            }
            catch (Exception e)
            {
                Console.WriteLine("⚠️ Error: " + e.Message);
            }
        }
    }
}
```

**Salida del código refactorizado**

Soy un Auto

Soy una Moto

Error: Demasiados pasajeros para un Auto

**¿Qué problemas tenía el código?**

La clase VehiculoFactory estaba haciendo más de lo que debía: además de crear objetos, estaba metiendo lógica de negocio, como validar cuántos pasajeros podía llevar cada vehículo. 
Esto rompía el principio de responsabilidad única (SRP). También tenía muchos if-else encadenados y devolvía null si le pasabas un tipo inválido, lo cual podía causar errores 
inesperados en tiempo de ejecución. Otro problema era que cualquier cambio en la lógica de negocio obligaba a modificar directamente el Factory, generando un acoplamiento fuerte 
entre creación y validación.

**Implementación de Factory Method**

Se implementó dejando que VehiculoFactory se encargue únicamente de crear objetos (Auto, Moto). Se practicó la separación de responsabilidades creando 
VehiculoService, que se hizo cargo de la lógica de negocio, como validar el número de pasajeros según el tipo de vehículo. También se eliminó el retorno de null y se sustituyó 
por excepciones claras (ArgumentException, Exception).

**¿Por qué se cambió?**
- Cohesión: cada clase ahora cumple una sola función, sin mezclar responsabilidades.
- Flexibilidad: se pueden agregar nuevos tipos de vehículos o nuevas reglas sin afectar el resto del código.
- Testabilidad: se puede probar VehiculoFactory y VehiculoService por separado.
- Mantenibilidad: el código quedó más claro y escalable, siguiendo buenas prácticas de Clean Code y SOLID.

**Impacto del refactor**
- El Factory solo crea y el Service solo valida, logrando una separación clara de responsabilidades.
- Se redujo el riesgo de errores al eliminar retornos null.
- El código quedó más listo para pruebas unitarias y futuras ampliaciones sin afectar lo que ya funciona.
