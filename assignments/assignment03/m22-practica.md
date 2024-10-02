¡Entiendo! A continuación, ajusto la propuesta para que sea un problema que el estudiante debe resolver: refactorizar código espagueti en una aplicación web en .NET Core sobre Ubuntu. Además, proporcionaremos un *playbook* de Ansible para configurar el entorno necesario en una instancia de Amazon EC2 a través de AWS Academy.

---

### **Descripción de la Actividad para el Estudiante**

**Título:** Refactorización de Código Espagueti en una Aplicación Web en .NET Core

**Objetivo:** Los estudiantes deberán mejorar y refactorizar una aplicación web escrita en código espagueti, aplicando patrones de diseño GoF (Gang of Four) para mejorar su estructura y mantenibilidad.

**Entorno:** La aplicación y el entorno de desarrollo estarán configurados en una instancia de Ubuntu en Amazon EC2, utilizando Ansible para la automatización de la instalación.

---

### **Instrucciones para el Estudiante**

1. **Acceso al Servidor:**
   - Accede a la instancia de Amazon EC2 que ha sido configurada para esta actividad.
   - Las credenciales y detalles de acceso te serán proporcionados por el instructor.

2. **Explora la Aplicación Web:**
   - La aplicación web en .NET Core se encuentra en el directorio `/home/ubuntu/app-web-espagueti`.
   - Ejecuta la aplicación para familiarizarte con su funcionalidad básica:
     ```bash
     cd /home/ubuntu/app-web-espagueti
     dotnet run
     ```
   - Navega a `http://localhost:5000` (o la dirección IP pública de la instancia) para ver la aplicación en funcionamiento.

3. **Analiza el Código Espagueti:**
   - Revisa el código fuente de la aplicación.
   - Identifica problemas comunes asociados con el código espagueti, como:
     - Falta de modularidad.
     - Funciones y clases con múltiples responsabilidades.
     - Código duplicado.
     - Dificultad para entender y seguir el flujo de ejecución.

4. **Aplica Patrones de Diseño GoF:**
   - Selecciona patrones de diseño apropiados para refactorizar el código. Algunos patrones sugeridos:
     - **Factory Method**: Para crear instancias de objetos de manera más flexible.
     - **Singleton**: Si hay necesidad de una única instancia global.
     - **Observer**: Para manejar eventos y notificaciones.
     - **Strategy**: Para encapsular algoritmos intercambiables.
   - Refactoriza el código paso a paso, asegurándote de:
     - Mejorar la legibilidad y mantenibilidad.
     - No alterar la funcionalidad original de la aplicación.
     - Documentar los cambios realizados y justificar la elección de cada patrón aplicado.

5. **Prueba la Aplicación Refactorizada:**
   - Ejecuta nuevamente la aplicación y verifica que todas las funcionalidades operen correctamente.
   - Realiza pruebas adicionales para asegurarte de que no se introdujeron nuevos errores.

6. **Documenta tu Trabajo:**
   - Prepara un informe que incluya:
     - Análisis inicial del código espagueti y los problemas identificados.
     - Patrones de diseño seleccionados y justificación de su uso.
     - Descripción de los cambios realizados en el código.
     - Conclusiones sobre cómo mejoró la aplicación después de la refactorización.

---

### **Playbook de Ansible para Configuración del Entorno**

Este *playbook* de Ansible automatiza la instalación del entorno necesario en la instancia de Ubuntu en Amazon EC2.

```yaml
---
- name: Configurar entorno para aplicación web en .NET Core
  hosts: amazon_ec2
  become: true
  tasks:
    - name: Actualizar paquetes e instalar dependencias
      apt:
        update_cache: yes
        name:
          - apt-transport-https
          - ca-certificates
          - gnupg
        state: present

    - name: Agregar clave GPG de Microsoft
      apt_key:
        url: https://packages.microsoft.com/keys/microsoft.asc
        state: present

    - name: Agregar repositorio de paquetes de Microsoft
      apt_repository:
        repo: 'deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-focal-prod focal main'
        state: present

    - name: Instalar .NET Core SDK
      apt:
        name: dotnet-sdk-5.0
        state: present

    - name: Clonar repositorio con código espagueti
      git:
        repo: 'https://github.com/tu-usuario/app-web-espagueti.git'
        dest: /home/ubuntu/app-web-espagueti
        version: master

    - name: Cambiar propietario del directorio de la aplicación
      file:
        path: /home/ubuntu/app-web-espagueti
        owner: ubuntu
        group: ubuntu
        recurse: yes

    - name: Habilitar el puerto 5000 en el firewall
      ufw:
        rule: allow
        port: 5000
        proto: tcp

    - name: Iniciar la aplicación (opcional)
      shell: |
        cd /home/ubuntu/app-web-espagueti
        dotnet run &
```

**Nota:** Asegúrate de reemplazar `'https://github.com/tu-usuario/app-web-espagueti.git'` con la URL real del repositorio que contiene el código espagueti.

---

### **Detalles Adicionales**

- **Código Espagueti Inicial:**

  El código proporcionado está intencionalmente mal estructurado para simular un entorno real donde se requiere refactorización. Aquí hay un fragmento de ejemplo:

  ```csharp
  using System;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.Hosting;

  namespace AppWebEspagueti
  {
      public class Program
      {
          public static void Main(string[] args)
          {
              var host = Host.CreateDefaultBuilder(args)
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder.Configure(app =>
                      {
                          app.Run(async context =>
                          {
                              string response = "";
                              string action = context.Request.Query["action"];
                              if (action == "list")
                              {
                                  response = "Listado de celulares: iPhone, Samsung, Xiaomi";
                              }
                              else if (action == "buy")
                              {
                                  string item = context.Request.Query["item"];
                                  response = $"Has comprado: {item}";
                              }
                              else
                              {
                                  response = "Bienvenido a la tienda de celulares. Usa '?action=list' para ver productos.";
                              }
                              await context.Response.WriteAsync(response);
                          });
                      });
                  })
                  .Build();

              host.Run();
          }
      }
  }
  ```

- **Objetivo de la Refactorización:**

  - Separar la lógica de negocios de la lógica de presentación.
  - Implementar controladores y vistas adecuados si se migra a un modelo MVC.
  - Aplicar patrones como **MVC**, **Factory Method**, o **Strategy** para mejorar la estructura del código.

---

### **Evaluación**

- **Criterios de Evaluación:**
  - Calidad de la refactorización y correcta aplicación de los patrones de diseño.
  - Mantenimiento de la funcionalidad original de la aplicación.
  - Claridad y profundidad del informe presentado.
  - Buenas prácticas de programación y documentación del código.

- **Entrega:**
  - Código fuente refactorizado.
  - Informe en formato PDF o Word.
  - Fecha límite: [Especificar fecha].

---

### **Recursos Adicionales**

- **Documentación de .NET Core:** [https://docs.microsoft.com/es-es/dotnet/core/](https://docs.microsoft.com/es-es/dotnet/core/)
- **Patrones de Diseño GoF:** [Refactoring.Guru en Español](https://refactoring.guru/es/design-patterns)
- **Introducción a Ansible:** [https://docs.ansible.com/ansible/latest/user_guide/index.html](https://docs.ansible.com/ansible/latest/user_guide/index.html)
