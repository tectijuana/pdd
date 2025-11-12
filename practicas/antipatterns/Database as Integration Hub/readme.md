# Database as Integration Hub
Ana Cristina Gutiérrez Martínez | 21211959
Fecha: 21 - Octubre - 2025

## ¿Qué es "Database as Integration Hub"?
El antipatrón Database as Integration Hub se refiere a la práctica de utilizar una base de datos única como el punto central para integrar múltiples sistemas o servicios. 
En este escenario, todos los sistemas acceden directamente a la base de datos para leer, insertar o actualizar datos, sin que exista una capa de integración o comunicación 
}explícita entre ellos.

Esta práctica se considera una mala práctica porque genera un acoplamiento fuerte entre los sistemas y la estructura interna de la base de datos. Esto reduce la 
independencia de cada sistema, dificultando su evolución y mantenimiento. Además, centralizar toda la integración en la base de datos provoca problemas de rendimiento 
y limita la escalabilidad del sistema en su conjunto.

## Ejemplo Técnico (python)
El siguiente ejemplo en Python ilustra este antipatrón utilizando una base de datos SQLite que actúa como hub de integración para tres servicios que realizan operaciones 
directas sobre la misma tabla Orders:

``` py
import sqlite3

# Conexión a la base de datos central
conn = sqlite3.connect('integration_hub.db')
cursor = conn.cursor()

# Creación de tabla Orders (ejecutar una sola vez)
cursor.execute('''
CREATE TABLE IF NOT EXISTS Orders (
    OrderID INTEGER PRIMARY KEY,
    CustomerID INTEGER,
    Status TEXT
)
''')
conn.commit()

# Servicio A: inserta una nueva orden
def service_a_insert_order(order_id, customer_id):
    cursor.execute('INSERT INTO Orders (OrderID, CustomerID, Status) VALUES (?, ?, ?)',
                   (order_id, customer_id, 'Pending'))
    conn.commit()

# Servicio B: consulta órdenes pendientes
def service_b_get_pending_orders():
    cursor.execute('SELECT * FROM Orders WHERE Status = ?', ('Pending',))
    return cursor.fetchall()

# Servicio C: actualiza el estado de una orden
def service_c_update_order_status(order_id, new_status):
    cursor.execute('UPDATE Orders SET Status = ? WHERE OrderID = ?', (new_status, order_id))
    conn.commit()

# Uso de los servicios
service_a_insert_order(1, 1001)
pending_orders = service_b_get_pending_orders()
print('Órdenes pendientes:', pending_orders)

service_c_update_order_status(1, 'Completed')
pending_orders_after = service_b_get_pending_orders()
print('Órdenes pendientes después de actualizar:', pending_orders_after)

conn.close()
```
Este código muestra cómo tres servicios distintos interactúan directamente con la misma base de datos, lo que ejemplifica el antipatrón.

## ¿Qué consecuencias tiene el uso de este antipatron? 
Tiene varias consecuencias negativas:
- Mantenimiento complicado: Cualquier cambio en el esquema de la base de datos puede afectar a todos los sistemas que dependen de ella.
- Problemas de rendimiento: La base de datos central se convierte en un cuello de botella, pues recibe un alto volumen de consultas y operaciones concurrentes,
lo que puede degradar la velocidad y disponibilidad del sistema.
- Escalabilidad limitada: La arquitectura no permite escalar individualmente cada servicio o distribuir la carga, ya que todos dependen del mismo repositorio de datos.
- Dependencias ocultas: Al no existir una capa de integración explícita, la comunicación y las reglas de negocio quedan dispersas, dificultando la comprensión y
evolución del sistema.

## ¿Qué solución tiene? 
Para corregir este antipatrón se recomiendan las siguientes buenas prácticas:
- Desacoplamiento mediante APIs: Cada sistema debe gestionar su propia base de datos y exponer funcionalidades a través de APIs REST u otras interfaces que permitan
la comunicación controlada entre servicios.
- Arquitectura orientada a eventos: Implementar sistemas de mensajería asíncrona (por ejemplo, Kafka o RabbitMQ) para intercambiar información mediante eventos,
reduciendo la dependencia directa.
- Patrones CQRS y Event Sourcing: Separar las responsabilidades de lectura y escritura y mantener un registro de eventos que faciliten la escalabilidad y el mantenimiento.
- Microservicios: Adoptar una arquitectura en la que cada microservicio sea responsable de sus datos y se comunique con otros mediante contratos bien definidos,
eliminando la necesidad de compartir una base de datos común.

## Reflexión
El antipatrón Database as Integration Hub puede parecer una solución sencilla para integrar sistemas, pero a largo plazo genera un fuerte acoplamiento, limita la 
escalabilidad y dificulta el mantenimiento. Centralizar la comunicación a través de una base de datos compartida crea cuellos de botella y aumenta el riesgo de errores 
al modificar el esquema. Por ello, es fundamental adoptar arquitecturas desacopladas, mediante APIs o eventos, que promuevan la independencia de los servicios y 
permitan una evolución más flexible y sostenible del sistema.
