
![DALL·E 2024-10-01 20 15 11 - A modern mascot for Ansible, representing software automation and design patterns, not in the form of a robot but as a friendly character with a sleek copy](https://github.com/user-attachments/assets/8790c7a2-a0e7-4be4-b715-732c96026849)



# Ansible Playbook para Instalar .NET Core 8 en Ubuntu 24 LTS

A continuación, te presento un ejemplo de un **Ansible Playbook** que puedes utilizar para instalar .NET Core 8 en un servidor Ubuntu 24 LTS. Este playbook también incluye los pasos para abrir el puerto 5000 en el grupo de seguridad y crear una aplicación básica que representa un sistema de viñedos.

#### Estructura del Proyecto


1. **Contenido de `playbook.yml`:**
   Este es el Ansible Playbook para instalar .NET Core 8 y abrir el puerto 5000 en el grupo de seguridad.
```yaml
---
- name: Instalar .NET Core 8 y configurar la aplicación de viñedos
  hosts: localhost
  become: yes
  tasks:
    - name: Actualizar el sistema
      apt:
        update_cache: yes
        upgrade: dist

    - name: Instalar dependencias necesarias
      apt:
        name:
          - apt-transport-https
          - ca-certificates
          - curl
          - software-properties-common
        state: present

    - name: Descargar el paquete de repositorio de Microsoft para Ubuntu 24.04 LTS
      command: wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O /tmp/packages-microsoft-prod.deb

    - name: Instalar el paquete de repositorio de Microsoft
      command: dpkg -i /tmp/packages-microsoft-prod.deb

    - name: Actualizar el caché de apt después de agregar el repositorio
      apt:
        update_cache: yes

    - name: Instalar .NET SDK
      apt:
        name: dotnet-sdk-8.0
        state: present

    - name: Crear directorio para la aplicación
      file:
        path: /home/ubuntu/vineyard_app
        state: directory

    - name: Crear archivo del proyecto
      copy:
        dest: /home/ubuntu/vineyard_app/vineyard_app.csproj
        content: |
          <Project Sdk="Microsoft.NET.Sdk.Web">

          <PropertyGroup>
              <TargetFramework>net8.0</TargetFramework>
          </PropertyGroup>

          </Project>

    - name: Crear archivo de código fuente
      copy:
        dest: /home/ubuntu/vineyard_app/Program.cs
        content: |
          using System;
          using System.Collections.Generic;
          using Microsoft.AspNetCore.Hosting;
          using Microsoft.Extensions.Hosting;
          using Microsoft.AspNetCore.Builder;
          using Microsoft.Extensions.DependencyInjection;

          namespace VineyardApp
          {
              public class Program
              {
                  public static void Main(string[] args)
                  {
                      CreateHostBuilder(args).Build().Run();
                  }

                  public static IHostBuilder CreateHostBuilder(string[] args) =>
                      Host.CreateDefaultBuilder(args)
                          .ConfigureWebHostDefaults(webBuilder =>
                          {
                              webBuilder.UseStartup<Startup>();
                          });
              }

              public class Startup
              {
                  public void ConfigureServices(IServiceCollection services)
                  {
                      services.AddControllers();
                  }

                  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
                  {
                      if (env.IsDevelopment())
                      {
                          app.UseDeveloperExceptionPage();
                      }
                      else
                      {
                          app.UseExceptionHandler("/Home/Error");
                          app.UseHsts();
                      }
                      app.UseHttpsRedirection();
                      app.UseRouting();
                      app.UseAuthorization();
                      app.UseEndpoints(endpoints =>
                      {
                          endpoints.MapControllers();
                      });
                  }
              }

              public class Vineyard
              {
                  public string Name { get; set; }
                  public string Location { get; set; }
                  public List<string> Grapes { get; set; }

                  public void PrintDetails()
                  {
                      Console.WriteLine($"Vineyard: {Name}, Location: {Location}, Grapes: {string.Join(", ", Grapes)}");
                  }
              }

              public class VineyardManager
              {
                  private List<Vineyard> vineyards = new List<Vineyard>();

                  public void AddVineyard(string name, string location, List<string> grapes)
                  {
                      var vineyard = new Vineyard { Name = name, Location = location, Grapes = grapes };
                      vineyards.Add(vineyard);
                      vineyard.PrintDetails();
                  }
              }
          }

    - name: Construir la aplicación de viñedos
      command: dotnet build /home/ubuntu/vineyard_app/vineyard_app.csproj
      args:
        chdir: /home/ubuntu/vineyard_app

    - name: Ejecutar la aplicación
      command: dotnet run --urls "http://0.0.0.0:5000"
      args:
        chdir: /home/ubuntu/vineyard_app
      async: 10
      poll: 0

   ```

2. **Refactorizar el Código:**
   El estudiante deberá identificar secciones del código de `VineyardManager` que se pueden refactorizar usando patrones de diseño, como el patrón de **Fábrica** o **Singleton**, para mejorar la calidad y la mantenibilidad del código.

3. **Ver la Aplicación en Ejecución:**
   Una vez que el playbook se ejecute correctamente, el estudiante podrá acceder a la aplicación en el navegador utilizando la dirección `http://your_server_ip:5000`.

### Notas Finales
- Recuerde checar su "security groups" para abrie el puerto 5000/tcp
- Usar sus notas de exposiciones para aplicar GoF a la solución spagetti
- Ofresco una disculpa por este método de aplicar problemas a resolver, estoy disponible para cualquier mejora para el grupo y la generación.
