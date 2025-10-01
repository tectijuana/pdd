## Mezclar lógica de abstracción e implementación en la misma clase
Eduardo Gallardo Dueñas 21212215 - 29/09/25

# ❌ Código con Code Smells (antes del refactor)
``` c#
// PaymentProcessor mezcla abstracción e implementación
// Code Smells detectados:
// 1. God Class: concentra demasiada lógica.
// 2. Switch Statements: difícil de mantener/escalar.
// 3. Falta de separación de responsabilidades: viola SRP y OCP.
public class PaymentProcessor
{
    private string _type;

    public PaymentProcessor(string type)
    {
        _type = type;
    }

    public void ProcessPayment(decimal amount)
    {
        if (_type == "credit")
        {
            Console.WriteLine($"Procesando pago con tarjeta de crédito: {amount:C}");
        }
        else if (_type == "paypal")
        {
            Console.WriteLine($"Procesando pago con PayPal: {amount:C}");
        }
        else
        {
            Console.WriteLine("Método de pago no soportado");
        }
    }
}

// Uso
class Program
{
    static void Main()
    {
        var processor = new PaymentProcessor("credit");
        processor.ProcessPayment(150.0m);
    }
}
```

# ✅ Refactor con Patrón Bridge
``` c#
// Implementor: interfaz para procesar pagos
public interface IPaymentMethod
{
    void Pay(decimal amount);
}

// ConcreteImplementors: implementaciones específicas
public class CreditCardPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Procesando pago con tarjeta de crédito: {amount:C}");
    }
}

public class PayPalPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Procesando pago con PayPal: {amount:C}");
    }
}

// Abstraction: define el rol de un procesador de pagos
public abstract class PaymentProcessor
{
    protected readonly IPaymentMethod _method;

    protected PaymentProcessor(IPaymentMethod method)
    {
        _method = method;
    }

    public abstract void Process(decimal amount);
}

// RefinedAbstraction: desacopla abstracción de implementación
public class OnlinePaymentProcessor : PaymentProcessor
{
    public OnlinePaymentProcessor(IPaymentMethod method) : base(method) { }

    public override void Process(decimal amount)
    {
        _method.Pay(amount);
    }
}

// Uso
class Program
{
    static void Main()
    {
        PaymentProcessor creditProcessor = new OnlinePaymentProcessor(new CreditCardPayment());
        creditProcessor.Process(150.0m);

        PaymentProcessor paypalProcessor = new OnlinePaymentProcessor(new PayPalPayment());
        paypalProcessor.Process(75.0m);
    }
}
```
