
Escobedo Aguilera Adrian

# ABSTRACT FACTORY

El patrón Abstract Factory es una técnica de diseño creacional que permite generar familias de objetos relacionados sin tener que especificar sus clases concretas.


# PROBLEMA

Imagina que estás desarrollando un simulador de una tienda de muebles. Tu código incluye clases que representan una familia de productos relacionados, como Silla, Sofá y Mesilla, con variantes disponibles, como Moderno, Victoriano y ArtDecó.
El reto es encontrar una manera de crear objetos de mobiliario que coincidan entre sí dentro de una misma familia de productos. Por ejemplo, un Sofá Moderno debe combinar con una Silla y una Mesilla del mismo estilo. Además, necesitamos una solución que permita agregar nuevos productos o familias sin modificar el código existente, ya que los catálogos de muebles cambian con frecuencia.


# SOLUCIÓN

El patrón Abstract Factory sugiere definir interfaces para cada tipo de producto dentro de una familia (Silla, Sofá, Mesilla). Luego, todas las variantes de esos productos deben implementar dichas interfaces. Por ejemplo, todas las versiones de una silla implementarán la interfaz "Silla".
Después, se crea una Fábrica Abstracta, que es una interfaz con métodos de creación para cada producto de la familia (como crearSilla, crearSofá, y crearMesilla). Estas fábricas devuelven productos abstractos que siguen las interfaces previamente definidas.


# SOLUCIÓN

Cada variante de una familia de productos tiene su propia fábrica concreta que implementa la Fábrica Abstracta. Por ejemplo, una Fábrica de Muebles Modernos creará únicamente Silla Moderna, Sofá Moderno y Mesilla Moderna.
El código cliente trabajará con estas fábricas y productos a través de sus interfaces abstractas, lo que permite cambiar la fábrica (y, por ende, la variante de los productos) sin modificar el código del cliente. El cliente sólo necesita saber que está trabajando con una "Silla" sin importar si es moderna o victoriana, siempre y cuando implemente el método sentarse.
La fábrica concreta se selecciona generalmente durante la inicialización de la aplicación, según configuraciones o preferencias del entorno.


# ESTRUCTURA

Productos abstractos: Declaran interfaces para los diferentes productos dentro de una familia.
Productos concretos: Son implementaciones específicas de los productos abstractos, agrupadas por variantes.

# ESTRUCTURA

Fábrica abstracta: Declara métodos para crear cada producto abstracto.
Fábricas concretas: Implementan los métodos de la fábrica abstracta y producen únicamente variantes específicas de los productos.

# APLICABILIDAD

Utilización del Patrón Abstract Factory: Usa el patrón Abstract Factory cuando tu código necesite manejar varias familias de productos relacionados, pero prefieras evitar depender de las clases concretas de esos productos. Esto puede ser útil si no tienes conocimiento previo de estas clases o si deseas permitir la expansión futura sin problemas.

Interfaz del Patrón Abstract Factory: El patrón Abstract Factory proporciona una interfaz para crear objetos pertenecientes a cada clase dentro de la familia de productos. Al utilizar esta interfaz para crear objetos, tu código no necesitará preocuparse por seleccionar la variante incorrecta de un producto, asegurando que todos los productos generados sean compatibles entre sí.

Implementación del Patrón Abstract Factory: Considera aplicar el patrón Abstract Factory cuando una clase contenga múltiples métodos de fábrica que confundan su responsabilidad principal.

Responsabilidad de la Clase en Diseño: En un diseño adecuado, cada clase debe encargarse de una sola tarea. Si una clase maneja diversos tipos de productos, podría ser conveniente extraer sus métodos de fábrica a una clase separada dedicada o a una implementación completa del patrón Abstract Factory.


# VENTAJAS

Puedes tener la certeza de que los productos que obtienes de una fábrica son compatibles entre sí.
Evitas un acoplamiento fuerte entre productos concretos y el código cliente.
Principio de responsabilidad única. Puedes mover el código de creación de productos a un solo lugar, haciendo que el código sea más fácil de mantener.
Principio de abierto/cerrado. Puedes introducir nuevas variantes de productos sin descomponer el código cliente existente.


# DESVENTAJAS

Puede ser que el código se complique más de lo que debería, ya que se introducen muchas nuevas interfaces y clases junto al patrón.

# Ejemplo
```
using System;

namespace AbstractFactoryExample
{
    // Interfaces para los productos
    public interface ICar
    {
        void Drive(); // Método que deben implementar todos los carros
    }

    public interface IMotorcycle
    {
        void Ride(); // Método que deben implementar todas las motos
    }

    // Interfaces para la fábrica abstracta
    public interface IVehicleFactory
    {
        ICar CreateCar(); // Método para crear un carro
        IMotorcycle CreateMotorcycle(); // Método para crear una moto
    }

    // Implementaciones concretas de productos (Gasolina)
    class GasolineCar : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Conduciendo un carro a gasolina.");
        }
    }

    class GasolineMotorcycle : IMotorcycle
    {
        public void Ride()
        {
            Console.WriteLine("Montando una moto a gasolina.");
        }
    }

    // Implementaciones concretas de productos (Eléctrico)
    class ElectricCar : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Conduciendo un carro eléctrico.");
        }
    }

    class ElectricMotorcycle : IMotorcycle
    {
        public void Ride()
        {
            Console.WriteLine("Montando una moto eléctrica.");
        }
    }

    // Implementaciones concretas de las fábricas
    class GasolineFactory : IVehicleFactory
    {
        public ICar CreateCar()
        {
            return new GasolineCar(); // Crea un carro a gasolina
        }

        public IMotorcycle CreateMotorcycle()
        {
            return new GasolineMotorcycle(); // Crea una moto a gasolina
        }
    }

    class ElectricFactory : IVehicleFactory
    {
        public ICar CreateCar()
        {
            return new ElectricCar(); // Crea un carro eléctrico
        }

        public IMotorcycle CreateMotorcycle()
        {
            return new ElectricMotorcycle(); // Crea una moto eléctrica
        }
    }

    // Cliente que utiliza las fábricas y productos abstractos
    class Client
    {
        private readonly ICar _car;
        private readonly IMotorcycle _motorcycle;

        // Constructor que recibe una fábrica y crea productos usando esa fábrica
        public Client(IVehicleFactory factory)
        {
            _car = factory.CreateCar(); // Crea un carro usando la fábrica
            _motorcycle = factory.CreateMotorcycle(); // Crea una moto usando la fábrica
        }

        // Método para ejecutar las operaciones de los productos
        public void Run()
        {
            _car.Drive(); // Llama al método Drive del carro
            _motorcycle.Ride(); // Llama al método Ride de la moto
        }
    }

    // Programa principal
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Produciendo vehículos a gasolina:");
            IVehicleFactory gasolineFactory = new GasolineFactory(); // Crea una fábrica de gasolina
            Client gasolineClient = new Client(gasolineFactory); // Crea un cliente con la fábrica de gasolina
            gasolineClient.Run(); // Ejecuta las operaciones de los productos a gasolina

            Console.WriteLine("\nProduciendo vehículos eléctricos:");
            IVehicleFactory electricFactory = new ElectricFactory(); // Crea una fábrica eléctrica
            Client electricClient = new Client(electricFactory); // Crea un cliente con la fábrica eléctrica
            electricClient.Run(); // Ejecuta las operaciones de los productos eléctricos
        }
    }
}

```
