# Patrón de Diseño Prototype en C#

## 1. ¿Qué es el Patrón Prototype?

El **Patrón Prototype** es un patrón de diseño creacional que permite copiar objetos existentes sin necesidad de hacer la clase a la que pertenecen. Este patrón es útil cuando la creación de un objeto es costosa o compleja y se necesita una forma eficiente de crear nuevas instancias.

## 2. ¿Cuándo Usar el Patrón Prototype?

Es ideal en situaciones como:
- Cuando la creación de un nuevo objeto requiere una operación compleja o costosa.
- Cuando se desea evitar la subclase de una clase y en su lugar se utiliza la clonación de un objeto existente.
- Cuando hay múltiples configuraciones posibles de un objeto y se quiere crear copias con ligeras variaciones.

## 3. Ejemplo del Mundo Real

**Contexto:**
En un sistema de diseño gráfico, puede haber múltiples objetos que representan formas (como círculos, cuadrados, etc.). Crear cada forma desde cero puede ser costoso en términos de recursos. Utilizando el patrón Prototype, se pueden clonar las formas existentes y modificar sus propiedades según sea necesario.

**Solución:**
El patrón Prototype permite crear nuevas instancias de las formas clonando objetos existentes, facilitando así la creación de nuevas configuraciones sin el costo de inicializar cada forma desde cero.

## 4. Implementación en C#

```csharp
using System;

public abstract class Shape
{
    public abstract Shape Clone();
    public abstract void Draw();
}

public class Circle : Shape
{
    public int Radius { get; set; }

    public Circle(int radius)
    {
        Radius = radius;
    }

    public override Shape Clone()
    {
        return new Circle(Radius);
    }

    public override void Draw()
    {
        Console.WriteLine("Dibujando un círculo con radio: " + Radius);
    }
}

public class Square : Shape
{
    public int Side { get; set; }

    public Square(int side)
    {
        Side = side;
    }

    public override Shape Clone()
    {
        return new Square(Side);
    }

    public override void Draw()
    {
        Console.WriteLine("Dibujando un cuadrado con lado: " + Side);
    }
}

public class Program
{
    public static void Main()
    {
        // Crear un círculo y un cuadrado
        Shape circle = new Circle(5);
        Shape square = new Square(4);

        // Clonar las formas
        Shape clonedCircle = circle.Clone();
        Shape clonedSquare = square.Clone();

        // Dibujar las formas originales
        circle.Draw();
        square.Draw();

        // Dibujar las formas clonadas
        clonedCircle.Draw();
        clonedSquare.Draw();
    }
}
```

## 5. Explicación del Código

- **Clase abstracta `Shape`**:
    - Define el método `Clone()` que debe ser implementado por las clases derivadas para permitir la clonación.
    - Define el método `Draw()` que debe ser implementado por las clases derivadas para dibujar la forma.

- **Clases `Circle` y `Square`**:
    - Ambas clases implementan el método `Clone()` para devolver una nueva instancia de sí mismas con las mismas propiedades.
    - Implementan el método `Draw()` para mostrar información sobre la forma.

- **Programa Principal (`Main`)**:
    - Se crean instancias de `Circle` y `Square`.
    - Se clonan ambas formas.
    - Se dibujan las formas originales y sus clones.

## 6. Beneficios del Patrón Prototype

- **Eficiencia**: Permite la creación de nuevos objetos sin tener que inicializarlos completamente desde cero.
- **Flexibilidad**: Facilita la creación de variaciones de un objeto sin depender de una jerarquía de clases compleja.
- **Simplicidad**: Reduce la necesidad de usar constructores complejos y de mantener un gran número de subclases.

##[Ver código en DotNetFiddle](https://dotnetfiddle.net/JnnJxK)
