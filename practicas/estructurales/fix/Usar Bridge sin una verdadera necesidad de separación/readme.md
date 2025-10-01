# Refactorización de Patrones Estructurales (GoF)  
## Tema: Uso Innecesario de Bridge  
## Daniel Omar Gonzalez Martinez 

### 📌 Contexto  
En la base de código se aplicó el patrón **Bridge**, pero en este caso se usó sin una verdadera necesidad de separar una abstracción de su implementación. Esto generó **complejidad innecesaria**, ya que la jerarquía de clases era pequeña y estable.  

El objetivo del refactor fue **simplificar la arquitectura**, eliminando el Bridge mal aplicado y utilizando una implementación directa más clara y mantenible.  

---

## 🚨 Identificación de Code Smells (3 mínimos)  

1. **Sobre-ingeniería**:  
   - El patrón Bridge fue aplicado en un caso trivial (solo dos clases concretas, sin variaciones reales).  
   - Esto añadió complejidad sin aportar flexibilidad.  

2. **Duplicidad de Abstracciones**:  
   - Se crearon interfaces (`IForma`, `IDibujoImplementor`) que solo delegaban una llamada innecesaria.  

3. **Dificultad de Mantenimiento**:  
   - Los nuevos desarrolladores debían navegar por varias capas de código para entender algo tan simple como dibujar un círculo.  

---

## 🛠️ Código Original (Bridge mal aplicado)

```csharp
// Abstracción
public abstract class Forma
{
    protected IDibujoImplementor implementor;

    public Forma(IDibujoImplementor implementor)
    {
        this.implementor = implementor;
    }

    public abstract void Dibujar();
}

// Implementor
public interface IDibujoImplementor
{
    void DibujarCirculo(int x, int y, int radio);
}

// Implementación concreta 1
public class DibujoConsola : IDibujoImplementor
{
    public void DibujarCirculo(int x, int y, int radio)
    {
        Console.WriteLine($"Dibujando círculo en consola en ({x},{y}) con radio {radio}");
    }
}

// Refinamiento
public class Circulo : Forma
{
    private int x, y, radio;

    public Circulo(int x, int y, int radio, IDibujoImplementor implementor)
        : base(implementor)
    {
        this.x = x;
        this.y = y;
        this.radio = radio;
    }

    public override void Dibujar()
    {
        implementor.DibujarCirculo(x, y, radio);
    }
}
```

👉 Problema: Solo se usa **DibujoConsola**, no hay más implementaciones. El Bridge aquí es innecesario.  

---

## ✅ Refactor Propuesto (Simplificación)

```csharp
public class Circulo
{
    private int x, y, radio;

    public Circulo(int x, int y, int radio)
    {
        this.x = x;
        this.y = y;
        this.radio = radio;
    }

    public void Dibujar()
    {
        Console.WriteLine($"Dibujando círculo en consola en ({x},{y}) con radio {radio}");
    }
}
```

- Se elimina la abstracción innecesaria.  
- El código queda más legible y directo.  
- Si en el futuro se necesita flexibilidad real (ejemplo: dibujar en **Consola** o en **API Gráfica**), **entonces sí se aplicaría Bridge**.  

---

## 📖 Justificación Técnica en el Pull Request  

- **Problema identificado:** Se aplicó **Bridge** donde no era necesario, generando sobre-ingeniería.  
- **Patrón aplicado en refactor:** Se simplificó removiendo el Bridge y dejando una implementación directa.  
- **Beneficios esperados:**  
  - Mayor legibilidad del código.  
  - Menor complejidad innecesaria.  
  - Código más fácil de mantener por otros desarrolladores.  

---

