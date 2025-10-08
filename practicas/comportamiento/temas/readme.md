
# 💥 Lista de 45 Problemas (Code Smells) — Patrones de Comportamiento (GoF)

> Aplicables para refactorización individual o en duplas. Recomendado para prácticas de 45 minutos por bloque.

---

## 📘 ¿Cómo usar esta lista?

* Elige por lista ya conocida el numero de problemas para resolver en la sesión.
* Ahora que **identifiquen el patrón ausente** y que **justifiquen su elección con GoF**.
* Exige que implementen solo una **parte funcional** del refactor, no el sistema completo.
* Estamos trabajando con http://dotnetfiddle.net para compartir funcionamiento y validar el caso de manera breve.
---


### 🔍 Problemas Generadores de Code Smells

1. **Lógica de descuentos con 7 `if` anidados**, donde cada tipo de cliente aplica una condición diferente. ❌ → ¿Strategy o Chain of Responsibility?

2. Se usa un `switch-case` para definir cómo mostrar un documento según su tipo (`PDF`, `Word`, `Excel`).
   ❌ → ¿Qué pasa con Template Method?

3. Una clase `Factura` tiene una propiedad `estado`, y múltiples `if` que cambian su comportamiento según si es "Pagada", "Pendiente" o "Cancelada".
   ❌ → ¿State, tal vez?

4. Se implementan 3 observadores como `if` en un método `ActualizarPantallas()`, en lugar de un mecanismo flexible de suscripción.
   ❌ → ¿Dónde está el Observer?

5. En un flujo de pagos, el código pregunta varias veces: `if(tipoTarjeta == "VISA")`, `if(tipoTarjeta == "MASTERCARD")`, `if(tipoTarjeta == "AMEX")`.
   ❌ → Abuso del `switch`, falta de Command o Strategy.

6. Clase `Motor` que realiza las siguientes tareas: prende motor, activa sensores, mide temperatura, envía datos por red, y registra en log.
   ❌ → ¿Mediator o Command podrían ayudar?

7. El código de backup guarda el estado del sistema en una propiedad pública y se restaura accediendo directamente a variables.
   ❌ → Ruptura de encapsulamiento. ¿Dónde está el Memento?

8. Cada vez que se recibe una orden, se hacen múltiples validaciones en el mismo método `ProcesarOrden()`, sin poder extenderlo.
   ❌ → ¡Ideal para Template Method!

9. En lugar de iterar con un `foreach`, el código accede directamente al índice del array manualmente, sin validación.
   ❌ → ¿Y el Iterator?

10. Tres clases diferentes acceden directamente entre sí para coordinar acciones (UI, lógica, base de datos), generando acoplamiento circular.
    ❌ → Mediator no se está usando correctamente.

11. El botón "Deshacer" de una aplicación solo borra el último cambio con un `undo()` sin saber qué comando se ejecutó.
    ❌ → Command sin historial. ¡Falta Command con Undo!

12. Se repite el mismo bloque de código en varias clases para calcular el total con IVA.
    ❌ → Repetición + falta de Strategy.

13. Cada vez que se recibe una notificación, se evalúan múltiples condiciones para ver a quién avisar (correo, SMS, push).
    ❌ → Observer mal implementado.

14. Se usan `bool` y `enum` para decidir qué comportamiento seguir en tiempo de ejecución.
    ❌ → Puede resolverse con State o Strategy.

15. Los objetos acceden directamente al estado interno de otros objetos para modificarlo.
    ❌ → Rompe encapsulamiento. ¿Dónde está Memento?

16. Una misma clase ejecuta los comandos y también los define.
    ❌ → Falta de separación clara de responsabilidades. ¿Command?

17. Se crean múltiples clases casi iguales solo para variar un pequeño paso de un algoritmo.
    ❌ → Template Method mal aplicado o sin aplicar.

18. El código intenta implementar una "máquina de estados", pero con `switch` por todos lados.
    ❌ → ¡Falta patrón State!

19. No se puede iterar sobre una colección personalizada sin conocer su estructura interna.
    ❌ → Iterator ausente.

