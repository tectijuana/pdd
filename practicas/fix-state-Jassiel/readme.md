# 🧠 Patrón State (GoF Comportamiento)

## 🎯 Objetivo
Refactorizar la gestión de estados de una factura usando el patrón **State** para eliminar múltiples if/else y mejorar la extensibilidad y mantenibilidad.

---

## 🧩 Descripción del patrón
El patrón **State** permite que un objeto altere su comportamiento cuando cambia su estado interno, sin necesidad de if/else distribuidos por todo el código.

- Cambia el comportamiento según el estado actual.
- Evita acoplamientos innecesarios y condicionales extensos.
- Facilita agregar nuevos estados en el futuro.

---

## 💻 Código implementado
```csharp
using System;

// ----------------------------
// 🧩 Ejemplo del patrón State (GoF)
// Autor: Rolando Jassiel Castro Hernández
// Problema #3: Factura con múltiples if para manejar estados
// ----------------------------

namespace PatronStateDemo
{
    // 🧠 Interfaz que define el comportamiento de un estado
    interface IEstadoFactura
    {
        void Procesar(Factura contexto);
    }

    // 🔵 Estado concreto: Pendiente
    class Pendiente : IEstadoFactura
    {
        public void Procesar(Factura contexto)
        {
            Console.WriteLine("💰 Procesando pago de la factura...");
            contexto.CambiarEstado(new Pagada());
        }
    }

    // 🟢 Estado concreto: Pagada
    class Pagada : IEstadoFactura
    {
        public void Procesar(Factura contexto)
        {
            Console.WriteLine("✅ La factura ya está pagada.");
        }
    }

    // 🔴 Estado concreto: Cancelada
    class Cancelada : IEstadoFactura
    {
        public void Procesar(Factura contexto)
        {
            Console.WriteLine("❌ La factura está cancelada. No se puede procesar.");
        }
    }

    // 🧾 Clase Contexto que mantiene una referencia al estado actual
    class Factura
    {
        private IEstadoFactura _estado;

        public Factura()
        {
            // Estado inicial por defecto
            _estado = new Pendiente();
        }

        public void CambiarEstado(IEstadoFactura nuevo)
        {
            _estado = nuevo;
        }

        public void Procesar()
        {
            _estado.Procesar(this);
        }

        public void Cancelar()
        {
            _estado = new Cancelada();
            Console.WriteLine("⚠️  La factura ha sido cancelada.");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("🧾 DEMO: Patrón State (GoF)\n");

            Factura f = new Factura();
            f.Procesar();  // Procesa pago y cambia a Pagada
            f.Procesar();  // Intenta procesar nuevamente
            f.Cancelar();  // Cancela la factura
            f.Procesar();  // Intenta procesar factura cancelada

            Console.WriteLine("\n✅ Ejecución finalizada correctamente.");
        }
    }
}

```

---

## 🧪 Ejecución
- Lenguaje: C#  
- Entorno: .NET 8.0 / Visual Studio / DotNetFiddle

**Salida esperada:**
```
💰 Procesando pago de la factura...
✅ La factura ya está pagada.
⚠️  La factura ha sido cancelada.
❌ La factura está cancelada. No se puede procesar.
```

**Enlace DotNetFiddle:** https://dotnetfiddle.net/voWNLC
---

## 👥 Integrante
- **Alumno:** Rolando Jassiel Castro Hernández  
- **Categoría:** GoF Comportamiento  
- **Patrón:** State  
