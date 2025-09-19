# Refactorización de monolitos a microservicios en PHP
### Joel Cuevas Estrada - 22210298

Migrar un monolito en PHP hacia una arquitectura de microservicios es un proceso que requiere estrategia, ya que no se trata solo de dividir código, sino de transformar la manera en que tu sistema funciona, escala y se mantiene. Te hago un desglose en pasos, buenas prácticas y ejemplos aplicados a PHP:

## 1. Evaluación del monolito

Antes de separar, identifica:

- Módulos lógicos claros: autenticación, facturación, catálogo, notificaciones, etc.

- Dependencias internas: qué partes del código se llaman entre sí.

- Cuellos de botella: consultas pesadas, controladores muy grandes, partes difíciles de testear.

## 2. Definir límites (Bounded Contexts)

Inspírate en Domain-Driven Design (DDD):
- Cada microservicio debe cubrir un dominio específico.

- Ejemplo en un e-commerce en PHP:
  User Service → registro/login, tokens JWT
  Catalog Service → productos, categorías
  Order Service → pedidos y pagos
  Notification Service → emails y SMS

## 3. Preparar comunicación entre servicios
En PHP puedes usar varios enfoques:
- REST con Slim o Laravel Lumen → servicios ligeros.
- gRPC (con extensiones PHP) → más rápido en sistemas complejos.
- Mensajería asíncrona (RabbitMQ, Kafka) → para procesos como correos o logs.

## 4. Refactor paso a paso
- Extrae un servicio pequeño del monolito (ej. notificaciones).
- Expón una API para ese servicio.
- Haz que el monolito consuma esa API en lugar del código interno.
- Itera con otros módulos hasta reducir el monolito a un núcleo mínimo.

## 5. Manejo de datos
- El monolito probablemente tenga una sola base de datos (MySQL, MariaDB, PostgreSQL).
- En microservicios, cada servicio debería tener su propia base de datos o esquema.
- Si no puedes separarlo aún, comienza con schemas separados dentro de la misma BD.

## 6. Observabilidad y despliegue
- Logs centralizados (Graylog, ELK, Monolog en PHP).
- Health checks para cada microservicio.
- Contenerización con Docker + Docker Compose para orquestar microservicios PHP.
- Eventualmente, migrar a Kubernetes si el sistema crece.

## 7. Herramientas útiles en PHP
- Frameworks ligeros: Slim, Lumen, Symfony MicroKernel.
- Autenticación: JWT con Firebase PHP JWT.
- Mensajería: php-amqplib (RabbitMQ).
- Service Discovery: Consul o Eureka (aunque en PHP suelen delegarse a Docker/K8s).

## Ejemplo practica

### Monolito (antes)
Supongamos que tienes un monolito Laravel y separas el módulo de notificaciones:
Monolito (antes)
```` php
// Dentro del monolito
class OrderController {
    public function store(Request $request) {
        $order = Order::create($request->all());
        Notification::send($order->user, new OrderCreated($order));
        return response()->json($order);
    }
}
````
### Después de extraer a microservicio (Slim API)
```` php
// Notification Service (API independiente)
$app->post('/send', function ($request, $response) {
    $data = $request->getParsedBody();
    mail($data['email'], "Order Created", "Your order #{$data['order_id']} was created.");
    return $response->withJson(['status' => 'sent']);
});
````
### Después de extraer a microservicio (Slim API)
````php

class OrderController {
    public function store(Request $request) {
        $order = Order::create($request->all());
        Http::post('http://notification-service/send', [
            'email' => $order->user->email,
            'order_id' => $order->id
        ]);
        return response()->json($order);
    }
}
````
## Bibliografia

Fowler, M. (2015). Microservices: a definition of this new architectural term. MartinFowler.com. https://martinfowler.com/articles/microservices.html

Balalaie, A., Heydarnoori, A., & Jamshidi, P. (2016). Microservices architecture enables DevOps: Migration to a cloud-native architecture. IEEE Software, 33(3), 42–52. https://doi.org/10.1109/MS.2016.64

Vogel, A. (2019). Migrating from Monolith to Microservices. InfoQ. https://www.infoq.com/articles/migrating-monolith-microservices/

PHP-FIG. (2020). PHP Standards Recommendations (PSR). PHP Framework Interop Group. https://www.php-fig.org/psr/
