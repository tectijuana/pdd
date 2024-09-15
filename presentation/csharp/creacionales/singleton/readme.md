# ❝𝐒𝐈𝐍𝐆𝐋𝐄𝐓𝐎𝐍❞
Imagina que necesitas un objeto especial en tu programa, pero solo quieres uno. El patrón Singleton te asegura que siempre tengas ese objeto único y que nadie más pueda crear copias. 
Es como tener un objeto muy especial al que todos pueden acceder, pero solo hay uno en todo el programa.

## Una Instancia, Múltiples Beneficios
El patrón Singleton es útil por varias razones clave que contribuyen a su popularidad en el desarrollo de software:
1.Única instancia: Asegura que una clase tenga solo un objeto a lo largo de toda la aplicación. 
2. Punto de acceso global: Proporciona un punto de acceso único a esta instancia desde cualquier parte del código.
3. Control de Creación: Permite un control preciso sobre cuándo y cómo se crea la instancia.
4. Estado Consistente: Garantiza que el estado de la instancia sea el mismo en toda la aplicación.
5. Facilidad de Mantenimiento: Centraliza la gestión de la instancia, simplificando los cambios.

## Ejemplo
```csharp
using System;

namespace RefactoringGuru.DesignPatterns.Singleton.Conceptual.NonThreadSafe
{
    // The Singleton class defines the `GetInstance` method that serves as an
    // alternative to constructor and lets clients access the same instance of
    // this class over and over.

    // EN : The Singleton should always be a 'sealed' class to prevent class
    // inheritance through external classes and also through nested classes.
    public sealed class Singleton
    {
        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private Singleton() { }

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static Singleton _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        // Finally, any singleton should define some business logic, which can
        // be executed on its instance.
        public void someBusinessLogic()
        {
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            if (s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }
        }
    }
}
