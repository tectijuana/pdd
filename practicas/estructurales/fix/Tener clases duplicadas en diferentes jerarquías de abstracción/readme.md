# Tener clases duplicadas en diferentes jerarquias de abstraccion
## Joel Cuevas Estrada - 22210298

## Codigo Espagueti

````c#
using System;
using System.Collections.Generic;

namespace SpaghettiExample
{
    // =============================
    // Jerarquía de Animales Domésticos
    // =============================
    public class Dog
    {
        public void Bark()
        {
            Console.WriteLine("Guau guau!");
        }

        public void Eat()
        {
            Console.WriteLine("El perro está comiendo croquetas.");
        }

        public void Sleep()
        {
            Console.WriteLine("El perro duerme en su casita.");
        }
    }

    public class Cat
    {
        public void Meow()
        {
            Console.WriteLine("Miau miau!");
        }

        public void Eat()
        {
            Console.WriteLine("El gato está comiendo atún.");
        }

        public void Sleep()
        {
            Console.WriteLine("El gato duerme en el sillón.");
        }
    }

    // =============================
    // Jerarquía de Animales Salvajes
    // =============================
    public class Lion
    {
        public void Roar()
        {
            Console.WriteLine("Roooar!");
        }

        public void Eat()
        {
            Console.WriteLine("El león está comiendo carne fresca.");
        }

        public void Sleep()
        {
            Console.WriteLine("El león duerme bajo un árbol.");
        }
    }

    public class Tiger
    {
        public void Growl()
        {
            Console.WriteLine("Grrrr!");
        }

        public void Eat()
        {
            Console.WriteLine("El tigre está comiendo un venado.");
        }

        public void Sleep()
        {
            Console.WriteLine("El tigre duerme en la selva.");
        }
    }

    // =============================
    // Programa Principal (espagueti total)
    // =============================
    class Program
    {
        static void Main(string[] args)
        {
            Dog d = new Dog();
            d.Bark();
            d.Eat();
            d.Sleep();

            Cat c = new Cat();
            c.Meow();
            c.Eat();
            c.Sleep();

            Lion l = new Lion();
            l.Roar();
            l.Eat();
            l.Sleep();

            Tiger t = new Tiger();
            t.Growl();
            t.Eat();
            t.Sleep();

            Console.ReadLine();
        }
    }
}


````

## Code Smells
- Duplicación de métodos: todas las clases tienen Eat() y Sleep(), pero implementados de forma aislada.
- No hay abstracciones comunes: cada animal hace lo suyo, sin herencia ni interfaces compartidas.
- Jerarquías separadas y desorganizadas: domésticos y salvajes tienen lógica repetida.
- Difícil de mantener: si se quire cambiar la lógica de Sleep(), se tiene que ir clase por clase.


## Refactorizacion con Bridge

- Identificar la duplicación
Métodos repetidos (Eat(), Sleep()) en todas las clases.
Distintos sonidos (Bark, Meow, Roar, Growl) con nombres diferentes → inconsistencia.
Dos jerarquías mezcladas: animales domésticos vs salvajes.

- Definir la Abstracción (Animal)
Crea una clase abstracta Animal que no implemente los métodos directamente, sino que delegue el comportamiento a otra clase.
````c#
public abstract class Animal
{
    protected IAnimalBehavior behavior;

    protected Animal(IAnimalBehavior behavior)
    {
        this.behavior = behavior;
    }

    public virtual void PerformSound() => behavior.MakeSound();
    public virtual void PerformEat() => behavior.Eat();
    public virtual void PerformSleep() => behavior.Sleep();
}
````

- Separar la Implementación (IAnimalBehavior)
Se define una interfaz para los comportamientos comunes (Bridge = “puente” hacia la implementación real).
````c#
public interface IAnimalBehavior
{
    void MakeSound();
    void Eat();
    void Sleep();
}
````

