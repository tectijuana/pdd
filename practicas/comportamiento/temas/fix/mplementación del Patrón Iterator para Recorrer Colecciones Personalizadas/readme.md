# 📘 Refactorización Parcial — Patrón Iterator (C# / .NET 8)

No.19 No se puede iterar sobre una colección personalizada sin conocer su estructura interna. ❌ → Iterator ausente.

## 📌 Contexto  
En la implementación original de la colección personalizada (`CustomBag`), **no era posible iterar sobre sus elementos** usando `foreach`.  
Esto generaba un fuerte acoplamiento entre la clase consumidora (`Program`) y la estructura interna de `CustomBag` (índices, listas internas, etc.).

El objetivo de este refactor parcial es **mejorar la encapsulación y permitir la iteración** sin exponer detalles internos, aplicando el patrón **Iterator** de forma idiomática en C# 8+ (.NET 8).

---

## 🔍 Problemas detectados (Code Smells)

### 1️⃣ Falta de Iterator  
`CustomBag` no implementaba `IEnumerable` ni `GetEnumerator()`.  
❌ No era posible usar `foreach`, lo cual rompe la compatibilidad con patrones del lenguaje (LINQ, colecciones genéricas, etc.).

### 2️⃣ Fuga de estructura interna  
El método `GetAt(int index)` expone el acceso directo a la lista interna.  
❌ Rompe el principio de encapsulamiento y aumenta el acoplamiento.

### 3️⃣ Dificultad de extensión  
Cualquier cambio en la forma de almacenar los datos (por ejemplo, pasar de `List<T>` a `HashSet<T>`) rompería el código consumidor.  
❌ Viola el principio *Open/Closed (OCP)*.

---

## ✅ Soluciones aplicadas (Refactor Parcial)

### 🧩 Aplicación del patrón **Iterator**
Se implementa una versión mínima de `IEnumerable<string>` usando `yield return`, lo que permite recorrer los elementos sin exponer cómo están almacenados.

### 🔄 Eliminación de la dependencia estructural  
El código cliente ahora puede usar `foreach` sin conocer la existencia de índices ni de listas internas.

### 🧠 Refactor conceptual
- `CustomBag` se convierte en una **colección iterable**.  
- La lógica de recorrido se mueve dentro de la propia clase, aislando a los consumidores.

---

## 🛠️ Patrones y principios aplicados

| Patrón / Principio | Rol en el refactor |
|--------------------|--------------------|
| 🌿 **Iterator (GoF)** | Separa la lógica de recorrido del contenedor. |
| 🧩 **Encapsulación** | El cliente ya no necesita conocer la estructura interna. |
| 🧱 **Open/Closed Principle (OCP)** | Permite cambiar la estructura interna sin romper el cliente. |
| 🧪 **Cohesión alta** | Cada clase hace una sola cosa. |
| 🧠 **Polimorfismo idiomático de C#** | Se usa `yield return` como forma nativa de construir iteradores. |

---

## ❌ Código original (antes del refactor)

```csharp
using System;
using System.Collections.Generic;

namespace BadIteratorDemo
{
    public class CustomBag
    {
        private readonly List<string> items = new();

        public void Add(string value) => items.Add(value);
        public string GetAt(int index) => items[index];
        public int Count => items.Count;
    }

    public class Program
    {
        public static void Main()
        {
            var bag = new CustomBag();
            bag.Add("Manzana");
            bag.Add("Pera");
            bag.Add("Uva");

            // ❌ No se puede usar foreach → Iterator ausente
            for (int i = 0; i < bag.Count; i++)
            {
                Console.WriteLine(bag.GetAt(i));
            }
        }
    }
}
```

## ✅ Código refactorizado (parcial)

🔒 Solo se aplicó el refactor mínimo necesario para permitir la iteración.
🚫 No se modificaron otros comportamientos internos ni se agregaron nuevas responsabilidades.
```csharp
using System;
using System.Collections;
using System.Collections.Generic;

namespace IteratorRefactor
{
    // Implementa IEnumerable → habilita el patrón Iterator
    public class CustomBag : IEnumerable<string>
    {
        private readonly List<string> items = new();

        public void Add(string value) => items.Add(value);

        // 🔄 Iterator interno simplificado
        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item; // 🌿 Aplica el patrón Iterator idiomático
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Program
    {
        public static void Main()
        {
            var bag = new CustomBag();
            bag.Add("Manzana");
            bag.Add("Pera");
            bag.Add("Uva");

            // ✅ Ahora el cliente no necesita conocer la estructura interna
            foreach (var item in bag)
            {
                Console.WriteLine($"Elemento: {item}");
            }
        }
    }
}
```
<img width="706" height="479" alt="image" src="https://github.com/user-attachments/assets/a8728466-121e-41b4-844a-c19bef7e3733" />

---

## 💡 Justificación técnica
💡 Justificación técnica
- Legibilidad: El recorrido con foreach es más claro y natural.
- Encapsulamiento:	El consumidor ya no depende de GetAt ni de índices.
- Extensibilidad:	Se puede cambiar el contenedor interno sin romper al cliente.
- Reusabilidad:	Compatible con LINQ y otras APIs estándar de .NET.

## 🔄 Impacto del refactor

- 🚫 Se eliminaron métodos que exponían detalles internos.
- ✅ Se añadió un iterador interno seguro y extensible.
- ✅ Se restauró la compatibilidad con el ecosistema de colecciones de .NET.
- 🌿 Aplicación correcta del patrón Iterator y cumplimiento de principios SOLID.

## 🧭 Reflexión

Durante este ejercicio se evidenció que la falta de un iterador adecuado puede romper la cohesión y encapsulación del diseño, incluso en casos simples.

Al aplicar el patrón Iterator, se redujo el acoplamiento entre el cliente y la estructura interna, demostrando que un pequeño refactor bien dirigido tiene un gran impacto en la mantenibilidad.

✳️ Lección aprendida:
El patrón Iterator no solo sirve para recorrer colecciones, sino para preservar la independencia entre quién recorre y qué se recorre.
La simplicidad de yield return en C# es una forma elegante y moderna de aplicar este principio sin complejidad adicional.
