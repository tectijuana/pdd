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
# 🧠 Antipatrón: Massive Stored Procedures

## 📘 Comprensión del Antipatrón 

**Massive Stored Procedures** (Procedimientos Almacenados Masivos) es un antipatrón que ocurre cuando se concentran demasiadas reglas de negocio y lógica de aplicación dentro de procedimientos almacenados en la base de datos.  
Aunque los procedimientos almacenados pueden mejorar el rendimiento para operaciones específicas, abusar de ellos genera una arquitectura rígida y difícil de mantener.

Se considera una **mala práctica** porque:
- Mezcla la lógica de negocio con la lógica de datos.
- Dificulta el versionamiento y las pruebas automatizadas.
- Hace que los cambios deban realizarse directamente en el servidor de base de datos.
- Rompe la separación de responsabilidades entre la base de datos y la capa de aplicación.

---

## 💻 Ejemplo Técnico 

```sql
-- Ejemplo de un procedimiento almacenado masivo
CREATE PROCEDURE ProcesarPedido
    @IdPedido INT
AS
BEGIN
    -- 1. Validar el pedido
    DECLARE @Estado VARCHAR(20)
    SELECT @Estado = Estado FROM Pedidos WHERE Id = @IdPedido

    IF @Estado <> 'Pendiente'
        RETURN

    -- 2. Calcular totales
    DECLARE @Total DECIMAL(10,2)
    SELECT @Total = SUM(Precio * Cantidad)
    FROM DetallesPedido WHERE IdPedido = @IdPedido

    -- 3. Actualizar inventario
    UPDATE Productos
    SET Stock = Stock - dp.Cantidad
    FROM Productos p
    JOIN DetallesPedido dp ON p.Id = dp.IdProducto
    WHERE dp.IdPedido = @IdPedido

    -- 4. Generar factura
    INSERT INTO Facturas (IdPedido, Total, Fecha)
    VALUES (@IdPedido, @Total, GETDATE())

    -- 5. Enviar notificación (mala práctica dentro del SP)
    EXEC msdb.dbo.sp_send_dbmail
        @profile_name = 'Notificaciones',
        @recipients = 'ventas@empresa.com',
        @subject = 'Nuevo Pedido Procesado',
        @body = 'El pedido ha sido procesado exitosamente.'
END
```

🔴 Este procedimiento mezcla **validaciones, cálculos, actualizaciones, generación de facturas y notificaciones** — todo en un solo bloque de código.

---

## ⚠️ Consecuencias

| Problema | Descripción |
|-----------|-------------|
| 💥 Mantenibilidad | Cualquier cambio en la lógica requiere modificar el SP directamente en la base de datos. |
| 🧩 Escalabilidad | Dificulta migraciones o distribución de la lógica hacia servicios o microservicios. |
| 🧪 Testeo | No se pueden aplicar pruebas unitarias o de integración fácilmente. |
| 🚫 Reutilización | La lógica encerrada en el SP no puede ser reutilizada por otras aplicaciones. |
| 🔒 Dependencia | La aplicación queda acoplada a un único motor de base de datos. |

---

## ✅ Solución Correctiva

### Buenas prácticas recomendadas:
1. **Separar la lógica de negocio**: mantener la lógica compleja en el código de la aplicación, no en la base de datos.  
2. **Usar ORM (Object Relational Mapping)** como Entity Framework, Hibernate o SQLAlchemy.  
3. **Adoptar arquitectura en capas o microservicios** para dividir responsabilidades.  
4. **Utilizar stored procedures pequeños y específicos**, solo para operaciones críticas de rendimiento.  
5. **Implementar patrones como Repository o Unit of Work** para gestionar acceso a datos de forma limpia y escalable.

🟢 Ejemplo refactorizado con C# y Entity Framework:

```csharp
public class PedidoService
{
    private readonly AppDbContext _context;

    public PedidoService(AppDbContext context)
    {
        _context = context;
    }

    public void ProcesarPedido(int idPedido)
    {
        var pedido = _context.Pedidos.Include(p => p.Detalles).FirstOrDefault(p => p.Id == idPedido);
        if (pedido == null || pedido.Estado != "Pendiente") return;

        decimal total = pedido.Detalles.Sum(d => d.Precio * d.Cantidad);

        foreach (var detalle in pedido.Detalles)
        {
            detalle.Producto.Stock -= detalle.Cantidad;
        }

        _context.Facturas.Add(new Factura { IdPedido = idPedido, Total = total, Fecha = DateTime.Now });
        pedido.Estado = "Procesado";
        _context.SaveChanges();

        EmailService.Enviar("ventas@empresa.com", "Nuevo Pedido Procesado", "El pedido ha sido procesado exitosamente.");
    }
}
```

---

