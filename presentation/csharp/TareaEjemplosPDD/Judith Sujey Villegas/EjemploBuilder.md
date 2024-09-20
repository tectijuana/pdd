# Patrón de Diseño Builder en C#

## 1. ¿Qué es el Patrón Builder?

El **Patrón Builder** es un patrón de diseño creacional que permite construir objetos complejos paso a paso. En lugar de tener un único constructor con muchos parámetros, el patrón Builder desglosa la creación en pasos, proporcionando un mayor control sobre el proceso de construcción de un objeto.

## 2. ¿Cuándo Usar el Patrón Builder?

Es ideal cuando:
- Se necesitan crear objetos complejos con muchos parámetros opcionales o configurables.
- Se quiere separar la lógica de construcción de un objeto de su representación.
- El proceso de creación de un objeto involucra varios pasos que podrían necesitar ser controlados.

## 3. Ejemplo del Mundo Real

**Contexto:**
En un sistema de gestión de restaurantes, al configurar el menú de platos, puede ser útil construir los platos paso a paso. Por ejemplo, construir un objeto "Plato" podría implicar especificar el nombre del plato, los ingredientes, el precio y si tiene opciones adicionales como salsa o bebida.

**Solución:**
El patrón Builder permite construir el objeto **Plato** paso a paso, facilitando la personalización del pedido sin la necesidad de un constructor enorme con múltiples parámetros.

## 4. Implementación en C#

```csharp
using System;
using System.Collections.Generic;

public class Dish
{
    // Propiedades del Plato
    public string Name { get; private set; }
    public List<string> Ingredients { get; private set; }
    public double Price { get; private set; }
    public bool HasDrink { get; private set; }
    public bool HasSauce { get; private set; }

    // Constructor privado para que solo el Builder pueda crear una instancia
    private Dish() { }

    // Clase interna Builder
    public class Builder
    {
        private readonly Dish _dish = new Dish();

        // Método para establecer el nombre
        public Builder SetName(string name)
        {
            _dish.Name = name;
            return this;
        }

        // Método para agregar ingredientes
        public Builder AddIngredient(string ingredient)
        {
            if (_dish.Ingredients == null)
            {
                _dish.Ingredients = new List<string>();
            }
            _dish.Ingredients.Add(ingredient);
            return this;
        }

        // Método para establecer el precio
        public Builder SetPrice(double price)
        {
            _dish.Price = price;
            return this;
        }

        // Método para indicar si tiene bebida
        public Builder IncludeDrink(bool hasDrink)
        {
            _dish.HasDrink = hasDrink;
            return this;
        }

        // Método para indicar si tiene salsa
        public Builder IncludeSauce(bool hasSauce)
        {
            _dish.HasSauce = hasSauce;
            return this;
        }

        // Método que devuelve el objeto construido
        public Dish Build()
        {
            return _dish;
        }
    }
}

public class Program
{
    public static void Main()
    {
        // Construcción de un plato utilizando el Builder
        Dish dish = new Dish.Builder()
                            .SetName("Hamburguesa")
                            .AddIngredient("Pan")
                            .AddIngredient("Carne")
                            .AddIngredient("Queso")
                            .SetPrice(9.99)
                            .IncludeDrink(true)
                            .IncludeSauce(false)
                            .Build();
        
        // Mostrar detalles del plato
        Console.WriteLine("Plato: " + dish.Name);
        Console.WriteLine("Ingredientes: " + string.Join(", ", dish.Ingredients));
        Console.WriteLine("Precio: $" + dish.Price);
        Console.WriteLine("Incluye bebida: " + (dish.HasDrink ? "Sí" : "No"));
        Console.WriteLine("Incluye salsa: " + (dish.HasSauce ? "No" : "Sí"));
    }
}
```

## 5. Explicación del Código

- **Clase `Dish`**:
    - Tiene varias propiedades como el nombre del plato, los ingredientes, el precio, si incluye bebida y si tiene salsa.
    - El constructor es privado, de modo que solo el **Builder** puede crear instancias de esta clase.
    - La clase `Builder` proporciona métodos para configurar los atributos del objeto `Dish` paso a paso. Cada método devuelve el mismo objeto **Builder**, permitiendo una construcción encadenada (fluent interface).
    - El método `Build()` finalmente devuelve el objeto completamente construido.

- **Programa Principal (`Main`)**:
    - Se utiliza el **Builder** para crear una instancia de `Dish`, añadiendo los ingredientes, configurando el precio y especificando si incluye bebida o salsa.
    - Se muestran los detalles del plato creado, como el nombre, ingredientes y si incluye bebida o salsa.

## 6. Beneficios del Patrón Builder

- **Modularidad**: Permite construir objetos paso a paso de forma modular y clara.
- **Flexibilidad**: Facilita la creación de objetos complejos con múltiples configuraciones sin necesidad de un constructor largo y difícil de manejar.
- **Legibilidad**: El uso de un Builder permite que el código sea más legible, ya que se entiende claramente qué atributos se están configurando.

## [Ver código en DotNetFiddle](https://dotnetfiddle.net/kyGVkw)
