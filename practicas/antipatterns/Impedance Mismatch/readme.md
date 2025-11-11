# 🧩 Antipatrón: Impedance Mismatch

**Alumno:** Diego Huerta Espinoza  
**No. Control:** 20212411  

## 📘 ¿Qué es Impedance Mismatch?

El antipatrón conocido como **Impedance Mismatch** describe el conflicto entre dos paradigmas fundamentales en el desarrollo de software:

- **Bases de datos relacionales**: organizan la información en tablas, filas, columnas, claves primarias y foráneas.
- **Lenguajes de programación orientados a objetos (OOP)**: modelan el mundo en clases, objetos, herencia, encapsulamiento y polimorfismo.

Este desajuste ocurre cuando el modelo de objetos no se traduce fácilmente al modelo relacional, lo que obliga a escribir código adicional para mapear, transformar y sincronizar datos entre ambos mundos.

---

## 🔍 ¿Por qué se considera una mala práctica?

Aunque es común en sistemas que usan ORMs (Object-Relational Mappers), el Impedance Mismatch se convierte en un antipatrón cuando:

- Se ignora el conflicto entre los modelos y se fuerza una integración sin abstraer adecuadamente.
- Se genera código repetitivo, frágil o difícil de mantener para adaptar objetos a tablas.
- Se rompe la encapsulación y se accede directamente a propiedades internas para cumplir con el modelo relacional.
- Se pierde flexibilidad al depender fuertemente de la estructura de la base de datos.

---

## 🧪 Ejemplo técnico

### 🧱 Modelo orientado a objetos (C#)

```csharp
class Empleado {
    public string Nombre { get; set; }
    public Direccion Direccion { get; set; }
}

class Direccion {
    public string Calle { get; set; }
    public string Ciudad { get; set; }
}
```

### 🗃️ Modelo relacional (SQL)

```sql
CREATE TABLE Empleados (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(100),
    Calle VARCHAR(100),
    Ciudad VARCHAR(100)
);
```

### ⚠️ Problema

La clase `Direccion` no tiene una tabla propia.

El ORM debe mapear manualmente los campos `Calle` y `Ciudad` dentro de `Empleado`.

Si se cambia la estructura de `Direccion`, hay que modificar el mapeo y posiblemente la tabla `Empleados`.

---

## 📉 Consecuencias del antipatrón

- 🔁 **Duplicación de lógica**: se repite código para transformar objetos en registros y viceversa.  
- 🧩 **Pérdida de encapsulamiento**: se accede directamente a propiedades internas para cumplir con el modelo relacional.  
- 🐛 **Errores de sincronización**: cambios en el modelo de objetos no se reflejan correctamente en la base de datos.  
- 🐌 **Rendimiento afectado**: consultas complejas y joins innecesarios por falta de alineación.  
- 🧱 **Dificultad para escalar**: agregar herencia, polimorfismo o relaciones complejas se vuelve costoso.  
- 🔒 **Acoplamiento fuerte**: el código depende directamente del esquema de la base de datos.  

---

## ✅ Soluciones correctivas

### 1. Usar un ORM moderno y bien configurado

Herramientas como **Entity Framework**, **Hibernate**, **Doctrine** o **Sequelize** permiten definir mapeos personalizados, relaciones complejas y estrategias de carga (lazy/eager loading).

### 2. Aplicar el patrón Repository

Este patrón desacopla el acceso a datos del modelo de negocio, permitiendo que las clases trabajen con interfaces abstractas en lugar de con SQL directo.

```csharp
interface IEmpleadoRepository {
    Empleado ObtenerPorId(int id);
    void Guardar(Empleado empleado);
}
```

### 3. Diseñar DTOs (Data Transfer Objects)

Los DTOs permiten adaptar los datos a estructuras más simples para persistencia, sin comprometer el modelo de dominio.

```csharp
class EmpleadoDTO {
    public string Nombre { get; set; }
    public string Calle { get; set; }
    public string Ciudad { get; set; }
}
```

### 4. Evitar herencia profunda en el modelo de objetos

Las jerarquías complejas son difíciles de mapear en SQL. Si se requiere herencia, usar estrategias como **Table per Hierarchy (TPH)** o **Table per Type (TPT)** según el ORM.

### 5. Considerar bases de datos orientadas a objetos o NoSQL

Si el modelo de dominio es altamente anidado o dinámico, bases como **MongoDB**, **Couchbase** o **Firebase** pueden reducir el desajuste.

---

## 🧠 Conclusión

El antipatrón **Impedance Mismatch** representa una fricción entre dos mundos: el relacional y el orientado a objetos.  
Reconocerlo y aplicar patrones como **Repository**, **DTOs** y **ORMs** bien configurados permite construir sistemas más limpios, escalables y mantenibles.

Evitar este antipatrón no significa abandonar las bases de datos relacionales, sino diseñar con conciencia de sus limitaciones y complementar con buenas prácticas de arquitectura y diseño.
