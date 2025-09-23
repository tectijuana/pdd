

## 🐾 Clínica Veterinaria y Hotel de Mascotas - Práctica de Patrones de Diseño Creacionales

**Directorio sugerido en GitHub**:
`/practicas/creacionales/clinicaVeterinariaHotelMascotas`

---

### 🎯 Objetivo de la práctica

Simular el diseño y posterior refactorización de una aplicación que gestiona una **clínica veterinaria** y un **hotel de mascotas**, aplicando **patrones de diseño creacionales** (Factory Method, Abstract Factory, Singleton, Builder, Prototype). Se busca que los estudiantes identifiquen problemas en un código espagueti inicial y propongan soluciones con base en los patrones GoF.

---

## 🧪 1. Descripción de la práctica

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

---

## 📚 3. Referencias recomendadas para el alumno

* Refactoring.Guru: Builder, Factory, Abstract Factory
* Libro: *Dive Into Design Patterns* – Alexander Shvets
* Libro: *Software Engineering with UML* – Bhuvan Unhelkar (capítulos sobre Use Cases y clases)
* Libro: *Peeling Design Patterns* – Narasimha Karumanchi (capítulos de Factory y Singleton)

