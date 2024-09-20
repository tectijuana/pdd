# Chain of Responsibility

## Introducción

El patrón **Chain of Responsibility** es un patrón de diseño de comportamiento que permite pasar solicitudes a lo largo de una cadena de manejadores. Cada manejador decide si procesa la solicitud o si la pasa al siguiente manejador de la cadena. Este patrón se usa comúnmente para desacoplar el emisor de una solicitud de su receptor, proporcionando así flexibilidad en la asignación de responsabilidades.

## Ejemplo (Problema)

### Problema
Imagina que en un sistema de bases de datos de una institución educativa, como el **Instituto Tecnológico de Tijuana**, se requiere procesar solicitudes de verificación de créditos de tutoría para estudiantes de diferentes semestres. El sistema necesita verificar los créditos de acuerdo con diferentes reglas, como el número de créditos obtenidos por semestre y el tipo de actividad realizada. ¿Cómo podemos organizar estas verificaciones para que no estén acopladas a una sola clase o función?

## Solución

El patrón **Chain of Responsibility** permite dividir el proceso de verificación en diferentes manejadores independientes. Cada manejador puede verificar un aspecto específico de la solicitud, y si no es capaz de procesarla, la pasa al siguiente manejador en la cadena.

Al usar este patrón, cada regla de validación se convierte en un manejador autónomo, permitiendo añadir o eliminar reglas sin afectar a las demás. Esto es especialmente útil en sistemas de verificación de datos donde las reglas pueden cambiar o ser extensibles.

## Objetivos

- Desacoplar el emisor de solicitudes de los receptores.
- Facilitar la adición o modificación de manejadores.
- Mejorar la escalabilidad del sistema de verificación de solicitudes.

## Estructura

- **Cliente**: El objeto que envía la solicitud.
- **Manejador (Handler)**: Interfaz que define un método para procesar la solicitud o pasarla al siguiente manejador.
- **Manejadores Concretos**: Implementaciones del manejador que procesan la solicitud o la pasan a otro manejador.
- **Cadena de Manejadores**: Conjunto de manejadores enlazados en secuencia.

![Estructura del Patrón](https://upload.wikimedia.org/wikipedia/commons/7/76/CoR_UML_class_diagram.svg)

## Analogía en el mundo real

Imagina que en el sistema de gestión de tutorías del **Instituto Tecnológico de Tijuana**, los estudiantes envían solicitudes para la verificación de créditos. Estas solicitudes deben pasar por varias verificaciones, como la cantidad de créditos obtenidos, la validez de las actividades realizadas y la autenticidad de los documentos proporcionados.

Cada verificación puede ser tratada como un manejador. Si un manejador no puede verificar una solicitud, la pasa al siguiente hasta que se complete la validación.

## Pseudocódigo

# Ejemplo del Patrón Chain of Responsibility en C#

Este ejemplo demuestra el patrón de diseño **Chain of Responsibility** (Cadena de Responsabilidad) en C#. El patrón permite que un objeto pase una solicitud a lo largo de una cadena de manejadores potenciales hasta que la solicitud sea manejada.

## Código

```csharp
using System;

namespace ChainOfResponsibilityExample
{
    // Clase base para los manejadores de soporte
    public abstract class SupportHandler
    {
        // Manejador siguiente en la cadena
        protected SupportHandler nextHandler;

        // Establece el siguiente manejador en la cadena
        public void SetNext(SupportHandler handler)
        {
            nextHandler = handler;
        }

        // Método abstracto que debe implementar cada manejador concreto
        public abstract void HandleRequest(SupportRequest request);
    }

    // Clase que representa una solicitud de soporte
    public class SupportRequest
    {
        public string Problem { get; private set; }
        public int DifficultyLevel { get; private set; }  // Nivel de dificultad: 1 = bajo, 2 = medio, 3 = alto

        // Constructor para inicializar las propiedades
        public SupportRequest(string problem, int difficultyLevel)
        {
            Problem = problem;
            DifficultyLevel = difficultyLevel;
        }
    }

    // Manejador concreto para problemas simples
    public class BasicSupport : SupportHandler
    {
        // Implementación del método HandleRequest para problemas de dificultad 1
        public override void HandleRequest(SupportRequest request)
        {
            if (request.DifficultyLevel == 1)
            {
                Console.WriteLine(string.Format("BasicSupport: Resolvió el problema '{0}' de dificultad {1}", request.Problem, request.DifficultyLevel));
            }
            else if (nextHandler != null)
            {
                // Pasa la solicitud al siguiente manejador en la cadena
                nextHandler.HandleRequest(request);
            }
        }
    }

    // Manejador concreto para problemas intermedios
    public class IntermediateSupport : SupportHandler
    {
        // Implementación del método HandleRequest para problemas de dificultad 2
        public override void HandleRequest(SupportRequest request)
        {
            if (request.DifficultyLevel == 2)
            {
                Console.WriteLine(string.Format("IntermediateSupport: Resolvió el problema '{0}' de dificultad {1}", request.Problem, request.DifficultyLevel));
            }
            else if (nextHandler != null)
            {
                // Pasa la solicitud al siguiente manejador en la cadena
                nextHandler.HandleRequest(request);
            }
        }
    }

    // Manejador concreto para problemas avanzados
    public class AdvancedSupport : SupportHandler
    {
        // Implementación del método HandleRequest para problemas de dificultad 3
        public override void HandleRequest(SupportRequest request)
        {
            if (request.DifficultyLevel == 3)
            {
                Console.WriteLine(string.Format("AdvancedSupport: Resolvió el problema '{0}' de dificultad {1}", request.Problem, request.DifficultyLevel));
            }
            else
            {
                // Mensaje si el problema no puede ser resuelto por este manejador
                Console.WriteLine(string.Format("AdvancedSupport: El problema '{0}' no pudo ser resuelto.", request.Problem));
            }
        }
    }

    // Clase principal, debe ser pública
    public class Program
    {
        // Método Main, debe ser estático y público
        public static void Main(string[] args)
        {
            // Configuración de la cadena de responsabilidad
            SupportHandler basicSupport = new BasicSupport();
            SupportHandler intermediateSupport = new IntermediateSupport();
            SupportHandler advancedSupport = new AdvancedSupport();

            // Establecemos la cadena: basicSupport -> intermediateSupport -> advancedSupport
            basicSupport.SetNext(intermediateSupport);
            intermediateSupport.SetNext(advancedSupport);

            // Simulamos diferentes solicitudes de soporte
            SupportRequest request1 = new SupportRequest("Reiniciar contraseña", 1);
            SupportRequest request2 = new SupportRequest("Problema con la conexión a internet", 2);
            SupportRequest request3 = new SupportRequest("Servidor caído", 3);
            SupportRequest request4 = new SupportRequest("Ciberataque en curso", 4);

            // Procesamos las solicitudes a través de la cadena
            basicSupport.HandleRequest(request1);
            basicSupport.HandleRequest(request2);
            basicSupport.HandleRequest(request3);
            basicSupport.HandleRequest(request4);

            Console.ReadLine();  // Para mantener la consola abierta
        }
    }
}
