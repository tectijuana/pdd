# 💣 Código Original Defectuoso (Pre-Refactorización)
Este código refleja el estado inicial de tu sistema, con todos los problemas identificados: alto acoplamiento, condicionales excesivos, y sin abstracción (IInstrumento).
| Problema Clave	| Código Específico |
| --- | --- |
| ❌ Sin Abstracción | No hay interfaz común. MusicStore debe tratar a Guitarra y Piano por separado. |
| ❌ Acoplamiento Alto	| MusicStore usa new Guitarra() y new Piano(). |
| ❌ Violación OCP	| El método Tocar debe modificarse cada vez que se añade un instrumento. |

``` c sharp
// 1. Clases concretas (NO comparten interfaz)
public class Guitarra
{
    public void Play()
    {
        Console.WriteLine("🎸 Cuerdas rasgueadas... ¡Sonido de Guitarra!");
    }
}

public class Piano
{
    public void Play()
    {
        Console.WriteLine("🎹 Teclas pulsadas... ¡Sonido de Piano!");
    }
}

// 2. Clase cliente con el código defectuoso
public class MusicStore
{
    public void Tocar(string tipoInstrumento) // ❌ PUNTO DE MODIFICACIÓN
    {
        // ❌ Uso excesivo de condicionales y lógica de creación acoplada
        if (tipoInstrumento.ToLower() == "guitarra")
        {
            Guitarra g = new Guitarra();
            g.Play();
        }
        else if (tipoInstrumento.ToLower() == "piano")
        {
            Piano p = new Piano();
            p.Play();
        }
        else
        {
            Console.WriteLine($"Error: Instrumento '{tipoInstrumento}' no reconocido.");
        }
        // Si añades Batería, debes modificar este 'if/else' (Violación OCP)
    }
}

// Ejemplo de Uso (el cliente llama a la tienda)
public class ProgramaDefectuoso
{
    public static void Main()
    {
        var tienda = new MusicStore();
        tienda.Tocar("Guitarra"); 
        tienda.Tocar("Piano");
    }
}

```

# ✅ Código Refactorizado con Patrón Factory Method (Post-Refactorización)
Este código aplica los pasos que discutimos, usando la interfaz IInstrumento y el patrón Factory para aislar la lógica de creación.

| Objetivo Cumplido		| Patrón / Principio |
| --- | --- |
| Abstracción | ntroducción de IInstrumento. |
| Responsabilidad Única (SRP)	| InstrumentoFactory maneja solo la creación. |
| Abierto/Cerrado (OCP)	| MusicStore no se modifica al añadir nuevos instrumentos. |
| Factory Method | 	La InstrumentoFactory delega la instanciación de clases concretas. |

``` c sharp
// --- 1. Abstracción: El Contrato (IInstrumento.cs) ---

// Cumple el objetivo #2: Introducir abstracción
public interface IInstrumento
{
    void Play();
    void Stop();
    void Replay();
}


// --- 2. Implementaciones Concretas (Guitarra.cs, Piano.cs) ---

public class Guitarra : IInstrumento
{
    public void Play() => Console.WriteLine("🎸 Cuerdas rasgueadas... ¡Sonido de Guitarra!");
    public void Stop() => Console.WriteLine("Guitarra silenciada.");
    public void Replay() => Console.WriteLine("Reiniciando el solo de guitarra.");
}

public class Piano : IInstrumento
{
    public void Play() => Console.WriteLine("🎹 Teclas pulsadas... ¡Sonido de Piano!");
    public void Stop() => Console.WriteLine("Piano silenciado.");
    public void Replay() => Console.WriteLine("Reiniciando el solo de piano.");
}


// --- 3. La Fábrica: Creación de Objetos (InstrumentoFactory.cs) ---

// Cumple el objetivo #5: Usar patrón de creación (Factory) y #6: Mejorar cohesión (SRP)
public static class InstrumentoFactory
{
    public static IInstrumento CrearInstrumento(string tipo)
    {
        // El 'switch' (condicional) está ahora encapsulado aquí.
        switch (tipo.ToLower())
        {
            case "guitarra":
                return new Guitarra();
            case "piano":
                return new Piano();
            // Para añadir un 'Violin', solo se modifica *ESTA* clase, no MusicStore.
            // case "violin":
            //     return new Violin(); 
            default:
                throw new ArgumentException($"Tipo de instrumento desconocido: {tipo}");
        }
    }
}


// --- 4. El Cliente: Uso Desacoplado (MusicStore.cs) ---

// Cumple el objetivo #1 (Eliminar condicionales) y #3 (Reducir acoplamiento)
public class MusicStore 
{
    // El método MusicStore.Tocar ya NO contiene condicionales 
    // y NO conoce las clases concretas (Guitarra, Piano).
    public void Tocar(string tipo)
    {
        // 1. Delegar la creación a la fábrica. Retorna IInstrumento.
        IInstrumento instrumento = InstrumentoFactory.CrearInstrumento(tipo);

        Console.WriteLine($"\n--- La tienda toca un: {tipo} ---");
        
        // 2. Usar el objeto a través de su interfaz (IInstrumento).
        instrumento.Play(); 
        instrumento.Stop();
    }
}


// --- 5. Ejemplo de uso (Main) ---
public class ProgramaRefactorizado
{
    public static void Main()
    {
        var tienda = new MusicStore();

        tienda.Tocar("Guitarra"); // Funciona
        tienda.Tocar("Piano");    // Funciona
        // tienda.Tocar("Violin"); // Si existiera la clase, solo se modifica la fábrica.
    }
}
```
