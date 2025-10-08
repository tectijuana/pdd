# Alvarado Cardona Antonio 22210279

# Patrón Adapter en C#
## Acoplar directamente dos clases incompatibles sin un adaptador intermedio
Este ejemplo muestra cómo **no** se debe acoplar directamente dos clases incompatibles, y luego cómo refactorizar usando un **Adapter** para desacoplar y mejorar la flexibilidad del código.

---

## Código mal estructurado (sin Adapter)

```csharp
using System;

namespace AdapterEjemploMal
{
    // Clase existente
    public class EnchufeEuropeo
    {
        public void ConectarEuropeo()
        {
            Console.WriteLine("Conectado con enchufe europeo.");
        }
    }

    // Cliente que espera un enchufe americano
    public class DispositivoAmericano
    {
        private EnchufeEuropeo _enchufe; // 🔴 Acoplamiento directo

        public DispositivoAmericano(EnchufeEuropeo enchufe)
        {
            _enchufe = enchufe;
        }

        public void Encender()
        {
            Console.WriteLine("Encendiendo dispositivo americano...");
            // Se intenta usar directamente el enchufe europeo
            _enchufe.ConectarEuropeo(); 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EnchufeEuropeo enchufe = new EnchufeEuropeo();
            DispositivoAmericano dispositivo = new DispositivoAmericano(enchufe);
            dispositivo.Encender();
        }
    }
}
```
## Problemas 
- El DispositivoAmericano está directamente acoplado a EnchufeEuropeo.
- Si cambia el tipo de enchufe, se rompe todo el código.
- No hay separación de responsabilidades.

## Código refactorizado (con Adapter)
``` csharp
using System;

namespace AdapterEjemploBien
{
    // Interfaz objetivo (lo que espera el cliente)
    public interface IEnchufeAmericano
    {
        void ConectarAmericano();
    }

    // Clase existente incompatible
    public class EnchufeEuropeo
    {
        public void ConectarEuropeo()
        {
            Console.WriteLine("Conectado con enchufe europeo.");
        }
    }

    // Adapter que traduce la interfaz
    public class AdaptadorEuropeoAAmericano : IEnchufeAmericano
    {
        private readonly EnchufeEuropeo _enchufeEuropeo;

        public AdaptadorEuropeoAAmericano(EnchufeEuropeo enchufeEuropeo)
        {
            _enchufeEuropeo = enchufeEuropeo;
        }

        public void ConectarAmericano()
        {
            Console.WriteLine("Usando adaptador de europeo a americano...");
            _enchufeEuropeo.ConectarEuropeo();
        }
    }

    // Cliente que espera un enchufe americano
    public class DispositivoAmericano
    {
        private readonly IEnchufeAmericano _enchufe;

        public DispositivoAmericano(IEnchufeAmericano enchufe)
        {
            _enchufe = enchufe;
        }

        public void Encender()
        {
            Console.WriteLine("Encendiendo dispositivo americano...");
            _enchufe.ConectarAmericano();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EnchufeEuropeo enchufeEuropeo = new EnchufeEuropeo();
            IEnchufeAmericano adaptador = new AdaptadorEuropeoAAmericano(enchufeEuropeo);
            DispositivoAmericano dispositivo = new DispositivoAmericano(adaptador);

            dispositivo.Encender();
        }
    }
}
```
## Ventajas del Adapter:

- El cliente (DispositivoAmericano) ahora depende de una abstracción (IEnchufeAmericano), no de una implementación concreta.
- Se pueden reutilizar clases existentes (EnchufeEuropeo) sin modificarlas.
- Favorece la extensibilidad y el principio de inversión de dependencias.
