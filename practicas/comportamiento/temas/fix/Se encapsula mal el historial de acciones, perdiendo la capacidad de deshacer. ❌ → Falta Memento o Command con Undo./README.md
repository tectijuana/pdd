# 🧾 Refactorización: Historial de Acciones Mal Encapsulado (Falta Memento)

## ❌ Problema Original

El sistema intenta mantener un historial de texto para permitir *deshacer cambios*,  
pero el historial está **mal encapsulado** y **no se pueden deshacer correctamente las acciones**.  

Esto provoca pérdida de información y acoplamiento innecesario entre partes del programa.

---

## ⚠️ Código Espagueti (sin patrón)

> ❌ El historial se maneja de forma global, sin encapsulamiento.  
> ❌ No hay separación entre el estado del texto y su gestión.  
> ❌ No existe la capacidad real de deshacer correctamente.

```csharp
using System;
using System.Collections.Generic;

public class Program
{
    static List<string> historial = new List<string>(); // ❌ Mal encapsulado
    static string textoActual = "";

    public static void Main()
    {
        Console.WriteLine("=== Editor Espagueti ===");
        while(true)
        {
            Console.WriteLine("\nTexto actual: " + textoActual);
            Console.WriteLine("1. Escribir texto");
            Console.WriteLine("2. Deshacer último cambio (no funciona bien)");
            Console.WriteLine("3. Salir");
            Console.Write("Opción: ");
            string op = Console.ReadLine();

            if(op == "1")
            {
                Console.Write("Nuevo texto: ");
                string nuevo = Console.ReadLine();
                historial.Add(textoActual); // Guarda sin control
                textoActual += nuevo; // Se modifica directamente
            }
            else if(op == "2")
            {
                if(historial.Count > 0)
                {
                    textoActual = historial[historial.Count - 1]; // ❌ Lógica confusa
                    historial.RemoveAt(historial.Count - 1);
                }
                else Console.WriteLine("Nada que deshacer.");
            }
            else if(op == "3") break;
        }
    }
}
```

---

## 🧩 Identificación del Patrón Ausente

**Patrón Faltante:** `Memento`

### 🧠 Justificación (según GoF)

> El patrón **Memento** permite capturar y externalizar el estado interno de un objeto sin violar su encapsulamiento,  
> de modo que el objeto pueda ser restaurado más tarde a ese estado.

**Aplicación en este caso:**
- El **Originator** es el editor de texto.
- El **Memento** guarda un estado previo del texto.
- El **Caretaker** administra el historial de estados (para deshacer).

---

## ⚙️ Refactor Parcial Implementado (solo la parte funcional)

Se implementa únicamente la lógica de *guardar y restaurar* el estado usando el patrón **Memento**.  
El sistema ya puede realizar operaciones de **Undo (Deshacer)** correctamente.

---

## ✅ Código Refactorizado (funcional en .NET Fiddle)

> 💡 Puedes copiar y pegar este código directamente en [dotnetfiddle.net](https://dotnetfiddle.net/) para probarlo.

```csharp
using System;
using System.Collections.Generic;

// ----- MEMENTO -----
public class Memento
{
    public string Estado { get; private set; }
    public Memento(string estado) => Estado = estado;
}

// ----- ORIGINATOR -----
public class EditorTexto
{
    private string texto = "";

    public void Escribir(string nuevo)
    {
        texto += nuevo;
    }

    public string ObtenerTexto() => texto;

    public Memento GuardarEstado()
    {
        return new Memento(texto);
    }

    public void RestaurarEstado(Memento memento)
    {
        texto = memento.Estado;
    }
}

// ----- CARETAKER -----
public class Historial
{
    private Stack<Memento> estados = new Stack<Memento>();

    public void Guardar(Memento m) => estados.Push(m);

    public Memento Deshacer()
    {
        return estados.Count > 0 ? estados.Pop() : null;
    }
}

// ----- PROGRAMA PRINCIPAL -----
public class Program
{
    public static void Main()
    {
        EditorTexto editor = new EditorTexto();
        Historial historial = new Historial();

        Console.WriteLine("=== Editor con Memento ===");
        while (true)
        {
            Console.WriteLine("\nTexto actual: " + editor.ObtenerTexto());
            Console.WriteLine("1. Escribir texto");
            Console.WriteLine("2. Deshacer último cambio");
            Console.WriteLine("3. Salir");
            Console.Write("Opción: ");
            string op = Console.ReadLine();

            if (op == "1")
            {
                historial.Guardar(editor.GuardarEstado());
                Console.Write("Nuevo texto: ");
                string nuevo = Console.ReadLine();
                editor.Escribir(nuevo);
            }
            else if (op == "2")
            {
                var estadoPrevio = historial.Deshacer();
                if (estadoPrevio != null)
                    editor.RestaurarEstado(estadoPrevio);
                else
                    Console.WriteLine("Nada que deshacer.");
            }
            else if (op == "3") break;
        }
    }
}
```

---

## 🧩 Comparación

| Aspecto | Código Espagueti | Código Refactorizado |
|----------|------------------|----------------------|
| Encapsulamiento | ❌ Variables globales | ✅ Estados controlados por Memento |
| Historial | ❌ Lista manipulada manualmente | ✅ Stack gestionado por Caretaker |
| Undo | ❌ Parcial y poco fiable | ✅ Restauración exacta del estado anterior |
| Patrón GoF | ❌ Ninguno | ✅ **Memento** |

---

## 📚 Conclusión

El uso del patrón **Memento** mejora el encapsulamiento, separa responsabilidades y permite implementar correctamente la función de **Undo** sin exponer el estado interno del editor.
