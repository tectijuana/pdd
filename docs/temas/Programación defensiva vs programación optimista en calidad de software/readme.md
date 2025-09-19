![IDESs Populares](https://www.tijuana.tecnm.mx//wp-content/uploads/2022/03/TecNM-ITT-sgc-2018-color-scaled-e1646127126124-1568x479.jpg)
## Datos del Alumno
#### Nombre: Emmanuel Isai Chavez Hernandez
#### No. Control: 23211005
#### Materia: Patrones de Diseño
#### Fecha: 18/09/2025

---

## Índice
1. [Programación defensiva](#programación-defensiva)  
2. [Principios clave de la programación defensiva](#principios-clave-de-la-programación-defensiva)  
3. [Ventajas de la programación defensiva](ventajas-de-la-programación-defensiva)  
4. [Desventajas de la programación defensiva](desventajas-de-la-programación-defensiva)  
5. [Programación optimista](#programación-optimista)  
6. [Principios clave de la programación optimista](#principios-clave-de-la-programación-optimista)  
7. [Ventajas de la programación optimista](ventajas-de-la-programación-optimista)  
8. [Desventajas de la programación optimista](desventajas-de-la-programación-optimista)  
9. [Conclusión](#conclusión)
10. [Referencias](#referencias)

---

# Programación defensiva vs programación optimista en calidad de software

---

## Programación defensiva 
La programación defensiva en calidad de software consiste en que el código responda de manera adecuada tanto para entradas de datos no válidas como para aquellos casos que creamos imposibles de corregir (Luis Parravicini, 2011, p.10).
Esto quiere decir que si al comprobar los parámetros de entrada que se reciben estos no son correctos se lanza una excepción (José F. Velez Serrano, 2011, p.78).

---

**Diagrama de la barrera de la programación defensiva**

![Programacion defensiva](https://interrupt.memfault.com/img/defensive-and-offensive-programming/internal-software.png)

> Fuente: interrupt.memfault.com

---

## Principios clave de la programación defensiva
- Validar entradas: Las entradas se deben de validar para proteger la aplicación de datos erróneos o usos indebidos
- Utilizar afirmaciones para la corrección del código: Las afirmaciones garantizan que las condiciones se mantengan verdaderas durante la ejecución del programa
- Manejo de excepciones estratégicas: Un manejo de excepciones bien elaborado asegura que la aplicación permanecerá estable ante errores.
- Alcance de variables: Reducir el alcance de las variables evita problemas a futuro y hace que el código se vea más limpio
- Configuraciones seguras predeterminadas: Las aplicaciones deben de tener a la seguridad como una opción predeterminada
- Desinfectar datos: Las entradas de los usuarios deben de ser limpiadas para evitar ataques de inyección
- Evitar números mágicos: Los valores codificados es mejor reemplazarlos con constantes para que el código sea más comprensible

**Atributos de la programación defensiva**

![Programacion defensiva](https://guidohenkel.com/wp-content/uploads/2022/04/DefensiveProgramming1.png)

> Fuente: guidohenkel.com

---

## Ventajas de la programación defensiva
- Mejora la seguridad del software: Al anticipar vulnerabilidades se pueden mitigar los ataques
- Aumenta la confiabilidad y estabilidad de las aplicaciones: Al implementar medidas proactivas contribuye a crear aplicaciones menos propensas a errores
- Reduce el riesgo de vulnerabilidades: Al validar detalladamente las entradas previene ataques de inyección
- Facilita el mantenimiento: Hace que el código sea más fácil de entender, mantener y depurar, al documentar y declarar las pre-condiciones 

---

## Desventajas de la programación defensiva
- Requiere mucho más código: Como mínimo, se tienen muchas más condiciones y comprobaciones que un programa similar sin programación defensiva.
- El rendimiento puede ser peor: Esto se debe a que las comprobaciones adicionales tardan en ejecutarse.
- Hace que sea más difícil trabajar con el código: Porque al implementar más medidas de comprobación el código crece exponencialmente.
- Recuperación de errores lenta: La recuperación de errores puede llevar mucho tiempo de planificación e implementación.

---

## Ejemplo de programación defensiva

El siguiente código escrito en JavaScript demuestra el uso de la función foo para validar datos.

```
function foo(nonEmptyString, naturalInteger) {
  if (
    typeof nonEmptyString !== 'string' || // si no es una cadena 
    nonEmptyString === '' || // si la cadena esta vacia 
    !Number.isInteger(naturalInteger) || // si no es un entero 
    naturalInteger < 1 // si no es un entero natural (1 o mas) 
  ) {
    // Opciones de manejo de errores
    // Cerrar el programa
    // o manejar el error en este apartado
    // o enviar una excepcion para que un codigo superior maneje el error 
    // o realizare cualquier metodo que la implementacion de recuperaciones de errores requiera 
  }
  // codigo para la ejecucion de una funcion normal 
}
```

> Fuente: programmingduck-com

---

## Programación optimista
La programación optimista se basa en aquellos programas que permiten una secuencia de acciones que interfieren en algunas corridas ejecutadas concurrentemente basadas en una predicción o suposición optimista. Los programas basados en programación optimista han sido usados en una variedad de aplicaciones como tolerancias al fallo, replicaciones, control de concurrencias y simulación de eventos discretos. En estos casos la programación optimista fue utilizada para disminuir el tiempo de respuesta y mejorar el rendimiento.
En base de datos la programación optimista es utilizada para el control de concurrencias donde se asume la improbabilidad de los conflictos entre transacciones.

---

**Algoritmos de control de concurrencias**

![Programación optimista](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEittis-ZGxf_bOHHq6Geqz2fL-vzj1TPij6Tfcsc42VphEBDYJWenhbDiPKSFBIfrnYGlacGzG1GwFtgJwU0hnaD3gpx84RC2_XwmuJwUBS1ZvGt7VLg-Shka1_ermmjQKX5N_pruJ3_zQA/s1600/clasalgor.gif)

> Fuente: transyconssistedist.blogspot.com

---

## Principios clave de la programación optimista
 - Suposición de éxito: El principio fundamental consiste en asumir que una operación o cálculo determinado se ejecutará correctamente, incluso antes de confirmar su resultado.
 - Concurrencia y paralelismo: Al proceder con el resultado supuesto, otras tareas o cálculos pueden ejecutarse en paralelo, lo que aumenta la velocidad general del sistema..
 - Actualizaciones inmediatas de la interfaz de usuario: En las aplicaciones orientadas al usuario, las actualizaciones optimistas muestran los cambios al instante, lo que mejora la percepción de capacidad de respuesta y la experiencia del usuario.
 - Gestión de errores para la reversión: El sistema debe estar preparado para gestionar los casos en los que la suposición optimista fue incorrecta. Esto implica revertir los cambios o restaurar el estado original si la operación finalmente falla.

---

## Ventajas del enfoque optimista
 - En una transacción con enfoque optimista, la reversión se vuelve muy fácil cuando los contactos están ahí.
 - En un enfoque optimista, no encontrará ninguna reversión en cascada porque solo utiliza la copia local de los datos y no la base de datos.

---

## Desventajas del enfoque optimista
 - Usar un enfoque optimista para el control de concurrencia puede ser muy costoso, ya que es necesario revertirlo.
 - Si hay un conflicto entre transacciones grandes y pequeñas en este método, entonces solo las transacciones grandes se revierten repetidamente ya que constan de más conflictos.

---

## Ejemplo de programación optimista en JavaScript 

```
// Optimistically Render Successful Button Like

likeButton.addEventListener("click", function (event) {
  optimisticallyUpdatePage()

  //begin server call
  fetch(API_URL)
  .then(response => response.json())
  .then(json => console.log("Successful Server Call"))
  .catch(error => {
    console.error("Unsuccessful Server Call", error)
    revertOptimisticChanges() //a function that undos the changes made
  })

})

// a helper function that updates the DOM
function optimisticallyUpdatePage(){
  updateLikeButton()
  increaseLikeAmount()
}
```
El codigo esta programado de manera optimista porque no espera a que el servidor accione los eventos de coneccion exitosa y asume que la coneccion ocurrira sin problema alguno.

> Fuente: medium.com

---

## Conclusión 
La programación defensiva como la programación optimista pueden influir directamente en la calidad del software ya sea para aumentar la calidad y disminuirla si no son aplicadas de manera correcta. Cada tipo de programación es útil en su propio contexto y trae grandes beneficios en el desarrollo de aplicaciones.

Por un lado la programación defensiva mejora la robustez y la seguridad del software buscando siempre anticiparse a los posibles fallos que puedan ocurrir a futuro y validando de manera continua todas las entradas y el entorno. Si se abusa de la programación defensiva podría llegar a ser contraproducente haciendo que el software se vuelva más complejo y difícil de mantener.

Mientras tanto la programación optimista asume que el sistema funcionará de manera correcta bajo condiciones normales lo que hace que el código sea más limpio y fácil de entender, esto puede aumentar el rendimiento del programa si es bien aplicado. En entornos controlados es la mejor opción pero se puede poner en riesgo la integridad del sistema si los errores no son controlados correctamente. 

---

## Referencias
- Google Books. (s. f.). https://www.google.com.mx/books/edition/Programaci%C3%B3n_web_segura/FK2rwqAAPsAC?hl=es&gbpv=1&dq=Programaci%C3%B3n+defensiva&pg=PA10&printsec=frontcover
- Google Books. (s. f.-b). https://www.google.com.mx/books/edition/Dise%C3%B1ar_y_programar_todo_es_empezar/lk57JxHhpyAC?hl=es&gbpv=1&dq=Programaci%C3%B3n+defensiva&pg=PA78&printsec=frontcover%20-
- Top defensive programming principles with examples. (2024, 21 abril). https://umbracare.net/blog/top-defensive-programming-principles-with-examples/
- Maldonado, R. (2024, 6 junio). Programación defensiva: ¿Qué es y cómo se hace? KeepCoding Bootcamps. https://keepcoding.io/blog/que-es-la-programacion-defensiva/
- Argalias, S. (2022, 16 enero). Defensive & offensive programming - Programming Duck. Programming Duck. https://programmingduck.com/articles/defensive-programming
- Cowan, C., Lutfiyya, H., & Bauer, M. (2002). Performance benefits of optimistic programming: a measure of HOPE. IEEE Xplore, 197-204. https://doi.org/10.1109/hpdc.1995.518710
- GeeksforGeeks. (2025, 23 julio). Difference Between Pessimistic Approach and Optimistic Approach in DBMS. GeeksforGeeks. https://www-geeksforgeeks-org.translate.goog/dbms/difference-between-pessimistic-approach-and-optimistic-approach-in-dbms/?_x_tr_sl=en&_x_tr_tl=es&_x_tr_hl=es&_x_tr_pto=tc
