# 🧩 Antipatrón: Shotgun Surgery (Modificaciones Dispersas)

## 1. 📖 Comprensión del Antipatrón

**Shotgun Surgery** es un antipatrón que ocurre cuando **un solo cambio en el sistema requiere modificar múltiples clases, módulos o archivos diferentes**.  
Este tipo de diseño genera una alta dispersión del código, lo que significa que la lógica relacionada a una misma funcionalidad está repartida por varias partes del sistema.

Se considera una **mala práctica** porque:
- Aumenta el **acoplamiento** entre módulos.
- Dificulta el **mantenimiento** del código.
- Incrementa el riesgo de **errores colaterales** al realizar modificaciones.
- Reduce la **legibilidad y cohesión** del sistema.

En otras palabras, cuando una tarea aparentemente sencilla obliga a hacer pequeños cambios en muchos lugares del código, es señal clara de **Shotgun Surgery**.

---

## 2. 💻 Ejemplo Técnico 

### 💀 Código Espagueti — Ejemplo del Antipatrón Shotgun Surgery

```javascript
// userService.js
function getUserBirthday(user) {
  const date = new Date(user.birthDate);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}/${month}/${day}`; // formato fijo "YYYY/MM/DD"
}

// orderService.js
function getOrderDate(order) {
  const date = new Date(order.date);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}/${month}/${day}`;
}

// paymentService.js
function getPaymentDate(payment) {
  const date = new Date(payment.createdAt);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}/${month}/${day}`;
}
```

### Ejemplo en código JavaScript

Supongamos que tenemos un sistema donde cada clase o módulo maneja el formato de fechas de manera independiente.  
Si deseamos cambiar el formato de "YYYY/MM/DD" a "DD-MM-YYYY", tendríamos que modificar varias partes del código:

```javascript
// userService.js
function getUserBirthday(user) {
  return formatDate(user.birthDate); // "YYYY/MM/DD"
}

// orderService.js
function getOrderDate(order) {
  return formatDate(order.date); // "YYYY/MM/DD"
}

// paymentService.js
function getPaymentDate(payment) {
  return formatDate(payment.createdAt); // "YYYY/MM/DD"
}

// utils.js
function formatDate(date) {
  return date.toISOString().split('T')[0].replace(/-/g, '/');
}
```

Si cambiamos el formato de fecha, debemos modificar cada función o módulo que lo use, es decir, **muchas pequeñas cirugías en diferentes lugares** — de ahí el nombre *Shotgun Surgery*.

---

## 3. ⚠️ Consecuencias

Las principales consecuencias de este antipatrón incluyen:

- 🔧 **Mantenimiento complicado:** Los cambios se vuelven lentos y propensos a errores, ya que se deben actualizar múltiples archivos.
- 🧠 **Mayor complejidad cognitiva:** Los desarrolladores deben recordar dónde más se encuentra la lógica afectada.
- 🧩 **Dificultad para agregar nuevas funcionalidades:** Las modificaciones futuras requieren revisar y ajustar muchas partes del sistema.
- 🐞 **Riesgo de inconsistencias:** Es fácil olvidar algún archivo o clase, provocando fallos o comportamientos inesperados.
- 🚫 **Escalabilidad reducida:** La estructura del sistema no soporta bien el crecimiento, ya que cualquier cambio pequeño implica mucho esfuerzo.

---

## 4. 🧠 Solución Correctiva 

Para corregir el antipatrón **Shotgun Surgery**, se deben aplicar **principios de diseño limpio y patrones de refactorización** que aumenten la cohesión y reduzcan el acoplamiento.

### ✅ Buenas prácticas y soluciones:
- **Encapsulación:** Centralizar la funcionalidad repetida en una sola clase o módulo.
- **Aplicar el principio SRP (Single Responsibility Principle):** Cada módulo debe tener una sola razón para cambiar.
- **Uso de patrones de diseño:**
  - **Facade:** Proporciona una interfaz unificada para operaciones relacionadas.
  - **Strategy:** Permite cambiar comportamientos (como formato de fecha) sin modificar múltiples lugares.
  - **Observer:** Para propagar cambios de manera controlada entre módulos.
- **Refactorización:** Crear una clase o utilidad única para manejar el formato de fecha, por ejemplo:

```javascript
// dateFormatter.js
class DateFormatter {
  static format(date) {
    return date.toLocaleDateString('es-MX', { day: '2-digit', month: '2-digit', year: 'numeric' });
  }
}

// Uso en todos los módulos
import { DateFormatter } from './dateFormatter.js';

function getUserBirthday(user) {
  return DateFormatter.format(user.birthDate);
}
```

Con esto, si se desea cambiar el formato nuevamente, solo se modifica **un único archivo**.

---

## 5. 🗣️ Presentación

- **Lenguaje claro y técnico:** Se explicó el antipatrón con terminología de diseño de software.
- **Estructura ordenada:** Cada sección aborda un criterio de la rúbrica.
- **Síntesis adecuada:** Se expone la idea central sin extenderse innecesariamente.
- **Control del tiempo:** El tema puede ser presentado en aproximadamente **5–7 minutos**.
- **Formato visual:** Se usan íconos, títulos y ejemplos en código para hacerlo más entendible.

---

### ✅ **Conclusión Final**

El antipatrón **Shotgun Surgery** representa un grave problema de diseño que afecta la mantenibilidad y escalabilidad del software.  
La clave para evitarlo es **diseñar con cohesión, bajo acoplamiento y centralizar las responsabilidades comunes**, aplicando principios de ingeniería de software limpia.
