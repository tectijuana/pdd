# Arquitecturas evolutivas y su relación con refactorización progresiva
# Alvarado Cardona Antonio 22210279

## Arquitecturas evolutivas

Las arquitecturas evolutivas son un enfoque de diseño de software en el que la arquitectura no se define como algo rígido y definitivo, sino como un sistema que puede adaptarse y mejorar con el tiempo.
Se basan en principios como:

**Adaptabilidad:** La arquitectura está preparada para responder a cambios en requerimientos, tecnologías o negocio.

**Incrementalidad:** El sistema crece poco a poco, en iteraciones.

**Feedback continuo:** Se mide la calidad de la arquitectura a través de pruebas, métricas y validaciones.

**Resiliencia al cambio:** Se minimiza el costo de introducir nuevas funcionalidades.

En lugar de “diseñar todo desde el principio”, la arquitectura se construye en paralelo con el desarrollo del software, evolucionando conforme el sistema crece.

### Refactorización progresiva

La refactorización progresiva es la práctica de mejorar continuamente el código existente, sin cambiar su funcionalidad observable, para:

- Reducir deuda técnica.
- Mejorar la legibilidad y mantenibilidad.
- Alinear la implementación con los principios de diseño.

Se hace en pequeños pasos controlados, acompañados de pruebas automatizadas, para evitar introducir errores.

## Relación entre arquitecturas evolutivas y refactorización progresiva
La conexión es muy estrecha:

#### Mantenimiento de la calidad arquitectónica:
Una arquitectura evolutiva necesita mantenerse flexible y sana en el tiempo. La refactorización progresiva permite ajustar el código y los módulos para que la arquitectura no se deteriore.

**Adaptación a cambios:** Cuando los requerimientos cambian, la refactorización facilita modificar componentes internos sin romper la funcionalidad externa, lo cual mantiene la capacidad evolutiva.

**Minimización de deuda técnica:** Si la arquitectura evoluciona pero no se refactoriza el código, la deuda técnica se acumula. Con refactorización progresiva se asegura que la evolución sea sostenible.

**Iteración conjunta:** Mientras la arquitectura evoluciona en ciclos (añadiendo nuevas capacidades), la refactorización acompaña cada paso para mantener la coherencia y robustez del sistema.

## Ejemplo práctico: Sistema de pedidos de café
### 1. Versión inicial (arquitectura simple)

Todo está en una sola clase:
``` java
class CafeApp {
    void tomarPedido() { ... }
    void prepararCafe() { ... }
    void cobrar() { ... }
}
```

Funciona bien al inicio porque el negocio es pequeño.

### 2. Problema (necesidad de evolución)

El negocio crece y muchos baristas usan la app al mismo tiempo.

Difícil mantener el código.

Si falla “cobrar”, también se rompe “tomarPedido”.

### 3. Refactorización progresiva

En lugar de reescribir todo:

Paso 1: Extraer responsabilidades en clases separadas.
``` java
class PedidoService { void tomarPedido() { ... } }
class CafeService { void prepararCafe() { ... } }
class PagoService { void cobrar() { ... } }
```

Paso 2: Conectar los servicios poco a poco.
``` java
class CafeApp {
    PedidoService pedido = new PedidoService();
    CafeService cafe = new CafeService();
    PagoService pago = new PagoService();
}
```
### 4. Resultado

La arquitectura evoluciona de un monolito desordenado a un sistema modular.

La refactorización progresiva permitió hacerlo en pasos pequeños, sin detener el negocio.


### En resumen:
Las arquitecturas evolutivas permiten que el sistema cambie y crezca de manera flexible, mientras que la refactorización progresiva es la herramienta práctica que asegura que esos cambios puedan hacerse de forma ordenada, sostenible y sin pérdida de calidad.
