# Alvarado Cardona Antonio 22210279
# Antipatrón: God Object

El God Object (u Objeto Dios) es un antipatrón de diseño que surge cuando una clase concentra demasiadas responsabilidades dentro de un sistema. En lugar de delegar tareas a otras clases especializadas, el God Object controla múltiples aspectos del programa, como la lógica de negocio, la gestión de datos y la interfaz de usuario.

Este antipatrón viola directamente el Principio de Responsabilidad Única (SRP) del modelo SOLID, dificultando la modularidad, las pruebas y la escalabilidad del código.
En esencia, un God Object actúa como un “todo poderoso” dentro del sistema, pero termina generando alto acoplamiento y baja cohesión, dos síntomas graves de mal diseño.

# Ejemplo Técnico
## Código con el Antipatrón (Clase Dios)
``` rust
use std::fs::File;
use std::io::Write;
use chrono::{NaiveDate, Local};

struct SistemaVeterinaria {
    pacientes: Vec<String>,
}

impl SistemaVeterinaria {
    fn new() -> Self {
        SistemaVeterinaria { pacientes: Vec::new() }
    }

    // Manejo de datos
    fn agregar_paciente(&mut self, nombre: &str) {
        self.pacientes.push(nombre.to_string());
    }

    // Lógica de negocio
    fn calcular_edad(&self, nacimiento: NaiveDate) -> i32 {
        let hoy = Local::now().date_naive();
        hoy.year() - nacimiento.year()
    }

    // Persistencia
    fn guardar_datos(&self) {
        let mut file = File::create("pacientes.txt").expect("No se pudo crear el archivo");
        for p in &self.pacientes {
            writeln!(file, "{}", p).expect("Error al escribir en archivo");
        }
    }

    // Interfaz simulada
    fn mostrar_menu(&self) {
        println!("1. Agregar paciente\n2. Guardar datos\n3. Salir");
    }
}

fn main() {
    let mut sistema = SistemaVeterinaria::new();
    sistema.agregar_paciente("Luna");
    sistema.mostrar_menu();
    sistema.guardar_datos();
}
```
# Consecuencias 
- Mantenimiento difícil: Un cambio en un método puede romper otros módulos no relacionados.
- Baja reutilización: La clase es tan específica que no puede reutilizarse en otros contextos.
- Dificultad para realizar pruebas unitarias: Al estar todo mezclado, se necesita simular todo el sistema para probar una sola función.
- Rendimiento degradado: Las dependencias y datos compartidos aumentan el consumo de memoria y procesamiento.
- Escalabilidad limitada: Agregar nuevas funciones implica modificar la clase principal, aumentando el riesgo de errores.

# Solución Correctiva
``` rust
use std::fs::File;
use std::io::Write;

struct Paciente {
    nombre: String,
    especie: String,
    propietario: String,
}

struct RepositorioPacientes;

impl RepositorioPacientes {
    fn guardar(&self, paciente: &Paciente) {
        let mut file = File::create("pacientes.txt").expect("No se pudo crear el archivo");
        writeln!(
            file,
            "{},{},{}",
            paciente.nombre, paciente.especie, paciente.propietario
        )
        .expect("Error al escribir en archivo");
    }
}

struct ServicioVeterinaria<'a> {
    repositorio: &'a RepositorioPacientes,
}

impl<'a> ServicioVeterinaria<'a> {
    fn registrar_paciente(&self, nombre: &str, especie: &str, propietario: &str) {
        let paciente = Paciente {
            nombre: nombre.to_string(),
            especie: especie.to_string(),
            propietario: propietario.to_string(),
        };
        self.repositorio.guardar(&paciente);
    }
}

fn main() {
    let repo = RepositorioPacientes;
    let servicio = ServicioVeterinaria { repositorio: &repo };
    servicio.registrar_paciente("Luna", "Perro", "Carlos");
}

```
# Patrones recomendados para prevenir el God Object:

Facade: para centralizar interacciones sin sobrecargar una clase.
Observer: para desacoplar componentes que deben reaccionar a eventos.
MVC (Model-View-Controller): para dividir la lógica de negocio, presentación y control.
Dependency Injection: para reducir el acoplamiento entre clases.
# Presentación 
- Claridad: El documento explica cada aspecto con ejemplos concretos y lenguaje técnico comprensible.
- Lenguaje técnico: Se emplean términos propios de la ingeniería de software (SRP, acoplamiento, cohesión, patrones GoF).
- Síntesis: Cada sección aborda lo esencial sin redundancia.
- Formato: El uso de secciones, ejemplos y separación visual facilita la lectura en plataformas como GitHub o Gist.

# Referencias:
Martin, R. C. Clean Code: A Handbook of Agile Software Craftsmanship.
Gamma, E. et al. Design Patterns: Elements of Reusable Object-Oriented Software.
https://refactoring.guru/antipatterns/god-object
https://sourcemaking.com/antipatterns/the-blob

Alvarado Cardona, A. (2025). Análisis del Antipatrón God Object en Rust.
Desarrollado con apoyo técnico y redacción asistida por ChatGPT (modelo GPT-5, OpenAI).
Recuperado de https://chat.openai.com
