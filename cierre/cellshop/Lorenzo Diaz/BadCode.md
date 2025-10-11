# Código Fuente Original con 10 Malas Prácticas (Code Smells)
``` Python
# Problema 1: Atributos públicos
# Problema 5: Se instancia directamente (se verá en el uso)
class Mobile:
    """Representa un celular en la tienda, con atributos públicos."""
    def __init__(self, brand, model, price, stock):
        self.brand = brand      # P1: Atributo público
        self.model = model      # P1: Atributo público
        self.price = price      # P1: Atributo público
        self.stock = stock      # P1: Atributo público

# Problema 6: Múltiples instancias permitidas
class StoreManager:
    """Clase de gestión global que no garantiza una única instancia."""
    def __init__(self):
        print("¡ADVERTENCIA! Se creó una nueva instancia de StoreManager.")
        self.config = {"logging_level": "DEBUG"}

    def get_config(self):
        return self.config

# Problema 3: Mezcla de responsabilidades (Inventario y Facturación)
class InventoryAndBilling:
    """Maneja la gestión de inventario Y la lógica de facturación."""
    
    # ------------------ Lógica de Inventario ------------------
    def check_stock(self, mobile):
        # P3: Lógica de Inventario
        return mobile.stock > 0

    def reduce_stock(self, mobile, quantity):
        if mobile.stock >= quantity:
            mobile.stock -= quantity
            return True
        return False
    
    # ------------------ Lógica de Facturación ------------------
    def generate_invoice(self, total_amount):
        # P3: Lógica de Facturación
        tax_rate = 0.15
        invoice_id = hash(total_amount) # Simulación de ID
        final_amount = total_amount * (1 + tax_rate)
        print(f"Factura generada: ID {invoice_id}. Impuesto: {tax_rate*100}%. Total: {final_amount}")
        return final_amount

# Problema 4: Lógica rígida y Problema 10: Lógica de promociones embebida/rígida
class Promotion:
    """Contiene lógica rígida de descuento por marca."""
    def calculate_discount(self, mobile, base_price):
        # P4: Lógica rígida (violación de OCP/Strategy)
        if mobile.brand == "Samsung":
            return base_price * 0.15  # 15% de descuento
        elif mobile.brand == "Apple":
            return base_price * 0.05   # 5% de descuento
        else:
            return 0.0

# Problema 2: Método largo y confuso
# Problema 7: Cambios no notifican a otros módulos
# Problema 9: Acoplamiento fuerte (interactúa directamente con InventoryAndBilling)
# Problema 10: Lógica de promociones dentro del proceso de venta
class SaleProcessor:
    """Procesa una venta con demasiadas responsabilidades en un solo método."""
    def __init__(self):
        # P9: Fuerte acoplamiento a una implementación concreta
        self.system = InventoryAndBilling()
        self.promo_engine = Promotion()
        self.manager = StoreManager() # P6: Dependencia a una clase no Singleton

    # P2: Método ProcessSale es largo y confuso
    def process_sale(self, brand, model, quantity, customer_is_vip=False):
        
        # P5: Instanciación directa (asumimos que buscamos el objeto en una "base de datos" simple)
        # En un escenario real, Mobile se instanciaría aquí, si no existe.
        mobile = Mobile(brand, model, 1000.00, 5) 
        
        print(f"\n--- Procesando Venta de {quantity} x {mobile.model} ---")

        # 1. Verificar Inventario (P2: Parte del método largo)
        if not self.system.check_stock(mobile):
            return "Venta fallida: Stock insuficiente."

        # 2. Aplicar Descuento Base por Marca (P2, P10: Parte de la lógica de promoción)
        base_discount = self.promo_engine.calculate_discount(mobile, mobile.price * quantity)
        total_price = (mobile.price * quantity) - base_discount
        print(f"Descuento base aplicado: {base_discount:.2f}")
        
        # 3. Lógica de Promociones Embebida (P10: Lógica secuencial dentro del método)
        if customer_is_vip:
            vip_discount = total_price * 0.05
            total_price -= vip_discount
            print(f"Descuento VIP aplicado: {vip_discount:.2f}")

        if total_price > 1500:
            shipping_cost = 0 # Envío gratis
        else:
            shipping_cost = 50
        total_price += shipping_cost
        print(f"Costo de envío: {shipping_cost:.2f}")
        
        # 4. Reducir Stock (P2: Parte del método largo)
        self.system.reduce_stock(mobile, quantity) 
        
        # 5. Generar Factura (P2: Parte del método largo)
        final_bill = self.system.generate_invoice(total_price)
        
        # 6. Registrar Log
        print(f"Log: Transacción completada. Precio final: {final_bill:.2f}")

        # P7: Falta de notificación: ningún otro módulo sabe que el stock ha cambiado.

        return "Venta procesada con éxito."

# ====================================================================
# Uso del Código (Demo de las malas prácticas)
# ====================================================================

# P6: Se permiten múltiples instancias de un gestor global
manager1 = StoreManager()
manager2 = StoreManager()
print(f"Misma instancia? {manager1 is manager2}") # Output: False (Mala práctica)

# P1: Acceso directo a atributos públicos
mobile = Mobile("Apple", "iPhone 13", 800.00, 10)
print(f"Precio inicial: {mobile.price}")
mobile.price = -500.00 # P1: Se permite un estado inválido
print(f"Precio manipulado: {mobile.price}")

processor = SaleProcessor()
processor.process_sale("Samsung", "Galaxy S23", 2, customer_is_vip=True)
processor.process_sale("Apple", "iPhone 13", 1, customer_is_vip=False)
