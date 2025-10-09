# 🎵 Práctica de Refactorización: Tienda de Música Digital (Python)

## 🧱 Descripción del problema original

La **Tienda de Música Digital** administra un catálogo de instrumentos musicales (guitarra, piano, batería, violín, etc.).  
El sistema original —desarrollado en **Python**— permite reproducir sonidos de cada instrumento, pero **cada nuevo instrumento requiere modificar** el código de la clase principal `MusicStore`, que contiene condicionales (`if`, `elif`, `else`) para decidir qué instrumento crear.

Esto provoca un **alto acoplamiento** y **viola varios principios de diseño**, dificultando la extensión del sistema y el mantenimiento del código en Python.

---

## 💣 Problemas detectados

| # | Problema | Descripción |
|---|-----------|-------------|
| 1 | Uso excesivo de condicionales | Cada nuevo instrumento requiere modificar `MusicStore`. |
| 2 | Falta de abstracción | No existe una interfaz común (por ejemplo, `Instrument`) para los instrumentos. |
| 3 | Alto acoplamiento | `MusicStore` conoce las clases concretas (`Guitar`, `Piano`, etc.). |
| 4 | Violación del principio Open/Closed | No es posible extender sin modificar código existente. |
| 5 | Falta de cohesión | `MusicStore` tiene múltiples responsabilidades (crear, listar, reproducir). |
| 6 | Dificultad para testear | No se pueden sustituir dependencias fácilmente. |
| 7 | Sin inversión de dependencias | `MusicStore` crea objetos directamente. |
| 8 | Duplicación de lógica | Se repiten estructuras similares en varios condicionales. |

---

## 🧩 Patrón(es) aplicados y justificación

| Patrón | Propósito | Justificación |
|--------|------------|---------------|
| **Factory Method (en Python)** | Delegar la creación de objetos a una subclase o fábrica. | Elimina condicionales, permite agregar instrumentos sin modificar la tienda, y desacopla la creación de objetos. |
| *(Opcional)* **Abstract Factory** | Crear familias de objetos relacionados. | Útil si se quisiera extender el sistema a diferentes tipos de instrumentos (cuerdas, percusión, viento, etc.). |

---

### 🧠 Beneficio principal

> “El cliente (`MusicStore`) deja de conocer las clases concretas de los instrumentos.  
> Solo depende de una abstracción (`Instrument`) y una fábrica que decide qué crear.”  

Este enfoque, aplicado en **Python**, facilita el mantenimiento, mejora la extensibilidad del sistema y respeta la filosofía de código limpio y desacoplado.

---

## 🧱 Cómo la solución respeta los principios SOLID

| Principio | Aplicación en la solución refactorizada |
|------------|------------------------------------------|
| **S - Single Responsibility** | Cada clase tiene una responsabilidad única (los instrumentos tocan, la fábrica crea, la tienda coordina). |
| **O - Open/Closed** | Pueden agregarse nuevos instrumentos sin modificar las clases existentes. |
| **L - Liskov Substitution** | Todos los instrumentos implementan la misma interfaz (`Instrument`), por lo tanto, son intercambiables. |
| **I - Interface Segregation** | Se usa una interfaz simple (`Instrument`) con un método esencial (`play()`). |
| **D - Dependency Inversion** | `MusicStore` depende de abstracciones, no de clases concretas. |

---

## 🐍 Lenguaje y entorno de desarrollo

- **Lenguaje:** Python 3.x  
- **Paradigma aplicado:** Programación orientada a objetos (OOP)  
- **Patrón de diseño principal:** Factory Method  
- **Objetivo:** Reducir acoplamiento y facilitar la extensión del catálogo de instrumentos.

---
## 🧩 Código base con malas prácticas (bad_music_store.py)
```python
# 🎵 Tienda de Música Digital (versión mala)
# Este código rompe todos los principios SOLID y no usa patrones GoF.
# Tu misión será identificar los problemas y luego refactorizarlo.

class MusicStore:
    def __init__(self):
        pass

    def play_instrument(self, instrument_type):
        # 💥 Gran cantidad de condicionales
        if instrument_type == "guitar":
            guitar = Guitar()
            guitar.play()
        elif instrument_type == "piano":
            piano = Piano()
            piano.play()
        elif instrument_type == "drum":
            drum = Drum()
            drum.play()
        elif instrument_type == "violin":
            violin = Violin()
            violin.play()
        else:
            print("❌ Instrumento no reconocido.")

    def list_instruments(self):
        # ⚠️ Cada vez que agregas un nuevo instrumento, hay que modificar esta lista
        print("Instrumentos disponibles: guitar, piano, drum, violin")


# 🪕 Clases concretas sin una interfaz común
class Guitar:
    def play(self):
        print("🎸 Tocando una guitarra eléctrica...")

class Piano:
    def play(self):
        print("🎹 Tocando un piano clásico...")

class Drum:
    def play(self):
        print("🥁 Tocando una batería...")

class Violin:
    def play(self):
        print("🎻 Tocando un violín...")


# 💣 Clase principal totalmente dependiente de MusicStore
if __name__ == "__main__":
    store = MusicStore()
    print("🎶 Bienvenido a la Tienda de Música Digital 🎶")
    store.list_instruments()
    choice = input("👉 Elige un instrumento: ").strip().lower()
    store.play_instrument(choice)
    print("✅ Fin de la demostración.")

```

