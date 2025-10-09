# Clase Motor que realiza las siguientes tareas: prende motor, activa sensores, mide temperatura, envía datos por red, y registra en log. ❌ → ¿Mediator o Command podrían ayudar?

**Nombre:** Ruben Campos Rivas  
**Número de Control:** 21211926  

---

## Introducción

En el desarrollo de software, es fundamental mantener un código limpio, modular y fácil de mantener. La clase `Motor`, que realiza múltiples tareas como prender el motor, activar sensores, medir temperatura, enviar datos por red y registrar en log, puede volverse compleja y difícil de extender si todas estas responsabilidades se concentran en una sola clase. Para solucionar este problema, se utiliza el patrón de diseño **Command**, que permite encapsular cada acción en un objeto separado, promoviendo la reutilización, flexibilidad y desacoplamiento del código.

---

## Justificación

El patrón **Command** es adecuado para este caso porque permite separar las distintas operaciones del motor en comandos individuales que el objeto motor puede ejecutar en secuencia. Esto facilita la modificación o extensión del comportamiento sin necesidad de modificar la clase principal, mejorando la mantenibilidad y escalabilidad del sistema. Además, facilita la gestión de operaciones complejas, control de ejecución y posibles deshacer o repeticiones de comandos en escenarios más avanzados.

---

# Código Malo (Sin patrón, todo junto)

```python
class Motor:
    def __init__(self):
        self.sensores_activados = False
        self.temperatura = 0

    def prender_motor(self):
        print("Motor encendido")

    def activar_sensores(self):
        self.sensores_activados = True
        print("Sensores activados")

    def medir_temperatura(self):
        self.temperatura = 75  # ejemplo fijo
        print(f"Temperatura medida: {self.temperatura}°C")

    def enviar_datos_por_red(self):
        print(f"Enviando datos: temperatura {self.temperatura}°C")

    def registrar_log(self, mensaje):
        print(f"Log: {mensaje}")

    def operar(self):
        self.prender_motor()
        self.activar_sensores()
        self.medir_temperatura()
        self.enviar_datos_por_red()

        self.registrar_log("Ciclo de operación completado")
        
```
# Código Mejorado con Command Pattern

```python
from abc import ABC, abstractmethod

# Comando abstracto
class Command(ABC):
    @abstractmethod
    def execute(self):
        pass

# Comandos concretos
class PrenderMotorCommand(Command):
    def execute(self):
        print("Motor encendido")

class ActivarSensoresCommand(Command):
    def execute(self):
        print("Sensores activados")

class MedirTemperaturaCommand(Command):
    def __init__(self):
        self.temperatura = 0

    def execute(self):
        self.temperatura = 75  # ejemplo fijo
        print(f"Temperatura medida: {self.temperatura}°C")
        return self.temperatura

class EnviarDatosPorRedCommand(Command):
    def __init__(self, temperatura):
        self.temperatura = temperatura

    def execute(self):
        print(f"Enviando datos: temperatura {self.temperatura}°C")

class RegistrarLogCommand(Command):
    def __init__(self, mensaje):
        self.mensaje = mensaje

    def execute(self):
        print(f"Log: {self.mensaje}")

# Motor solo ejecuta comandos
class Motor:
    def __init__(self):
        self.commands = []

    def add_command(self, command):
        self.commands.append(command)

    def operar(self):
        temperatura = None
        for command in self.commands:
            if isinstance(command, MedirTemperaturaCommand):
                temperatura = command.execute()
            elif isinstance(command, EnviarDatosPorRedCommand):
                command.temperatura = temperatura
                command.execute()
            else:
                command.execute()

# Uso
motor = Motor()
motor.add_command(PrenderMotorCommand())
motor.add_command(ActivarSensoresCommand())
motor.add_command(MedirTemperaturaCommand())
motor.add_command(EnviarDatosPorRedCommand(temperatura=None))  # temperatura se actualiza en operar
motor.add_command(RegistrarLogCommand("Ciclo de operación completado"))
motor.operar()
