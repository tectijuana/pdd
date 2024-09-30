## Template Method (Método Plantilla)

El patrón **Template Method** es un patrón de diseño de comportamiento que define el esqueleto de un algoritmo en una clase base, dejando que las subclases implementen detalles específicos sin cambiar la estructura general del algoritmo.

Este patrón permite que se controle la estructura del flujo de trabajo en la clase base, mientras que los detalles de algunos pasos del proceso pueden variar en las subclases. De esta manera, se pueden reutilizar partes comunes del algoritmo y modificar solo aquellos pasos que son específicos para cada caso.

### Estructura
1. **Clase abstracta (Template Class):** Define el esqueleto del algoritmo en el método plantilla, que consiste en una serie de pasos. Algunos de estos pasos pueden estar implementados en la clase base, mientras que otros son dejados como abstractos para que las subclases los implementen.
2. **Subclases concretas:** Estas clases heredan de la clase abstracta y proporcionan implementaciones específicas de los métodos abstractos definidos en la clase base.

### Ventajas
- **Reutilización de código:** Las subclases reutilizan el código común definido en la clase base, mientras que pueden cambiar solo los detalles específicos.
- **Control del flujo:** El patrón asegura que la estructura del algoritmo no se modifique, manteniendo consistencia en los procesos generales.
- **Facilidad para extender:** Es fácil añadir nuevas variantes del algoritmo simplemente creando nuevas subclases que implementen los métodos abstractos.

### Desventajas
- **Rigidez en la estructura:** Aunque proporciona flexibilidad en algunos pasos, el algoritmo base no se puede cambiar, lo que puede limitar su uso en algunos escenarios.
- **Complejidad:** Puede añadir complejidad en diseños si se abusa de su implementación en algoritmos que no requieren una estructura tan rígida.

### Ejemplo de uso
El *Template Method* es útil cuando diferentes tipos de operaciones siguen una secuencia similar de pasos pero requieren que algunos de esos pasos sean implementados de manera diferente. Por ejemplo:
- Preparación de bebidas como té o café, donde algunos pasos son comunes (hervir agua, verter en la taza), pero los pasos específicos varían (remojar té vs. colar café).

## Ejemplo en C# del patrón Template Method

A continuación se muestra un ejemplo simple de cómo implementar el patrón de diseño **Template Method** en C#.

```csharp
using System;

namespace TemplateMethodExample
{
    // Clase abstracta que define el método plantilla
    abstract class Beverage
    {
        // Método plantilla
        public void Prepare()
        {
            BoilWater();
            Brew();
            PourInCup();
            AddCondiments();
        }

        // Pasos específicos que las subclases deben implementar
        protected abstract void Brew();
        protected abstract void AddCondiments();

        private void BoilWater()
        {
            Console.WriteLine("Hirviendo agua...");
        }

        private void PourInCup()
        {
            Console.WriteLine("Vertiendo en la taza...");
        }
    }

    // Clase concreta que implementa los pasos específicos
    class Tea : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Remojando la bolsita de té...");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Agregando limón...");
        }
    }

    // Clase concreta que implementa los pasos específicos
    class Coffee : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Colando el café...");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Agregando azúcar y leche...");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Beverage tea = new Tea();
            Beverage coffee = new Coffee();

            Console.WriteLine("Preparando té:");
            tea.Prepare();

            Console.WriteLine("\nPreparando café:");
            coffee.Prepare();
        }
    }
}
```

### Explicación del código:

- **Beverage:** Clase abstracta que contiene el método plantilla `Prepare()`, el cual define la secuencia para preparar una bebida.
- **Tea** y **Coffee:** Subclases que implementan los métodos específicos `Brew()` y `AddCondiments()`.
- **Main:** Crea instancias de `Tea` y `Coffee`, y llama al método plantilla para prepararlos.
