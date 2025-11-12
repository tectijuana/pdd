# Antipatrán: Vendor Lock-In
#
Daniel Omar Gonzalez Martinez 21212342 

## 1. Qué es Vendor Lock-In 

Vendor Lock-In ocurre cuando tu sistema depende demasiado de un proveedor específico para servicios, herramientas o infraestructura, y cambiar a otro proveedor es difícil o costoso.

Es una mala práctica porque:
- Limita la flexibilidad de tu sistema.
- Genera dependencia de un solo proveedor.
- Incrementa los costos y riesgos si el proveedor cambia sus condiciones o servicios.
- Complica la migración a otras plataformas.

Se presenta con frecuencia cuando se usan APIs propietarias, servicios de nube exclusivos o formatos de datos propios del proveedor.


## 2. Ejemplo Técnico 

Si suponemos que hay una aplicación que usa solo servicios de AWS (S3, Lambda, DynamoDB):

```python
import boto3

s3 = boto3.client('s3')
s3.upload_file('archivo.txt', 'mi-bucket', 'archivo.txt')

dynamodb = boto3.resource('dynamodb')
tabla = dynamodb.Table('Usuarios')
tabla.put_item(Item={'ID': '123', 'Nombre': 'Daniel'})
```

Si queremos cambiar a Google Cloud, tendriamos que reescribir mucho código, modificar infraestructura y adaptarte a nuevas APIs. Esto puede generar retrasos y costos adicionales.


## 3. Consecuencias 

- **Mantenimiento:** Cambiar o actualizar componentes es difícil.
- **Rendimiento:** Limitado por las capacidades del proveedor.
- **Escalabilidad:** Escalar puede ser costoso o depender de límites del proveedor.
- **Riesgo:** Cambios de precios o discontinuidad de servicios afectan directamente al sistema.
- **Innovación limitada:** Difícil integrar nuevas tecnologías si dependen de otros proveedores.


## 4. Cómo evitarlo 

1. Usar **estándares abiertos** y APIs universales.
2. Crear **capas de abstracción** para separar tu código del proveedor.
3. Utilizar **contenedores** como Docker o Kubernetes para facilitar la portabilidad.
4. Diseñar pensando en **múltiples proveedores**.
5. Aplicar patrones de diseño como **Adapter** para cambiar fácilmente de proveedor.

Ejemplo sencillo con Adapter:

```python
class StorageAdapter:
    def upload(self, filename, bucket, object_name):
        raise NotImplementedError

class S3Adapter(StorageAdapter):
    def upload(self, filename, bucket, object_name):
        import boto3
        s3 = boto3.client('s3')
        s3.upload_file(filename, bucket, object_name)

class GCPStorageAdapter(StorageAdapter):
    def upload(self, filename, bucket, object_name):
        from google.cloud import storage
        client = storage.Client()
        bucket = client.bucket(bucket)
        blob = bucket.blob(object_name)
        blob.upload_from_filename(filename)

# Uso
storage_service = S3Adapter()  # Cambiar por GCPStorageAdapter() si se necesita
storage_service.upload('archivo.txt', 'mi-bucket', 'archivo.txt')
```
Esta estrategia permite cambiar de proveedor con un esfuerzo mínimo.
