## Datos del Alumno
#### Nombre: Emmanuel Isai Chavez Hernandez
#### No. Control: 23211005
#### Materia: Patrones de Diseño
#### Fecha: 30/09/2025

---

# Patrones Estructurales (GoF) Crear un adaptador con múltiples responsabilidades (Adapter)

Ejemplo de código en C# generado con ChatGPT con un Adapter mal implementado, con code smells y múltiples responsabilidades.
La idea es que sirva como “BadCode” para que después el estudiante lo refactorice aplicando el patrón Adapter correctamente y eliminando los problemas.

---

## Ejemplo de BadCode en C# (.NET 8)

```csharp
// Ejemplo con Code Smells intencionales
// Contexto: Queremos adaptar un sistema viejo de pagos a un nuevo estándar.
// El "Adapter" hace demasiado, rompe SRP y mezcla responsabilidades.

using System;
using System.Collections.Generic;

namespace PaymentSystem
{
    // Interfaz esperada por el cliente
    public interface INewPaymentProcessor
    {
        void Pay(decimal amount);
    }

    // Sistema antiguo de pagos
    public class OldPaymentSystem
    {
        public void ProcessPayment(string amount)
        {
            Console.WriteLine($"Procesando pago antiguo: {amount} USD");
        }
    }

    // Mal Adapter
    public class PaymentAdapter : INewPaymentProcessor
    {
        private OldPaymentSystem oldSystem = new OldPaymentSystem();

        // Code smell 1: Viola SRP, hace conversión, logging, validación y pago en un mismo método
        public void Pay(decimal amount)
        {
            Console.WriteLine(">>> [LOG] Intentando procesar pago..."); // Logging mezclado
            if (amount < 0)
            // validación que debería estar en otra capa
            {
                Console.WriteLine("Cantidad inválida, no se puede pagar.");
                return;
            }

            // Conversión mágica de decimal a string (mala práctica)
            string amountAsString = "$" + amount.ToString("F2");

            // Adaptador llama al sistema viejo directamente
            oldSystem.ProcessPayment(amountAsString);

            // Lógica extra innecesaria en el adaptador
            SaveTransactionHistory(amount);
            SendEmailNotification("Pago realizado con éxito: " + amountAsString);
        }

        // Métodos extras que no deberían estar en el Adapter
        private void SaveTransactionHistory(decimal amount)
        {
            Console.WriteLine($"Guardando historial en memoria... (pago de {amount})");
        }

        private void SendEmailNotification(string message)
        {
            Console.WriteLine($"Enviando correo: {message}");
        }
    }

    // Cliente
    public class Program
    {
        public static void Main()
        {
            INewPaymentProcessor processor = new PaymentAdapter();
            processor.Pay(150.75m);
        }
    }
}
```

---

## Code Smells detectados

1. **Violación del Principio de Responsabilidad Única (SRP)**
   El "PaymentAdapter" no solo adapta, también valida, guarda historial y envía correos.

2. **Conversión de tipos acoplada dentro del Adapter**
   Convertir "decimal → string" debería delegarse o encapsularse en otra clase.

3. **Falta de cohesión y acoplamiento fuerte**
   El Adapter se vuelve una clase “Dios” porque controla logging, persistencia y notificaciones.

4. **Métodos privados que no son parte del rol de Adapter**
   "SaveTransactionHistory" y "SendEmailNotification" no deberían existir ahí.

## Aplicación del patrón Adapter
El patrón Adapter se aplica para adaptar una interfaz antigua (OldPaymentSystem) a una interfaz nueva esperada por el cliente (INewPaymentProcessor).
En este caso, el sistema viejo procesa pagos con un string (e.g., "$100.00"), mientras que el nuevo sistema trabaja con decimal. 

## Refactor funcional
El código refactorizado es completamente funcional:
•	El adaptador funciona correctamente adaptando el viejo sistema de pagos.
•	Se puede usar tanto con el constructor simplificado como con inyección de dependencias personalizada.
•	También maneja correctamente casos de error como montos inválidos.

## Justificación técnica
El uso del patrón Adapter está justificado porque:
1.	Permite reutilizar código existente (el sistema viejo de pagos) sin modificarlo.
2.	Hace que el sistema viejo sea compatible con la nueva interfaz esperada.
3.	Permite una transición progresiva entre sistemas (de uno viejo a uno nuevo).


## Calidad del código refactorizado
1.	Código limpio y legible: Los nombres son descriptivos y el flujo es fácil de seguir.
2.	Reutilización y extensibilidad: Es fácil cambiar la implementación de alguna dependencia (por ejemplo, usar un logger diferente o guardar en base de datos real en vez de memoria).
3.	Pruebas y mantenimiento: Gracias a la inyección de dependencias y separación de responsabilidades, el código es fácil de testear y mantener.

---

## Código Corregido

