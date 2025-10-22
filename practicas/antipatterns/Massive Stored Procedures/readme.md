# 🧠 Antipatrón: Massive Stored Procedures  
**Tema:** Bases de Datos y Persistencia  
**Alumno:** Joshua Isaías Ruiz López  
**Materia:** Diseño de Software  

---

## 🧩 1. Comprensión del Antipatrón (10 pts)

**Massive Stored Procedures** (Procedimientos almacenados masivos) se refiere a la práctica de colocar **demasiada lógica de negocio dentro de procedimientos almacenados en la base de datos**, en lugar de mantener esa lógica en el código de la aplicación.  

Esto ocurre cuando un equipo de desarrollo traslada la mayor parte del procesamiento —validaciones, reglas de negocio, control de flujo, cálculos complejos— a procedimientos SQL, dejando la aplicación como una simple interfaz.  

🔴 **Por qué es una mala práctica:**
- Rompe la **separación de responsabilidades** entre la capa de datos y la de negocio.  
- Hace que el código sea **difícil de mantener, probar y versionar**.  
- Reduce la **portabilidad** entre distintos motores de base de datos.  
- Genera **dependencia excesiva** de los DBAs o del motor SQL en uso.  

---

## 💻 2. Ejemplo Técnico (10 pts)

### ❌ Ejemplo de un antipatrón “Massive Stored Procedure”

```sql
CREATE PROCEDURE ProcesarVenta
    @ClienteId INT,
    @ProductoId INT,
    @Cantidad INT,
    @Descuento DECIMAL(10,2)
AS
BEGIN
    DECLARE @Stock INT, @Precio DECIMAL(10,2), @Total DECIMAL(10,2)

    SELECT @Stock = Stock, @Precio = Precio FROM Productos WHERE Id = @ProductoId

    IF @Stock < @Cantidad
    BEGIN
        RAISERROR('No hay suficiente stock', 16, 1)
        RETURN
    END

    SET @Total = (@Precio * @Cantidad) - @Descuento

    INSERT INTO Ventas(ClienteId, ProductoId, Cantidad, Total, Fecha)
    VALUES(@ClienteId, @ProductoId, @Cantidad, @Total, GETDATE())

    UPDATE Productos SET Stock = Stock - @Cantidad WHERE Id = @ProductoId

    IF @Total > 10000
    BEGIN
        INSERT INTO LogVentasAltas(ClienteId, Total, Fecha)
        VALUES(@ClienteId, @Total, GETDATE())
    END
```
## ⚠️ 3. Consecuencias (10 pts)

| Problema | Descripción |
|-----------|--------------|
| **Mantenimiento difícil** | Cualquier cambio en las reglas requiere editar código SQL largo y poco legible. |
| **Falta de versionamiento** | No siempre se tiene control de versiones de los procedimientos en repositorios. |
| **Pérdida de portabilidad** | Cambiar de motor (SQL Server, MySQL, Oracle) es costoso por diferencias de sintaxis. |
| **Riesgo de errores** | Al mezclar demasiadas responsabilidades, una pequeña modificación puede romper otras partes. |
| **Dificultad en pruebas unitarias** | No se pueden probar fácilmente los fragmentos de lógica sin ejecutar toda la base de datos. |

---

## 🛠️ 4. Solución Correctiva (10 pts)

✅ **Buenas prácticas para evitar el antipatrón:**

1. **Mover la lógica de negocio a la capa de aplicación**  
   Las reglas de negocio deben implementarse en un lenguaje de programación (C#, Java, Python, etc.) dentro de servicios o clases específicas.  
   El procedimiento almacenado debe limitarse a tareas simples de CRUD (Create, Read, Update, Delete).

2. **Aplicar el principio de “Single Responsibility” (SOLID)**  
   Cada procedimiento debe tener **una sola responsabilidad**: por ejemplo, insertar una venta o actualizar stock, pero no ambos.

3. **Uso moderado de procedimientos almacenados**  
   Utilizarlos solo para operaciones **críticas de rendimiento o agregaciones complejas** que realmente lo requieran.

4. **Versionar los scripts SQL**  
   Mantener los procedimientos almacenados en el mismo repositorio de código para control de versiones y despliegues automatizados.

5. **Adoptar una arquitectura limpia (Clean Architecture)**  
   La base de datos debe ser un **detalle de implementación**, no el lugar donde vive la lógica de negocio.

---

### ✅ Ejemplo corregido (en C# + SQL)

**Procedimiento almacenado más simple:**

```sql
CREATE PROCEDURE InsertarVenta
    @ClienteId INT,
    @ProductoId INT,
    @Cantidad INT,
    @Total DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Ventas(ClienteId, ProductoId, Cantidad, Total, Fecha)
    VALUES(@ClienteId, @ProductoId, @Cantidad, @Total, GETDATE())
END

```
### Lógica en la aplicación (C#)
```
decimal precio = producto.Precio;
decimal total = (precio * cantidad) - descuento;

if (producto.Stock < cantidad)
    throw new Exception("No hay suficiente stock.");

if (total > 10000)
    RegistrarVentaAlta(clienteId, total);

RepositorioVentas.InsertarVenta(clienteId, productoId, cantidad, total);
RepositorioProductos.ActualizarStock(productoId, cantidad);
```
### 🎯 5. Conclusión
El antipatrón Massive Stored Procedures surge por una mala distribución de responsabilidades entre la aplicación y la base de datos.
Aunque puede parecer conveniente al principio, termina generando un sistema rígido, acoplado y difícil de escalar.

La solución es mantener la lógica de negocio fuera de la base de datos, aplicar principios SOLID y usar procedimientos almacenados solo cuando realmente aporten valor al rendimiento o la seguridad.
