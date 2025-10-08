# Crear decoradores que alteran el estado en vez de solo añadir comportamiento

Practica para la propuesta de la correcion de un badcode generado con el tema de "Crear decoradores que alteran el estado en vez de solo añadir comportamiento"

Montaño Zaragoza Marcos Ulises 21211998

## Bad Code generado
```csharp
using System;
using System.Collections.Generic;

namespace DecoratorBadExample
{
    // Clase base de bebidas
    public class Bebida
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Calorias { get; set; }

        public Bebida(string nombre, double precio, int calorias)
        {
            Nombre = nombre;
            Precio = precio;
            Calorias = calorias;
        }

        public virtual void MostrarInfo()
        {
            Console.WriteLine($"Bebida: {Nombre}, Precio: {Precio}, Calorías: {Calorias}");
        }
    }

    // Decorador de azúcar - MAL: cambia atributos internos directamente
    public class AzucarDecorador : Bebida
    {
        private Bebida bebida;

        public AzucarDecorador(Bebida b) : base(b.Nombre, b.Precio, b.Calorias)
        {
            bebida = b;
            // ❌ Mutación directa de estado en lugar de añadir comportamiento
            bebida.Calorias += 50;
            bebida.Precio += 0.5;
            bebida.Nombre += " con Azúcar";
        }

        public override void MostrarInfo()
        {
            bebida.MostrarInfo();
        }
    }

    // Decorador de leche - MAL: lógica duplicada y mutación directa
    public class LecheDecorador : Bebida
    {
        private Bebida bebida;

        public LecheDecorador(Bebida b) : base(b.Nombre, b.Precio, b.Calorias)
        {
            bebida = b;
            bebida.Calorias += 100;
            bebida.Precio += 1.0;
            bebida.Nombre += " con Leche";
        }

        public override void MostrarInfo()
        {
            bebida.MostrarInfo();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bebida cafe = new Bebida("Café", 2.0, 10);
            cafe.MostrarInfo();

            // ❌ Decoradores alteran el objeto original
            Bebida cafeConAzucar = new AzucarDecorador(cafe);
            cafeConAzucar.MostrarInfo();

            Bebida cafeConLeche = new LecheDecorador(cafeConAzucar);
            cafeConLeche.MostrarInfo();

            // Resultado: estado mutado en cascada, difícil de predecir
        }
    }
}
```

## 📌 Contexto
Actualmente, los decoradores (AzucarDecorador, LecheDecorador) mutan directamente el estado interno de la clase Bebida, lo cual contradice la intención del patrón Decorator en el GoF: añadir responsabilidades dinámicamente sin modificar el objeto original.

Esto genera efectos secundarios inesperados (precio y calorías alteradas permanentemente en cascada), rompe principios de encapsulamiento, y complica la extensión futura del sistema.

---

## 🚩 Problemas detectados (10)

1. **Mutación directa** del objeto base (`bebida.Calorias += 50;`).
2. **Estado inconsistente**: la bebida original queda alterada.
3. **Duplicación de lógica** (cada decorador repite incremento de calorías/precio).
4. **Violación SRP** (decorador altera estado + presenta información).
5. **Mala jerarquía**: herencia innecesaria de `Bebida` + composición al mismo tiempo.
6. **Falta de encapsulamiento**: los atributos quedan expuestos a cambios.
7. **Orden impredecible**: aplicar `AzucarDecorador` antes o después de `LecheDecorador` da resultados distintos no planeados.
8. **Dependencia fuerte** con la clase concreta `Bebida`.
9. **Violación OCP**: agregar un nuevo decorador implica tocar código viejo.
10. **Difícil extensibilidad**: no se pueden encadenar decoradores sin efectos colaterales.

---

## ✅ Refactor Propuesto

- Implementar el patrón **Decorator** correctamente:  
  - El decorador no modifica el objeto base.  
  - Cada decorador aporta comportamiento extra calculado **on-demand**.  
