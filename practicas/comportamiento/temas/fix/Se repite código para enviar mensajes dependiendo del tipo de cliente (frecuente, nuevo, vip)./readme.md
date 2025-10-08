## Tema:

Repetición de código al enviar mensajes según tipo de cliente (frecuente, nuevo, VIP).

Montaño Zaragoza Marcos Ulises 

21211998

## Análisis del Problema

El código original presenta **repetición** y **violación del principio de abierto/cerrado (OCP)**, ya que cada nuevo tipo de cliente obliga a modificar el método `EnviarMensaje`.  
Esto genera **acoplamiento fuerte** entre `ServicioMensajeria` y los tipos de cliente, además de baja flexibilidad y mantenibilidad.

## Código Problemático (antes del refactor)
```csharp
using System;

namespace MensajeriaApp
{
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; } // "Frecuente", "Nuevo", "VIP"
    }

    public class ServicioMensajeria
    {
        public void EnviarMensaje(Cliente cliente)
        {
            if (cliente.Tipo == "Frecuente")
            {
                Console.WriteLine($"[Mensaje Frecuente] Gracias por seguir con nosotros, {cliente.Nombre}!");
                Console.WriteLine("Te recordamos revisar nuestras promociones semanales.");
            }
            else if (cliente.Tipo == "Nuevo")
            {
                Console.WriteLine($"[Mensaje Bienvenida] ¡Bienvenido, {cliente.Nombre}!");
                Console.WriteLine("Esperamos que disfrutes de nuestros servicios.");
            }
            else if (cliente.Tipo == "VIP")
            {
                Console.WriteLine($"[Mensaje VIP] Estimado {cliente.Nombre}, su experiencia premium está lista.");
                Console.WriteLine("Contáctese con su asesor personalizado para beneficios exclusivos.");
            }
            else
            {
                Console.WriteLine($"Hola {cliente.Nombre}, gracias por visitarnos.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var servicio = new ServicioMensajeria();

            servicio.EnviarMensaje(new Cliente { Nombre = "Ana", Tipo = "Frecuente" });
            servicio.EnviarMensaje(new Cliente { Nombre = "Luis", Tipo = "Nuevo" });
            servicio.EnviarMensaje(new Cliente { Nombre = "Marta", Tipo = "VIP" });
        }
    }
}

```

---

## Problemas detectados

1. **Duplicación de código** en los bloques condicionales.  
2. **Violación del Principio Abierto/Cerrado (OCP)**: agregar un nuevo tipo de cliente implica modificar el método.  
3. **Fuerte acoplamiento** entre `ServicioMensajeria` y la lógica de los tipos de cliente.  
4. **Dificultad para pruebas unitarias**, ya que la lógica está incrustada directamente.  
5. **Escasa extensibilidad**, no se pueden añadir nuevos tipos sin tocar el código existente.  
6. **Ausencia de polimorfismo** (se usa lógica condicional en lugar de delegación).  
7. **Baja cohesión**: `ServicioMensajeria` realiza más de una responsabilidad.  
8. **No hay inversión de dependencias**: el comportamiento no está abstraído.  
9. **Falta de reutilización del comportamiento común**.  
10. **Dificultad para internacionalización o cambio de formato de mensaje**.

### Intención del patrón
> El patrón **Strategy** define una familia de algoritmos, los encapsula y los hace intercambiables.  
> Permite que el algoritmo varíe independientemente de los clientes que lo usan.

### Justificación
Aplicar **Strategy** es adecuado porque:

- Permite **reemplazar los bloques `if`** con clases específicas para cada tipo de mensaje.  
- Facilita **extender el sistema** sin modificar el código existente (cumpliendo con OCP).  
- Promueve la **inversión de dependencias**, ya que `ServicioMensajeria` dependerá de una abstracción (`IMensajeEstrategia`) y no de implementaciones concretas.  
- Mejora la **cohesión** al separar responsabilidades.  
- Facilita **pruebas unitarias** y la reutilización de estrategias.

---

##  Código Refactorizado

```csharp
using System;
using System.Collections.Generic;

namespace MensajeriaApp
{
    // Estrategia Común
    public interface IMensajeEstrategia
    {
        void Enviar(Cliente cliente);
    }

    // Estrategias Concretas
    public class MensajeFrecuente : IMensajeEstrategia
    {
        public void Enviar(Cliente cliente)
        {
            Console.WriteLine($"[Mensaje Frecuente] Gracias por seguir con nosotros, {cliente.Nombre}!");
            Console.WriteLine("Te recordamos revisar nuestras promociones semanales.");
        }
    }

    public class MensajeNuevo : IMensajeEstrategia
    {
        public void Enviar(Cliente cliente)
        {
            Console.WriteLine($"[Mensaje Bienvenida] ¡Bienvenido, {cliente.Nombre}!");
            Console.WriteLine("Esperamos que disfrutes de nuestros servicios.");
        }
    }

    public class MensajeVIP : IMensajeEstrategia
    {
        public void Enviar(Cliente cliente)
        {
            Console.WriteLine($"[Mensaje VIP] Estimado {cliente.Nombre}, su experiencia premium está lista.");
            Console.WriteLine("Contáctese con su asesor personalizado para beneficios exclusivos.");
        }
    }

    public class MensajeGenerico : IMensajeEstrategia
    {
        public void Enviar(Cliente cliente)
        {
            Console.WriteLine($"Hola {cliente.Nombre}, gracias por visitarnos.");
        }
    }

    // Entidad Cliente
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; } // "Frecuente", "Nuevo", "VIP"
    }

    // Contexto que usa la estrategia
    public class ServicioMensajeria
    {
        private readonly Dictionary<string, IMensajeEstrategia> _estrategias;

        public ServicioMensajeria()
        {
            _estrategias = new Dictionary<string, IMensajeEstrategia>
            {
                { "Frecuente", new MensajeFrecuente() },
                { "Nuevo", new MensajeNuevo() },
                { "VIP", new MensajeVIP() }
            };
        }

        public void EnviarMensaje(Cliente cliente)
        {
            if (!_estrategias.TryGetValue(cliente.Tipo, out var estrategia))
                estrategia = new MensajeGenerico();

            estrategia.Enviar(cliente);
        }
    }

    // Programa Principal
    class Program
    {
        static void Main(string[] args)
        {
            var servicio = new ServicioMensajeria();

            servicio.EnviarMensaje(new Cliente { Nombre = "Ana", Tipo = "Frecuente" });
            servicio.EnviarMensaje(new Cliente { Nombre = "Luis", Tipo = "Nuevo" });
            servicio.EnviarMensaje(new Cliente { Nombre = "Marta", Tipo = "VIP" });
            servicio.EnviarMensaje(new Cliente { Nombre = "Carlos", Tipo = "Desconocido" });
        }
    }
}
```
<img width="1165" height="563" alt="image" src="https://github.com/user-attachments/assets/db2096fd-035a-4eb7-8978-4b1907c52fe0" />

## Beneficios del Refactor

Cumple con OCP: agregar nuevos tipos de clientes no requiere modificar el código existente.

Alta cohesión: cada clase tiene una única responsabilidad.

Bajo acoplamiento: ServicioMensajeria no depende de implementaciones concretas.

Facilidad de prueba y mantenimiento.

Extensible: se pueden añadir estrategias para promociones, idiomas, etc.

Código más legible y limpio.

## Reflexión final

El uso del Patrón Strategy transforma una lógica condicional rígida en un diseño flexible, escalable y orientado a la extensión.
Este enfoque permite que el sistema evolucione sin romper lo existente, alineándose con los principios SOLID y las buenas prácticas de diseño de software.
