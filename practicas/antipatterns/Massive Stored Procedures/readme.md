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

