using System;
using System.Collections.Generic;

namespace ClinicaMascotas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new MascotaFactory();
            var servicio = new ServicioVeterinario();
            var hotel = new HotelMascotas();

            var dog = factory.Crear("Perro", "Firulais");
            var cat = factory.Crear("Gato", "Mishi");

            servicio.RegistrarConsulta(dog);
            servicio.RegistrarVacuna(dog);
            servicio.RegistrarConsulta(cat);
            servicio.RegistrarVacuna(cat);

            hotel.AsignarHabitacion(dog);
            hotel.AsignarHabitacion(cat);
        }
    }

    public abstract class Mascota
    {
        public string Nombre { get; }
        public string Tipo { get; }

        protected Mascota(string nombre, string tipo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("El tipo es obligatorio");

            Nombre = nombre;
            Tipo = tipo;
        }
    }

    public sealed class Perro : Mascota
    {
        public Perro(string nombre) : base(nombre, "Perro") { }
    }

    public sealed class Gato : Mascota
    {
        public Gato(string nombre) : base(nombre, "Gato") { }
    }

    // Factory Method: punto único de creación por Tipo 
    public interface IMascotaFactory
    {
        Mascota Crear(string tipo, string nombre);
    }

    public sealed class MascotaFactory : IMascotaFactory
    {
        public Mascota Crear(string tipo, string nombre) => tipo switch
        {
            "Perro" => new Perro(nombre),
            "Gato"  => new Gato(nombre),
            _       => throw new NotSupportedException($"Tipo no soportado: {tipo}")
        };
    }

    public class ServicioVeterinario
    {
        public void RegistrarConsulta(Mascota m) =>
            Console.WriteLine($"Consulta registrada para {m.Nombre} ({m.Tipo})");

        public void RegistrarVacuna(Mascota m) =>
            Console.WriteLine($"Vacuna aplicada a {m.Nombre} ({m.Tipo})");
    }

    public class HotelMascotas
    {
        public void AsignarHabitacion(Mascota m) =>
            Console.WriteLine($"Habitación asignada a {m.Nombre} ({m.Tipo})");
    }
}