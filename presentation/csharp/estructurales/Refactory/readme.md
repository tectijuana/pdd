<pre>

    <p align=center>

Tecnológico Nacional de México
Instituto Tecnológico de Tijuana

Departamento de Sistemas y Computación
Ingeniería en Sistemas Computacionales

Semestre:
Agosto - Diciembre 2024

Materia:
Patrones y Diseño

Docente:
M.C. Rene Solis Reyes 

Unidad:
1

Título del trabajo:
Exposición Refactory

Estudiante:
Marquez Santillan Jose Eduardo 21210395
Díaz Berumen María de los Ángeles 21210368

    </p>

</pre>

# Refactory
## ¿Qué es refactory?
Refactoring es un proceso sistemático de mejora del código.Esto se hace sin crear una nueva funcionalidad que pueda transformar un desastre en cóigo limpio y diseño simple.

<img src="https://static.vecteezy.com/system/resources/previews/031/891/892/original/code-refactoring-icon-vector.jpg" width="200">

## Código Sucio o dirty code
El código sucio es resultado de la inexperiencia multiplicado por plazos ajustables, mala gestión y atajos. Se usa la metáfora "deuda técnica" que fue sugerida originalmente por Ward Cunningham. Puedes acelerar temporalmente sin escribir pruebas para nuevas funciones, pero esto ralentizará gradualmente tu progreso día a día hasta que paguemos la deuda escribiendo pruebas.

<img src="https://cdn.iowacomputergurus.com/blog/the-hidden-security-risks-of-dirty-website-code.jpeg" width="200">


### Causas
Las causas más comunes del dirty code son las siguientes:
- Presión empresarial
- Falta de comprensión de las consecuencias de la deuda técnica
- No poder combatir la estricta coherencia de los componentes
- Falta de pruebas
- Falta de documentación
- Falta de interacción entre los miembros del equipo
- Desarrollo simultáneo a largo plazo en varias ramas
- Refactorización retrasada
- incompetencia
 ## Código limpio o Clean Code
 La refactorización tiene como objetivo principal combatir la deuda técnica. Transforma un desorden en código limpio y diseño simple. 

<img src="https://ih1.redbubble.net/image.912856463.4905/st,small,845x845-pad,1000x1000,f8f8f8.u2.jpg" width="200">


### Características de Clean Code
Estas son algunas de las características del Código Limpio:
- El código limpio es obvio para otros programadores
- No contiene duplicaciones: Cada vez que se debe realizar un cambio en un código duplicado, hay que recordar realizar el mismo cambio en todo. Esto aumenta la carga y se ralentiza el progreso.
- Contiene una cantidad mínima de clases: Menos código significa significa menos mantenimiento.
- Pasa todas las pruebas: Sabes que tu código está sucio cuando solo el 95 % de tus pruebas pasan. Sabes que estás en problemas cuando la cobertura de tus pruebas es del 0 %.


# Proceso de refactorización

<img src="https://www.ionos.mx/digitalguide/fileadmin/_processed_/f/7/csm_refactoring-t_790e2e53f4.webp" width="300">

La refactorización consiste en realizar pequeños cambios para mejorar el código sin alterar su funcionalidad. Sabes que no se ha implementado correctamente cuando el código sigue siendo problemático después del proceso. En estos casos, puede ser necesario reescribir partes del código, siempre y cuando se disponga de pruebas y tiempo suficiente. Es importante no mezclar la refactorización con la implementación de nuevas funcionalidades. Después de refactorizar, las pruebas existentes deben aprobarse; de lo contrario, podrían haber errores en la refactorización o pruebas mal diseñadas.



# Code Smells o el codigo huele mal

<img src="https://www.alpharithms.com/wp-content/uploads/696/code-smell-overcoded.jpg" width="300">


### Hinchazones: Se refiere a métodos o clases demasiado grandes que complican el trabajo.
- Método largo
- Clase grande
- Obsesión primitiva
- Lista larga de parámetros
- Grupos de datos

### Abusadores de la orientación a objetos: Es el uso incorrecto de principios orientados a objetos.
- Clases alternativas con diferentes interfaces
- Legado rechazado
- Declaraciones Switch
- Campo temporal

### Preventores del cambio: Cambios que se realizan en un lugar y requieren una modificacion en otros.
- Cambio divergente
- Jerarquías de herencia paralelas
- Cirugía de escopeta

### Desechables: Es el código innecesario que debe eliminarse para mejorar el código.
- Comentarios
- Código duplicado
- Clase de datos
- Código muerto
- Clase perezosa
- Generalidad especulativa

### Acopladores: Son clases demasiadas acopladas o con delegación excesiva.
- Envidia de características
- Intimidad inapropiada
- Clase de biblioteca incompleta
- Cadenas de mensajes
- Hombre intermedio


# Técnicas de refactorización

<img src="https://xurxodev.com/content/images/2022/11/beneficios-refactoring.png" width="300">

### Métodos de composición: Consiste en simplificar métodos y eliminar duplicación de código.
- Método de extracción
- Extraer variable
- Reemplazar Temp con Query
- Dividir variable temporal

### Mover funciones entre objetos: Consiste en reorganizar la funcionalidad entre clases.
- Método de movimiento
- Extraer clase
- Eliminar intermediarios
- Mover campo
- Ocultar delegado

### Organizando datos: Este consiste en manejar mejor los datos y reducir dependencias.
- Reemplazar número mágico por constante simbólica
- Reemplazar el valor de los datos por un objeto
- Campo encapsulado
- Colección encapsulada
- Cambiar valor a referencia o viseversa
- Campo autoencapsulado
- Reemplazar código de tipo por clase

### Simplificación de expresiones condicionales: Simplemente es reducir la complejidad de condicionales.
- Consolidar la expresión condicional
- Descomponer condicional
- Reemplazar condicional por polimorfismo
- Quitar la bandera de control
- Consolidar fragmentos condicionales duplicados
- Reemplazar cláusulas condicionales anidadas por cláusulas de protección

### implificación de llamadas a métodos: Simplificar y mejorar la claridad de las llamadas a métodos.
- Cambiar nombre de método
- Eliminar parámetro
- Reemplazar código de error por excepción
- Reemplazar parámetro con llamada de método
- Agregar parámetro
- Separar consulta y modificador
- Reemplazar el constructor por método de fábrica

### Cómo lidiar con la generalización: Técnicas para manejar la abstracción y la herencia.
- Extraer subclase
- Extraer superclase
- Extraer interfaz
- Reemplazar la herencia por la delegación
- Método de empujar hacia abajo
- Campo de empuje hacia abajo
- Contraer jerarquía
