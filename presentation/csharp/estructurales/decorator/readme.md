## Elvirez Dávila Ulises Gabriel - 20211769
# Decorator
Decorator es un patrón de diseño estructural que te permite añadir funcionalidades a objetos 
colocando estos objetos dentro de objetos encapsuladores especiales que contienen estas funcionalidades.
El decorator hereda la interfaz de la misma clase que el objeto que decora, de esta forma permite que los objetos decorados puedan ser tratados como si fueran instancias del obejeto original. 

![DecoratorImage](https://refactoring.guru/images/patterns/content/decorator/decorator.png?id=710c66670c7123e0928d3b3758aea79e)

# Analogía en el mundo real
Vestir ropa es un ejemplo del uso de decoradores. Cuando tienes frío, te cubres con un suéter. Si sigues teniendo frío a pesar
del suéter, puedes ponerte una chaqueta encima. Si está lloviendo, puedes ponerte un impermeable. Todas estas prendas “extienden” 
tu comportamiento básico pero no son parte de ti, y puedes quitarte fácilmente cualquier prenda cuando lo desees.

![ClothesImage](https://refactoring.guru/images/patterns/content/decorator/decorator-comic-1.png?id=80d95baacbfb91f5bcdbdc7814b0c64d)

# Estructura
1. El Componente declara la interfaz común tanto para wrappers como para objetos envueltos. 
2. Componente Concreto es una clase de objetos envueltos. Define el comportamiento básico, que los decoradores pueden alterar.
3. La clase Decoradora Base tiene un campo para referenciar un objeto envuelto. El tipo del campo debe declararse como la interfaz del componente para que pueda contener tanto los componentes concretos como los decoradores.
4. Los Decoradores Concretos definen funcionalidades adicionales que se pueden añadir dinámicamente a los componentes.
5. El Cliente puede envolver componentes en varias capas de decoradores, siempre y cuando trabajen con todos los objetos a través de la interfaz del componente.

![StructureImage](https://refactoring.guru/images/patterns/diagrams/decorator/structure.png) 

# Pros
### ✔ Es posible extender el comportamiento de un objeto sin crear una nueva subclase.
### ✔ Es posible combinar varios comportamientos envolviendo un objeto con varios decoradores.
### ✔ Es posible añadir o eliminar responsabilidades de un objeto durante el tiempo de ejecución.
### ✔ Es posible de responsabilidad única. Puedes dividir una clase monolítica que implementa muchas variantes posibles de comportamiento, en varias clases más pequeñas.

# Contras
### ❌ Resulta difícil eliminar un wrapper específico de la pila de wrappers.
### ❌ El código de configuración inicial de las capas pueden tener un aspecto desagradable.
### ❌ Es difícil implementar un decorador de tal forma que su comportamiento no dependa del orden en la pila de decoradores.

# Uso en código C#
```C#
using System;

namespace PatronDecoradorEjemplo
{
    // Interfaz que define el comportamiento de las notificaciones.
    public interface INotificacion
    {
        void EnviarMensaje(string mensaje);
    }

    // Clase concreta que implementa la interfaz de notificación básica (enviar por correo electrónico).
    public class NotificacionEmail : INotificacion
    {
        public void EnviarMensaje(string mensaje)
        {
            Console.WriteLine("Enviando mensaje por correo electrónico: " + mensaje);
        }
    }

    // Clase abstracta decorador que implementa la misma interfaz y permite extender funcionalidades.
    public abstract class NotificacionDecorador : INotificacion
    {
        protected INotificacion _notificacion;

        public NotificacionDecorador(INotificacion notificacion)
        {
            _notificacion = notificacion;
        }

        public virtual void EnviarMensaje(string mensaje)
        {
            _notificacion.EnviarMensaje(mensaje);
        }
    }

    // Decorador concreto que añade la funcionalidad de enviar notificaciones por SMS.
    public class NotificacionSMS : NotificacionDecorador
    {
        public NotificacionSMS(INotificacion notificacion) : base(notificacion) { }

        public override void EnviarMensaje(string mensaje)
        {
            base.EnviarMensaje(mensaje); // Llama al método original
            Console.WriteLine("Enviando mensaje por SMS: " + mensaje);
        }
    }

    // Decorador concreto que añade la funcionalidad de enviar notificaciones por WhatsApp.
    public class NotificacionWhatsApp : NotificacionDecorador
    {
        public NotificacionWhatsApp(INotificacion notificacion) : base(notificacion) { }

        public override void EnviarMensaje(string mensaje)
        {
            base.EnviarMensaje(mensaje); // Llama al método original
            Console.WriteLine("Enviando mensaje por WhatsApp: " + mensaje);
        }
    }

    class Programa
    {
        static void Main(string[] args)
        {
            // Creamos una notificación básica (por email).
            INotificacion notificacion = new NotificacionEmail();
            
            // Decoramos la notificación para enviar también por SMS.
            notificacion = new NotificacionSMS(notificacion);
            
            // Decoramos nuevamente para enviar también por WhatsApp.
            notificacion = new NotificacionWhatsApp(notificacion);

            // Enviamos un mensaje con todas las notificaciones (email, SMS, WhatsApp).
            notificacion.EnviarMensaje("¡Hola! Este es un mensaje importante.");
        }
    }
}
```
https://dotnetfiddle.net/aIibXV

# Referencias
Decorator. (s. f.). https://refactoring.guru/es/design-patterns/decorator
