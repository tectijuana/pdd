# 📌 Refactorización con Patrón Adapter

---

## 1. Identificación de Code Smells

En la versión inicial del código (sin refactorizar) se identifican los siguientes problemas estructurales:

1. **Uso excesivo de `if`/`switch`**  
   El comportamiento se adapta manualmente con condicionales en lugar de abstraer las diferencias entre interfaces.

2. **Violación del Principio de Responsabilidad Única (SRP)**  
   La clase cliente debe conocer todos los posibles tipos de reproductores y adaptarse a ellos, en lugar de delegar la conversión a una clase especializada.

3. **Acoplamiento rígido**  
   Si se agrega un nuevo tipo de reproductor o formato, se requiere modificar el código del cliente, lo que viola el principio de Abierto/Cerrado (OCP).

---

## 2. Aplicación del patrón adecuado

El patrón estructural utilizado fue **Adapter**, siendo el más apropiado para resolver el problema identificado.  
Permite que clases con interfaces incompatibles trabajen juntas mediante la creación de un adaptador que traduce las llamadas del cliente a los métodos correctos de las clases concretas.

---

## 3. Refactor funcional (parcial o total)

El código refactorizado:

- **Compila sin errores.**  
- **Tiene lógica coherente.**  
- **Se integra correctamente** con el resto del sistema.  

No fue necesaria una reescritura total, únicamente una **extracción de responsabilidades** y la implementación del Adapter.  
El cliente (`MediaPlayer`) ahora delega la lógica de reproducción al adaptador (`MediaAdapter`), y cada clase concreta (`Mp3Player`, `Mp4Player`, `VlcPlayer`) se encarga de su propio comportamiento.

---

## 4. Justificación técnica en Pull Request

**Descripción del problema:**  
El código original usaba `if/switch` para manejar múltiples formatos de audio y video, provocando rigidez y violando principios de diseño.  

**Patrón aplicado:**  
Se aplicó el patrón **Adapter**, creando una interfaz común `IAdvancedMediaPlayer` y adaptadores que encapsulan la lógica de cada formato.  

**Beneficios esperados:**  
- Código más flexible y mantenible.  
- Apertura a nuevos formatos sin modificar la lógica del cliente.  
- Aplicación de los principios **SRP** y **OCP**.  
- Mayor cohesión y menor acoplamiento.  

---

## 5. Calidad del código refactorizado

- **Legibilidad:** Nombres claros (`Mp3Player`, `MediaAdapter`, `MediaPlayer`).  
- **Coherencia:** La lógica de negocio está encapsulada y separada por responsabilidades.  
- **Nombres correctos:** Las clases reflejan de manera explícita el rol que cumplen.  
- **Separación de responsabilidades:** El cliente solo reproduce, el Adapter traduce y cada clase concreta se encarga de su propio formato.  
- **Uso idiomático en C# / .NET 8:** Se emplea `switch` moderno en expresiones, excepciones claras y clases bien organizadas.  

---

### Código sin refactorizar (❌ Malo)

```cs
// Cliente que depende directamente de los formatos
public class MediaPlayer
{
    public void Play(string type, string filename)
    {
        switch (type)
        {
            case "mp3":
                Console.WriteLine($"Reproduciendo MP3: {filename}");
                break;
            case "mp4":
                Console.WriteLine($"Reproduciendo MP4: {filename}");
                break;
            case "vlc":
                Console.WriteLine($"Reproduciendo VLC: {filename}");
                break;
            default:
                Console.WriteLine("Formato no soportado");
                break;
        }
    }
}

// Ejecución
class Program
{
    static void Main()
    {
        MediaPlayer player = new MediaPlayer();
        player.Play("mp3", "cancion1.mp3");
        player.Play("mp4", "video1.mp4");
        player.Play("vlc", "pelicula1.vlc");
    }
}
```
### Código refactorizado (Bueno)
```cs
// Interface común
public interface IAdvancedMediaPlayer
{
    void Play(string filename);
}

// Implementaciones concretas
public class Mp3Player : IAdvancedMediaPlayer
{
    public void Play(string filename)
    {
        Console.WriteLine($"Reproduciendo MP3: {filename}");
    }
}

public class Mp4Player : IAdvancedMediaPlayer
{
    public void Play(string filename)
    {
        Console.WriteLine($"Reproduciendo MP4: {filename}");
    }
}

public class VlcPlayer : IAdvancedMediaPlayer
{
    public void Play(string filename)
    {
        Console.WriteLine($"Reproduciendo VLC: {filename}");
    }
}

// Adapter que traduce la solicitud
public class MediaAdapter
{
    private readonly IAdvancedMediaPlayer _player;

    public MediaAdapter(string type)
    {
        _player = type switch
        {
            "mp3" => new Mp3Player(),
            "mp4" => new Mp4Player(),
            "vlc" => new VlcPlayer(),
            _ => throw new NotSupportedException("Formato no soportado")
        };
    }

    public void Play(string filename)
    {
        _player.Play(filename);
    }
}

// Cliente
public class MediaPlayer
{
    public void Play(string type, string filename)
    {
        try
        {
            var adapter = new MediaAdapter(type);
            adapter.Play(filename);
        }
        catch (NotSupportedException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

// Ejecución
class Program
{
    static void Main()
    {
        MediaPlayer player = new MediaPlayer();
        player.Play("mp3", "cancion1.mp3");
        player.Play("mp4", "video1.mp4");
        player.Play("vlc", "pelicula1.vlc");
    }
}
```
