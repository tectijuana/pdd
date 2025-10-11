# 📖 Refactorización de Componentes Gráficos con Patrón Composite.

## 📝 Formato del Pull Request

### 🔍 Problemas detectados
- Uso de **estructuras de control (`if/switch`) para distinguir tipos** en lugar de polimorfismo.  
- El atributo `Type` obliga a que el método `Draw()` verifique manualmente qué hacer con cada tipo → **violación del principio abierto/cerrado (OCP)**.  
- El código es poco extensible: agregar un nuevo componente (ejemplo: `Label`) implica modificar el método `Draw` en la clase base.  
- El diseño genera **alta dependencia** de un único método que contiene toda la lógica de control.  

### 🛠 Patrones aplicados
- *Composite* → estructura jerárquica donde cada objeto (`Window`, `Button`, etc.) implementa la misma interfaz `IUIComponent`.  
- *Polimorfismo* → cada clase conoce cómo representarse sin depender de condicionales.  
- *Responsabilidad clara* → `Window` actúa como un *Composite* que puede contener hijos, y `Button` como una hoja (*Leaf*).  

### 💡 Justificación del cambio
Con este refactor:  
- *Extensibilidad:* es posible agregar nuevos componentes sin modificar el código existente.  
- *Mantenibilidad:* el comportamiento de cada tipo está encapsulado en su propia clase.  
- *Cohesión:* cada clase tiene una única responsabilidad (dibujar un componente o coordinar hijos).  
- *Escalabilidad:* la estructura jerárquica puede crecer (ventanas con múltiples subcomponentes) sin complejidad adicional.  

### 🔄 Impacto
- Se eliminan estructuras condicionales innecesarias.  
- La aplicación cumple principios SOLID (OCP y SRP).  
- El código queda listo para manejar árboles de componentes más complejos, incluso anidados.  

### 📌 Próximos pasos sugeridos
- Añadir más componentes (ejemplo: `Label`, `CheckBox`) para comprobar la extensibilidad del Composite.  
- Integrar un patrón *Decorator* para agregar estilos visuales (ejemplo: bordes, colores) sin alterar las clases existentes.  
- Implementar pruebas unitarias que verifiquen el correcto renderizado de estructuras jerárquicas de UI.  

---

## 💻 Código de ejemplo

### 🚨 Código con malas prácticas (uso de `if/switch` en lugar de polimorfismo)
```csharp
using System;
using System.Collections.Generic;

namespace BadCompositeExample
{
    // Clase base genérica sin polimorfismo
    public class UIComponent
    {
        public string Type { get; set; } // 🚨 Se usa un string para distinguir tipos
        public string Name { get; set; }
        public List<UIComponent> Children { get; set; } = new List<UIComponent>();

        public void Draw()
        {
            if (Type == "Button")
            {
                Console.WriteLine($"[Botón]: {Name}");
            }
            else if (Type == "Window")
            {
                Console.WriteLine($"[Ventana]: {Name}");
                foreach (var child in Children)
                {
                    child.Draw(); // 🚨 sigue dependiendo de if/switch
                }
            }
            else
            {
                Console.WriteLine($"[Componente desconocido]: {Name}");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            var window = new UIComponent { Type = "Window", Name = "Ventana Principal" };
            var button = new UIComponent { Type = "Button", Name = "Aceptar" };

            window.Children.Add(button);
            window.Draw();
        }
    }
}
using System;
using System.Collections.Generic;

namespace GoodCompositeExample
{
    // Interfaz común: todos los componentes deben poder "dibujarse"
    public interface IUIComponent
    {
        void Draw();
    }

    // Hoja (Leaf): Botón
    public class Button : IUIComponent
    {
        private readonly string _name;

        public Button(string name)
        {
            _name = name;
        }

        public void Draw()
        {
            Console.WriteLine($"[Botón]: {_name}");
        }
    }

    // Compuesto (Composite): Ventana que contiene otros componentes
    public class Window : IUIComponent
    {
        private readonly string _name;
        private readonly List<IUIComponent> _children = new();

        public Window(string name)
        {
            _name = name;
        }

        public void Add(IUIComponent component)
        {
            _children.Add(component);
        }

        public void Draw()
        {
            Console.WriteLine($"[Ventana]: {_name}");
            foreach (var child in _children)
            {
                child.Draw(); // ✅ polimorfismo en acción
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            var mainWindow = new Window("Ventana Principal");

            var btnOk = new Button("Aceptar");
            var btnCancel = new Button("Cancelar");

            mainWindow.Add(btnOk);
            mainWindow.Add(btnCancel);

            mainWindow.Draw();
        }
    }
}
