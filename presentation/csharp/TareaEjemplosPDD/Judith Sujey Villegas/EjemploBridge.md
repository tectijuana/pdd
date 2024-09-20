# Patrón de Diseño Bridge en C#

## 1. ¿Qué es el Patrón Bridge?

El **Patrón Bridge** es un patrón de diseño estructural que permite separar una abstracción de su implementación, de modo que ambas puedan variar de manera independiente. Este patrón es útil para evitar la creación de grandes jerarquías de clases que combinan diversas implementaciones y abstracciones.

## 2. ¿Cuándo Usar el Patrón Bridge?

Es ideal en situaciones como:
- Cuando se desea evitar una proliferación de subclases que combinan distintas variaciones de abstracción e implementación.
- Cuando se quiere desacoplar la interfaz de un objeto de su implementación, permitiendo que ambos evolucionen de manera independiente.
- Cuando se necesita que un sistema pueda ser extendido con nuevas variantes de implementación sin afectar a las clases existentes.

## 3. Ejemplo del Mundo Real

**Contexto:**
En una aplicación de dibujo, se pueden tener diferentes formas (como círculos y cuadrados) y diferentes estilos de dibujo (como dibujo a mano alzada o digital). En lugar de crear una clase para cada combinación de forma y estilo, se puede utilizar el patrón Bridge para separar estas preocupaciones.

**Solución:**
El patrón Bridge permite que las formas y los estilos de dibujo se desarrollen de forma independiente, facilitando la adición de nuevas formas o estilos sin necesidad de cambiar las clases existentes.

## 4. Implementación en C#

```csharp
using System;

public interface IDrawingAPI
{
    void DrawCircle(double x, double y, double radius);
    void DrawSquare(double x, double y, double side);
}

public class DrawingAPI1 : IDrawingAPI
{
    public void DrawCircle(double x, double y, double radius)
    {
        Console.WriteLine("Dibujo API1: Círculo en (" + x + ", " + y + ") con radio " + radius);
    }

    public void DrawSquare(double x, double y, double side)
    {
        Console.WriteLine("Dibujo API1: Cuadrado en (" + x + ", " + y + ") con lado " + side);
    }
}

public class DrawingAPI2 : IDrawingAPI
{
    public void DrawCircle(double x, double y, double radius)
    {
        Console.WriteLine("Dibujo API2: Círculo en (" + x + ", " + y + ") con radio " + radius);
    }

    public void DrawSquare(double x, double y, double side)
    {
        Console.WriteLine("Dibujo API2: Cuadrado en (" + x + ", " + y + ") con lado " + side);
    }
}

public abstract class Shape
{
    protected IDrawingAPI drawingAPI;

    protected Shape(IDrawingAPI drawingAPI)
    {
        this.drawingAPI = drawingAPI;
    }

    public abstract void Draw();
}

public class Circle : Shape
{
    private double x, y, radius;

    public Circle(double x, double y, double radius, IDrawingAPI drawingAPI)
        : base(drawingAPI)
    {
        this.x = x;
        this.y = y;
        this.radius = radius;
    }

    public override void Draw()
    {
        drawingAPI.DrawCircle(x, y, radius);
    }
}

public class Square : Shape
{
    private double x, y, side;

    public Square(double x, double y, double side, IDrawingAPI drawingAPI)
        : base(drawingAPI)
    {
        this.x = x;
        this.y = y;
        this.side = side;
    }

    public override void Draw()
    {
        drawingAPI.DrawSquare(x, y, side);
    }
}

public class Program
{
    public static void Main()
    {
        Shape circle1 = new Circle(5, 10, 7, new DrawingAPI1());
        Shape square1 = new Square(20, 30, 15, new DrawingAPI1());

        Shape circle2 = new Circle(15, 25, 5, new DrawingAPI2());
        Shape square2 = new Square(35, 45, 10, new DrawingAPI2());

        circle1.Draw();
        square1.Draw();
        circle2.Draw();
        square2.Draw();
    }
}
```

## 5. Explicación del Código

- **Interfaz `IDrawingAPI`**:
    - Define los métodos que las implementaciones de dibujo deben proporcionar, como `DrawCircle` y `DrawSquare`.

- **Clases `DrawingAPI1` y `DrawingAPI2`**:
    - Implementan la interfaz `IDrawingAPI` y proporcionan diferentes formas de dibujar círculos y cuadrados.

- **Clase abstracta `Shape`**:
    - Mantiene una referencia a la interfaz `IDrawingAPI`, lo que permite a las formas usar diferentes implementaciones de dibujo.

- **Clases `Circle` y `Square`**:
    - Heredan de `Shape` y utilizan el método `Draw()` para delegar la tarea de dibujo a la implementación de `IDrawingAPI`.

- **Programa Principal (`Main`)**:
    - Se crean instancias de `Circle` y `Square`, cada una utilizando diferentes implementaciones de dibujo.
    - Se dibujan las formas utilizando sus respectivas APIs de dibujo.

## 6. Beneficios del Patrón Bridge

- **Desacoplamiento**: Permite separar la abstracción de la implementación, lo que facilita la evolución independiente de ambas.
- **Flexibilidad**: Facilita la adición de nuevas implementaciones sin necesidad de modificar las clases existentes.
- **Escalabilidad**: Permite la creación de nuevas combinaciones de abstracciones e implementaciones sin un aumento exponencial en la cantidad de clases.
## Enlace al código en DotNetFiddle:

[Ver código en DotNetFiddle](https://dotnetfiddle.net/2xWThC)
