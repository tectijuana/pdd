# El botón "Deshacer" de una aplicación solo borra el último cambio con un undo() sin saber qué comando se ejecutó. ❌ → Command sin historial. ¡Falta Command con Undo!
## Joel Cuevas Estrada - 22210298

```c#
// === Código Espagueti (sin patrón Command) ===
using System;

class Editor
{
    public string Texto { get; set; } = "";

    public void Escribir(string nuevoTexto)
    {
        Texto += nuevoTexto;
        Console.WriteLine($"Texto actual: {Texto}");
    }

    public void BorrarUltimo()
    {
        if (Texto.Length > 0)
        {
            Texto = Texto.Substring(0, Texto.Length - 1);
        }
        Console.WriteLine($"Texto tras deshacer: {Texto}");
    }
}

class Program
{
    static void Main()
    {
        var editor = new Editor();

        editor.Escribir("H");
        editor.Escribir("o");
        editor.Escribir("l");
        editor.Escribir("a");

        // Usuario presiona "Deshacer"
        editor.BorrarUltimo();
    }
}
```

## Code Smells

- Falta de encapsulación de acciones (commands) → no hay forma de saber qué acción se ejecutó.

- No hay historial de comandos → “Deshacer” no puede restaurar correctamente cambios complejos.

- Alto acoplamiento: el editor maneja toda la lógica de acción y undo.

## Refactorización usando el patrón Command con Undo
Ahora aplicamos el patrón Command, donde cada acción sabe cómo ejecutarse y deshacerse.
Esto permite mantener un historial y realizar “undo” correctamente.

```c#
// === Refactor con patrón Command ===
using System;
using System.Collections.Generic;

// ----- Interfaz Command -----
interface ICommand
{
    void Ejecutar();
    void Deshacer();
}

// ----- Receptor -----
class Editor
{
    public string Texto { get; set; } = "";
}

// ----- Command abstracto -----
abstract class CommandBase : ICommand
{
    protected Editor editor;
    private string backup = "";

    public CommandBase(Editor editor)
    {
        this.editor = editor;
    }

    protected void GuardarEstado()
    {
        backup = editor.Texto;
    }

    public abstract void Ejecutar();

    public virtual void Deshacer()
    {
        editor.Texto = backup;
        Console.WriteLine($"[UNDO] Texto restaurado: {editor.Texto}");
    }
}

// ----- Command concreto -----
class EscribirCommand : CommandBase
{
    private string texto;

    public EscribirCommand(Editor editor, string texto) : base(editor)
    {
        this.texto = texto;
    }

    public override void Ejecutar()
    {
        GuardarEstado();
        editor.Texto += texto;
        Console.WriteLine($"[EXEC] Texto actual: {editor.Texto}");
    }
}

// ----- Invoker -----
class Aplicacion
{
    private readonly Editor editor = new Editor();
    private readonly Stack<ICommand> historial = new Stack<ICommand>();

    public void EjecutarComando(ICommand comando)
    {
        comando.Ejecutar();
        historial.Push(comando);
    }

    public void Deshacer()
    {
        if (historial.Count > 0)
        {
            ICommand comando = historial.Pop();
            comando.Deshacer();
        }
        else
        {
            Console.WriteLine("No hay acciones para deshacer.");
        }
    }

    public void MostrarTexto()
    {
        Console.WriteLine($"Texto actual: {editor.Texto}");
    }

    public Editor GetEditor() => editor;
}

// ----- Programa principal -----
class Program
{
    static void Main()
    {
        var app = new Aplicacion();
        var editor = app.GetEditor();

        app.EjecutarComando(new EscribirCommand(editor, "H"));
        app.EjecutarComando(new EscribirCommand(editor, "o"));
        app.EjecutarComando(new EscribirCommand(editor, "l"));
        app.EjecutarComando(new EscribirCommand(editor, "a"));

        app.Deshacer();
        app.Deshacer();
    }
}

```
