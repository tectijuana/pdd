
<img width="721" alt="Screenshot 2024-10-24 at 10 03 06 p m" src="https://github.com/user-attachments/assets/16474ad1-c074-4cde-8d07-f7f5a9fa79c6">



## Problema de Código Mal Estructurado en un MVC con .NET 8

Este ejercicio simula un proyecto **MVC en .NET 8** que presenta diversos problemas que los estudiantes deberán identificar y resolver. El escenario involucra la gestión de una tienda de dispositivos móviles, con un controlador, vistas y modelos implementados de forma incorrecta.

---

### 🎯 **Tabla de Objetivos del Ejercicio**

| **Objetivo**                              | **Descripción**                                                                 |
|-------------------------------------------|---------------------------------------------------------------------------------|
| **Identificación de Code Smells**         | Localizar código redundante y mal organizado dentro del proyecto.                |
| **Aplicación de Principios SOLID**        | Asegurarse de que el código respeta los principios de diseño modular.            |
| **Uso de Patrones GoF**                   | Implementar Factory Method o Dependency Injection para desacoplar componentes.  |
| **Optimización del Controlador**          | Reducir lógica innecesaria en controladores mediante refactorización.            |
| **Separación de Responsabilidades**       | Asegurar que cada capa (Modelo, Vista, Controlador) esté claramente diferenciada.|

---

### 🚩 **Escenario: Tienda de Dispositivos Móviles**
La aplicación permite a los usuarios:
1. Ver un listado de dispositivos móviles disponibles.
2. Agregar dispositivos al carrito de compras.
3. Realizar pedidos.

Sin embargo, el código está plagado de malas prácticas, mezclando lógica de negocio en controladores y duplicando código. El siguiente es un ejemplo del código inicial con errores.

---

## 📂 **Código Inicial con Problemas**

### **Controller: MobileController.cs**

```csharp
using Microsoft.AspNetCore.Mvc;
using MobileStore.Models;

namespace MobileStore.Controllers
{
    public class MobileController : Controller
    {
        public IActionResult Index()
        {
            var mobiles = new List<Mobile>
            {
                new Mobile { Id = 1, Name = "iPhone 14", Price = 999 },
                new Mobile { Id = 2, Name = "Samsung Galaxy S23", Price = 899 }
            };
            ViewBag.Mobiles = mobiles;
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Mobile>>("Cart") ?? new List<Mobile>();
            var mobile = new Mobile { Id = id, Name = "Placeholder", Price = 0 }; // Error: Placeholder
            cart.Add(mobile);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }
    }
}
```

### **Modelo: Mobile.cs**

```csharp
namespace MobileStore.Models
{
    public class Mobile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

### **Vista: Index.cshtml**

```html
@{
    ViewBag.Title = "Tienda de Móviles";
}

<h1>@ViewBag.Title</h1>
<ul>
    @foreach (var mobile in ViewBag.Mobiles)
    {
        <li>@mobile.Name - $@mobile.Price
            <a href="@Url.Action("AddToCart", new { id = mobile.Id })">Agregar al carrito</a>
        </li>
    }
</ul>
```

---

## ⚠️ **Problemas Identificados**

1. **Controlador con Lógica de Negocio**: El `MobileController` contiene lógica para manejar el carrito, que debería estar en una capa de servicios.
2. **Código Duplicado**: La creación de objetos `Mobile` en diferentes partes del controlador.
3. **Uso Inadecuado de ViewBag**: Uso excesivo de `ViewBag` en lugar de ViewModels para manejar datos.
4. **Falta de Principio de Responsabilidad Única (SRP)**: El controlador gestiona la vista y la lógica del carrito.
5. **Problema de Hardcoding**: El móvil añadido al carrito tiene un valor "Placeholder".

---

## 📌 **Actividad para el Estudiante**

1. **Refactorización del Controlador**:
   - Extraer la lógica del carrito a una clase de servicio.
   - Implementar un patrón de fábrica (`Factory Method`) para crear objetos `Mobile`.

2. **Aplicación del Principio SRP**:
   - Crear un **ViewModel** para gestionar los datos de la vista.

3. **Uso de Dependencias**:
   - Aplicar **Dependency Injection (DI)** para inyectar el servicio del carrito.

4. **Mejorar la Vista**:
   - Eliminar el uso de `ViewBag` y reemplazarlo por un **ViewModel**.

---

---

## 📝 **Resumen y Conclusión**

Este ejercicio desafía al estudiante a aplicar principios de diseño y patrones de manera práctica para refactorizar un proyecto de MVC en .NET 8. Al trabajar con este ejemplo, los estudiantes no solo deben identificar los problemas existentes, sino también diseñar una solución más eficiente y limpia aplicando **buenas prácticas y patrones GoF**.
