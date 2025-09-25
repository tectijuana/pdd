# Técnicas de Refactorización en TypeScript usando AST

## 1. Introducción
La refactorización consiste en **mejorar el código sin alterar su funcionalidad**.  
En TypeScript, una herramienta poderosa para refactorizar es el **AST (Abstract Syntax Tree)**, que representa la estructura del código en forma de árbol.  

- **Código fuente ➝ Parser ➝ AST ➝ Transformaciones ➝ Código refactorizado**  
- Permite cambios automáticos, seguros y consistentes.  

---

## 2. Técnicas de Refactorización con AST

### 2.1 Renombrado de variables y funciones
- **Problema:** Nombres poco claros.  
- **Solución con AST:** Localizar identificadores en el árbol y reemplazarlos de forma sistemática.  

```ts
// Antes
let x = 10;
function calc(y: number) { return x + y; }

// Refactorizado con AST
let baseValue = 10;
function calculateTotal(amount: number) { return baseValue + amount; }
```

---

### 2.2 Extracción de funciones
- **Problema:** Funciones muy largas.  
- **Solución:** Identificar bloques de código en el AST y moverlos a una nueva función.  

```ts
// Antes
function process(data: number[]) {
  let sum = 0;
  for (let d of data) sum += d;
  console.log(sum / data.length);
}

// Después
function process(data: number[]) {
  console.log(average(data));
}

function average(data: number[]) {
  return data.reduce((a, b) => a + b, 0) / data.length;
}
```

---

### 2.3 Eliminación de código duplicado
- AST permite detectar patrones repetidos y consolidarlos en una sola función o clase.  

```ts
// Antes
function areaRect(w: number, h: number) { return w * h; }
function areaSquare(s: number) { return s * s; }

// Después
function area(shape: { w: number, h: number }) { return shape.w * shape.h; }
```

---

### 2.4 Aplicación de patrones de diseño
Con AST se pueden automatizar transformaciones hacia **patrones comunes** (Factory, Singleton, Strategy).  
Ejemplo: convertir instanciaciones repetidas en una **Factory** centralizada.

---

## 3. Relación con Calidad y Patrones
- **Calidad:** menos duplicación, mejor legibilidad, más mantenible.  
- **Patrones de diseño:** AST ayuda a transformar código hacia estructuras más limpias.  
- **Prevención de errores humanos:** la refactorización manual es propensa a fallos, AST lo evita.  

---

## 4. Ejemplos y Comparaciones
- **Manual vs AST**:  
  - Manual: buscar/reemplazar con riesgo de errores.  
  - AST: cambios conscientes de contexto (ej. distingue `id` de variable vs string `"id"`).  

- **Ejemplo práctico**:  
  - **Editor**: VS Code usa el AST de TypeScript para sugerir refactorizaciones.  

---

## 5. Análisis Crítico y Reflexión
- **Ventajas:** Precisión, automatización, escalabilidad.  
- **Limitaciones:** Curva de aprendizaje, herramientas adicionales (ej. `ts-morph`, `typescript-eslint`).  
- **Reflexión:** El uso de AST es más relevante en equipos grandes y proyectos de larga duración. Para proyectos pequeños puede ser demasiado costo/beneficio.  

---

## 6. Conclusión
Refactorizar con AST en TypeScript:
- **Asegura consistencia.**  
- **Facilita patrones de diseño.**  
- **Eleva la calidad del software.**

En el contexto del curso de **PDD (Patrones de Diseño y Desarrollo)**, AST es una herramienta que conecta teoría (patrones) con práctica (refactorización automatizada).

---

## 7. Referencias

- Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1994). *Design Patterns: Elements of Reusable Object-Oriented Software.* Addison-Wesley.  
- Fowler, M. (2018). *Refactoring: Improving the Design of Existing Code (2nd Edition).* Addison-Wesley.  
- TypeScript Team. (2023). *TypeScript Handbook.* Microsoft. https://www.typescriptlang.org/docs/  
- Ts-morph. (2023). *TypeScript Compiler API wrapper.* https://ts-morph.com  

---

## 8. ANEXO: Uso de LLMs

### Prompts utilizados
> "Técnicas de refactorización en TypeScript usando AST con ejemplos prácticos para un trabajo en .md"  

### Reflexión ética
El uso de LLMs (como ChatGPT) apoyó en la **redacción estructurada** y en la **claridad de ejemplos**, pero no sustituye la comprensión personal.  
Es mi responsabilidad **validar, complementar y citar fuentes académicas** para garantizar la calidad y honestidad del trabajo.
