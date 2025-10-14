<div align="center">
<img width="466" height="312" alt="image" src="https://github.com/user-attachments/assets/5ecb9fb6-4c67-4bb8-bed1-6f280a0b7f03" />
</div>

## 🐾 Clínica Veterinaria y Hotel de Mascotas - Práctica de Patrones de Diseño Creacionales

---

### 🎯 Objetivo de la práctica

Simular el diseño y posterior refactorización de una aplicación que gestiona una **clínica veterinaria** y un **hotel de mascotas**, aplicando **patrones de diseño creacionales** (Factory Method, Abstract Factory, Singleton, Builder, Prototype). Se busca que los estudiantes identifiquen problemas en un código espagueti inicial y propongan soluciones con base en los patrones GoF.


## 🎯 Objetivo especifico

Refactorizar un proyecto inicial espagueti aplicando patrones como:

- 🏭 Factory Method
- 🧱 Abstract Factory
- 🧑‍🎨 Builder
- 👤 Singleton
- 🧬 Prototype

---

## 🧪 Descripción de la práctica

Una aplicación espagueti que gestiona:

* Registro de mascotas (perros, gatos, aves).
* Asignación de habitaciones en el hotel.
* Registro de servicios médicos.
* Facturación y servicios complementarios (baño, comida especial, etc.).

🧨 **Problemas deliberados (10 sugerencias):**

1. Código duplicado al crear instancias de diferentes tipos de mascotas.
2. Alta dependencia entre clases concretas.
3. Mala separación entre lógica médica y hotelera.
4. Inexistencia de una clase de configuración centralizada.
5. Método `Main()` con lógica operativa.
6. Clases monolíticas sin separación de responsabilidades.
7. Enumeraciones mal utilizadas para distinguir tipos de servicios.
8. Propiedades públicas expuestas sin encapsulación.
9. Métodos largos y difíciles de probar.
10. No existe una fábrica para instanciar habitaciones personalizadas.

---

Este proyecto es una simulación sencilla de una aplicación de clínica veterinaria y hotel de mascotas, escrita en C# con .NET 8.0, diseñada para ser **refactorizada** y para aplicar **patrones de diseño GoF** del tipo creacional.

---

## 🧪 Actividades sugeridas

1. Identificar responsabilidades mal asignadas.
2. Aplicar patrón `Abstract Factory` para crear mascotas.
3. Usar `Singleton` para controlar la instancia del hotel.
4. Reorganizar clases usando SRP y DIP.
5. Documentar los cambios realizados.

---

## ⚙️ Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Editor recomendado: Visual Studio Code o Rider

---

## ▶️ Ejecución

