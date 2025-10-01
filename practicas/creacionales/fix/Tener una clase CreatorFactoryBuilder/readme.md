# Refactor Creacional – Tener una clase CreatorFactoryBuilder (#38)  
**Alumno:** Sanchez Hernandez Evelyn Belen  
**No. Control:** 21212047  

---

## Contexto  
En el dominio de aplicaciones de gestión de usuarios, es común crear cuentas con diferentes configuraciones (Administrador, Editor, Lector). Un error frecuente es mezclar un Factory con un Builder en una sola clase, rompiendo la cohesión y creando dependencias rígidas.  

Este es el caso del antipatrón CreatorFactoryBuilder, donde se centralizan tanto la lógica de creación (Factory) como la construcción paso a paso (Builder) en un único objeto.

---

##  BadCode – CreatorFactoryBuilder Improvisado  

```csharp
// Antipatrón: CreatorFactoryBuilder
// Mezcla responsabilidades de Factory y Builder en una sola clase.

public class CreatorFactoryBuilder
{
    public User CreateUser(string type, string username, string password)
    {
        // Factory Method mal implementado
        if (type == "admin")
            return new User { Role = "Admin", Username = username, Password = password, Permissions = "ALL" };
        else if (type == "editor")
            return new User { Role = "Editor", Username = username, Password = password, Permissions = "WRITE" };
        else if (type == "reader")
            return new User { Role = "Reader", Username = username, Password = password, Permissions = "READ" };

        return null;
    }

    // Builder mal mezclado
    public User BuildDefaultUser()
    {
        return new User
        {
            Role = "Guest",
            Username = "guest",
            Password = "1234",
            Permissions = "NONE"
        };
    }
}

public class User
{
    public string Role { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Permissions { get; set; }

    public override string ToString() => $"{Role}: {Username} ({Permissions})";
}
```

---

##  Problemas detectados  

1. **Clase Dios (God Object):** `CreatorFactoryBuilder` hace demasiado (crear y construir usuarios).  
2. **Violación del Principio de Responsabilidad Única (SRP):** Factory y Builder juntos generan baja cohesión.  
3. **Rigidez:** agregar un nuevo tipo de usuario requiere modificar toda la clase.  
4. **Dependencias concretas:** se crean objetos `User` directamente dentro de la clase.  
5. **Dificultad para pruebas:** no hay interfaces, lo que dificulta mockear usuarios.  

---

##  Refactor con Patrones Creacionales  

Para resolver el problema, se separaron las responsabilidades:  

- **Factory Method:** encapsula la lógica de creación de usuarios según el rol.  
- **Builder:** permite configurar paso a paso los atributos de un usuario sin sobrecargar la creación.  

### Código Refactorizado  

```csharp
// Producto
public class User
{
    public string Role { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Permissions { get; private set; }

    internal User(string role, string username, string password, string permissions)
    {
        Role = role;
        Username = username;
        Password = password;
        Permissions = permissions;
    }

    public override string ToString() => $"{Role}: {Username} ({Permissions})";
}

// Builder
public class UserBuilder
{
    private string _role;
    private string _username;
    private string _password;
    private string _permissions;

    public UserBuilder WithRole(string role) { _role = role; return this; }
    public UserBuilder WithUsername(string username) { _username = username; return this; }
    public UserBuilder WithPassword(string password) { _password = password; return this; }
    public UserBuilder WithPermissions(string permissions) { _permissions = permissions; return this; }

    public User Build()
    {
        if (string.IsNullOrWhiteSpace(_role)) throw new InvalidOperationException("Role requerido");
        if (string.IsNullOrWhiteSpace(_username)) throw new InvalidOperationException("Username requerido");

        return new User(_role, _username, _password ?? "default", _permissions ?? "NONE");
    }
}

// Factory Method
public abstract class UserFactory
{
    public abstract User CreateUser(string username, string password);
}

public class AdminFactory : UserFactory
{
    public override User CreateUser(string username, string password) =>
        new UserBuilder()
            .WithRole("Admin")
            .WithUsername(username)
            .WithPassword(password)
            .WithPermissions("ALL")
            .Build();
}

public class ReaderFactory : UserFactory
{
    public override User CreateUser(string username, string password) =>
        new UserBuilder()
            .WithRole("Reader")
            .WithUsername(username)
            .WithPassword(password)
            .WithPermissions("READ")
            .Build();
}
```

---

## Justificación del cambio  

- **Cohesión interna:** cada clase cumple un rol específico (Factory crea, Builder construye).  
- **Testabilidad:** se pueden inyectar y testear implementaciones de `UserFactory`.  
- **Flexibilidad:** agregar nuevos roles (p. ej. Editor) requiere solo crear una nueva factory, sin modificar el resto.  
- **Principios SOLID:** SRP y OCP ahora se cumplen.  

---

## Impacto  

- Se eliminó el antipatrón `CreatorFactoryBuilder`.  
- El sistema ahora está desacoplado y preparado para crecer.  
- La creación de usuarios es consistente y validada.  
- La arquitectura permite pruebas unitarias sin dependencias rígidas.  

---

## Conclusión Personal
La práctica me permitió identificar cómo la mezcla innecesaria de patrones en una sola clase genera un código rígido, acoplado y difícil de mantener.  
Al aplicar Factory Method y Builder de manera separada, el sistema ganó en claridad, cohesión y extensibilidad.  
Este ejercicio reforzó mi capacidad para detectar *code smells* y refactorizar con intención, logrando un diseño más profesional y sostenible.
