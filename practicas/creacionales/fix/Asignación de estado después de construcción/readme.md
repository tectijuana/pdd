# Antipatrón: Asignación de estado después de construcción (Builder) — Dominio: Vehículo
Jesus Antonio Triana Corvera - C20212681
---

## 1. Identificación de problemas 

### Situación
- `VehicleBuilder.Build()` retorna una instancia **incompleta** (`Vehicle`) y el cliente completa propiedades críticas *después* (motor, transmisión, frenos).  
- Entre `Build()` y las asignaciones tardías existe una **ventana de inconsistencia** donde no se cumplen invariantes (p. ej., un vehículo sin sistema de frenos).

### Problemas detectados
- **Inmutabilidad rota**: `Vehicle` expone setters públicos para atributos esenciales.  
- **Validación dispersa**: no hay control centralizado en `Build()`; errores aparecen en tiempo de uso.  
- **Acoplamiento al orden**: la lógica depende del *sequence* de setters post-`Build()`.  
- **Fragilidad en tests**: setups largos con riesgo de omitir propiedades obligatorias.  

**Ejemplo (antipatrón):**
```csharp
public class Vehicle
{
    public string Model { get; set; }
    public Engine Engine { get; set; }
    public Transmission Transmission { get; set; }
    public BrakeSystem BrakeSystem { get; set; }
}

public class VehicleBuilder
{
    private string _model;

    public VehicleBuilder WithModel(string model) { _model = model; return this; }

    // Mala práctica: retorna vehículo incompleto
    public Vehicle Build() => new Vehicle { Model = _model };
}

// Uso → antipatrón
var v = new VehicleBuilder()
    .WithModel("Sedan-X")
    .Build();

// Asignaciones tardías (estado crítico; orden importa)
v.Engine = new GasEngine(180);
v.Transmission = new Automatic6();
v.BrakeSystem = new AbsBrakes();
```

**Efectos:** `v` puede “escaparse” a otros componentes **antes** de tener frenos o transmisión, generando fallos intermitentes o NPEs.

---

## 2. Aplicación correcta del patrón 

### Solución aplicada
- Consolidar **todos los requisitos obligatorios** en el Builder.  
- `Build()` **valida** invariantes y entrega un objeto **consistente**.  
- Propiedades críticas solo lectura (`get`) o `init`; setters públicos eliminados.  

**Refactor (Builder correcto):**
```csharp
public class Vehicle
{
    public string Model { get; }
    public Engine Engine { get; }
    public Transmission Transmission { get; }
    public BrakeSystem BrakeSystem { get; }

    internal Vehicle(string model, Engine engine, Transmission transmission, BrakeSystem brakes)
    {
        Model = model;
        Engine = engine;
        Transmission = transmission;
        BrakeSystem = brakes;
    }
}

public class VehicleBuilder
{
    private string _model;
    private Engine _engine;
    private Transmission _transmission;
    private BrakeSystem _brakes;

    public VehicleBuilder WithModel(string model) { _model = model; return this; }
    public VehicleBuilder WithEngine(Engine engine) { _engine = engine; return this; }
    public VehicleBuilder WithTransmission(Transmission transmission) { _transmission = transmission; return this; }
    public VehicleBuilder WithBrakes(BrakeSystem brakes) { _brakes = brakes; return this; }

    public Vehicle Build()
    {
        if (string.IsNullOrWhiteSpace(_model)) throw new InvalidOperationException("Model requerido");
        if (_engine is null) throw new InvalidOperationException("Engine requerido");
        if (_transmission is null) throw new InvalidOperationException("Transmission requerida");
        if (_brakes is null) throw new InvalidOperationException("BrakeSystem requerido");

        return new Vehicle(_model, _engine, _transmission, _brakes);
    }
}
```

### Variante (si hay condicionamiento por tipo de motor)
Para encapsular la creación de motores/transmisiones dependientes del *trim* o combustible, utilizar **Factory Method/Abstract Factory** y pasar la factory al Builder, evitando asignación tardía y *switches* externos.

```csharp
public abstract class DrivetrainFactory
{
    public abstract Engine CreateEngine();
    public abstract Transmission CreateTransmission();
}

public class HybridDrivetrainFactory : DrivetrainFactory
{
    public override Engine CreateEngine() => new HybridEngine(110, 60); // ICE + e-motor
    public override Transmission CreateTransmission() => new ECVT();
}

public class VehicleBuilderV2
{
    private string _model;
    private DrivetrainFactory _drivetrainFactory;
    private BrakeSystem _brakes;

    public VehicleBuilderV2 WithModel(string model) { _model = model; return this; }
    public VehicleBuilderV2 WithDrivetrain(DrivetrainFactory factory) { _drivetrainFactory = factory; return this; }
    public VehicleBuilderV2 WithBrakes(BrakeSystem brakes) { _brakes = brakes; return this; }

    public Vehicle Build()
    {
        if (string.IsNullOrWhiteSpace(_model)) throw new InvalidOperationException("Model requerido");
        if (_drivetrainFactory is null) throw new InvalidOperationException("DrivetrainFactory requerida");
        if (_brakes is null) throw new InvalidOperationException("BrakeSystem requerido");

        var engine = _drivetrainFactory.CreateEngine();
        var transmission = _drivetrainFactory.CreateTransmission();
        return new Vehicle(_model, engine, transmission, _brakes);
    }
}
```

---

## 3. Justificación técnica

- **Consistencia por construcción**: no existe ventana de uso en estado inválido; invariantes se aplican en `Build()`.  
- **Encapsulamiento**: la lógica de ensamblaje (motor/transmisión/frenos) está centralizada; el cliente no coordina detalles.  
- **SRP**: `VehicleBuilder` orquesta construcción; `Vehicle` solo modela estado válido.  
- **OCP**: con *Factory Method/Abstract Factory* se agregan nuevos trenes motrices sin modificar el cliente.  
- **Testabilidad**: pruebas se simplifican (un único punto de validación; *arrange* corto).  
- **Menor acoplamiento temporal**: el orden de llamadas post-`Build()` deja de importar.  

---

**Conclusión**  
El antipatrón proviene de exponer instancias incompletas. El Builder debe **cerrar la construcción** y **validar invariantes**. Cuando existan decisiones de creación dependientes de variantes (híbrido/EV/ICE), introducir una **fábrica** para mantener OCP y evitar asignaciones tardías.
