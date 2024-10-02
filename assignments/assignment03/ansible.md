
![DALL·E 2024-10-01 20 15 11 - A modern mascot for Ansible, representing software automation and design patterns, not in the form of a robot but as a friendly character with a sleek copy](https://github.com/user-attachments/assets/cdc8f197-0b67-4d6b-bb55-62eba7c03fbb)



## **Micro Tutorial: Automatización con Ansible en EC2 Ubuntu**

### **1. Introducción a Ansible y Amazon EC2 Ubuntu**

**Ansible** es una herramienta de automatización de TI que permite gestionar y configurar múltiples servidores de manera eficiente y reproducible. Utiliza **YAML** para definir **playbooks**, lo que facilita la creación de scripts legibles y mantenibles.

**Amazon EC2 (Elastic Compute Cloud)** es un servicio de AWS que proporciona instancias de servidores virtuales en la nube. **Ubuntu** es una de las distribuciones de Linux más populares y ampliamente utilizadas en entornos de servidores debido a su estabilidad y facilidad de uso.

### **2. Importancia de Ansible en las Operaciones de TI Modernas**

- **Automatización Simplificada:** Permite automatizar tareas repetitivas, reduciendo errores humanos y aumentando la eficiencia.
- **Infraestructura como Código (IaC):** Gestiona la infraestructura mediante código, facilitando la versión, reutilización y colaboración.
- **Escalabilidad:** Gestiona fácilmente múltiples servidores, lo que es esencial para aplicaciones que escalan horizontalmente.
- **Consistencia y Reproducibilidad:** Asegura que las configuraciones sean consistentes en todos los entornos, desde desarrollo hasta producción.

### **3. Prácticas de Laboratorio: Configuración y Uso de Ansible en EC2 Ubuntu**

#### **Paso 1: Configuración de la Instancia EC2 Ubuntu**

1. **Crear una Instancia EC2:**
   - Inicia sesión en tu cuenta de AWS.
   - Navega a **EC2 Dashboard** y selecciona **Launch Instance**.
   - Elige **Ubuntu Server** como AMI (Amazon Machine Image).
   - Selecciona el tipo de instancia (por ejemplo, **t2.micro** para pruebas gratuitas).
   - Configura detalles como almacenamiento, etiquetas y grupo de seguridad (asegúrate de abrir los puertos necesarios, como SSH y HTTP).
   - Revisa y lanza la instancia, descargando la clave PEM para acceder vía SSH.

2. **Conectar a la Instancia:**
   ```bash
   ssh -i /ruta/a/tu-clave.pem ubuntu@<IP-Pública-de-tu-EC2>
   ```

#### **Paso 2: Instalación de Ansible en tu Máquina Local**

1. **Actualizar el Sistema:**
   ```bash
   sudo apt update
   sudo apt upgrade -y
   ```

2. **Instalar Ansible:**
   ```bash
   sudo apt install ansible -y
   ```

3. **Verificar la Instalación:**
   ```bash
   ansible --version
   ```

#### **Paso 3: Configuración de Ansible para Gestionar la Instancia EC2**

1. **Configurar el Archivo de Inventario:**
   Crea un archivo llamado `hosts.ini` con el siguiente contenido:
   ```ini
   [ec2_instances]
   servidor_ec2 ansible_host=<IP-Pública-de-tu-EC2> ansible_user=ubuntu ansible_ssh_private_key_file=/ruta/a/tu-clave.pem
   ```

2. **Probar la Conexión:**
   ```bash
   ansible -i hosts.ini ec2_instances -m ping
   ```
   Deberías recibir una respuesta `pong`, indicando que Ansible puede comunicarse con la instancia.

#### **Paso 4: Crear y Ejecutar un Playbook de Ansible**

1. **Crear un Playbook Básico:**
   Crea un archivo llamado `configurar_servidor.yaml` con el siguiente contenido:
   ```yaml
   ---
   - name: Configurar Servidor Ubuntu en EC2
     hosts: ec2_instances
     become: yes

     tasks:
       - name: Actualizar el cache de APT
         apt:
           update_cache: yes

       - name: Instalar Nginx
         apt:
           name: nginx
           state: present

       - name: Iniciar y habilitar el servicio Nginx
         systemd:
           name: nginx
           state: started
           enabled: yes

       - name: Crear una página web simple
         copy:
           dest: /var/www/html/index.html
           content: |
             <html>
             <head><title>Bienvenido</title></head>
             <body>
               <h1>¡Hola desde Ansible y EC2 Ubuntu!</h1>
             </body>
             </html>
   ```

2. **Ejecutar el Playbook:**
   ```bash
   ansible-playbook -i hosts.ini configurar_servidor.yaml
   ```

3. **Verificar la Configuración:**
   - Abre un navegador web y navega a `http://<IP-Pública-de-tu-EC2>`.
   - Deberías ver la página "¡Hola desde Ansible y EC2 Ubuntu!".

### **4. Ventajas de Aprender y Usar Ansible con EC2 Ubuntu para tu Carrera**

- **Demanda en el Mercado Laboral:** Las habilidades en automatización y gestión de infraestructura son altamente valoradas en roles de DevOps, administración de sistemas y desarrollo de software.
- **Eficiencia Operacional:** Dominar Ansible te permite gestionar entornos complejos de manera eficiente, lo que es crucial para empresas que manejan infraestructuras a gran escala.
- **Flexibilidad y Adaptabilidad:** Ansible es agnóstico a la plataforma, lo que te permite trabajar con diversos sistemas operativos y servicios en la nube.
- **Mejora Continua y Escalabilidad:** La capacidad de automatizar despliegues y configuraciones facilita la implementación de prácticas de integración y entrega continua (CI/CD).
- **Colaboración y Gestión de Configuraciones:** Facilita la colaboración entre equipos y asegura que las configuraciones sean consistentes y versionadas, reduciendo conflictos y errores.
- **Crecimiento Profesional:** Adquirir conocimientos en herramientas de automatización como Ansible abre oportunidades para roles avanzados y proyectos más desafiantes en tu carrera profesional.

### **5. Buenas Prácticas en el Uso de Ansible**

- **Uso de Roles:** Organiza tus playbooks en roles para mejorar la reutilización y mantenibilidad.
- **Versionamiento:** Usa sistemas de control de versiones como Git para gestionar tus playbooks y cambios.
- **Variables y Plantillas:** Utiliza variables y plantillas Jinja2 para hacer tus playbooks más flexibles y configurables.
- **Pruebas y Validación:** Prueba tus playbooks en entornos de desarrollo antes de aplicarlos en producción.
- **Documentación:** Mantén una buena documentación de tus playbooks y roles para facilitar el entendimiento y colaboración.

### **6. Recursos Adicionales**

- **Documentación Oficial de Ansible:** [Ansible Documentation](https://docs.ansible.com/)
- **Guía de AWS para EC2:** [Amazon EC2 Documentation](https://docs.aws.amazon.com/ec2/)
- **Tutoriales de Ansible para Principiantes:** [Ansible Getting Started](https://www.ansible.com/resources/get-started)
- **Cursos y Certificaciones:**
  - **Udemy:** Cursos sobre Ansible y AWS.
  - **Coursera:** Especializaciones en DevOps y automatización.
  - **Certificaciones de AWS:** AWS Certified DevOps Engineer.

