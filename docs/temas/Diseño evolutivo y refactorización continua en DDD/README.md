# **Diseño evolutivo y refactorización continua en DDD**
*Eduardo Gallardo Dueñas 21212215* - 18/09/2025


## Diseño evolutivo en DDD
El diseño evolutivo parte de que el modelo de software no es estático, sino que debe adaptarse conforme cambian los requisitos del negocio. En DDD esto se enfatiza: se considera un proceso de diseño evolutivo que 
requiere iteración continua (DDD-crew, 2021). Dicho de otro modo, el modelo de dominio se entiende como vivo, porque evoluciona con el negocio (Granado, 2023). En la práctica, esto implica no fijar el modelo de una 
vez por todas; al contrario, se construye una versión inicial simplificada y se refina continuamente con la retroalimentación de expertos en el dominio (Zeljko, 2022).

## Refactorización continua en DDD
La refactorización continua es la reestructuración incremental del código sin cambiar su funcionalidad externa, con el fin de mejorar su diseño interno. En DDD es clave para mantener el modelo alineado con el negocio.
Esta práctica permite mejorar grandes bases de código paso a paso (Khosravi, 2019). Además, cuando se detecta que el modelo no refleja bien la realidad, es recomendable corregirlo lo antes posible para evitar que el 
equipo quede atrapado en una visión obsoleta del dominio (Granado, 2023).

En la práctica, se manifiesta en acciones como:

- mover operaciones de servicios a entidades,

- crear objetos de valor en lugar de tipos primitivos,

- dividir o agrupar agregados según se descubren nuevas reglas.

## Interacción con patrones de diseño en DDD
Estas prácticas se materializan mediante los patrones tácticos de DDD (Evans, 2003):

- Entidades → con identidad propia (ej. Pedido).

- Objetos de Valor → inmutables sin identidad (ej. Dirección).

- Agregados → clústers de entidades/valores con invariantes (ej. Pedido con sus Ítems).

- Repositorios → uno por agregado, permiten guardar y recuperar agregados enteros (Evans, 2003).

- Fábricas → encapsulan la creación de objetos complejos (Evans, 2003).

- Servicios de Dominio → lógica de negocio que no pertenece a una sola entidad.

Durante la evolución, estos patrones aparecen o se reorganizan de forma natural: se introducen nuevos agregados, se extraen objetos de valor, se crean servicios o se simplifican repositorios según lo dicten 
los cambios en el dominio.

## Ejemplos conceptuales
1. Sistema de facturación con tarifas

  -  Inicial: dos clases TarifaFija y TarifaPorHora.

  - Evolución: se descubre duplicación → refactorización a una sola entidad Tarifa con atributo tipo.

2. Gestión de pedidos en una tienda

  - Inicial: Pedido con lista de ítems y total.

  - Evolución: cada ítem requiere identidad y consistencia → Pedido se convierte en Agregado, con PedidoRepository y PedidoFactory.

3. Dirección de envío

  - Inicial: atributo String en Pedido.

  - Evolución: necesidad de validaciones → se convierte en un Objeto de Valor Direccion.

## Referencias

1. DDD-crew. (2021). DDD Starter Modeling Process. Recuperado de https://github.com/ddd-crew/ddd-starter-modelling-process

2. Evans, E. (2003). Domain-Driven Design: Tackling Complexity in the Heart of Software. Boston: Addison-Wesley. Recuperado de
  https://fabiofumarola.github.io/nosql/readingMaterial/Evans03.pdf

4. Granado, G. (2023). Domain Driven Design: Teoría, conceptos y práctica. Medium. Recuperado de https://medium.com/

5. Khosravi, H. (2019). The Value of Continuous Refactoring. ThoughtWorks. Recuperado de https://thoughtworks.com/

6. Zeljko, M. (2022). Domain-Driven Design Principles. Hackernoon. Recuperado de https://hackernoon.com/

# Ejemplos
Con estos ejemplos puedes ver cómo el diseño evoluciona y cómo la refactorización continua permite aplicar patrones tácticos de DDD.
## Ejemplo 1: Evolución de tarifas
Versión inicial (duplicación de clases)

``` cs
public class TarifaFija
{
    public decimal Monto { get; set; }
    public decimal CalcularCosto(int horas)
    {
        return Monto;
    }
}

public class TarifaPorHora
{
    public decimal PrecioHora { get; set; }

    public decimal CalcularCosto(int horas)
    {
        return PrecioHora * horas;
    }
}
```

Refactorización continua (unificar en una sola entidad)
``` cs
public enum TipoTarifa
{
    Fija,
    PorHora
}

public class Tarifa
{
    public TipoTarifa Tipo { get; private set; }
    public decimal Valor { get; private set; }

    public Tarifa(TipoTarifa tipo, decimal valor)
    {
        Tipo = tipo;
        Valor = valor;
    }

    public decimal CalcularCosto(int horas)
    {
        return Tipo == TipoTarifa.Fija ? Valor : Valor * horas;
    }
}
```
Aplicamos diseño evolutivo: pasamos de dos clases duplicadas a una entidad más expresiva, eliminando redundancia.

## Ejemplo 2: Pedido como agregado
Versión inicial (muy simple)
``` cs
public class Pedido
{
    public List<string> Productos { get; set; } = new List<string>();
    public decimal Total { get; set; }
}
```
Evolución con DDD (Agregado + Repositorio + Fábrica)
``` cs
// Entidad Item
public class Item
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    public decimal Precio { get; private set; }

    public Item(string nombre, decimal precio)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        Precio = precio;
    }
}

// Agregado Pedido
public class Pedido
{
    private List<Item> _items = new List<Item>();
    public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

    public decimal Total => _items.Sum(i => i.Precio);

    public void AgregarItem(Item item)
    {
        _items.Add(item);
    }
}

// Fábrica de Pedido
public static class PedidoFactory
{
    public static Pedido CrearConItemInicial(string nombre, decimal precio)
    {
        var pedido = new Pedido();
        pedido.AgregarItem(new Item(nombre, precio));
        return pedido;
    }
}

// Repositorio (interfaz)
public interface IPedidoRepository
{
    void Guardar(Pedido pedido);
    Pedido ObtenerPorId(Guid id);
}
```

Aquí aplicamos refactorización continua: el Pedido pasó de ser una clase plana a un Agregado con reglas claras, usando Entidades (Item), Fábrica y Repositorio.