```bash
cd clinica_mascotas_app
dotnet build
dotnet run
````

---

## 📚 Referencias

* Refactoring.Guru: [https://refactoring.guru/es/design-patterns](https://refactoring.guru/es/design-patterns)
* Dive Into Design Patterns – Alexander Shvets

---

> ✍️ Recuerda grabar un video de tu refactorización o subir screenshots del antes/después si así lo solicita el docente.

## 📚 3. Referencias recomendadas para el alumno

* Refactoring.Guru: Builder, Factory, Abstract Factory
* Libro: *Dive Into Design Patterns* – Alexander Shvets
* Libro: *Software Engineering with UML* – Bhuvan Unhelkar (capítulos sobre Use Cases y clases)
* Libro: *Peeling Design Patterns* – Narasimha Karumanchi (capítulos de Factory y Singleton)
  
---

## ✅ Rúbrica de Evaluación – Clínica Veterinaria y Hotel de Mascotas

| Criterio                       | Excelente (5 pts)                                                                                        | Bueno (3 pts)                                           | Insuficiente (1 pt)                                       | Puntos  |
| ------------------------------ | -------------------------------------------------------------------------------------------------------- | ------------------------------------------------------- | --------------------------------------------------------- | ------- |
| Aplicación de Patrones GoF     | Usa al menos 4 patrones correctamente: Factory Method, Abstract Factory, Singleton, Builder o Prototype. | Usa 1–2 patrones pero con errores menores.              | No se aplican patrones correctamente o no se identifican. |         |
| Refactorización                | Código completamente limpio y modularizado, sin duplicaciones.                                           | Parcialmente refactorizado, con algunos métodos largos. | Código espagueti sin mejoras significativas.              |         |
| Estructura del Proyecto        | Separación clara entre lógica de clínica, hotel y servicios. Buen uso de clases y namespaces.            | Estructura aceptable pero acoplada.                     | Mezcla responsabilidades y no sigue estructura limpia.    |         |
| Comentarios y buenas prácticas | Código comentado en español, buenas prácticas de C#.                                                     | Comentarios escasos o poco claros.                      | Sin comentarios, código difícil de entender.              |         |
| Documentación y entrega        | `README.md` completo con explicación, instrucciones de ejecución y dependencias.                         | Documentación incompleta o sin formato.                 | No hay documentación clara.                               |         |
| **Total**                      |                                                                                                          |                                                         |                                                           | **/25** |

---


## ⚙️ 2. Ansible Playbook Base

```yaml
---
- name: Instalar .NET 8 y preparar entorno para Clínica Veterinaria y Hotel de Mascotas
  hosts: localhost
  become: yes
  tasks:
    - name: Actualizar sistema
      apt:
        update_cache: yes
        upgrade: dist

    - name: Instalar herramientas necesarias
      apt:
        name:
          - apt-transport-https
          - ca-certificates
          - curl
          - software-properties-common
        state: present

    - name: Descargar repositorio de Microsoft
      command: wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O /tmp/packages-microsoft-prod.deb

    - name: Instalar repositorio de Microsoft
      command: dpkg -i /tmp/packages-microsoft-prod.deb

    - name: Actualizar caché apt
      apt:
        update_cache: yes

    - name: Instalar .NET SDK 8.0
      apt:
        name: dotnet-sdk-8.0
        state: present

    - name: Crear directorio del proyecto
      file:
        path: /home/ubuntu/clinica_mascotas_app
        state: directory

    - name: Crear archivo del proyecto
      copy:
        dest: /home/ubuntu/clinica_mascotas_app/clinica_mascotas_app.csproj
        content: |
          <Project Sdk="Microsoft.NET.Sdk.Web">
            <PropertyGroup>
              <TargetFramework>net8.0</TargetFramework>
            </PropertyGroup>
          </Project>

    - name: Crear código base con errores comunes para refactorizar
      copy:
        dest: /home/ubuntu/clinica_mascotas_app/Program.cs
        content: |
          using System;
          using System.Collections.Generic;

          namespace ClinicaMascotas
          {
              public class Program
              {
                  public static void Main(string[] args)
                  {
                      // Aquí hay una mezcla de responsabilidades: hotel, veterinaria, facturación...
                      var dog = new Mascota("Firulais", "Perro");
                      var cat = new Mascota("Mishi", "Gato");

                      var servicio = new ServicioVeterinario();
                      servicio.RegistrarConsulta(dog.Nombre);
                      servicio.RegistrarVacuna(cat.Nombre);

                      var hotel = new HotelMascotas();
                      hotel.AsignarHabitacion(dog.Nombre);
                      hotel.AsignarHabitacion(cat.Nombre);
                  }
              }

              public class Mascota
              {
                  public string Nombre;
                  public string Tipo;

                  public Mascota(string nombre, string tipo)
                  {
                      Nombre = nombre;
                      Tipo = tipo;
                  }
              }

              public class ServicioVeterinario
              {
                  public void RegistrarConsulta(string mascota)
                  {
                      Console.WriteLine($"Consulta registrada para {mascota}");
                  }

                  public void RegistrarVacuna(string mascota)
                  {
                      Console.WriteLine($"Vacuna aplicada a {mascota}");
                  }
              }

              public class HotelMascotas
              {
                  public void AsignarHabitacion(string mascota)
                  {
                      Console.WriteLine($"Habitación asignada a {mascota}");
                  }
              }
          }

    - name: Compilar la aplicación
      command: dotnet build /home/ubuntu/clinica_mascotas_app/clinica_mascotas_app.csproj
      args:
        chdir: /home/ubuntu/clinica_mascotas_app

    - name: Ejecutar la aplicación
      command: dotnet run
      args:
        chdir: /home/ubuntu/clinica_mascotas_app
      async: 10
      poll: 0
```
## Codigo Corregido 
``` c sharp
using System;
using System.Collections.Generic;

namespace ClinicaMascotas
{
    // ====================================================================
    // INTERFACES BASE
    // ====================================================================

    // Producto Base para Factory Method
    public interface IMascota
    {
        string Nombre { get; }
        void Jugar();
    }

    // Productos Base para Abstract Factory
    public interface IServicio
    {
        string TipoServicio();
    }

    public interface IHabitacion
    {
        void Asignar(string nombreMascota);
    }

    // Producto Complejo (Builder) y Prototype
    public interface IFichaIngreso : ICloneable
    {
        void MostrarResumen();
    }

    // ====================================================================
    // 1. PATRÓN SINGLETON 👤 (Sistema Central de Control)
    // ====================================================================

    /// <summary>
    /// Patrón Singleton: Garantiza una única instancia del sistema central 
    /// para gestionar recursos (ej. inventario de habitaciones).
    /// </summary>
    public sealed class SistemaCentral
    {
        // Implementación thread-safe (Inicialización estática)
        private static readonly SistemaCentral instancia = new SistemaCentral();

