# Patrón de Diseño Interpreter

## ¿Qué es el patrón de diseño Interpreter?

El patrón de diseño **Interpreter** es un patrón de diseño comportamental que define una forma de interpretar y evaluar la gramática o las expresiones de un lenguaje. Proporciona un mecanismo para evaluar oraciones en un lenguaje representando su gramática como un conjunto de clases. Cada clase representa una regla o expresión en la gramática, y el patrón permite que estas clases se compongan de manera jerárquica para interpretar expresiones complejas.

El patrón implica definir una jerarquía de clases de expresiones, tanto terminales como no terminales, para representar los elementos de la gramática del lenguaje.

Las expresiones terminales representan los bloques de construcción básicos, mientras que las expresiones no terminales representan composiciones de estos bloques.

La estructura en árbol del patrón de diseño Interpreter es algo similar a la definida por el patrón de diseño Composite, con las expresiones terminales siendo objetos hoja y las expresiones no terminales siendo compuestos.

## Componentes del patrón de diseño Interpreter

1. **Client**: Actor que dispara la ejecución del interpreter.
2. **Context**: Objeto con información global que será utilizada por el intérprete para leer y almacenar información global entre todas las clases que conforman el patrón, este es enviado al interpreter el cual lo replica por toda la estructura.
3. **AbstractExpression**: Interface que define la estructura mínima de una expresión.
4. **TerminalExpression**: Se refiere a expresiones que no tienen más continuidad y al ser evaluadas o interpretadas terminan la ejecución de esa rama. Estas expresiones marcan el final de la ejecución de un sub-árbol de la expresión.
5. **NonTerminalExpression**: Son expresiones compuestas y dentro de ellas existen más expresiones que deben ser evaluadas. Estas estructuras son interpretadas utilizando recursividad hasta llegar a una expresión Terminal.

## Analogia de la vida real del patrón de diseño Interpreter

Imagina que estás viajando a un país extranjero donde no hablas el idioma nativo. En tal escenario, podrías necesitar la ayuda de un intérprete para comunicarte eficazmente con los locales.

Así es como el patrón Interpreter se relaciona con esta situación:

1. **Gramática del idioma**: Al igual que un lenguaje de programación tiene sus propias reglas gramaticales, cada idioma hablado tiene su propia gramática y sintaxis. Por ejemplo, el inglés, el francés o el mandarín tienen sus propias reglas para la estructura de las oraciones, el orden de las palabras y el vocabulario.

2. **Intérprete**: El intérprete en esta analogía es la persona que sirve como intermediario entre tú y los locales. Entienden tanto tu idioma (el idioma de entrada) como el idioma local (el idioma objetivo).

3. **Expresiones**: Tus oraciones o frases habladas son como expresiones en un lenguaje de programación. Representan la información o las instrucciones que deseas transmitir a los locales.

4. **Contexto**: El contexto en esta analogía podría ser el trasfondo cultural o el contexto situacional en el que tiene lugar la comunicación. Este contexto ayuda al intérprete a comprender los matices y sutilezas de la conversación.

5. **Proceso de traducción**: El intérprete escucha tus expresiones habladas, interpreta su significado y luego las traduce al idioma local. Pueden descomponer tus oraciones en unidades más pequeñas (palabras o frases), comprender su significado y luego reformularlas en el idioma objetivo usando la gramática y el vocabulario adecuados.

## Ejemplo en C#

```csharp
using System;
using System.Collections.Generic;

// Interfaz común para todas las expresiones
public interface IExpression
{
    int Interpret();
}

// Clase que representa una expresión numérica (terminal)
public class NumberExpression : IExpression
{
    private int _number;

    public NumberExpression(int number)
    {
        _number = number;
    }

    public int Interpret()
    {
        return _number;
    }
}

// Clase que representa una suma (no terminal)
public class AddExpression : IExpression
{
    private IExpression _leftExpression;
    private IExpression _rightExpression;

    public AddExpression(IExpression leftExpression, IExpression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }

    public int Interpret()
    {
        return _leftExpression.Interpret() + _rightExpression.Interpret();
    }
}

// Clase que representa una resta (no terminal)
public class SubtractExpression : IExpression
{
    private IExpression _leftExpression;
    private IExpression _rightExpression;

    public SubtractExpression(IExpression leftExpression, IExpression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }

    public int Interpret()
    {
        return _leftExpression.Interpret() - _rightExpression.Interpret();
    }
}

// Cliente que utiliza el patrón Interpreter
public class InterpreterClient
{
    public static void Main(string[] args)
    {
        // Interpretar la expresión: (5 + 10) - (8 + 2)

        // Expresiones terminales
        IExpression num1 = new NumberExpression(5);
        IExpression num2 = new NumberExpression(10);
        IExpression num3 = new NumberExpression(8);
        IExpression num4 = new NumberExpression(2);

        // Expresiones no terminales
        IExpression sum1 = new AddExpression(num1, num2); // 5 + 10
        IExpression sum2 = new AddExpression(num3, num4); // 8 + 2

        // Expresión completa: (5 + 10) - (8 + 2)
        IExpression result = new SubtractExpression(sum1, sum2);

        Console.WriteLine("Resultado: " + result.Interpret()); // Resultado: 5
    }
}

```
# Referencias
https://reactiveprogramming.io/blog/es/patrones-de-diseno/interpreter
https://www.geeksforgeeks.org/interpreter-design-pattern/
