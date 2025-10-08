# Una misma clase ejecuta los comandos y también los define. ❌ → Falta de separación clara de responsabilidades. ¿Command?

Eduardo Gallardo Dueñas 21212215 - 07/10/25

## Código con el problema (sin aplicar patrón) — Falta de separación de responsabilidades

``` cs
 using System;
public class RemoteControl
{
    // Este método mezcla la lógica de ejecución y definición de comandos.
    // La clase RemoteControl tiene conocimiento directo de lo que hace cada acción.
    public void PressButton(string action)
    {
        if (action == "ON")
        {
            Console.WriteLine("La luz se ha encendido");
        }
        else if (action == "OFF")
        {
            Console.WriteLine("La luz se ha apagado");
        }
        else
        {
            Console.WriteLine("Acción no reconocida");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Se crea un control remoto que ejecuta acciones directas.
        RemoteControl control = new RemoteControl();
        // La clase principal le pasa comandos como texto.
        // Si se agregan nuevas acciones, hay que modificar RemoteControl.
        control.PressButton("ON");   // Output: La luz se ha encendido
        control.PressButton("OFF");  // Output: La luz se ha apagado
    }
}
```
### Problemas:

- La clase RemoteControl viola el principio de responsabilidad única (SRP).

- Si se agregan nuevas funciones (como “atenuar” o “cambiar color”), se debe modificar la clase, violando el principio de abierto/cerrado (OCP).

- No hay una estructura flexible para agregar o manejar comandos de forma independiente.


## ✅ Código refactorizado aplicando el patrón Command — Separación de responsabilidades



``` cs
using System;
// Interfaz que define el contrato de un comando.
public interface ICommand
{
    void Execute(); // Método que ejecutará el comando.
}
// Clase Receptora (Receiver): conoce los detalles de cómo realizar la acción.
public class Light
{
    public void TurnOn()
    {
        Console.WriteLine("La luz se ha encendido");
    }
    public void TurnOff()
    {
        Console.WriteLine("La luz se ha apagado");
    }
}
// Comando concreto para encender la luz.
// Encapsula la acción "encender" y delega la ejecución al receptor.
public class LightOnCommand : ICommand
{
    private Light _light; // Referencia al receptor
    public LightOnCommand(Light light)
    {
        _light = light;
    }
    public void Execute()
    {
        _light.TurnOn(); // Delegamos la acción al receptor.
    }
}
// Comando concreto para apagar la luz.
// Encapsula la acción "apagar" y delega la ejecución al receptor.
public class LightOffCommand : ICommand
{
    private Light _light; // Referencia al receptor
    public LightOffCommand(Light light)
    {
        _light = light;
    }
    public void Execute()
    {
        _light.TurnOff(); // Delegamos la acción al receptor.
    }
}
// Invocador (Invoker): ejecuta comandos sin conocer sus detalles.
public class RemoteControl
{
    private ICommand _command; // Referencia al comando actual
    // Asigna un comando al control remoto.
    public void SetCommand(ICommand command)
    {
        _command = command;
    }
    // Ejecuta el comando asignado.
    public void PressButton()
    {
        _command.Execute(); // Llama al método Execute() sin saber qué hace.
    }
}
// Programa principal
class Program
{
    static void Main(string[] args)
    {
        // Creamos el receptor: el objeto que realmente realiza la acción.
        Light light = new Light();
        // Creamos los comandos concretos, pasando el receptor.
        ICommand turnOn = new LightOnCommand(light);
        ICommand turnOff = new LightOffCommand(light);
        // Creamos el invocador.
        RemoteControl remote = new RemoteControl();
        // Asignamos el comando "encender" al invocador y lo ejecutamos.
        remote.SetCommand(turnOn);
        remote.PressButton();   // Output: La luz se ha encendido
        // Asignamos el comando "apagar" al invocador y lo ejecutamos.
        remote.SetCommand(turnOff);
        remote.PressButton();   // Output: La luz se ha apagado
    }
}
```

## Justificación (según GoF y Shvets):


El patrón Command se utiliza para separar el objeto que invoca una operación del que la ejecuta.

Su propósito es encapsular una petición como un objeto, permitiendo parametrizar clientes con diferentes solicitudes, hacer colas o registros de comandos, y soportar operaciones como deshacer/rehacer.


En este caso, la misma clase está encargándose de definir y ejecutar las acciones, lo que viola el principio de responsabilidad única (SRP). El patrón Command corrige esa falta de separación de responsabilidades, ya que introduce tres papeles:

## Conclusión general

La base de datos presenta una estructura funcional, pero con debilidades de consistencia y actualización.
El hallazgo más relevante es la falta de correspondencia entre los créditos aprobados y el estatus académico, lo cual debe atenderse prioritariamente para mantener la coherencia de los reportes institucionales.

Con la aplicación del plan de mejora continua, la base de datos podrá alcanzar niveles altos de integridad, completitud y confiabilidad, garantizando decisiones basadas en información precisa.
