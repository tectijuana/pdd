# 🧑‍💻 Revisión Técnica – Patrón Memento (Estrada Solano Abraham)

### 👤 Revisor
**Jesus Antonio Triana Corvera – C20212681**

### 📌 PR Revisado
**Patrón GoF – Memento: Ruptura de encapsulamiento entre objetos.**

---

## ✅ Checklist Técnica

| Ítem | ¿Cumple? | Comentarios |
|------|-----------|-------------|
| **1. Identifica al menos un code smell estructural real** | ✅ Sí | Se detecta correctamente la ruptura de encapsulamiento (acceso directo a `nivel`, `vida`, `puntaje`). Se explica el impacto en coherencia del estado. |
| **2. Aplica un patrón estructural adecuado** | ✅ Sí (Memento) | Memento evita exponer el estado interno y permite restauración controlada. La elección está bien justificada. |
| **3. La solución es coherente y mejora el diseño** | ✅ Sí | Roles claros: Originator (`Juego`), Memento (`Memento`), Caretaker (`Historial`). Se restablece el encapsulamiento y se habilita undo. |
| **4. El código es legible y está bien estructurado** | ✅ Sí | Métodos y nombres claros; visibilidad adecuada (`private`/público). Flujo sencillo de entender. |
| **5. El PR está bien documentado y argumentado** | ✅ Sí | Incluye problema, código malo, solución refactor, beneficios y conclusión. Presentación clara. |

---

## 🧠 Observaciones Técnicas

- El **code smell** está bien enmarcado: modificación externa del estado rompe encapsulamiento y genera inconsistencias.
- La implementación de **Memento** es canónica y separa responsabilidades:  
  - **Originator**: encapsula y crea/restaura snapshots.  
  - **Memento**: inmutable, expone solo lo necesario al originator.  
  - **Caretaker**: administra historial sin conocer detalles internos.
- Uso de **`Stack<Memento>`** para undo secuencial: decisión apropiada para revertir estados en orden LIFO.
- Se recupera **SRP** y se reduce el acoplamiento entre componentes.

---

## 🛠️ Sugerencias de Mejora

1. **Robustez en Caretaker**  
   - Agregar guardas en `Deshacer()` para evitar `Pop()` en pila vacía:  
     ```csharp
     public bool TryDeshacer(out Memento? m)
     {
         if (historial.Count == 0) { m = null; return false; }
         m = historial.Pop();
         return true;
     }
     ```
2. **Control de memoria / límites de historial**  
   - Considerar un **límite configurable** de snapshots o política de compactación si el número de estados puede crecer mucho.
3. **Pruebas unitarias**  
   - Añadir tests para:  
     - Restauración múltiple en cadena (varios undo).  
     - No corrupción del estado cuando el historial está vacío.  
     - Inmutabilidad efectiva del `Memento`.
4. **Comparativa breve en la documentación**  
   - Contrastar **Memento vs Prototype** (snapshot vs clon) y por qué Memento preserva mejor el encapsulamiento.

---

## 🧪 Verificación Manual (escenario mínimo)

1. `SetEstado("Nivel 1", 3, 100)` → `Guardar()`.  
2. `SetEstado("Nivel 2", 2, 250)` → `Guardar()`.  
3. `Deshacer()` y `Restaurar()` → estado vuelve a **Nivel 2**.  
4. Repetir `Deshacer()` → estado vuelve a **Nivel 1**.  
5. Intentar `Deshacer()` con historial vacío → debe manejarse sin excepción.

---

## 🧾 Beneficios Observados

- **Encapsulamiento** restaurado: cero acceso directo a campos internos.  
- **Reversibilidad** del estado con bajo acoplamiento.  
- **Extensibilidad** para features de **Undo/Redo**.  
- **Claridad de responsabilidades** entre las tres entidades del patrón.

---

## 🎯 Conclusión del Revisor

> Excelente aplicación del **patrón Memento** para resolver la ruptura de encapsulamiento. El refactor mejora significativamente la mantenibilidad y la seguridad del estado. Solo faltan pequeñas mejoras de robustez (guardas y límites del historial) y cobertura con pruebas.

---

## 🏁 Calificación según rúbrica

| Criterio | Puntos | Evaluación |
|---------|--------|------------|
| 1. Identificación de Code Smells | 25 | ✅ 25 |
| 2. Aplicación del patrón adecuado | 20 | ✅ 20 |
| 3. Refactor funcional y coherente | 20 | ✅ 20 |
| 4. Legibilidad y estructura del código | 15 | ✅ 15 |
| 5. Argumentación y documentación del PR | 15 | ✅ 15 |
| 6. Profesionalismo y presentación | 5 | ✅ 5 |
| **Total** | **100** | **Excelente (100/100)** |

---
