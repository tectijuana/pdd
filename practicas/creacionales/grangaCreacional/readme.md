
<img width="1536" height="1024" alt="image" src="https://github.com/user-attachments/assets/318d860e-2b7f-49d7-9b89-964198fb0507" />

## 🐄 Práctica Integral: **Sistema de Alimentación Automatizada para Granja Inteligente**

### 🎯 Objetivo General:

Refactorizar un sistema de alimentación de ganado que actualmente usa lógica rígida y mal estructurada. El estudiante debe aplicar **todos los patrones creacionales GoF** para mejorar la mantenibilidad, extensibilidad y modularidad del código.

---

## 🧩 Contexto Temático

Una granja tiene un sistema automatizado que:

* Detecta animales (vacas, cerdos, gallinas)
* Les sirve **comidas específicas**
* Les provee **bebidas y suplementos**
* Genera **rutinas completas de alimentación**
* Clona rutinas pasadas si funcionan bien (por eficiencia)
* Usa un único **registro central** para control y monitoreo (donde se aplica Singleton)

---

## 🧨 Código Inicial (Malo)

```csharp
public class FeedingSystem
{
    public void Feed(string animal)
    {
        if (animal == "Cow")
            Console.WriteLine("Feeding cow: hay, water, salt block.");
        else if (animal == "Pig")
            Console.WriteLine("Feeding pig: grains, juice.");
        else if (animal == "Chicken")
            Console.WriteLine("Feeding chicken: seeds, water.");
    }
}
```

---

## 🧠 Retos del Estudiante

| Patrón GoF              | Aplicación esperada                                                         |
| ----------------------- | --------------------------------------------------------------------------- |
| 🧪 **Factory Method**   | Crear instancias de `Dieta` para cada tipo de animal según una fábrica.     |
| 🧪 **Abstract Factory** | Crear familias: `Alimento`, `Bebida` y `Suplemento` por tipo de animal.     |
| 🧪 **Builder**          | Construir `RutinaAlimentacionCompleta` paso a paso (desayuno, snack, cena). |
| 🧪 **Prototype**        | Clonar rutinas anteriores para usarlas como base para nuevos animales.      |
| 🧪 **Singleton**        | Clase `RegistroGlobalAlimentacion` centraliza la auditoría del sistema.     |

---

## 🔍 Actividades

1. Identifica en qué parte del código mal diseñado se puede aplicar cada patrón.
2. Refactoriza sin romper la lógica de negocio.
3. Asegúrate de cumplir con los principios SOLID.
4. Documenta los cambios con comentarios en español.
5. Documenta en un GIST para la entrega

---

Perfecto, aquí tienes el escenario completo que integra los **5 patrones creacionales GoF** dentro de una estructura en `.NET 8` **listo para refactorizar**. Este archivo `Program.cs` será incluido en un `playbook.yml` compatible con **Ubuntu 24.04** y grabación `asciinema`.

---

## 🧪 Estructura del Código Mal Diseñado (`Program.cs`)

Este es el código que los estudiantes deben refactorizar, integrando los patrones **Factory Method**, **Abstract Factory**, **Builder**, **Prototype** y **Singleton**:

```csharp
using System;
using System.Collections.Generic;

namespace GranjaInteligente
{
    public class FeedingSystem
    {
        public void Alimentar(string animal)
        {
            if (animal == "Vaca")
                Console.WriteLine("Dando heno, agua y sal mineral a la vaca.");
            else if (animal == "Cerdo")
                Console.WriteLine("Dando granos y jugo al cerdo.");
            else if (animal == "Gallina")
                Console.WriteLine("Dando semillas y agua a la gallina.");
            else
                Console.WriteLine("Animal desconocido.");
        }
    }

    public class RegistroAlimentacion
    {
        private static RegistroAlimentacion instancia;
        private RegistroAlimentacion() {}

        public static RegistroAlimentacion ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new RegistroAlimentacion();

            return instancia;
        }

        public void Registrar(string mensaje)
        {
            Console.WriteLine($"[REGISTRO] {mensaje}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var sistema = new FeedingSystem();
            sistema.Alimentar("Vaca");
            sistema.Alimentar("Cerdo");

            var registro = RegistroAlimentacion.ObtenerInstancia();
            registro.Registrar("Ciclo de alimentación completado.");
        }
    }
}
```

---

## 🧰 ¿Qué debe hacer el estudiante?

1. Reemplazar `FeedingSystem` usando **Factory Method** para crear dietas.
2. Agregar una **Abstract Factory** para generar familias de objetos: `Alimento`, `Bebida`, `Suplemento`.
3. Aplicar **Builder** para generar rutinas de alimentación diaria.
4. Implementar **Prototype** para clonar rutinas exitosas y ahorrar configuración.
5. Corregir el uso **deficiente de Singleton** (hacerlo thread-safe).

---


