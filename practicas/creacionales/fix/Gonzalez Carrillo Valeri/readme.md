# 🧪 Actividad de Cierre: Refactorizando Patrones Creacionales  
🎯 Objetivo  
Practicar la refactorización de patrones creacionales (GoF) detectando problemas reales de diseño (**code smells**) y aplicando soluciones limpias y elegantes.  

Este caso usa el contexto de una **tienda de ropa online** 👕🛒 donde se implementó mal el **Singleton**. _**Además de usar Singleton como contenedor global.**_

# 💥Refactor Creacional - Tienda Ropa Online 👕🛒  

### 🔍 Problemas detectados

1. **`CarritoCompras` como Singleton**  
   - Todos los usuarios comparten **el mismo carrito global**, que es un claro ejemplo de Singleton mal usado como contenedor global.  
   - Problemas: inconsistencia de datos, falta de aislamiento por usuario, errores en pedidos.  

2. **`Inventario` como Singleton mutable**  
   - Estado compartido globalmente entre todos los usuarios.  
   - Problema: concurrencia descontrolada → stock puede quedar negativo o incorrecto.  
   - Otro caso de Singleton usado como contenedor global de datos mutables.  

3. **`ConfiguracionPagos` como Singleton global**  
   - Cualquier clase puede cambiar el método de pago de toda la aplicación.  
   - Problema: rompe la seguridad y dificulta pruebas unitarias.  
   - Otro ejemplo de Singleton usado como contenedor global.  

---

### ✅ Soluciones aplicadas

1. **`CarritoFactory` + `CarritoCompras` por usuario**  
   - Se elimina el Singleton, ahora cada usuario tiene su propio carrito.  
   - Resultado: no hay contenedor global compartido.  

2. **`InventarioRepository` seguro con concurrencia**  
   - Se elimina el Singleton mutable y se maneja el inventario mediante un repositorio concurrente.  
   - Resultado: cada operación es controlada, sin depender de un estado global inseguro.  

3. **`Strategy` para métodos de pago**  
   - Se elimina el Singleton global de configuración de pagos.  
   - Ahora cada `Checkout` recibe la estrategia de pago que necesita.  
   - Resultado: flexible, seguro y testeable, sin contenedor global.  

---

### 🛠 Patrones aplicados  

- 🏗️ **Factory Method** para crear carritos por usuario.  
- 📦 **Repository** para manejar inventario de forma segura y concurrente.  
- 🔄 **Strategy** para implementar métodos de pago intercambiables.  

---

### 💡 Justificación del cambio  

Mejoramos:  

- 🧩 **Cohesión interna** → clases enfocadas en una sola responsabilidad.  
- 🧪 **Testabilidad** → fácil inyección de dependencias y mocks.  
- 🔧 **Flexibilidad ante cambios** → nuevos carritos, inventarios o métodos de pago sin alterar código cliente.  

---

### 🔄 Impacto  

- 🚫 Eliminación del **Singleton como contenedor global**.  
- ✅ Cumplimiento del **principio de inversión de dependencias (D de SOLID)**.  
- 🏗️ Arquitectura lista para **pruebas unitarias y entornos concurrentes**.  

---

## ❌ Código con Problemas (Mal uso de Singleton)  

### 1. Carrito de Compras Global  
```csharp
// ❌ Problema: Uso de Singleton global para todos los usuarios
public class CarritoCompras
{
    private static CarritoCompras _instancia;
    private List<string> _productos = new List<string>();

    private CarritoCompras() { }

    public static CarritoCompras Instancia
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = new CarritoCompras();
            }
            return _instancia;
        }
    }

    public void AgregarProducto(string producto)
    {
        _productos.Add(producto);
    }

    public List<string> ObtenerProductos()
    {
        return _productos;
    }
}
```
### 2. Inventario
```csahrp
// ❌ Problema: Singleton con estado mutable y sin control de concurrencia
public class Inventario
{
    private static Inventario _instancia;
    private Dictionary<string, int> _stock = new Dictionary<string, int>();

    private Inventario() { }

    public static Inventario Instancia
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = new Inventario();
            }
            return _instancia;
        }
    }

    public void ActualizarStock(string producto, int cantidad)
    {
        _stock[producto] = cantidad;
    }

    public int ObtenerStock(string producto)
    {
        return _stock.ContainsKey(producto) ? _stock[producto] : 0;
    }
}
```
### 3. ConfiguracionPagos
```csahrp
// ❌ Problema: Singleton como contenedor global, cualquier clase puede modificar la config
public class ConfiguracionPagos
{
    private static ConfiguracionPagos _instancia;
    public string MetodoPago { get; set; } = "TARJETA";

    private ConfiguracionPagos() { }

    public static ConfiguracionPagos Instancia
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = new ConfiguracionPagos();
            }
            return _instancia;
        }
    }
}
```
---
## ✅ Código Refactorizado con Patrones
1. CarritoCompras + CarritoFactory
```csharp
using System;
using System.Collections.Generic;

public class CarritoCompras
{
    private List<string> _productos = new List<string>();

    public void AgregarProducto(string producto)
    {
        _productos.Add(producto);
    }

    public IReadOnlyList<string> ObtenerProductos()
    {
        return _productos.AsReadOnly();
    }
}

public static class CarritoFactory
{
    public static CarritoCompras CrearCarrito()
    {
        return new CarritoCompras();
    }
}

public class Program
{
    public static void Main()
    {
        var carrito = CarritoFactory.CrearCarrito();
        carrito.AgregarProducto("Camiseta");
        carrito.AgregarProducto("Pantalón");

        foreach (var item in carrito.ObtenerProductos())
        {
            Console.WriteLine(item);
        }
    }
}
```
<img width="658" height="568" alt="image" src="https://github.com/user-attachments/assets/c2602e1e-1428-4c3a-ac51-afbb261f54fc" />

