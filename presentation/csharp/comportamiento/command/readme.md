# Exposicion sobre el Patron de Comando
## Emiliano Garcia Cordero - 20211779

# Command Pattern

Command es un patrón de diseño de comportamiento que convierte una solicitud en un objeto independiente que contiene toda la información sobre la solicitud. Esta transformación te permite parametrizar los métodos con diferentes solicitudes, retrasar o poner en cola la ejecución de una solicitud y soportar operaciones que no se pueden realizar.

# Analogía en el mundo real

![imagenprueba](https://refactoring.guru/images/patterns/content/command/command-comic-1.png?id=551df832f445080976f3116e0dc120c9)


<p>Un camarero se acerca y toma tu pedido, apuntándolo en un papel. El camarero se va a la cocina y pega el pedido a la pared. Al cabo de un rato, el pedido llega al chef, que lo lee y prepara la comida. El cocinero coloca la comida en una bandeja junto al pedido. El camarero descubre la bandeja, comprueba el pedido para asegurarse de que todo está como lo querías, y lo lleva todo a tu mesa.</p>

<p>El pedido en papel hace la función de un comando. Permanece en una cola hasta que el chef está listo para servirlo. Este pedido contiene toda la información relevante necesaria para preparar la comida. Permite al chef empezar a cocinar de inmediato, en lugar de tener que correr de un lado a otro aclarando los detalles del pedido directamente contigo.</p>

# Estructura
![image](https://github.com/user-attachments/assets/4a2653a1-7722-426c-99a7-249621a672dd)

1. La clase Emisora (o invocadora) es responsable de inicializar las solicitudes. Esta clase debe tener un campo para almacenar una referencia a un objeto de comando. El emisor activa este comando en lugar de enviar la solicitud directamente al receptor. Ten en cuenta que el emisor no es responsable de crear el objeto de comando. Normalmente, obtiene un comando precreado de parte del cliente a través del constructor.
2. La interfaz Comando normalmente declara un único método para ejecutar el comando.
3. Los Comandos Concretos implementan varios tipos de solicitudes. Un comando concreto no se supone que tenga que realizar el trabajo por su cuenta, sino pasar la llamada a uno de los objetos de la lógica de negocio. Sin embargo, para lograr simplificar el código, estas clases se pueden fusionar. Los parámetros necesarios para ejecutar un método en un objeto receptor pueden declararse como campos en el comando concreto. Puedes hacer inmutables los objetos de comando permitiendo la inicialización de estos campos únicamente a través del constructor.
4. La clase Receptora contiene cierta lógica de negocio. Casi cualquier objeto puede actuar como receptor. La mayoría de los comandos solo gestiona los detalles sobre cómo se pasa una solicitud al receptor, mientras que el propio receptor hace el trabajo real.
5. El Cliente crea y configura los objetos de comando concretos. El cliente debe pasar todos los parámetros de la solicitud, incluyendo una instancia del receptor, dentro del constructor del comando. Después de eso, el comando resultante puede asociarse con uno o varios emisores.



# Pseudocódigo
El patrón Command ayuda a rastrear el historial de operaciones ejecutadas y hace posible revertir una operación si es necesario.
![pseudocodigo](https://refactoring.guru/images/patterns/diagrams/command/example.png?id=1f42c8395fe54d0e409026b91881e2a0)

# Codigo ejemplo

[DotNetFiddle](https://dotnetfiddle.net/Sc5fEM)

```c#
using System;

// Interfaz del comando.
public interface ICommand
{
    void Execute();
}

// Comando para registrar una venta simple.
class RegisterSaleCommand : ICommand
{
    private string _product;
    private int _quantity;

    public RegisterSaleCommand(string product, int quantity)
    {
        this._product = product;
        this._quantity = quantity;
    }

    public void Execute()
    {
        Console.WriteLine("Registrando venta: Producto: " + _product + ", Cantidad: " + _quantity);
    }
}

// Comando para aplicar un descuento a la venta.
class ApplyDiscountCommand : ICommand
{
    private double _discountPercentage;

    public ApplyDiscountCommand(double discountPercentage)
    {
        this._discountPercentage = discountPercentage;
    }

    public void Execute()
    {
        Console.WriteLine("Aplicando descuento del " + _discountPercentage + "% a la venta.");
    }
}

// Comando complejo para actualizar el inventario.
class UpdateInventoryCommand : ICommand
{
    private InventoryManager _inventoryManager;
    private string _product;
    private int _quantity;

    public UpdateInventoryCommand(InventoryManager inventoryManager, string product, int quantity)
    {
        this._inventoryManager = inventoryManager;
        this._product = product;
        this._quantity = quantity;
    }

    public void Execute()
    {
        Console.WriteLine("Actualizando inventario...");
        _inventoryManager.RemoveProduct(_product, _quantity);
    }
}

// Receptor que maneja el inventario.
class InventoryManager
{
    public void RemoveProduct(string product, int quantity)
    {
        Console.WriteLine("Inventario: Producto '" + product + "' reducido en " + quantity + " unidades.");
    }

    public void AddProduct(string product, int quantity)
    {
        Console.WriteLine("Inventario: Producto '" + product + "' incrementado en " + quantity + " unidades.");
    }
}

// El Invocador que manejará los comandos.
class PointOfSaleInvoker
{
    private ICommand _onSale;
    private ICommand _onDiscount;
    private ICommand _onInventoryUpdate;

    public void SetSaleCommand(ICommand command)
    {
        this._onSale = command;
    }

    public void SetDiscountCommand(ICommand command)
    {
        this._onDiscount = command;
    }

    public void SetInventoryUpdateCommand(ICommand command)
    {
        this._onInventoryUpdate = command;
    }

    public void ProcessSale()
    {
        Console.WriteLine("Iniciando proceso de venta...");

        if (this._onSale is ICommand)
        {
            this._onSale.Execute();
        }

        if (this._onDiscount is ICommand)
        {
            this._onDiscount.Execute();
        }

        if (this._onInventoryUpdate is ICommand)
        {
            this._onInventoryUpdate.Execute();
        }

        Console.WriteLine("Proceso de venta finalizado.");
    }
}

// Cliente
public class Program
{
    public static void Main(string[] args)
    {
        // Crear el invocador (punto de venta)
        PointOfSaleInvoker pos = new PointOfSaleInvoker();

        // Configurar los comandos
        pos.SetSaleCommand(new RegisterSaleCommand("Laptop", 1));
        pos.SetDiscountCommand(new ApplyDiscountCommand(10)); // 10% de descuento
        InventoryManager inventoryManager = new InventoryManager();
        pos.SetInventoryUpdateCommand(new UpdateInventoryCommand(inventoryManager, "Laptop", 1));

        // Ejecutar el proceso de venta
        pos.ProcessSale();
    }
}
```


Y la salida seria la siguente:

<pre>
Iniciando proceso de venta...
Registrando venta: Producto: Laptop, Cantidad: 1
Aplicando descuento del 10% a la venta.
Actualizando inventario...
Inventario: Producto 'Laptop' reducido en 1 unidades.
Proceso de venta finalizado.
</pre>

# Explicacion
1. Comando para registrar una venta: Este comando simula el registro de una venta de un producto, como una "Laptop" con una cantidad de 1.


2. Comando para aplicar un descuento: Este comando aplica un descuento del 10% a la venta.


3. Comando para actualizar el inventario: Después de realizar la venta, se actualiza el inventario restando la cantidad vendida.


4. Receptor (InventoryManager): El InventoryManager gestiona el inventario, reduciendo o aumentando productos según las acciones ejecutadas.


5. Invoker (PointOfSaleInvoker): Es el encargado de ejecutar los comandos en el momento adecuado, en este caso, durante el proceso de venta.


# Pros y contras
| Pros     | Contras |
|----------|----------|
| Principio de responsabilidad única. Puedes desacoplar las clases que invocan operaciones de las que realizan esas operaciones.    | El código puede complicarse, ya que estás introduciendo una nueva capa entre emisores y receptores.   |
| Principio de abierto/cerrado. Puedes introducir nuevos comandos en la aplicación sin descomponer el código cliente existente. 
| Puedes implementar deshacer/rehacer. 
| Puedes implementar la ejecución diferida de operaciones.  
| Puedes ensamblar un grupo de comandos simples para crear uno complejo. 


# Referencias
[Refactoring](https://refactoring.guru/es/design-patterns/command)
