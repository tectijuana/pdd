## Emmanuel Isai Chavez Hernandez
## No. Control: 23211005
## 24/09/2025

---

# Practica Actividad de Cierre: Refactorizando Patrones Creacionales

---
## Objetivo

Aplicar lo aprendido sobre patrones creacionales (GoF) mediante la detección de code smells y propuestas de refactorización en código realista. Esta actividad simula una revisión de código en un entorno de desarrollo profesional usando Pull Requests.

---
## Codigo Espageti proporcionado por chat-gpt (new directamente en el controlador)

```
Perfecto 🚀. Te dejo un **ejemplo de código espagueti en C# .NET 8** de una pequeña "tienda de videojuegos".
El código está intencionalmente **mal estructurado** con **3 code smells** para que luego puedas aplicar el patrón **Factory Method** y mejorar la arquitectura.

---

## 🎮 Código Espagueti (con code smells)

```csharp
using Microsoft.AspNetCore.Mvc;

namespace TiendaVideojuegos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideojuegoController : ControllerBase
    {
        // CODE SMELL 1: Lógica de negocio mezclada directamente en el controlador
        [HttpGet("comprar")]
        public string Comprar(string tipo)
        {
            // CODE SMELL 2: Uso directo de "new" en el controlador (acoplamiento fuerte)
            if (tipo == "aventura")
            {
                var juego = new Videojuego("Zelda", 59.99);
                return $"Compraste {juego.Nombre} por ${juego.Precio}";
            }
            else if (tipo == "shooter")
            {
                var juego = new Videojuego("Call of Duty", 69.99);
                return $"Compraste {juego.Nombre} por ${juego.Precio}";
            }
            else if (tipo == "deportes")
            {
                var juego = new Videojuego("FIFA 25", 79.99);
                return $"Compraste {juego.Nombre} por ${juego.Precio}";
            }

            // CODE SMELL 3: Cadena de if/else difícil de mantener y extender
            return "Ese tipo de videojuego no está disponible.";
        }
    }

    // Clase simple de Videojuego (sin abstracción ni interfaz)
    public class Videojuego
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }

        public Videojuego(string nombre, double precio)
        {
            Nombre = nombre;
            Precio = precio;
        }
    }
}
```

---

## ⚠️ Code Smells presentes:

1. **Lógica de negocio en el controlador** → rompe el principio de responsabilidad única (SRP).
2. **Uso de `new` directamente en el controlador** → acoplamiento fuerte y poca flexibilidad.
3. **Cadena de `if/else` gigante** → difícil de escalar si se agregan más tipos de videojuegos.
   
```

---

## Codigo corregido Factory Method (items relacionados)

