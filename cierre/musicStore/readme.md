
<img width="1223" alt="Screenshot 2024-10-24 at 4 59 30 p m" src="https://github.com/user-attachments/assets/3df1dedc-9729-4b72-99fd-12f4a14f9603">

### Problema de Código Defectuoso para Resolver con Patrones de Diseño GoF, adaptarlo a su Lenguage Favorito.

**Escenario:**  
Una tienda de música en línea administra su catálogo de instrumentos musicales (guitarras, pianos, baterías) con lógica codificada en múltiples clases, pero actualmente la implementación es rígida y presenta duplicación de código al gestionar distintos productos. Cada vez que se añade un nuevo tipo de instrumento, es necesario modificar varias clases, lo que rompe con el principio *Open/Closed* de SOLID y aumenta la deuda técnica. El código actual también tiene problemas de **alta acoplamiento** y **baja cohesión**.

### Código Defectuoso Actual:
Este es un ejemplo de cómo se implementó inicialmente (con problemas de diseño):

```csharp
using System;

public class Guitar
{
    public void Play()
    {
        Console.WriteLine("Playing a guitar.");
    }
}

public class Piano
{
    public void Play()
    {
        Console.WriteLine("Playing a piano.");
    }
}

public class MusicStore
    public void PlayInstrument(string instrumentType)
    {
        if (instrumentType == "Guitar")
        {
            var guitar = new Guitar();
            guitar.Play();
        }
        else if (instrumentType == "Piano")
        {
            var piano = new Piano();
            piano.Play();
        }
        else
        {
            Console.WriteLine("Instrument not available.");
        }
    }
}
```

### Problemas Identificados:
1. **Uso excesivo de `if-else`:** Cada nuevo instrumento requiere cambios en múltiples lugares del código.
2. **Alta acoplamiento:** La clase `MusicStore` depende de implementaciones específicas (Guitar, Piano).
3. **Dificultad para escalar:** Agregar nuevos instrumentos significa modificar código existente.
4. **No se aplica el principio Open/Closed:** Las clases deben estar abiertas para extensión, pero cerradas para modificación.
5. **Falta de abstracción:** La gestión de instrumentos podría beneficiarse de una interfaz o clase base común.

---

### Objetivos a Resolver con Patrones GoF:

| **Objetivo** | **Descripción** |
|--------------|-----------------|
| 1. Refactorización | Eliminar los bloques de `if-else` y utilizar un patrón para mejorar la escalabilidad. |
| 2. Abstracción | Crear una interfaz común para todos los instrumentos. |
| 3. Reducción del acoplamiento | Asegurar que `MusicStore` no conozca implementaciones específicas. |
| 4. Aplicar Open/Closed | Permitir agregar nuevos instrumentos sin modificar código existente. |
| 5. Escalabilidad | Facilitar la inclusión de nuevos tipos de instrumentos sin romper la arquitectura. |
| 6. Aplicación del patrón Factory Method | Usar este patrón para la creación de instancias dinámicas. |
| 7. Pruebas más sencillas | Hacer que las pruebas unitarias sean más fáciles gracias a la desacoplación. |
| 8. Implementación de Inversión de Dependencias | Inyectar dependencias en lugar de crearlas dentro de las clases. |
| 9. Cohesión | Asegurar que cada clase tenga una responsabilidad clara. |
| 10. Reutilización | Hacer que los instrumentos puedan ser utilizados en otros módulos si es necesario. |

---

### Siguiente Paso:
Se espera que el estudiante aplique el **Patrón Factory Method** o **Abstract Factory** para abordar estos problemas. La clase `MusicStore` solo debería interactuar con una interfaz común (por ejemplo, `IInstrument`), y la creación de los objetos específicos debe delegarse a una clase de fábrica.

