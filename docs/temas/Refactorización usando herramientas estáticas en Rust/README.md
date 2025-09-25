# 📖 Refactorización usando herramientas estáticas en Rust

## ✨ Datos Personales

> **Alumno:** Alvarez Armenta Steve Jovanni<br>
> **Num de Control:** 21211909<br>
> **Materia:** Patrones de diseño

## ✨ Introducción

La **refactorización** es el proceso de **mejorar el código sin alterar su funcionalidad**, con el objetivo de hacerlo más legible, mantenible y robusto.
En **Rust**, este proceso cobra especial relevancia gracias a su sistema de **ownership, lifetimes y borrowing**, que garantizan seguridad en memoria y concurrencia sin necesidad de *garbage collector*.

Las **herramientas estáticas** de Rust —como `Clippy`, `Rustfmt`, `cargo-audit`, `cargo-udeps`— ayudan a identificar malas prácticas, dependencias inseguras y redundancias, permitiendo aplicar **refactorizaciones guiadas** que elevan la **calidad del software**.

Este tema conecta directamente con el curso de **Patrones de Diseño (PDD)**, ya que permite implementar patrones de manera más limpia, estructurada y segura.

---

## 🎯 Objetivos de la investigación

* ✅ **Presentación clara** del tema asignado.
* ✅ **Ejemplos y comparaciones prácticas**.
* ✅ **Relación directa** con refactorización, calidad y patrones de diseño.
* ✅ **Análisis crítico y originalidad**, enfocado en PDD.

---

## 🛠 Herramientas estáticas en Rust

* **Clippy** → Detecta redundancias, malas prácticas y sugiere mejoras.
* **Rustfmt** → Formatea el código según estándares oficiales.
* **cargo-audit** → Revisa dependencias inseguras o vulnerables.
* **cargo-udeps** → Detecta dependencias no utilizadas.

---

## 🔍 Ejemplos prácticos

### 1. Redundancia detectada por Clippy

```rust
fn main() {
    let x = 5;
    if x == 5 {
        println!("Cinco");
    } else {
        if x == 5 { // redundancia detectada por Clippy
            println!("Cinco otra vez");
        }
    }
}
```

👉 **Refactorizado:**

```rust
fn main() {
    let x = 5;
    if x == 5 {
        println!("Cinco");
    }
}
```

---

### 2. Patrones de diseño con `rustfmt` (Singleton)

Código **sin formato:**

```rust
use std::sync::{Arc, Mutex};

struct Config { value: String }
impl Config {
    fn new() -> Self { Config { value: "default".to_string() } }
}
static mut SINGLETON: Option<Arc<Mutex<Config>>> = None;
fn get_instance() -> Arc<Mutex<Config>> {
 unsafe {
 if SINGLETON.is_none() { SINGLETON = Some(Arc::new(Mutex::new(Config::new()))) }
 SINGLETON.clone().unwrap()
 }
}
```

👉 **Refactorizado con `rustfmt`:**

```rust
use std::sync::{Arc, Mutex};

struct Config {
    value: String,
}

impl Config {
    fn new() -> Self {
        Config {
            value: "default".to_string(),
        }
    }
}

static mut SINGLETON: Option<Arc<Mutex<Config>>> = None;

fn get_instance() -> Arc<Mutex<Config>> {
    unsafe {
        if SINGLETON.is_none() {
            SINGLETON = Some(Arc::new(Mutex::new(Config::new())));
        }
        SINGLETON.clone().unwrap()
    }
}
```

📌 **Impacto:** mismo patrón (Singleton), pero con claridad, consistencia y estándar.

---

### 3. Seguridad con `cargo-audit`

Dependencia vulnerable:

```toml
[dependencies]
chrono = "0.2"
```

Al ejecutar:

```bash
cargo audit
```

Se obtiene una alerta de vulnerabilidad.

👉 **Refactorización:** actualizar dependencias → código más **seguro y confiable**.

---

## 🧩 Relación con refactorización, calidad y patrones

* **Refactorización** en Rust no es solo limpieza visual, también **previene errores críticos en compilación**.
* Los **patrones de diseño** (Singleton, Factory, Observer, etc.) se benefician de un código limpio y libre de redundancias.
* La **calidad del software** mejora notablemente:

  * `rustfmt` asegura consistencia de estilo.
  * `Clippy` enseña mejores prácticas.
  * `cargo-audit` garantiza seguridad.
  * `cargo-udeps` elimina bloat innecesario.

---

## 🤔 Análisis crítico y reflexión

Rust introduce un **paradigma diferente** frente a lenguajes tradicionales como Java o C#:

* En **Java**, la refactorización se centra en optimizar estructuras OOP.
* En **Rust**, es esencial porque involucra **seguridad de memoria, concurrencia y rendimiento**.

El uso de herramientas estáticas en Rust no solo limpia el código:

* **Educa al programador** con sugerencias de mejores prácticas.
* **Previene deuda técnica** en proyectos grandes.
* Hace que los **patrones de diseño** sean más sostenibles y fáciles de mantener.

👉 En conclusión:
**Rust + refactorización + herramientas estáticas = software seguro, escalable y alineado a buenas prácticas de diseño.**

---

## 📌 Conclusión

La refactorización en Rust con ayuda de herramientas estáticas:

* 🔹 Mejora la implementación de **patrones de diseño**.
* 🔹 Eleva la **seguridad, mantenibilidad y escalabilidad**.
* 🔹 Promueve un código que no solo funciona, sino que **evoluciona de manera sana en el tiempo**.

> 💡 En un mundo donde la deuda técnica destruye proyectos, Rust y sus herramientas estáticas son un **escudo contra errores futuros**.

---

👨‍💻 *Curso de Patrones de Diseño – Investigación sobre refactorización en Rust*
