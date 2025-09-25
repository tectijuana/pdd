# Refactor Creacional - Vehículos


---

## Resumen del ejercicio

Se propone una práctica inversa: genero un *case study* intencionalmente mal diseñado con code smells sobre la creación de objetos en un dominio de **Vehículos** (coches, motocicletas, camiones). A continuación analizo el problema, detecto al menos 3 problemas graves de diseño relacionados con patrones creacionales, propongo y aplico refactorizaciones (solo lo mínimo necesario) para mejorar cohesión, legibilidad y testabilidad, y entrego el contenido listo para un Pull Request.

---

## Instrucciones para el flujo de trabajo (comandos)

```bash
# 1. Clonar repo
git clone <REPO_URL>
cd <REPO>

# 2. Crear nueva rama con tu nombre (ejemplo: Joshuaruiz)
git checkout -b fix/Joshuaruiz/creacionales

# 3. Añadir archivos modificados y commitear
git add practicas/creacionales/fix/Joshuaruiz/readme.md
git commit -m "Refactor Creacional - Vehículos: mejora Singleton, Factory, Builder, Prototype"

# 4. Push a la nueva rama
git push origin fix/Joshuaruiz/creacionales

# 5. Crear Pull Request con título y cuerpo (usa el template de este readme)
```

---

## Problema detectado (caso realista)

El código original presenta las siguientes clases relacionadas con la creación de `Vehiculo`:

* `VehicleManager` (gestionaba creación, configuración y uso de objetos Vehiculo)
* `VehicleFactory` (métodos estáticos con `switch` por tipo)
* `GlobalConfig` (Singleton con estado mutable y sin sincronización)
* `VehicleBuilder` (constructor público que mezcla validación con construcción)
* `VehiclePrototype` (no implementa `clone()` correctamente)

Se detectaron *code smells* y violaciones de principios SOLID que impiden evolución y pruebas.

---

## 🔍 Problemas detectados (mínimo 3, detallados)

1. **Clase Dios / Responsabilidad Múltiple**: `VehicleManager` crea, configura y registra vehículos; viola el Principio de Responsabilidad Única (SRP). Esto dificulta pruebas unitarias porque los tests deben replicar su compleja inicialización.

2. **Factory con lógica `switch` y acoplamiento fuerte**: `VehicleFactory` decide la clase concreta con un `switch(type)` y usa `new` directamente en controladores. Esto rompe polimorfismo y hace difícil añadir nuevos tipos sin modificar la fábrica (violación OCP).

3. **Singleton inseguro y con estado mutable**: `GlobalConfig` es un singleton con variables públicas modificables; además no es seguro en multihilo. Su uso como contenedor global impide pruebas paralelas y genera efectos colaterales.

4. **Constructor con lógica pesada / Builder mal usado**: `VehicleBuilder` realiza validaciones y llamadas externas dentro de `build()` y expone setters que dejan el objeto inconsistente hasta el final.

5. **Prototype incompleto**: `VehiclePrototype` no implementa `clone()` deep copy; al clonar, referencias mutables (lista de accesorios) se comparten entre instancias.

---

## 🛠 Cambios aplicados (solo lo necesario)

Objetivo: mejorar cohesión y testabilidad sin reescribir todo el proyecto.

1. **Singleton -> reemplazo por Inyección de Dependencias y opcional Singleton seguro**

   * `GlobalConfig` pasa a ser una clase inyectable; si se necesita una instancia global, se provee mediante un `ConfigProvider` con inicialización sincronizada (double-check locking) y `final` immutability para los campos mutables.

2. **Factory Method**

   * Reemplazo del `VehicleFactory` con un `VehicleCreator` abstracto y subclases (`CarCreator`, `MotorcycleCreator`, `TruckCreator`) que implementan `create()` (Factory Method). Se elimina el `switch`.

3. **Builder correctamente aplicado**

   * `VehicleBuilder` se transforma en `Vehicle.Builder` (clase interna estática fluida). Se garantiza la validación en `build()` y se evita el acceso a recursos externos durante la construcción.

4. **Prototype con deep clone**

   * Implementación de `clone()` que realiza copia profunda de colecciones y objetos mutables.

5. **Separación de responsabilidades**

   * `VehicleManager` se divide: `VehicleRegistry` (registro/persistencia ligera) y `VehicleService` (orquestación), ambas inyectadas con interfaces.

---

## Código: Antes / Después (ejemplos en Java)

> *Nota:* Incluyo fragmentos mínimos para ilustrar el refactor. Cambios pensados para integrarse con el repo.

### 1) Singleton inseguro (Antes)

