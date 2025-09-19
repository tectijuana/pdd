# Control de deuda técnica con herramientas CI/CD modernas
## Jesus Antonio Triana Corvera C20212681
### Definición general
La **deuda técnica** es el costo adicional que surge cuando se elige una **solución rápida** (atajo de diseño, código mal estructurado, falta de pruebas) en lugar de una solución más adecuada pero más costosa en tiempo.  
El término fue introducido por **Ward Cunningham** en 1992 y sigue siendo fundamental en ingeniería de software moderna.

### Características principales
- Es **inevitable**: todo sistema acumula deuda técnica, incluso si se siguen buenas prácticas.  
- Es **medible**: herramientas modernas permiten cuantificarla (ej. líneas duplicadas, complejidad ciclomática).  
- Es **gestionable**: puede priorizarse como parte del backlog.  
- Es **contagiosa**: si no se controla, se expande a otros módulos del sistema.  

### Tipologías de deuda técnica
1. **Intencional y estratégica**: asumida con plena consciencia, como acelerar un prototipo.  
2. **No intencional**: aparece por falta de experiencia o desconocimiento técnico.  
3. **Por evolución tecnológica**: software que queda obsoleto frente a librerías y frameworks modernos.  
4. **De proceso**: prácticas de desarrollo mal definidas, falta de pruebas, integración manual.  

### Impacto en el ciclo de vida del software
- **Desarrollo inicial**: deuda baja, pero se generan compromisos por decisiones rápidas.  
- **Mantenimiento**: la deuda empieza a ralentizar la entrega de nuevas funciones.  
- **Escalabilidad**: arquitecturas frágiles colapsan bajo nuevas exigencias.  
- **Producción**: errores recurrentes, incidentes y altos costos de corrección.  

**Ejemplo real**: El bug del *Ariane 5* (1996) costó $370M por reutilizar código de Ariane 4 sin refactorizar ni probar, acumulando una deuda técnica no gestionada.

---

### Herramientas CI/CD que ayudan a gestionar deuda técnica

- **SonarQube**  
  - Detecta *code smells*, duplicación de código, cobertura de pruebas y métricas de mantenibilidad.  
  - Define **Quality Gates**: si no se cumplen umbrales (ej. menos de 5% de duplicación, 80% de cobertura), el pipeline falla.  
  - Métrica clave: *Technical Debt Ratio* (proporción entre deuda y esfuerzo de desarrollo).  

- **Snyk / OWASP Dependency-Check**  
  - Identifican vulnerabilidades conocidas en dependencias de proyectos.  
  - Notifican CVEs críticas que deben ser parchadas antes de desplegar.  
  - Ejemplo: una librería vulnerable como *Log4j* puede ser bloqueada automáticamente.  

- **Dependabot / Renovate**  
  - Automatizan la actualización de dependencias y frameworks.  
  - Previenen la acumulación de deuda por versiones obsoletas.  

- **Jenkins / GitHub Actions / GitLab CI**  
  - Permiten pipelines que integran pruebas, análisis estático, análisis dinámico y despliegues condicionales.  
  - Implementan estrategias de **“shift-left testing”**: detectar problemas lo más temprano posible.  

---

### Comparación: Pipeline con vs. sin control de deuda

| Aspecto                   | Sin control de deuda técnica               | Con control de deuda técnica (Quality Gates) |
|---------------------------|--------------------------------------------|----------------------------------------------|
| **Velocidad inicial**     | Alta, se despliega rápido.                 | Moderada, validaciones adicionales.          |
| **Mantenimiento**         | Costoso, cada cambio rompe funcionalidades.| Más estable, los refactors se aplican antes. |
| **Seguridad**             | Vulnerabilidades pasan desapercibidas.     | Alertas automáticas y parches inmediatos.    |
| **Moral del equipo**      | Baja, trabajar en “código sucio” frustra.  | Alta, código más legible y predecible.       |
| **Escalabilidad**         | Riesgo de colapso.                         | Preparado para crecimiento futuro.           |

**Conclusión**: invertir tiempo en control de deuda técnica ralentiza entregas iniciales, pero ahorra enormes costos en el mediano y largo plazo.

---

### Refactorización como estrategia de pago de deuda
Refactorizar significa **mejorar el código sin alterar su comportamiento externo**.  
Ejemplos comunes:
- Dividir métodos grandes en métodos más pequeños.  
- Eliminar duplicación usando herencia, interfaces o patrones.  
- Renombrar variables y clases para mayor legibilidad.  
- Añadir pruebas unitarias a código crítico.  

**Cómo CI/CD apoya el refactor**:
- SonarQube detecta complejidad y duplicación → obliga al refactor.  
- Jenkins/GitHub Actions ejecutan pruebas tras refactor para garantizar que nada se rompe.  

### Patrones de diseño como prevención de deuda
- **Strategy**: sustituye condicionales por polimorfismo → evita duplicación.  
- **Factory Method / Abstract Factory**: centralizan la creación de objetos → evitan código rígido.  
- **Observer**: desacopla componentes → facilita escalabilidad.  
- **Decorator**: agrega responsabilidades sin modificar código existente.  
- **Singleton (bien aplicado)**: previene instanciación excesiva, aunque mal uso puede generar deuda.  

### Relación con calidad y principios de diseño
- **SOLID**: aplicar principios de responsabilidad única, inversión de dependencias, etc., evita deuda.  
- **Clean Code (Robert C. Martin)**: fomenta nombres claros, funciones cortas y simplicidad.  
- **Refactor continuo**: apoyado en pipelines CI/CD, convierte la deuda en algo monitoreado constantemente.  

---

### En entornos profesionales
- Casos reales:  
  - **Equifax (2017)**: no actualizó *Apache Struts*, deuda técnica derivó en la filtración de datos de 147M personas.  
  - **Knight Capital (2012)**: software obsoleto causó pérdidas de 440M USD en 45 minutos.  

### Reflexión crítica
La deuda técnica **no debe eliminarse completamente**, ya que en ocasiones estratégicas es válida para sacar más rápido el producto sin embargo, no debemos abusar de ella. Considero que los patrones de diseño nos pueden ayudar a evitar este tipo de deudas, al ofrecer un "manual" de soluciones aplicadas al software que ya han demostrado funcionar, brindando así alternativas claras para la resolución de problemas. El verdadero problema surge cuando la deuda técnica **no se documenta, mide ni controla**, pues en ese punto se convierte en una carga invisible que frena el desarrollo. En este sentido, las herramientas CI/CD cumplen un rol esencial al actuar como **auditores automáticos** que hacen visible lo invisible y permiten gestionar la deuda de manera disciplinada y sostenible.  

---

