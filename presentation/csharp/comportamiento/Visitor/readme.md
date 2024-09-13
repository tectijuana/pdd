### SANCHEZ BAEZ LESLI LISET 17212182

# PATRÓN VISITOR

## ¿Qué es el patrón Visitor?

Visitor es un patrón de diseño de comportamiento que permite separar algoritmos de los objetos sobre los que operan y añadir nuevos comportamientos a una jerarquía de clases existente sin alterar el código.

Se utiliza para separar la lógica u operaciones que se pueden realizar sobre una estructura compleja. En ocasiones nos podemos encontrar con estructuras de datos que requieren realizar operaciones sobre ella, pero estas operaciones pueden ser muy variadas e incluso se pueden desarrollar nuevas a medida que la aplicación crece.

A medida que estas operaciones crecen, el número de operaciones que deberá tener la estructura también crecerá haciendo que administrar la estructura sea muy complejo. Por esta razón el patrón de diseño Visitor propone la separación de estas operaciones en clases independientes llamadas Visitantes, las cuales son creadas implementando una interface común y no requiere modificar la estructura inicial para agregar la operación.

## Situaciones donde es aplicable

Cuando necesites realizar una operación sobre todos los elementos de una compleja estructura de objetos (por ejemplo, un árbol de objetos). El patrón Visitor te permite ejecutar una operación sobre un grupo de objetos con diferentes clases, haciendo que un objeto visitante implemente distintas variantes de la misma operación que correspondan a todas las clases objetivo.

Para limpiar la lógica de negocio de comportamientos auxiliares. El patrón te permite hacer que las clases primarias de tu aplicación estén más centradas en sus trabajos principales extrayendo el resto de los comportamientos y poniéndolos dentro de un grupo de clases visitantes.

Cuando un comportamiento sólo tenga sentido en algunas clases de una jerarquía de clases, pero no en otras. Puedes extraer este comportamiento y ponerlo en una clase visitante separada e implementar únicamente aquellos métodos visitantes que acepten objetos de clases relevantes, dejando el resto vacíos.

## Componentes principales

<img src="https://refactoring.guru/images/patterns/diagrams/visitor/structure-es.png">

1- Visitor. La interfaz Visitor declara un grupo de métodos visitantes que pueden tomar elementos concretos de una estructura de objetos como argumentos. Estos métodos pueden tener los mismos nombres si el programa está escrito en un lenguaje que soporte la sobrecarga, pero los tipos de sus parámetros deben ser diferentes.

2- ConcreteVisitor. Cada ConcreteVisitor implementa varias versiones de los mismos comportamientos, personalizadas para las distintas clases de elemento concreto. 

3- Element. La interfaz Element declara un método para “aceptar” visitantes. Este método deberá contar con un parámetro declarado con el tipo de la interfaz visitante.

4- ConcreteElement. Cada ConcreteElement debe implementar el método de aceptación. El propósito de este método es redirigir la llamada al método adecuado del visitante correspondiente a la clase de elemento actual. Piensa que, aunque una clase base de elemento implemente este método, todas las subclases deben sobrescribir este método en sus propias clases e invocar el método adecuado en el objeto visitante. 

5- Client. Componente que interactúa con la estructura (Element) y con el Visitor, éste es responsable de crear los visitantes y enviarlos al elemento para su procesamiento. 

## Diagrama de secuencia

<img src="https://reactiveprogramming.io/_next/image?url=%2Fbooks%2Fpatterns%2Fimg%2Fpatterns-articles%2Fvisitor-sequence.png&w=1920&q=75">

1. El cliente crea la estructura (Element).

2. El cliente crea la instancia de Visitor a utilizar sobre la estructura.

3. El cliente ejecuta el método accept de la estructura y la envía al Visitor.

4. El Element le dice a Visitor con que método lo debe procesar. El Visitor deberá tener un método para cada tipo de clase de la estructura.

5. El Visitor analiza al Element mediante su método visitElement y repite el proceso de ejecutar el método accept sobre los hijos del Element. Nuevamente el Visitor deberá tener un método para procesar cada clase hija de la estructura.

6. El ConcreteElementA le indica al Visitor con qué método debe procesarlo, el cual es visitElementA.

7. El Visitor continúa con los demás hijos de Element y esta vez ejecuta el método accept sobre el ConcreteElementB.

8. El ConcreteElementB le indica al Visitor con qué método debe procesarlo, el cual es visitElementB.

9. Finalmente el Visitor termina la operación sobre la estructura cuando ha recorrido todos los objetos, obteniendo un resultado que es solicitado por el cliente mediante el método getResults (el resultado es opcional ya que existen operaciones que no arrojan resultados).

## Pros y Contras 

### Pros 
- Principio de abierto/cerrado. Puedes introducir un nuevo comportamiento que puede funcionar con objetos de clases diferentes sin cambiar esas clases.

- Principio de responsabilidad única. Puedes tomar varias versiones del mismo comportamiento y ponerlas en la misma clase.

- Un objeto visitante puede acumular cierta información útil mientras trabaja con varios objetos. Esto puede resultar útil cuando quieras atravesar una compleja estructura de objetos, como un árbol de objetos, y aplicar el visitante a cada objeto de esa estructura.

### Contras
- Debes actualizar todos los visitantes cada vez que una clase se añada o elimine de la jerarquía de elementos.

- Los visitantes pueden carecer del acceso necesario a los campos y métodos privados de los elementos con los que se supone que deben trabajar.

## Referencias 

Visitor. (s. f.). https://reactiveprogramming.io/blog/es/patrones-de-diseno/visitor

Visitor. (s. f.-b). https://refactoring.guru/es/design-patterns/visitor


