# 🧑‍💻 Revisión de Código - Patrones de Comportamiento (GoF)

## 👤 Revisor: Joel Cuevas Estrada

📌 PR Revisado: temas/fix/¿Strategy o chain of responsability?/perrreview.md

✅ Checklist Técnica

| Ítem | ¿Cumple? | Comentarios |
|------|-----------|-------------|
| 1. Identifica al menos un code smell de comportamiento | [✅] Sí | Identifica correctamente el exceso de condicionales (code smell: **Long Conditional / Spaghetti Code**) y cómo esto viola el principio OCP. |
| 2. Aplica un patrón de comportamiento adecuado | [✅] Sí | Aplica el **Patrón Strategy**, que es el más apropiado para eliminar condicionales múltiples y permitir intercambiar algoritmos de descuento. |
| 3. La solución mejora la estructura del diseño | [✅] Sí | Se reduce el acoplamiento al separar las estrategias y se aumenta la cohesión en cada clase. |
| 4. El código es legible y está bien estructurado | [✅] Sí | La implementación es clara, con nombres coherentes y clases bien definidas. |
| 5. El PR está bien documentado y argumentado | [✅] Sí | La explicación del patrón y sus beneficios está bien fundamentada, con un análisis previo del problema. |

🧠 Observaciones Técnicas

El patrón **Strategy** fue implementado de forma correcta y completa.  
Cada tipo de cliente ahora tiene su propia estrategia de descuento, lo que permite **extender el sistema sin modificar el código existente**.  
El uso del diccionario en el contexto (`CalculadoraDescuentos`) facilita el acceso dinámico a las estrategias sin condicionales, lo cual mejora el diseño y la legibilidad.

🛠️ Sugerencias de Mejora

- **Inyección de dependencias:** Podrías considerar inyectar las estrategias a través del constructor para permitir una configuración más flexible o pruebas unitarias más sencillas.  
- **Nombres de estrategias:** Para mayor claridad, podrías agregar el sufijo `Strategy` a las clases (por ejemplo, `DescuentoRegularStrategy`) y mantener consistencia con la convención del patrón.  
- **Validaciones adicionales:** Si se prevé que los tipos de cliente puedan venir de fuentes externas, se podría validar el tipo antes de buscarlo en el diccionario.  

🎯 Conclusión del Revisor

Excelente implementación del **Patrón Strategy**.  
El código pasó de un diseño rígido y acoplado a uno **modular, mantenible y extensible**, alineado con los principios SOLID.  
Se logra una clara separación de responsabilidades entre el cálculo del descuento y la lógica del cliente.  
Solo se recomienda mejorar la **inyección de dependencias y nomenclatura** para alcanzar un diseño aún más limpio y escalable.
