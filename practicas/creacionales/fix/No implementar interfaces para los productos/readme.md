#
Autor: Rojas Garcia Kevin Argenis
Fecha: 2025-09-24
Descripción: Practica Factory Method Celulares bad code
 ============================================

## Codigo con malas practicas a refactorizar:

```
// Código MAL HECHO sobre celulares
// Múltiples problemas de diseño intencionales para practicar refactorización con GoF.

using System;
using System.Collections.Generic;

namespace CelularesApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new CelularFactory();
            var celulares = new List<object>();

            int opcion = 0;
            while (opcion != 3)
            {
                Console.WriteLine("Bienvenido a la tienda de celulares");
                Console.WriteLine("1. Crear celular");
                Console.WriteLine("2. Mostrar celulares");
                Console.WriteLine("3. Salir");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1)
                {
                    Console.WriteLine("Ingrese tipo de celular (Samsung, Apple, Otro):");
                    string tipo = Console.ReadLine();

                    // 🚨 Factory mal implementado: usa if en vez de polimorfismo
                    var celular = factory.CrearCelular(tipo);
                    celulares.Add(celular);
                }
                else if (opcion == 2)
                {
                    foreach (var c in celulares)
                    {
                        // 🚨 No hay interfaz ni clase base
                        var dic = (Dictionary<string, object>)c;
                        Console.WriteLine("Celular: " + dic["marca"] + " - " + dic["modelo"] + " - $" + dic["precio"]);
                    }
                }
                else if (opcion == 3)
                {
                    Console.WriteLine("Saliendo...");
                }
            }
        }
    }

    // 🚨 "Factory" mal hecho: contiene lógica de negocio y rompe SRP
    public class CelularFactory
    {
        public object CrearCelular(string tipo)
        {
            if (tipo == "Samsung")
            {
                var celular = new Dictionary<string, object>();
                celular["marca"] = "Samsung";
                celular["modelo"] = "Galaxy Ultra";
                celular["precio"] = 1200.50;
                return celular;
            }
            else if (tipo == "Apple")
            {
                var celular = new Dictionary<string, object>();
                celular["marca"] = "Apple";
                celular["modelo"] = "iPhone 15";
                celular["precio"] = 1500.00;
                return celular;
            }
            else
            {
                var celular = new Dictionary<string, object>();
                celular["marca"] = "Genérico";
                celular["modelo"] = "Modelo X";
                celular["precio"] = 500.00;
                return celular;
            }
        }
    }
}

```


# Refactorización con Patrón Creacional (Factory Method)

## 1. Análisis del Problema

El código original intenta simular una tienda de celulares, pero presenta varios errores de diseño graves:

1. **No existe una jerarquía clara de productos**  
   - Los celulares se manejan como `Dictionary<string, object>`, lo que rompe la cohesión y obliga a hacer conversiones inseguras.  
   - No hay encapsulamiento ni tipado fuerte.  

2. **Factory mal diseñado**  
   - La clase `CelularFactory` usa condicionales (`if/else`) para decidir qué crear.  
   - Esto viola el **Principio Abierto/Cerrado (OCP)** porque cada nuevo tipo de celular requiere modificar el método.  

3. **Mezcla de responsabilidades**  
   - El Factory contiene tanto lógica de creación como de negocio (atributos de los celulares).  
   - Rompe el **Principio de Responsabilidad Única (SRP)**.  

---

## 2. Solución Propuesta

Se aplica el patrón **Factory Method** para mejorar la legibilidad, cohesión y reutilización.

### Cambios principales:
- Se define una **interfaz común (`ICelular`)** que representa el producto.  
- Se crean **clases concretas (`Samsung`, `Apple`, `Generico`)** que implementan la interfaz.  
- Se introducen fábricas específicas (`SamsungFactory`, `AppleFactory`, `GenericoFactory`) que encapsulan la creación.  
- Se reemplazan los `if` por un **switch expression** que elige la fábrica adecuada.  

---

## 3. Código Refactorizado

```csharp
using System;
using System.Collections.Generic;

namespace CelularesApp
{
    // 1. Producto común
    public interface ICelular
    {
        string Marca { get; }
        string Modelo { get; }
        double Precio { get; }
        void MostrarInfo();
    }

    // 2. Productos concretos
    public class Samsung : ICelular
    {
        public string Marca => "Samsung";
        public string Modelo => "Galaxy Ultra";
        public double Precio => 1200.50;

        public void MostrarInfo()
        {
            Console.WriteLine($"Celular: {Marca} - {Modelo} - ${Precio}");
        }
    }

    public class Apple : ICelular
    {
        public string Marca => "Apple";
        public string Modelo => "iPhone 15";
        public double Precio => 1500.00;

        public void MostrarInfo()
        {
            Console.WriteLine($"Celular: {Marca} - {Modelo} - ${Precio}");
        }
    }

    public class Generico : ICelular
    {
        public string Marca => "Genérico";
        public string Modelo => "Modelo X";
        public double Precio => 500.00;

        public void MostrarInfo()
        {
            Console.WriteLine($"Celular: {Marca} - {Modelo} - ${Precio}");
        }
    }

    // 3. Factory Method
    public interface ICelularFactory
    {
        ICelular CrearCelular();
    }

    public class SamsungFactory : ICelularFactory
    {
        public ICelular CrearCelular() => new Samsung();
    }

    public class AppleFactory : ICelularFactory
    {
        public ICelular CrearCelular() => new Apple();
    }

    public class GenericoFactory : ICelularFactory
    {
        public ICelular CrearCelular() => new Generico();
    }

    // 4. Programa principal
    public class Program
    {
        public static void Main(string[] args)
        {
            var celulares = new List<ICelular>();
            int opcion = 0;

            while (opcion != 3)
            {
                Console.WriteLine("Bienvenido a la tienda de celulares");
                Console.WriteLine("1. Crear celular");
                Console.WriteLine("2. Mostrar celulares");
                Console.WriteLine("3. Salir");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1)
                {
                    Console.WriteLine("Ingrese tipo de celular (Samsung, Apple, Otro):");
                    string tipo = Console.ReadLine();

                    ICelularFactory factory = tipo switch
                    {
                        "Samsung" => new SamsungFactory(),
                        "Apple" => new AppleFactory(),
                        _ => new GenericoFactory()
                    };

                    celulares.Add(factory.CrearCelular());
                }
                else if (opcion == 2)
                {
                    foreach (var c in celulares)
                        c.MostrarInfo();
                }
                else if (opcion == 3)
                {
                    Console.WriteLine("Saliendo...");
                }
            }
        }
    }
}
```


