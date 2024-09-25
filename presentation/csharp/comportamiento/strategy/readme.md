# Patrón de Diseño Strategy
## Definición del Patrón de Diseño Strategy
Strategy es un patrón de diseño de comportamiento que te permite definir una familia de algoritmos, colocar cada uno de ellos en una clase separada y hacer sus objetos intercambiables.
## Estructura del Patrón Strategy
El patrón Strategy sugiere que tomes esa clase que hace algo específico de muchas formas diferentes y extraigas todos esos
algoritmos para colocarlos en clases separadas llamadas estrategias.
<p>La clase original, llamada contexto, debe tener un campo para
almacenar una referencia a una de las estrategias. El contexto
delega el trabajo a un objeto de estrategia vinculado en lugar
de ejecutarlo por su cuenta.</p>
<p>La clase contexto no es responsable de seleccionar un algoritmo adecuado para la tarea. En lugar de eso, el cliente pasa la
estrategia deseada a la clase contexto. De hecho, la clase contexto no sabe mucho acerca de las estrategias. Funciona con
todas las estrategias a través de la misma interfaz genérica,
que sólo expone un único método para disparar el algoritmo
encapsulado dentro de la estrategia seleccionada.</p>
<p>De esta forma, el contexto se vuelve independiente de las estrategias concretas, así que puedes añadir nuevos algoritmos o
modificar los existentes sin cambiar el código de la clase contexto o de otras estrategias.</p>

![imagen](https://github.com/user-attachments/assets/8383dd71-ae94-4c60-a684-6f1438f5ad95)

1. La clase **Contexto** mantiene una referencia a una de las estrategias concretas y se comunica con este objeto únicamente a través de la interfaz estrategia.
2. La interfaz **Estrategia** es común a todas las estrategias concretas. Declara un método que la clase contexto utiliza para ejecutar una estrategia.
3. Las **Estrategias Concretas** implementan distintas variaciones de un algoritmo que la clase contexto utiliza.
4. La clase contexto invoca el método de ejecución en el objeto de estrategia vinculado cada vez que necesita ejecutar el algoritmo. La clase contexto no sabe con qué tipo de estrategia funciona o cómo se ejecuta el algoritmo.
5. El Cliente crea un objeto de estrategia específico y lo pasa a la clase contexto. La clase contexto expone un modificador *set* que permite a los clientes sustituir la estrategia asociada al contexto durante el tiempo de ejecución.

### Ejemplo en el mundo real
![imagen](https://github.com/user-attachments/assets/a39c1305-315e-46f9-971b-65079c06fd1b)


## Aplicabilidad

- Utiliza el patrón Strategy cuando quieras utiliza distintas variantes de un algoritmo dentro de un objeto y poder cambiar de un algoritmo a otro durante el tiempo de ejecución.<br>
> El patrón Strategy te permite alterar indirectamente el comportamiento del objeto durante el tiempo de ejecución asociándolo con distintos subobjetos que pueden realizar subtareas específicas de distintas maneras.
- Utiliza el patrón Strategy cuando tengas muchas clases similares que sólo se diferencien en la forma en que ejecutan cierto comportamiento.<br>
> El patrón Strategy te permite extraer el comportamiento variante para ponerlo en una jerarquía de clases separada y combinar las clases originales en una, reduciendo con ello el código duplicado.

## Pros y contras
### Pros

- Puedes intercambiar algoritmos usados dentro de un objeto durante el tiempo de ejecución.
- Puedes aislar los detalles de implementación de un algoritmo del código que lo utiliza.
- Puedes sustituir la herencia por composición.
- *Principio de abierto/cerrado*. Puedes introducir nuevas estrategias sin tener que cambiar el contexto.

### Contras

- Si sólo tienes un par de algoritmos que raramente cambian, no hay una razón real para complicar el programa en exceso con nuevas clases e interfaces que vengan con el patrón.
- Los clientes deben conocer las diferencias entre estrategias para poder seleccionar la adecuada.
- Muchos lenguajes de programación modernos tienen un soporte de tipo funcional que te permite implementar distintas versiones de un algoritmo dentro de un grupo de funciones anónimas. Entonces puedes utilizar estas funciones exactamente como habrías utilizado los objetos de estrategia, pero sin saturar tu código con clases e interfaces adicionales.


## Ejemplo Codigo

https://dotnetfiddle.net/hv6EXK

```c#
using System;

// Step 1: Define the Strategy interface (IPaymentStrategy)
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

// Step 2: Implement Concrete Strategies (Credit Card, PayPal, Bank Transfer)
public class CreditCardPayment : IPaymentStrategy
{
    private string _cardNumber;
    private string _cardHolderName;

    public CreditCardPayment(string cardNumber, string cardHolderName)
    {
        _cardNumber = cardNumber;
        _cardHolderName = cardHolderName;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Credit Card. Card Holder: {_cardHolderName}, Card Number: {_cardNumber}");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    private string _email;

    public PayPalPayment(string email)
    {
        _email = email;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using PayPal. PayPal account: {_email}");
    }
}

public class BankTransferPayment : IPaymentStrategy
{
    private string _bankAccountNumber;

    public BankTransferPayment(string bankAccountNumber)
    {
        _bankAccountNumber = bankAccountNumber;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Bank Transfer. Bank Account Number: {_bankAccountNumber}");
    }
}

// Step 3: Create the Context class (ShoppingCart)
public class ShoppingCart
{
    private IPaymentStrategy _paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void Checkout(decimal amount)
    {
        if (_paymentStrategy == null)
        {
            Console.WriteLine("Payment strategy not selected.");
            return;
        }

        _paymentStrategy.Pay(amount);
    }
}

// Step 4: Demonstrate the Strategy Pattern in action
class Program
{
    static void Main(string[] args)
    {
        ShoppingCart cart = new ShoppingCart();

        // Set the payment strategy to Credit Card and checkout
        cart.SetPaymentStrategy(new CreditCardPayment("1234-5678-9876-5432", "John Doe"));
        cart.Checkout(100.50m);

        // Set the payment strategy to PayPal and checkout
        cart.SetPaymentStrategy(new PayPalPayment("john.doe@example.com"));
        cart.Checkout(75.25m);

        // Set the payment strategy to Bank Transfer and checkout
        cart.SetPaymentStrategy(new BankTransferPayment("987654321"));
        cart.Checkout(50.75m);
    }
}

```
