
## 💥 Lista de 50 Temas que Generan Code Smells

### 🏗️ Enfocados exclusivamente en Patrones Estructurales (GoF)

> Patrones estructurales GoF:

1. Adapter
2. Bridge
3. Composite
4. Decorator
5. Facade
6. Flyweight
7. Proxy

---

### 🔍 Adapter

1. Acoplar directamente dos clases incompatibles sin un adaptador intermedio
2. Usar `if` o `switch` para adaptar comportamiento en lugar de una clase Adapter
3. Crear adaptadores múltiples con duplicación de lógica
4. No seguir la interfaz esperada por el cliente
5. El adaptador depende de la clase concreta en lugar de una interfaz
6. Inyectar lógica de negocio en el adaptador
7. Mezclar adaptación y validación en la misma clase
8. Crear un adaptador con múltiples responsabilidades
9. Nombrar mal el adaptador, confundiendo su propósito
10. Olvidar probar casos límite de incompatibilidad

---

### 🔍 Bridge

11. Tener clases duplicadas en diferentes jerarquías de abstracción
12. Inyectar la implementación concreta en lugar de una interfaz
13. Acoplar fuertemente la abstracción con la implementación
14. Violación del principio de inversión de dependencias
15. Usar Bridge donde la herencia simple era suficiente
16. Mezclar lógica de abstracción e implementación en la misma clase
17. El cliente accede directamente a la implementación
18. No permitir cambiar la implementación en tiempo de ejecución
19. Añadir métodos innecesarios a la interfaz implementadora
20. Usar Bridge sin una verdadera necesidad de separación

---

### 🔍 Composite

21. No validar los hijos en una estructura compuesta
22. Violación del principio de transparencia al tener métodos que no aplican a hojas
23. Lógica condicional `if (esHoja)` en múltiples lugares
24. No aplicar la recursividad correctamente
25. Mezclar lógica de representación con lógica estructural
26. No unificar el tratamiento de hojas y compuestos
27. Crear nodos compuestos que no contienen hijos
28. Usar estructuras de control para distinguir tipos en lugar de polimorfismo
29. Repetir código para gestionar listas de hijos en varias clases
30. No utilizar interfaces comunes para todos los componentes

---

### 🔍 Decorator

31. Crear decoradores que alteran el estado en vez de solo añadir comportamiento
32. No mantener la misma interfaz que el componente decorado
33. Decoradores que no llaman al componente base
34. Decoradores mal encadenados
35. Uso excesivo de decoradores que complica la trazabilidad
36. No inyectar el componente base correctamente
37. Confundir Decorator con Proxy o Adapter
38. Agregar lógica de validación o construcción en el decorador
39. Romper la responsabilidad única en decoradores grandes
40. Implementar múltiples decoradores con comportamiento duplicado

---

### 🔍 Facade

41. Hacer que el Facade exponga detalles internos del sistema
42. Incluir lógica de negocio compleja dentro del Facade
43. Crear una fachada que dependa directamente de muchas clases concretas
44. No documentar qué simplifica la fachada
45. Romper la encapsulación interna al permitir llamadas profundas desde el Facade
46. Añadir funcionalidades nuevas solo al Facade, rompiendo SRP
47. No mantener coherencia entre subsistemas y fachada
48. Usar el Facade como clase “God” que lo controla todo
49. Tener múltiples fachadas que se solapan en responsabilidades
50. Ignorar excepciones internas, ocultando errores críticos al cliente

---

## 📊 Rúbrica de Evaluación

### Actividad: Refactorización de Patrones Estructurales (GoF)

> **Modalidad**: Individual
> **Duración estimada**: 50 minutos
> **Formato de entrega**: Pull Request en Git con justificación y refactor parcial
> **Lenguaje**: C# (.NET 8)
> **Contexto**: Refactor sobre base de código mal estructurado

---

### ✅ Criterios de Evaluación

| Criterio                                     | Descripción                                                                                                                                                                         | Puntos     |
| -------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------- |
| **1. Identificación de Code Smells**         | El estudiante detecta correctamente al menos **3 problemas estructurales** relacionados con patrones GoF.                                                                           | **25 pts** |
| **2. Aplicación del patrón adecuado**        | El patrón estructural utilizado (Adapter, Bridge, Composite, etc.) **es el más apropiado** para resolver el problema identificado.                                                  | **20 pts** |
| **3. Refactor funcional (parcial o total)**  | El código refactorizado compila, tiene lógica coherente y se integra correctamente con el resto del sistema. No se requiere refactor total, pero sí que **lo modificado funcione**. | **20 pts** |
| **4. Justificación técnica en Pull Request** | El PR incluye una descripción clara del problema, el patrón aplicado y los beneficios esperados. Se entiende la **intención del cambio**.                                           | **15 pts** |
| **5. Calidad del código refactorizado**      | Legibilidad, coherencia, nombres correctos, separación de responsabilidades y uso idiomático del lenguaje (C# / .NET 8).                                                            | **10 pts** |
| **6. Uso correcto de Git**                   | Se usó una rama adecuada (`fix/nombre-alumno`), commit semántico y PR bien formado.                                                                                                 | **5 pts**  |
| **7. Profesionalismo y presentación**        | El PR está bien redactado, sin errores graves de ortografía, y es **entendible por otros desarrolladores**.                                                                         | **5 pts**  |

---

### 🧮 Ponderación Total

| Nivel de logro  | Rango      | Descripción                                                                             |
| --------------- | ---------- | --------------------------------------------------------------------------------------- |
| Excelente 🟢    | 90–100 pts | Refactor claro, bien justificado, aplicación precisa del patrón y PR profesional.       |
| Bueno 🟡        | 75–89 pts  | Problemas menores de justificación, naming o elección del patrón, pero solución sólida. |
| Regular 🟠      | 60–74 pts  | Refactor incompleto o confuso, errores en la elección del patrón o PR poco claro.       |
| Insuficiente 🔴 | 0–59 pts   | No hay refactor real, se confunden patrones, o no se justifica adecuadamente.           |

---
