# Refactor Problema #5 — Strategy (GoF)
**Alumno(a):** Jocelin Maribel Bernal Enciso  
**Número de lista:** 5  
**Materia:** Patrones de diseño

---

## 1) Enunciado
En un flujo de pagos, el código pregunta varias veces por el tipo de tarjeta (`VISA`, `MASTERCARD`, `AMEX`) utilizando condicionales `if`/`switch`, lo que genera un código inflexible y difícil de mantener.

---

## 2) Code Smell identificado
- Uso excesivo de condicionales (`if`, `else if`, `switch`) para determinar el comportamiento según el tipo de tarjeta.
- Violación del Principio Abierto/Cerrado (OCP): agregar una nueva tarjeta requiere modificar el código existente.
- Alta complejidad ciclomática
- Bajo nivel de cohesión en la clase `ProcesadorPagos`.

---

## 3) Patrón elegido: Strategy (GoF)

### **Justificación:**
El patrón Strategy permite encapsular algoritmos o comportamientos que pueden intercambiarse en tiempo de ejecución. En este caso, cada tipo de tarjeta implementa su propia estrategia de pago, eliminando condicionales y facilitando la extensión del sistema.

### **Beneficios obtenidos:**
- Elimina condicionales por tipo de tarjeta.  
- Facilita la extensión del sistema (cumple con OCP).  
- Mejora la mantenibilidad y testabilidad.  
- Reduce el acoplamiento y la complejidad del código.

---

## 4) Código refactorizado
```csharp
using System;
using System.Collections.Generic;
using System.Globalization;

public class Payment
{
    public string CardType { get; }
    public string CardNumber { get; }
    public decimal Amount { get; }
    public string Currency { get; }

    public Payment(string cardType, string cardNumber, decimal amount, string currency = "USD")
    {
        CardType = cardType?.Trim().ToUpperInvariant() ?? "";
        CardNumber = cardNumber ?? "";
        Amount = amount;
        Currency = currency ?? "USD";
    }

    public string MaskedCard()
    {
        if (string.IsNullOrWhiteSpace(CardNumber) || CardNumber.Length < 4) return "****";
        var last4 = CardNumber.Substring(CardNumber.Length - 4);
        return $"**** **** **** {last4}";
    }
}

public class PaymentResult
{
    public bool Success { get; }
    public string Message { get; }
    public string AuthCode { get; }

    public PaymentResult(bool success, string message, string authCode = "")
    {
        Success = success;
        Message = message;
        AuthCode = authCode;
    }

    public override string ToString() => $"{(Success ? "OK" : "ERR")} | {Message} {(string.IsNullOrEmpty(AuthCode) ? "" : $"(Auth:{AuthCode})")}";
}

public interface IPaymentStrategy
{
    string Name { get; }
    PaymentResult Process(Payment payment);
}

public class VisaPayment : IPaymentStrategy
{
    public string Name => "VISA";
    public PaymentResult Process(Payment p)
    {
        if (p.Amount <= 0) return new PaymentResult(false, "Monto inválido");
        return new PaymentResult(true, $"VISA approved {p.Amount.ToString("C", CultureInfo.InvariantCulture)} to {p.MaskedCard()}", "V-12345");
    }
}

public class MastercardPayment : IPaymentStrategy
{
    public string Name => "MASTERCARD";
    public PaymentResult Process(Payment p)
    {
        if (p.Amount <= 0) return new PaymentResult(false, "Monto inválido");
        return new PaymentResult(true, $"MC approved {p.Amount.ToString("C", CultureInfo.InvariantCulture)} to {p.MaskedCard()}", "M-67890");
    }
}

public class AmexPayment : IPaymentStrategy
{
    public string Name => "AMEX";
    public PaymentResult Process(Payment p)
    {
        if (p.Amount <= 0) return new PaymentResult(false, "Monto inválido");
        var fee = 1.25m;
        var total = p.Amount + fee;
        return new PaymentResult(true, $"AMEX approved {total.ToString("C", CultureInfo.InvariantCulture)} (incl. fee) to {p.MaskedCard()}", "A-24680");
    }
}

public class PaymentProcessor
{
    private readonly Dictionary<string, IPaymentStrategy> _strategies = new Dictionary<string, IPaymentStrategy>();

    public PaymentProcessor Register(IPaymentStrategy strategy)
    {
        if (strategy == null) throw new ArgumentNullException(nameof(strategy));
        _strategies[strategy.Name.ToUpperInvariant()] = strategy;
        return this;
    }

    public PaymentResult Process(Payment p)
    {
        if (p == null) return new PaymentResult(false, "Pago nulo");
        if (!_strategies.TryGetValue(p.CardType, out var strategy))
        {
            return new PaymentResult(false, $"Tipo de tarjeta no soportado: '{p.CardType}'");
        }
        return strategy.Process(p);
    }
}

public class Program
{
    public static void Main()
    {
        var processor = new PaymentProcessor()
            .Register(new VisaPayment())
            .Register(new MastercardPayment())
            .Register(new AmexPayment());

        var casos = new List<Payment>
        {
            new Payment("VISA", "4111111111111111", 100.00m),
            new Payment("MASTERCARD", "5555555555554444", 50.50m),
            new Payment("AMEX", "371449635398431", 25.00m),
            new Payment("NARA", "0000000000000000", 10.00m)
        };

        foreach (var p in casos)
        {
            var result = processor.Process(p);
            Console.WriteLine($"{p.CardType,-12} -> {result}");
        }
    }
}
```

---

## 5) Resultados esperados
| Caso | Entrada | Resultado esperado |
|------|----------|--------------------|
| VISA | 100.00 | VISA approved, Auth V-12345 |
| MASTERCARD | 50.50 | MC approved, Auth M-67890 |
| AMEX | 25.00 | AMEX approved (incl. fee), Auth A-24680 |
| NARA | 10.00 | Tipo no soportado |
| VISA | 0 | Error: monto inválido |

**Ejecución:** 
<img width="1726" height="950" alt="image" src="https://github.com/user-attachments/assets/e15a7920-70b5-4757-9c4c-1b9ffd2395e0" />

---

## 6) Extensión (Principio OCP)
Para agregar una nueva tarjeta (por ejemplo, CARNET):
```csharp
public class CarnetPayment : IPaymentStrategy
{
    public string Name => "CARNET";
    public PaymentResult Process(Payment p)
    {
        if (p.Amount <= 0) return new PaymentResult(false, "Monto inválido");
        return new PaymentResult(true, $"CARNET approved {p.Amount:C} to {p.MaskedCard()}", "C-11223");
    }
}
```
Solo se necesita registrar la nueva estrategia:
```csharp
processor.Register(new CarnetPayment());
```

---

## 7) Diagrama del patrón Strategy (ASCII)
```
+------------------+           +-----------------------+
| PaymentProcessor |--(map)--> | IPaymentStrategy     |
+------------------+           +-----------------------+
         |                     ^           ^           ^
         |                     |           |           |
         v                     |           |           |
    Process(Payment)     +-----------+ +-----------+ +-----------+
                         | VisaPayment| | McPayment | | AmexPayment|
                         +-----------+ +-----------+ +-----------+
```

---

## 8) Conclusión
El uso del patrón Strategy permitió eliminar las estructuras condicionales por tipo de tarjeta, mejorando la extensibilidad del sistema y cumpliendo con el principio de diseño abierto/cerrado. Además, el código se volvió más modular, legible y fácil de mantener, permitiendo agregar nuevas estrategias sin alterar la lógica central del procesador de pagos.

---


