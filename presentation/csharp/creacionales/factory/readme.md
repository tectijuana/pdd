# Propósito
Factory Method es un patrón de diseño creacional que proporciona una interfaz para crear objetos en una superclase, mientras permite a las subclases alterar el tipo de objetos que se crearán.

# Patrón Factory Method

## Problema
Imagina que estás creando una aplicación de gestión logística. La primera versión de tu aplicación sólo es capaz de manejar el transporte en camión, por lo que la mayor parte de tu código se encuentra dentro de la clase **Camión**.

Al cabo de un tiempo, tu aplicación se vuelve bastante popular. Cada día recibes decenas de peticiones de empresas de transporte marítimo para que incorpores la logística por mar a la aplicación.



### Añadir una nueva clase de transporte al programa provoca un problema
Añadir una nueva clase al programa no es tan sencillo si el resto del código ya está acoplado a clases existentes.

Estupendo, ¿verdad? Pero, ¿qué pasa con el código? En este momento, la mayor parte de tu código está acoplado a la clase **Camión**. Para añadir barcos a la aplicación habría que hacer cambios en toda la base del código. Además, si más tarde decides añadir otro tipo de transporte a la aplicación, probablemente tendrás que volver a hacer todos estos cambios.

Al final acabarás con un código bastante sucio, plagado de condicionales que cambian el comportamiento de la aplicación dependiendo de la clase de los objetos de transporte.


