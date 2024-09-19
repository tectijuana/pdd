## 1. ¿Qué es el patrón Flyweight?
El patrón **Flyweight** es un patrón de diseño estructural que permite ahorrar memoria cuando se trabaja con un gran número de objetos similares. El objetivo es compartir la mayor cantidad de datos posibles entre estos objetos, en lugar de almacenar toda la información para cada uno de ellos.
Este patrón es utilizado cuando la optimización de los recursos es algo primordial ya que elimina la redundancia de objetos con propiedades idénticas.

---

## 2. ¿Cuándo usar Flyweight?
El patrón Flyweight es ideal en las siguientes situaciones:

- Tener una gran cantidad de objetos que ocupan mucho espacio en memoria.
- Muchos de estos objetos comparten datos intrínsecos, que pueden ser reutilizados.

Ejemplos clásicos:
- Elementos en un editor gráfico (como círculos, rectángulos).
- Personajes en un juego masivo en línea (MMORPG), donde muchos personajes tienen los mismos atributos básicos (textura, color).

---

## 3. Componentes Clave
| Componente      | Descripción                                                                 |
|-----------------|-----------------------------------------------------------------------------|
| **Flyweight**    | La interfaz que declara los métodos para el estado intrínseco compartido.   |
| **ConcreteFlyweight** | Implementa el estado intrínseco y permite compartirlo entre los objetos.|
| **FlyweightFactory** | Se encarga de gestionar los objetos Flyweight y evitar duplicados.     |
| **Client**       | Usa los objetos Flyweight y maneja el estado extrínseco.                   |

---
## 9. Ventajas y Desventajas del Patrón Flyweight

### Ventajas:
1. **Ahorro de Memoria**: El mayor beneficio del patrón Flyweight es la reducción significativa del uso de memoria, ya que muchos objetos comparten su estado intrínseco (datos que no cambian entre objetos).
   
   - En lugar de crear instancias separadas para cada objeto, se comparten partes comunes, lo cual es especialmente útil cuando trabajas con miles o millones de objetos similares.

2. **Mejora el Rendimiento en Escenarios de Alto Costo de Memoria**: Al reducir el número de objetos en memoria, se mejora el rendimiento del sistema, especialmente en aplicaciones gráficas, videojuegos, o cuando hay una gran cantidad de objetos en el mismo espacio de memoria.

3. **Reutilización de Objetos**: Permite la reutilización de objetos y evita la creación innecesaria de nuevos objetos, lo que se traduce en un sistema más eficiente y fácil de mantener.

4. **Escalabilidad**: Al reducir el consumo de recursos, es más fácil escalar la aplicación sin comprometer demasiado el rendimiento.

---

### Desventajas:
1. **Complejidad Adicional**: La implementación del patrón Flyweight introduce complejidad, ya que tienes que separar el estado intrínseco (compartido) y extrínseco (específico de cada instancia). Esto puede hacer el código más difícil de entender y mantener.

2. **Menos Flexibilidad**: Los objetos Flyweight no pueden modificarse fácilmente, ya que parte de su estado es compartido. Si necesitas objetos que puedan cambiar internamente sin restricciones, Flyweight puede no ser la mejor opción.

3. **Costos de Administración**: El **Flyweight Factory** necesita gestionar los objetos compartidos, lo que puede añadir una sobrecarga en términos de seguimiento y búsqueda de los objetos existentes. Si no se implementa correctamente, el sistema podría perder eficiencia.

4. **Aplicable solo a ciertos problemas**: No todos los sistemas o problemas requieren el uso de Flyweight. Si tienes un número reducido de objetos o los objetos son lo suficientemente distintos entre sí, el patrón Flyweight puede ser innecesario y agregar sobrecomplicación.

---
## Ejemplo
https://dotnetfiddle.net/naGNne
```csharp
using System;
using System.Collections.Generic;

public interface ITreeType
{
    void Draw(int x, int y);
}

public class TreeType : ITreeType
{
    private string name;
    private string color;
    private string otherTreeData;

    public TreeType(string name, string color, string otherTreeData)
    {
        this.name = name;
        this.color = color;
        this.otherTreeData = otherTreeData;
    }

    public void Draw(int x, int y)
    {
        Console.WriteLine($"Drawing {name} tree of color {color} at position ({x}, {y}). Data: {otherTreeData}");
    }
}

public class TreeTypeFactory
{
    private Dictionary<string, ITreeType> treeTypes = new Dictionary<string, ITreeType>();

    public ITreeType GetTreeType(string name, string color, string otherTreeData)
    {
        string key = $"{name}_{color}_{otherTreeData}";

        if (!treeTypes.ContainsKey(key))
        {
            treeTypes[key] = new TreeType(name, color, otherTreeData);
        }

        return treeTypes[key];
    }
}

public class Tree
{
    private int x;
    private int y;
    private ITreeType type;

    public Tree(int x, int y, ITreeType type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }

    public void Draw()
    {
        type.Draw(x, y);
    }
}

public class Program
{
    public static void Main()
    {
        TreeTypeFactory treeTypeFactory = new TreeTypeFactory();

        ITreeType oakType = treeTypeFactory.GetTreeType("Oak", "Green", "Tall and strong");
        ITreeType pineType = treeTypeFactory.GetTreeType("Pine", "DarkGreen", "Evergreen and slim");

        List<Tree> trees = new List<Tree>
        {
            new Tree(10, 20, oakType),
            new Tree(30, 40, oakType),
            new Tree(50, 60, pineType),
            new Tree(70, 80, pineType)
        };

        foreach (Tree tree in trees)
        {
            tree.Draw();
        }
    }
}
