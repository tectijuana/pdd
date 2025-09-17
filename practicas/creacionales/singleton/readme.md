

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
   * ¿Hay duplicidad de pedidos?
   * ¿Hay acoplamiento innecesario?

2. **Refactoriza aplicando el patrón Singleton:**

   * Asegura que `RegistroPedidos` tenga **una sola instancia global y segura**.
   * Implementa la **versión thread-safe** para evitar errores en concurrencia.

3. **Evalúa ventajas y riesgos del Singleton en este caso:**

   * ¿Podrías testear esta clase?
   * ¿Qué pasaría si decides reiniciar el conteo de pedidos?

---

### 🧪 Sugerencias de patrones complementarios:

* **Observer**: si quieres notificar a otros módulos cuando se agrega un pedido.
* **Command**: si cada pedido se representa como un comando a ejecutar.
* **Memento**: si quieres guardar el estado del registro a intervalos.

---

### 🧾 Ansible Playbook de la práctica

```yaml
---
- name: Instalar .NET 8 y desplegar simulador de cafetería con Singleton
  hosts: localhost
  become: yes
  tasks:
    - name: Instalar dotnet-sdk-8.0
      apt:
        name: dotnet-sdk-8.0
        state: present

    - name: Crear carpeta de la práctica
      file:
        path: /home/ubuntu/cafeteria_singleton
        state: directory

    - name: Crear proyecto
      command: dotnet new console -n CafeteriaSingleton
      args:
        chdir: /home/ubuntu/cafeteria_singleton

    - name: Copiar código con errores de diseño
      copy:
        dest: /home/ubuntu/cafeteria_singleton/CafeteriaSingleton/Program.cs
        content: |
          // Código con múltiples instancias de RegistroPedidos
          // TU MISIÓN: Detectar, refactorizar, aplicar Singleton

    - name: Ejecutar el código para observar problemas
      command: dotnet run
      args:
        chdir: /home/ubuntu/cafeteria_singleton/CafeteriaSingleton
```

---

☕ Código inicial con errores (sin Singleton)

Aquí tienes el **código inicial defectuoso** del sistema de pedidos de una cafetería. Está **intencionalmente mal diseñado** para que puedas **practicar la refactorización aplicando el patrón Singleton**.

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

### 🧠 Desafío

Refactoriza `RegistroPedidos` aplicando el patrón **Singleton (versión thread-safe con lazy initialization)** para que **todos los baristas usen la misma instancia compartida**.

¿Listo para intentarlo?
¿Quieres que también te sugiera pasos guía para la refactorización?
