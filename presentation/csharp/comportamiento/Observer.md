# Patron de Diseno Observer
## Definicion del Patron de Diseno Observer
Observer es un patrón de diseño de comportamiento que te permite definir un mecanismo de suscripción para notificar a varios objetos sobre cualquier evento que le suceda al objeto que están observando.

## Estructura del Patron Observer
El objeto que tiene un estado interesante suele denominarse sujeto, pero, como también va a notificar a otros objetos los cambios en su estado, le llamaremos notificador (en ocasiones también llamado publicador). El resto de los objetos que quieren conocer los cambios en el estado del notificador, se denominan suscriptores.

El patrón Observer sugiere que añadas un mecanismo de suscripción a la clase notificadora para que los objetos individuales puedan suscribirse o cancelar su suscripción a un flujo de eventos que proviene de esa notificadora. ¡No temas! No es tan complicado como parece. En realidad, este mecanismo consiste en:<br>
a) Un campo matriz para almacenar una lista de referencias a objetos suscriptores<br>
b) Varios métodos públicos que permiten añadir suscriptores y eliminarlos de esa lista.

![structure-indexed](https://github.com/user-attachments/assets/767013af-9e5a-485a-82a3-7f145fa72234)

1.- El **Notificador** envía eventos de interés a otros objetos. Esos eventos ocurren cuando el notificador cambia su estado o ejecuta algunos comportamientos. Los notificadores contienen una infraestructura de suscripción que permite a nuevos y antiguos suscriptores abandonar la lista.

2.- Cuando sucede un nuevo evento, el notificador recorre la lista de suscripción e invoca el método de notificación declarado en la interfaz suscriptora en cada objeto suscriptor.

3.- La interfaz **Suscriptora** declara la interfaz de notificación. En la mayoría de los casos, consiste en un único método actualizar. El método puede tener varios parámetros que permitan al notificador pasar algunos detalles del evento junto a la actualización.

4.- Los **Suscriptores Concretos** realizan algunas acciones en respuesta a las notificaciones emitidas por el notificador. Todas estas clases deben implementar la misma interfaz de forma que el notificador no esté acoplado a clases concretas.

5.- Normalmente, los suscriptores necesitan cierta información contextual para manejar correctamente la actualización. Por este motivo, a menudo los notificadores pasan cierta información de contexto como argumentos del método de notificación. El notificador puede pasarse a sí mismo como argumento, dejando que los suscriptores extraigan la información necesaria directamente.

6.- El **Cliente** crea objetos tipo notificador y suscriptor por separado y después registra a los suscriptores para las actualizaciones del notificador.

### Ejemplo en el mundo real
![image](https://github.com/user-attachments/assets/c04d0f4f-c99d-4a61-acaa-81fb7edf3423)

## Ejemplo codigo

```c#
using System;
using System.Collections.Generic;

// Interfaz del Observador
public interface IObservador
{
    void Actualizar(string mensaje);
}

// Clase Sujeto
public class Sujeto
{
    private List<IObservador> observadores = new List<IObservador>();
    private string estado;

    public string Estado
    {
        get { return estado; }
        set
        {
            estado = value;
            Notificar();  // Notifica a todos los observadores cuando cambia el estado
        }
    }

    // Método para agregar observadores
    public void AgregarObservador(IObservador observador)
    {
        observadores.Add(observador);
    }

    // Método para eliminar observadores
    public void EliminarObservador(IObservador observador)
    {
        observadores.Remove(observador);
    }

    // Método para notificar a los observadores
    public void Notificar()
    {
        foreach (var observador in observadores)
        {
            observador.Actualizar(estado);
        }
    }
}

// Clase Observador Concreto
public class ObservadorConcreto : IObservador
{
    private string nombre;
    private string estadoObservador;

    public ObservadorConcreto(string nombre)
    {
        this.nombre = nombre;
    }

    // Actualiza el estado del observador con el nuevo estado del sujeto
    public void Actualizar(string mensaje)
    {
        estadoObservador = mensaje;
        Console.WriteLine($"{nombre} ha sido notificado. Nuevo estado: {estadoObservador}");
    }
}

// Clase principal para la prueba
class Programa
{
    static void Main(string[] args)
    {
        // Crear un Sujeto
        Sujeto sujeto = new Sujeto();

        // Crear observadores
        ObservadorConcreto observador1 = new ObservadorConcreto("Observador 1");
        ObservadorConcreto observador2 = new ObservadorConcreto("Observador 2");

        // Agregar observadores al sujeto
        sujeto.AgregarObservador(observador1);
        sujeto.AgregarObservador(observador2);

        // Cambiar el estado del sujeto
        sujeto.Estado = "Estado A";

        // Eliminar un observador
        sujeto.EliminarObservador(observador2);

        // Cambiar el estado nuevamente
        sujeto.Estado = "Estado B";
    }
}
```
### Resultado

Observador 1 ha sido notificado. Nuevo estado: Estado A<br><br>
Observador 2 ha sido notificado. Nuevo estado: Estado A<br><br>
Observador 1 ha sido notificado. Nuevo estado: Estado B


## Aplicabilidad

- Utiliza el patrón Observer cuando los cambios en el estado de un objeto puedan necesitar cambiar otros objetos y el grupo de objetos sea desconocido de antemano o cambie dinámicamente.
> Puedes experimentar este problema a menudo al trabajar con clases de la interfaz gráfica de usuario. Por ejemplo, si creaste clases personalizadas de botón y quieres permitir al cliente colgar código cliente de tus botones para que se active cuando un usuario pulse un botón.<br><br>
> El patrón Observer permite que cualquier objeto que implemente la interfaz suscriptora pueda suscribirse a notificaciones de eventos en objetos notificadores. Puedes añadir el mecanismo de suscripción a tus botones, permitiendo a los clientes acoplar su código personalizado a través de clases suscriptoras personalizadas.
- Utiliza el patrón cuando algunos objetos de tu aplicación deban observar a otros, pero sólo durante un tiempo limitado o en casos específicos.
> La lista de suscripción es dinámica, por lo que los suscriptores pueden unirse o abandonar la lista cuando lo deseen.

## Pros y Contras
### Pros
- Principio de abierto/cerrado. Puedes introducir nuevas clases suscriptoras sin tener que cambiar el código de la notificadora (y viceversa si hay una interfaz notificadora).
- Puedes establecer relaciones entre objetos durante el tiempo de ejecución.

### Contras
- Los suscriptores son notificados en un orden aleatorio.
