# ❝𝐒𝐈𝐍𝐆𝐋𝐄𝐓𝐎𝐍❞
Imagina que necesitas un objeto especial en tu programa, pero solo quieres uno. El patrón Singleton te asegura que siempre tengas ese objeto único y que nadie más pueda crear copias. 
Es como tener un objeto muy especial al que todos pueden acceder, pero solo hay uno en todo el programa.
## ¿Qué es el patrón singleton?
Es una técnica creacional que garantiza que una clase tenga solamente una instancia, al mismo tiempo que ofrece un acceso global a esa instancia. El patrón de diseño Singleton fue descrito en el libro Dessign Patterns, Elements of Reusable Object-Oriented Software escrito por cuatro autores también conocidos como GoF (“Gang of four”).

## Una Instancia, Múltiples Beneficios
El patrón Singleton es útil por varias razones clave que contribuyen a su popularidad en el desarrollo de software:

1. Única instancia: Asegura que una clase tenga solo un objeto a lo largo de toda la aplicación. 

2. Punto de acceso global: Proporciona un punto de acceso único a esta instancia desde cualquier parte del código.

3. Control de Creación: Permite un control preciso sobre cuándo y cómo se crea la instancia.

4. Estado Consistente: Garantiza que el estado de la instancia sea el mismo en toda la aplicación.

5. Facilidad de Mantenimiento: Centraliza la gestión de la instancia, simplificando los cambios.

## Analogía en el mundo real
El gobierno es un ejemplo excelente del patrón Singleton. Un país sólo puede tener un gobierno oficial. Independientemente de las identidades personales de los individuos que forman el gobierno, el título “Gobierno de X” es un punto de acceso global que identifica al grupo de personas a cargo.
## Casos de Uso Comunes del Patrón Singleton
Este patrón es útil cuando quieres controlar quién y cómo utiliza un objeto. Es más seguro que usar variables globales, ya que evita que otros partes del código modifiquen accidentalmente el objeto. Además, puedes ajustar este patrón para permitir más de un objeto si lo necesitas
1. Gestión de Conexiones a Bases de Datos: Asegura una única conexión activa, optimizando recursos y evitando problemas de concurrencia.
2. Registro de Eventos o Logs: Proporciona un mecanismo centralizado para manejar logs, garantizando consistencia y evitando duplicados.
3. Configuración de la Aplicación: Almacena parámetros globales y preferencias, asegurando acceso uniforme en toda la aplicación.
4. Caché de Datos: Permite almacenar y acceder a datos en memoria, mejorando el rendimiento al evitar consultas repetidas.
5. Gestión de Recursos Compartidos: Controla el acceso a recursos como impresoras o sistemas de archivos, garantizando una única instancia.
7. Fábricas y Servicios: Asegura una única instancia de fábricas de objetos o servicios comunes, facilitando su acceso global.
# Ejemplo
https://dotnetfiddle.net/4KOalS
```csharp
using System;

public class Program
{
    public static void Main(string[] args)
    {
        Singleton instance = Singleton.GetInstance();
        Console.WriteLine("Singleton instance created.");
    }
}

public class Singleton
{
    private static Singleton instance;

    private Singleton() { }

    public static Singleton GetInstance()
    {
        if (instance == null)
        {
            instance = new Singleton();
        }

        return instance;
    }
}

