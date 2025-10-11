# 💥 Decoradores Mal Encadenados  // Ricardo Rodriguez Carreras 21212360
📂 Refactorización de Patrones Estructurales (GoF) — C# (.NET 8)

---

## 🔎 Identificación del Problema

El patrón **Decorator** permite extender el comportamiento de un objeto dinámicamente.  
Un *decorador mal encadenado* ocurre cuando:

- Se aplican decoradores **duplicados** (ej. `Logging` dos veces).  
- El orden de encadenamiento altera el resultado de forma no controlada.  
- Los decoradores incluyen lógica de negocio en lugar de solo enriquecer el comportamiento.  

### Code Smells detectados
1. **Duplicación de efectos** → aplicar el mismo decorador dos veces genera resultados repetidos.  
2. **Orden-dependencia oculta** → el cliente decide el orden y produce resultados inesperados.  
3. **Decoradores con lógica extra** → violación de separación de responsabilidades.  

---

## 🏗️ Patrón aplicado

- Se mantiene el **Decorator** como patrón principal.  
- Se introduce un **Builder de decoradores** para:
  - Controlar **orden de aplicación**.  
  - Prevenir **duplicados** de decoradores.  
  - Validar instanciación correcta.  

Con esto, el cliente no encadena manualmente → se evita el *code smell* de decoradores mal encadenados.

---

## 🧩 Código Problemático (antes)


public class Client
{
    public static void Run()
    {
        INotifier notifier = new EmailNotifier();

        // ❌ Encadenado manual inseguro
        notifier = new LoggingDecorator(notifier);
        notifier = new CompressionDecorator(notifier);
        notifier = new LoggingDecorator(notifier); // Duplicado

        notifier.Send("Hola mundo");
    }
}



✅ Refactor Propuesto (después)
---
// Uso del Builder
---
public class ClientRefactored
{
    public static void Run()
    {
        INotifier baseNotifier = new EmailNotifier();

        var builder = new NotifierDecoratorBuilder()
            .Add<LoggingDecorator>()      // primero Logging
            .Add<CompressionDecorator>()  // luego Compresión
            .Add<EncryptionDecorator>()   // finalmente Encripción
            .Add<LoggingDecorator>();     // segundo Logging ignorado automáticamente ✅

        INotifier notifier = builder.Build(baseNotifier);

        notifier.Send("Hola mundo");
    }
}


Resultado esperado en consola
---
[Log] Enviando mensaje...
[Email] ENC(COMP(Hola mundo))

🛠️ Beneficios del Refactor
---

Evita duplicados gracias a un HashSet<Type> en el builder.

Orden controlado de aplicación de decoradores.

Contratos claros con NotifierDecoratorBase.

Separación de responsabilidades: decoradores solo extienden, no deciden.

Refactor parcial y funcional: compatible con INotifier original.





