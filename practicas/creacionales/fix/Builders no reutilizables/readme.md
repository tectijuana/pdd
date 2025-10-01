# 📖 Refactorización de Vehículos con Patrones GoF

## 📝 Formato del Pull Request

### 🔍 Problemas detectados
- El patrón *Builder* se implementó de forma incorrecta: el mismo objeto VehiculoBuilder se reutiliza para crear múltiples vehículos, lo que ocasiona que los atributos se mezclen entre instancias.  
- La falta de un reinicio del estado interno del Builder provoca que configuraciones anteriores persistan en los nuevos vehículos creados.  
- El VehiculoManager depende directamente de un Builder no reutilizable, dificultando la extensión y reduciendo la confiabilidad de la construcción de objetos.  

### 🛠 Patrones aplicados
- *Builder seguro y reutilizable* → cada construcción inicia desde un objeto limpio, evitando fugas de estado.  
- *Director opcional* → centraliza la lógica de construcción de configuraciones comunes de vehículos (ejemplo: auto estándar, moto deportiva).  
- Se refactorizó para que cada Builder devuelva un nuevo objeto y reinicie su estado interno tras la construcción.  

### 💡 Justificación del cambio
Con este refactor:  
- *Confiabilidad:* se evitan estados compartidos accidentales entre diferentes vehículos.  
- *Cohesión:* cada Builder mantiene una responsabilidad clara: construir vehículos paso a paso sin mezclar configuraciones.  
- *Extensibilidad:* es posible crear nuevos Builders especializados (ejemplo: CamionBuilder) sin afectar los existentes.  
- *Mantenibilidad:* el uso de un Director permite encapsular configuraciones frecuentes y reducir duplicidad de código.  

### 🔄 Impacto
- Se eliminan riesgos de inconsistencias en objetos al garantizar que cada Build() produce un vehículo limpio.  
- El sistema queda preparado para *pruebas unitarias*, verificando la correcta creación de vehículos en diferentes escenarios.  
- La arquitectura es más robusta y flexible, con separación clara entre el proceso de construcción y la representación final.  

### 📌 Próximos pasos sugeridos
- Añadir más Builders para distintos tipos de vehículos (ejemplo: camiones, bicicletas eléctricas).  
- Integrar el patrón *Prototype* para clonar vehículos base a partir de configuraciones creadas con Builder.  
- Usar validaciones dentro de los Builders para asegurar que los vehículos no queden con configuraciones incompletas.  

---

## 💻 Código de ejemplo

### 🚨 Código con malas prácticas (Builder no reutilizable)
```csharp
using System;

namespace VehiculosApp
{
    // Clase Vehículo
    public class Vehiculo
    {
        public string Color { get; set; }
        public string Motor { get; set; }
        public int Ruedas { get; set; }

        public void MostrarInfo()
        {
            Console.WriteLine($"Vehículo -> Color: {Color}, Motor: {Motor}, Ruedas: {Ruedas}");
        }
    }

    // 🚨 Builder que reutiliza el mismo objeto
    public class VehiculoBuilder
    {
        private Vehiculo vehiculo = new Vehiculo();

        public VehiculoBuilder SetColor(string color)
        {
            vehiculo.Color = color;
            return this;
        }

        public VehiculoBuilder SetMotor(string motor)
        {
            vehiculo.Motor = motor;
            return this;
        }

        public VehiculoBuilder SetRuedas(int ruedas)
        {
            vehiculo.Ruedas = ruedas;
            return this;
        }

        public Vehiculo Build()
        {
            // 🚨 Retorna siempre la misma instancia → datos mezclados
            return vehiculo;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new VehiculoBuilder();

            var auto = builder.SetColor("Rojo").SetMotor("1.6L").SetRuedas(4).Build();
            var moto = builder.SetColor("Negro").SetMotor("600cc").SetRuedas(2).Build();

            // 🚨 Problema: auto y moto comparten estado interno
            auto.MostrarInfo();
            moto.MostrarInfo();
        }
    }
}
using System;

namespace VehiculosApp
{
    // Clase Vehículo
    public class Vehiculo
    {
        public string Color { get; set; }
        public string Motor { get; set; }
        public int Ruedas { get; set; }

        public void MostrarInfo()
        {
            Console.WriteLine($"Vehículo -> Color: {Color}, Motor: {Motor}, Ruedas: {Ruedas}");
        }
    }

    // ✅ Builder seguro: reinicia estado tras cada construcción
    public class VehiculoBuilder
    {
        private Vehiculo vehiculo;

        public VehiculoBuilder()
        {
            Reset();
        }

        private void Reset()
        {
            vehiculo = new Vehiculo();
        }

        public VehiculoBuilder SetColor(string color)
        {
            vehiculo.Color = color;
            return this;
        }

        public VehiculoBuilder SetMotor(string motor)
        {
            vehiculo.Motor = motor;
            return this;
        }

        public VehiculoBuilder SetRuedas(int ruedas)
        {
            vehiculo.Ruedas = ruedas;
            return this;
        }

        public Vehiculo Build()
        {
            Vehiculo resultado = vehiculo;
            Reset(); // 🔑 Reinicia para permitir nuevas construcciones limpias
            return resultado;
        }
    }

    // Director opcional: define construcciones predefinidas
    public class VehiculoDirector
    {
        public Vehiculo ConstruirAutoEstandar(VehiculoBuilder builder)
        {
            return builder.SetColor("Azul").SetMotor("2.0L").SetRuedas(4).Build();
        }

        public Vehiculo ConstruirMotoDeportiva(VehiculoBuilder builder)
        {
            return builder.SetColor("Rojo").SetMotor("1000cc").SetRuedas(2).Build();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new VehiculoBuilder();
            var director = new VehiculoDirector();

            // ✅ Vehículos construidos de manera independiente
            var auto = director.ConstruirAutoEstandar(builder);
            var moto = director.ConstruirMotoDeportiva(builder);

            auto.MostrarInfo();
            moto.MostrarInfo();
        }
    }
}
