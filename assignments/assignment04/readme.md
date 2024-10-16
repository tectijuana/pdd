### Ansible Playbook para Instalar .NET Core 8 en Ubuntu 24 LTS

A continuación, te presento un ejemplo de un **Ansible Playbook** que puedes utilizar para instalar .NET Core 8 en un servidor Ubuntu 24 LTS. Este playbook también incluye los pasos para abrir el puerto 5000 en el grupo de seguridad y crear una aplicación básica que representa un sistema de viñedos.

#### Estructura del Proyecto

1. **Directorio del Proyecto:**
   ```
   ansible-playbook-dotnet/
   ├── inventory
   ├── playbook.yml
   └── vineyard_app/
       ├── Program.cs
       └── vineyard_app.csproj
   ```

2. **Contenido de `inventory`:**
   Asegúrate de que tu archivo `inventory` apunte a tu servidor en AWS:
   ```ini
   [aws]
   your_server_ip ansible_ssh_user=ubuntu
   ```

3. **Contenido de `playbook.yml`:**
   Este es el Ansible Playbook para instalar .NET Core 8 y abrir el puerto 5000 en el grupo de seguridad.
   ```yaml
   ---
   - name: Instalar .NET Core 8 y configurar la aplicación de viñedos
     hosts: aws
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

       - name: Agregar el repositorio de Microsoft
         apt_repository:
           repo: "deb [arch=amd64] https://packages.microsoft.com/repos/ubuntu/ $(lsb_release -cs) main"
           state: present

       - name: Importar la clave de Microsoft
         apt_key:
           url: https://packages.microsoft.com/keys/microsoft.asc
           state: present

       - name: Instalar .NET SDK
         apt:
           name: dotnet-sdk-8.0
           state: present

       - name: Crear directorio para la aplicación
         file:
           path: /home/ubuntu/vineyard_app
           state: directory

       - name: Copiar los archivos de la aplicación
         copy:
           src: vineyard_app/
           dest: /home/ubuntu/vineyard_app/
           owner: ubuntu
           group: ubuntu
           mode: '0755'

       - name: Construir la aplicación de viñedos
         command: dotnet build /home/ubuntu/vineyard_app/vineyard_app.csproj
         args:
           chdir: /home/ubuntu/vineyard_app

       - name: Ejecutar la aplicación
         command: dotnet run --urls "http://0.0.0.0:5000" &
         args:
           chdir: /home/ubuntu/vineyard_app

       - name: Abrir puerto 5000 en el grupo de seguridad
         ec2_group:
           name: your_security_group_name
           region: your_aws_region
           rules:
             - proto: tcp
               from_port: 5000
               to_port: 5000
               cidr_ip: 0.0.0.0/0
           state: present
   ```

4. **Contenido de `vineyard_app/Program.cs`:**
   Aquí tienes un código de ejemplo de un programa "espagueti" que representa una aplicación de viñedos. Se requiere que los estudiantes refactoricen este código utilizando un patrón de diseño de la GoF.
   ```csharp
   using System;
   using System.Collections.Generic;
   using Microsoft.AspNetCore.Hosting;
   using Microsoft.Extensions.Hosting;

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

       // Sección de código espagueti
       public class VineyardManager
       {
           private List<Vineyard> vineyards = new List<Vineyard>();

           public void AddVineyard(string name, string location, List<string> grapes)
           {
               var vineyard = new Vineyard { Name = name, Location = location, Grapes = grapes };
               vineyards.Add(vineyard);
               vineyard.PrintDetails(); // Método que puede mejorarse
           }
       }
   }
   ```

### Instrucciones para el Estudiante

1. **Ejecutar el Playbook:**
   Inicia sesión en el servidor a través de SSH y ejecuta el playbook con el siguiente comando:
   ```bash
   ansible-playbook -i inventory playbook.yml
   ```

2. **Refactorizar el Código:**
   El estudiante deberá identificar secciones del código de `VineyardManager` que se pueden refactorizar usando patrones de diseño, como el patrón de **Fábrica** o **Singleton**, para mejorar la calidad y la mantenibilidad del código.

3. **Ver la Aplicación en Ejecución:**
   Una vez que el playbook se ejecute correctamente, el estudiante podrá acceder a la aplicación en el navegador utilizando la dirección `http://your_server_ip:5000`.

### Notas Finales
Este ejercicio no solo les enseñará a los estudiantes sobre el uso de Ansible para la automatización de la instalación de software, sino que también les proporcionará la oportunidad de practicar la refactorización de código en C# aplicando patrones de diseño.
