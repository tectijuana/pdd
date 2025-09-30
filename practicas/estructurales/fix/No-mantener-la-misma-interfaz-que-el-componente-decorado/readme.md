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
