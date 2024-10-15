# ZenUML para Ejercicios del Curso de Patrones de Diseño GoF

## Introducción a ZenUML

ZenUML es una herramienta innovadora en línea que facilita la creación de diagramas de secuencia usando una sintaxis simple y basada en texto. Es especialmente útil para aquellos que trabajan con patrones de diseño, como los Patrones de Diseño GoF (Gang of Four), ya que permite ilustrar cómo los objetos interactúan a lo largo del tiempo. ZenUML utiliza un estilo de diagrama simplificado y legible que permite a los estudiantes y desarrolladores centrarse en los conceptos fundamentales sin la complejidad que a veces se encuentra en las herramientas tradicionales de diagramación.

### Ventajas de ZenUML en Línea

- **Facilidad de Uso**: ZenUML permite la creación rápida de diagramas de secuencia simplemente escribiendo código similar a pseudocódigo. No requiere una curva de aprendizaje pronunciada.
- **Visualización en Tiempo Real**: Los diagramas se generan a medida que se escribe el código, lo que facilita la retroalimentación inmediata y permite iterar los diseños de manera eficiente.
- **Integración**: ZenUML se puede integrar con varias plataformas de desarrollo y entornos de documentación, como Confluence, facilitando su uso en equipos colaborativos.
- **Nuevo Estilo de Diagrama**: ZenUML adopta un estilo moderno que se enfoca en la simplicidad y claridad, dejando de lado la complejidad innecesaria de otros sistemas. Este estilo es especialmente adecuado para ilustrar patrones como Singleton, Command, Chain of Responsibility, entre otros.

### Ejemplo Extendido: Patrón Singleton

El patrón Singleton es un patrón de diseño creacional que asegura que una clase tenga una única instancia y proporciona un punto de acceso global a ella. ZenUML puede ayudar a visualizar la interacción para asegurar que solo se cree una instancia de la clase.

A continuación se presenta un ejemplo extendido del código del patrón Singleton en Java y cómo se puede representar su diagrama de secuencia en ZenUML.

#### Ejemplo de Código Singleton en Csharp

```csharp
using System;

public class ConfiguracionSistema
{
    // Instancia única de la clase, inicializada de manera lazy
    private static ConfiguracionSistema instancia;

    // Bloqueo para asegurar que el acceso sea thread-safe
    private static readonly object bloqueo = new object();

    // Constructor privado para evitar instancias externas
    private ConfiguracionSistema()
    {
        Console.WriteLine("Configuración del sistema inicializada.");
    }

    // Método público para obtener la instancia única
    public static ConfiguracionSistema ObtenerInstancia()
    {
        // Doble verificación de la instancia para eficiencia
        if (instancia == null)
        {
            lock (bloqueo)
            {
                if (instancia == null)
                {
                    instancia = new ConfiguracionSistema();
                }
            }
        }
        return instancia;
    }

    // Método de ejemplo que utiliza la configuración del sistema
    public void MostrarConfiguracion()
    {
        Console.WriteLine("Mostrando configuración del sistema...");
    }
}

public class Cliente
{
    public static void Main(string[] args)
    {
        // Obtener la instancia del Singleton
        ConfiguracionSistema config1 = ConfiguracionSistema.ObtenerInstancia();
        config1.MostrarConfiguracion();

        // Obtener la instancia nuevamente para verificar que es la misma
        ConfiguracionSistema config2 = ConfiguracionSistema.ObtenerInstancia();
        config2.MostrarConfiguracion();

        // Verificar si ambas instancias son la misma
        Console.WriteLine($"Las instancias son iguales: {config1 == config2}");
    }
}

```

#### Diagrama de Secuencia en ZenUML

![zenuml-2](https://github.com/user-attachments/assets/560f552d-2f2f-460d-8b46-8b87fdabc5e3)



En ZenUML, podrías representar el comportamiento del método `getInstancia` para ilustrar cómo se asegura que solo exista una única instancia de la clase `Singleton`.

```zenuml
// Simulando una solicitud para obtener la configuración del sistema
Cliente.ObtenerInstancia() {
  // Verificar si la instancia es nula
  if (instancia == null) {
    // Entrar en la sección crítica con lock (para asegurar thread safety)
    lock (bloqueo) {
      // Verificar nuevamente la instancia dentro del bloqueo
      if (instancia == null) {
        // Crear una nueva instancia de ConfiguracionSistema
        new ConfiguracionSistema()
      }
    }
  }
  // Retornar la instancia única
  return instancia
}

// Cliente muestra la configuración del sistema
Cliente.MostrarConfiguracion() {
  // Llamada al método que muestra la configuración
  ConfiguracionSistema.MostrarConfiguracion()
}

// Cliente vuelve a solicitar la instancia del Singleton
Cliente.ObtenerInstancia() {
  return instancia
}

// Cliente vuelve a mostrar la configuración del sistema
Cliente.MostrarConfiguracion() {
  ConfiguracionSistema.MostrarConfiguracion()
}




```



¿Cómo se refleja en el diagrama?
El cliente llama al método ObtenerInstancia.
Se verifica si la instancia es null. Si es así, se entra en una sección crítica con lock para evitar que otro hilo cree la instancia simultáneamente.
Si la instancia sigue siendo null, se crea una nueva instancia.
El cliente obtiene la misma instancia y llama a MostrarConfiguracion.
Este diagrama visualiza claramente cómo se asegura que solo haya una instancia de ConfiguracionSistema.

¿Cómo probarlo?
Tus estudiantes pueden pegar este DSL en https://app.zenuml.com para ver el diagrama de secuencia renderizado.

Con esto tendrás un ejemplo más práctico, tanto en el código como en el diagrama, que muestra el uso del patrón Singleton en un caso típico de gestión de configuración.

¿Listo para comenzar a diagramar tus patrones GoF con ZenUML? Para la documentacion en GitHub