        private int _habitacionesDisponibles = 10;

        private SistemaCentral() => Console.WriteLine("[SINGLETON] Sistema Central de Gestión inicializado.");

        public static SistemaCentral ObtenerInstancia()
        {
            return instancia;
        }

        /// <summary>
        /// Simula la reserva y control de recursos.
        /// </summary>
        public bool ReservarHabitacion()
        {
            if (_habitacionesDisponibles > 0)
            {
                _habitacionesDisponibles--;
                Console.WriteLine($"[SINGLETON] Habitación reservada. Quedan {_habitacionesDisponibles} disponibles.");
                return true;
            }
            return false;
        }
    }

    // ====================================================================
    // 2. PATRÓN FACTORY METHOD 🏭 (Creación de Mascotas)
    // ====================================================================

    // --- Productos Concretos ---
    public class Perro : IMascota
    {
        public string Nombre { get; private set; }
        public Perro(string nombre) => Nombre = nombre;
        public void Jugar() => Console.WriteLine($"Perro ({Nombre}): Requiere paseo y juegos de búsqueda.");
    }

    public class Gato : IMascota
    {
        public string Nombre { get; private set; }
        public Gato(string nombre) => Nombre = nombre;
        public void Jugar() => Console.WriteLine($"Gato ({Nombre}): Le gusta el silencio y perseguir el láser.");
    }

    // --- Creador Abstracto y Concretos ---
    public abstract class CreadorMascota
    {
        public abstract IMascota CrearMascota(string nombre);

        public IMascota RegistrarNuevaMascota(string nombre)
        {
            IMascota mascota = CrearMascota(nombre); // Factory Method

            if (SistemaCentral.ObtenerInstancia().ReservarHabitacion())
            {
                Console.WriteLine($"[FACTORY] Se registró a {mascota.Nombre} y se le asignó un espacio.");
            }
            return mascota;
        }
    }

    public class CreadorPerro : CreadorMascota
    {
        public override IMascota CrearMascota(string nombre) => new Perro(nombre);
    }

    public class CreadorGato : CreadorMascota
    {
        public override IMascota CrearMascota(string nombre) => new Gato(nombre);
    }

    // ====================================================================
    // 3. PATRÓN ABSTRACT FACTORY 🧱 (Hotel y Servicios)
    // ====================================================================

    // --- Productos Concretos de Hotel ---
    public class ServicioBaño : IServicio { public string TipoServicio() => "Baño Estándar"; }
    public class ServicioGrooming : IServicio { public string TipoServicio() => "Grooming de Lujo (Corte y Tratamiento)"; }

    public class HabitacionBasica : IHabitacion
    {
        public void Asignar(string nombreMascota) => Console.WriteLine($"[HOTEL] Asignado a {nombreMascota} en Habitación Básica.");
    }
    public class HabitacionVip : IHabitacion
    {
        public void Asignar(string nombreMascota) => Console.WriteLine($"[HOTEL] Asignado a {nombreMascota} en Suite VIP con cámaras.");
    }

    /// <summary>
    /// Fábrica Abstracta: Define la familia de productos (Habitación + Servicio).
    /// </summary>
    public interface IHotelFactory
    {
        IHabitacion CrearHabitacion();
        IServicio CrearServicioComplementario();
    }

    public class HotelEconomicoFactory : IHotelFactory
    {
        public IHabitacion CrearHabitacion() => new HabitacionBasica();
        public IServicio CrearServicioComplementario() => new ServicioBaño();
    }

    public class HotelLujoFactory : IHotelFactory
    {
        public IHabitacion CrearHabitacion() => new HabitacionVip();
        public IServicio CrearServicioComplementario() => new ServicioGrooming();
    }

    // ====================================================================
    // 4. PATRÓN BUILDER 🧑‍🎨 & PROTOTYPE 🧬 (Ficha de Ingreso)
    // ====================================================================

    /// <summary>
    /// Producto Complejo: Ficha de Ingreso. Implementa Prototype (ICloneable).
    /// </summary>
    public class FichaIngreso : IFichaIngreso
    {
        public string Mascota { get; set; } = "N/A";
        public string Habitacion { get; set; } = "Pendiente";
        public string Servicios { get; set; } = "Ninguno";
        public List<string> Observaciones { get; set; } = new List<string>();

        public void MostrarResumen()
        {
            Console.WriteLine($"\n--- Ficha de Ingreso de {Mascota} ---");
            Console.WriteLine($"  Habitación: {Habitacion}");
            Console.WriteLine($"  Servicios: {Servicios}");
            Console.WriteLine($"  Obs: {string.Join("; ", Observaciones)}");
            Console.WriteLine("--------------------------------------");
        }

