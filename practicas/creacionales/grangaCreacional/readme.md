
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


## 🧾 Rúbrica de Evaluación – Refactorización con Patrones Creacionales

| Criterio                                             | Excelente (5)                                                                 | Bueno (4)                                                        | Aceptable (3)                                                     | Insuficiente (1-2)                                     |
|------------------------------------------------------|--------------------------------------------------------------------------------|------------------------------------------------------------------|------------------------------------------------------------------|--------------------------------------------------------|
| 🧠 Identificación de Problemas en el Código          | Identifica y documenta correctamente los 5 errores clave en el diseño inicial | Identifica la mayoría de los errores relevantes                  | Menciona algunos problemas pero no los relaciona bien             | No identifica errores significativos                   |
| 🏗️ Aplicación de Factory Method                     | Implementa correctamente el patrón y lo explica con claridad                  | Implementa el patrón pero con errores menores                    | Implementa parcialmente, falta cohesión o justificación            | No aplica el patrón o lo hace incorrectamente          |
| 🏭 Aplicación de Abstract Factory                   | Crea familias de productos completas y coherentes                             | Aplica el patrón con algunas omisiones                          | Implementa con errores estructurales                               | No aplica el patrón o hay confusión conceptual         |
| 🧱 Uso del Builder                                   | Usa correctamente el patrón para construir rutinas complejas                 | Aplica el patrón, pero le falta modularidad o claridad           | Estructura débil, pasos mal definidos                             | No logra construir objetos paso a paso                 |
| 🧬 Uso de Prototype                                  | Clona correctamente objetos para reutilizar estructuras                      | Aplica el patrón pero sin aprovechar sus ventajas                | Clonación incompleta o errónea                                     | No aplica clonación, uso erróneo de referencias        |
| 🔒 Implementación de Singleton (correcta)           | Implementación segura, thread-safe, bien documentada                         | Implementación funcional pero no segura para concurrencia        | Patrón aplicado sin encapsulamiento ni validación                 | Código rígido o Singleton mal implementado             |
| 💡 Creatividad en la Solución                        | Integra múltiples patrones con fluidez y originalidad                         | Aplica combinaciones de patrones coherentes                     | Uso mecánico de patrones, sin adaptación contextual               | Aplicación forzada o sin conexión con el problema      |
| 📝 Documentación y Comentarios                       | Comentarios claros, en español, explicando cada patrón aplicado              | Comentarios adecuados pero escuetos                             | Comentarios mínimos o ambiguos                                    | Sin documentación o confusa                           |
| 💻 Compilación y Ejecución en .NET 8                | Proyecto compila y corre perfectamente en .NET 8                              | Corre con advertencias menores                                  | Requiere ajustes o dependencias externas                          | No compila o falla al ejecutar                         |
| 🎥 Reflexión personal del modelado y uso de esta técnica CleanCode                      |          Explica con sus palabras el donde aplicar  y lograr distinguir una mejora en sus técnicas actuales.              |     no        |                                |                                |

**Puntaje Máximo:** 50 puntos  
**Nota Final:**  
- 46-50 → 🌟 Excelente  
- 40-45 → ✅ Bueno  
- 30-39 → ⚠️ Regular  
- <30 → ❌ Necesita mejorar



