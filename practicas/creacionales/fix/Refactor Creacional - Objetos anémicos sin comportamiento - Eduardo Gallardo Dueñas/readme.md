## Objetos anémicos sin comportamiento
Eduardo Gallardo Dueñas 21212215 - 24/09/25

# Bad Code (Java)

``` java
// Clase anémica: solo datos, sin lógica
public class Vehiculo {
    private String marca;
    private String modelo;
    private int anio;

    public Vehiculo(String marca, String modelo, int anio) {
        this.marca = marca;
        this.modelo = modelo;
        this.anio = anio;
    }

    // Getters y setters, sin ningún comportamiento
    public String getMarca() { return marca; }
    public void setMarca(String marca) { this.marca = marca; }

    public String getModelo() { return modelo; }
    public void setModelo(String modelo) { this.modelo = modelo; }

    public int getAnio() { return anio; }
    public void setAnio(int anio) { this.anio = anio; }
}

// Factory que crea objetos pero deja lógica fuera
public class VehiculoFactory {
    public Vehiculo crearVehiculo(String marca, String modelo, int anio) {
        return new Vehiculo(marca, modelo, anio);
    }
}

// Uso del objeto anémico
public class Main {
    public static void main(String[] args) {
        VehiculoFactory factory = new VehiculoFactory();
        Vehiculo carro = factory.crearVehiculo("Toyota", "Corolla", 1995);

        // Lógica que debería estar en Vehiculo, pero está en el cliente
        int edad = 2025 - carro.getAnio();
        if (edad > 25) {
            System.out.println("El vehículo es clásico.");
        } else {
            System.out.println("El vehículo es moderno.");
        }
    }
}

```

# Problemas Detectados
1. Vehiculo es un objeto anémico → solo tiene atributos y getters/setters.

2. La lógica (es clásico o no) está en el cliente, no en el modelo.

3. La Factory solo devuelve new Vehiculo(...), sin encapsular reglas de creación.dos (ej. anio negativo o marca vacía) porque la lógica de validación estaba en otra parte del código en vez de encapsularse dentro del objeto.

# Código Corregido
``` java
// Clase con comportamiento
public class Vehiculo {
    private final String marca;
    private final String modelo;
    private final int anio;

    public Vehiculo(String marca, String modelo, int anio) {
        if (marca == null || modelo == null) {
            throw new IllegalArgumentException("Marca y modelo son obligatorios.");
        }
        if (anio <= 0) {
            throw new IllegalArgumentException("El año debe ser positivo.");
        }
        this.marca = marca;
        this.modelo = modelo;
        this.anio = anio;
    }

    public String getMarca() { return marca; }
    public String getModelo() { return modelo; }
    public int getAnio() { return anio; }

    // 👇 Comportamiento encapsulado
    public boolean esClasico() {
        return (2025 - anio) > 25;
    }

    public String descripcionDetallada() {
        return marca + " " + modelo + " (" + anio + ")";
    }
}

// Factory mejorada que usa la validación del constructor
public class VehiculoFactory {
    public Vehiculo crearVehiculo(String marca, String modelo, int anio) {
        return new Vehiculo(marca, modelo, anio);
    }
}

// Uso después del refactor
public class Main {
    public static void main(String[] args) {
        VehiculoFactory factory = new VehiculoFactory();
        Vehiculo carro = factory.crearVehiculo("Toyota", "Corolla", 1995);

        // 👇 Ahora la lógica está dentro del objeto
        if (carro.esClasico()) {
            System.out.println(carro.descripcionDetallada() + " es clásico.");
        } else {
            System.out.println(carro.descripcionDetallada() + " es moderno.");
        }
    }
}
```

# Mejoras después del refactor:

1. Vehiculo ya no es anémico → contiene su propia lógica (esClasico(), descripcionDetallada()).

2. Validaciones se realizan en el constructor → los objetos siempre nacen en estado válido.

3. El cliente (Main) no se encarga de lógica → aplica el principio de “Tell, don’t ask”.

4. La Factory sigue existiendo, pero delega la creación con validaciones seguras.
