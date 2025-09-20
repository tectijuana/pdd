# Clean Architecture y Onion Architecture
![maxresdefault](https://github.com/user-attachments/assets/ee6db6cf-6f9d-444f-98d3-d3b2f4509070)

Montaño Zaragoza Marcos Ulises 21211998

Explicacion sobre los fundamentos, estructura, similitudes y diferencias entre **Onion Architecture** y **Clean Architecture**.

---

## Introducción

En el desarrollo de software empresarial es común enfrentar **alta complejidad** en lógica de negocio, interacción con múltiples servicios externos y cambios tecnológicos constantes.  

Para enfrentar estos retos surgieron arquitecturas como **Onion Architecture** y **Clean Architecture**, cuyo objetivo es desacoplar la lógica del dominio (la parte más estable del software) de los detalles técnicos como frameworks, bases de datos o interfaces gráficas.  

---

## Origen y contexto histórico

- **Onion Architecture** fue propuesta por **Jeffrey Palermo en 2008** como una alternativa a la tradicional arquitectura en capas (layered architecture), que solía generar un fuerte acoplamiento con la base de datos y frameworks.  
  Palermo observó que en muchos proyectos, la base de datos se convertía en el “centro” del sistema, y buscó invertir ese paradigma colocando al **dominio** en el núcleo.  

- **Clean Architecture** fue presentada por **Robert C. Martin (Uncle Bob) en 2012**, inspirada en varias arquitecturas previas (Hexagonal, Onion, Screaming Architecture). Su enfoque clave es organizar la aplicación en torno a **casos de uso**, con independencia total de frameworks.  

---

## Principios comunes

Ambas arquitecturas comparten estos fundamentos:

- **Dependency Inversion Principle (DIP):**  
  El dominio y la lógica de aplicación no deben depender de frameworks, sino de interfaces y abstracciones.  

- **Separation of Concerns (SoC):**  
  Cada capa tiene responsabilidades claras: el dominio define reglas, la infraestructura conecta con el mundo externo.  

- **Independencia tecnológica:**  
  Puedes reemplazar base de datos, UI o frameworks sin reescribir la lógica central.  

- **Testabilidad:**  
  Permiten aislar el core para escribir pruebas unitarias sin dependencias externas.  

---

##  Onion Architecture

### Características principales
- El **dominio es el núcleo** del sistema.  
- Las capas externas dependen de las internas.  
- Favorece un modelo de **entidades ricas** (con reglas y comportamientos, no solo datos).  
- Define **interfaces en el núcleo** y las implementaciones concretas en la periferia.  

### Capas y responsabilidades
1. **Domain (Core):**  
   - Entidades de negocio (por ejemplo, `Cliente`, `Pedido`).  
   - Interfaces que describen contratos (ejemplo: `IRepositorioCliente`).  

2. **Application Services:**  
   - Contienen lógica de aplicación que orquesta entidades y repositorios.  
   - No conocen detalles de infraestructura, solo usan interfaces.  

3. **Infrastructure:**  
   - Implementa contratos definidos en el dominio (por ejemplo, `RepositorioClienteSQL`).  
   - Maneja persistencia, acceso a APIs, logging, etc.  

4. **Presentation (UI / API):**  
   - Controladores, vistas o endpoints que llaman a la capa de aplicación.  

### Ejemplo práctico
Un servicio de pedidos en Onion:
- `Pedido` (Entidad en Domain).  
- `IRepositorioPedido` (Interface en Domain).  
- `ServicioPedidos` (Application Services).  
- `RepositorioPedidoEF` (Infrastructure, usa Entity Framework).  
- `PedidosController` (UI, ASP.NET Core).  

---

## Clean Architecture

### Características principales
- Coloca en el centro **entidades** y **casos de uso**.  
- Define explícitamente una capa de **Use Cases / Interactors**, donde se orquesta la lógica de aplicación.  
- Usa **adaptadores de interfaz** para comunicar el núcleo con el mundo exterior.  
- Hace explícito el principio de independencia de frameworks: *“Los frameworks son detalles, no decisiones arquitectónicas”*.  

### Capas y responsabilidades
1. **Entities:**  
   - Reglas de negocio fundamentales, independientes de tecnología.  

2. **Use Cases / Application Layer:**  
   - Casos de uso concretos, como “Registrar cliente” o “Procesar pago”.  
   - Orquestan entidades y definen el flujo de la aplicación.  

3. **Interface Adapters:**  
   - Adaptan datos entre el dominio y las interfaces externas.  
   - Ejemplo: convertir DTOs a entidades y viceversa.  

4. **Frameworks & Drivers:**  
   - Bases de datos, interfaces gráficas, librerías externas.  
   - Considerados “detalles de implementación”.  

### Ejemplo práctico
Un caso de uso de pedidos en Clean:
- `Pedido` (Entidad en Domain).  
- `RegistrarPedido` (Use Case / Interactor).  
- `PedidoController` (Interface Adapter, recibe request HTTP y llama al caso de uso).  
- `RepositorioPedidoEF` (Driver en Infrastructure).  

---

##  Similitudes

- Separan **dominio** de **infraestructura**.  
- Usan **capas concéntricas** donde las dependencias apuntan hacia el núcleo.  
- Promueven **testabilidad y flexibilidad**.  
- Se basan en principios de **SOLID**, especialmente DIP.  

---

##  Diferencias

| Aspecto | Onion Architecture | Clean Architecture |
|---------|-------------------|--------------------|
| Enfoque | Núcleo de dominio | Casos de uso |
| Organización | Domain → Application → Infraestructura | Entities → Use Cases → Adapters → Frameworks |
| Complejidad | Más simple, directa | Más estricta, más estructura |
| Ideal para | Equipos pequeños, proyectos medianos | Proyectos grandes, lógica compleja |
| Riesgo | Menos énfasis en casos de uso | Mayor sobrecarga inicial |

---

## Ventajas y desventajas

**Onion Architecture**
-  Simplicidad.  
- Claridad en torno al dominio.  
-  Puede dispersar lógica de aplicación si no se cuida.  

**Clean Architecture**
-  Organización clara por casos de uso.  
-  Altamente escalable.  
-  Curva de aprendizaje más alta.  
-  Sobrecarga en proyectos pequeños.  

---

##  Casos de uso recomendados

- **Onion Architecture:**  
  Aplicaciones con lógica de negocio moderada, donde el dominio debe ser fuerte pero sin necesidad de sobreestructurar.  

- **Clean Architecture:**  
  Aplicaciones grandes, sistemas críticos o de larga vida útil donde los casos de uso deben estar claramente definidos y separados.  

---

##  Ejemplo de estructura en .NET

**Clean Architecture**

<img width="210" height="275" alt="Clean-Architecture-Core-Structure" src="https://github.com/user-attachments/assets/c3898de6-7c5c-4060-ab0d-66aba211fee1" />

---

**Onion Architecture**

<img width="257" height="347" alt="Onion-Architecture-Core-Structure-1" src="https://github.com/user-attachments/assets/f956f66f-dadd-43e8-befd-9d02101b9e49" />


---

##  Reflexion

El uso de patrones de diseño en arquitecturas como Onion Architecture y Clean Architecture es un punto de debate recurrente en el desarrollo de software. 
Ambos enfoques se nutren de principios de la programación orientada a objetos y de la ingeniería de software basada en patrones, lo que permite ofrecer 
soluciones probadas a problemas comunes. 

Los patrones de diseño aportan beneficios como:

* Estandarización del lenguaje técnico: facilitan la comunicación entre desarrolladores al ofrecer una terminología compartida.

* Reutilización de soluciones probadas: patrones como Dependency Injection, Repository o Adapter son claves en estas arquitecturas.

* Mantenibilidad y escalabilidad: permiten modificar componentes internos sin afectar el sistema completo.

En conclusión, los patrones de diseño deben verse como herramientas y no como dogmas. Tanto Onion como Clean Architecture muestran que la 
combinación estratégica de patrones puede llevar a sistemas robustos y testeables, pero es responsabilidad del equipo evaluar el contexto del proyecto 
antes de decidir su implementación. El valor real de un patrón no está en su uso per se, sino en resolver problemas concretos de forma eficiente.

---

##  Referencias (formato APA)

- Miranda, R. (2024, August 7). *What are the Differences Between Onion Architecture and Clean Architecture in .NET?* Code Maze. https://code-maze.com/dotnet-differences-between-onion-architecture-and-clean-architecture/  

- RoshanGavandi. (2024, October 12). *Comparing Onion Architecture vs. Clean Architecture: Key Differences, Practical Use Cases, and Best Practices for Modern Software Development.* Medium (roshancloudarchitect.me). https://roshancloudarchitect.me/comparing-onion-architecture-vs-1ab4863419e6  

- Raveendran, P. (2023, April 29). *Clean Architecture and Onion Architecture.* Medium. https://medium.com/@prasadnair44/clean-architecture-and-onion-architecture-b235de7c2e60

