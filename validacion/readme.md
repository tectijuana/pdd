Nota: Asegúrate de tener instalado qrencode. Puedes instalarlo en la mayoría de las distribuciones de Linux con:

sudo apt install qrencode  # En distribuciones basadas en Debian/Ubuntu
sudo yum install qrencode  # En distribuciones basadas en RedHat

validation.sh

#!/bin/bash

# Verifica si qrencode está instalado
if ! command -v qrencode &> /dev/null; then
    echo "Error: qrencode no está instalado. Por favor instálalo e intenta de nuevo."
    exit 1
fi

# Solicita el nombre del estudiante
read -p "Ingresa tu nombre: " nombre_estudiante

# Obtiene la fecha y hora actual en formato mexicano (DD/MM/AAAA y formato de 24 horas)
fecha_actual=$(date +"%d/%m/%Y")
hora_actual=$(date +"%H:%M:%S")

# Concatena el nombre, fecha y hora
contenido_qr="$nombre_estudiante a las $fecha_actual $hora_actual"

# Genera el código QR
archivo_salida="${nombre_estudiante// /_}_qr.png"
qrencode -o "$archivo_salida" "$contenido_qr"

echo "Código QR generado y guardado como $archivo_salida"

Explicación

	1.	Entrada del usuario: Solicita al estudiante que ingrese su nombre.
	2.	Formato de fecha y hora: Usa el comando date para obtener la fecha en DD/MM/AAAA y la hora en HH:MM:SS.
	3.	Concatenación: Combina el nombre del estudiante con la fecha y hora.
	4.	Generación del código QR: Usa qrencode para crear el código QR con ese contenido y lo guarda como una imagen PNG.
	5.	Nombre del archivo: El código QR se guarda usando el nombre del estudiante (reemplazando los espacios por guiones bajos).

Uso

	1.	Guarda el script como validation.sh.
	2.	Hazlo ejecutable:

chmod +x validation.sh


	3.	Ejecuta el script:

./validation.sh



Este script generará una imagen con un código QR que contiene el nombre del estudiante, la fecha y la hora, guardada en el directorio actual.
