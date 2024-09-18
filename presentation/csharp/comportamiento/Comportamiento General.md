
# Generalidades

Una de las características principales que diferencia a un **programador de hobby**, que únicamente escribe código de menor escala (aproximadamente cincuenta a cien líneas de código monolítico), de un **programador profesional** es la capacidad de diseñar o mantener un proyecto más sofisticado, donde existen cientos de líneas de código y además es desarrollado por otros programadores.

Para evitar problemas, se utilizan **patrones de comportamiento**, los cuales imponen un estándar al momento de diseñar y mantener código, sin tener que "descifrar arqueológicamente" qué funciones y comportamientos tiene un programa que fue escrito por un solo programador aficionado.

Los patrones de comportamiento, en términos sencillos, definen cómo se comunican los objetos entre sí para mantener el orden. Es necesario utilizar estos patrones de manera concisa para evitar errores de armonización entre las diferentes piezas de código.

## Ejemplos

Entre algunos de estos patrones de comportamiento se encuentran:

- Chain of Responsibility
- Command
- Interpreter
- Iterator
- Mediator
- Memento
- Observer
- State
- Strategy
- Template Method
- Visitor

Todos ellos tienen como objetivo **manipular objetos de manera ordenada y estandarizada**. Cuando no se utiliza alguno de estos patrones, se pueden presentar problemas en los que una función de código modifique la información o estructura de un objeto, lo que puede llevar a tener que reescribir código similar al previamente utilizado.

Una frase muy común entre programadores es: 

> "Cuando escribas código, hazlo de la mejor manera para que la próxima persona tenga buena facilidad de leerlo."

Esto significa que es mejor apegarse a estos patrones para evitar reescribir código, cuya única aportación al proyecto sería añadir complejidad.