- Crear las implementaciones concretas
Cada animal tendrá su propia implementación de IAnimalBehavior, en lugar de clases duplicadas:
````c#
public class DogBehavior : IAnimalBehavior
{
    public void MakeSound() => Console.WriteLine("Guau guau!");
    public void Eat() => Console.WriteLine("El perro está comiendo croquetas.");
    public void Sleep() => Console.WriteLine("El perro duerme en su casita.");
}
````

- Construir jerarquías independientes
Ahora se puede separar animales por categoría sin repetir código.
````c#
public class DomesticAnimal : Animal
{
    public DomesticAnimal(IAnimalBehavior behavior) : base(behavior) { }
}

public class WildAnimal : Animal
{
    public WildAnimal(IAnimalBehavior behavior) : base(behavior) { }
}
````

- Usar composición en lugar de herencia
En el Main, cada animal se crea combinando jerarquía + comportamiento:

````c#
Animal dog = new DomesticAnimal(new DogBehavior());
Animal lion = new WildAnimal(new LionBehavior());
````

- Eliminar duplicación y ajustar nombres
Ya no se necesita Dog, Cat, Lion, Tiger como clases completas → solo quedan sus Behaviors.
Todas las acciones se unifican (PerformSound(), PerformEat(), PerformSleep()).

## Codigo final corregido

````c#
using System;

namespace BridgeExample
{
    // Implementación: comportamiento que puede variar
    public interface IAnimalBehavior
    {
        void MakeSound();
        void Eat();
        void Sleep();
    }

    // Implementaciones concretas
    public class DogBehavior : IAnimalBehavior
    {
        public void MakeSound() => Console.WriteLine("Guau guau!");
        public void Eat() => Console.WriteLine("El perro está comiendo croquetas.");
        public void Sleep() => Console.WriteLine("El perro duerme en su casita.");
    }

    public class CatBehavior : IAnimalBehavior
    {
        public void MakeSound() => Console.WriteLine("Miau miau!");
        public void Eat() => Console.WriteLine("El gato está comiendo atún.");
        public void Sleep() => Console.WriteLine("El gato duerme en el sillón.");
    }

    public class LionBehavior : IAnimalBehavior
    {
        public void MakeSound() => Console.WriteLine("Roooar!");
        public void Eat() => Console.WriteLine("El león está comiendo carne fresca.");
        public void Sleep() => Console.WriteLine("El león duerme bajo un árbol.");
    }

    public class TigerBehavior : IAnimalBehavior
    {
        public void MakeSound() => Console.WriteLine("Grrrr!");
        public void Eat() => Console.WriteLine("El tigre está comiendo un venado.");
        public void Sleep() => Console.WriteLine("El tigre duerme en la selva.");
    }

    // Abstracción: Animal
    public abstract class Animal
    {
        protected IAnimalBehavior behavior;

        protected Animal(IAnimalBehavior behavior)
        {
            this.behavior = behavior;
        }

        public virtual void PerformSound() => behavior.MakeSound();
        public virtual void PerformEat() => behavior.Eat();
        public virtual void PerformSleep() => behavior.Sleep();
    }

    // Jerarquía independiente: doméstico o salvaje
    public class DomesticAnimal : Animal
    {
        public DomesticAnimal(IAnimalBehavior behavior) : base(behavior) { }
    }

    public class WildAnimal : Animal
    {
        public WildAnimal(IAnimalBehavior behavior) : base(behavior) { }
    }

    // Programa principal
    class Program
    {
        static void Main(string[] args)
        {
            Animal dog = new DomesticAnimal(new DogBehavior());
            Animal cat = new DomesticAnimal(new CatBehavior());
            Animal lion = new WildAnimal(new LionBehavior());
            Animal tiger = new WildAnimal(new TigerBehavior());

            dog.PerformSound();
            dog.PerformEat();
            dog.PerformSleep();

            Console.WriteLine();

            cat.PerformSound();
            cat.PerformEat();
            cat.PerformSleep();

            Console.WriteLine();

            lion.PerformSound();
            lion.PerformEat();
            lion.PerformSleep();

            Console.WriteLine();

            tiger.PerformSound();
            tiger.PerformEat();
            tiger.PerformSleep();
        }
    }
}
````
