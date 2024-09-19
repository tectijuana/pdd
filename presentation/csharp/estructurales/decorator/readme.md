## Elvirez Dávila Ulises Gabriel - 20211769
# Decorator
Decorator es un patrón de diseño estructural que te permite añadir funcionalidades a objetos 
colocando estos objetos dentro de objetos encapsuladores especiales que contienen estas funcionalidades.
El decorator hereda la interfaz de la misma clase que el objeto que decora, de esta forma permite que los objetos decorados puedan ser tratados como si fueran instancias del obejeto original. 

![DecoratorImage](https://refactoring.guru/images/patterns/content/decorator/decorator.png?id=710c66670c7123e0928d3b3758aea79e)

# Analogía en el mundo real
Vestir ropa es un ejemplo del uso de decoradores. Cuando tienes frío, te cubres con un suéter. Si sigues teniendo frío a pesar
del suéter, puedes ponerte una chaqueta encima. Si está lloviendo, puedes ponerte un impermeable. Todas estas prendas “extienden” 
tu comportamiento básico pero no son parte de ti, y puedes quitarte fácilmente cualquier prenda cuando lo desees.

![DecoratorImage](https://refactoring.guru/images/patterns/content/decorator/decorator-comic-1.png?id=80d95baacbfb91f5bcdbdc7814b0c64d)

# Uso en código
```Python
# Componente base: representa el objeto base, en este caso, un café básico.
class Cafe:
    def costo(self):
        return 5  # Precio base del café

    def descripcion(self):
        return "Café"  # Descripción del café base

# Decorador abstracto: mantiene una referencia al objeto Café y lo decora.
class DecoradorCafe(Cafe):
    def __init__(self, cafe):
        self._cafe = cafe  # Mantiene una referencia al objeto a decorar (el café)

    def costo(self):
        return self._cafe.costo()  # Delega el costo al objeto decorado

    def descripcion(self):
        return self._cafe.descripcion()  # Delega la descripción al objeto decorado

# Decorador concreto: añade leche al café y aumenta el costo.
class Leche(DecoradorCafe):
    def costo(self):
        return self._cafe.costo() + 1  # Añade el costo de la leche

    def descripcion(self):
        return self._cafe.descripcion() + ", Leche"  # Añade "Leche" a la descripción

# Decorador concreto: añade azúcar al café y aumenta el costo.
class Azucar(DecoradorCafe):
    def costo(self):
        return self._cafe.costo() + 0.5  # Añade el costo del azúcar

    def descripcion(self):
        return self._cafe.descripcion() + ", Azúcar"  # Añade "Azúcar" a la descripción

# Uso del patrón Decorator:
cafe_simple = Cafe()
print(cafe_simple.descripcion())  # "Café"
print(cafe_simple.costo())  # 5

# Decora el café con leche
cafe_con_leche = Leche(cafe_simple)
print(cafe_con_leche.descripcion())  # "Café, Leche"
print(cafe_con_leche.costo())  # 6

# Decora el café con leche y azúcar
cafe_con_leche_y_azucar = Azucar(cafe_con_leche)
print(cafe_con_leche_y_azucar.descripcion())  # "Café, Leche, Azúcar"
print(cafe_con_leche_y_azucar.costo())  # 6.5
```

# Pros
### ✔ Es posible extender el comportamiento de un objeto sin crear una nueva subclase.
### ✔ Es posible combinar varios comportamientos envolviendo un objeto con varios decoradores.
### ✔ Es posible añadir o eliminar responsabilidades de un objeto durante el tiempo de ejecución.
### ✔ Es posible de responsabilidad única. Puedes dividir una clase monolítica que implementa muchas variantes posibles de comportamiento, en varias clases más pequeñas.

# Contras
### ❌ Resulta difícil eliminar un wrapper específico de la pila de wrappers.
### ❌ El código de configuración inicial de las capas pueden tener un aspecto desagradable.
### ❌ Es difícil implementar un decorador de tal forma que su comportamiento no dependa del orden en la pila de decoradores.
