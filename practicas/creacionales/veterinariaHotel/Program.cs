using System;
using System.Collections.Generic;

namespace ClinicaMascotas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Aquí hay una mezcla de responsabilidades: hotel, veterinaria, facturación...
            var dog = new Mascota("Firulais", "Perro");
            var cat = new Mascota("Mishi", "Gato");

            var servicio = new ServicioVeterinario();
            servicio.RegistrarConsulta(dog.Nombre);
            servicio.RegistrarVacuna(cat.Nombre);

            var hotel = new HotelMascotas();
            hotel.AsignarHabitacion(dog.Nombre);
            hotel.AsignarHabitacion(cat.Nombre);
        }
    }

    public class Mascota
    {
        public string Nombre;
        public string Tipo;

        public Mascota(string nombre, string tipo)
        {
            Nombre = nombre;
            Tipo = tipo;
        }
    }

    public class ServicioVeterinario
    {
        public void RegistrarConsulta(string mascota)
        {
            Console.WriteLine($"Consulta registrada para {mascota}");
        }

        public void RegistrarVacuna(string mascota)
        {
            Console.WriteLine($"Vacuna aplicada a {mascota}");
        }
    }

    public class HotelMascotas
    {
        public void AsignarHabitacion(string mascota)
        {
            Console.WriteLine($"Habitación asignada a {mascota}");
        }
    }
}