20. En una app de chat, cada módulo notifica manualmente a todos los demás sin usar Observer.
    ❌ → Acoplamiento brutal.

21. Para validar una orden, se usan 8 pasos consecutivos en el mismo método `Validar()`, sin posibilidad de extensión.
    ❌ → Candidate para Chain of Responsibility.

22. Se usa una clase `Notificador` con `if` para decidir si se envía correo, SMS o notificación push.
    ❌ → Falta Strategy o Command.

23. Una clase de "Controlador" tiene más de 500 líneas con lógica de múltiples módulos.
    ❌ → God Object, necesita Mediator y otros refactors.

24. Una clase visitante (Visitor) intenta visitar nodos sin interfaz común.
    ❌ → Visitor mal aplicado.

25. Lógica compleja en clases pequeñas que dependen entre sí y que cambian frecuentemente.
    ❌ → Mediator podría mejorar el diseño.

26. Código que recorre listas y al mismo tiempo modifica sus elementos directamente.
    ❌ → Mal uso del Iterator.

27. Un plugin tiene múltiples pasos para ejecutarse, pero estos pasos no son reutilizables ni intercambiables.
    ❌ → Template Method ausente.

28. Se encapsula mal el historial de acciones, perdiendo la capacidad de deshacer.
    ❌ → Falta Memento o Command con Undo.

29. Una clase `Procesador` tiene múltiples métodos para distintas órdenes (`ProcesarFactura`, `ProcesarPago`, etc.)
    ❌ → ¡Usa Command Pattern!

30. Todos los cambios en el objeto `Cliente` se realizan desde una clase `GestorClientes`.
    ❌ → Rompe SRP. ¿Puede Visitor o Memento ayudar?

31. Se repite código para enviar mensajes dependiendo del tipo de cliente (frecuente, nuevo, vip).
    ❌ → Strategy o Chain podrían ser opción.

32. El código mezcla pasos obligatorios y opcionales sin una estructura clara.
    ❌ → Candidate para Template Method.

33. Los cambios de estado de un objeto se manejan con múltiples `if` sin transición clara.
    ❌ → ¡State al rescate!

34. Varias clases tienen métodos similares como `Ejecutar()`, pero cada uno lo hace distinto.
    ❌ → Falta de abstracción común. ¿Command, Strategy o Template?

35. Se crea una nueva instancia de `Logger` cada vez que se ejecuta un método para guardar cambios.
    ❌ → Acoplamiento innecesario. ¿Observer?

36. La clase `CarritoDeCompras` conoce detalles de cada tipo de producto para aplicar reglas distintas.
    ❌ → Visitor podría desacoplar esto.

37. Los métodos para exportar reportes (`PDF`, `Excel`, `CSV`) están todos juntos en una clase enorme.
    ❌ → Strategy para separar comportamientos.

38. Se crea una clase por cada algoritmo de búsqueda sin reutilización de lógica común.
    ❌ → Template Method mal aprovechado.

39. Una clase envía una solicitud de cambio y luego pregunta por su estado varias veces.
    ❌ → ¿Dónde está Observer o State?

40. La lógica de notificación se encuentra duplicada en varios lugares.
    ❌ → Observer no implementado.

41. Los objetos se configuran según propiedades en tiempo de ejecución (`if config.Tipo == "A"`).
    ❌ → Strategy permitiría polimorfismo.

42. En un menú de comandos, todo se gestiona con `switch` y no se pueden agregar nuevos sin tocar el código existente.
    ❌ → Command podría solucionarlo.

43. El algoritmo de cálculo de precio incluye múltiples reglas por tipo de producto, metidas en `if`.
    ❌ → Strategy al rescate.

44. Se actualizan componentes de UI directamente desde la lógica de negocio.
    ❌ → Mediator o Observer romperían el acoplamiento.

45. Una clase implementa `Ejecutar`, `Deshacer` y `Rehacer`, pero tiene lógica duplicada en cada método.
    ❌ → Command + Memento ayudarían a desacoplar y encapsular.

---
