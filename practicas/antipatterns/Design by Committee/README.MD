#  Antipatrón: Design by Committee

*Estudiante: Abraham Estrada Solano*
---
## 1. Comprensión del Antipatrón

**Design by Committee** es un antipatrón que ocurre cuando las decisiones de diseño se toman colectivamente por un grupo grande de personas, sin un liderazgo claro ni una visión unificada.  
Cada integrante busca incluir su propia opinión o interés, lo que resulta en un producto final inconsistente, sobrecargado o carente de identidad.

Este fenómeno es común en equipos donde:
- **No hay una figura de diseño** o dirección técnica con autoridad final.  
- **Las decisiones se toman por consenso** sin criterios objetivos.  
- Se intenta **complacer a todos los involucrados** (clientes, usuarios, directivos, desarrolladores, etc.).

 **Por qué es una mala práctica:**  
Porque diluye la coherencia del producto, retrasa los procesos y genera soluciones mediocres, producto del compromiso entre múltiples puntos de vista en lugar de un diseño sólido.

---

## 2. Ejemplo Técnico

### Ejemplo en desarrollo de software
Un equipo desarrolla una nueva interfaz web y cada miembro del comité propone cambios visuales o funcionales:
- El gerente pide más botones visibles.  
- El diseñador quiere un diseño minimalista.  
- El programador quiere menos carga visual.  
- El cliente pide incluir todos los requerimientos en una sola pantalla.

El resultado es una interfaz confusa, con diferentes estilos y sin jerarquía visual clara.  

### Ejemplo en código
```javascript
// Cada miembro del comité agrega su propia validación sin consenso
function validarFormulario(form) {
  if (!form.nombre) alert("Falta nombre");
  if (form.nombre.length < 3) alert("Nombre muy corto");
  if (form.nombre.length > 20) alert("Nombre muy largo");
  if (form.nombre.includes("!")) alert("Caracteres inválidos");
  // Otro miembro del equipo agrega su propio control redundante
  if (form.nombre.match(/[0-9]/)) alert("No debe contener números");
}
```
El resultado es código repetitivo, sin una regla unificada ni un enfoque común de validación.

---

## 3. Consecuencias

Los efectos más comunes del antipatrón *Design by Committee* son:

- **Pérdida de identidad del producto:** las decisiones se fragmentan y el diseño carece de coherencia.  
- **Aumento de la complejidad:** se agregan funciones innecesarias o redundantes.  
- **Retrasos y sobrecostos:** las revisiones y debates prolongan la entrega.  
- **Dificultad de mantenimiento:** al no haber un criterio central, el código o el diseño se vuelve inconsistente.  
- **Disminución de la calidad percibida:** el usuario final recibe un producto menos funcional y menos intuitivo.

---

##  4. Solución Correctiva

Para evitar o corregir *Design by Committee*, se recomienda:

1. **Establecer liderazgo técnico o de diseño:** definir una persona o rol (por ejemplo, *Product Owner* o *Lead Designer*) con autoridad final en las decisiones de diseño.  
2. **Definir una visión clara del producto:** documentar objetivos, valores y principios de diseño desde el inicio.  
3. **Basar decisiones en evidencia:** usar datos, pruebas de usuario y métricas, no solo opiniones.  
4. **Aplicar patrones de diseño y guías de estilo:** mantener coherencia mediante *Design Systems* o estándares visuales.  
5. **Fomentar la colaboración estructurada:** reuniones breves y con propósito, evitando el exceso de opiniones sin filtro.

**Patrón alternativo:** *Single Vision Leadership* o *Design Authority Pattern*, donde una persona o equipo pequeño garantiza la coherencia general del diseño.

---


 **Autor:** Abraham Estrada Solano 
 **Fecha:** 21 de Octubre 2025  
 **Materia / Proyecto:** Patrones de Diseño de Software  
