#Feature Envy (clases que espían otras clases)

1. Comprensión del Antipatrón: Explica qué es y por qué se considera una mala práctica.
2. Ejemplo Técnico: Muestra un ejemplo real o de código donde ocurra el antipatrón.
3. Consecuencias: Analiza los efectos en el mantenimiento, rendimiento o escalabilidad.
4. Solución Correctiva: Propone buenas prácticas o patrones alternativos para corregirlo.
5. Presentación: Claridad, lenguaje técnico, control del tiempo y síntesis.

##Comprensión del Antipatrón Feature Envy
El antipatrón conocido como Feature Envy ocurre cuando una clase o método accede en exceso a los datos o comportamientos de otra clase 
en lugar de manejar sus propias responsabilidades. En otras palabras, una clase "espía" a otra porque utiliza repetidamente sus atributos o métodos
para realizar tareas que, idealmente, deberían estar dentro de la propia clase. Esta dependencia excesiva es considerada una mala práctica porque rompe
con el principio de encapsulación, una piedra angular en la programación orientada a objetos. Cuando una clase depende demasiado de los detalles internos de otra,
se genera un acoplamiento fuerte que dificulta la evolución y el mantenimiento del código.

##Ejemplo Técnico
Imagina un sistema donde hay una clase Order y una clase Customer.
Supongamos que la clase Order tiene un método que calcula un descuento especial basado en la ubicación del cliente:

class Customer:
    def __init__(self, name, location):
        self.name = name
        self.location = location

class Order:
    def __init__(self, customer, amount):
        self.customer = customer
        self.amount = amount

    def calculate_discount(self):
        if self.customer.location == "VIP Zone":
            return self.amount * 0.2
        else:
            return self.amount * 0.1


En este ejemplo, el método calculate_discount de la clase Order accede directamente a la propiedad location de Customer para decidir el descuento.
Esto es un claro caso de Feature Envy, ya que Order está "espiando" los detalles internos de Customer para tomar una decisión que probablemente debería
estar más relacionada con el cliente.

##Consecuencias
El impacto de este antipatrón en el código es significativo. Primero, dificulta el mantenimiento: cualquier cambio en la estructura o lógica
de Customer puede afectar múltiples clases que dependen de sus detalles internos, creando un efecto dominó. Segundo, el acoplamiento fuerte reduce
la escalabilidad y flexibilidad, ya que las clases no son independientes y su reutilización se complica. Además, esta situación puede introducir problemas
de rendimiento cuando las clases hacen muchas consultas innecesarias a otras, y también puede aumentar la probabilidad de errores por cambios inesperados
en datos que deberían ser privados.

##Solución Correctiva
Para corregir el antipatrón Feature Envy, es fundamental aplicar principios de diseño orientado a objetos como la encapsulación y la responsabilidad única.
En el ejemplo anterior, la lógica para calcular el descuento basado en la ubicación debería trasladarse a la clase Customer. Así, Order delega la responsabilidad,
accediendo a un método en Customer que encapsula esa lógica:

class Customer:
    def __init__(self, name, location):
        self.name = name
        self.location = location

    def get_discount_rate(self):
        if self.location == "VIP Zone":
            return 0.2
        else:
            return 0.1

class Order:
    def __init__(self, customer, amount):
        self.customer = customer
        self.amount = amount

    def calculate_discount(self):
        discount_rate = self.customer.get_discount_rate()
        return self.amount * discount_rate


Este enfoque mejora la cohesión de cada clase y minimiza el acoplamiento, facilitando la evolución y el mantenimiento del sistema. Además,
patrones como Law of Demeter o Tell, Don't Ask ayudan a evitar este antipatrón al fomentar que los objetos se comuniquen mediante mensajes y
no accedan directamente a datos internos de otros objetos.

##Presentación
En resumen, el antipatrón Feature Envy representa una violación del principio de encapsulación donde una clase abusa del acceso a otra,
provocando problemas en la mantenibilidad y escalabilidad del software. Detectar y corregir este antipatrón mediante el reparto adecuado de responsabilidades
y el uso de métodos propios es clave para mantener un código limpio, modular y adaptable. Mantener una comunicación clara entre objetos mediante mensajes y
evitar dependencias internas excesivas es una práctica esencial para un diseño orientado a objetos saludable.
