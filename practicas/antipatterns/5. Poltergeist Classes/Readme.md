# 🧠 Antipatrón: Poltergeist Classes

## 👩‍💻 Datos del Alumno
**Nombre:** Jocelin Maribel Bernal Enciso  
**Número de Control:** 21211919  
**Carrera:** Ingeniería en Sistemas Computacionales  
**Tema:** Antipatrones fuera de GoF — *Poltergeist Classes*  
 

---

## 🧩 1. Comprensión del Antipatrón 

### 📖 Definición
El antipatrón **Poltergeist Classes** describe clases que **aparecen y desaparecen rápidamente durante la ejecución del sistema**, sin tener una responsabilidad clara ni aportar valor real al diseño.  
Estas clases son transitorias: se crean, ejecutan una o dos funciones triviales (como invocar otra clase o pasar parámetros) y luego se destruyen.  

Su existencia suele ser síntoma de un diseño apresurado o de **una mala interpretación del principio de responsabilidad única (SRP)**. En otras palabras, son “fantasmas” del código que aumentan la complejidad sin ofrecer beneficios tangibles.

---

### 🧠 Origen del concepto
El término *Poltergeist* (del alemán *“espíritu ruidoso”*) proviene de la literatura de **antipatrones de Brown, Malveau, McCormick y Mowbray (1998)**, donde se asocian con clases que “hacen ruido” en el sistema sin contribuir a la funcionalidad.  
Este concepto surgió como parte de un esfuerzo por catalogar los errores recurrentes en diseño orientado a objetos que obstaculizan el mantenimiento y la escalabilidad del software.

---

### 💡 Características comunes
- Son **invocadas de manera temporal** y rara vez reutilizadas.  
- Carecen de **estado interno significativo**.  
- Suelen **delegar todo el trabajo a otra clase**.  
- Dificultan el rastreo del flujo lógico del sistema.  
- Crean una **falsa sensación de modularidad**, cuando en realidad solo añaden capas innecesarias.

---

## 💻 2. Ejemplo Técnico (10 pts)

### ❌ Ejemplo de código con el antipatrón
```csharp
public class OrdenPoltergeist
{
    public void Procesar()
    {
        var procesador = new ProcesadorOrden();
        procesador.Ejecutar();
    }
}

public class ProcesadorOrden
{
    public void Ejecutar()
    {
        Console.WriteLine("Procesando la orden...");
    }
}
```

🔎 En este ejemplo:
- `OrdenPoltergeist` no realiza ninguna lógica útil.
- Solo **crea un objeto** de otra clase (`ProcesadorOrden`) y **llama un método**.
- El sistema podría funcionar perfectamente sin `OrdenPoltergeist`.

---

### ✅ Código refactorizado sin antipatrón
```csharp
public class ProcesadorOrden
{
    public void Procesar()
    {
        Console.WriteLine("Procesando la orden...");
    }
}
```

🔄 La funcionalidad se consolida en una sola clase con **propósito definido**, eliminando intermediarios inútiles.

---

## ⚠️ 3. Consecuencias (10 pts)

El uso de clases poltergeist genera varios efectos negativos tanto en el diseño como en la evolución del sistema:

| Tipo de Impacto | Descripción |
|------------------|-------------|
| 🔁 **Complejidad accidental** | Aumentan la cantidad de clases sin necesidad real. |
| 🧩 **Difícil mantenimiento** | Más entidades que comprender, documentar y probar. |
| ⚡ **Rendimiento degradado** | Se crean y destruyen objetos sin propósito funcional. |
| 🧠 **Pérdida de trazabilidad** | Dificultan entender el flujo de ejecución y depurar errores. |
| 💣 **Violación de SRP y cohesión** | Las responsabilidades están mal distribuidas o mal entendidas. |

---

### 📉 Ejemplo real en la industria
En sistemas empresariales grandes (ERP o CRM), es común encontrar *Poltergeist Classes* llamadas `Manager`, `Helper` o `Controller` que solo sirven para delegar tareas a servicios reales, sin aportar valor.  
Estos elementos confunden al equipo de desarrollo y hacen que el sistema se vuelva más frágil ante cambios.

---

## 🧰 4. Solución Correctiva (10 pts)

### 🧱 Estrategias de mejora

1. **Eliminar intermediarios innecesarios:**  
   Fusionar la lógica útil directamente en la clase que ejecuta el comportamiento real.

2. **Reasignar responsabilidades:**  
   Redefinir la arquitectura para que cada clase tenga un propósito y una razón clara para existir.

3. **Aplicar el principio SRP (Single Responsibility Principle):**  
   Cada clase debe encargarse de una única parte bien definida del sistema.

4. **Usar patrones adecuados cuando haya un propósito legítimo:**  
   - Si la clase coordina acciones complejas → aplicar *Facade*.  
   - Si representa comandos independientes → usar *Command Pattern*.  
   - Si controla flujos dinámicos de comunicación → considerar *Mediator Pattern*.

---

### 🌟 Refactorización estructurada
Un rediseño adecuado separa las capas lógicas:

```
📦 Dominio
 ┣ 📜 ProcesadorOrden.cs
 ┣ 📜 ReporteService.cs
 ┗ 📜 NotificadorEmail.cs
```

Cada clase con **propósito claro y responsabilidades delimitadas**, reduciendo el acoplamiento y mejorando la cohesión.

---

## 🧩 5. Relevancia actual y buenas prácticas

En el desarrollo moderno, especialmente con frameworks como **Spring Boot, ASP.NET Core, Angular o Django**, es común caer en este antipatrón al sobreabstraer componentes.

Ejemplo:  
Crear un `UserManagerService` que solo llama a `UserRepository` sin agregar validación, lógica o seguridad.  
Esto **rompe la regla YAGNI (You Aren’t Gonna Need It)** y genera sobrecarga estructural.

Las metodologías ágiles promueven **simplicidad intencional**, por lo que eliminar clases “fantasma” es una forma de mantener la arquitectura limpia y sostenible.

---

## 🎤 6. Sintesis

> El antipatrón *Poltergeist Classes* representa clases transitorias que no aportan valor funcional.  
> Son un síntoma de un diseño sobrecomplicado que viola la cohesión y el principio SRP.  
> Al eliminarlas, el sistema gana claridad, eficiencia y mantenibilidad.  
> La solución se basa en consolidar la lógica, simplificar el flujo y aplicar patrones legítimos como *Facade* o *Command* cuando sea necesario.  
> En resumen, un buen diseño no necesita fantasmas: necesita propósito y estructura.

---

## 📚 7. Referencias Bibliográficas

1. Brown, W. J., Malveau, R. C., McCormick, H. W., & Mowbray, T. J. (1998). *AntiPatterns: Refactoring Software, Architectures, and Projects in Crisis*. Wiley.  
2. Martin, R. C. (2003). *Agile Software Development: Principles, Patterns, and Practices*. Prentice Hall.  
3. Fowler, M. (2004). *Patterns of Enterprise Application Architecture*. Addison-Wesley.  
4. Larman, C. (2005). *Applying UML and Patterns: An Introduction to Object-Oriented Analysis and Design*. Prentice Hall.  
5. Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1995). *Design Patterns: Elements of Reusable Object-Oriented Software*. Addison-Wesley.  
6. McConnell, S. (2004). *Code Complete (2nd Edition)*. Microsoft Press.  


---

> 💬 *“Las clases Poltergeist son fantasmas del código: aparecen, hacen ruido y desaparecen, dejando solo confusión. Un buen diseño las exorciza con claridad y propósito.”*
