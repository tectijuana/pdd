# Refactor Creacional - NullObject

#Joshua Ruiz Lopez 21212363

## Resumen del ejercicio

En este caso generamos un *case study* con un problema clásico: **no aplicar el patrón NullObject en la creación de objetos**, lo que produce múltiples validaciones `if (obj == null)` dispersas por el código. A partir de este anti-patrón, se identifican problemas de diseño, se propone una refactorización usando NullObject y se documenta el proceso para entregar vía Pull Request.

---

## Instrucciones para el flujo de trabajo (comandos)

```bash
# 1. Clonar repo
git clone <REPO_URL>
cd <REPO>

# 2. Crear nueva rama
git checkout -b fix/Joshuaruiz/creacionales/nullobject

# 3. Añadir archivos modificados y commitear
git add practicas/creacionales/fix/Joshuaruiz/readme.md
git commit -m "Refactor Creacional - NullObject: eliminación de null checks con NullObject"

# 4. Push a la nueva rama
git push origin fix/Joshuaruiz/creacionales/nullobject

# 5. Crear Pull Request con título y cuerpo (usa el template de este readme)
```

---

## Problema detectado (caso realista)

En un sistema de **Usuarios** (ejemplo de dominio), la clase `UserService` obtiene objetos `User` desde un repositorio. Cuando el usuario no existe, el repositorio retorna `null`. Esto provoca que en múltiples lugares del código haya validaciones como:

```java
if(user == null){
   // manejar error
}else{
   user.sendNotification(...);
}
```

---

## 🔍 Problemas detectados

1. **Repetición de validaciones `null`**: múltiples clases deben validar si el objeto retornado es `null`, generando código duplicado y difícil de mantener.

2. **Violación del Principio de Abierto/Cerrado**: al querer cambiar el comportamiento de "usuario inexistente" se requiere modificar múltiples clases que hacen la validación `if (user == null)`.

3. **Acoplamiento excesivo**: la lógica de negocio depende de la implementación concreta de `null` como indicador de ausencia.

---

## 🛠 Patrón aplicado

Se introduce el **patrón NullObject** creando una clase `NullUser` que implementa la interfaz `User` pero con comportamiento vacío (sin efectos). Así eliminamos la necesidad de comprobar `null` explícitamente.

---

## Código: Antes / Después (ejemplo en Java)

### Antes (anti-patrón)

```java
public class UserService {
    private final UserRepository repo;

    public void notifyUser(String id, String msg){
        User user = repo.findById(id);
        if(user == null){
            System.out.println("Usuario no encontrado");
        }else{
            user.sendNotification(msg);
        }
    }
}
```

### Después (con NullObject)

```java
// Interfaz común
public interface User {
    void sendNotification(String msg);
}

// Implementación real
public class RealUser implements User {
    private final String name;
    public RealUser(String name){ this.name = name; }
    public void sendNotification(String msg){
        System.out.println("Enviando a "+name+": "+msg);
    }
}

// NullObject
public class NullUser implements User {
    public void sendNotification(String msg){
        // No hace nada
    }
}

// Repositorio
public class UserRepository {
    private final Map<String, User> data = new HashMap<>();
    public User findById(String id){
        return data.getOrDefault(id, new NullUser());
    }
}

// Servicio ya sin if-null
public class UserService {
    private final UserRepository repo;
    public UserService(UserRepository repo){ this.repo = repo; }
    public void notifyUser(String id, String msg){
        User user = repo.findById(id);
        user.sendNotification(msg);
    }
}
```

---

## 💡 Justificación del cambio

* Elimina código repetitivo con validaciones `null`.
* Encapsula el comportamiento "vacío" en un objeto especializado (`NullUser`).
* Aumenta la cohesión y facilita mantenimiento: cambios futuros se hacen en `NullUser`.
* Mejora testabilidad: se puede verificar comportamiento sin necesidad de `null` explícito.

---

## 🔄 Impacto

* Se reducen errores por omisión de validación `null`.
* Se mejora legibilidad y claridad.
* Se prepara la arquitectura para extensión sin modificar clientes (cumple OCP).

---

## 📝 Formato del Pull Request

**Título del PR:** `Refactor Creacional - NullObject - Joshuaruiz (anexo LLM)`

**Nombre del problema:** No aplicar patrón NullObject en creación.

### 🔍 Problemas detectados

* Repetición de validaciones `null`.
* Dependencia en `null` como estado válido.
* Código duplicado y difícil de mantener.

### 🛠 Patrón aplicado

* Implementación de **NullObject** (`NullUser`) para encapsular comportamiento vacío.

### 💡 Justificación del cambio

* Cohesión y claridad aumentadas.
* Eliminación de condicionales repetidos.
* Flexibilidad ante cambios en el manejo de "usuario inexistente".

### 🔄 Impacto

* Código más simple y fácil de probar.
* Reducción de bugs relacionados con `NullPointerException`.
* Preparación para escalabilidad.

---

## ✅ Cambios realizados 

* `UserRepository` ahora retorna `NullUser` en lugar de `null`.
* Creada la clase `NullUser` que implementa `User`.
* Eliminados condicionales `if(user == null)` en `UserService`.

---
