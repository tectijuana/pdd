
## 🛠️ **Pista: Servicio Refactorizado**

```csharp
public interface IMobileService
{
    List<Mobile> GetAvailableMobiles();
    void AddToCart(int id);
    List<Mobile> GetCart();
}

public class MobileService : IMobileService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MobileService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public List<Mobile> GetAvailableMobiles()
    {
        return new List<Mobile>
        {
            new Mobile { Id = 1, Name = "iPhone 14", Price = 999 },
            new Mobile { Id = 2, Name = "Samsung Galaxy S23", Price = 899 }
        };
    }

    public void AddToCart(int id)
    {
        var cart = GetCart();
        var mobile = GetAvailableMobiles().FirstOrDefault(m => m.Id == id);
        if (mobile != null)
        {
            cart.Add(mobile);
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson("Cart", cart);
        }
    }

    public List<Mobile> GetCart()
    {
        return _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<Mobile>>("Cart") ?? new List<Mobile>();
    }
}
```
