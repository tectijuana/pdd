# Clases Dios (God Objects)

## Alvarado Cardona Antonio 22210279
## 1. Clase Dios con Acoplamiento excesivo
// Esta clase mezcla lógica de negocio, persistencia y presentación.
// Es un "God Object" típico que hace de todo: CRUD, imprimir, conectar a BD, etc.
``` csharp
using System;
using System.Collections.Generic;

namespace EjemploGodObject
{
    public class SistemaVinos // <- CLASE DIOS
    {
        private List<string> vinos = new List<string>();

        // Lógica de negocio + persistencia
        public void AgregarVino(string nombre)
        {
            vinos.Add(nombre);
            GuardarEnBaseDeDatos(nombre); // Acoplamiento con infraestructura
        }

        // Presentación mezclada con negocio
        public void MostrarVinos()
        {
            Console.WriteLine("Lista de vinos:");
            foreach (var v in vinos)
            {
                Console.WriteLine($"- {v}");
            }
        }

        // Infraestructura mezclada con negocio
        private void GuardarEnBaseDeDatos(string nombre)
        {
            Console.WriteLine($"[DEBUG] Guardando {nombre} en la BD...");
            // Aquí iría código real de conexión
        }
    }
}
```

👉 Hints para refactorizar:

Separar en: RepositorioVinos (persistencia), CatalogoVinos (negocio), ConsolaUI (presentación).

Posible uso de Facade para unificar interfaces.

Aplicar Strategy si se necesita soportar múltiples formas de guardar (archivo, BD, API).

## Refactorización (SRP + Facade + Strategy)
``` csharp
using System;
using System.Collections.Generic;

// --- Capa de persistencia ---
public interface IRepositorioVinos
{
    void Guardar(string nombre);
}

public class RepositorioBD : IRepositorioVinos
{
    public void Guardar(string nombre)
    {
        Console.WriteLine($"[BD] Guardando {nombre} en la base de datos...");
    }
}

public class RepositorioArchivo : IRepositorioVinos
{
    public void Guardar(string nombre)
    {
        Console.WriteLine($"[Archivo] Guardando {nombre} en archivo...");
    }
}

// --- Capa de negocio ---
public class CatalogoVinos
{
    private readonly IRepositorioVinos _repositorio;
    private readonly List<string> _vinos = new();

    public CatalogoVinos(IRepositorioVinos repositorio)
    {
        _repositorio = repositorio;
    }

    public void AgregarVino(string nombre)
    {
        _vinos.Add(nombre);
        _repositorio.Guardar(nombre);
    }

    public IEnumerable<string> ObtenerVinos() => _vinos;
}

// --- Capa de presentación ---
public class ConsolaUI
{
    public void MostrarVinos(IEnumerable<string> vinos)
    {
        Console.WriteLine("Lista de vinos:");
        foreach (var v in vinos)
        {
            Console.WriteLine($"- {v}");
        }
    }
}

// --- Facade ---
public class SistemaVinosFacade
{
    private readonly CatalogoVinos _catalogo;
    private readonly ConsolaUI _ui;

    public SistemaVinosFacade(CatalogoVinos catalogo, ConsolaUI ui)
    {
        _catalogo = catalogo;
        _ui = ui;
    }

    public void AgregarYMostrar(string nombre)
    {
        _catalogo.AgregarVino(nombre);
        _ui.MostrarVinos(_catalogo.ObtenerVinos());
    }
}
```
## 2. Clase Dios que impide Testing y mantenibilidad
// Clase con demasiadas responsabilidades internas
// Difícil de testear porque todo está acoplado, sin inyección de dependencias.
``` csharp
using System;

namespace EjemploGodObject
{
    public class ProcesadorPedidos // <- CLASE DIOS
    {
        public void ProcesarPedido(string cliente, string producto, int cantidad)
        {
            Console.WriteLine($"Procesando pedido: {producto} x{cantidad} para {cliente}");

            // Cálculo de impuestos (debería estar separado)
            var impuestos = cantidad * 0.15;
            Console.WriteLine($"Impuestos calculados: {impuestos}");

            // Lógica de envío mezclada
            Console.WriteLine($"Generando envío para {cliente}...");

            // Facturación mezclada
            Console.WriteLine($"Factura generada para {producto}");
        }
    }
}
```

👉 Hints para refactorizar:

Separar en CalculadorImpuestos, ServicioEnvio, ServicioFacturacion.

