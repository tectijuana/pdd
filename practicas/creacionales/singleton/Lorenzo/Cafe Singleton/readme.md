<div align="center">
<img width="500" height="500" alt="image" src="https://github.com/user-attachments/assets/c3a906c6-4265-4741-be19-2d34ee5b749a" />
</div>

## ☕ Práctica: Refactorización con Singleton en una Cafetería

### 🔍 Escenario del Mundo Real

La cafetería "Café Singleton" tiene un sistema interno que utiliza una clase para llevar el **registro central de pedidos del día**. Sin embargo, cada vez que un barista toma un pedido, se crea una nueva instancia de `RegistroPedidos`, provocando inconsistencias y pérdidas de información.

### 🎯 Objetivo

Identificar los problemas en el diseño y aplicar el patrón **Singleton** para que toda la cafetería use la **misma instancia compartida del registro de pedidos**.

---

### 🧩 Categoría del patrón: **Creacional**

El patrón **Singleton** pertenece a los patrones **Creacionales**, ya que su objetivo es **controlar la instancia** de un objeto que solo debe existir **una vez en toda la aplicación**.

---

### 📋 Actividades

1. **Detecta los Code Smells** en el diseño inicial:

   * ¿Cuántas instancias se crean?
     Se crean múltiples instancias de RegistroPedidos, una por cada vez que se llama a new RegistroPedidos(). En el ejemplo del archivo, se crean al menos dos instancias, una para registroBarista1 y otra para registroBarista2.
   * ¿Hay duplicidad de pedidos?
     No hay duplicidad de pedidos en la misma instancia, pero cada instancia tiene su propia lista de pedidos, lo que causa una pérdida de información y una inconsistencia en el registro central. Es decir, los pedidos de un barista no se ven reflejados en el registro del otro.
   * ¿Hay acoplamiento innecesario?
     Sí, existe un acoplamiento entre el código que crea las instancias (la clase Program) y la clase RegistroPedidos. La clase Program necesita saber cómo construir la clase RegistroPedidos, lo que podría complicar el código si la lógica de inicialización cambiara.

2. **Refactoriza aplicando el patrón Singleton:**

   * Asegura que `RegistroPedidos` tenga **una sola instancia global y segura**.
   * Implementa la **versión thread-safe** para evitar errores en concurrencia.

3. **Evalúa ventajas y riesgos del Singleton en este caso:**

   * ¿Podrías testear esta clase?
     Testear una clase con el patrón Singleton puede ser complicado. Como la clase controla su propia creación y tiene un punto de acceso global, es difícil reemplazarla con un objeto simulado (un mock) para las pruebas. Esto hace que las pruebas unitarias sean más difíciles de implementar.
   * ¿Qué pasaría si decides reiniciar el conteo de pedidos?
     Si decides reiniciar el conteo de pedidos, tendrías que agregar un método específico en la clase Singleton para limpiar la lista, o bien, asignar una nueva instancia a la variable estática _instancia para borrar el estado anterior. Por ejemplo, podrías crear un método público ReiniciarPedidos().
---

### 🧪 Sugerencias de patrones complementarios:

* **Observer**: si quieres notificar a otros módulos cuando se agrega un pedido.
* **Command**: si cada pedido se representa como un comando a ejecutar.
* **Memento**: si quieres guardar el estado del registro a intervalos.

---
## ☕ Código inicial con errores (sin Singleton)

📁 **Ubicación sugerida:** `/home/ubuntu/cafeteria_singleton/CafeteriaSingleton/Program.cs`

```csharp
using System;
using System.Collections.Generic;

namespace CafeteriaSingleton
{
    public class Pedido
    {
        public string Cliente { get; set; }
        public string Bebida { get; set; }

        public Pedido(string cliente, string bebida)
        {
            Cliente = cliente;
            Bebida = bebida;
        }
    }

    // ❌ Esta clase se instancia cada vez, perdiendo el control central
    public class RegistroPedidos
    {
        private List<Pedido> pedidos = new List<Pedido>();

        public void AgregarPedido(Pedido pedido)
        {
            pedidos.Add(pedido);
            Console.WriteLine($"📝 Pedido agregado: {pedido.Cliente} - {pedido.Bebida}");
        }

        public void MostrarPedidos()
        {
            Console.WriteLine("📋 Pedidos registrados:");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"- {pedido.Cliente}: {pedido.Bebida}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ☠️ Cada barista crea su propio registro
            var registroBarista1 = new RegistroPedidos();
            registroBarista1.AgregarPedido(new Pedido("Ana", "Latte"));

            var registroBarista2 = new RegistroPedidos();
            registroBarista2.AgregarPedido(new Pedido("Luis", "Café Americano"));

            // ❌ No se ven todos los pedidos, cada uno tiene su propia lista
            Console.WriteLine("\nRegistro del barista 1:");
            registroBarista1.MostrarPedidos();

            Console.WriteLine("\nRegistro del barista 2:");
            registroBarista2.MostrarPedidos();

            Console.WriteLine("\n¿Dónde está la lista completa? 🤔");
        }
    }
}
```

