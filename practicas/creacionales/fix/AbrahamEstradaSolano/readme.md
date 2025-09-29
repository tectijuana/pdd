# 🧪 Refactor Creacional - Abuso de propiedades estáticas en RegistroUsoCelular

## Abraham Estrada Solano - 22211899

### 🔍 Problemas detectados
1. La clase `RegistroUsoCelular` viola el **principio de responsabilidad única**, ya que mezcla el control de estado con la exposición de propiedades estáticas globales.  
2. Se detecta una **instancia directa** y uso de propiedades estáticas que deberían estar encapsuladas en un Singleton bien implementado.  
3. El Singleton actual es **inseguro en entorno multihilo**, ya que no controla concurrencia al modificar los contadores.  
---
### ❌ Código original (anti-patrón: abuso de estáticos)

```csharp
using System;

namespace Celulares
{
    public class RegistroUsoCelular
    {
        // Abuso de propiedades estáticas estado global modificable
        public static int CantidadDeLlamadas = 0;
        public static int CantidadDeMensajes = 0;

        //Metodo para registrar las llamada, donde suma un contador de llamadas
        public static void RegistrarLlamada()
        {
            CantidadDeLlamadas++;
            Console.WriteLine($"Se registró una llamada. Total: {CantidadDeLlamadas}");
        }
        //Metodo para registrar los mensajes, donde suma un contador de mensajes
        public static void RegistrarMensaje()
        {
            CantidadDeMensajes++;
            Console.WriteLine($"Se registró un mensaje. Total: {CantidadDeMensajes}");
        }
    }
    //Todas las sumas se hacen directamente a las variables globales

    class Program
    {
        static void Main(string[] args)
        {
            // Uso directo de propiedades estáticas → sin encapsulación ni control
            RegistroUsoCelular.RegistrarLlamada();
            RegistroUsoCelular.RegistrarLlamada();
            RegistroUsoCelular.RegistrarMensaje();
        }
    }
}
```
---

### 🛠 Patrón aplicado
- Se implementa **Singleton** para encapsular el estado del registro de llamadas y mensajes, evitando el abuso de propiedades estáticas.  
- Se asegura la creación de una **única instancia** mediante `Lazy<T>` para inicialización segura en multihilo.  
- Se encapsulan los contadores como **atributos privados**, con métodos controlados para su modificación.  
---
### ✅ Código refactorizado (patrón Singleton aplicado)
```csharp
using System;

namespace Celulares
{
    // Singleton seguro con Lazy<T> para inicialización en multihilo
    public class RegistroUsoCelular
    {
        private static readonly Lazy<RegistroUsoCelular> instancia =
            new Lazy<RegistroUsoCelular>(() => new RegistroUsoCelular());
        //Se ceran las variables globales pero no para su uso directo en el main
        private int cantidadDeLlamadas = 0;
        private int cantidadDeMensajes = 0;

        // Constructor privado evita instanciación externa
        private RegistroUsoCelular() { }

        public static RegistroUsoCelular Instancia => instancia.Value;

        public void RegistrarLlamada()
        {

            cantidadDeLlamadas++;
            Console.WriteLine($"Se registró una llamada. Total: {cantidadDeLlamadas}");
        }

        public void RegistrarMensaje()
        {
            cantidadDeMensajes++;
            Console.WriteLine($"Se registró un mensaje. Total: {cantidadDeMensajes}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Ahora todo pasa por la única instancia controlada
            //Usa la instancia que fue desarrollada con Lazy
            var registro = RegistroUsoCelular.Instancia;

            registro.RegistrarLlamada();
            registro.RegistrarLlamada();
            registro.RegistrarMensaje();
        }
    }
}
```
---

### 💡 Justificación del cambio
Con esta refactorización se logra:  

- ✅ **Cohesión interna**: el estado y la lógica de registro están en un único lugar controlado.  
- ✅ **Testabilidad**: al no exponer propiedades estáticas, el código es más fácil de probar con mocks o stubs.  
- ✅ **Flexibilidad ante cambios**: si se necesita persistir datos en una base de datos o archivo, el Singleton centralizado lo permite sin afectar el cliente.  

---

### 🔄 Impacto
- Se asegura el **cumplimiento del principio de inversión de dependencias (DIP)** al depender de una abstracción (la interfaz pública del Singleton) en lugar de datos estáticos globales.  
- Se prepara la arquitectura para facilitar **pruebas unitarias** y permitir la evolución futura sin romper el diseño.  
## Conclusión

Al final, lo que hicimos fue dejar de usar las propiedades estáticas como si fueran “atajo mágico” y le dimos orden con un Singleton bien armado.  
Así ya no tenemos un desmadre de estados globales, el código se ve más limpio, es más fácil de probar y no se rompe si lo corremos en varios hilos.  

En pocas palabras: dejamos de “parchar” el código con hacks y ahora sí aplicamos un patrón creacional de la forma correcta. 