Posible uso de Command para encapsular cada paso como acción independiente.

Usar Dependency Injection para mejorar testabilidad.

## Refactorización (Command + DI + SRP)
``` csharp
using System;

// --- Servicios ---
public interface ICalculadorImpuestos
{
    double Calcular(int cantidad);
}

public class CalculadorIVA : ICalculadorImpuestos
{
    public double Calcular(int cantidad) => cantidad * 0.15;
}

public interface IServicioEnvio
{
    void GenerarEnvio(string cliente);
}

public class ServicioEnvio : IServicioEnvio
{
    public void GenerarEnvio(string cliente) => 
        Console.WriteLine($"[Envío] Generando envío para {cliente}");
}

public interface IServicioFacturacion
{
    void GenerarFactura(string cliente, string producto);
}

public class ServicioFacturacion : IServicioFacturacion
{
    public void GenerarFactura(string cliente, string producto) =>
        Console.WriteLine($"[Factura] Generada para {producto} a nombre de {cliente}");
}

// --- Patrón Command ---
public interface ICommand
{
    void Ejecutar();
}

public class ProcesarPedidoCommand : ICommand
{
    private readonly string _cliente;
    private readonly string _producto;
    private readonly int _cantidad;
    private readonly ICalculadorImpuestos _impuestos;
    private readonly IServicioEnvio _envio;
    private readonly IServicioFacturacion _facturacion;

    public ProcesarPedidoCommand(string cliente, string producto, int cantidad,
        ICalculadorImpuestos impuestos, IServicioEnvio envio, IServicioFacturacion facturacion)
    {
        _cliente = cliente;
        _producto = producto;
        _cantidad = cantidad;
        _impuestos = impuestos;
        _envio = envio;
        _facturacion = facturacion;
    }

    public void Ejecutar()
    {
        Console.WriteLine($"Procesando pedido: {_producto} x{_cantidad} para {_cliente}");
        var impuestos = _impuestos.Calcular(_cantidad);
        Console.WriteLine($"Impuestos: {impuestos}");
        _envio.GenerarEnvio(_cliente);
        _facturacion.GenerarFactura(_cliente, _producto);
    }
}
```
## 3. Clase Dios que viola el principio de Extensibilidad (OCP)
// Esta clase crece de forma incontrolada cada vez que se agregan nuevos productos.
// Cada nueva opción implica modificar el switch/código existente.
``` csharp
using System;

namespace EjemploGodObject
{
    public class FabricaProductos // <- CLASE DIOS
    {
        public object CrearProducto(string tipo)
        {
            switch (tipo)
            {
                case "vino":
                    return new { Nombre = "Vino Tinto", Precio = 10.0 };
                case "queso":
                    return new { Nombre = "Queso Manchego", Precio = 5.0 };
                case "jamon":
                    return new { Nombre = "Jamón Ibérico", Precio = 12.0 };
                default:
                    throw new Exception("Tipo de producto no soportado");
            }
        }
    }
}
```

👉 Hints para refactorizar:

Aquí encaja un Factory Method o un Abstract Factory.

También podrías aplicar Polimorfismo para eliminar el switch.

Extender sería mucho más fácil: solo agregas una clase nueva sin modificar esta.

## Refactorización (Factory Method + Polimorfismo)
``` csharp
using System;

// --- Producto base ---
public abstract class Producto
{
    public string Nombre { get; protected set; }
    public double Precio { get; protected set; }

    public override string ToString() => $"{Nombre} - ${Precio}";
}

// --- Productos concretos ---
public class Vino : Producto
{
    public Vino()
    {
        Nombre = "Vino Tinto";
        Precio = 10.0;
    }
}

public class Queso : Producto
{
    public Queso()
    {
        Nombre = "Queso Manchego";
        Precio = 5.0;
    }
}

public class Jamon : Producto
{
    public Jamon()
    {
        Nombre = "Jamón Ibérico";
        Precio = 12.0;
    }
}

// --- Factory Method ---
public abstract class FabricaProductos
{
    public abstract Producto CrearProducto();
}

public class FabricaVino : FabricaProductos
{
    public override Producto CrearProducto() => new Vino();
}

public class FabricaQueso : FabricaProductos
{
    public override Producto CrearProducto() => new Queso();
}

public class FabricaJamon : FabricaProductos
{
    public override Producto CrearProducto() => new Jamon();
}
```
