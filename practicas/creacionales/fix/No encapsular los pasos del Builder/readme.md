# Refactorizando Patrones Creacionales - No encapsular los pasos del Builder - Diego Huerta

---

## Nombre del problema
**No encapsular los pasos del Builder**

---

##  Problemas detectados
1. **Clase `VehiculoBuilder` expone pasos sin encapsular**: el cliente tiene que llamar a los métodos en orden correcto manualmente, lo que genera inconsistencias (ejemplo: crear un auto sin motor).  
2. **Violación del Principio de Responsabilidad Única**: la misma clase mezcla lógica de construcción con lógica de validación del objeto.  
3. **Rigidez ante cambios**: agregar un nuevo tipo de `Vehiculo` obliga a modificar el Builder y el código cliente, en lugar de extender.  

---

##  Patrón aplicado
- Se **implementa correctamente el patrón Builder** encapsulando la lógica de construcción paso a paso dentro del **Director (`VehiculoDirector`)**.  
- Se asegura que el cliente solo llame a `construirVehiculoCompleto()`, y no a pasos internos que puedan romper la coherencia.  
- Se corrige el diseño para que los objetos se construyan de manera **consistente y predecible**.  

---

##  Justificación del cambio
Con esta refactorización:  

- 🔹 **Cohesión interna**: el Builder solo construye, el Director controla el orden de pasos.  
- 🔹 **Testabilidad**: podemos probar cada parte del Builder sin depender de la secuencia manual de llamadas.  
- 🔹 **Flexibilidad**: se pueden agregar nuevos tipos de vehículos (`MotoBuilder`, `CamionBuilder`) sin afectar al cliente.  

---

##  Impacto
- Se evita que el cliente final tenga que saber el orden de los pasos.  
- Se asegura el cumplimiento del **Principio de Inversión de Dependencias (DIP)**, ya que el cliente solo conoce la interfaz `Builder`.  
- Se prepara la arquitectura para pruebas unitarias y extensibilidad.  

---
##  Antes
El cliente llamaba directamente a los métodos del `Builder`.  
Esto era propenso a errores si olvidaba algún paso (ejemplo: no construir el motor).

```csharp
var builder = new VehiculoBuilder();

// El cliente controla el orden 
// Si se olvida de llamar a ConstruirMotor(), el vehículo queda incompleto
builder.ConstruirRuedas();
builder.ConstruirCarroceria();

var auto = builder.GetVehiculo();
Console.WriteLine(auto);
```
##  Después

Se agregó un Director (VehiculoDirector) que encapsula los pasos.
Ahora el cliente solo pide un vehículo completo, sin preocuparse por el orden de construcción.
```csharp

var builder = new AutoBuilder();
var director = new VehiculoDirector(builder);

// El cliente obtiene un objeto consistente 
// El Director se encarga de los pasos internos
var auto = director.ConstruirVehiculoCompleto();

Console.WriteLine(auto);
```
