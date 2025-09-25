# 🧪 Actividad de Cierre – Refactorizando Patrones Creacionales

## 🎯 Objetivo

Aplicar lo aprendido sobre **patrones creacionales (GoF)** detectando *code smells* y proponiendo refactorización en código realista. La dinámica simula una revisión profesional de código mediante **Pull Requests**.

---

## 📦 Proyecto Base

Este repositorio contiene interfaces y clases con **métodos redundantes** para simular el *code smell* #33. El código original obliga a implementar métodos innecesarios en todas las clases, reduciendo cohesión y aumentando acoplamiento.

Ejemplo original:

```java
public interface Vehiculo {
    void arrancar();
    void detener();
    void volar();   // ❌ No todos los vehículos vuelan
}
```

Problema: Clases como `Auto` o `Barco` deben implementar `volar()` aunque no aplique.

---

## ✅ Actividad

1. Crea una **rama nueva** en tu fork de GitHub:

```
fix/refactor-TuNombre
```

2. Analiza el problema y redacta el caso en este README.md.
3. Detecta **al menos 3 problemas graves de diseño** relacionados con patrones creacionales.
4. Refactoriza el código **solo lo necesario** para mejorar:

   * Legibilidad
   * Cohesión
   * Reutilización
5. Crea un **Pull Request** con el título:

```
Refactor Creacional – Item 33 – [Tu Nombre] – (opcional: anexo LLM)
```

---

## 🔍 Problema detectado

* La interfaz `Vehiculo` obliga a implementar métodos irrelevantes como `volar()`.
* Violación del **Principio de Segregación de Interfaces** (ISP).
* Alto acoplamiento y dificultad para pruebas unitarias.

---

## 🛠 Patrón aplicado

* **Factory Method** para instanciar la clase de vehículo adecuada.
* Interfaces separadas según comportamiento específico:

```java
public interface Vehiculo {
    void arrancar();
    void detener();
}

public interface Volador {
    void volar();
}
```

* Cada clase implementa solo las interfaces que necesita:

```java
public class Auto implements Vehiculo {
    public void arrancar() { /* ... */ }
    public void detener() { /* ... */ }
}

public class Avion implements Vehiculo, Volador {
    public void arrancar() { /* ... */ }
    public void detener() { /* ... */ }
    public void volar() { /* ... */ }
}
```

* La creación de objetos queda encapsulada mediante **Factory Method**:

```java
public abstract class VehiculoFactory {
    public abstract Vehiculo crearVehiculo();
}

public class AutoFactory extends VehiculoFactory {
    @Override
    public Vehiculo crearVehiculo() {
        return new Auto();
    }
}

public class AvionFactory extends VehiculoFactory {
    @Override
    public Vehiculo crearVehiculo() {
        return new Avion();
    }
}
```

---

## 💡 Justificación

* Se reduce el **acoplamiento** y se aumenta la **cohesión**.
* Cada clase implementa **solo lo que necesita**.
* La creación de objetos queda **encapsulada**, facilitando pruebas unitarias y futuros cambios.

---

## 🔄 Impacto

* Cumplimiento de **ISP** y **Principio de Inversión de Dependencias (DIP)**.
* Arquitectura flexible y preparada para nuevos tipos de vehículos.
* Código más **legible, mantenible y testeable**.

---

## 💥 Referencia de Code Smells 

* Interfaces con métodos redundantes.
* Clases obligadas a implementar métodos que no aplican.
* Dificultad de pruebas unitarias.
* Violación del Principio de Segregación de Interfaces (ISP).
* Acoplamiento innecesario y baja cohesión.
