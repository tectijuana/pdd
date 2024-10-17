
![DALL·E 2024-10-01 20 15 11 - A modern mascot for Ansible, representing software automation and design patterns, not in the form of a robot but as a friendly character with a sleek copy](https://github.com/user-attachments/assets/8790c7a2-a0e7-4be4-b715-732c96026849)



# Tutorial: Uso de Asciinema en Ubuntu 24.04 LTS para Estudiantes de AWS Academy

Este tutorial enseñará cómo instalar y usar **asciinema** en Ubuntu 24.04 LTS para grabar sesiones de terminal mientras modificas código en entornos como AWS Academy. Asciinema es una herramienta ligera que permite grabar y compartir sesiones de terminal de manera eficiente, ideal para documentar y compartir el progreso de tus proyectos.

## Índice

- [Instalación de Asciinema](#instalación-de-asciinema)
- [Grabación de Sesiones](#grabación-de-sesiones)
- [Subir y Compartir Grabaciones](#subir-y-compartir-grabaciones)
- [Ejemplo de Uso con AWS Academy](#ejemplo-de-uso-con-aws-academy)
- [Consideraciones Finales](#consideraciones-finales)

## Instalación de Asciinema

1. **Actualizar los paquetes del sistema**

   Antes de instalar asciinema, asegúrate de que tu sistema esté actualizado:

   ```bash
   sudo apt update && sudo apt upgrade -y
   ```

2. **Instalar asciinema**

   Asciinema está disponible en los repositorios oficiales de Ubuntu, por lo que puedes instalarlo con el siguiente comando:

   ```bash
   sudo apt install asciinema -y
   ```

3. **Verificar la instalación**

   Una vez que la instalación esté completa, puedes verificar que asciinema esté instalado correctamente ejecutando:

   ```bash
   asciinema --version
   ```

   Deberías ver algo como `asciinema 2.x.x`.

## Grabación de Sesiones

### Iniciar una grabación

Para comenzar a grabar tu sesión de terminal, simplemente ejecuta:

```bash
asciinema rec
```

### Finalizar la grabación

Cuando hayas terminado, presiona `Ctrl+D`, preguntara si desea sUbirlo a Asciinema.org

Si no especificaste un archivo para la grabación, al finalizar, asciinema te ofrecerá subirla automáticamente a su plataforma:

   ```bash
   asciinema rec
   ```

   Después de detener la grabación, asciinema te dará un enlace que puedes compartir.

   Debe Ud de recuperar el enlace y colocandolo en su navegador, de ahi su correo electronico para reclamar la propiedad del programa.

3. **Compartir la grabación**

   Una vez subida, en asciinema.org puedes compartir el enlace generado con tus profesores o compañeros de clase. Si es necesario, puedes incrustar la grabación en tu sitio web o documentación.

## Ejemplo de Uso con AWS Academy

Supongamos que estás trabajando en AWS Academy y necesitas modificar un archivo de código en una instancia de EC2. El proceso podría ser algo como esto:

  Inicia la grabación con:

   ```bash
   asciinema rec 
   ```
1. **Conectar a tu instancia EC2**:
   ```bash
   ssh -i "tu_clave.pem" ubuntu@tu-instancia-ec2.amazonaws.com
   ```

2. **Modificar el archivo de código**:
   ```bash
   nano mi_proyecto.py
   ```

3. **Probar el código**:
   ```bash
   python3 mi_proyecto.py
   ```


   Luego, interactúa con tu instancia de AWS EC2, modifica el código y prueba los resultados. Una vez terminado, detén la grabación con `Ctrl+D` y `(UPLOAD)`.

   Si prefieres, sube la grabación y comparte el enlace para demostrar tu progreso en el proyecto.

## Consideraciones Finales

- **Compatibilidad con otras plataformas**: Aunque este tutorial se basa en Ubuntu, asciinema es compatible con otras distribuciones de Linux y macOS.
- **Privacidad**: Si trabajas con información sensible, asegúrate de que no se graben claves o credenciales en tus sesiones.
- **Colaboración en equipo**: Asciinema facilita la colaboración permitiendo a otros ver exactamente lo que has hecho, sin necesidad de estar presente.

Asciinema es una herramienta excelente para documentar tu flujo de trabajo, especialmente en proyectos colaborativos o cuando trabajas en entornos en la nube como AWS Academy.
