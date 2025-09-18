<img width="972" height="765" alt="image" src="https://github.com/user-attachments/assets/797d114c-61f2-4893-950f-aaa6e3298cb0" />


# Actividades Prácticas

En esta carpeta se encuentran las prácticas distribuidas por patrón y tipo.
Cada práctica incluye:
- Instrucciones detalladas
- Diagramas UML
- Código fuente
- Criterios de evaluación

---

## ✅ ¿Qué es un Ansible Playbook?

Un **Ansible Playbook** es un archivo escrito en el lenguaje **YAML** que contiene una o más tareas que Ansible debe ejecutar sobre uno o varios servidores (hosts). Estas tareas pueden ser:

* Instalar programas
* Crear usuarios
* Modificar archivos de configuración
* Iniciar servicios
* Realizar actualizaciones del sistema
* Entre muchas otras acciones

Usar Playbooks permite automatizar y repetir tareas de administración de sistemas de forma consistente y sencilla, lo cual es ideal para practicar y aprender a gestionar servidores como **Ubuntu Server**.

---

## 🧩 ¿Qué lo hace especial?

* **YAML**: Fácil de leer y escribir.
* **Idempotente**: Si ejecutas el Playbook varias veces, solo aplica los cambios si son necesarios.
* **Automatización sin agentes**: No necesitas instalar nada en el servidor remoto (si estás usando otros equipos). Solo SSH y Python.

Perfecto, vamos a crear un ejemplo práctico de un **Ansible Playbook** que:

🔧 **Instala y configura un servidor Flask en `localhost`**,
🚀 **Ejecuta una aplicación Flask básica**,
📡 **Usa `systemd` para ejecutar el servidor Flask como un servicio permanente.**

Todo esto en un entorno **Ubuntu Server**, sin inventario (solo localhost).

---

## ✅ ¿Qué hace este Playbook?

1. Instala Python 3, pip y otros paquetes necesarios.
2. Crea una aplicación básica de Flask.
3. Configura un servicio de systemd para que Flask se inicie automáticamente.
4. Inicia y habilita el servicio.

---

## 📂 Estructura esperada

Supongamos que trabajas en un directorio como:

```
~/flask_ansible/
├── flask_app.py          # Tu app Flask (se crea desde Ansible)
├── flask.service         # Archivo systemd (se crea desde Ansible)
├── playbook.yml          # Tu playbook de Ansible
```

Pero en este caso, **Ansible generará los archivos automáticamente**.

---

## 🧾 Paso 1: Crear el Playbook

Guarda este contenido como `playbook.yml`:

```yaml
---
- name: Instalar y configurar servidor Flask en localhost
  hosts: localhost
  connection: local
  become: true

  vars:
    app_dir: /opt/flask_app
    app_file: flask_app.py
    service_name: flask

  tasks:
    - name: Instalar dependencias necesarias
      apt:
        name:
          - python3
          - python3-pip
        update_cache: yes
        state: present

    - name: Instalar Flask con pip
      pip:
        name: flask
        executable: pip3

    - name: Crear directorio para la app Flask
      file:
        path: "{{ app_dir }}"
        state: directory
        owner: "{{ ansible_user }}"
        mode: '0755'

    - name: Crear aplicación Flask de ejemplo
      copy:
        dest: "{{ app_dir }}/{{ app_file }}"
        content: |
          from flask import Flask
          app = Flask(__name__)

          @app.route("/")
          def hello():
              return "¡Hola desde Flask en Ansible!"

          if __name__ == "__main__":
              app.run(host="0.0.0.0", port=5000)

    - name: Crear archivo systemd para Flask
      copy:
        dest: /etc/systemd/system/{{ service_name }}.service
        content: |
          [Unit]
          Description=Flask App
          After=network.target

          [Service]
          User={{ ansible_user }}
          WorkingDirectory={{ app_dir }}
          ExecStart=/usr/bin/python3 {{ app_dir }}/{{ app_file }}
          Restart=always

          [Install]
          WantedBy=multi-user.target

    - name: Recargar systemd para reconocer el nuevo servicio
      command: systemctl daemon-reexec

    - name: Habilitar el servicio Flask
      systemd:
        name: "{{ service_name }}"
        enabled: yes
        state: started
```

---

## ▶ Paso 2: Ejecutar el Playbook

Abre la terminal y ejecuta:

```bash
ansible-playbook playbook.yml --ask-become-pass
```

> Esto instalará todo y lanzará la app Flask como un servicio.

---

## 🌐 Paso 3: Verificar que el servidor funciona

Abre tu navegador o usa `curl` desde la terminal:

```bash
curl http://localhost:5000
```

Deberías ver:

```
¡Hola desde Flask en Ansible!
```

---

## 📦 Resultado del servicio

Tu aplicación Flask ahora está:

* Corriendo como un servicio del sistema con `systemd`.
* Escuchando en el puerto `5000` en `0.0.0.0` (accesible desde cualquier IP si se permite).
* Se reinicia automáticamente si falla.

Puedes controlarla como cualquier otro servicio:

```bash
sudo systemctl status flask
sudo systemctl restart flask
```

---

## 🧠 Bonus: Acceso desde red local

Si estás usando un servidor Ubuntu en red, asegúrate de que el puerto 5000 esté abierto:

```bash
sudo ufw allow 5000
```

Y podrás acceder desde otra máquina cony no olvide abrir el puerto 5000/tcp en "security groups de AWS Academy"

```
http://IP_DEL_SERVIDOR:5000
```

---

## ✅ Este ejemplo muestra cómo:

* Automatizar completamente la creación y ejecución de una app web con Flask.
* Usar `localhost` como objetivo sin inventario.
* Usar `systemd` para mantener la app corriendo.

---


