
> Para estudiantes sin tema de exposición, por NUMERO DE LISTA DE AMBAR (siempre y cuando no se repita)

🔹 Contexto de aplicación real,

🔹 Diagrama UML,

🔹 Explicación de los beneficios del patrón,

🔹 Ejemplo de mala práctica y propuesta de refactorización,

🔹 Relación con los principios **SOLID**.

---

## 🧩 **Catálogo de 45 Temas de Exposición — Patrones de Diseño GoF (libre lenguaje)**

| Nº | Patrón / Combinación                          | Tipo                         | Contexto / Uso Común                                              | Enfoque Didáctico Sugerido                              |
| -- | --------------------------------------------- | ---------------------------- | ----------------------------------------------------------------- | ------------------------------------------------------- |
| 1  | **Factory Method**                            | Creacional                   | Creación de objetos sin acoplarse a clases concretas.             | Refactorizar clases con muchos `if` de tipos concretos. |
| 2  | **Abstract Factory + Prototype**              | Creacional                   | Crear familias de productos reutilizando prototipos.              | Demostrar variantes de UI o temas visuales.             |
| 3  | **Singleton**                                 | Creacional                   | Control de instancias únicas (configuración, logging).            | Debatir sobre su abuso y alternativas (DI, IoC).        |
| 4  | **Builder**                                   | Creacional                   | Construcción paso a paso de objetos complejos.                    | Generar reportes o solicitudes HTTP dinámicas.          |
| 5  | **Prototype**                                 | Creacional                   | Clonar objetos sin conocer sus detalles internos.                 | Simular clonación de personajes o formularios.          |
| 6  | **Adapter + Bridge**                          | Estructural                  | Integrar APIs incompatibles y separar abstracción/implementación. | Mostrar compatibilidad entre versiones de servicios.    |
| 7  | **Composite + Iterator**                      | Estructural / Comportamiento | Recorrer estructuras jerárquicas.                                 | Modelar árbol genealógico o estructura de empresa.      |
| 8  | **Decorator**                                 | Estructural                  | Agregar funcionalidades dinámicamente.                            | Añadir validaciones o auditorías a clases de servicio.  |
| 9  | **Proxy + Facade**                            | Estructural                  | Control de acceso y simplificación de subsistemas.                | Mostrar cómo ocultar complejidad de APIs externas.      |
| 10 | **Observer + Mediator**                       | Comportamiento               | Comunicación desacoplada entre objetos.                           | Simular chats, notificaciones o sensores IoT.           |
| 11 | **Strategy + State**                          | Comportamiento               | Cambiar comportamientos en tiempo de ejecución.                   | Simular modos de juego o flujos de compra.              |
| 12 | **Command + Memento**                         | Comportamiento               | Implementar deshacer/rehacer.                                     | Refactorizar editores o formularios con historial.      |
| 13 | **Chain of Responsibility + Template Method** | Comportamiento               | Procesamiento en cadena flexible.                                 | Validación secuencial de peticiones.                    |
| 14 | **Interpreter + Visitor**                     | Comportamiento               | Evaluar expresiones o procesar estructuras.                       | Crear mini-lenguajes o reglas de negocio.               |
| 15 | **Flyweight**                                 | Estructural                  | Compartir objetos para optimizar memoria.                         | Renderizado de gráficos, emojis o celdas de Excel.      |
| 16 | **Bridge + Strategy**                         | Estructural / Comportamiento | Cambiar comportamientos y plataformas dinámicamente.              | Aplicaciones multiplataforma (móvil, escritorio, web).  |
| 17 | **Facade + Adapter**                          | Estructural                  | Simplificar y compatibilizar subsistemas.                         | Envolver servicios REST heredados.                      |
| 18 | **Command + Observer**                        | Comportamiento               | Ejecutar acciones y notificar resultados.                         | Sistema de eventos o colas de comandos.                 |
| 19 | **Memento + State**                           | Comportamiento               | Guardar estados de un objeto en diferentes momentos.              | Sistema de checkpoints o autosave.                      |
| 20 | **Decorator + Strategy**                      | Estructural / Comportamiento | Comportamientos dinámicos con estrategias intercambiables.        | Algoritmos de compresión o cifrado.                     |
| 21 | **Bridge + Command**                          | Estructural / Comportamiento | Separar abstracción de ejecución de comandos.                     | Control remoto de dispositivos.                         |
| 22 | **Singleton + Facade**                        | Creacional / Estructural     | Acceso global a un subsistema simplificado.                       | Manejo centralizado de configuración.                   |
| 23 | **Adapter + Decorator**                       | Estructural                  | Adaptar y extender funcionalidades.                               | Extender clases de librerías externas.                  |
| 24 | **Chain of Responsibility + Observer**        | Comportamiento               | Procesamiento reactivo en cadena.                                 | Sistema de alertas o monitoreo de logs.                 |
| 25 | **Template Method + Strategy**                | Comportamiento               | Estructura de algoritmo fija con pasos variables.                 | Proceso de exportación con estrategias de formato.      |
| 26 | **Abstract Factory + Singleton**              | Creacional                   | Fábricas globales controladas por instancia única.                | Gestión de conexiones o recursos compartidos.           |
| 27 | **Builder + Prototype**                       | Creacional                   | Construcción y clonación de objetos complejos.                    | Formularios dinámicos con campos personalizados.        |
| 28 | **Proxy + Observer**                          | Estructural / Comportamiento | Monitoreo y control de acceso.                                    | Proxy de red que notifica accesos.                      |
| 29 | **Decorator + Command**                       | Estructural / Comportamiento | Añadir responsabilidades a comandos.                              | Sistema de logging o auditoría de acciones.             |
| 30 | **Strategy + Chain of Responsibility**        | Comportamiento               | Selección y ejecución flexible de algoritmos.                     | Procesamiento de pagos o filtros de imagen.             |
| 31 | **Mediator + Command**                        | Comportamiento               | Centralizar coordinación entre comandos.                          | Panel de control o interfaz de usuario modular.         |
| 32 | **Visitor + Composite**                       | Comportamiento / Estructural | Operar sobre estructuras jerárquicas.                             | Análisis sintáctico o recorrido de árbol XML.           |
| 33 | **State + Observer**                          | Comportamiento               | Notificación de cambios de estado.                                | Dispositivos IoT o domótica.                            |
| 34 | **Bridge + Flyweight**                        | Estructural                  | Conectar estructuras livianas y cambiantes.                       | Motor de gráficos optimizado.                           |
| 35 | **Memento + Mediator**                        | Comportamiento               | Recuperar estados en sistemas coordinados.                        | Chats con mensajes recuperables.                        |
| 36 | **Command + Strategy**                        | Comportamiento               | Comandos que aplican diferentes estrategias.                      | Macrocomandos o batch de operaciones.                   |
| 37 | **Adapter + Strategy**                        | Estructural / Comportamiento | Integrar y seleccionar algoritmos compatibles.                    | Sistema de plugins o drivers.                           |
| 38 | **Prototype + Flyweight**                     | Creacional / Estructural     | Reutilizar objetos clonados y compartidos.                        | Videojuegos con entidades similares.                    |
| 39 | **Decorator + Facade**                        | Estructural                  | Extender subsistemas simplificados.                               | Sistema de reportes o dashboards extensibles.           |
| 40 | **Observer + Strategy**                       | Comportamiento               | Notificar y cambiar comportamiento según evento.                  | IA de juego o respuesta adaptativa.                     |
| 41 | **Template Method + Command**                 | Comportamiento               | Definir estructura base de ejecución de comandos.                 | Procesamiento por lotes o pipelines.                    |
| 42 | **Composite + Strategy**                      | Estructural / Comportamiento | Estrategias aplicadas a estructuras jerárquicas.                  | Aplicación de descuentos o reglas por categoría.        |
| 43 | **Abstract Factory + Decorator**              | Creacional / Estructural     | Fábricas que generan objetos decorables.                          | Sistema de creación de UI con componentes visuales.     |
| 44 | **Visitor + Strategy**                        | Comportamiento               | Aplicar diferentes comportamientos al visitar objetos.            | Analizador con estrategias de recorrido.                |
| 45 | **Builder + Command + Observer**              | Creacional / Comportamiento  | Construcción, ejecución y monitoreo de procesos.                  | Pipeline de CI/CD o orquestador de tareas.              |

---

## 🧠 Recomendaciones Didácticas Finales

* Asignar **1 patrón o combinación por estudiante**.
* Incluir en cada exposición:

  * Ejemplo **“bad code”** inicial.
  * Refactorización propuesta.
  * Justificación del patrón.
  * Comparativa con otros patrones posibles.
* Cierre grupal con discusión de **SOLID + GoF**: cómo se complementan.

---