### 2. InventarioRepository (seguro con concurrencia)
```csahrp
// ✅ Solución: Repository con concurrencia controlada
using System;
using System.Collections.Concurrent;

// Esta es la clase que gestiona el inventario.
public class InventarioRepository
{
    private readonly ConcurrentDictionary<string, int> _stock = new();

    public void ActualizarStock(string producto, int cantidad)
    {
        _stock[producto] = cantidad;
    }

    public int ObtenerStock(string producto)
    {
        return _stock.TryGetValue(producto, out var cantidad) ? cantidad : 0;
    }
}

// Esta clase contiene el punto de entrada del programa (Main).
public class Program
{
    public static void Main(string[] args)
    {
        // Creamos una instancia de InventarioRepository.
        var inventario = new InventarioRepository();

        // Actualizamos el stock de productos de ropa.
        inventario.ActualizarStock("Blusas", 50);
        inventario.ActualizarStock("Faldas", 30);
        inventario.ActualizarStock("Pantalones", 75);

        // Obtenemos y mostramos el stock.
        int stockBlusas = inventario.ObtenerStock("Blusas");
        Console.WriteLine($"Stock de Blusas: {stockBlusas}");

        int stockFaldas = inventario.ObtenerStock("Faldas");
        Console.WriteLine($"Stock de Faldas: {stockFaldas}");

        int stockVestidos = inventario.ObtenerStock("Vestidos"); // Un producto que no se ha añadido.
        Console.WriteLine($"Stock de Vestidos: {stockVestidos}");
    }
}
```
<img width="737" height="518" alt="image" src="https://github.com/user-attachments/assets/20eb4428-716d-44a0-b02c-69f06a045931" />

### 3. Strategy para métodos de pago
```csharp
// ✅ Solución: Strategy para definir comportamientos de pago
using System; // Es necesario para usar Console.WriteLine

// La interfaz define la estructura común para todas las estrategias de pago.
public interface IEstrategiaPago
{
    void Pagar(decimal monto);
}

// Clase para la estrategia de pago con tarjeta.
public class PagoTarjeta : IEstrategiaPago
{
    public void Pagar(decimal monto)
    {
        Console.WriteLine($"Pagando {monto:C} con tarjeta.");
    }
}

// Clase para la estrategia de pago con PayPal.
public class PagoPaypal : IEstrategiaPago
{
    public void Pagar(decimal monto)
    {
        Console.WriteLine($"Pagando {monto:C} con PayPal.");
    }
}

// Esta clase utiliza la estrategia de pago.
public class ProcesadorPagos
{
    private readonly IEstrategiaPago _estrategiaPago;

    public ProcesadorPagos(IEstrategiaPago estrategiaPago)
    {
        _estrategiaPago = estrategiaPago;
    }

    public void Procesar(decimal monto)
    {
        _estrategiaPago.Pagar(monto);
    }
}

// Clase principal que contiene el punto de entrada del programa.
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Ejemplo de Pago con Tarjeta ---");
        // Creamos un procesador de pagos y le pasamos la estrategia de Pago con Tarjeta.
        ProcesadorPagos procesadorTarjeta = new ProcesadorPagos(new PagoTarjeta());
        procesadorTarjeta.Procesar(150.75m); // El sufijo 'm' indica que es un tipo decimal.

        Console.WriteLine("\n--- Ejemplo de Pago con PayPal ---");
        // Cambiamos de estrategia de pago sin modificar la clase ProcesadorPagos.
        ProcesadorPagos procesadorPaypal = new ProcesadorPagos(new PagoPaypal());
        procesadorPaypal.Procesar(200.50m);
    }
}
```
<img width="761" height="596" alt="image" src="https://github.com/user-attachments/assets/73204b17-bcac-43eb-b395-2a100fc6e9b0" />

---
### 📊 UML del Refactor
<img width="1067" height="526" alt="image" src="https://github.com/user-attachments/assets/4b2adc30-a957-40d4-aec8-b70396b5d798" />

---
### 📝Reflexión

Esta práctica demuestra que los patrones de diseño no son recetas que se aplican sin pensar. El patrón Singleton tiene su lugar, pero raramente es para manejar estados mutables y compartidos globalmente. La lección principal aquí es: el Singleton es un antipatrón cuando se usa como un contenedor global de datos.

Al aplicar Factory, Repository y Strategy, tu código ahora es:

- Más Cohesivo: Cada clase tiene una única responsabilidad bien definida (crear carritos, gestionar el inventario, procesar pagos).
- Menos Acoplado: Los componentes (ProcesadorPagos, por ejemplo) no están rígidamente atados a una implementación específica, sino a una interfaz (IEstrategiaPago).
- Más Testeable: Puedes probar cada componente de forma aislada, inyectando "mocks" o versiones de prueba de sus dependencias, sin la complejidad de un estado global.

En resumen, pasaste de una arquitectura frágil a una arquitectura sólida y escalable, lista para manejar los desafíos de una aplicación real con múltiples usuarios y operaciones concurrentes. Es un claro ejemplo de cómo la refactorización con patrones de diseño puede transformar un "código que funciona" en un "código bien diseñado".