```
// Nombre: Chavez Hernandez Emmanuel Isai
// No.Control: 23211005
// Fecha: 30/09/2025
// Materia Patrones de Diseño
// Practica Adapter

using System;
using System.Collections.Generic;

namespace PaymentSystem
{
    // Interfaz esperada por el cliente - se mantiene igual
    public interface INewPaymentProcessor
    {
        void Pay(decimal amount);
    }

    // Sistema antiguo de pagos - se mantiene igual
    public class OldPaymentSystem
    {
        public void ProcessPayment(string amount)
        {
            Console.WriteLine($"Procesando pago antiguo: {amount} USD");
        }
    }

    // Interfaz para el logger - Separación de responsabilidades
    public interface ILogger
    {
        void Log(string message);
    }

    // Implementación concreta del logger
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($">>> [LOG] {message}");
        }
    }

    // Interfaz para el servicio de notificaciones
    public interface INotificationService
    {
        void SendNotification(string message);
    }

    // Implementación concreta del servicio de notificaciones
    public class EmailNotificationService : INotificationService
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Enviando correo: {message}");
        }
    }

    // Interfaz para el repositorio de transacciones
    public interface ITransactionRepository
    {
        void SaveTransaction(decimal amount);
    }

    // Implementación concreta del repositorio
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        public void SaveTransaction(decimal amount)
        {
            Console.WriteLine($"Guardando historial en memoria... (pago de {amount})");
        }
    }

    // Servicio de validación - Responsabilidad única
    public class PaymentValidator
    {
        public bool IsValidAmount(decimal amount)
        {
            return amount >= 0;
        }
    }

    // Conversor de formato - Responsabilidad única
    public class AmountConverter
    {
        public string ConvertToLegacyFormat(decimal amount)
        {
            return "$" + amount.ToString("F2");
        }
    }

    // Adapter CORREGIDO - Ahora solo se encarga de la adaptación
    public class PaymentAdapter : INewPaymentProcessor
    {
        private readonly OldPaymentSystem _oldSystem;
        private readonly PaymentValidator _validator;
        private readonly AmountConverter _converter;
        private readonly ILogger _logger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly INotificationService _notificationService;

        // Inyección de dependencias - principio de inversión de dependencias
        public PaymentAdapter(
            OldPaymentSystem oldSystem,
            PaymentValidator validator,
            AmountConverter converter,
            ILogger logger,
            ITransactionRepository transactionRepository,
            INotificationService notificationService)
        {
            _oldSystem = oldSystem;
            _validator = validator;
            _converter = converter;
            _logger = logger;
            _transactionRepository = transactionRepository;
            _notificationService = notificationService;
        }

        // Constructor simplificado para casos básicos
        public PaymentAdapter()
        {
            _oldSystem = new OldPaymentSystem();
            _validator = new PaymentValidator();
            _converter = new AmountConverter();
            _logger = new ConsoleLogger();
            _transactionRepository = new InMemoryTransactionRepository();
            _notificationService = new EmailNotificationService();
        }

        // Método Pay ahora solo coordina las diferentes responsabilidades
        public void Pay(decimal amount)
        {
            _logger.Log("Intentando procesar pago...");
            
            // Validación delegada al validador especializado
            if (!_validator.IsValidAmount(amount))
            {
                Console.WriteLine("Cantidad inválida, no se puede pagar.");
                return;
            }

            try
            {
                // Conversión delegada al conversor especializado
                string amountAsString = _converter.ConvertToLegacyFormat(amount);
                
                // El adaptador solo hace lo que debe hacer: adaptar la llamada
                _oldSystem.ProcessPayment(amountAsString);
                
                // Las responsabilidades adicionales son delegadas
                _transactionRepository.SaveTransaction(amount);
                _notificationService.SendNotification($"Pago realizado con éxito: {amountAsString}");
                
                _logger.Log("Pago procesado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error procesando pago: {ex.Message}");
                throw;
            }
        }
    }

    // Cliente
    public class Program
    {
        public static void Main()
        {
            // Uso con constructor simplificado
            INewPaymentProcessor processor = new PaymentAdapter();
            processor.Pay(150.75m);

            Console.WriteLine("\n--- Usando inyección de dependencias ---\n");
            
            // Uso con inyección explícita de dependencias (más flexible y testeable)
            var oldSystem = new OldPaymentSystem();
            var validator = new PaymentValidator();
            var converter = new AmountConverter();
            var logger = new ConsoleLogger();
            var repository = new InMemoryTransactionRepository();
            var notificationService = new EmailNotificationService();

            INewPaymentProcessor customProcessor = new PaymentAdapter(
                oldSystem, validator, converter, logger, repository, notificationService);
            
            customProcessor.Pay(200.50m);
            
            // Test de caso inválido
            Console.WriteLine("\n--- Test caso inválido ---\n");
            customProcessor.Pay(-50.00m);
        }
    }
}

```


