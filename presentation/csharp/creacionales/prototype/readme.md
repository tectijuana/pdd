## Edwin Alejandro Vargas Reyes - 19290961
# **Prototype**
![Patron de diseño prototype](https://refactoring.guru/images/patterns/cards/prototype-mini-3x.png)

Pertenece a la categoría de **patrones creacionales**. Estos se centran en cómo crear objetos de manera que se favorezca la flexibilidad y la reutilización del código. El patrón **Prototype** se utiliza cuando queremos crear nuevos objetos copiando o "clonando" una instancia existente en lugar de crearlos desde cero 

## Ejemplo

Una empresa de transporte gestiona una flota de vehículos, Cada vehículo tiene características comunes (tipo, color, motor, etc.), pero existen muchos vehículos que comparten las mismas configuraciones.

Crear cada uno de estos vehículos desde cero usando el constructor de una clase puede ser ineficiente, especialmente cuando la mayoría de ellos solo difieren en algunos detalles. Esta repetición de creación puede ser costosa en términos de tiempo de desarrollo y recursos computacionales.

-   Prototype permite crear objetos rápidamente mediante clonación sin la necesidad de definir una gran cantidad de subclases.

-   Prototype facilita la extensión y modificación de objetos en tiempo de ejecución.

-   Prototype reduce la repetición de código, ya que no necesitas duplicar lógica común en múltiples subclases.

-   Prototype ofrece flexibilidad para manejar nuevas configuraciones y cambios sin requerir una reestructuración completa del código.

https://dotnetfiddle.net/hz4c5E 

## Código en C#
```C#
// Clase base Vehicle
public class Vehicle
{
    public string Type { get; set; }
    public string Color { get; set; }
    public string Engine { get; set; }

    // Constructor
    public Vehicle(string type, string color, string engine)
    {
        Type = type;
        Color = color;
        Engine = engine;
    }

    // Método para clonar el vehículo actual
    public Vehicle Clone()
    {
        return new Vehicle(Type, Color, Engine);
    }
}

// Crear prototipos específicos
public class Program
{
    public static void Main(string[] args)
    {
        Vehicle carPrototype = new Vehicle("Car", "Red", "V4");
        Vehicle truckPrototype = new Vehicle("Truck", "Blue", "V8");

        // Clonar los prototipos para crear nuevos vehículos
        Vehicle car1 = carPrototype.Clone();
        Vehicle car2 = carPrototype.Clone();
        Vehicle truck1 = truckPrototype.Clone();

        // Modificar algunas propiedades si es necesario
        car1.Color = "Black";
        truck1.Engine = "V10";

        // Mostrar resultados
        System.Console.WriteLine($"Car1: {car1.Type}, Color: {car1.Color}, Engine: {car1.Engine}");
        System.Console.WriteLine($"Car2: {car2.Type}, Color: {car2.Color}, Engine: {car2.Engine}");
        System.Console.WriteLine($"Truck1: {truck1.Type}, Color: {truck1.Color}, Engine: {truck1.Engine}");
    }
}


```
Pros y contras

✅ Puedes clonar objetos sin acoplarlos a sus clases concretas.

✅ Obtienes una alternativa a la herencia al tratar con preajustes de configuración para objetos complejos.

✅ Ofrece la posibilidad de que el cliente genere objetos de un tipo desconocido.

❌ Clonar objetos complejos con referencias circulares puede resultar complicado.



