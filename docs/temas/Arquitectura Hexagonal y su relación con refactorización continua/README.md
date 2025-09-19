**Gutiérrez Martínez Ana Cristina | 21211959**

**Fecha: 18/09/2025**

# Arquitectura Hexagonal y su relación con refactorización continua

Una de las metodologías más efectivas para lograr un código limpio y estructurado es la combinación de la arquitectura hexagonal con los principios de Domain-Driven Design (DDD). 
‍
La arquitectura hexagonal (arquitectura de puertos y adaptadores) es un estilo de diseño de software que promueve la separación de preocupaciones, permitiendo que la lógica de negocio permanezca independiente de los detalles de infraestructura. Este enfoque facilita la creación de aplicaciones más robustas y fáciles de mantener. 

La arquitectura hexagonal se centra en tres componentes principales:

- Dominios Centrales: La lógica de negocio y las reglas del dominio.
- Puertos: Interfaces que definen cómo los dominios centrales interactúan con el mundo exterior.
- Adaptadores: Implementaciones concretas de los puertos que se conectan con tecnologías específicas, como bases de datos, servicios web, etc.

Una de las ventajas de la arquitectura hexagonal es que nos permite aislar la lógica de negocio de la infraestructura, además, la separación clara facilita la creación de pruebas unitarias efectivas. Asimismo, permite la incorporación de nuevos requerimientos y tecnologías sin afectar la lógica central. 

## ¿Cómo implementamos la arquitectura hexagonal? 

El primer paso sería definir los contextos para organizar bien la lógica de dominio. Por ejemplo: 

- Contexto de gestión de usuarios (gestiona la autenticación y los perfiles de los usuarios)
- Contexto de procesamiento de pedidos (gestiona pedidos, pagos y envíos)
- Contexto de facturación (gestiona facturas y transacciones financieras)

El siguiente paso sería utilizar/definir los puertos y adaptadores para mayor flexibilidad:

**Definición del puerto:**

public interface IOrderRepository
{
    void Save(Order order);
    Order FindById(Guid orderId);
}

**Definición e implantación de un adaptador (capa de infraestructura):**

public class OrderRepository : IOrderRepository
{
    private readonly DbContext _context;
    
    public OrderRepository(DbContext context)
    {
        _context = context;
    }
    
    public void Save(Order order) => _context.Orders.Add(order);
    public Order FindById(Guid orderId) => _context.Orders.Find(orderId);
}

**Y el paso tres sería separar la lógica de aplicación de la lógica de dominio:**

public class OrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public void PlaceOrder(Guid customerId, List<OrderItem> items)
    {
        var order = new Order(customerId, items);
        _orderRepository.Save(order);
    }
}

**Finalmente, garantizaremos la comprobabilidad con la inyección de dependencias (separamos la lógica del negocio de la infraestructura):**

[Fact]
public void Should_Create_Order_Successfully()
{
    var repositoryMock = new Mock<IOrderRepository>();
    var service = new OrderService(repositoryMock.Object);

    service.PlaceOrder(Guid.NewGuid(), new List<OrderItem> { new OrderItem("Product1", 2) });

    repositoryMock.Verify(r => r.Save(It.IsAny<Order>()), Times.Once);
}

## Ahora, la pregunta central en el tema sería: ¿Cuál es la relación de la arquitectura hexagonal con la refactorización continua?
En sencillas palabras, la arquitectura hexagonal por si misma es un "compañero" de la refactorización continua, ya que nos permite mejorar y reorganizar el código de una manera constante sin realmente afectar la lógica central de nada. 

Como la arquitectura hexagonal literalmente se basa en la separación por capas -entre el núcleo y el dominio-, esto lo vuelve un entorno ideal para la refactorización continua. Resulta hasta más sencillo realizar cambios internos sin afectar al resto del sistema. 

## Referencias: 

El poder de la arquitectura hexagonal - The Codest. (2023, 13 de junio). The Codest. https://thecodest.co/es/blog/el-poder-de-la-arquitectura-hexagonal/

Arquitectura Hexagonal en Symfony | Desarrollo. (s.f.). Unow: Sumamos equipos tech de calidad. https://unow.es/posts/arquitectura-hexagonal-en-symfony 

Diaz, I. (s.f.). Arquitectura Hexagonal y el DDD - Apiumhub. Apiumhub. https://apiumhub.com/es/tech-blog-barcelona/arquitectura-hexagonal-y-el-ddd/


