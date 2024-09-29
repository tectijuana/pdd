![logo tec](https://github.com/user-attachments/assets/0b5a18fc-0968-45d2-a1cd-914a75adfa59)

<pre>

	<p align=center>

<b>Tecnológico Nacional de México
Instituto Tecnológico de Tijuana

Departamento de Sistemas y Computación
Ingeniería en Sistemas Computacionales

Semestre:
Agosto - Diciembre 2024

Materia:
Patrones de Diseño

Docente:
M.C. Rene Solis Reyes 

Unidad:
1

Título del trabajo:
Pros y Contras de los Patrones de Diseño GOF

Estudiantes:
Ortiz Garcia Nayeli 21210406
Rodriguez Cruz Luis Fernando 21210421</b>

	</p>

</pre>

<div align="center">
<h1>Pros y Contras de los Patrones de Diseño GOF</h1>
</div>

---

## Índice
1. [Introducción](#introducción)
2. [Pros](#pros)
3. [Contras](#contras)
4. [Ejemplo de Código](#ejemplo-de-código)
5. [Conclusión](#conclusión)

---

<div id="introducción"></div>

# Introducción  
Los "Patrones de Diseño GOF" (Gang of Four) fueron popularizados en el libro *"Design Patterns: Elements of Reusable Object-Oriented Software"* publicado en 1994 por Erich Gamma, Richard Helm, Ralph Johnson y John Vlissides. Estos patrones ayudan a escribir código más estructurado, mantenible y reutilizable. Sin embargo, como cualquier herramienta, tienen ventajas y desventajas que es importante considerar.

---

<div id="pros"></div>

# PROS

### 1. **Reutilización de Código**  
- Permiten reutilizar soluciones probadas, evitando reinventar la rueda.
- Reducen el tiempo de desarrollo al basarse en estructuras previamente documentadas.

### 2. **Estandarización**  
- Facilitan la comprensión del código por parte de otros desarrolladores.
- Mejoran la comunicación entre programadores con nombres de patrones conocidos (Singleton, Factory, Observer, etc.).

### 3. **Mantenibilidad y Flexibilidad**  
- Promueven la separación de responsabilidades, facilitando la modificación o ampliación del sistema.
- Proporcionan flexibilidad al permitir cambios sin reescribir grandes porciones de código.

### 4. **Buenas Prácticas**  
- Fomentan el uso de herencia, polimorfismo y otros principios sólidos de programación orientada a objetos.
- Facilitan el diseño de sistemas modulares y adaptables a futuras necesidades.

---

<div id="contras"></div>

# CONTRAS

### 1. **Sobrecarga de Complejidad**  
- En proyectos pequeños, pueden añadir complejidad innecesaria.
- Aplicar un patrón sin necesidad clara puede hacer que el código sea más difícil de mantener.

### 2. **Curva de Aprendizaje**  
- Requieren un conocimiento profundo de la programación orientada a objetos.
- Implementarlos correctamente puede llevar tiempo, y sin experiencia adecuada, es fácil cometer errores.

### 3. **Rigidez en la Aplicación**  
- No siempre son la mejor solución para todos los problemas. En algunos casos, es preferible utilizar enfoques más simples o personalizados.
- Pueden causar problemas de dependencia o dificultar la prueba de unidades, como en el caso del patrón Singleton.

### 4. **Tendencia a la Sobreingeniería**  
- Existe el riesgo de implementar patrones por popularidad en lugar de necesidad, lo que lleva a una sobreingeniería del sistema.
- En entornos ágiles, algunos patrones pueden ser demasiado complejos para ser implementados rápidamente.

---

<div id="ejemplo-de-código"></div>

# Ejemplo de Código

```csharp
// Patrón Singleton: Pro - Reutilización de Código, Contra - Sobrecarga de Complejidad

// Clase que implementa el patrón Singleton
public class Logger
{
    private static Logger _instance;

    // Constructor privado para evitar instanciación directa
    private Logger() { }

    // Método estático que asegura solo una instancia
    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    // Método para registrar logs
    public void Log(string message)
    {
        Console.WriteLine("Log: " + message);
    }
}

// Uso del Singleton
class Program
{
    static void Main(string[] args)
    {
        // Obtenemos la única instancia de Logger
        Logger logger = Logger.GetInstance();
        logger.Log("Este es un mensaje de log.");
        
        // Intento de crear otra instancia, sigue siendo el mismo objeto
        Logger anotherLogger = Logger.GetInstance();
        anotherLogger.Log("Este es otro mensaje de log.");

        // Ambas variables apuntan a la misma instancia
        Console.WriteLine(Object.ReferenceEquals(logger, anotherLogger)); // True
    }
}
```
### **Pro:**
**Reutilización de Código**: Como puedes ver, el patrón **Singleton** asegura que solo haya una instancia de la clase `Logger`. Esto permite que la misma instancia se reutilice en todo el programa, evitando múltiples creaciones innecesarias.

### **Contra:**
**Sobrecarga de Complejidad**: Aunque el patrón **Singleton** puede ser útil en ciertos casos, **añade complejidad** en proyectos pequeños o situaciones donde no es necesario tener una única instancia, y también puede dificultar las pruebas unitarias, ya que la instancia única puede generar dependencias inesperadas.

<div id="conclusión"></div>

# Conclusión
Los patrones de diseño GOF son herramientas poderosas que mejoran la calidad del software cuando se aplican adecuadamente. No obstante, es fundamental usarlos con criterio, considerando sus limitaciones, para maximizar sus beneficios y evitar sobrecomplicar los sistemas.

	

</body>


