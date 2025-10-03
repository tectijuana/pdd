//Patron de diseño Composite
//Johan Herrera
//Ejercicio de viñedo
using System;
using System.Collections.Generic;

namespace BodegaComposite
{
    // Interfaz común (solo las operaciones compartidas)
    public interface IComponente
    {
        void Mostrar(int nivel);
    }

    // Se crea la clase Hoja (planta)
    public class Planta : IComponente
    {
        private string nombre;
        public Planta(string nombre) => this.nombre = nombre;

        public void Mostrar(int nivel)
        {
            Console.WriteLine(new String('-', nivel) + nombre);
        }
    }

    // Se crea la clase Composite (fila de viñedo)
    //Ya aplicado el patron de diseño Composite
    public class Fila : IComponente
    {
        //En Hijos se usa una lista para guardar subcomponentes
        private string nombre;
        private List<IComponente> hijos = new List<IComponente>();
        
        public Fila(string nombre) => this.nombre = nombre;

        // Métodos específicos de los composites donde maneja la jerarquia 
        public void Add(IComponente c) => hijos.Add(c);
        public void Remove(IComponente c) => hijos.Remove(c);

        //Se va imprimir su propio nombre "Mostrar"
        public void Mostrar(int nivel)
        {
            Console.WriteLine(new String('-', nivel) + nombre);
            foreach (var hijo in hijos)
                hijo.Mostrar(nivel + 2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // La fila con varias plantas
            Fila fila1 = new Fila("Fila Norte");
            fila1.Add(new Planta("Malbec"));
            fila1.Add(new Planta("Cabernet"));

            //Se hace otra fila con otras plantas
            Fila fila2 = new Fila("Fila Sur");
            fila2.Add(new Planta("Syrah"));
            fila2.Add(new Planta("Merlot"));

            // Se crea el viñedo que contiene las filas completas
            Fila viñedo = new Fila("Viñedo Principal");
            viñedo.Add(fila1);
            viñedo.Add(fila2);

            // Muestra toda la estructura del viñedo
            viñedo.Mostrar(1);
        }
    }
}
