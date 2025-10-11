Tres clases diferentes acceden directamente entre sí para coordinar acciones (UI, lógica, base de datos), generando acoplamiento circular. ❌ → Mediator no se está usando correctamente.

El patrón Mediator está diseñado para reducir el acoplamiento entre clases, centralizando la comunicación en un "mediador" en lugar de permitir que las clases se comuniquen directamente entre sí. Esto es útil cuando tienes múltiples componentes (como UI, lógica de negocio y base de datos) que necesitan coordinar acciones sin crear dependencias circulares.
En el escenario que mencionas, si tres clases (por ejemplo, UI, lógica y base de datos) acceden directamente entre sí y esto genera un acoplamiento circular, entonces no se está utilizando correctamente el Mediator. El objetivo del Mediator es evitar que las clases interactúen directamente, sino que deben comunicarse a través del mediador.

##Problema:
Acoplamiento circular: Si las clases A, B y C se llaman entre sí, se crea un ciclo de dependencias que hace que el sistema sea difícil de entender y mantener.
Falta de un intermediario claro: El Mediator debe ser quien coordine las acciones entre las clases, pero en tu caso parece que las clases están gestionando estas interacciones directamente.

##Solución (cómo debería ser):
Centralizar la comunicación: Las clases deben enviar mensajes o eventos al mediador, quien luego gestionará la comunicación entre ellas.
UI solo debería comunicarse con el Mediator.
Lógica solo debería comunicarse con el Mediator.
Base de datos solo debería comunicarse con el Mediator.

El mediador será el responsable de decidir qué clase necesita recibir la información o qué acciones tomar.

Ejemplo:
##Sin Mediator (acoplamiento directo y circular):
class UI:
    def update_data(self, data):
        # Llamada directa a la lógica
        logic.process_data(data)

class Logic:
    def process_data(self, data):
        # Llamada directa a la base de datos
        database.save_data(data)

class Database:
    def save_data(self, data):
        print("Saving data:", data)

##Con Mediator (sin acoplamiento circular):
class Mediator:
    def __init__(self):
        self.ui = UI(self)
        self.logic = Logic(self)
        self.database = Database(self)

    def notify(self, sender, event, data=None):
        if event == "data_updated":
            self.logic.process_data(data)
        elif event == "data_processed":
            self.database.save_data(data)

class UI:
    def __init__(self, mediator):
        self.mediator = mediator

    def update_data(self, data):
        # Envía la solicitud al Mediator
        self.mediator.notify(self, "data_updated", data)

class Logic:
    def __init__(self, mediator):
        self.mediator = mediator

    def process_data(self, data):
        # Envia la solicitud de procesamiento al Mediator
        self.mediator.notify(self, "data_processed", data)

class Database:
    def __init__(self, mediator):
        self.mediator = mediator

    def save_data(self, data):
        print("Saving data:", data)

##Beneficios:
Desacoplamiento: Las clases no tienen que saber nada sobre las otras. Solo interactúan con el mediador.
Facilidad de mantenimiento: Cambiar la forma en que interactúan las clases no afecta al resto del sistema, ya que todo pasa a través del mediador.
Flexibilidad: El mediador puede ser modificado para manejar nuevas reglas de negocio sin tener que modificar las clases que usan el sistema.
