# Refactorización de Patrones Estructurales (GoF) – Facade
## Evitar incluir lógica de negocio compleja dentro del Facade

---

## 1. Identificación de Code Smells

- **Violación del Principio de Responsabilidad Única (SRP)**  
  El *Facade* mezclaba orquestación (llamadas a subsistemas) con reglas de negocio (descuentos, impuestos, validaciones).  

- **Acoplamiento rígido (violación OCP)**  
  Si cambian las reglas de negocio, es necesario modificar el *Facade*, rompiendo el principio de Abierto/Cerrado.  

- **Dificultad para testear**  
  El *Facade* no puede probarse de forma aislada porque contiene reglas de negocio críticas mezcladas con infraestructura.  

---

## 2. Aplicación del patrón adecuado

El patrón **Facade** se usó correctamente para simplificar la interacción con subsistemas,  
pero estaba mal implementado al incluir lógica de negocio.  

La solución aplicada fue:  
- Extraer las reglas a **servicios especializados** (`IPricingService`, `IInventoryService`, etc.).  
- Dejar al *Facade* únicamente como **orquestador** de llamadas.  

---

## 3. Refactor funcional (parcial o total)

- Se extrajo la lógica de precios y validaciones a un `PricingService`.  
- Se crearon interfaces para permitir inyección de dependencias (`IInventoryService`, `IPaymentService`, `INotificationService`).  
- El *Facade* ahora solo coordina, sin contener reglas de negocio.  

El código compila, funciona y mantiene el mismo flujo de compra.  
La lógica de negocio quedó desacoplada y centralizada en servicios de dominio.  

---

## 4. Justificación técnica en Pull Request

**Problema detectado:**  
El *Facade* contenía lógica de negocio compleja (descuentos, IVA, validaciones), mezclando responsabilidades y generando acoplamiento.  

**Patrón aplicado:**  
Se mantuvo el **Facade**, pero refactorizado a un *Facade delgado* que delega la lógica a servicios de negocio.  

**Beneficios obtenidos:**  
- Mayor mantenibilidad y extensibilidad.  
- Reglas de negocio probadas de forma independiente.  
- Cumplimiento con SRP y OCP.  
- Mejor cohesión y menor acoplamiento.  

---

## 5. Calidad del código refactorizado

- **Legibilidad:** nombres claros y consistentes.  
- **Separación de responsabilidades:** el *Facade* solo orquesta; servicios encapsulan la lógica.  
- **Coherencia:** cada clase cumple un rol específico.  
- **Uso idiomático en C# / .NET 8:** interfaces, excepciones claras, inyección de dependencias.  

---

## 6. Código comparativo

### ❌ Versión inicial (Facade con lógica de negocio mal ubicada)
```csharp
public class ECommerceFacade
{
    private readonly PaymentGateway _payment = new PaymentGateway();
    private readonly EmailSender _email = new EmailSender();
    private readonly InventoryApi _inventory = new InventoryApi();

    public string PlaceOrder(int productId, int quantity, string customerEmail)
    {
        if (quantity <= 0) throw new ArgumentException("Cantidad inválida");

        var stock = _inventory.GetStock(productId);
        if (stock < quantity) return "Stock insuficiente";

        // Lógica de negocio dentro del Facade ❌
        decimal unitPrice = _inventory.GetUnitPrice(productId);
        decimal subtotal = unitPrice * quantity;
        if (quantity >= 5) subtotal *= 0.90m; // descuento
        decimal tax = subtotal * 0.16m;       // IVA
        decimal total = subtotal + tax;

        if (!_payment.Charge(total)) return "Pago rechazado";

        _inventory.Decrease(productId, quantity);
        _email.Send(customerEmail, $"Tu compra fue aprobada por {total:C2}");

        return "Orden creada";
    }
}
```
### ✅ Refactor con servicios de dominio
```csharp
public class ECommerceFacade
{
    private readonly IInventoryService _inventory;
    private readonly IPricingService _pricing;
    private readonly IPaymentService _payment;
    private readonly INotificationService _notify;

    public ECommerceFacade(
        IInventoryService inventory,
        IPricingService pricing,
        IPaymentService payment,
        INotificationService notify)
    {
        _inventory = inventory;
        _pricing = pricing;
        _payment = payment;
        _notify = notify;
    }

    public string PlaceOrder(int productId, int quantity, string customerEmail)
    {
        if (quantity <= 0) throw new ArgumentException("Cantidad inválida");

        int stock = _inventory.GetStock(productId);
        if (stock < quantity) return "Stock insuficiente";

        var (subtotal, tax, total) = _pricing.Calculate(productId, quantity, _inventory.GetUnitPrice(productId));

        if (!_payment.Charge(total)) return "Pago rechazado";

        _inventory.Decrease(productId, quantity);
        _notify.Send(customerEmail, $"Compra aprobada. Subtotal: {subtotal:C2}, IVA: {tax:C2}, Total: {total:C2}");

        return "Orden creada";
    }
}
```
## 7. Conclusión

El refactor permitió que el patrón Facade cumpla su propósito original:
- simplificar la interacción con subsistemas, sin contener lógica de negocio.

Esto mejora la mantenibilidad, extensibilidad, testabilidad y cohesión del sistema.
