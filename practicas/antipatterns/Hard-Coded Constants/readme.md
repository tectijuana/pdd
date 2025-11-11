# Hard-Coded Constants ( Antipatrones fuera de GoF)
# Nombre: Ruben Campos Rivas
# Numero de control: 21211926
---

## 🧩 ¿Qué es y por qué se considera una mala práctica?
El **antipatrón Hard-Coded Constants** se presenta cuando se colocan valores fijos directamente dentro del código fuente, como números mágicos, tasas, rutas de archivos, claves o URLs.  
Por ejemplo:  
- Escribir `0.16` para representar el IVA.  
- Usar `"/home/user/data/"` como ruta dentro del código.  

Esto se considera una **mala práctica** porque:
- Rompe el **principio de flexibilidad y mantenibilidad del software**.  
- Obliga a modificar el código fuente cada vez que un valor cambia.  
- Dificulta la reutilización del código en distintos entornos o países.  
- Aumenta la posibilidad de errores al duplicar valores en múltiples lugares.  

En resumen, **el código se vuelve rígido, difícil de mantener y propenso a errores**.

---

## ❌ Ejemplo de código con el antipatrón
```python
def calcular_precio_total(precio):
    iva = precio * 0.16      # Constante codificada
    descuento = precio * 0.10  # Otra constante fija
    total = precio + iva - descuento
    return total

print(calcular_precio_total(100))
```
### 🔍 Problemas detectados:
- Si el IVA cambia de 16% a 18%, hay que editar el código.  
- Si el mismo valor se repite en varios módulos, puede haber inconsistencias.  
- No se puede reutilizar el código fácilmente en diferentes contextos (por ejemplo, otro país con distinto impuesto).  

### ✅ Código refactorizado (solución correcta)

**config.py (archivo de configuración):**
```python
IVA = 0.16
DESCUENTO = 0.10

### main.py (lógica del programa)
```python
import config

def calcular_precio_total(precio):
    iva = precio * config.IVA
    descuento = precio * config.DESCUENTO
    total = precio + iva - descuento
    return total

print(calcular_precio_total(100))
```
### 💡 Buenas prácticas aplicadas
- Los valores importantes están fuera del código fuente principal.  
- Se facilita la modificación, lectura y reutilización.  
- La lógica del programa se mantiene limpia y clara.  
- Se evita recompilar o tocar el código fuente para simples cambios.  

### ⚙️ Análisis de efectos en mantenimiento, rendimiento y escalabilidad
| **Aspecto**      | **Efecto negativo del antipatrón** |
|------------------|-----------------------------------|
| **Mantenimiento** | Requiere buscar y reemplazar valores manualmente en múltiples lugares del código. Incrementa el riesgo de errores. |
| **Rendimiento**   | Aunque el impacto es bajo en ejecución, aumenta el tiempo de desarrollo y retrasa actualizaciones. |
| **Escalabilidad** | Dificulta adaptar el software a distintos entornos (por ejemplo, servidores, países o monedas). |
| **Seguridad**     | Si se codifican claves o rutas sensibles, pueden quedar expuestas en el repositorio. |
