# Refactor Creacional - Daniel Omar Gonzalez Martinez 

## Problemas detectados
1. La clase `VehiculoFactory` no tenía documentación indicando que usa el patrón **Factory Method**, parecía solo una clase normal.  
2. La clase `ServicioPaqueteBuilder` no explicaba que aplicaba el patrón **Builder**, lo que hace difícil entenderlo a simple vista.  
3. La clase `ConexionDB` estaba como **Singleton**, pero no lo decía en ninguna parte y además no era segura para hilos.  

---

## Cambios realizados
- Se agregaron comentarios XML en cada clase indicando qué patrón usa.  
- `VehiculoFactory` ahora usa polimorfismo en lugar de condicionales.  
- `ServicioPaqueteBuilder` tiene pasos claros de construcción.  
- `ConexionDB` se implementó como Singleton seguro para hilos y con documentación.  

---

## Justificación
Con la documentación cualquier persona que vea el código entiende de inmediato qué patrón está aplicado.  
Esto mejora la legibilidad y ayuda a que no se confunda con una clase común.  

---

## Impacto
- Más fácil mantener el código.  
- Otros compañeros pueden entender rápido el diseño.  
- Se evita que alguien modifique sin saber que hay un patrón intencional.  

---

## Ejemplos en C#

```csharp
/// <summary>
/// Patrón: Factory Method
/// Crea objetos de tipo Vehiculo sin exponer la lógica de instanciación.
/// </summary>
public abstract class VehiculoFactory
{
    public abstract Vehiculo CrearVehiculo();
}
```

```csharp
/// <summary>
/// Patrón: Singleton
/// Garantiza que exista solo una instancia de la conexión a base de datos.
/// </summary>
public sealed class ConexionDB
{
    private static readonly object candado = new object();
    private static ConexionDB instancia;

    private ConexionDB() { }

    public static ConexionDB Instancia
    {
        get
        {
            if (instancia == null)
            {
                lock (candado)
                {
                    if (instancia == null)
                    {
                        instancia = new ConexionDB();
                    }
                }
            }
            return instancia;
        }
    }
}
```

```csharp
/// <summary>
/// Patrón: Builder
/// Permite construir paso a paso un objeto de tipo Paquete.
/// </summary>
public class ServicioPaqueteBuilder
{
    private Paquete _paquete = new Paquete();

    public ServicioPaqueteBuilder AgregarHabitacion(string tipo)
    {
        _paquete.Habitacion = tipo;
        return this;
    }

    public ServicioPaqueteBuilder AgregarComida(string menu)
    {
        _paquete.Comida = menu;
        return this;
    }

    public Paquete Build()
    {
        return _paquete;
    }
}
```

```csharp
public class Paquete
{
    public string Habitacion { get; set; }
    public string Comida { get; set; }

    public override string ToString()
    {
        return $"Habitación: {Habitacion}, Comida: {Comida}";
    }
}
```
