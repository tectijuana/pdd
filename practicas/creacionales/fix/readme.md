<img width="1024" height="1024" alt="image" src="https://github.com/user-attachments/assets/808ddcaa-0d31-4901-bceb-4a0c194155bb" />

# 🧪 Actividad de Cierre: Refactorizando Patrones Creacionales

## 🎯 Objetivo

Aplicar lo aprendido sobre **patrones creacionales (GoF)** mediante la detección de **code smells** y propuestas de refactorización en código realista. Esta actividad simula una revisión de código en un entorno de desarrollo profesional usando Pull Requests.

---

## 📦 Proyecto Base

El repositorio contiene clases implementadas incorrectamente con:
- Singleton
- Factory Method
- Abstract Factory
- Builder
- Prototype

El código presenta **malas prácticas intencionadas**, errores comunes, y anti-patrones frecuentes.

Compilador En linea: https://dotnetfiddle.net

---

## ✅ Actividad es dar "inversa" a como practica, generando el problema y resolverlo, puede usar temas como: vehiculos, celulares, etc para comprenda mejor el evento.

Durante los 50 minutos de clase, debes:

1. **Clonar este repositorio** en tu equipo local.
2. Crear una nueva rama con tu nombre:
   
> Donde será la recepción via **.../practicas/creacionales/fix/___(nombre del tema a tratar, sin su nombre)______/readme.md**
> y no olvide el anexo.md (prompts y reflexión OPCIONAL para eBOOKS)

4. Analizar el problema redactandlo hacia el caso, argumente su código y detectar **al menos 3 problemas graves de diseño** relacionados con los patrones creacionales.
5. Modificar solo lo necesario para mejorar la legibilidad, la cohesión y la reutilización usando un patrón correcto.
6. **Crear un Pull Request** con el título:

   ```bash
   Refactor Creacional - nombre del problema
     (despues su nombre, y algun comentario como anexo LLM, etc.=
   ```
7. Incluir en el cuerpo del PR lo siguiente:

   Nombre del problema-

---

## 📝 Formato del Pull Request


### 🔍 Problemas detectados
1. Clase `X` viola el principio de responsabilidad única.
2. Se detecta una instancia directa que debería ser manejada por un Factory.
3. El Singleton actual es inseguro en entorno multihilo.

### 🛠 Patrón aplicado
- Se implementa `Builder` para separar la construcción compleja de objetos `Vehiculo`.
- Se reemplaza el uso manual de `new` con un `Factory Method`.

### 💡 Justificación del cambio
Mejoramos:
- Cohesión interna
- Testabilidad
- Flexibilidad ante cambios

### 🔄 Impacto
Se asegura el cumplimiento del principio de inversión de dependencias y se prepara la arquitectura para facilitar pruebas unitarias.


---

## 💥 Lista de 50 Temas que Generan Code Smells


Estos son ejemplos de situaciones o prácticas comunes que pueden conducir a mal uso de patrones y deben ser evitadas:

1. Clases Dios (God Objects)
2. Singleton con estado mutable
3. Singleton sin control de concurrencia
4. Constructores con más de 4 parámetros
5. Constructores que ejecutan lógica pesada
6. Condiciones múltiples para crear objetos
7. Uso excesivo de `switch` para tipos
8. `new` directamente en el controlador
9. Clases que construyen y usan el objeto
10. Factories que retornan objetos inconsistentes
11. Falta de interfaz en los productos creados
12. Builders que exponen estado interno
13. Prototype sin implementación de `Clone`
14. Uso de patrones sin necesidad (overengineering)
15. Abuso de propiedades estáticas
16. Objetos anémicos sin comportamiento
17. Lógica duplicada en múltiples constructores
18. No aplicar principio de inversión de dependencias
19. Usar Singleton como contenedor global
20. No documentar qué patrón se está usando
21. Factory mezclado con lógica de negocio
22. Abuso de `ServiceLocator`
23. No encapsular los pasos del Builder
24. Clases que tienen múltiples responsabilidades
25. No separar creación del uso del objeto
26. Constructor que accede a base de datos
27. Singleton con dependencia externa inyectada mal
28. Builders no reutilizables
29. Uso de constantes mágicas para tipos
30. Herencia innecesaria entre productos
31. Confundir Abstract Factory con Factory Method
32. Builders con métodos obligatorios desordenados
33. Interfaces con métodos redundantes
34. Crear objetos sin validar estado
35. Factories que retornan clases concretas directamente
36. Falta de pruebas en objetos creados dinámicamente
37. No aplicar patrón NullObject en creación
38. Tener una clase `CreatorFactoryBuilder`
39. No inyectar dependencias necesarias en el constructor
40. Uso de `if-else` anidados para selección de tipos
41. Asignación de estado después de construcción
42. Singleton con `Dispose` sin patrón IDisposable
43. Factory con múltiples niveles de delegación
44. Acoplamiento fuerte entre cliente y producto
45. No implementar interfaces para los productos
46. Reutilizar Singletons para múltiples propósitos
47. Usar `Thread.Sleep` en el constructor
48. Crear múltiples instancias “Singleton” en pruebas
49. Ignorar el principio de sustitución de Liskov en fábricas
50. Atraparse en un anti-patrón por querer usar “todos los patrones”

---

**¡Buena suerte! Refactoriza con intención y diseña con elegancia.**


### 🔍 Clasificación por Patrón

#### 🧱 Singleton (items relacionados):

* 2, 3, 15, 19, 27, 42, 46, 48
  *Problemas: estado mutable, concurrencia, mal uso de global state, reciclaje indebido.*

#### 🏭 Factory Method (items relacionados):

* 6, 7, 8, 10, 11, 17, 20, 21, 22, 29, 30, 31, 35, 36, 40, 43, 44, 45, 49
  *Problemas: creación con `if`, sin polimorfismo, lógica en el Factory, ruptura de encapsulamiento.*

#### 🧰 Abstract Factory (items relacionados):

* 5, 11, 12, 20, 23, 26, 31, 35, 43, 45
  *Problemas: Factories mezclados, productos concretos visibles, no separación de familias.*

#### 🧑‍🔧 Builder (items relacionados):

* 4, 12, 18, 23, 24, 25, 28, 32, 41
  *Problemas: pasos desordenados, objetos en construcción inconsistentes, métodos obligatorios sin orden.*

#### 🧬 Prototype (items relacionados):

* 13, 34
  *Problemas: no implementar `Clone`, no validar estado copiado.*

#### 🧩 Comunes a todos los creacionales (generalizados):

* 1, 9, 14, 16, 33, 37, 38, 39, 47, 50
  *Problemas de responsabilidad única, separación de concerns, anti-patrones, mal diseño conceptual.*

---

---

## 📌 Evaluación

| Criterio                       | Puntos |
| ------------------------------ | ------ |
| Identificación de problemas    | 30%    |
| Aplicación correcta del patrón | 30%    |
| Justificación técnica          | 30%    |
| Claridad y formato del PR      | 10%    |
