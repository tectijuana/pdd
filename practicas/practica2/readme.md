# Práctica 2: Instalación de lenguajes en AWS Ubuntu EC2 y despliegue de *Hola Mundo*

> 📚 Esta práctica sigue los lineamientos de uso responsable de IA del TecNM – Campus TecTijuana. Consulta la guía oficial aquí:
> [Guía de uso de IA en el curso](https://github.com/tectijuana/sp/blob/main/AI_GUIDANCE.md)

## Objetivo

Configurar una instancia **Ubuntu Server en AWS EC2** desde la **Consola web** y preparar el entorno con **7 lenguajes populares (ranking 2025)**, verificando su instalación con un **despliegue de "Hola Mundo"** por cada lenguaje. La evidencia de todo el flujo se registrará y compartirá mediante **Asciinema**.

## Unidad / Tema

**Curso:** Patrones de Diseño (TIB-2006)
**Unidad sugerida:** Fundamentos y entorno de desarrollo para patrones de diseño
**Referencia de curso:** Programa TIB-2006 – Patrones de diseño (TecNM)

## Herramientas necesarias

* Cuenta de **AWS** con acceso a **EC2** (free tier)
* **Consola web** de AWS (EC2) + **EC2 Instance Connect** (SSH vía navegador)
* **Ubuntu Server 24.04 LTS** (o 22.04 LTS) como AMI
* **Asciinema** para grabación de sesión
* **Git** y **GitHub** para publicar la práctica

## Lenguajes a instalar (ranking 2025)

Basado en índices/encuestas 2025 (TIOBE y Stack Overflow):

1. **Python**
2. **C++**
3. **Rust**
4. **Java**
5. **C# (.NET)**
6. **JavaScript (Node.js)**
7. **Go (Golang)**

> Nota: Se incluyen 7 lenguajes populares a 2025. Puedes agregar **Rust** o **PHP** como extra opcional si el tiempo lo permite.

---

## Desarrollo

### Paso 1. Crear instancia EC2 Ubuntu desde la consola web

1. Inicia sesión en **AWS Console** → **EC2** → **Launch instance**.
2. **Name**: `pd-ubuntu-lab`
3. **AMI**: Ubuntu Server **24.04 LTS** (amd64)
4. **Instance type**: `t2.micro` (free tier elegible).
5. **Key pair**: *Opcional* si usarás **EC2 Instance Connect**.
6. **Network settings**: Security Group con **salida (egress)** abierta; no es necesario abrir puertos de entrada para EC2 Instance Connect.
7. **Storage**: 12–16 GiB gp3 (sugerido).
8. **Launch instance**.

### Paso 2. Conexión vía EC2 Instance Connect (navegador)

1. En **EC2 → Instances**, selecciona `pd-ubuntu-lab` → **Connect**.
2. Pestaña **EC2 Instance Connect** → **Connect**.
3. Se abrirá una terminal web con usuario `ubuntu`.

### Paso 3. Preparación del sistema

```bash
sudo apt update && sudo apt -y upgrade
sudo apt -y install build-essential curl git unzip
```

### Paso 4. Instalar Asciinema y comenzar a grabar

```bash
sudo apt -y install asciinema
asciinema --version
# Inicia la grabación (sigue con todos los pasos siguientes dentro de esta sesión grabada)
asciinema rec
# (al finalizar todo) presiona Ctrl+D y sigue las instrucciones para subir el cast
```

> Alternativa snap (si fuese necesario): `sudo snap install asciinema --classic`

### Paso 5. Instalar y verificar cada lenguaje con "Hola Mundo"

> Ejecuta **cada bloque** dentro de la sesión grabada de Asciinema.

#### 5.1 Python 3

```bash
python3 --version || sudo apt -y install python3 python3-pip
python3 - <<'PY'
print("Hola Mundo desde Python")
PY
```


#### 5.2 C++ (g++)

```bash
sudo apt -y install g++
cat > hola.cpp <<'CPP'
#include <iostream>
int main(){ std::cout << "Hola Mundo en C++" << std::endl; return 0; }
CPP
g++ hola.cpp -o hola_cpp && ./hola_cpp
```

#### 5.3 RUST

```bash
// Elaborarlo
```


#### 5.4 Java (OpenJDK)

```bash
sudo apt -y install default-jdk
javac -version
cat > Hola.java <<'JAVA'
public class Hola { public static void main(String[] args){ System.out.println("Hola Mundo en Java"); } }
JAVA
javac Hola.java && java Hola
```

#### 5.5 C# (.NET SDK)

> En Ubuntu 24.04, .NET puede instalarse desde repositorios mantenidos por Canonical.

```bash
sudo apt -y install dotnet-sdk-8.0
dotnet --info
mkdir dotnet-hola && cd dotnet-hola
 dotnet new console -n HolaCs -o .
 sed -i 's/Console.WriteLine("Hello, World!");/Console.WriteLine("Hola Mundo en C#");/' Program.cs
 dotnet run
cd ..
```

#### 5.6 JavaScript (Node.js)

> Opción simple (repos Ubuntu). Para versiones LTS recientes, considera NodeSource o `nvm`.

```bash
sudo apt -y install nodejs npm
node -v && npm -v
node - <<'JS'
console.log('Hola Mundo en Node.js')
JS
```

#### 5.7 Go (Golang)

> Método oficial con tarball es común, pero en Ubuntu 24.04 también puedes usar `apt`.

```bash
# opción rápida
sudo apt -y install golang
go version
cat > hola.go <<'GO'
package main
import "fmt"
func main(){ fmt.Println("Hola Mundo en Go") }
GO
go run hola.go
```

### Paso 6. Cierre y publicación del *cast*

1. **Finaliza** la grabación con `Ctrl+D`.
2. Sigue el **link** que te proporciona Asciinema para tu cast público.
3. Copia la URL para incluirla en tu entrega.

---

## Código (si aplica)

Los fragmentos de código y comandos están incluidos en la sección **Desarrollo**. Se espera que el alumno los ejecute **en vivo** durante la grabación Asciinema.

## Resultados esperados

* Instancia **EC2 Ubuntu** creada y accesible vía **EC2 Instance Connect**.
* **7 lenguajes** instalados y **"Hola Mundo"** ejecutado en cada uno.
* **Grabación Asciinema** pública con todo el flujo (desde la conexión hasta las ejecuciones).
* En el repositorio GitHub: archivo `PRACTICA2.md` con **capturas** (opcional) y la **URL** del cast.

## Reflexión final

1. ¿Qué ventajas ofrece un entorno Linux en la nube para practicar patrones de diseño?
2. ¿Qué lenguaje del top 2025 consideras más adecuado para implementar patrones estructurales y por qué?
3. ¿Cómo automatizarías la preparación del entorno (script Bash, Ansible, CloudInit)?
4. ¿Qué riesgos de seguridad consideraste al exponer/administrar tu instancia?

## Recursos adicionales

* Programa TIB-2006 Patrones de diseño (TecNM): [https://informatica.voaxaca.tecnm.mx/wp-content/uploads/2022/03/TIB-2006-Patrones-de-diseno.pdf](https://informatica.voaxaca.tecnm.mx/wp-content/uploads/2022/03/TIB-2006-Patrones-de-diseno.pdf)
* AWS – Conectar con **EC2 Instance Connect**: [https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ec2-instance-connect-methods.html](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ec2-instance-connect-methods.html)
* Asciinema – Instalación y *Quick Start*: [https://docs.asciinema.org/manual/cli/installation/](https://docs.asciinema.org/manual/cli/installation/)  |  [https://docs.asciinema.org/manual/cli/quick-start/](https://docs.asciinema.org/manual/cli/quick-start/)
* .NET en Ubuntu 24.04 (Canonical/Microsoft): [https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-install](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-install)
* Node.js en Ubuntu (guías): [https://www.digitalocean.com/community/tutorials/how-to-install-node-js-on-ubuntu-22-04](https://www.digitalocean.com/community/tutorials/how-to-install-node-js-on-ubuntu-22-04)
* Go en Ubuntu 24.04 (guía actualizada): [https://www.cherryservers.com/blog/install-go-ubuntu-2404](https://www.cherryservers.com/blog/install-go-ubuntu-2404)
* Ranking 2025 (consulta y compara):

  * TIOBE Index: [https://www.tiobe.com/tiobe-index/](https://www.tiobe.com/tiobe-index/)
  * TechRepublic (resumen mensual 2025): [https://www.techrepublic.com/article/tiobe-index-language-rankings/](https://www.techrepublic.com/article/tiobe-index-language-rankings/)
  * Stack Overflow Developer Survey 2025: [https://survey.stackoverflow.co/2025/technology](https://survey.stackoverflow.co/2025/technology)

## Instrucciones para entrega

1. Revisar la práctica al repositorio GitHub del curso.
2. Nombre el Asciinema con su nombre, fecha, control y objetivo (puede hacer un encabezado)
3. Al finalizar Asciinema publicarlo en su perfil y de ahi enviar el Compartido/Sharing
4. Debe de llenar todo el formato de la publicación como descripción, etc
5. Cumple con la rúbrica en Idoceo y enviar enlaces diversos

