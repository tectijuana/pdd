# Patrón de Diseño Interpreter

## ¿Qué es el patrón de diseño Interpreter?

El patrón de diseño **Interpreter** es un patrón de diseño comportamental que define una forma de interpretar y evaluar la gramática o las expresiones de un lenguaje. Proporciona un mecanismo para evaluar oraciones en un lenguaje representando su gramática como un conjunto de clases. Cada clase representa una regla o expresión en la gramática, y el patrón permite que estas clases se compongan de manera jerárquica para interpretar expresiones complejas.

El patrón implica definir una jerarquía de clases de expresiones, tanto terminales como no terminales, para representar los elementos de la gramática del lenguaje.
Las expresiones terminales representan los bloques de construcción básicos, mientras que las expresiones no terminales representan composiciones de estos bloques.
La estructura en árbol del patrón de diseño Interpreter es algo similar a la definida por el patrón de diseño Composite, con las expresiones terminales siendo objetos hoja y las expresiones no terminales siendo compuestos.


