# Refactorización de Patrones Estructurales (GoF) - Adapter / Inyectar lógica de negocio en el adaptador

## Código base mal estructurado (antes del refactor)

```csharp
// Adaptador con lógica de negocio mal ubicada
public class CustomerAdapter
{
    public string GetCustomerInfo(int customerId)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException("Invalid Customer Id");
        }

        // Lógica de negocio directamente en el adaptador
        var customerData = $"Customer data for ID {customerId}";
        var customerStatus = customerId % 2 == 0 ? "Active" : "Inactive";

        return $"{customerData} - Status: {customerStatus}";
    }
}
# Refactor con patrón Adapter e inyección de dependencias

```csharp
// Interfaz de la lógica de negocio
public interface ICustomerService
{
    string GetCustomerInfo(int customerId);
}

// Implementación de la lógica de negocio
public class CustomerService : ICustomerService
{
    public string GetCustomerInfo(int customerId)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException("Invalid Customer Id");
        }

        var customerData = $"Customer data for ID {customerId}";
        var customerStatus = customerId % 2 == 0 ? "Active" : "Inactive";

        return $"{customerData} - Status: {customerStatus}";
    }
}
```
# Adaptador que inyecta la lógica de negocio 
```
public class CustomerAdapter
{
    private readonly ICustomerService _customerService;

    public CustomerAdapter(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public string GetCustomerInfo(int customerId)
    {
        // Solo llama a la lógica de negocio ya separada
        return _customerService.GetCustomerInfo(customerId);
    }
}
```
# Uso del adaptador con lógica de negocio inyectada

```csharp
class Program
{
    static void Main()
    {
        // Crear instancia del servicio de lógica de negocio
        ICustomerService customerService = new CustomerService();

        // Inyectar el servicio en el adaptador
        var adapter = new CustomerAdapter(customerService);

        // Usar el adaptador para obtener la información del cliente
        Console.WriteLine(adapter.GetCustomerInfo(10)); // Output: Customer data for ID 10 - Status: Active
        Console.WriteLine(adapter.GetCustomerInfo(3));  // Output: Customer data for ID 3 - Status: Inactive
    }
}
```
# Justificación para el Pull Request

### Problemas detectados

- El adaptador `CustomerAdapter` tenía lógica de negocio embebida, lo que viola el principio de responsabilidad única.
- Dificultad para testear y mantener la lógica de negocio por estar acoplada al adaptador.
- Falta de inyección de dependencias, lo que genera acoplamiento fuerte y dificulta la extensibilidad.

### Solución aplicada

- Se extrajo la lógica de negocio a la interfaz `ICustomerService` y su implementación `CustomerService`.
- El adaptador ahora solo delega la llamada a la lógica de negocio inyectada.
- Esta estructura cumple con el patrón Adapter, separando claramente responsabilidades y promoviendo la modularidad.

### Beneficios

- Mejora la mantenibilidad y testabilidad del código.
- Facilita la extensión futura del servicio sin afectar el adaptador.
- Permite reutilización del servicio de negocio en otros contextos y facilita la integración con otros componentes.





