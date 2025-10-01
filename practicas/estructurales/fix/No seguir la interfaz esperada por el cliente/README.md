# Actividad: Refactorización de Patrones Estructurales (GoF)

## 🧑‍🎓 Datos del estudiante

- **Nombre:** Jesus Enrique Barboza Noriega
- **Número de control:** 21211913 

## ❌ Código sin refactorizar

En este escenario, el sistema espera que todos los teléfonos sigan la interfaz `IPhone` 
con métodos para **llamadas** y **mensajes** usando el estándar **E.164** (`+52 664 1234567`). 
Sin embargo, el cliente usa directamente una clase `OldPhone` con otra firma incompatible.

```csharp
// Interfaz esperada por el cliente (NO se respeta en este ejemplo)
public interface IPhone
{
    void Call(string e164Number);                 // Ej: "+52 664 1234567"
    void SendMessage(string e164Number, string text);
}

// Librería legacy (no la controlamos, tiene otra interfaz)
public sealed class OldPhone
{
    public void Dial(int areaCode, int localNumber)
        => System.Console.WriteLine($"[OldPhone] Dial {areaCode}-{localNumber}");

    public void Sms(string toLocal, string text)
        => System.Console.WriteLine($"[OldPhone] SMS to {toLocal}: {text}");
}

// 🚨 MAL: el cliente NO usa IPhone, depende directo de OldPhone
public sealed class PhoneClientService
{
    private readonly OldPhone _phone = new OldPhone();

    public void NotifyUser(string e164)
    {
        // Conversión manual y frágil del número E.164
        var parts = e164.Replace("+", "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var area = int.Parse(parts[1]);                 // Ej: "664"
        var local = int.Parse(parts[2]);                // Ej: "1234567"

        // Llamadas con firma incompatible
        _phone.Dial(area, local);
        _phone.Sms(parts[2], "Hola, tu pedido va en camino.");
    }
}
```

---

## 🕵️ Identificación de Code Smells

Se detectan **3 problemas estructurales** relacionados con patrones GoF:

1. **Incompatibilidad de Interfaces (Target vs Adaptee)**  
   El sistema espera `IPhone.Call(string)` y `IPhone.SendMessage(string, string)`, pero `OldPhone` expone `Dial(int, int)` y `Sms(string, string)`.  
   → _Este caso es típico de necesidad de **Adapter**._

2. **Acoplamiento fuerte a una clase concreta**  
   `PhoneClientService` depende directamente de `OldPhone`, no de la interfaz `IPhone`.  
   → Viola el principio de inversión de dependencias (DIP).

3. **Duplicación de lógica de conversión**  
   La conversión de E.164 → `area/local` se repite y vive en el cliente.  
   → Falta de cohesión, se mezcla lógica de dominio con lógica de parsing.

---

## 🛠️ Aplicación del patrón adecuado

El patrón **Adapter** es el más apropiado porque:  

- Permite que `OldPhone` (Adaptee) se use sin modificarlo.  
- Traduce la interfaz esperada (`IPhone`) hacia la interfaz real (`OldPhone`).  
- Encapsula la lógica de conversión, evitando que se propague por el sistema.  

Otros patrones estructurales como **Bridge** o **Composite** no resuelven la incompatibilidad de **firmas** de forma directa.  

---

## 💡 Código refactorizado con Adapter

```csharp
// 🎯 Interfaz esperada (Target)
public interface IPhone
{
    void Call(string e164Number);                 // Ej: "+52 664 1234567"
    void SendMessage(string e164Number, string text);
}

// 📞 Clase legacy (Adaptee) que no podemos modificar
public sealed class OldPhone
{
    public void Dial(int areaCode, int localNumber)
        => System.Console.WriteLine($"[OldPhone] Dial {areaCode}-{localNumber}");

    public void Sms(string toLocal, string text)
        => System.Console.WriteLine($"[OldPhone] SMS to {toLocal}: {text}");
}

// 🔌 Adapter: traduce de la interfaz esperada (IPhone) a la interfaz real (OldPhone)
public sealed class OldPhoneAdapter : IPhone
{
    private readonly OldPhone _adaptee = new OldPhone();

    // Adapter implementa la interfaz esperada
    public void Call(string e164Number)
    {
        var (area, local) = ParseE164(e164Number);
        // Traducción: Call(string) → Dial(int, int)
        _adaptee.Dial(area, local);
    }

    public void SendMessage(string e164Number, string text)
    {
        var (_, local) = ParseE164(e164Number);
        // Traducción: SendMessage(string, string) → Sms(string, string)
        _adaptee.Sms(local.ToString(), text);
    }

    // Método privado: parsing del número E.164 encapsulado en el Adapter
    private static (int area, int local) ParseE164(string e164)
    {
        var parts = e164.Replace("+", "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
        // parts[0] = country, parts[1] = area, parts[2] = local
        return (area: int.Parse(parts[1]), local: int.Parse(parts[2]));
    }
}

// ✅ Cliente ahora depende de IPhone (contrato), no de OldPhone
public sealed class PhoneClientService
{
    private readonly IPhone _phone;

    public PhoneClientService(IPhone phone) => _phone = phone;

    public void NotifyUser(string e164)
    {
        _phone.Call(e164); // interfaz clara y estable
        _phone.SendMessage(e164, "Hola, tu pedido va en camino.");
    }
}

// 🚀 Demo de uso
public static class Demo
{
    public static void Main()
    {
        // Inyectamos el Adapter como si fuera un IPhone
        IPhone phone = new OldPhoneAdapter();
        var service = new PhoneClientService(phone);

        service.NotifyUser("+52 664 1234567");
    }
}
```

---

## 📋 Justificación técnica

**Problema:**  
El cliente esperaba una interfaz `IPhone` con llamadas y mensajes basados en números E.164. Sin embargo, el servicio utilizaba directamente la clase `OldPhone`, con otra firma incompatible. Esto generaba **acoplamiento fuerte**, **duplicación de lógica de parsing** y rompía el contrato esperado.

**Patrón aplicado:**  
Se implementó el patrón **Adapter** (`OldPhoneAdapter`), que implementa `IPhone` y traduce las llamadas a `OldPhone`. El cliente ahora depende de una **abstracción estable** (`IPhone`) y no del detalle (`OldPhone`).

**Beneficios esperados:**  
- ✅ **Desacoplamiento:** el cliente no conoce `OldPhone`, solo `IPhone`.  
- ✅ **Reutilización:** encapsula la lógica de conversión E.164 → `area/local`.  
- ✅ **Flexibilidad:** se pueden inyectar otras implementaciones de `IPhone` sin modificar el cliente.  
- ✅ **Mantenibilidad:** el contrato `IPhone` sigue siendo el punto de entrada estable para el resto del sistema.  