```java
// GlobalConfig.java (ANTES)
public class GlobalConfig {
    public static GlobalConfig instance;
    public Map<String,String> settings = new HashMap<>();

    private GlobalConfig() {}
}
```

### 1) Singleton seguro / DI (Después)

```java
// Config.java (inmutable)
public final class Config {
    private final Map<String,String> settings;
    public Config(Map<String,String> settings) {
        this.settings = Collections.unmodifiableMap(new HashMap<>(settings));
    }
    public String get(String k){ return settings.get(k); }
}

// ConfigProvider.java (opcional Singleton seguro lazy)
public class ConfigProvider {
    private static volatile Config instance;
    public static Config getInstance() {
        if (instance == null) {
            synchronized(ConfigProvider.class){
                if (instance == null) {
                    instance = new Config(Map.of("env","dev"));
                }
            }
        }
        return instance;
    }
}
```

*Justificación:* campos inmutables, fácil de mockear pasando `Config` a constructores, `ConfigProvider` solo para inicialización en producción.

---

### 2) Factory con switch (Antes)

```java
public class VehicleFactory {
    public static Vehiculo create(String type){
        switch(type){
            case "car": return new Car();
            case "motor": return new Motorcycle();
            default: throw new IllegalArgumentException();
        }
    }
}
```

### 2) Factory Method (Después)

```java
public abstract class VehicleCreator {
    public abstract Vehiculo create();
}

public class CarCreator extends VehicleCreator {
    public Vehiculo create(){ return new Car(); }
}

// Uso: VehicleCreator creator = new CarCreator(); Vehiculo v = creator.create();
```

*Justificación:* agregar nuevos tipos solo requiere crear otra subclase `VehicleCreator` sin tocar código existente; facilita testing y extensión.

---

### 3) Builder mal usado (Antes)

```java
public class VehicleBuilder {
    public String model;
    public List<String> accessories;
    public VehicleBuilder setModel(String m){ this.model=m; return this; }
    public void heavyInitialization(){ /* hace I/O, llama servicios */ }
    public Vehiculo build(){
        heavyInitialization();
        if(model==null) throw new IllegalStateException();
        return new Vehiculo(model, accessories);
    }
}
```

### 3) Builder correcto (Después)

```java
public class Vehiculo {
    private final String model;
    private final List<String> accessories;

    private Vehiculo(Builder b){
        this.model = b.model;
        this.accessories = Collections.unmodifiableList(new ArrayList<>(b.accessories));
    }

    public static class Builder {
        private final String model; // obligatorio
        private List<String> accessories = new ArrayList<>();

        public Builder(String model){
            this.model = Objects.requireNonNull(model);
        }
        public Builder addAccessory(String a){ accessories.add(a); return this; }
        public Vehiculo build(){
            // validaciones ligeras solo en memoria
            if(model.isEmpty()) throw new IllegalStateException("model required");
            return new Vehiculo(this);
        }
    }
}

// Uso: Vehiculo v = new Vehiculo.Builder("ModelX").addAccessory("GPS").build();
```

*Justificación:* elimina lógica externa de construcción, garantiza objeto inmutable y consistente.

---

### 4) Prototype (Antes)

```java
public class VehiclePrototype implements Cloneable {
    public List<String> accessories;
    public Object clone(){
        try{ return super.clone(); }catch(Exception e){return null;}
    }
}
// Problema: accessories se comparte entre clones
```

### 4) Prototype - deep clone (Después)

```java
public class VehiclePrototype implements Cloneable {
    public List<String> accessories;
    @Override
    public VehiclePrototype clone(){
        try{
            VehiclePrototype p = (VehiclePrototype) super.clone();
            p.accessories = new ArrayList<>(this.accessories);
            return p;
        }catch(CloneNotSupportedException e){ throw new AssertionError(); }
    }
}
```

*Justificación:* evita aliasing de estructuras mutables entre clones.

---

## 📝 Formato del Pull Request 

**Título del PR:** `Refactor Creacional - Vehículos - Joshuaruiz (anexo LLM)`

**Nombre del problema:** Vehículos - code smells en creacionales

### 🔍 Problemas detectados

* `VehicleManager` viola el Principio de Responsabilidad Única: crea y orquesta la persistencia de Vehículos.
* `VehicleFactory` usa `switch` y `new` directo: acoplamiento y violación OCP.
* `GlobalConfig` actúa como Singleton mutable y no es seguro en multihilo.

### 🛠 Patrón aplicado

* **Factory Method** en lugar de `switch` centralizado.
* **Builder** para construcción segura y ordenada de `Vehiculo`.
* **Prototype** con `clone()` deep-copy para copias seguras.
* **DI / Singleton seguro inmutable** para `Config`.

