<h1 style="text-align: center;">Patrón Mediator</h1>

# Propósito
Mediator es un patrón de diseño de comportamiento que te permite reducir las dependencias caóticas entre objetos. El patrón restringe las comunicaciones directas entre los objetos, forzándolos a colaborar únicamente a través de un objeto mediador.

<p align="center">
  <img src="https://refactoring.guru/images/patterns/content/mediator/mediator.png" alt="image">
</p>


# Analogía en el mundo real

Los pilotos de los aviones que llegan o salen del área de control del aeropuerto no se comunican directamente entre sí. En lugar de eso, hablan con un controlador de tráfico aéreo, que está sentado en una torre alta cerca de la pista de aterrizaje. Sin el controlador de tráfico aéreo, los pilotos tendrían que ser conscientes de todos los aviones en las proximidades del aeropuerto y discutir las prioridades de aterrizaje con un comité de decenas de otros pilotos. Probablemente, esto provocaría que las estadísticas de accidentes aéreos se dispararan.

La torre no necesita controlar el vuelo completo. Sólo existe para imponer límites en el área de la terminal porque el número de actores implicados puede resultar difícil de gestionar para un piloto.
<p align="center">
  <img src="https://github.com/user-attachments/assets/7e39e11f-9b9a-41e8-b7d6-d5509ea26961" alt="image">
</p>


# Estructura

Los Componentes son varias clases que contienen cierta lógica de negocio. Cada componente tiene una referencia a una interfaz mediadora, declarada con su tipo. El componente no conoce la clase de la interfaz mediadora, por lo que puedes reutilizarlo en otros programas vinculándolo a una mediadora diferente.

La interfaz Mediadora declara métodos de comunicación con los componentes, que normalmente incluyen un único método de notificación. Los componentes pueden pasar cualquier contexto como argumentos de este método, incluyendo sus propios objetos, pero sólo de tal forma que no haya acoplamiento entre un componente receptor y la clase del emisor.

<p align="center">
  <img src="https://github.com/user-attachments/assets/ab50924f-1b30-4f8b-a1c3-afdedcd7fddb" alt="image">
</p>

Los Mediadores Concretos encapsulan las relaciones entre varios componentes. Los mediadores concretos a menudo mantienen referencias a todos los componentes que gestionan y en ocasiones gestionan incluso su ciclo de vida.

Los componentes no deben conocer otros componentes. Si le sucede algo importante a un componente, o dentro de él, sólo debe notificar a la interfaz mediadora. Cuando la mediadora recibe la notificación, puede identificar fácilmente al emisor, lo cual puede ser suficiente para decidir qué componente debe activarse en respuesta.

Desde la perspectiva de un componente, todo parece una caja negra. El emisor no sabe quién acabará gestionando su solicitud, y el receptor no sabe quién envió la solicitud.

# Pseudocódigo

En este ejemplo, el patrón Mediator te ayuda a eliminar dependencias mutuas entre varias clases UI: botones, casillas y etiquetas de texto.

<p align="center">
  <img src="https://github.com/user-attachments/assets/ec49dd63-b16c-4579-bf74-70e3758a94a5" alt="image">
</p>

Un elemento activado por un usuario, no se comunica directamente con otros elementos, aunque parezca que debería. En lugar de eso, el elemento solo necesita dar a conocer el evento al mediador, pasando la información contextual junto a la notificación.
En este ejemplo, el diálogo de autenticación actúa como mediador. Sabe cómo deben colaborar los elementos concretos y facilita su comunicación indirecta. Al recibir una notificación sobre un evento, el diálogo decide qué elemento debe encargarse del evento y redirige la llamada en consecuencia.

# Codigo

```c#
El cliente desencadena la preparación para la reunión.
Alice se está preparando para la reunión.
El coordinador reacciona a la Reunión y desencadena las siguientes operaciones:
Bob está enviando el informe.

El cliente desencadena la fecha límite.
Bob está compilando el informe.

```

# Output
<pre>
El cliente desencadena la preparación para la reunión.
Alice se está preparando para la reunión.
El coordinador reacciona a la Reunión y desencadena las siguientes operaciones:
Bob está enviando el informe.

El cliente desencadena la fecha límite.
Bob está compilando el informe.

</pre>


