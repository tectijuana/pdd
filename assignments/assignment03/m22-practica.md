Para adaptar el *playbook* de Ansible y cumplir con la propuesta de convertir el programa de ventas de celulares en C# a una versión web en Ubuntu usando ASP.NET Core, puedes seguir el siguiente ajuste al *playbook* original. Esto incluirá la instalación de las dependencias necesarias y la creación de la aplicación web en ASP.NET Core en la instancia de Amazon EC2.

### Ansible Playbook: Instalación y Configuración para una Aplicación Web en C#

```yaml
---
- hosts: amazon_ec2
  become: true
  tasks:
    - name: Actualizar los paquetes en la instancia
      apt:
        update_cache: yes
        upgrade: dist

    - name: Instalar dependencias para .NET Core
      apt:
        name: "{{ item }}"
        state: present
      with_items:
        - apt-transport-https
        - ca-certificates
        - gnupg

    - name: Agregar la llave GPG de Microsoft
      apt_key:
        url: https://packages.microsoft.com/keys/microsoft.asc
        state: present

    - name: Agregar el repositorio de Microsoft para .NET Core
      apt_repository:
        repo: 'deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-focal-prod focal main'
        state: present

    - name: Instalar .NET Core SDK
      apt:
        name: dotnet-sdk-5.0
        state: present

    - name: Crear el directorio de la aplicación web
      file:
        path: /home/ubuntu/MiAppWeb
        state: directory

    - name: Crear una aplicación web en ASP.NET Core
      command: 'dotnet new webapp -o MiAppWeb'
      args:
        chdir: /home/ubuntu

    - name: Publicar la aplicación en modo Release
      command: 'dotnet publish --configuration Release'
      args:
        chdir: /home/ubuntu/MiAppWeb

    - name: Ejecutar la aplicación web en segundo plano
      command: 'dotnet run &'
      args:
        chdir: /home/ubuntu/MiAppWeb

    - name: Instalar Nginx para servir la aplicación
      apt:
        name: nginx
        state: present

    - name: Configurar Nginx como proxy inverso para ASP.NET Core
      copy:
        dest: /etc/nginx/sites-available/default
        content: |
          server {
              listen 80;
              server_name localhost;

              location / {
                  proxy_pass http://localhost:5000;
                  proxy_http_version 1.1;
                  proxy_set_header Upgrade $http_upgrade;
                  proxy_set_header Connection keep-alive;
                  proxy_set_header Host $host;
                  proxy_cache_bypass $http_upgrade;
              }
          }

    - name: Reiniciar Nginx para aplicar los cambios
      service:
        name: nginx
        state: restarted

    - name: Crear usuarios para los estudiantes
      user:
        name: "{{ item }}"
        state: present
        groups: sudo
      with_items:
        - estudiante1
        - estudiante2
        - estudiante3
```

### Explicación del Playbook

1. **Actualización del Sistema**: Actualiza los paquetes en la instancia EC2.
2. **Instalación de .NET Core**: Se agregan las claves GPG y los repositorios necesarios para instalar el SDK de .NET Core, que es esencial para desarrollar y ejecutar aplicaciones web en C#.
3. **Creación de la Aplicación Web**: Se genera una aplicación básica de ASP.NET Core con el comando `dotnet new webapp`.
4. **Publicación de la Aplicación**: El código de la aplicación se compila en modo *Release* para prepararlo para producción.
5. **Ejecución de la Aplicación**: Se ejecuta el servidor web Kestrel de ASP.NET Core en segundo plano en el puerto 5000.
6. **Instalación de Nginx**: Se instala y configura Nginx como proxy inverso para que la aplicación ASP.NET Core sea accesible en el puerto 80 de la instancia.
7. **Configuración de Proxy Inverso**: Nginx redirige las solicitudes HTTP a la aplicación ASP.NET Core que corre en el puerto 5000.
8. **Reinicio de Nginx**: Se reinicia Nginx para aplicar la configuración.
9. **Creación de Usuarios**: Se crean usuarios para los estudiantes que accederán a la instancia.

### Configuración del Entorno en AWS EC2

- **Configuración de la instancia EC2**: Utiliza una instancia de Amazon EC2 con Ubuntu y un grupo de seguridad que permita el tráfico HTTP en el puerto 80 para que la aplicación sea accesible desde un navegador.
- **Acceso de los estudiantes**: Los estudiantes podrán acceder a la instancia EC2 para trabajar en el código, aplicar patrones de diseño GoF y mejorar la arquitectura del programa.

Con este *playbook*, tendrás la aplicación web de ventas de celulares corriendo en tu instancia EC2 y servida por Nginx, lista para que los estudiantes la modifiquen y optimicen con patrones de diseño. ¿Te gustaría ajustar alguna parte o agregar funcionalidades adicionales?
