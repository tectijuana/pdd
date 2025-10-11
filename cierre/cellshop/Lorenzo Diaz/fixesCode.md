# 🏗️ 1. Definición de Contratos (Interfaces)
## Definimos las interfaces (clases abstractas en Python) que garantizan el desacoplamiento entre los módulos (P4, P7, P8, P10).
```python
from abc import ABC, abstractmethod

# P7: Observer Pattern - Interfaz para recibir notificaciones
class IObserver(ABC):
    @abstractmethod
    def update(self, subject_state):
        pass

# P4: Strategy Pattern - Interfaz para calcular descuentos
class IDiscountStrategy(ABC):
    @abstractmethod
    def calculate_discount(self, base_price: float) -> float:
        pass

# P8: Decorator Pattern - Interfaz para el Celular y sus Decoraciones
class IMobileComponent(ABC):
    @abstractmethod
    def get_price(self) -> float:
        pass

# P10: Chain of Responsibility - Interfaz para manejar promociones
class IPromotionHandler(ABC):
    @abstractmethod
    def set_next(self, handler):
        pass

    @abstractmethod
    def handle(self, sale_request):
        pass

# Objeto de Solicitud (Request Object) que viaja por la cadena (P10)
class SaleRequest:
    def __init__(self, mobile, quantity, customer_is_vip=False):
        self.mobile = mobile
        self.quantity = quantity
        self.customer_is_vip = customer_is_vip
        self.final_price = mobile.get_price() * quantity # P8: Usa el precio decorado
        self.discounts_applied = []
```
# 📱 2. Clase Mobile y Decorator (P1, P8)
## Aplicamos Encapsulamiento (P1) a la clase Mobile y la convertimos en el Componente Base (P8).
``` python
# P1: Encapsulamiento (usando @property para getters/setters)
class Mobile(IMobileComponent):
    def __init__(self, brand, model, price, stock):
        self._brand = brand
        self._model = model
        
        # P1: Usamos el setter para validar el precio
        self.set_price(price) 
        self._stock = stock

    # Getter de precio (lectura)
    @property
    def price(self):
        return self._price

    # Setter de precio (escritura con validación)
    def set_price(self, value):
        if value <= 0:
            raise ValueError("El precio debe ser positivo.")
        self._price = value

    # P8: Implementación del método de Componente Base
    def get_price(self) -> float:
        # P8: Retorna el precio base (sin decoración)
        return self._price

# P8: Decorator - Clase base abstracta
class MobileDecorator(IMobileComponent):
    def __init__(self, component: IMobileComponent):
        # P8: El decorador contiene el componente (Composición)
        self._component = component

    def get_price(self) -> float:
        # P8: Delega la llamada al componente envuelto
        return self._component.get_price()

# P8: Decorador Concreto (Añade $50)
class InsuranceDecorator(MobileDecorator):
    def get_price(self) -> float:
        # P8: Llama al precio del componente y añade su coste
        return self._component.get_price() + 50.0

# P8: Decorador Concreto (Añade $20)
class ScreenProtectorDecorator(MobileDecorator):
    def get_price(self) -> float:
        # P8: Llama al precio del componente y añade su coste
        return self._component.get_price() + 20.0
```
# 🏭 3. Factory Method y Singleton (P5, P6)
## Implementamos el método de fábrica para la creación de Mobile y garantizamos la instancia única de StoreManager.
```python
# P5: Factory Method Pattern
class MobileFactory:
    @staticmethod
    def create_mobile(type: str, *args, **kwargs) -> Mobile:
        # P5: Encapsula la lógica de creación (si hay subtipos)
        if type == "smartphone":
            # Aquí se puede añadir lógica compleja de inicialización si es necesario
            print("Factory: Creando Smartphone.")
            return Mobile(*args, **kwargs)
        # Podríamos tener un 'FeaturePhone' aquí, pero usamos Mobile para simplificar
        return Mobile(*args, **kwargs)

# P6: Singleton Pattern
class StoreManager:
    """Garantiza que solo exista una instancia para la configuración global."""
    _instance = None

    def __new__(cls):
        # P6: Bloquea la creación de instancias adicionales
        if cls._instance is None:
            cls._instance = super(StoreManager, cls).__new__(cls)
            cls._instance.is_initialized = False # Usado para inicialización única
        return cls._instance

    def __init__(self):
        if not hasattr(self, 'is_initialized') or not self.is_initialized:
            print("StoreManager (Singleton): Inicializando conexión a DB y configs.")
            self.config = {"logging_level": "INFO"}
            self.is_initialized = True

    def get_config(self):
        return self.config
```
# ⚙️ 4. SRP y Observer (P3, P7)
## Separamos la clase InventoryAndBilling y convertimos al InventoryManager en el Sujeto para notificar a los Observadores.
``` python
# P3: SRP - Separación de responsabilidades de InventoryAndBilling
class BillingSystem:
    """Solo se encarga de la lógica de facturación."""
    def generate_invoice(self, total_amount):
        # Lógica de Facturación limpia
        tax_rate = 0.15
        invoice_id = hash(total_amount)
        final_amount = total_amount * (1 + tax_rate)
        print(f"[Billing] Factura generada ID {invoice_id}. Total con impuesto: {final_amount:.2f}")
        return final_amount

# P7: Observer - El InventoryManager es el Sujeto
class InventoryManager:
    """Solo se encarga del stock. También es el Sujeto (notificador)."""
    def __init__(self):
        self._observers = []
        self._stock_level = {} # {mobile_id: quantity}

    # P7: Métodos del Sujeto
    def attach(self, observer: IObserver):
        self._observers.append(observer)

    def _notify(self, mobile):
        # P7: Notifica a todos los observadores con el estado del móvil afectado
        for observer in self._observers:
            observer.update(mobile)

    # Lógica de Inventario
    def reduce_stock(self, mobile, quantity):
        # Asume que ya verificamos stock
        mobile._stock -= quantity # Accedemos directamente al privado para modificar el estado
        print(f"[Inventory] Stock de {mobile._model} reducido. Nuevo stock: {mobile._stock}")
        
        # P7: ¡Notificación clave!
        self._notify(mobile) 

# P7: Observador Concreto
class PurchasingModule(IObserver):
    def update(self, mobile: Mobile):
        if mobile._stock < 5:
            print(f"🚨 [PurchasingModule] ALERTA: Stock bajo para {mobile._model} ({mobile._stock}). ¡Hacer pedido!")
```
# 🧩 5. Strategy y Chain of Responsibility (P4, P10)
## Definimos las estrategias de descuento y construimos la cadena de promociones.
``` python
# P4: Estrategias Concretas de Descuento
class SamsungDiscountStrategy(IDiscountStrategy):
    def calculate_discount(self, base_price: float) -> float:
        return base_price * 0.15 # 15%

class DefaultDiscountStrategy(IDiscountStrategy):
    def calculate_discount(self, base_price: float) -> float:
        return 0.0

# P10: Chain of Responsibility - Clase base del manejador
class AbstractPromotionHandler(IPromotionHandler):
    _next_handler = None # El sucesor en la cadena

    def set_next(self, handler: IPromotionHandler):
        # P10: Construye la cadena
        self._next_handler = handler
        return handler

    def handle(self, request: SaleRequest):
        # P10: Si no maneja la solicitud, la pasa al siguiente
        if self._next_handler:
            return self._next_handler.handle(request)
        return request # Fin de la cadena

# P10: Manejador Concreto 1
class VIPDiscountHandler(AbstractPromotionHandler):
    def handle(self, request: SaleRequest):
        if request.customer_is_vip:
            vip_discount = request.final_price * 0.05
            request.final_price -= vip_discount
            request.discounts_applied.append(f"VIP (5%): {vip_discount:.2f}")
        
        # P10: Propaga la solicitud (incluso si no aplicó el descuento)
        return super().handle(request)

# P10: Manejador Concreto 2
class ShippingHandler(AbstractPromotionHandler):
    def handle(self, request: SaleRequest):
        shipping_cost = 50.0 # Coste por defecto
        if request.final_price > 1500:
            shipping_cost = 0.0 # Envío gratis
            request.discounts_applied.append("Envío Gratis")
        
        request.final_price += shipping_cost
        
        # P10: Propaga la solicitud
        return super().handle(request)
```
# 🏛️ 6. Facade y SRP para el Proceso de Venta (P9, P2)
## Creamos la Fachada para simplificar la interacción del cliente y aplicamos el SRP dividiendo el método de venta.
``` python
# P9: Facade Pattern - Simplifica el acceso a subsistemas complejos
class SaleFacade:
    def __init__(self, inventory: InventoryManager, billing: BillingSystem):
        # P9: La fachada tiene referencias a los subsistemas
        self.inventory = inventory
        self.billing = billing
        # P6: Obtiene la única instancia del StoreManager
        self.manager = StoreManager() 

    # P9: El método único y simplificado que el cliente llama
    def process_full_sale(self, mobile_spec: dict, quantity: int, customer_is_vip: bool):
        
        # P2: El Facade ahora coordina, delegando las tareas a métodos de SRP

        # 1. Preparación y Creación del Objeto (P5/P8)
        mobile = self._prepare_mobile(mobile_spec)
        
        # 2. Aplicar Descuento Base (P4)
        base_discount = self._apply_base_discount(mobile)
        
        # 3. Construir y Ejecutar la Cadena de Promociones (P10)
        final_request = self._run_promotion_chain(mobile, quantity, customer_is_vip, base_discount)
        
        # 4. Finalizar Transacción (P3/P7)
        return self._finalize_transaction(mobile, quantity, final_request)


    # --- P2: Métodos de Responsabilidad Única (SRP) ---

    def _prepare_mobile(self, spec: dict) -> IMobileComponent:
        # P5: Usa la fábrica para crear el objeto base
        base_mobile = MobileFactory.create_mobile(
            type="smartphone",
            brand=spec['brand'], 
            model=spec['model'], 
            price=spec['price'], 
            stock=spec['stock']
        )
        # P8: Aplica decoraciones según sea necesario (simulación)
        if spec.get('add_insurance'):
            base_mobile = InsuranceDecorator(base_mobile)
        return base_mobile

    def _apply_base_discount(self, mobile: Mobile) -> float:
        # P4: Determina la estrategia basada en el estado del objeto
        if mobile._brand == "Samsung":
            strategy = SamsungDiscountStrategy()
        else:
            strategy = DefaultDiscountStrategy()
        
        discount = strategy.calculate_discount(mobile.get_price())
        print(f"Descuento base Strategy: {discount:.2f}")
        return discount

    def _run_promotion_chain(self, mobile: Mobile, quantity: int, customer_is_vip: bool, base_discount: float) -> SaleRequest:
        
        # Creamos el objeto solicitud y aplicamos el descuento base
        request = SaleRequest(mobile, quantity, customer_is_vip)
        request.final_price -= base_discount
        
        # P10: Construimos la cadena: VIP -> Envío
        vip_handler = VIPDiscountHandler()
        shipping_handler = ShippingHandler()
        
        vip_handler.set_next(shipping_handler)
        
        # P10: Ejecutamos la cadena, solo interactuamos con el primer eslabón
        return vip_handler.handle(request)

    def _finalize_transaction(self, mobile: Mobile, quantity: int, request: SaleRequest):
        # P3/P7: Delegamos al InventoryManager (Sujeto)
        if mobile._stock < quantity:
            return "Venta Fallida: Stock insuficiente"

        self.inventory.reduce_stock(mobile, quantity) 
        
        # P3: Delegamos al BillingSystem
        final_bill = self.billing.generate_invoice(request.final_price)
        
        print("\n--- RESUMEN FINAL ---")
        print(f"Descuentos aplicados: {', '.join(request.discounts_applied)}")
        print(f"Precio con impuestos: {final_bill:.2f}")

        return "Venta procesada con éxito (via Facade)."
```
# 🧪 7. Uso del Código Refactorizado
## El uso ahora es mucho más limpio y desacoplado, demostrando que todos los patrones funcionan.
``` python
# Inicialización de Subsistemas
inventory_manager = InventoryManager()
billing_system = BillingSystem()

# P7: Registramos el Observador
purchasing_module = PurchasingModule()
inventory_manager.attach(purchasing_module)

# P9: Inicializamos la Fachada (El único punto de entrada para la venta)
shop_facade = SaleFacade(inventory_manager, billing_system)

# P6: Verificamos el Singleton (ambos acceden a la misma instancia)
manager1 = StoreManager()
manager2 = StoreManager()
print(f"\n[Verificación Singleton] ¿Misma instancia? {manager1 is manager2}")


# --- Venta 1: Samsung (con seguro y VIP) ---
samsung_spec = {
    'brand': 'Samsung', 
    'model': 'Galaxy S23', 
    'price': 1600.00, 
    'stock': 10,
    'add_insurance': True # P8: Aplicar Decorator
}

# P9: Solo llamamos al método simple de la Fachada
print("\n===== VENTA 1: Samsung (VIP, con seguro, Envío Gratis) =====")
shop_facade.process_full_sale(samsung_spec, 1, customer_is_vip=True) 
# P7: La venta reducirá stock y activará la notificación si es necesario.
# P4: La estrategia de Samsung se aplica.
# P10: La cadena (VIP -> Envío) se ejecuta.


# --- Venta 2: Apple (sin VIP, stock bajo) ---
apple_spec = {
    'brand': 'Apple', 
    'model': 'iPhone 15', 
    'price': 490.00, 
    'stock': 4, # Stock bajo para activar P7
    'add_insurance': False
}

print("\n===== VENTA 2: Apple (Sin VIP, Stock Bajo) =====")
shop_facade.process_full_sale(apple_spec, 1, customer_is_vip=False)
# P7: ¡El PurchasingModule debería lanzar una alerta de stock bajo aquí!
```
