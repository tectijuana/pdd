# 🎨 Refactorización con Patrón Decorator

## 📌 Contexto
Este proyecto corresponde a la práctica de **Calidad de Software**, en la cual se realiza una **refactorización de código mal estructurado** aplicando el patrón **Decorator** en C# (.NET 8).  

El **Code Smell detectado** fue:  
👉 *Decoradores que no llaman al componente base*, lo cual rompe la cadena de responsabilidades y hace que las funcionalidades decoradas no se ejecuten correctamente.

---

## 📊 Rúbrica de Evaluación
Actividad: Refactorización de Patrones Estructurales (GoF)  
Modalidad: Individual  
Duración estimada: 50 minutos  
Formato de entrega: Pull Request en Git con justificación y refactor parcial  
Lenguaje: **C# (.NET 8)**  

| Criterio | Descripción | Puntos |
|----------|-------------|--------|
| 1. Identificación de Code Smells | Detecta correctamente el problema: Decoradores que no llaman al componente base. | 25 |
| 2. Aplicación del patrón adecuado | Se aplica correctamente el patrón **Decorator** resolviendo el problema. | 20 |
| 3. Refactor funcional | El código refactorizado compila y el decorador mantiene la lógica base más la añadida. | 20 |
| 4. Justificación técnica en Pull Request | El PR explica el problema y los beneficios del patrón aplicado. | 15 |
| 5. Calidad del código refactorizado | Código legible, nombres coherentes, buena separación de responsabilidades. | 10 |
| 6. Uso correcto de Git | Rama `fix/nombre-alumno`, commit semántico y PR bien formado. | 5 |
| 7. Profesionalismo y presentación | Redacción clara y entendible para otros desarrolladores. | 5 |

**Total máximo: 100 pts**

---

## 🚀 Instrucciones de Ejecución
1. Clonar el repositorio:
   ```bash
   git clone <url-del-repo>
   cd <carpeta-del-proyecto>
