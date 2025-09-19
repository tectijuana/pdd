#
Autor: Rojas Garcia Kevin Argenis
Fecha: 2025-09-18
Descripción: Investiacion sobre Aplicaciones del patrón CQRS en Node.js
 ============================================

# ¿Qué es CQRS? 

**CQRS** significa **Command Query Responsibility Segregation**, o en español, **Segregación de Responsabilidades entre Comandos y Consultas**. Es un **patrón de arquitectura** que separa la lógica de **lectura** (queries) de la lógica de **escritura** (commands) en una aplicación.

En lugar de tener un solo objeto o método que maneje tanto la lectura como la escritura de datos (como en un CRUD típico), CQRS propone separar estas dos responsabilidades en **modelos diferentes**, para que cada uno evolucione y escale de forma independiente.

# ¿Por qué es importante?
En sistemas complejos o de alto rendimiento, los requisitos para leer datos no son los mismos que para escribirlos. CQRS permite:

-   Optimizar las **consultas** sin afectar las **escrituras**
    
-   Controlar mejor los **efectos secundarios** de los comandos
    
-   Mejorar la **escalabilidad**, la **seguridad** y la **trazabilidad**



# Ejemplo

 ### Caso realista: una API de órdenes en una tienda online

En una tienda online hecha en Node.js, podríamos tener una API REST con rutas como:

```bash
GET /orders/:id        → Obtener detalles de una orden
POST /orders           → Crear una nueva orden
PUT /orders/:id        → Actualizar estado de la orden

```

### Con un enfoque tradicional (CRUD):

-   Un solo controlador maneja tanto la lectura como la escritura.
    
-   El mismo modelo (por ejemplo, un `Order` de Mongoose o Sequelize) se usa para todo.
    
-   Riesgo: mezclar lógica de negocio compleja con detalles de persistencia.
### Con CQRS:

-   Se divide en dos flujos:
    
    -   **Commands** → `CreateOrderCommand`, `UpdateOrderStatusCommand`
        
    -   **Queries** → `GetOrderByIdQuery`, `GetOrdersByCustomerQuery`
        
-   Cada uno tiene su handler separado.
### Implementación sencilla en Node.js

Estructura básica de carpetas:

```bash
src/
  commands/
    handlers/
      createOrderHandler.js
    createOrderCommand.js
  queries/
    handlers/
      getOrderHandler.js
    getOrderQuery.js
  models/
    Order.js
  controllers/
    orderController.js

```
Comando: Crear orden
```bash
// commands/createOrderCommand.js
class CreateOrderCommand {
  constructor(customerId, items) {
    this.customerId = customerId;
    this.items = items;
  }
}
module.exports = CreateOrderCommand;

```



```bash
// commands/handlers/createOrderHandler.js
const Order = require('../../models/Order');

async function handleCreateOrder(command) {
  const order = new Order({
    customerId: command.customerId,
    items: command.items,
    status: 'Pending',
  });
  return await order.save();
}

module.exports = handleCreateOrder;

```


Consulta: Obtener orden

```bash
// queries/getOrderQuery.js
class GetOrderQuery {
  constructor(orderId) {
    this.orderId = orderId;
  }
}
module.exports = GetOrderQuery;

```




```bash
// queries/handlers/getOrderHandler.js
const Order = require('../../models/Order');

async function handleGetOrder(query) {
  return await Order.findById(query.orderId).lean(); // solo lectura
}

module.exports = handleGetOrder;

```


### Refactorización:

Aplicar CQRS implica **refactorizar** una arquitectura CRUD tradicional, moviendo lógica de negocio a **comandos** y **consultas** especializadas. Esto mejora:

-   La **cohesión** del código (cada handler tiene una única responsabilidad)
    
-   La **trazabilidad** (cada acción queda clara y auditable)
    
-   La **testabilidad** (puedes testear los handlers de comandos y queries por separado)
    

### Mejora de calidad:

-   Previene efectos colaterales inesperados
    
-   Separa el dominio de la lógica de infraestructura
    
-   Facilita cambios en la capa de lectura sin romper la escritura (y viceversa)

### ¿Cuándo usar CQRS?

✅ **Sí conviene usarlo cuando:**

-   Tienes operaciones de lectura muy diferentes a las de escritura
    
-   Necesitas auditar o versionar comandos
    
-   Tu sistema crecerá en complejidad (microservicios, eventos, etc.)
    
-   Hay un enfoque de **arquitectura orientada al dominio (DDD)**
    

🚫 **No es recomendable si:**

-   Tu aplicación es pequeña o sencilla
    
-   Agregar complejidad extra no aporta valor
    
-   No hay diferencia significativa entre lectura y escritura
    

### Reflexión crítica:

En el contexto de un curso de **Patrones de Diseño y Desarrollo (PDD)**, CQRS es un excelente ejemplo de cómo **un patrón arquitectónico puede mejorar la mantenibilidad, escalabilidad y separación de responsabilidades** en proyectos reales.

En lugar de tratar al sistema como un CRUD monolítico, este patrón nos obliga a pensar en **acciones y consultas como unidades independientes**, lo que ayuda a tener una arquitectura limpia y desacoplada.
