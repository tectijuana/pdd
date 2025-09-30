## Emmanuel Isai Chavez Hernandez
## No. Control: 23211005
## 24/09/2025

---

# Practica Actividad de Cierre: Refactorizando Patrones Creacionales

---
## Objetivo

Aplicar lo aprendido sobre patrones creacionales (GoF) mediante la detección de code smells y propuestas de refactorización en código realista. Esta actividad simula una revisión de código en un entorno de desarrollo profesional usando Pull Requests.

---
## Codigo Espagueti proporcionado por chat-gpt tema Tienda de Videojuegos (new directamente en el controlador)

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
   
---

## Codigo corregido con Factory Method (items relacionados)

```
using Microsoft.AspNetCore.Mvc;

namespace TiendaVideojuegos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideojuegoController : ControllerBase
    {
        private readonly IVideojuegoFactory _videojuegoFactory;

        public VideojuegoController(IVideojuegoFactory videojuegoFactory)
        {
            _videojuegoFactory = videojuegoFactory;
        }

        [HttpGet("comprar")]
        public string Comprar(string tipo)
        {
            var juego = _videojuegoFactory.CrearVideojuego(tipo);
            if (juego == null)
            {
                return "Ese tipo de videojuego no está disponible.";
            }
            return $"Compraste {juego.Nombre} por ${juego.Precio}";
        }
    }

    // Interfaz para el Factory
    public interface IVideojuegoFactory
    {
        Videojuego CrearVideojuego(string tipo);
    }

    // Implementación concreta del Factory
    public class VideojuegoFactory : IVideojuegoFactory
    {
        public Videojuego CrearVideojuego(string tipo)
        {
            return tipo.ToLower() switch
            {
                "aventura" => new Videojuego("Zelda", 59.99),
                "shooter" => new Videojuego("Call of Duty", 69.99),
                "deportes" => new Videojuego("FIFA 25", 79.99),
                _ => null
            };
        }
    }

    // Clase Videojuego
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

## Soluciones aplicadas

### Problemas detectados

* **Lógica de negocio mezclada en el controlador:** El controlador maneja la creación de objetos, lo que no es su responsabilidad.
* **Acoplamiento fuerte:** Uso directo de `new` para crear instancias dentro del controlador.
* **Cadena de `if/else` difícil de mantener:** La creación de videojuegos según tipo está en múltiples condiciones que dificultan la extensión y mantenimiento.

---

### Patrón aplicado

* **Patrón Factory:** Se crea una fábrica (`VideojuegoFactory`) que encapsula la lógica de creación de objetos `Videojuego`, desacoplando esta responsabilidad del controlador.

---

### Justificación del cambio

* **Separación de responsabilidades:** El controlador solo maneja la interacción, mientras que la fábrica crea objetos.
* **Desacoplamiento:** El controlador no conoce detalles de creación, facilitando cambios futuros.
* **Facilidad de mantenimiento:** Añadir nuevos tipos de videojuegos es más sencillo, modificando solo la fábrica sin tocar el controlador.
* **Código más limpio y legible:** Se elimina la cadena de if/else y el uso directo de `new` dentro del controlador.

---

### Impacto

* **Mejora la escalabilidad:** Permite agregar nuevos tipos de videojuegos sin modificar la lógica del controlador.
* **Reduce el riesgo de errores:** Centraliza la creación de objetos en un solo lugar.
* **Mejora la mantenibilidad:** Código más modular y organizado.

---

### Conclusión

Aplicar el patrón Factory en este caso mejora significativamente la calidad del código al separar responsabilidades, reducir el acoplamiento y hacer la aplicación más fácil de extender y mantener, cumpliendo con buenas prácticas de diseño orientado a objetos.

---
