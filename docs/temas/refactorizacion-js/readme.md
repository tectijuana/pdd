# Alumno: Estrada Solano Abraham 22211899
# Refactorización de código orientado a eventos en JavaScript

La refactorización es el proceso de mejorar la estructura y el diseño del código sin cambiar su funcionamiento. El objetivo es hacer que el código sea más limpio, fácil de entender y sencillo de mantener. Esto no significa añadir nuevas funciones o corregir errores, sino organizar mejor el código para que sea más eficiente y escalable.

La refactorización en los sistemas de streaming garantiza que los componentes que gestionan el procesamiento de datos en tiempo real sean eficientes, flexibles y fáciles de mantener a lo largo del tiempo. A medida que los sistemas de streaming se amplían, un código mal estructurado puede convertirse en un obstáculo, lo que provoca una degradación del rendimiento o dificultades para añadir nuevas funciones. Mediante la refactorización, los desarrolladores pueden optimizar los flujos de datos, mejorar la utilización de los recursos y simplificar la depuración y la supervisión, lo que garantiza que los sistemas de streaming sigan siendo fiables incluso cuando aumentan los volúmenes de datos.

En este contexto, la refactorización no solo ayuda a los desarrolladores a mantener un código limpio, sino que también respalda la escalabilidad y la fiabilidad de toda la arquitectura de streaming, lo que la convierte en una práctica crucial en entornos con un uso intensivo de datos.

---

## La importancia de la refactorización

### Mantener un código limpio
En cualquier proyecto de software de gran envergadura, el código limpio se refiere a un código bien estructurado, legible y fácil de mantener que se ajusta a las mejores prácticas, lo que facilita su depuración, ampliación y optimización a lo largo del tiempo.

### Mejora de la depuración y el mantenimiento
Un código bien escrito permite a los ingenieros identificar y solucionar rápidamente los cuellos de botella, lo que ayuda a minimizar el tiempo de inactividad o los retrasos en la transmisión de datos. Cuando se procesan grandes volúmenes de datos, incluso una pequeña ineficiencia en el código puede provocar retrasos o sobrecargar los recursos, por lo que un código limpio ayuda a evitar estos problemas.

### Utilización eficiente de los recursos
In cloud environments, where you are billed based on resource usage, writing clean, efficient code minimizes the overhead and ensures that your system uses resources (e.g., compute and storage) effectively. Efficient pipelines are able to process more data with fewer compute resources, reducing operational costs while maintaining performance.

### Reducir la deuda técnica
En su prisa por ofrecer nuevas funciones, los equipos suelen hacer concesiones que acumulan deuda técnica, es decir, el costo de corregir posteriormente un código que no es óptimo. La refactorización periódica es la forma de saldar esta deuda antes de que se salga de control.  
En los sistemas de streaming, la deuda técnica puede manifestarse en forma de partición ineficiente de temas, esquemas mal diseñados o lógica de procesamiento de eventos engorrosa. En el contexto de los sistemas de streaming, la deuda técnica puede manifestarse de varias maneras, como una partición ineficiente de temas, esquemas mal diseñados y una lógica de procesamiento de eventos inadecuada.

---

## Técnicas de Refactorización

### Método de extracción
Cuando un bloque de código realiza demasiadas tareas a la vez, resulta difícil seguirlo y mantenerlo. El **método de extracción** consiste en tomar parte de ese código y moverlo a su propia función o método. Cada nuevo método debe tener un nombre claro y descriptivo basado en lo que hace. Este enfoque hace que el código esté más organizado, sea más fácil de entender y más fácil de probar.

#### Ejemplo

**Antes de la refactorización**
```javascript
function procesarPedido(pedido) {
  console.log("Procesando pedido de " + pedido.cliente);
  
  // Calcular total
  let total = 0;
  for (let i = 0; i < pedido.items.length; i++) {
    total += pedido.items[i].precio * pedido.items[i].cantidad;
  }
  console.log("Total: $" + total);

  // Enviar confirmación
  console.log("Enviando confirmación a " + pedido.cliente);
}
```
**Problema:**
El cálculo del total está dentro de la función y mezcla responsabilidades.
Esto hace que el código sea menos claro y difícil de reutilizar en otros lados.

**Después de la refactorización (Extracción)**
```javascript
function calcularTotal(items) {
  let total = 0;
  for (let i = 0; i < items.length; i++) {
    total += items[i].precio * items[i].cantidad;
  }
  return total;
}

function procesarPedido(pedido) {
  console.log("Procesando pedido de " + pedido.cliente);

  const total = calcularTotal(pedido.items);
  console.log("Total: $" + total);

  console.log("Enviando confirmación a " + pedido.cliente);
}
```
### Cambiar el nombre de las variables
Los nombres adecuados para las variables son esenciales para que el código sea legible.
Durante la refactorización, es posible que encuentres que algunas variables tienen nombres vagos o confusos.
Por ejemplo, usar un nombre como x o datos no proporciona suficiente información sobre lo que representa la variable.
**Cambiar el nombre de estas variables** por algo más descriptivo, como costoTotal o datosDelCliente, ayudará a cualquiera que lea el código a entenderlo más fácilmente.
#### Ejemplo

**Antes de la refactorización**
```javascript
  
  function calcular(d, t) {
  return d / t;
}

let r = calcular(100, 2);
console.log("Resultado: " + r);
```
**Problema:**
**d**, **t** y **r** no dicen mucho sobre su propósito.
Otro programador tendría que leer toda la función para entender que son distancia, tiempo y velocidad.

**Después de la refactorización**
```javascript
  function calcularVelocidad(distancia, tiempo) {
  return distancia / tiempo;
  }

  let velocidad = calcularVelocidad(100, 2);
  console.log("Velocidad: " + velocidad + " km/h");
```

### Simplificar las condiciones
Las sentencias condicionales complejas (como las sentencias **if** o **switch**) pueden dificultar la lectura y comprensión del código.
La refactorización de estas sentencias condicionales puede implicar varias técnicas.

#### Ejemplo

**Antes de la refactorización**
```javascript
function puedeVotar(edad, esCiudadano) {
  if (edad >= 18) {
    if (esCiudadano === true) {
      return true;
    } else {
      return false;
    }
  } else {
    return false;
  }
}

console.log(puedeVotar(20, true));  // true
console.log(puedeVotar(16, true));  // false
```

**Problema:**
Hay ifs anidados innecesarios.
El código es más largo y menos legible.

**Después de la refactorización**
```javascript
  function puedeVotar(edad, esCiudadano) {
    return edad >= 18 && esCiudadano;
  }

  console.log(puedeVotar(20, true));  // true
  console.log(puedeVotar(16, true));  // false
```

### Referencia APA
Refactoring (code): Everything you need to know. (s/f). Confluent. Recuperado el 19 de septiembre de 2025, de https://www.confluent.io/learn/refactoring/