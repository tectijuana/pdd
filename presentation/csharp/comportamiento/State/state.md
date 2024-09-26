![cooltext465993927895781](https://github.com/user-attachments/assets/4aad7820-2bfa-4345-91c8-37a3f5154827)

## **Es un patrón de diseño de comportamiento que permite a un objeto alterar su comportamiento cuando su estado interno cambia. Parece como si el objeto cambiara su clase.**

### El patrón extrae comportamientos relacionados con el estado, los coloca dentro de clases de estado separadas y fuerza al objeto original a delegar el trabajo de una instancia de esas clases, en lugar de actuar por su cuenta.

### Ejemplo real
Los botones e interruptores de tu smartphone se comportan de forma diferente dependiendo del estado actual del dispositivo:
- Cuando el teléfono está desbloqueado, al pulsar botones se ejecutan varias funciones.
- Cuando el teléfono está bloqueado, pulsar un botón desbloquea la pantalla.
- Cuandolabatería del teléfono está baja, pulsar un botón muestra la pantalla de carga

### Ejemplo de uso: 
El patrón State se utiliza habitualmente en C# para convertir las enormes máquinas de estados basadas en switch, en objetos.

### Identificación:
El patrón State se puede reconocer por métodos que cambian su comportamiento dependiendo del estado del objeto, controlado externamente..

### Uso en la vida real:
Este patrón es útil cuando una clase debe cambiar su comportamiento basado en su estado interno, como por ejemplo:

- Maquinas de estados (como una máquina expendedora que cambia de estado después de cada paso).
- Sistemas de autorización, donde un usuario tiene diferentes permisos dependiendo de si está autenticado o no.
- Juegos donde un personaje cambia de estado (caminando, corriendo, saltando, etc.) y su comportamiento varía en consecuencia.

Este diseño hace que sea fácil agregar nuevos estados sin modificar mucho el código existente, lo que favorece la extensibilidad y mantenibilidad.

### Funcionamiento:
- El cliente crea un objeto Context e inicia con el estado ConcreteStateA.
- Cuando se llama a Request1(), el estado actual maneja la solicitud (en este caso, ConcreteStateA), ejecuta su lógica, y luego cambia el estado del contexto a ConcreteStateB.
- Después, cuando se llama a Request2(), el nuevo estado (ConcreteStateB) maneja la solicitud y luego cambia el estado de vuelta a ConcreteStateA.
- Esto permite que el objeto Context cambie su comportamiento dinámicamente conforme se realizan las transiciones entre estados.

https://dotnetfiddle.net/BacxwN

```csharp
using System;

namespace RefactoringGuru.DesignPatterns.State.Conceptual
{
    // El Contexto define la interfaz de interés para los clientes. También
    // mantiene una referencia a una instancia de una subclase de Estado,
    // que representa el estado actual del Contexto.
    class Context
    {
        // Una referencia al estado actual del Contexto.
        private State _state = null;

        public Context(State state)
        {
            this.TransitionTo(state);
        }

        // El Contexto permite cambiar el objeto Estado en tiempo de ejecución.
        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context: Transición a {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        // El Contexto delega parte de su comportamiento al objeto Estado actual.
        public void Request1()
        {
            this._state.Handle1();
        }

        public void Request2()
        {
            this._state.Handle2();
        }
    }

    // La clase base Estado declara métodos que todos los Estados Concretos deben
    // implementar y también proporciona una referencia al objeto Contexto,
    // asociado con el Estado. Esta referencia puede ser utilizada por los Estados
    // para hacer transiciones del Contexto a otro Estado.
    abstract class State
    {
        protected Context _context;

        public void SetContext(Context context)
        {
            this._context = context;
        }

        public abstract void Handle1();

        public abstract void Handle2();
    }

    // Los Estados Concretos implementan varios comportamientos, asociados con un
    // estado del Contexto.
    class ConcreteStateA : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateA maneja request1.");
            Console.WriteLine("ConcreteStateA quiere cambiar el estado del contexto.");
            this._context.TransitionTo(new ConcreteStateB());
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateA maneja request2.");
        }
    }

    class ConcreteStateB : State
    {
        public override void Handle1()
        {
            Console.Write("ConcreteStateB maneja request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateB maneja request2.");
            Console.WriteLine("ConcreteStateB quiere cambiar el estado del contexto.");
            this._context.TransitionTo(new ConcreteStateA());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // El código del cliente.
            var context = new Context(new ConcreteStateA());
            context.Request1();
            context.Request2();
        }
    }
}
