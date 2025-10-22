
# Over-Engineering #
Eduardo Gallardo Dueñas 21212215 - 21/10/25

## Definición: 
El antipatrón Over-Engineering (o sobreingeniería) ocurre cuando un desarrollador o equipo implementa una solución más compleja de lo necesario para resolver un problema. Se da cuando el código incluye abstracciones innecesarias, funciones no solicitadas, o estructuras demasiado genéricas que no aportan valor inmediato al proyecto.

En pocas palabras, es cuando se busca “hacer el código perfecto o demasiado flexible”, sacrificando simplicidad, mantenibilidad y tiempo de desarrollo.

Por qué es una mala práctica:

- Aumenta la complejidad del sistema sin necesidad.

- Dificulta el mantenimiento y la comprensión del código.

- Provoca retrasos en la entrega del software.

- Incrementa el riesgo de errores al añadir capas o funcionalidades no requeridas.

- Viola el principio KISS (Keep It Simple, Stupid), uno de los pilares del buen diseño de software.


## Ejemplo en Python: una solución sobreingenierizada para imprimir un saludo 
``` python
from abc import ABC, abstractmethod

# Se crea una jerarquía de clases completamente innecesaria
class IGreeting(ABC):
    @abstractmethod
    def greet(self):
        pass

class Greeting(IGreeting):
    def __init__(self, message_strategy):
        self.message_strategy = message_strategy

    def greet(self):
        return self.message_strategy.get_message()

class MessageStrategy(ABC):
    @abstractmethod
    def get_message(self):
        pass

class EnglishMessage(MessageStrategy):
    def get_message(self):
        return "Hello, world!"

class SpanishMessage(MessageStrategy):
    def get_message(self):
        return "¡Hola, mundo!"

# Uso
greeting = Greeting(EnglishMessage())
print(greeting.greet())
```
Problema:
El código anterior utiliza interfaces, clases abstractas y estrategias solo para mostrar un simple “Hola mundo”. Esto no aporta valor real y hace que el código sea más difícil de mantener.

## El mismo resultado se podría obtener con una función sencilla:
``` python
def greet(language="en"):
    if language == "es":
        print("¡Hola, mundo!")
    else:
        print("Hello, world!")

greet("es")
```

Conclusión del ejemplo:
El primer enfoque es un claro caso de Over-Engineering: intenta aplicar patrones de diseño donde no son necesarios.

# Consecuencias

| **Consecuencia**               | **Descripción**                                                                                        |
| ------------------------------ | ------------------------------------------------------------------------------------------------------ |
| **Mantenimiento difícil**      | Más clases, interfaces y dependencias dificultan la lectura y actualización del código.                |
| **Mayor costo y tiempo**       | Los desarrolladores invierten tiempo diseñando una arquitectura que no aporta funcionalidad adicional. |
| **Riesgo de errores**          | Cuantas más capas se añaden, más puntos de fallo potenciales existen.                                  |
| **Falsa sensación de calidad** | Aunque el código “parezca” profesional por su complejidad, no necesariamente es eficiente ni útil.     |
| **Baja productividad**         | Los equipos pierden agilidad al tener que comprender una estructura innecesariamente complicada.       |

# Solución Correctiva
Para evitar caer en Over-Engineering, se deben seguir principios de diseño simples y orientados a la necesidad real:

## Buenas prácticas

1. Aplicar el principio KISS: Mantén el código lo más simple posible.

2. Evitar el YAGNI (You Aren’t Gonna Need It): No implementes funcionalidades que el usuario no ha pedido.

3. Refactorizar progresivamente: Mejora el código solo cuando sea necesario.

4. Diseñar para el presente: No escribas código en función de posibles casos futuros que aún no existen.

5. Revisar en equipo: Un code review puede detectar complejidad innecesaria.

## Patrones alternativos

- Patrón Strategy o Factory solo cuando exista una necesidad real de extensibilidad.

- Principio de responsabilidad única (SRP): Cada módulo debe tener un propósito claro y limitado.

- Patrón Simple Function o Procedural Design cuando la lógica es directa.

# Conclusión General

El antipatrón Over-Engineering es un error común en el desarrollo de software cuando los programadores buscan anticiparse a necesidades futuras o aplicar patrones de forma innecesaria. La clave está en mantener un equilibrio entre simplicidad, claridad y escalabilidad, aplicando la ingeniería solo hasta el punto que el problema lo requiere.
