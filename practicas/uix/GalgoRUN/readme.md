

## 🏍️ Proyecto Integrador: **GalgoRUN – App de Delivery Universitaria**

### 🎯 Objetivo General

Diseñar y prototipar en **Figma** una aplicación de entregas a domicilio dentro del campus universitario llamada **GalgoRUN**, y luego implementar su lógica usando **Patrones de Diseño GoF** para un sistema modular, escalable y reutilizable.

---

### 🧩 Contexto Profesional

En universidades como **Harvard, MIT y Stanford**, Figma se usa en cursos de ingeniería de software y diseño de interacción para **crear prototipos funcionales**, validar experiencias de usuario, y luego **traducir el flujo visual a arquitectura de software real** (MVC, GoF, SOLID).
Tus estudiantes replicarán este flujo profesional, uniendo **diseño + patrones + código**.

---

### 🚀 Escenario del Proyecto

**App:** GalgoRUN
**Descripción:**
GalgoRUN conecta estudiantes y repartidores dentro del campus para entregar alimentos, documentos, o materiales en puntos designados. La app incluye tres roles principales:

1. **Cliente** (pide comida o paquetes)
2. **Repartidor** (acepta pedidos y realiza entregas)
3. **Administrador** (gestiona productos, rutas, reportes)

---

### 🧰 Etapa 1 – Diseño de Interfaz en Figma

Cada equipo diseñará un **mockup funcional** con al menos 6 pantallas principales:

| Pantalla             | Contenido clave                                           |
| -------------------- | --------------------------------------------------------- |
| 🏠 Inicio/Login      | Acceso según tipo de usuario (cliente, repartidor, admin) |
| 🍔 Menú de productos | Listado de productos o pedidos disponibles                |
| 📦 Carrito/Pedido    | Detalle del pedido, cantidad y dirección                  |
| 🛵 Seguimiento       | Mapa o estado del envío (en camino, entregado)            |
| 👤 Perfil            | Datos del usuario o repartidor                            |
| 📊 Panel Admin       | Reportes de entregas, ganancias, usuarios                 |

💡 *Tip:* Usa componentes reutilizables, íconos, y flujo interactivo entre pantallas.
👉 Exporta el diseño final en **PDF** o comparte el **link de prototipo** de Figma.

---

### ⚙️ Etapa 2 – Aplicación de Patrones GoF

Cada equipo seleccionará **3 Patrones de Diseño GoF** (1 por tipo: creacional, estructural y de comportamiento) que mejor se adapten al sistema.

| Tipo               | Ejemplo de aplicación en GalgoRUN                                                     |
| ------------------ | ------------------------------------------------------------------------------------- |
| **Creacional**     | *Factory Method* para crear diferentes tipos de usuarios (Cliente, Repartidor, Admin) |
| **Estructural**    | *Facade* para simplificar el acceso a APIs de mapas, pagos y notificaciones           |
| **Comportamiento** | *Observer* para notificar al cliente cuando su pedido cambia de estado                |

---

### 💻 Etapa 3 – Desarrollo CRUD

Los estudiantes implementarán un CRUD funcional (lenguaje libre) con las entidades:

* Usuario
* Pedido
* Producto
* Repartidor
* Reporte

Integrando los patrones seleccionados para lograr modularidad y bajo acoplamiento.

---

### 📦 Entregables

| Fase | Entregable                                | Formato               |
| ---- | ----------------------------------------- | --------------------- |
| 1    | Prototipo Figma de la App GalgoRUN        | `.fig` + `.pdf`       |
| 2    | Documento técnico: patrones GoF aplicados | `.md` o `.pdf`        |
| 3    | Código fuente del CRUD                    | Repositorio en GitHub |
| 4    | Video o demo de presentación              | `.mp4` o link LOOM no mas 5 min         |

---

### 🧾 Rúbrica (100 puntos)

| Criterio                   | Descripción                            | Ponderación |
| -------------------------- | -------------------------------------- | ----------- |
| Diseño visual (Figma)      | Interfaz clara, coherente y atractiva  | 25          |
| Aplicación de patrones GoF | Correcta selección y justificación     | 25          |
| Lógica CRUD funcional      | Correcta implementación de entidades   | 30          |
| Documentación técnica      | UML + conexión entre UI y patrones     | 10          |
| Presentación final         | Explicación de resultados y decisiones | 10          |

---

### 💬 Actividad en Figma

Cada estudiante o equipo colaborará en un **FigJam** con secciones:

1. **Brief del problema**: “¿Qué necesita resolver GalgoRUN?”
2. **Flujo de usuario (User Flow)**: desde que pide hasta que recibe el pedido.
3. **Mapa de componentes (UI)**: botones, formularios, listas.
4. **Patrones GoF sugeridos**: diagramas y su función.

---

Prompt listo para usar en el Asistente de IA de Figma (Figma AI Assistant) para **login, menú, carrito, mapa, perfil, admin**.
Solo cópialo y pégalo en el chat dentro de Figma → selecciona **“Generate UI” o “Mockup App”** según la versión, así tus Uds. solo lo personalizan..

---

## 🏍️ Prompt para Figma AI Assistant

**Título:** Mockup App “GalgoRUN” – Delivery Universitario

**Prompt:**

> Diseña una aplicación móvil llamada **GalgoRUN**, un sistema de delivery universitario inspirado en Rappi y UberEats.
> Debe tener una estética moderna, minimalista y con colores institucionales (azul y blanco).
> Crea un **mockup funcional para pantalla móvil (iPhone 14 o Android estándar)** que incluya las siguientes pantallas:
>
> 1. **Login / Registro**
>
>    * Campos: correo, contraseña, botón “Iniciar sesión”
>    * Enlace “¿Eres nuevo? Regístrate”
>    * Logo de GalgoRUN (puede ser un galgo o motocicleta)
> 2. **Menú principal (Cliente)**
>
>    * Lista de productos o restaurantes (tarjetas con imagen, precio y botón “Agregar”)
>    * Barra inferior de navegación: Inicio, Pedidos, Perfil
> 3. **Carrito de pedido**
>
>    * Lista de ítems agregados
>    * Total y botón “Realizar pedido”
>    * Icono de eliminar productos
> 4. **Seguimiento del pedido (Mapa)**
>
>    * Indicador de ubicación actual del repartidor
>    * Estado del pedido (en preparación, en camino, entregado)
>    * Mapa ilustrativo o marcador de posición
> 5. **Perfil del usuario / repartidor**
>
>    * Foto de perfil
>    * Datos personales (nombre, matrícula, contacto)
>    * Botón “Cerrar sesión”
> 6. **Panel del Administrador**
>
>    * Tabla o lista con pedidos activos, repartidores y ganancias
>    * Gráficas simples o estadísticas con barras o círculos
>
> ⚙️ Incluye componentes reutilizables:
>
> * Botones primarios y secundarios
> * Tarjetas de producto
> * Barra de navegación inferior
> * Tipografía sans-serif moderna (ej. Inter, Roboto, Poppins)
>
> 💡 Estilo general: UI limpia tipo “Material Design 3”, fondo claro, acentos en azul y verde.
> 💬 Nombres de pantallas claros para los estudiantes: `login`, `menu_cliente`, `carrito`, `seguimiento`, `perfil`, `admin_panel`.

---

### 📄 Opcional – Exportar para tus alumnos

Una vez que el Asistente genere el diseño:

1. Duplica el archivo (`File → Duplicate to your drafts`).
2. Exporta en PDF (`File → Export frames to PDF`).
3. Así cada alumno tendrá un punto de partida editable y documentado.

---
