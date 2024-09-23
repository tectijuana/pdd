![cooltext466778189035788](https://github.com/user-attachments/assets/da6f7c79-920a-415d-bd0d-2c6094950417)


### **Estructura Revisada de la Actividad**

#### **Ruleta 1: Categoría de Patrones de Diseño GoF**
- **Creacional**
- **Estructural**
- **Comportamental**

#### **Ruleta 2: Dominio o Área de Aplicación**
- **Transporte**
- **Aviación**
- **Industrial**
- **Salud**
- **Educación**
- **Entretenimiento**
- **Finanzas**
- **Tecnología**
- **Comercio Electrónico**

#### **Ruleta 3: Requisitos o Escenario Específico**
- **Necesidad de Flexibilidad en la Creación de Objetos**
- **Reutilización de Código y Componentes**
- **Comunicación entre Objetos**
- **Gestión de Estados y Ciclos de Vida**
- **Optimización de Recursos**
- **Extensibilidad y Mantenibilidad**
- **Manejo de Eventos y Notificaciones**
- **Encapsulación de Comportamientos**
- **Control de Acceso a Recursos**

### **Desarrollo de la Actividad**

1. **Preparación Previa:**
   - **Descripción de Patrones por Categoría:** Proporciona a los estudiantes una breve descripción de cada patrón de diseño, organizados por las tres categorías de la primera ruleta. Esto les ayudará a identificar rápidamente qué patrones son relevantes según la categoría seleccionada.
   - **Ejemplos de Aplicación:** Incluye ejemplos sencillos de cómo se implementa cada patrón en C#, preferiblemente relacionados con algunos de los dominios o requisitos seleccionados en las ruletas 2 y 3.

2. **Formación de Equipos:**
   - **Tamaño de los Equipos:** Forma equipos pequeños de 2 a 4 estudiantes para fomentar la colaboración.
   - **Roles dentro del Equipo:** Asigna roles como desarrollador, diseñador, y presentador para asegurar que todos participen activamente.

3. **Proceso de la Actividad:**
   - **Paso 1: Giro de las Ruletas**
     - Cada equipo gira las tres ruletas para obtener:
       - **Categoría de Patrón (Ruleta 1)**
       - **Dominio o Área de Aplicación (Ruleta 2)**
       - **Requisito o Escenario Específico (Ruleta 3)**
   
   - **Paso 2: Selección del Patrón de Diseño**
     - Basándose en la **Categoría** obtenida en la Ruleta 1, los estudiantes revisan los patrones disponibles en esa categoría.
     - Consideran el **Dominio** y el **Requisito** para seleccionar el patrón de diseño que mejor se adapte a la situación.
     - **Ejemplo de Selección:**
       - **Categoría:** Comportamental
       - **Dominio:** Transporte
       - **Requisito:** Manejo de Eventos y Notificaciones
       - **Patrón Sugerido:** **Observer**
   
   - **Paso 3: Desarrollo del Producto**
     - En un tiempo limitado (por ejemplo, 30 minutos), los equipos desarrollan una aplicación simple en C# que implemente el patrón seleccionado en el contexto del dominio y requisito obtenidos.
     - **Ejemplos de Productos:**
       - **Observer:** Aplicación de seguimiento de vehículos que notifica a los usuarios sobre el estado en tiempo real.
       - **Factory Method:** Sistema de generación de tickets para diferentes tipos de transporte (avión, tren, autobús).

4. **Presentación y Feedback:**
   - **Presentaciones Breves:** Cada equipo presenta su producto en 2-3 minutos, explicando cómo aplicaron el patrón de diseño seleccionado.
   - **Retroalimentación:** Proporciona feedback constructivo sobre la correcta aplicación del patrón, la funcionalidad del producto, y la creatividad del equipo.

5. **Evaluación:**
   - **Criterios de Evaluación:**
     - Correcta implementación del patrón de diseño.
     - Funcionalidad y usabilidad del producto.
     - Creatividad en la aplicación del patrón al dominio seleccionado.
     - Trabajo en equipo y presentación.
   - **Reconocimientos:** Considera otorgar pequeños premios o reconocimientos para los equipos destacados, incentivando así la participación y el esfuerzo.

### **Sugerencias para las Ruletas**

#### **Ruleta 1: Categoría de Patrones de Diseño GoF**
- **Creacional:** Patrones que se enfocan en la creación de objetos.
- **Estructural:** Patrones que se enfocan en la composición de clases u objetos.
- **Comportamental:** Patrones que se enfocan en la comunicación entre objetos.

#### **Ruleta 2: Dominio o Área de Aplicación**
Incluye una variedad de dominios para que los estudiantes puedan aplicar los patrones en contextos diversos. Algunos ejemplos:
- **Transporte**
- **Aviación**
- **Industrial**
- **Salud**
- **Educación**
- **Entretenimiento**
- **Finanzas**
- **Tecnología**
- **Comercio Electrónico**

#### **Ruleta 3: Requisitos o Escenario Específico**
Esta ruleta ayuda a definir un escenario concreto que guiará la aplicación del patrón. Algunos ejemplos:
- **Necesidad de Flexibilidad en la Creación de Objetos**
- **Reutilización de Código y Componentes**
- **Comunicación entre Objetos**
- **Gestión de Estados y Ciclos de Vida**
- **Optimización de Recursos**
- **Extensibilidad y Mantenibilidad**
- **Manejo de Eventos y Notificaciones**
- **Encapsulación de Comportamientos**
- **Control de Acceso a Recursos**

### **Mapeo de Patrones a Categorías y Requisitos**

Presento una tabla de referencia para los estudiantes que incluye todos los **Patrones de Diseño GoF** divididos por categorías, con sus respectivas descripciones y los problemas o requisitos que resuelven. Esta tabla servirá como una guía en la actividad.

### **Tabla de Referencia de Patrones de Diseño GoF**

| **Categoría**       | **Patrón**              | **Descripción**                                                                                                                                              | **Requisitos/Problemas que Resuelve**                                    |
|---------------------|-------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------|
| **Creacional**      | **Singleton**           | Asegura que una clase tenga una única instancia y proporciona un punto de acceso global a ella.                                                              | Control de acceso a recursos, gestión de instancias únicas                |
| **Creacional**      | **Factory Method**      | Define una interfaz para crear un objeto, pero permite que las subclases decidan qué clase instanciar.                                                       | Flexibilidad en la creación de objetos                                    |
| **Creacional**      | **Abstract Factory**    | Proporciona una interfaz para crear familias de objetos relacionados sin especificar sus clases concretas.                                                   | Creación de familias de objetos relacionados                               |
| **Creacional**      | **Builder**             | Separa la construcción de un objeto complejo de su representación para que el mismo proceso de construcción pueda crear diferentes representaciones.           | Construcción de objetos complejos paso a paso                             |
| **Creacional**      | **Prototype**           | Permite crear nuevos objetos copiando una instancia existente, conocido como prototipo.                                                                        | Creación eficiente de objetos mediante clonación                           |
| **Estructural**     | **Adapter**             | Permite que clases con interfaces incompatibles trabajen juntas mediante un adaptador que traduce las interfaces.                                               | Integración de sistemas con interfaces diferentes                         |
| **Estructural**     | **Bridge**              | Desacopla una abstracción de su implementación para que ambas puedan variar independientemente.                                                               | Separación de abstracción e implementación                                 |
| **Estructural**     | **Composite**           | Compone objetos en estructuras de árbol para representar jerarquías parte-todo.                                                                                   | Tratamiento uniforme de objetos individuales y compuestos                  |
| **Estructural**     | **Decorator**           | Añade responsabilidades adicionales a un objeto de manera dinámica sin alterar su estructura.                                                                   | Extensibilidad y mantenibilidad                                            |
| **Estructural**     | **Facade**              | Proporciona una interfaz simplificada a un conjunto de interfaces en un subsistema, facilitando su uso.                                                          | Simplificación de interacciones con sistemas complejos                     |
| **Estructural**     | **Flyweight**           | Usa el compartimiento para soportar grandes cantidades de objetos de grano fino de manera eficiente en cuanto a memoria.                                          | Optimización de uso de memoria con objetos repetitivos                     |
| **Estructural**     | **Proxy**               | Proporciona un sustituto o marcador de posición de otro objeto para controlar el acceso a él.                                                                    | Control de acceso, carga perezosa, seguridad                                 |
| **Comportamental**  | **Chain of Responsibility** | Evita acoplar el emisor de una solicitud a su receptor, dando a más de un objeto la oportunidad de manejar la solicitud.                                       | Desacoplamiento emisor-receptor de solicitudes                             |
| **Comportamental**  | **Command**             | Encapsula una solicitud como un objeto, permitiendo parametrizar clientes con diferentes solicitudes y soportar operaciones de deshacer.                           | Manejo de solicitudes y operaciones como objetos                            |
| **Comportamental**  | **Interpreter**         | Dada una gramática para un lenguaje, define un intérprete que usa la representación para interpretar oraciones en el lenguaje.                                   | Interpretación de lenguajes específicos                                     |
| **Comportamental**  | **Iterator**            | Proporciona una manera de acceder secuencialmente a los elementos de un objeto agregado sin exponer su representación subyacente.                                 | Acceso secuencial a colecciones                                            |
| **Comportamental**  | **Mediator**            | Define un objeto que encapsula cómo interactúan un conjunto de objetos, promoviendo un acoplamiento débil.                                                       | Simplificación de comunicaciones entre objetos                              |
| **Comportamental**  | **Memento**             | Sin violar la encapsulación, captura y externaliza el estado interno de un objeto para poder restaurarlo más tarde.                                             | Restauración de estados de objetos                                          |
| **Comportamental**  | **Observer**            | Define una dependencia de uno a muchos entre objetos para que cuando uno cambie, los demás sean notificados.                                                    | Manejo de eventos y notificaciones                                         |
| **Comportamental**  | **State**               | Permite a un objeto alterar su comportamiento cuando su estado interno cambia, pareciendo cambiar su clase.                                                     | Gestión de estados y comportamientos cambiantes                              |
| **Comportamental**  | **Strategy**            | Define una familia de algoritmos, encapsula cada uno y los hace intercambiables.                                                                               | Flexibilidad en la implementación de algoritmos                             |
| **Comportamental**  | **Template Method**     | Define el esqueleto de un algoritmo en una operación, dejando algunos pasos a las subclases.                                                                    | Reutilización de la estructura de un algoritmo                              |
| **Comportamental**  | **Visitor**             | Representa una operación a realizar sobre los elementos de una estructura de objetos, permitiendo definir nuevas operaciones sin cambiar las clases de los elementos. | Añadir nuevas operaciones a estructuras existentes sin modificar las clases |

### **Descripción de los Patrones de Diseño GoF**

#### **Patrones Creacionales**

1. **Singleton**
   - **Descripción:** Garantiza que una clase tenga una única instancia y proporciona un punto de acceso global a ella.
   - **Uso Común:** Manejo de conexiones a bases de datos, gestores de configuración.

2. **Factory Method**
   - **Descripción:** Define una interfaz para crear un objeto, pero permite que las subclases decidan qué clase instanciar.
   - **Uso Común:** Frameworks que necesitan ser extensibles por parte de los desarrolladores.

3. **Abstract Factory**
   - **Descripción:** Proporciona una interfaz para crear familias de objetos relacionados sin especificar sus clases concretas.
   - **Uso Común:** Sistemas que deben ser independientes de cómo se crean, componen y representan sus productos.

4. **Builder**
   - **Descripción:** Separa la construcción de un objeto complejo de su representación para que el mismo proceso de construcción pueda crear diferentes representaciones.
   - **Uso Común:** Construcción de objetos con muchos parámetros opcionales.

5. **Prototype**
   - **Descripción:** Permite crear nuevos objetos copiando una instancia existente, conocido como prototipo.
   - **Uso Común:** Clonación de objetos para evitar el costo de creación desde cero.

#### **Patrones Estructurales**

1. **Adapter**
   - **Descripción:** Permite que clases con interfaces incompatibles trabajen juntas mediante un adaptador que traduce las interfaces.
   - **Uso Común:** Integración de librerías de terceros.

2. **Bridge**
   - **Descripción:** Desacopla una abstracción de su implementación para que ambas puedan variar independientemente.
   - **Uso Común:** Sistemas que deben ser extensibles tanto en abstracciones como en implementaciones.

3. **Composite**
   - **Descripción:** Compone objetos en estructuras de árbol para representar jerarquías parte-todo.
   - **Uso Común:** Representación de estructuras jerárquicas como árboles de componentes.

4. **Decorator**
   - **Descripción:** Añade responsabilidades adicionales a un objeto de manera dinámica sin alterar su estructura.
   - **Uso Común:** Añadir funcionalidades a objetos de manera flexible.

5. **Facade**
   - **Descripción:** Proporciona una interfaz simplificada a un conjunto de interfaces en un subsistema, facilitando su uso.
   - **Uso Común:** Simplificación de la interacción con sistemas complejos.

6. **Flyweight**
   - **Descripción:** Usa el compartimiento para soportar grandes cantidades de objetos de grano fino de manera eficiente en cuanto a memoria.
   - **Uso Común:** Manejo de grandes cantidades de objetos similares, como caracteres en un editor de texto.

7. **Proxy**
   - **Descripción:** Proporciona un sustituto o marcador de posición de otro objeto para controlar el acceso a él.
   - **Uso Común:** Control de acceso, carga perezosa, logging.

#### **Patrones Comportamentales**

1. **Chain of Responsibility**
   - **Descripción:** Evita acoplar el emisor de una solicitud a su receptor, dando a más de un objeto la oportunidad de manejar la solicitud.
   - **Uso Común:** Manejo de eventos, procesamiento de solicitudes en cadena.

2. **Command**
   - **Descripción:** Encapsula una solicitud como un objeto, permitiendo parametrizar clientes con diferentes solicitudes y soportar operaciones de deshacer.
   - **Uso Común:** Implementación de acciones como objetos, operaciones deshacer/rehacer.

3. **Interpreter**
   - **Descripción:** Dada una gramática para un lenguaje, define un intérprete que usa la representación para interpretar oraciones en el lenguaje.
   - **Uso Común:** Implementación de lenguajes de dominio específico.

4. **Iterator**
   - **Descripción:** Proporciona una manera de acceder secuencialmente a los elementos de un objeto agregado sin exponer su representación subyacente.
   - **Uso Común:** Recorrido de colecciones.

5. **Mediator**
   - **Descripción:** Define un objeto que encapsula cómo interactúan un conjunto de objetos, promoviendo un acoplamiento débil.
   - **Uso Común:** Coordinación de comunicación entre múltiples objetos.

6. **Memento**
   - **Descripción:** Sin violar la encapsulación, captura y externaliza el estado interno de un objeto para poder restaurarlo más tarde.
   - **Uso Común:** Implementación de operaciones de deshacer.

7. **Observer**
   - **Descripción:** Define una dependencia de uno a muchos entre objetos para que cuando uno cambie, los demás sean notificados.
   - **Uso Común:** Sistemas de eventos, suscripción a cambios de estado.

8. **State**
   - **Descripción:** Permite a un objeto alterar su comportamiento cuando su estado interno cambia, pareciendo cambiar su clase.
   - **Uso Común:** Implementación de máquinas de estados.

9. **Strategy**
   - **Descripción:** Define una familia de algoritmos, encapsula cada uno y los hace intercambiables.
   - **Uso Común:** Selección de algoritmos en tiempo de ejecución.

10. **Template Method**
    - **Descripción:** Define el esqueleto de un algoritmo en una operación, dejando algunos pasos a las subclases.
    - **Uso Común:** Reutilización de la estructura de un algoritmo mientras se permite la personalización de ciertos pasos.

11. **Visitor**
    - **Descripción:** Representa una operación a realizar sobre los elementos de una estructura de objetos, permitiendo definir nuevas operaciones sin cambiar las clases de los elementos.
    - **Uso Común:** Operaciones sobre estructuras complejas de objetos, como árboles.

---

![Screenshot 2024-09-22 at 8 04 49 p m](https://github.com/user-attachments/assets/fc6f1d9d-ff4f-4fd9-969b-db4ff40c8f3d)

### **Cómo Utilizar la Tabla en la Actividad**

https://spinthewheel.io/es/wheels/95AwHjm9khTTUerUHECKcz0xJmU9MA==

1. **Giro de las Ruletas:**
   - **Ruleta 1:** Selecciona la **Categoría** del patrón (Creacional, Estructural, Comportamental).
   - **Ruleta 2:** Selecciona el **Dominio o Área de Aplicación** (Transporte, Aviación, Industrial, Salud, etc.).
   - **Ruleta 3:** Selecciona el **Requisito o Escenario Específico** (Necesidad de Flexibilidad en la Creación de Objetos, Manejo de Eventos y Notificaciones, etc.).

2. **Selección del Patrón:**
   - Basándose en la **Categoría** obtenida en la Ruleta 1, los estudiantes consultarán la tabla para identificar los patrones disponibles en esa categoría.
   - Considerarán el **Dominio** y el **Requisito** para elegir el patrón que mejor se adapte a la situación.

3. **Desarrollo del Producto:**
   - En un tiempo limitado (por ejemplo, 30 minutos), los equipos desarrollarán una aplicación simple en C# que implemente el patrón seleccionado en el contexto del dominio y requisito obtenidos.

4. **Presentación y Feedback:**
   - Cada equipo presentará su producto, explicando cómo aplicaron el patrón de diseño seleccionado.
   - Se proporcionará retroalimentación sobre la correcta aplicación del patrón, la funcionalidad del producto y la creatividad del equipo.

### **Ejemplo de Uso de la Tabla**

**Ejemplo 1:**

- **Categoría:** Comportamental
- **Dominio:** Entretenimiento
- **Requisito:** Manejo de Eventos y Notificaciones
- **Patrón Seleccionado:** **Observer**

**Producto Resultante:**
Crear un sistema de notificaciones para eventos de entretenimiento donde los usuarios pueden suscribirse a eventos específicos y recibir actualizaciones en tiempo real cuando cambia el estado del evento.

**Ejemplo 2:**

- **Categoría:** Creacional
- **Dominio:** Salud
- **Requisito:** Flexibilidad en la Creación de Objetos
- **Patrón Seleccionado:** **Factory Method**

**Producto Resultante:**
Desarrollar una aplicación de gestión de pacientes que utiliza el Factory Method para crear diferentes tipos de registros de pacientes (adultos, niños, emergencias) sin modificar el código existente.

### **Consejos para los Estudiantes**

- **Entender el Problema:** Antes de elegir un patrón, asegúrate de comprender completamente el problema o requisito que estás tratando de resolver.
- **Seleccionar el Patrón Adecuado:** Utiliza la tabla de referencia para identificar qué patrón se alinea mejor con tu situación.
- **Implementación Correcta:** Asegúrate de seguir las mejores prácticas al implementar el patrón para aprovechar sus beneficios completos.
- **Documentación y Comentarios:** Documenta tu código y comenta cómo el patrón está siendo aplicado para facilitar la comprensión y mantenimiento.

### 

1. **Claridad en las Instrucciones:**
   - Asegúrate de explicar claramente cada etapa de la actividad, incluyendo cómo girar las ruletas, cómo interpretar los resultados, y cómo seleccionar e implementar el patrón de diseño.

2. **Recursos Disponibles:**
   - Proporciona a los estudiantes acceso rápido a documentación y ejemplos de patrones de diseño en C#. Puedes compartir enlaces a [Refactoring Guru](https://refactoring.guru/design-patterns) o documentación oficial de [Microsoft C#](https://docs.microsoft.com/es-es/dotnet/csharp/).

3. **Asistencia Durante la Actividad:**
   - Mantente disponible para resolver dudas y proporcionar orientación durante el desarrollo del proyecto. Esto ayudará a mantener a los equipos enfocados y avanzar dentro del tiempo estipulado.

4. **Fomento de la Creatividad:**
   - Anima a los estudiantes a pensar creativamente sobre cómo aplicar los patrones en diferentes contextos. No hay una única manera correcta de implementar un patrón, lo que promueve la innovación y el aprendizaje profundo.

5. **Tiempo de Desarrollo:**
   - Considera ajustar el tiempo de desarrollo según la complejidad esperada del producto. Si es necesario, amplía ligeramente el tiempo o divide la actividad en fases para asegurar que los estudiantes puedan completar un producto funcional.

### **Conclusión**

Esta estructura revisada garantiza que las selecciones de las ruletas guíen a los estudiantes de manera efectiva en la elección del patrón de diseño adecuado, promoviendo una comprensión práctica y aplicada de los patrones de diseño GoF. Al proporcionar una guía clara y recursos adecuados, los estudiantes podrán desarrollar productos ejecutables en el tiempo estipulado, consolidando su aprendizaje de una manera lúdica y colaborativa.

¡Espero que esta propuesta te sea de gran ayuda y que la actividad sea un éxito rotundo!