        /// <summary>
        /// Implementación de Prototype: Clonación profunda de la lista de Observaciones.
        /// </summary>
        public object Clone()
        {
            FichaIngreso clon = (FichaIngreso)this.MemberwiseClone();
            // Clonación profunda de la lista para asegurar la independencia
            clon.Observaciones = new List<string>(this.Observaciones);

            Console.WriteLine($"[PROTOTYPE] Ficha de {this.Mascota} clonada exitosamente.");
            return clon;
        }
    }

    // Interfaz del Builder
    public interface IFichaBuilder
    {
        void Reset();
        void SetMascota(IMascota mascota);
        void SetTipoHabitacion(string tipo);
        void AddServicio(IServicio servicio);
        void AddObservacionMedica(string obs);
        FichaIngreso ObtenerFicha();
    }

    // Builder Concreto
    public class FichaBasicaBuilder : IFichaBuilder
    {
        private FichaIngreso _ficha;

        public FichaBasicaBuilder() => this.Reset();
        public void Reset() => this._ficha = new FichaIngreso();

        public void SetMascota(IMascota mascota) => this._ficha.Mascota = mascota.Nombre;
        public void SetTipoHabitacion(string tipo) => this._ficha.Habitacion = tipo;
        public void AddServicio(IServicio servicio) => this._ficha.Servicios = servicio.TipoServicio();
        public void AddObservacionMedica(string obs) => this._ficha.Observaciones.Add(obs);

        public FichaIngreso ObtenerFicha()
        {
            FichaIngreso resultado = this._ficha;
            this.Reset();
            return resultado;
        }
    }

    // Clase Director (Podría usarse para simplificar la construcción, omitida aquí para claridad)

    // ====================================================================
    // 5. PROGRAMA PRINCIPAL (ORQUESTACIÓN Y DEMOSTRACIÓN)
    // ====================================================================

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine(" DEMOSTRACIÓN DEL SISTEMA REFACTORIZADO (5 PATRONES) ");
            Console.WriteLine("=================================================");

            // 🔒 SINGLETON: Acceso al sistema central.
            var sistemaCentral = SistemaCentral.ObtenerInstancia();

            // 🏭 FACTORY METHOD: Creación polimórfica de mascotas.
            Console.WriteLine("\n--- 1. Registro de Mascotas (Factory Method) ---");
            CreadorMascota creadorPerro = new CreadorPerro();
            IMascota firulais = creadorPerro.RegistrarNuevaMascota("Firulais");

            CreadorMascota creadorGato = new CreadorGato();
            IMascota mishi = creadorGato.RegistrarNuevaMascota("Mishi");

            // 🧱 ABSTRACT FACTORY: Creación de familias coherentes de Hotel + Servicios.
            Console.WriteLine("\n--- 2. Asignación de Hotel (Abstract Factory) ---");
            IHotelFactory fabricaLujo = new HotelLujoFactory();

            IHabitacion habVip = fabricaLujo.CrearHabitacion();
            IServicio servicioLujo = fabricaLujo.CrearServicioComplementario();
            habVip.Asignar(firulais.Nombre);
            Console.WriteLine($"  Servicio complementario: {servicioLujo.TipoServicio()}");

            // 🧑‍🎨 BUILDER: Construcción controlada de la Ficha de Ingreso (objeto complejo).
            Console.WriteLine("\n--- 3. Creación de Ficha (Builder) ---");

            IFichaBuilder builderFicha = new FichaBasicaBuilder();
            builderFicha.SetMascota(firulais);
            builderFicha.SetTipoHabitacion("Suite VIP");
            builderFicha.AddServicio(servicioLujo);
            builderFicha.AddObservacionMedica("Vacuna al día.");
            builderFicha.AddObservacionMedica("Necesita atención especial por ansiedad.");

            var fichaFirulais = builderFicha.ObtenerFicha();
            fichaFirulais.MostrarResumen();

            // 🧬 PROTOTYPE: Clonar la ficha compleja de Firulais para otro perro similar.
            Console.WriteLine("\n--- 4. Clonación de Ficha (Prototype) ---");

            var fichaClonada = (FichaIngreso)fichaFirulais.Clone();
            fichaClonada.Mascota = "Rex (Nueva Mascota)"; // Se cambia solo el nombre
            fichaClonada.Observaciones[0] = "Vacuna pendiente."; // Se modifica una observación

            fichaClonada.MostrarResumen();

            Console.WriteLine("\nFIN del proceso. Se aplicaron los 5 patrones creacionales.");
            Console.WriteLine("=================================================");
        }
    }
}
```
