# Decorator
un *decorator* (decorador) es un patrón de diseño estructural que permite añadir responsabilidades o comportamientos adicionales a un objeto de manera dinámica, sin modificar su código fuente original.

👉 Se utiliza mucho para extender funcionalidades sin tener que heredar ni modificar la clase existente.


### Un decorador:

* Mantiene la misma interfaz que el objeto que decora.

* Envuelve al objeto original y puede interceptar o extender sus métodos.

* Permite composición flexible en lugar de herencia rígida.


## Codigo malo
```C#
          using System;
          using System.Collections.Generic;

          namespace SchoolApp
          {
              // Interfaz base
              public interface IEstudiante
              {
                  string Nombre();
                  double Calificacion();
              }

              // Clase concreta
              public class EstudianteBasico : IEstudiante
              {
                  public string Nombre() => "Ana";
                  public double Calificacion() => 8.5;
              }

              // Decorador mal diseñado ❌
              // No implementa la misma interfaz IEstudiante
              public class EstudianteConClub
              {
                  private IEstudiante estudiante;

                  public EstudianteConClub(IEstudiante estudiante)
                  {
                      this.estudiante = estudiante;
                  }

                  // Métodos con nombres distintos ❌
                  public string InfoDetallada() => estudiante.Nombre() + " - Club de Ciencias";
                  public double NotaFinal() => estudiante.Calificacion() + 0.5;
              }

              // Otro decorador mal diseñado ❌
              public class EstudianteConBeca
              {
                  private IEstudiante estudiante;

                  public EstudianteConBeca(IEstudiante estudiante)
                  {
                      this.estudiante = estudiante;
                  }

                  // Métodos inconsistentes ❌
                  public string Mostrar() => estudiante.Nombre() + " (Becado)";
                  public double Ponderacion() => estudiante.Calificacion() + 1.0;
              }

              public class Program
              {
                  public static void Main(string[] args)
                  {
                      var estudiante = new EstudianteBasico();
                      Console.WriteLine($"Estudiante: {estudiante.Nombre()}, Nota: {estudiante.Calificacion()}");

                      // Uso de decoradores mal hechos ❌
                      var club = new EstudianteConClub(estudiante);
                      Console.WriteLine(club.InfoDetallada() + " - Nota: " + club.NotaFinal());

                      var beca = new EstudianteConBeca(estudiante);
                      Console.WriteLine(beca.Mostrar() + " - Nota: " + beca.Ponderacion());

                      // 🚨 Problema: No puedo tratarlos como IEstudiante de manera uniforme
                  }
              }
          }
```


## 1. Identificación de problemas
Se detectaron los siguientes problemas estructurales en el código original:
- ❌ Los decoradores (`EstudianteConClub`, `EstudianteConBeca`) **no implementaban la interfaz `IEstudiante`**, rompiendo el Principio de Sustitución de Liskov.
- ❌ Métodos inconsistentes: cada decorador introducía nombres distintos (`InfoDetallada`, `NotaFinal`, `Mostrar`, `Ponderacion`), lo cual dificulta la uniformidad de uso.
- ❌ El cliente no podía tratar a los objetos decorados como `IEstudiante`, eliminando la flexibilidad y rompiendo el propósito del patrón Decorator.


## Version Corregida
``` c#
using System;

namespace SchoolApp
{
    // Interfaz base
    public interface IEstudiante
    {
        string Nombre();
        double Calificacion();
    }

    // Clase concreta
    public class EstudianteBasico : IEstudiante
    {
        public string Nombre() => "Ana";
        public double Calificacion() => 8.5;
    }

    // Decorador base: implementa la interfaz y delega
    public abstract class EstudianteDecorator : IEstudiante
    {
        protected IEstudiante estudiante;

        public EstudianteDecorator(IEstudiante estudiante)
        {
            this.estudiante = estudiante;
        }

        public virtual string Nombre() => estudiante.Nombre();
        public virtual double Calificacion() => estudiante.Calificacion();
    }

    // Decorador concreto: añade club
    public class EstudianteConClub : EstudianteDecorator
    {
        public EstudianteConClub(IEstudiante estudiante) : base(estudiante) { }

        public override string Nombre() => base.Nombre() + " - Club de Ciencias";
        public override double Calificacion() => base.Calificacion() + 0.5;
    }

    // Decorador concreto: añade beca
    public class EstudianteConBeca : EstudianteDecorator
    {
        public EstudianteConBeca(IEstudiante estudiante) : base(estudiante) { }

        public override string Nombre() => base.Nombre() + " (Becado)";
        public override double Calificacion() => base.Calificacion() + 1.0;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            IEstudiante estudiante = new EstudianteBasico();
            Console.WriteLine($"Estudiante: {estudiante.Nombre()}, Nota: {estudiante.Calificacion()}");

            // Ahora los decoradores funcionan bien ✅
            estudiante = new EstudianteConClub(estudiante);
            Console.WriteLine($"Estudiante: {estudiante.Nombre()}, Nota: {estudiante.Calificacion()}");

            estudiante = new EstudianteConBeca(estudiante);
            Console.WriteLine($"Estudiante: {estudiante.Nombre()}, Nota: {estudiante.Calificacion()}");
        }
    }
}
```
### Aplicación del patrón adecuado

El patrón Decorator es el más apropiado porque:

* Mantiene la interfaz común `IEstudiante`.

* Permite extender dinámicamente el comportamiento de los estudiantes (añadir club, beca, etc.) sin modificar el código original.

* Evita herencia rígida y promueve la composición flexible.

### Salida
<img width="518" height="77" alt="image" src="https://github.com/user-attachments/assets/683745fb-7ab6-409f-a8f8-9fb31582a470" />


## Este refactor corrige la mala aplicación del patrón:

* Problema original: decoradores no implementaban la interfaz y usaban métodos inconsistentes.

* Solución: aplicación del patrón Decorator con una clase base abstracta (EstudianteDecorator) que delega en el componente.

* Beneficios: flexibilidad para extender, uniformidad en la interfaz, mayor mantenibilidad y cumplimiento de principios SOLID (LSP, OCP).
