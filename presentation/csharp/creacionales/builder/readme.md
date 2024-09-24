### **Título**
- **Título**: "Patrón de Diseño: Builder"
- **Subtítulo**: Una solución flexible para la creación de objetos complejos.
- **David Páez Beltrán**.

![image](https://github.com/user-attachments/assets/832256f4-7276-4402-9383-f52a1c4bbb87)

---

### **Introducción**
- **¿Qué son los Patrones de Diseño?**
  - Definición: Son soluciones repetibles a problemas comunes en el diseño de software.
  - Clasificación: Creacionales, estructurales y de comportamiento.
  - El patrón Builder pertenece a los patrones **creacionales**.

---

### **¿Qué es el Patrón Builder?**
- **Definición**:
  - Un patrón que permite crear objetos complejos paso a paso.
  - Separa la construcción de un objeto de su representación final.
- **Cuando usarlo**: Cuando el proceso de construcción de un objeto es complejo o involucra muchas configuraciones.

![image](https://github.com/user-attachments/assets/cf151f4b-05dd-4fe4-9c8f-6ddfb15c8b1b)

---

### **Problema que Soluciona**
- **¿Cuál es el problema?**
  - Si tenemos un constructor con muchos parámetros, puede ser difícil leer y entender.
  - Los objetos con múltiples configuraciones o propiedades opcionales pueden ser complicados de instanciar correctamente.
  
---

### **Componentes del Patrón Builder**
- **Componentes principales**:
  - **Builder**: Interface que define los pasos para construir un objeto.
  - **ConcreteBuilder**: Implementa la interfaz Builder y construye partes específicas del objeto.
  - **Director**: Dirige el proceso de construcción utilizando el Builder.
  - **Producto**: El objeto que es construido.

![image](https://github.com/user-attachments/assets/7b5a5ab5-b845-460d-b5ee-3390e24e92b0)

---

### **Diagrama UML**
- **Diagrama UML del Patrón Builder**:
  - Muestra la relación entre Builder, Director, ConcreteBuilder y Producto.
  - Explica cómo cada parte interactúa en la construcción del objeto.

---

### **Flujo de Trabajo del Patrón**
- **Pasos**:
  1. El Director utiliza el Builder para construir partes del producto.
  2. El Builder ensambla las partes según las indicaciones del Director.
  3. El Builder finalmente entrega el objeto completo.

![image](https://github.com/user-attachments/assets/3508dd69-3701-43b0-ba28-046620ebf84d)

---

### **Ventajas del Patrón Builder**
- **Beneficios**:
  - **Flexibilidad**: Permite crear variaciones del objeto con pasos diferentes.
  - **Legibilidad**: Mejora la claridad del código comparado con el uso de constructores largos.
  - **Modularidad**: Facilita la creación de objetos paso a paso.
  
---

### **Ejemplo en Código (C#)**
- **Ejemplo básico**:
  ```csharp
  // Producto
  class Car {
      public string Engine { get; set; }
      public string Seats { get; set; }
  }

  // Builder
  interface ICarBuilder {
      void BuildEngine();
      void BuildSeats();
      Car GetCar();
  }

  // ConcreteBuilder
  class SportsCarBuilder : ICarBuilder {
      private Car car = new Car();
      public void BuildEngine() { car.Engine = "V8"; }
      public void BuildSeats() { car.Seats = "Leather"; }
      public Car GetCar() { return car; }
  }

  // Director
  class Director {
      public void Construct(ICarBuilder builder) {
          builder.BuildEngine();
          builder.BuildSeats();
      }
  }
  ```

---

### **Aplicaciones del Patrón Builder**
- **Ámbitos de uso**:
  - Creación de objetos con múltiples configuraciones como **interfaces gráficas**.
  - Construcción de objetos complejos en el **diseño de juegos**.
  - Utilizado en **procesos de generación de documentos** o **archivos**.

  ![image](https://github.com/user-attachments/assets/669dc2ee-eacd-4f73-bac9-9cfaa4587bb2)

---

### **Desventajas del Patrón Builder**
- **Complejidad**: Introduce más clases y estructuras al diseño.
- **Sobrecarga**: Puede ser excesivo si el objeto a crear es simple.

---

### **Conclusión**
- **Resumen**:
  - El patrón Builder es una excelente opción para la creación de objetos complejos.
  - Mejora la claridad y la flexibilidad en la creación de objetos.
  - Es uno de los patrones más usados en situaciones donde se requiere una gran personalización.

---

