# Patrón de Diseño Interpreter

## ¿Qué es el patrón de diseño Interpreter?

El patrón de diseño **Interpreter** es un patrón de diseño comportamental que define una forma de interpretar y evaluar la gramática o las expresiones de un lenguaje. Proporciona un mecanismo para evaluar oraciones en un lenguaje representando su gramática como un conjunto de clases. Cada clase representa una regla o expresión en la gramática, y el patrón permite que estas clases se compongan de manera jerárquica para interpretar expresiones complejas.

El patrón implica definir una jerarquía de clases de expresiones, tanto terminales como no terminales, para representar los elementos de la gramática del lenguaje.

Las expresiones terminales representan los bloques de construcción básicos, mientras que las expresiones no terminales representan composiciones de estos bloques.

La estructura en árbol del patrón de diseño Interpreter es algo similar a la definida por el patrón de diseño Composite, con las expresiones terminales siendo objetos hoja y las expresiones no terminales siendo compuestos.

## Componentes del patrón de diseño Interpreter

1. Client:Actor que dispara la ejecución del interpreter.
2. Context:Objeto con información global que será utilizada por el intérprete para leer y almacenar información global entre todas las clases que conforman el patrón, este es enviado al interpreter el cual lo replica por toda la estructura.
3. AbstractExpression:Interface que define la estructura mínima de una expresión.
4. TerminalExpression:Se refiere a expresiones que no tienen más continuidad y al ser evaluadas o interpretadas terminan la ejecución de esa rama. Estas expresiones marcan el final de la ejecución de un sub-árbol de la expresión.
5. NonTerminalExpression:Son expresiones compuestas y dentro de ellas existen más expresiones que deben ser evaluadas. Estas estructuras son interpretadas utilizando recursividad hasta llegar a una expresión Terminal.
