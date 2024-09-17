using System;

namespace RefactoringGuru.DesignPatterns.Memento.Conceptual
{
    // El Originador contiene un estado importante que puede cambiar con el tiempo.
    // También define un método para guardar el estado dentro de un memento y
    // otro método para restaurar el estado desde él.
    class Originator
    {
        // Para simplificar, el estado del originador se almacena dentro de una
        // única variable.
        private string _state;

        public Originator(string state)
        {
            this._state = state;
            Console.WriteLine("Originador: Mi estado inicial es: " + state);
        }

        // La lógica de negocios del Originador puede afectar su estado interno.
        // Por lo tanto, el cliente debe hacer una copia de seguridad del estado antes de
        // ejecutar métodos de la lógica de negocios mediante el método save().
        public void DoSomething()
        {
            Console.WriteLine("Originador: Estoy haciendo algo importante.");
            this._state = this.GenerateRandomString(30);
            Console.WriteLine($"Originador: y mi estado ha cambiado a: {_state}");
        }

        // Genera una cadena aleatoria de una longitud dada.
        private string GenerateRandomString(int length = 10)
        {
            string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];

                // Pausa para simular un proceso más largo.
                Thread.Sleep(12);

                length--;
            }

            return result;
        }

        // Guarda el estado actual dentro de un memento.
        public IMemento Save()
        {
            return new ConcreteMemento(this._state);
        }

        // Restaura el estado del Originador desde un objeto memento.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Clase de memento desconocida " + memento.ToString());
            }

            this._state = memento.GetState();
            Console.Write($"Originador: Mi estado ha cambiado a: {_state}");
        }
    }

    // La interfaz Memento proporciona una forma de recuperar los metadatos del memento,
    // como la fecha de creación o el nombre. Sin embargo, no expone el estado del Originador.
    public interface IMemento
    {
        string GetName();

        string GetState();

        DateTime GetDate();
    }

    // El Memento Concreto contiene la infraestructura para almacenar el estado del Originador.
    class ConcreteMemento : IMemento
    {
        private string _state;

        private DateTime _date;

        public ConcreteMemento(string state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        // El Originador utiliza este método cuando restaura su estado.
        public string GetState()
        {
            return this._state;
        }
        
        // El resto de los métodos son utilizados por el Cuidador para mostrar los metadatos.
        public string GetName()
        {
            return $"{this._date} / ({this._state.Substring(0, 9)})...";
        }

        public DateTime GetDate()
        {
            return this._date;
        }
    }

    // El Cuidador no depende de la clase Memento Concreto. Por lo tanto, no tiene acceso
    // al estado del originador, que está almacenado dentro del memento.
    // Trabaja con todos los mementos a través de la interfaz base Memento.
    class Caretaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private Originator _originator = null;

        public Caretaker(Originator originator)
        {
            this._originator = originator;
        }

        // Guarda una copia de seguridad del estado actual del Originador.
        public void Backup()
        {
            Console.WriteLine("\nCuidador: Guardando el estado del Originador...");
            this._mementos.Add(this._originator.Save());
        }

        // Restaura el estado anterior del Originador.
        public void Undo()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            Console.WriteLine("Cuidador: Restaurando estado a: " + memento.GetName());

            try
            {
                this._originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }
        }

        // Muestra el historial de mementos guardados.
        public void ShowHistory()
        {
            Console.WriteLine("Cuidador: Aquí está la lista de mementos:");

            foreach (var memento in this._mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Código del cliente.
            Originator originator = new Originator("Super-duper-super-puper-super.");
            Caretaker caretaker = new Caretaker(originator);

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            Console.WriteLine();
            caretaker.ShowHistory();

            Console.WriteLine("\nCliente: ¡Ahora, vamos a deshacer!\n");
            caretaker.Undo();

            Console.WriteLine("\n\nCliente: ¡Una vez más!\n");
            caretaker.Undo();

            Console.WriteLine();
        }
    }
}
