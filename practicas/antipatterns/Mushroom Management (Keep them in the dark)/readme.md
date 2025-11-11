# Mushroom Management (Keep them in the dark)

El Mushroom Management (también llamado “Keep them in the dark”) es un antipatrón de gestión donde los líderes o administradores ocultan información importante al equipo de desarrollo o trabajo, tratándolos como si “no necesitaran saber” los detalles del proyecto o las decisiones estratégicas.

## Por qué es una mala práctica:

* Limita la comunicación y la transparencia.

* Provoca desmotivación y pérdida de confianza en la organización.

* Los desarrolladores toman decisiones basadas en suposiciones erróneas.

* Se desperdicia tiempo en rehacer trabajo mal orientado.

Frase resumen:

> “Al equipo se le mantiene en la oscuridad y se le da abono (desinformación) para que crezca, pero nunca se le deja ver la luz.”.

---

## 💻 2. Ejemplo Técnico 

Una empresa llamada **DataVision Labs** está desarrollando un sistema interno para **procesar y validar datos de clientes**.  
El gerente del proyecto, presionado por los plazos, decide **no explicar todos los requerimientos** al equipo de desarrollo para “evitar confusiones” y “acelerar el trabajo”.  

El gerente simplemente dice:

> “Implementen una función que valide la información de los usuarios antes de guardarla en la base de datos. No se preocupen por los detalles, después ajustamos.”

### 🔹 Etapa 1: Desarrollo sin información

Los desarrolladores reciben el encargo y asumen que se trata de una validación genérica. Implementan algo sencillo:
```Python
def validar_datos(usuario):
    if not usuario.get("nombre") or not usuario.get("email"):
        raise ValueError("Faltan datos obligatorios")
    return True
```

El código parece correcto a simple vista, pasa las pruebas básicas y se entrega en tiempo.

Sin embargo, nadie les explicó que el cliente final (una institución bancaria) requiere:

* Validar los correos según un dominio específico.

* Cifrar la información antes de almacenarla.

* Cumplir con la norma ISO 27001 de protección de datos.

### 🔹 Etapa 2: Descubrimiento del error

Durante la demostración al cliente, el sistema rechaza usuarios válidos y almacena datos sin cifrar.
El área legal detecta el incumplimiento de normativas, lo que podría generar sanciones.

El gerente reacciona con enojo:

> “¿Cómo no sabían que debían aplicar la norma ISO 27001? ¡Eso estaba en los documentos del contrato!”

Los desarrolladores responden:

> “Nunca tuvimos acceso al contrato ni a esos documentos, solo se nos dijo que validáramos los datos.”

### 🔹 Etapa 3: Retrabajo y pérdida de tiempo

El equipo tiene que reconstruir la lógica completa, implementar cifrado y rehacer las pruebas.
Esto retrasa el proyecto tres semanas y genera costos adicionales por horas extras.

Código corregido (una versión mejor orientada):
```Python
from cryptography.fernet import Fernet

CLAVE_CIFRADO = Fernet.generate_key()
fernet = Fernet(CLAVE_CIFRADO)

def validar_datos(usuario):
    """
    Valida la información según ISO 27001
    y cifra los datos sensibles antes de almacenarlos.
    """
    if not usuario.get("nombre") or not usuario.get("email"):
        raise ValueError("Faltan datos obligatorios")
    
    if not usuario["email"].endswith("@banco-seguro.com"):
        raise ValueError("Dominio de correo no permitido")

    # Cifrado del campo confidencial
    usuario["email"] = fernet.encrypt(usuario["email"].encode()).decode()
    return usuario
```
### 🔹 Etapa 4: Efectos secundarios visibles

| Problema                     | Consecuencia directa                                       |
| ---------------------------- | ---------------------------------------------------------- |
| Falta de comunicación        | El equipo no comprendió las normas de seguridad requeridas |
| Falta de acceso a documentos | Implementaciones parciales e incorrectas                   |
| Desmotivación del equipo     | Sensación de culpa injusta y frustración                   |
| Retrabajo                    | Pérdida de tiempo y aumento de costos                      |

### 🔹 Etapa 5: Cambio de enfoque (solución posterior)

Después de este incidente, la empresa adoptó una práctica llamada “Reunión de contexto técnico”, donde antes de iniciar cada módulo:

* Se comparten los objetivos del cliente.

* Se explican las normas o requisitos legales.

* Se documenta todo en un wiki interno accesible.

Así, el equipo comprende no solo qué hacer, sino por qué hacerlo.

---

## ⚠️ 3. Consecuencias

Efectos negativos:

* 🔧 Mantenimiento: Código inconsistente y sin coherencia, ya que cada desarrollador trabaja con su propia interpretación.

* 🚀 Rendimiento: Retrasos en entregas, errores frecuentes y pérdida de productividad.

* 📈 Escalabilidad: Dificultad para integrar nuevas funciones, ya que el diseño base se hizo sin visión global.

* 😞 Equipo: Desmotivación, rotación alta de personal y desconfianza hacia la dirección.

Ejemplo:
> En proyectos grandes, este antipatrón puede hacer que los equipos se conviertan en simples “ejecutores”, incapaces de aportar mejoras o prevenir errores críticos.

---

## ✅ 4. Solución Correctiva (10 pts)

Buenas prácticas:

* Comunicación abierta y bidireccional: Fomentar reuniones de actualización donde todos entiendan el “por qué” detrás de las decisiones.

* Documentación clara: Compartir requerimientos, objetivos y métricas de éxito desde el inicio.

* Liderazgo transparente: Los líderes deben explicar no solo qué se hace, sino por qué se hace.

* Patrón alternativo:

  * Aplicar el patrón "Open Communication" o "Transparent Management".

  * Implementar prácticas ágiles (Scrum, Kanban) con reuniones diarias, revisiones y retrospectivas.

  * Usar herramientas de colaboración (Confluence, Notion, Jira) para mantener toda la información accesible.

