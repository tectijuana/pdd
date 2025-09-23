# Refactorización Funcional vs. Refactorización Orientada a Objetos
#Joshua Isaias Ruiz Lopez 21212363
## 📌 Introducción
La refactorización es una práctica fundamental en el desarrollo de software. Su objetivo es **mejorar la calidad interna del código** sin alterar su funcionalidad externa.  
Este proceso no busca añadir nuevas características, sino **hacer que el código existente sea más claro, eficiente, mantenible y escalable**.

Dentro del mundo de la refactorización, existen distintos enfoques. Dos de los más comunes son:

- **Refactorización Funcional**: basada en principios de programación funcional como inmutabilidad, funciones puras y composición.
- **Refactorización Orientada a Objetos (OOP)**: basada en clases, objetos y encapsulación.

En este documento se profundiza en cada uno de estos estilos, mostrando ejemplos, ventajas, limitaciones y su impacto en la calidad del software.

---

## 🔹 Refactorización Funcional

La programación funcional es un paradigma que trata el cómputo como la evaluación de funciones matemáticas.  
En este contexto, la **refactorización funcional** busca transformar el código hacia:

- Uso de **funciones puras** (sin efectos secundarios).  
- **Inmutabilidad**: evitar modificar variables o estructuras de datos.  
- **Composición de funciones**: resolver problemas complejos combinando funciones pequeñas.  

Esto conduce a un código más **predecible, fácil de probar y menos propenso a errores**.

### Ejemplo en Python (antes y después)
```python
# Código antes de refactorizar
def calcular_total(productos):
    total = 0
    for p in productos:
        total += p["precio"]
    return total

productos = [{"precio": 10}, {"precio": 20}, {"precio": 30}]
print(calcular_total(productos))  # 60


# Refactorizado en estilo funcional
calcular_total = lambda productos: sum(p["precio"] for p in productos)

productos = [{"precio": 10}, {"precio": 20}, {"precio": 30}]
print(calcular_total(productos))  # 60
