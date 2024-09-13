# Casos Reales de Patrones de Diseño GoF
Este proyecto ilustra la implementación y aplicación de varios patrones de diseño descritos en el libro Design Patterns: Elements of Reusable Object-Oriented Software de Erich Gamma, Richard Helm, Ralph Johnson y John Vlissides, conocidos como los Gang of Four (GoF). A continuación, se presentan ejemplos prácticos de algunos patrones de diseño comunes utilizados en el desarrollo de software en C#.

Patrones de Diseño Presentados
1. Singleton
Propósito: Garantiza que una clase tenga una única instancia y proporciona un punto de acceso global a ella.

Caso Real: En una aplicación de configuración de sistema, el patrón Singleton se utiliza para asegurar que solo haya una instancia de la clase ConfigurationManager que maneje la configuración global del sistema.

```csharp
Copiar código
public class ConfigurationManager
{
    private static ConfigurationManager _instance;
    private static readonly object _lock = new object();
    private Dictionary<string, string> _config = new Dictionary<string, string>();

    private ConfigurationManager() { }

    public static ConfigurationManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }
                return _instance;
            }
        }
    }

    public void Set(string key, string value)
    {
        _config[key] = value;
    }

    public string Get(string key)
    {
        return _config.TryGetValue(key, out var value) ? value : null;
    }
}
```
2. Observer
Propósito: Define una dependencia de uno a muchos entre objetos, de manera que cuando un objeto cambie de estado, todos sus dependientes sean notificados y actualizados automáticamente.

Caso Real: En una aplicación de noticias, el patrón Observer se utiliza para permitir que varios usuarios reciban actualizaciones en tiempo real sobre nuevos artículos.

```csharp
Copiar código
using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(string article);
}

public class NewsPublisher
{
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Notify(string article)
    {
        foreach (var observer in _observers)
        {
            observer.Update(article);
        }
    }

    public void PublishArticle(string article)
    {
        Notify(article);
    }
}

public class NewsReader : IObserver
{
    public void Update(string article)
    {
        Console.WriteLine($"New article published: {article}");
    }
}
```
3. Factory Method
Propósito: Define una interfaz para crear un objeto, pero permite a las subclases alterar el tipo de objeto que se va a crear.

Caso Real: En una aplicación de pago en línea, el patrón Factory Method se utiliza para crear instancias de diferentes tipos de métodos de pago (por ejemplo, tarjeta de crédito, PayPal) según la elección del usuario.

```csharp
Copiar código
public abstract class PaymentProcessor
{
    public abstract IPayment CreatePayment();
}

public class CreditCardPaymentProcessor : PaymentProcessor
{
    public override IPayment CreatePayment()
    {
        return new CreditCardPayment();
    }
}

public class PayPalPaymentProcessor : PaymentProcessor
{
    public override IPayment CreatePayment()
    {
        return new PayPalPayment();
    }
}

public interface IPayment
{
    void Process();
}

public class CreditCardPayment : IPayment
{
    public void Process()
    {
        Console.WriteLine("Processing credit card payment");
    }
}

public class PayPalPayment : IPayment
{
    public void Process()
    {
        Console.WriteLine("Processing PayPal payment");
    }
}

public class PaymentFactory
{
    public PaymentProcessor GetPaymentProcessor(string paymentType)
    {
        return paymentType switch
        {
            "credit_card" => new CreditCardPaymentProcessor(),
            "paypal" => new PayPalPaymentProcessor(),
            _ => throw new ArgumentException("Unknown payment type")
        };
    }
}
```
4. Decorator
Propósito: Permite agregar funcionalidades a un objeto de manera dinámica sin alterar su estructura.

Caso Real: En una aplicación de procesamiento de imágenes, el patrón Decorator se usa para añadir diferentes filtros a las imágenes (como sepia, blanco y negro) sin modificar el objeto de imagen original.

csharp
Copiar código
using System;

public interface IImage
{
    void Display();
}

public class ConcreteImage : IImage
{
    public void Display()
    {
        Console.WriteLine("Displaying image");
    }
}

public abstract class ImageDecorator : IImage
{
    protected readonly IImage _image;

    protected ImageDecorator(IImage image)
    {
        _image = image;
    }

    public virtual void Display()
    {
        _image.Display();
    }
}

public class SepiaDecorator : ImageDecorator
{
    public SepiaDecorator(IImage image) : base(image) { }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("Applying sepia filter");
    }
}

public class BlackAndWhiteDecorator : ImageDecorator
{
    public BlackAndWhiteDecorator(IImage image) : base(image) { }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("Applying black and white filter");
    }
}
Esta sección en el README.md proporciona ejemplos claros de cómo se implementan estos patrones en C#. Puedes adaptar y expandir los ejemplos según sea necesario para tu proyecto.





ChatGPT puede cometer errores. Considera verificar la información importante.
