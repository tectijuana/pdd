![IDESs Populares](https://www.tijuana.tecnm.mx//wp-content/uploads/2022/03/TecNM-ITT-sgc-2018-color-scaled-e1646127126124-1568x479.jpg)
## Datos del Alumno
#### Nombre: Emmanuel Isai Chavez Hernandez
#### No. Control: 23211005
#### Materia: Patrones de Diseño
#### Fecha: 18/09/2025

---

## Índice
1. [Introducción](#introducción)  
2. [Entorno de Desarrollo Integrado (IDE)](#entorno-de-desarrollo-integrado-ide)  
3. [Cadenas de herramientas (Toochains)](#cadenas-de-herramientas-toolchains)  
4. [IDEs y cadenas de herramientas específicas](#ides-y-cadenas-de-herramientas-específicas)
5. [Conclusión sobre IDEs y Toolchains](#conclusión-sobre-ides-y-toolchains)
6. [Referencias](#referencias)

---

# Programación defensiva vs programación optimista en calidad de software

---

## Programación defensiva 
La programación defensiva consiste en que el código responda de manera adecuada tanto para entradas de datos no validas como para aquellos casos que creamos imposibles de corregir (Luis Parravicini, 2011, p.10).
Esto quiere decir que si al comprobar los parametros de entrada que se reciben estos no son correctos se lanza una excepcion (Jose F. Velez Serrano, 2011, p.78).

---

## Principios clave de la programación defensiva
- Validar entradas: Las entradas se deben de validar para proteger la aplicacion de datos erroneos o usos indebidos
- Utilizar afirmaciones para la corrección del código: Las afirmaciones garantizan que las condiciones se mantengan verdaderas durante la ehjecucion del programa
- Manejo de excepciones estrategicas: Un manejo de excepciones bien elaborado asegura que la aplicacion permanecera estable ante errores.
- Alcance de variables: Reducir el alcance de las variables evita problemas a futuro y hace que el codigo se vea mas limpio
- Configuraciones seguras predeterminadas: Las aplicaciones deben de tener a la seguridad como una opcion predeterminada
- Desinfectar datos: Las entradas de los usuarios deben de ser limpiadas para evitar ataques de inyeccion
- Evitar numeros magicos: Los valores codificados es mejor reemplazarlos con constantes paar que el codigo sea mas comprensible

---

## Ventajas de la programacion defensiva
- Mejora la seguridad del software: Al anticipar vulnerabilidades se pueden mitigar los ataques
- Aumenta la confiabilidad y estabilidad de las aplicaciones Al implementar medidas proactivas contribuye a crear aplicaciones menos propensas a errores
- Reduce el riesgo de vulnerabilidades Al validar detalladamente las entradas previene ataques de inyeccion
- Facilita el mantenimiento: Hace que el codigo sea mas facil de entender, mantener y depurar, al documentar y declarar las pre-condiciones 

---

## Desventajas de la programacion defensiva
- Requiere mucho más código. Como mínimo, tendrás muchas más condiciones y comprobaciones que un programa similar sin programación defensiva.
- El rendimiento puede ser peor. Esto se debe a que las comprobaciones adicionales tardan en ejecutarse.
- Hace que sea más difícil trabajar con el código porque hay mucho más código.
- La recuperación de errores puede llevar mucho tiempo de planificación e implementación.

---
## Referencias
- Google Books. (s. f.). https://www.google.com.mx/books/edition/Programaci%C3%B3n_web_segura/FK2rwqAAPsAC?hl=es&gbpv=1&dq=Programaci%C3%B3n+defensiva&pg=PA10&printsec=frontcover
- Google Books. (s. f.-b). https://www.google.com.mx/books/edition/Dise%C3%B1ar_y_programar_todo_es_empezar/lk57JxHhpyAC?hl=es&gbpv=1&dq=Programaci%C3%B3n+defensiva&pg=PA78&printsec=frontcover%20-
- Top defensive programming principles with examples. (2024, 21 abril). https://umbracare.net/blog/top-defensive-programming-principles-with-examples/
- Maldonado, R. (2024, 6 junio). Programación defensiva: ¿Qué es y cómo se hace? KeepCoding Bootcamps. https://keepcoding.io/blog/que-es-la-programacion-defensiva/
- Argalias, S. (2022, 16 enero). Defensive & offensive programming - Programming Duck. Programming Duck. https://programmingduck.com/articles/defensive-programming