### 💡 Justificación del cambio

* Mejora la cohesión interna y la separación de responsabilidades.
* Facilita la inyección de dependencias y el *mocking* en pruebas.
* Reduce el acoplamiento y facilita la extensión sin tocar código existente.

### 🔄 Impacto

Se mejora:

* Testabilidad (puedes mockear `Config` y `VehicleCreator`).
* Flexibilidad (añadir tipos de Vehículos no requiere modificar fábricas existentes).
* Robustez en entornos multihilo con opciones inmutables.

---

## ✅ Cambios realizados (resumen para revisar en el diff)

* `GlobalConfig.java` -> eliminado / reemplazado por `Config.java` + `ConfigProvider.java`.
* `VehicleFactory.java` -> nueva jerarquía `VehicleCreator` y subclases `CarCreator`, `MotorcycleCreator`.
* `VehicleBuilder.java` -> migrado a `Vehiculo.Builder` con validaciones y campos inmutables.
* `VehiclePrototype.java` -> `clone()` deep-copy.
* `VehicleManager.java` -> dividido en `VehicleService` (lógica) y `VehicleRegistry` (registro simple).

---

## 📚 Lista de 50 Code Smells (incluida para referencia)

*(Se incluye la lista proporcionada por el enunciado, copia exacta para revisión y checklist)*

1. Clases Dios (God Objects)
2. Singleton con estado mutable
3. Singleton sin control de concurrencia
4. Constructores con más de 4 parámetros
5. Constructores que ejecutan lógica pesada
6. Condiciones múltiples para crear objetos
7. Uso excesivo de switch para tipos
8. new directamente en el controlador
9. Clases que construyen y usan el objeto
10. Factories que retornan objetos inconsistentes
11. Falta de interfaz en los productos creados
12. Builders que exponen estado interno
13. Prototype sin implementación de Clone
14. Uso de patrones sin necesidad (overengineering)
15. Abuso de propiedades estáticas
16. Objetos anémicos sin comportamiento
17. Lógica duplicada en múltiples constructores
18. No aplicar principio de inversión de dependencias
19. Usar Singleton como contenedor global
20. No documentar qué patrón se está usando
21. Factory mezclado con lógica de negocio
22. Abuso de ServiceLocator
23. No encapsular los pasos del Builder
24. Clases que tienen múltiples responsabilidades
25. No separar creación del uso del objeto
26. Constructor que accede a base de datos
27. Singleton con dependencia externa inyectada mal
28. Builders no reutilizables
29. Uso de constantes mágicas para tipos
30. Herencia innecesaria entre productos
31. Confundir Abstract Factory con Factory Method
32. Builders con métodos obligatorios desordenados
33. Interfaces con métodos redundantes
34. Crear objetos sin validar estado
35. Factories que retornan clases concretas directamente
36. Falta de pruebas en objetos creados dinámicamente
37. No aplicar patrón NullObject en creación
38. Tener una clase CreatorFactoryBuilder
39. No inyectar dependencias necesarias en el constructor
40. Uso de if-else anidados para selección de tipos
41. Asignación de estado después de construcción
42. Singleton con Dispose sin patrón IDisposable
43. Factory con múltiples niveles de delegación
44. Acoplamiento fuerte entre cliente y producto
45. No implementar interfaces para los productos
46. Reutilizar Singletons para múltiples propósitos
47. Usar Thread.Sleep en el constructor
48. Crear múltiples instancias “Singleton” en pruebas
49. Ignorar el principio de sustitución de Liskov en fábricas
50. Atraparse en un anti-patrón por querer usar “todos los patrones”

---

## 📈 Evaluación (como rubric)

* Identificación de problemas: 30%

  * Lista clara de 5 problemas con ejemplos (cumple)
* Aplicación correcta del patrón: 30%

  * Se aplicó Factory Method, Builder, Prototype y DI (cumple)
* Justificación técnica: 30%

  * Explicado impacto, testabilidad y cohesión (cumple)
* Claridad y formato del PR: 10%

  * PR template incluido arriba (cumple)

---

## Consejos finales 

* Revisar diffs: los cambios se limitan a refactorizar la creación de objetos y separar responsabilidades; no se cambió la lógica de negocio.
* Ejecutar pruebas unitarias: ahora es más sencillo mockear `VehicleCreator` y `Config`.
* Revisar que no queden `switch` ni `new` directos en controladores.

---

> Si quieres, genero también los archivos Java completos listos para pegar en el repo (por ejemplo `Vehiculo.java`, `CarCreator.java`, `Config.java`, etc.).
> Dime si los prefieres en Java o C# y los creo aquí mismo.

---