---

## 🧩 Código corregido (bad_music_store.py)
```python
# 🎵 Tienda de Música Digital (versión refactorizada con Factory Method)
# ✅ Cumple con los principios SOLID y aplica un patrón GoF (Factory Method).

from abc import ABC, abstractmethod

# 🎸 1️⃣ Interfaz común para todos los instrumentos
class Instrument(ABC):
    @abstractmethod
    def play(self):
        pass


# 🎶 2️⃣ Clases concretas de instrumentos
class Guitar(Instrument):
    def play(self):
        print("🎸 Tocando una guitarra eléctrica...")

class Piano(Instrument):
    def play(self):
        print("🎹 Tocando un piano clásico...")

class Drum(Instrument):
    def play(self):
        print("🥁 Tocando una batería...")

class Violin(Instrument):
    def play(self):
        print("🎻 Tocando un violín...")


# 🏭 3️⃣ Fábrica: responsable de crear los instrumentos
class InstrumentFactory:
    def create_instrument(self, instrument_type: str) -> Instrument:
        instrument_type = instrument_type.lower()
        instruments = {
            "guitar": Guitar,
            "piano": Piano,
            "drum": Drum,
            "violin": Violin
        }

        if instrument_type in instruments:
            return instruments[instrument_type]()
        else:
            raise ValueError("❌ Instrumento no reconocido.")


# 🏪 4️⃣ MusicStore depende de abstracciones, no de clases concretas
class MusicStore:
    def __init__(self, factory: InstrumentFactory):
        self.factory = factory

    def play_instrument(self, instrument_type: str):
        try:
            instrument = self.factory.create_instrument(instrument_type)
            instrument.play()
        except ValueError as e:
            print(e)

    def list_instruments(self):
        print("🎵 Instrumentos disponibles: guitar, piano, drum, violin")


# 🚀 5️⃣ Punto de entrada principal
if __name__ == "__main__":
    store = MusicStore(InstrumentFactory())
    print("🎶 Bienvenido a la Tienda de Música Digital 🎶")
    store.list_instruments()

    choice = input("👉 Elige un instrumento: ").strip().lower()
    store.play_instrument(choice)

    print("✅ Fin de la demostración.")
```

<img width="1092" height="361" alt="Screenshot 2025-10-09 162952" src="https://github.com/user-attachments/assets/fba87206-e406-46ef-8b57-f625d19caa92" />
<img width="719" height="228" alt="Screenshot 2025-10-09 163004" src="https://github.com/user-attachments/assets/2ea592f1-30e5-437e-953e-4b5622bc494b" />

## ✅ Pruebas unitarias de las clases desacopladas
```python
import unittest
from factory.concrete_factory import ConcreteInstrumentFactory

class TestInstrumentFactory(unittest.TestCase):
    def test_create_guitar(self):
        factory = ConcreteInstrumentFactory()
        instrument = factory.create_instrument("guitar")
        self.assertIsNotNone(instrument)
        self.assertTrue(hasattr(instrument, "play"))

    def test_invalid_instrument(self):
        factory = ConcreteInstrumentFactory()
        with self.assertRaises(ValueError):
            factory.create_instrument("unknown")

if __name__ == "__main__":
    unittest.main()

```
<img width="629" height="301" alt="Screenshot 2025-10-09 163359" src="https://github.com/user-attachments/assets/78f46955-2a93-4af8-bdb7-66fe08551c82" />
<img width="711" height="227" alt="image" src="https://github.com/user-attachments/assets/33fe83bd-d3e0-4e49-81d9-51b6d5f678fc" />

## 🧩Diagrama UML de tu solución final.
<img width="505" height="654" alt="Screenshot 2025-10-09 161313" src="https://github.com/user-attachments/assets/93dcb317-1cde-4743-841d-378162b074c5" />

## 💭 Reflexión personal

Esta práctica me permitió comprender de manera más profunda cómo aplicar principios SOLID y patrones de diseño, como el Factory Method, para mejorar la calidad y mantenibilidad del software. El código original presentaba alto acoplamiento, condicionales excesivas y dificultades para extender funcionalidades, problemas muy comunes en sistemas reales.

Refactorizar la Tienda de Música Digital me enseñó a desacoplar responsabilidades, organizar mejor las clases y facilitar la extensibilidad, aplicando buenas prácticas de diseño orientadas a objetos. Como resultado, el código es más limpio, flexible y fácil de probar, lo que refuerza la importancia de crear software robusto, sostenible y fácil de mantener, no solo funcional. Esta experiencia fortaleció mi visión como futuro ingeniero de software, enfocándome en soluciones elegantes y de calidad.