- Aplicar **composición sobre herencia**: los decoradores envuelven a `IBebida`.  
- Asegurar que **precio y calorías se calculen dinámicamente** en lugar de alterarse.  


### Ejemplo tras refactor:
```csharp
using System;

namespace DecoratorRefactor
{
    // Contrato común
    public interface IBebida
    {
        string Nombre { get; }
        double Precio { get; }
        int Calorias { get; }
        void MostrarInfo();
    }

    // Clase base
    public class Bebida : IBebida
    {
        public string Nombre { get; }
        public double Precio { get; }
        public int Calorias { get; }

        public Bebida(string nombre, double precio, int calorias)
        {
            Nombre = nombre;
            Precio = precio;
            Calorias = calorias;
        }

        public void MostrarInfo()
        {
            Console.WriteLine($"Bebida: {Nombre}, Precio: {Precio}, Calorías: {Calorias}");
        }
    }

    // Clase base para decoradores
    public abstract class BebidaDecorador : IBebida
    {
        protected IBebida bebida;

        public BebidaDecorador(IBebida b)
        {
            bebida = b;
        }

        public abstract string Nombre { get; }
        public abstract double Precio { get; }
        public abstract int Calorias { get; }

        public virtual void MostrarInfo()
        {
            Console.WriteLine($"Bebida: {Nombre}, Precio: {Precio}, Calorías: {Calorias}");
        }
    }

    // Decorador Azúcar
    public class AzucarDecorador : BebidaDecorador
    {
        public AzucarDecorador(IBebida b) : base(b) { }

        public override string Nombre => bebida.Nombre + " con Azúcar";
        public override double Precio => bebida.Precio + 0.5;
        public override int Calorias => bebida.Calorias + 50;
    }

    // Decorador Leche
    public class LecheDecorador : BebidaDecorador
    {
        public LecheDecorador(IBebida b) : base(b) { }

        public override string Nombre => bebida.Nombre + " con Leche";
        public override double Precio => bebida.Precio + 1.0;
        public override int Calorias => bebida.Calorias + 100;
    }

    class Program
    {
        static void Main(string[] args)
        {
            IBebida cafe = new Bebida("Café", 2.0, 10);
            cafe.MostrarInfo();

            IBebida cafeConAzucar = new AzucarDecorador(cafe);
            cafeConAzucar.MostrarInfo();

            IBebida cafeConLeche = new LecheDecorador(cafeConAzucar);
            cafeConLeche.MostrarInfo();

            // ✅ Estado del café original NO fue alterado
            cafe.MostrarInfo();
        }
    }
}
```
<img width="1113" height="247" alt="image" src="https://github.com/user-attachments/assets/e4bbcd28-0a89-4873-92e8-59b9d8e20058" />

## 📈 Beneficios del refactor

- Se **preserva el estado original** de la bebida.
- Los decoradores **añaden comportamiento dinámico** sin mutar estado.
- **Sistema extensible**: nuevos ingredientes solo requieren implementar otro decorador.
- Aplicación correcta de **OCP** y **composición sobre herencia**.
- Eliminación de **efectos secundarios inesperados**.

---

## 🔄 Alternativas de patrones

- **Strategy**: si el cálculo de precio/calorías fuera seleccionable dinámicamente en lugar de acumulativo.
- **Chain of Responsibility**: si quisiéramos aplicar reglas condicionales encadenadas sobre las bebidas.
- **Proxy**: si la intención fuera controlar acceso a una bebida pesada (ej. cachear calorías de bebidas complejas).

---

## 📝 Conclusión

El refactor permite que los decoradores cumplan su función principal: **añadir responsabilidades de forma dinámica y segura** sin alterar el estado original del objeto. Esto mejora la **mantenibilidad**, la **extensibilidad** y la **claridad del código**, además de cumplir con los principios SOLID y buenas prácticas de diseño orientado a objetos.
