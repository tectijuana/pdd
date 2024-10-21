## Problema de Código Defectuoso: Sistema de Reservas de Hotel

### Contexto
Un equipo de desarrollo ha implementado un **Sistema de Reservas de Hotel** utilizando C# y .NET 8. Sin embargo, el diseño es **espagueti**, lleno de código duplicado y responsabilidades mal distribuidas. El sistema tiene problemas de mantenimiento y es propenso a errores.

Tu tarea es **reorganizar y mejorar** este sistema aplicando **patrones de diseño GoF** (Gang of Four). Aquí se presenta el **código defectuoso** para que lo puedas analizar y corregir mediante patrones de diseño adecuados.

---

### Código Defectuoso en C#

```csharp
using System;
using System.Collections.Generic;

namespace HotelBooking
{
    public class Hotel
    {
        public string Name { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();

        public void AddRoom(string roomType, double price)
        {
            var room = new Room() { RoomType = roomType, Price = price };
            Rooms.Add(room);
        }

        public void PrintAvailableRooms()
        {
            foreach (var room in Rooms)
            {
                Console.WriteLine($"Room: {room.RoomType}, Price: {room.Price}");
            }
        }
    }

    public class Room
    {
        public string RoomType { get; set; }
        public double Price { get; set; }
    }

    public class Reservation
    {
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void MakeReservation(Hotel hotel, string roomType, DateTime startDate, DateTime endDate)
        {
            var room = hotel.Rooms.Find(r => r.RoomType == roomType);
            if (room != null)
            {
                Hotel = hotel;
                Room = room;
                StartDate = startDate;
                EndDate = endDate;
                Console.WriteLine($"Reservation made for {room.RoomType} from {startDate} to {endDate}.");
            }
            else
            {
                Console.WriteLine("Room not available.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hotel = new Hotel { Name = "Luxury Inn" };
            hotel.AddRoom("Single", 100);
            hotel.AddRoom("Double", 200);

            var reservation = new Reservation();
            reservation.MakeReservation(hotel, "Single", DateTime.Now, DateTime.Now.AddDays(2));

            hotel.PrintAvailableRooms();
        }
    }
}
```

---

### Problemas Detectados

1. **Alta acoplamiento:** La clase `Reservation` depende directamente de `Hotel` y `Room`.
2. **Código duplicado:** El proceso de creación de habitaciones está disperso sin abstracciones.
3. **Responsabilidades mal distribuidas:** La lógica de reservas está incrustada en la clase `Reservation`.
4. **Falta de flexibilidad:** No es posible agregar nuevas funcionalidades, como diferentes tipos de habitaciones sin modificar las clases existentes.

---

### Tarea: Aplicar Patrones GoF para Refactorizar

1. **Factory Method** para encapsular la creación de habitaciones.
2. **Strategy Pattern** para definir diferentes estrategias de reserva (p. ej., reservas estándar vs. VIP).
3. **Singleton** para gestionar la instancia de `Hotel` como recurso global.
4. **Observer Pattern** para notificar cambios en las reservas a otras partes interesadas (como servicios de limpieza).

---

### Caso Documentado en Zenuml

```zenuml
@startuml
actor User
participant "Hotel" as H
participant "Reservation" as R
participant "RoomFactory" as F

User -> H: AddRoom("Single", 100)
H -> F: CreateRoom("Single", 100)
F -> H: Room

User -> R: MakeReservation(hotel, "Single", startDate, endDate)
R -> H: FindRoom("Single")
H -> R: Room
R -> User: Reservation Made
@enduml
```

---

### Código QR del Trabajo

[Genera QR Aquí](https://api.qrserver.com/v1/create-qr-code/?data=Sistema+Hotel+Patrones+GoF+Refactorizar&size=300x300)

---

### Próximos Pasos

- **Refactoriza** este código aplicando los patrones sugeridos.
- **Construye y prueba** la solución refactorizada.
- **Documenta** los cambios realizados y justifica cada patrón aplicado.

---

Este ejercicio busca mejorar la **calidad del código** y **habilitar la escalabilidad** del sistema aplicando patrones de diseño adecuados.

---

Csharp GoF https://github.com/tectijuana/design-patterns-csharp/tree/main
