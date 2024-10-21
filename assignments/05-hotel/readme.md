## Problema de Código Defectuoso: Sistema de Reservas de Hotel

### Contexto
Un equipo de desarrollo ha implementado un **Sistema de Reservas de Hotel** utilizando C# y .NET 8. Sin embargo, el diseño es **espagueti**, lleno de código duplicado y responsabilidades mal distribuidas. El sistema tiene problemas de mantenimiento y es propenso a errores.

La misión es **reorganizar y mejorar** este sistema aplicando **patrones de diseño GoF** (Gang of Four). Aquí se presenta el **código defectuoso** para que lo puedas analizar y corregir mediante patrones de diseño adecuados.


        
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

Solución extendida aplicada en AWSACademy en Ubuntu 24 LTS
revisar el Ansible Playbook.

---

### Caso Documentado en UML
![zenuml-3](https://github.com/user-attachments/assets/fabb64a0-fbec-4897-9ff6-42e920956e93)



---

### Código QR del Trabajo

[Genera QR Aquí](https://api.qrserver.com/v1/create-qr-code/?data=Sistema+Hotel+Patrones+GoF+Refactorizar&size=300x300)

---

### Próximos Pasos

- **Refactoriza** este código aplicando los patrones sugeridos.
- **Construye y prueba** la solución refactorizada.
- **Documenta** los cambios realizados y justifica cada patrón aplicado.

SOLID https://es.wikipedia.org/wiki/SOLID
---

Este ejercicio busca mejorar la **calidad del código** y **habilitar la escalabilidad** del sistema aplicando patrones de diseño adecuados.

# Rúbrica de Evaluación para la Práctica de Patrones de Diseño

| **Categoría**                            | **Excelente (25%)**                                                                                                           | **Bueno (20%)**                                                                                                   | **Regular (15%)**                                                                                            | **Deficiente (10%)**                                                                                      |
|------------------------------------------|------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------|
| **Aplicación de Patrones de Diseño GoF** | Implementa **todos** los patrones solicitados: Factory Method, Strategy, Singleton y Observer correctamente y con propósito claro. | Implementa **tres** de los cuatro patrones solicitados. Patrones bien aplicados, aportando mejoras significativas. | Implementa **dos** patrones, aunque con algunos errores menores en su uso. Aporta mejoras limitadas.         | Implementa **uno o ninguno** de los patrones o están mal aplicados. No evidencia comprensión de su uso.    |
| **Calidad y Mantenimiento del Código**   | Código bien estructurado, legible y siguiendo principios SOLID. Elimina duplicación y distribuye responsabilidades correctamente. | Código claro con algunas mejoras posibles. Reduce duplicación y distribuye responsabilidades mejor que el original. | Código con mejoras limitadas en estructura y legibilidad. Algunos problemas de duplicación persisten.         | Código sigue siendo espagueti. No mejora la estructura ni la distribución de responsabilidades.            |
| **Pruebas y Funcionalidad**              | La aplicación compila y funciona sin errores. Implementa pruebas unitarias/integración y nuevas funcionalidades opcionales.   | La aplicación funciona con mínimas fallas. Realiza algunas pruebas para validar la funcionalidad.                 | La aplicación tiene errores menores o funcionalidad incompleta. Las pruebas son insuficientes.                | La aplicación no compila o no funciona. Falta de pruebas o errores críticos que afectan el funcionamiento. |
| **Documentación y Justificación de Cambios** | Documentación detallada y clara. Justifica patrones aplicados con argumentos sólidos y aporta diagramas UML actualizados.      | Documentación adecuada pero con oportunidades de profundización. Diagramas presentes pero mejorables.              | Documentación limitada o incompleta. Justificaciones superficiales. Diagramas ausentes o inadecuados.         | No se entrega documentación o es confusa. No hay justificación clara ni diagramas.                         |

---

**Puntuación Total:** Suma de las puntuaciones obtenidas en cada categoría sobre 100%.


---

Csharp GoF https://github.com/tectijuana/design-patterns-csharp/tree/main