---

### 🚨 Code Smells Detectados

* Se crean múltiples instancias de `RegistroPedidos`.
* No hay un punto de acceso único ni control de concurrencia.
* El estado de los pedidos **no se comparte** entre baristas.
* Violación del principio **Single Source of Truth**.

---
## ☕ Código inicial sin errores (con Singleton)
```csharp
using System;
using System.Collections.Generic;

namespace CafeteriaSingleton
{
    public class Pedido
    {
        public string Cliente { get; set; }
        public string Bebida { get; set; }

        public Pedido(string cliente, string bebida)
        {
            Cliente = cliente;
            Bebida = bebida;
        }
    }

    public class RegistroPedidos
    {
        private List<Pedido> pedidos = new List<Pedido>();
        
        private static RegistroPedidos _instancia;
        private static readonly object _candado = new object();
        
        private RegistroPedidos() { }

        public static RegistroPedidos ObtenerInstancia()
        {
            lock (_candado)
            {
                if (_instancia == null)
                    _instancia = new RegistroPedidos();
                return _instancia;
            }
        }
        public void AgregarPedido(Pedido pedido)
        {
            pedidos.Add(pedido);
            Console.WriteLine($"📝 Pedido agregado: {pedido.Cliente} - {pedido.Bebida}");
        }

        public void MostrarPedidos()
        {
            Console.WriteLine("📋 Pedidos registrados:");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"- {pedido.Cliente}: {pedido.Bebida}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var registroBarista1 = RegistroPedidos.ObtenerInstancia();
            registroBarista1.AgregarPedido(new Pedido("Ana", "Latte"));

            var registroBarista2 = RegistroPedidos.ObtenerInstancia();
            registroBarista2.AgregarPedido(new Pedido("Luis", "Café Americano"));

            // ✅ Ahora ambos registros apuntan a la misma lista
            Console.WriteLine("\nRegistro del barista 1:");
            registroBarista1.MostrarPedidos();

            Console.WriteLine("\nRegistro del barista 2:");
            registroBarista2.MostrarPedidos();

            Console.WriteLine("\n¡Ahora la lista completa está disponible! 😉"); 
        }
    }
}
```

### 1️⃣ **Detecta el problema**

Hazte estas preguntas:

* ¿Por qué hay múltiples instancias de `RegistroPedidos`?
* ¿Qué pasa con los pedidos registrados por diferentes baristas?
* ¿Cuál sería la consecuencia si varios hilos acceden a este objeto?

---

### 2️⃣ **Reflexiona**

* ¿Qué ventajas trajo el Singleton?
  La principal ventaja es que garantizó que solo haya una única instancia de RegistroPedidos en toda la aplicación, eliminando el problema de la inconsistencia y asegurando un punto de acceso global y centralizado para todos los baristas. También es una versión segura para entornos multihilo (thread-safe), lo cual es ideal para una cafetería con varios baristas trabajando al mismo tiempo.
* ¿Hubo alguna limitación?
  Sí. Como se mencionó anteriormente, la principal limitación es que el Singleton dificulta las pruebas unitarias, ya que la clase es difícil de aislar del resto del código para probar su comportamiento de manera independiente.
* ¿Sería mejor usar Dependency Injection en lugar de Singleton?
  Para este escenario, el Singleton funciona bien, ya que el registro de pedidos es un recurso global que debe ser compartido. Sin embargo, en aplicaciones más grandes, a menudo se prefiere la Inyección de Dependencias (DI). La DI permite pasar la instancia de una clase a otra, lo que hace que el código sea más modular, más fácil de probar y más flexible, ya que la clase que recibe la dependencia no necesita saber cómo se crea esa dependencia.
