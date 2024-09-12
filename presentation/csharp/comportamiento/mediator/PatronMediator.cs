using System;

namespace GestiónDeEventosOficina
{
    // El Mediador (Coordinador de Eventos) declara un método utilizado por los empleados para notificar
    // al coordinador sobre varios eventos. El Coordinador puede reaccionar a estos eventos y
    // pasar la ejecución a otros empleados.
    public interface ICoordinadorDeEventos
    {
        void Notificar(object remitente, string evento);
    }

    // El Coordinador de Eventos implementa el comportamiento cooperativo coordinando varios
    // empleados.
    class CoordinadorDeEventos : ICoordinadorDeEventos
    {
        private Empleado _empleado1;
        private Empleado _empleado2;

        public CoordinadorDeEventos(Empleado empleado1, Empleado empleado2)
        {
            this._empleado1 = empleado1;
            this._empleado1.EstablecerCoordinador(this);
            this._empleado2 = empleado2;
            this._empleado2.EstablecerCoordinador(this);
        }

        public void Notificar(object remitente, string evento)
        {
            if (evento == "Reunión")
            {
                Console.WriteLine("El coordinador reacciona a la Reunión y desencadena las siguientes operaciones:");
                this._empleado2.EnviarInforme();
            }
            if (evento == "Fecha límite")
            {
                Console.WriteLine("El coordinador reacciona a la Fecha límite y desencadena las siguientes operaciones:");
                this._empleado1.CompilarInforme();
                this._empleado2.EnviarInforme();
            }
        }
    }

    // La Clase Base proporciona la funcionalidad básica de almacenar una
    // instancia del coordinador dentro de los objetos empleados.
    class EmpleadoBase
    {
        protected ICoordinadorDeEventos _coordinador;

        public EmpleadoBase(ICoordinadorDeEventos coordinador = null)
        {
            this._coordinador = coordinador;
        }

        public void EstablecerCoordinador(ICoordinadorDeEventos coordinador)
        {
            this._coordinador = coordinador;
        }
    }

    // Empleados concretos implementan varias funcionalidades. No dependen de otros empleados
    // directamente. Tampoco dependen de ninguna clase concreta de coordinador.
    class Empleado : EmpleadoBase
    {
        public string Nombre { get; set; }

        public Empleado(string nombre, ICoordinadorDeEventos coordinador = null) : base(coordinador)
        {
            Nombre = nombre;
        }

        public void PrepararseParaReunion()
        {
            Console.WriteLine($"{Nombre} se está preparando para la reunión.");
            this._coordinador.Notificar(this, "Reunión");
        }

        public void CompilarInforme()
        {
            Console.WriteLine($"{Nombre} está compilando el informe.");
            this._coordinador.Notificar(this, "InformeCompilado");
        }

        public void EnviarInforme()
        {
            Console.WriteLine($"{Nombre} está enviando el informe.");
            this._coordinador.Notificar(this, "InformeEnviado");
        }
    }

    class Programa
    {
        static void Main(string[] args)
        {
            // El código del cliente.
            Empleado empleado1 = new Empleado("Alice");
            Empleado empleado2 = new Empleado("Bob");
            new CoordinadorDeEventos(empleado1, empleado2);

            Console.WriteLine("El cliente desencadena la preparación para la reunión.");
            empleado1.PrepararseParaReunion();

            Console.WriteLine();

            Console.WriteLine("El cliente desencadena la fecha límite.");
            empleado2.CompilarInforme();
        }
    }
}

