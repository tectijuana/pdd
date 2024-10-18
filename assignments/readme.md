![asciinema](https://github.com/user-attachments/assets/cefe7f1d-ea8c-4e85-81f5-b13902319c4f)

# Asciinema para las entregas de las intervesiones de PPD GoF

Estamos haciendo lo posible para subir la calidad del trabajo que esta atendiendo, para esto es la garantia que se revisa sus técnicas que van incrementandose de nuevas habilidades, todo esto es la memoria del trabajo realizado. Asciinema permite como docente ver superar las dificultades en ocaciones básicas del dominio de la práctica.

Queda una copia en su perfil del portal, y un enlace compartido para el docente; asi recordando y aprendiendo de esos caminos que le llevo a dar un resultado acertado en su trabajo académico.

Para crear un tutorial sobre cómo usar **asciinema** en Ubuntu 24.04 LTS con el objetivo de ayudar a estudiantes que necesitan modificar código en AWS Academy, podemos seguir estos pasos. Este tutorial se enfocará en la instalación de asciinema, la grabación de sesiones de terminal, y la forma de compartir estas grabaciones. Además, incluiremos ejemplos prácticos y consideraciones para trabajar con AWS.

# Tutorial: Uso de Asciinema en Ubuntu 24.04 LTS para Estudiantes de AWS Academy

Este tutorial te enseñará cómo instalar y usar **asciinema** en Ubuntu 24.04 LTS para grabar sesiones de terminal mientras modificas código en entornos como AWS Academy. Asciinema es una herramienta ligera que permite grabar y compartir sesiones de terminal de manera eficiente, ideal para documentar y compartir el progreso de tus proyectos.

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

Este comando iniciará la grabación. Si deseas guardar la grabación en un archivo local, puedes especificar el nombre del archivo:

```bash
asciinema rec nombre_de_tu_grabacion.cast
```

### Finalizar la grabación

Cuando hayas terminado, presiona `Ctrl+D` o escribe `exit` para detener la grabación. Si grabaste sin un archivo específico, asciinema te preguntará si deseas subir la grabación a su plataforma.

### Ejemplo de grabación

Un ejemplo típico en el que podrías usar asciinema es para mostrar cómo modificas archivos de código y pruebas tus cambios en la terminal:

```bash
asciinema rec modificando_codigo_en_AWSAcademy.cast
```

Durante la grabación, puedes modificar código, ejecutar scripts, o interactuar con los recursos en AWS.

## Subir y Compartir Grabaciones

1. **Subir a asciinema.org**

   Si no especificaste un archivo para la grabación, al finalizar, asciinema te ofrecerá subirla automáticamente a su plataforma:

   ```bash
   asciinema rec
   ```

   Después de detener la grabación, asciinema te dará un enlace que puedes compartir.

2. **Guardar localmente y subir manualmente**

   Si prefieres guardar la grabación en tu máquina local primero, puedes hacerlo especificando un archivo al grabar, como se mostró antes. Luego, si decides subir la grabación más tarde, usa:

   ```bash
   asciinema upload nombre_de_tu_grabacion.cast
   ```

   Esto generará un enlace donde tu grabación estará disponible.

3. **Compartir la grabación**

   Una vez subida, puedes compartir el enlace generado con tus profesores o compañeros de clase. Si es necesario, puedes incrustar la grabación en tu sitio web o documentación.

## Ejemplo de Uso con AWS Academy

Supongamos que estás trabajando en AWS Academy y necesitas modificar un archivo de código en una instancia de EC2. El proceso podría ser algo como esto:

1. **Conectar a tu instancia EC2**:
   ```bash
   ssh -i "tu_clave.pem" ubuntu@tu-instancia-ec2.amazonaws.com
   ```

2. **Modificar el archivo de código**:
   ```bash
   # Tambien puede trabajar en VStudio o con GIST, etc.
   
   nano mi_proyecto.py
   ```

3. **Probar el código**:
   ```bash
   python3 mi_proyecto.py
   dot net run
   ...
   ...
   ...
   
   ```
   

4. **Grabar esta sesión**:

   Inicia la grabación con:

   ```bash
   asciinema rec sesion_modificando_AWSAcademy.cast
   ```

   Luego, interactúa con tu instancia de AWS EC2, modifica el código y prueba los resultados. Una vez terminado, detén la grabación con `Ctrl+D` o `exit`.

   Si prefieres, sube la grabación y comparte el enlace para demostrar tu progreso en el proyecto.

## Consideraciones Finales

- **Compatibilidad con otras plataformas**: Aunque este tutorial se basa en Ubuntu, asciinema es compatible con otras distribuciones de Linux y macOS.
- **Privacidad**: Si trabajas con información sensible, asegúrate de que no se graben claves o credenciales en tus sesiones.
- **Colaboración en equipo**: Asciinema facilita la colaboración permitiendo a otros ver exactamente lo que has hecho, sin necesidad de estar presente.

Asciinema es una herramienta excelente para documentar tu flujo de trabajo, especialmente en proyectos colaborativos o cuando trabajas en entornos en la nube como AWS Academy.



Este archivo `README.md` es una guía concisa para estudiantes que buscan grabar sus sesiones de trabajo en AWS usando asciinema en Ubuntu. Debería ayudarles a documentar su progreso y compartir fácilmente su trabajo con compañeros y profesores.