## Solución
El patrón **Factory Method** sugiere que, en lugar de llamar al operador `new` para construir objetos directamente, se invoque a un método fábrica especial. No te preocupes: los objetos se siguen creando a través del operador `new`, pero se invocan desde el método fábrica. Los objetos devueltos por el método fábrica a menudo se denominan _productos_.
![image](https://github.com/user-attachments/assets/1a3491e2-cf76-4498-b4f6-2618567b87f8)
### La estructura de las clases creadoras
Las subclases pueden alterar la clase de los objetos devueltos por el método fábrica.

A simple vista, puede parecer que este cambio no tiene sentido, ya que tan solo hemos cambiado el lugar desde donde invocamos al constructor. Sin embargo, piensa en esto: ahora puedes sobrescribir el método fábrica en una subclase y cambiar la clase de los productos creados por el método.

No obstante, hay una pequeña limitación: las subclases sólo pueden devolver productos de distintos tipos si dichos productos tienen una clase base o interfaz común. Además, el método fábrica en la clase base debe tener su tipo de retorno declarado como dicha interfaz.
![image](https://github.com/user-attachments/assets/d2e158e1-454d-48de-ac39-766a3e069f7c)

### La estructura de la jerarquía de productos
Todos los productos deben seguir la misma interfaz.

Por ejemplo, tanto la clase **Camión** como la clase **Barco** deben implementar la interfaz **Transporte**, que declara un método llamado `entrega`. Cada clase implementa este método de forma diferente: los camiones entregan su carga por tierra, mientras que los barcos lo hacen por mar. El método fábrica dentro de la clase **LogísticaTerrestre** devuelve objetos de tipo camión, mientras que el método fábrica de la clase **LogísticaMarítima** devuelve barcos.

### La estructura del código tras aplicar el patrón Factory Method
Siempre y cuando todas las clases de producto implementen una interfaz común, podrás pasar sus objetos al código cliente sin descomponerlo.

El código que utiliza el método fábrica (a menudo denominado _código cliente_) no encuentra diferencias entre los productos devueltos por varias subclases, y trata a todos los productos como la clase abstracta **Transporte**. El cliente sabe que todos los objetos de transporte deben tener el método `entrega`, pero no necesita saber cómo funciona exactamente.
![image](https://github.com/user-attachments/assets/fdca1baa-f4c1-4e1e-a618-5edf210eb153)

# Aplicabilidad

- Utiliza el **Método Fábrica** cuando no conozcas de antemano las dependencias y los tipos exactos de los objetos con los que deba funcionar tu código.

  El patrón **Factory Method** separa el código de construcción de producto del código que hace uso del producto. Por ello, es más fácil extender el código de construcción de producto de forma independiente al resto del código.

  Por ejemplo, para añadir un nuevo tipo de producto a la aplicación, sólo tendrás que crear una nueva subclase creadora y sobrescribir el Factory Method que contiene.

- Utiliza el **Factory Method** cuando quieras ofrecer a los usuarios de tu biblioteca o framework una forma de extender sus componentes internos.

  La herencia es probablemente la forma más sencilla de extender el comportamiento por defecto de una biblioteca o un framework. Pero, ¿cómo reconoce el framework si debe utilizar tu subclase en lugar de un componente estándar?

  La solución es reducir el código que construye componentes en todo el framework a un único patrón **Factory Method** y permitir que cualquiera sobrescriba este método además de extender el propio componente.

  Veamos cómo funcionaría. Imagina que escribes una aplicación utilizando un framework de UI de código abierto. Tu aplicación debe tener botones redondos, pero el framework sólo proporciona botones cuadrados. Extiendes la clase estándar `Botón` con una subclase `BotónRedondo`, pero ahora tienes que decirle a la clase principal `FrameworkUI` que utilice la nueva subclase de botón en lugar de la clase por defecto.

  Para conseguirlo, creamos una subclase `UIConBotonesRedondos` a partir de una clase base del framework y sobrescribimos su método `crearBotón`. Si bien este método devuelve objetos `Botón` en la clase base, haces que tu subclase devuelva objetos `BotónRedondo`. Ahora, utiliza la clase `UIConBotonesRedondos` en lugar de `FrameworkUI`. ¡Eso es todo!

- Utiliza el **Factory Method** cuando quieras ahorrar recursos del sistema mediante la reutilización de objetos existentes en lugar de reconstruirlos cada vez.

  A menudo experimentas esta necesidad cuando trabajas con objetos grandes y que consumen muchos recursos, como conexiones de bases de datos, sistemas de archivos y recursos de red.

  Pensemos en lo que hay que hacer para reutilizar un objeto existente:

  1. Primero, debemos crear un almacenamiento para llevar un registro de todos los objetos creados.
  2. Cuando alguien necesite un objeto, el programa deberá buscar un objeto disponible dentro de ese agrupamiento y devolverlo al código cliente.
  3. Si no hay objetos disponibles, el programa deberá crear uno nuevo (y añadirlo al agrupamiento).

  ¡Eso es mucho código! Y hay que ponerlo todo en un mismo sitio para no contaminar el programa con código duplicado.

  Es probable que el lugar más evidente y cómodo para colocar este código sea el constructor de la clase cuyos objetos intentamos reutilizar. Sin embargo, un constructor siempre debe devolver nuevos objetos por definición, no puede devolver instancias existentes.

  Por lo tanto, necesitas un método regular capaz de crear nuevos objetos, además de reutilizar los existentes. Eso suena bastante a lo que hace un patrón **Factory Method**.

# Cómo implementarlo

1. Haz que todos los productos sigan la misma interfaz. Esta interfaz deberá declarar métodos que tengan sentido en todos los productos.
2. Añade un patrón **Factory Method** vacío dentro de la clase creadora. El tipo de retorno del método deberá coincidir con la interfaz común de los productos.
3. Encuentra todas las referencias a constructores de producto en el código de la clase creadora. Una a una, sustitúyelas por invocaciones al **Factory Method**, mientras extraes el código de creación de productos para colocarlo dentro del **Factory Method**. 
4. Crea un grupo de subclases creadoras para cada tipo de producto enumerado en el **Factory Method**. Sobrescribe el **Factory Method** en las subclases y extrae las partes adecuadas de código constructor del método base.
5. Si hay demasiados tipos de producto y no tiene sentido crear subclases para todos ellos, puedes reutilizar el parámetro de control de la clase base en las subclases.
6. Si, tras todas las extracciones, el **Factory Method** base queda vacío, puedes hacerlo abstracto. Si queda algo dentro, puedes convertirlo en un comportamiento por defecto del método.

# Pros y contras

- Evitas un acoplamiento fuerte entre el creador y los productos concretos.
- **Principio de responsabilidad única**: puedes mover el código de creación de producto a un lugar del programa, haciendo que el código sea más fácil de mantener.
- **Principio de abierto/cerrado**: puedes incorporar nuevos tipos de productos en el programa sin descomponer el código cliente existente.
- Puede ser que el código se complique, ya que debes incorporar una multitud de nuevas subclases para implementar el patrón.

# Relaciones con otros patrones

- Muchos diseños empiezan utilizando el **Factory Method** (menos complicado y más personalizable mediante las subclases) y evolucionan hacia **Abstract Factory**, **Prototype** o **Builder** (más flexibles, pero más complicados).
- Las clases del **Abstract Factory** a menudo se basan en un grupo de métodos de fábrica, pero también puedes utilizar **Prototype** para escribir los métodos de estas clases.
- Puedes utilizar el patrón **Factory Method** junto con el **Iterator** para permitir que las subclases de la colección devuelvan distintos tipos de iteradores que sean compatibles con las colecciones.
- **Prototype** no se basa en la herencia, por lo que no presenta sus inconvenientes. No obstante, **Prototype** requiere de una inicialización complicada del objeto clonado. **Factory Method** se basa en la herencia, pero no requiere de un paso de inicialización.
- **Factory Method** es una especialización del **Template Method**. Al mismo tiempo, un **Factory Method** puede servir como paso de un gran **Template Method**.

# Ejemplo en c#

Imaginemos que tenemos una aplicación que trabaja con diferentes tipos de notificaciones, como SMS y correos electrónicos. Usaremos el Factory Method para crear estas notificaciones.

``` csharp

using System;

namespace PatronFactoryMethod
{
    // Paso 1: Definir la interfaz del producto que es la notificación
    public interface INotificacion
    {
        // Método que deben implementar todas las notificaciones
        void EnviarNotificacion();
    }

    // Paso 2: Implementar clases concretas que representan diferentes tipos de notificaciones

    // Implementación concreta de notificación por SMS
    public class SmsNotificacion : INotificacion
    {
        public void EnviarNotificacion()
        {
            Console.WriteLine("Enviando notificación por SMS.");
        }
    }

    // Implementación concreta de notificación por correo electrónico
    public class EmailNotificacion : INotificacion
    {
        public void EnviarNotificacion()
        {
            Console.WriteLine("Enviando notificación por correo electrónico.");
        }
    }

    // Paso 3: Crear la clase abstracta Factory que define el método Factory

    // Clase abstracta que declara el método Factory para crear objetos de tipo INotificacion
    public abstract class NotificacionFactory
    {
        // Método abstracto que debe ser implementado por las fábricas concretas
        public abstract INotificacion CrearNotificacion();
    }

    // Paso 4: Crear las fábricas concretas que implementan el Factory Method

    // Fábrica concreta que crea una instancia de SmsNotificacion
    public class SmsNotificacionFactory : NotificacionFactory
    {
        public override INotificacion CrearNotificacion()
        {
            return new SmsNotificacion();
        }
    }

    // Fábrica concreta que crea una instancia de EmailNotificacion
    public class EmailNotificacionFactory : NotificacionFactory
    {
        public override INotificacion CrearNotificacion()
        {
            return new EmailNotificacion();
        }
    }

    // Paso 5: Usar el patrón Factory Method en el código cliente

    class Program
    {
        static void Main(string[] args)
        {
            // Crear una fábrica para notificaciones SMS
            NotificacionFactory smsFactory = new SmsNotificacionFactory();
            // Usar la fábrica para crear una instancia de SmsNotificacion
            INotificacion smsNotificacion = smsFactory.CrearNotificacion();
            // Enviar la notificación por SMS
            smsNotificacion.EnviarNotificacion();

            // Crear una fábrica para notificaciones por correo electrónico
            NotificacionFactory emailFactory = new EmailNotificacionFactory();
            // Usar la fábrica para crear una instancia de EmailNotificacion
            INotificacion emailNotificacion = emailFactory.CrearNotificacion();
            // Enviar la notificación por correo electrónico
            emailNotificacion.EnviarNotificacion();
        }
    }

}

```
# Ejecución
![image](https://github.com/user-attachments/assets/f5396326-364f-4269-a7e6-eac3ed318b80)

# Explicación

- INotificacion es la interfaz que define el contrato para las notificaciones.
- SmsNotificacion y EmailNotificacion son implementaciones concretas de esa interfaz.
- NotificacionFactory es la clase abstracta que define el método CrearNotificacion(), el cual es implementado por las subclases concretas.
- SmsNotificacionFactory y EmailNotificacionFactory son las fábricas concretas que crean instancias de notificaciones específicas.
Cuando se ejecuta el programa, según la fábrica que uses, se creará y enviará una notificación por SMS o correo electrónico, lo que te permite separar la lógica de creación del uso del objeto.


