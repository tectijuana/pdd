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


## Ejemplo Codigo

https://dotnetfiddle.net/yaQN3L

```c#
using System;
using System.Collections.Generic;

namespace prueba
{
    // El Contexto define la interfaz de intereses del cliente.
    class Context
    {
        // El Contexto mantiene una referencia de uno de los objetos de Estrategia. El
        // Contexto no sabe la clase concreta de Estrategia. Debería
        // trabajar con todas las estrategias a traves de la interfaz Estrategia.
        private IStrategy _strategy;

        public Context()
        { }

        //Usualmente, el Contexto acepta una estrategia a traves del constructor, pero
        // también brinda un setter para cambiarlo en tiempo de ejecución.
        public Context(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Usualmente, el Contexto permite reemplazar un objeto Estrategia en tiempo de ejecución.
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // El Contexto delega algo de trabajo al objeto Estrategia en vez de
        // implementar multiples versiones del algoritmo por si solo.
        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Contexto: Ordenando datos usando la estrategia.");
            var result = this._strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Empty;
            foreach (var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }
    }

    // La interfaz Estrategia declara las operaciones en comun que soporta a todas las 
    // versiones de algúl algoritmo.
    //
    // El Contexto usa esta interfaz para llamar al algortimo definido por concreto.
    // Estrategias.
    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    // Estrategias Concretas implementan el algorigmo mientras siguen la base de la
    // interfaz Estrategia. La interfaz los hace intercambiables en el Contexto.
    class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // El codigo cliente elige una estrategia en concreto y la pasa al
            // contexto. El cliente debe conocer la diferencia entre las estraregias
            // para poder escoger la opcioón correcta.
            var context = new Context();

            Console.WriteLine("Cliente: La Estrategia esta colocada en orden normal.");
            context.SetStrategy(new ConcreteStrategyA());
            context.DoSomeBusinessLogic();
            
            Console.WriteLine();
            
            Console.WriteLine("Cliente: La Estrategia esta colocada en orden reverso.");
            context.SetStrategy(new ConcreteStrategyB());
            context.DoSomeBusinessLogic();
        }
    }
}
```
## Aplicabilidad

- Utiliza el patrón Strategy cuando quieras utiliza distintas variantes de un algoritmo dentro de un objeto y poder cambiar de un algoritmo a otro durante el tiempo de ejecución.<br>
> El patrón Strategy te permite alterar indirectamente el comportamiento del objeto durante el tiempo de ejecución asociándolo con distintos subobjetos que pueden realizar subtareas específicas de distintas maneras.
- Utiliza el patrón Strategy cuando tengas muchas clases similares que sólo se diferencien en la forma en que ejecutan cierto comportamiento.<br>
> El patrón Strategy te permite extraer el comportamiento variante para ponerlo en una jerarquía de clases separada y combinar las clases originales en una, reduciendo con ello el código duplicado.
- Utiliza el patrón para aislar la lógica de negocio de una clase, de los detalles de implementación de algoritmos que pueden no ser tan importantes en el contexto de esa lógica.<br>
> El patrón Strategy te permite aislar el código, los datos internos y las dependencias de varios algoritmos, del resto del código. Los diversos clientes obtienen una interfaz simple para ejecutar los algoritmos y cambiarlos durante el tiempo de ejecución.
- Utiliza el patrón cuando tu clase tenga un enorme operador condicional que cambie entre distintas variantes del mismo algoritmo. <br>
> El patrón Strategy te permite suprimir dicho condicional extrayendo todos los algoritmos para ponerlos en clases separadas, las cuales implementan la misma interfaz. El objeto original delega la ejecución a uno de esos objetos, en lugar de implementar todas las variantes del algoritmo.

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
