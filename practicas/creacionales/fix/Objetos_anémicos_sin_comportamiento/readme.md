## Objetos anémicos sin comportamiento
Eduardo Gallardo Dueñas 21212215 - 24/09/25

# Bad Code (C#)

```csharp
// Clase anémica: solo datos, sin lógica
public class Vehiculo
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int Año { get; set; }

    public Vehiculo(string marca, string modelo, int año)
    {
        Marca = marca;
        Modelo = modelo;
        Año = año;
    }
}

// Factory que crea objetos pero deja lógica fuera
public class VehiculoFactory
{
    public Vehiculo CrearVehiculo(string marca, string modelo, int año)
    {
        return new Vehiculo(marca, modelo, año);
    }
}

// Uso del objeto anémico
public class Program
{
    public static void Main()
    {
        VehiculoFactory factory = new VehiculoFactory();
        Vehiculo carro = factory.CrearVehiculo("Toyota", "Corolla", 1995);

        // Lógica que debería estar en Vehiculo, pero está en el cliente
        int edad = 2025 - carro.Año;
        if (edad > 25)
        {
            Console.WriteLine("El vehículo es clásico.");
        }
        else
        {
            Console.WriteLine("El vehículo es moderno.");
        }
    }
}
````

# Problemas Detectados

1. Vehiculo es un objeto anémico → solo tiene atributos y getters/setters.

2. La lógica (es clásico o no) está en el cliente, no en el modelo.

3. La Factory solo devuelve `new Vehiculo(...)`, sin encapsular reglas de creación ni validación.

---

# Código Corregido (C#)

```csharp
// Clase con comportamiento
public class Vehiculo
{
    public string Marca { get; }
    public string Modelo { get; }
    public int Año { get; }

    public Vehiculo(string marca, string modelo, int año)
    {
        if (string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo))
        {
            throw new ArgumentException("Marca y modelo son obligatorios.");
        }
        if (año <= 0)
        {
            throw new ArgumentException("El año debe ser positivo.");
        }

        Marca = marca;
        Modelo = modelo;
        Año = año;
    }

    // 👇 Comportamiento encapsulado
    public bool EsClasico()
    {
        return (2025 - Año) > 25;
    }

    public string DescripcionDetallada()
    {
        return $"{Marca} {Modelo} ({Año})";
    }
}

// Factory mejorada que usa la validación del constructor
public class VehiculoFactory
{
    public Vehiculo CrearVehiculo(string marca, string modelo, int año)
    {
        return new Vehiculo(marca, modelo, año);
    }
}

// Uso después del refactor
public class Program
{
    public static void Main()
    {
        VehiculoFactory factory = new VehiculoFactory();
        Vehiculo carro = factory.CrearVehiculo("Toyota", "Corolla", 1995);

        // 👇 Ahora la lógica está dentro del objeto
        if (carro.EsClasico())
        {
            Console.WriteLine($"{carro.DescripcionDetallada()} es clásico.");
        }
        else
        {
            Console.WriteLine($"{carro.DescripcionDetallada()} es moderno.");
        }
    }
}
```

# Mejoras después del refactor:

1. Vehiculo ya no es anémico → contiene su propia lógica (`EsClasico()`, `DescripcionDetallada()`).

2. Validaciones se realizan en el constructor → los objetos siempre nacen en estado válido.

3. El cliente (`Program`) no se encarga de lógica → aplica el principio de **“Tell, don’t ask”**.

4. La Factory sigue existiendo, pero delega la creación con validaciones seguras.

