# **Análisis Estratégico del Anti-Patrón Arquitectónico 'Sistema de Tubería de Estufa' (*Stovepipe System*): Desmantelamiento y Modernización de Sistemas Heredados** 
## **Fundamentos Arquitectónicos y Contextualización del Anti-Patrón** 
### **Introducción al Concepto de Anti-Patrón en Ingeniería de Software** 
   En la ingeniería de software, la gestión de la complejidad y la promoción de la agilidad se basan en la aplicación de patrones arquitectónicos sólidos. Un patrón es una solución recurrente y probada a un problema común. Por contraste, un anti-patrón representa una solución habitual que, a pesar de su aplicación inicial, conduce inevitablemente a consecuencias negativas, destructivas o costosas a largo plazo. 

   El *Stovepipe System* (Sistema de Tubería de Estufa) es uno de los anti-patrones arquitectónicos más significativos en el mundo del software contemporáneo, especialmente en el contexto de la modernización de sistemas heredados.1 Los anti-patrones de esta naturaleza no son meros fallos técnicos aislados; son barreras estratégicas directas que impiden la capacidad de una organización para innovar, escalar y responder a las demandas cambiantes del mercado. Su existencia perpetúa la rigidez y aumenta exponencialmente los costos operativos. 
### **La Arquitectura *Stovepipe*: Definición y Características Intrínsecas** 
   El *Stovepipe Architecture Anti-Pattern* se describe fundamentalmente como una colección de ideas, conceptos y componentes mal relacionados (*ill-related*) que culminan en una arquitectura intrínsecamente frágil (*brittle architecture*).1 Este patrón negativo se observa con frecuencia en sistemas heredados que han evolucionado orgánicamente a lo largo del tiempo.2 

   La característica definitoria de un sistema *stovepipe* es su primordial énfasis en la integración vertical. El sistema se construye de punta a punta para satisfacer un requisito de negocio muy específico, sin considerar la reutilización potencial de sus componentes o su integración horizontal con otras funcionalidades empresariales. Esto crea un silo funcional que opera de forma aislada. 

   Los rasgos distintivos que diagnostican este anti-patrón son: 

   1. **Falta Crítica de Reutilización:** El síntoma principal es la ausencia de reutilización de código. Las funciones de software, incluso aquellas de naturaleza potencialmente general, se desarrollan y se emplean exclusivamente para entradas y propósitos limitados dentro de ese silo vertical. Esto significa que la inversión en desarrollo se duplica constantemente a través de la organización.2 
   1. **Riesgo de Fragilidad del Software:** La inflexibilidad es la consecuencia operativa más dañina. La rigidez resultante, conocida como fragilidad del software (*software brittleness*), implica que incluso cambios menores realizados dentro de una parte del sistema tienen un alto riesgo de causar efectos secundarios imprevistos y regresiones en funciones que deberían ser independientes.1 
### **Delimitación Terminológica y Exclusión de Conceptos Irrelevantes** 
Es fundamental aislar el concepto de *Stovepipe System* en la programación de sus homólogos en otras disciplinas de ingeniería. El término no debe confundirse con la clasificación de sistemas de tuberías mecánicas (como el suministro o retorno hidrónico) en software de diseño asistido como Revit 3, ni con productos físicos como tuberías de chimeneas.4 

De manera similar, el *stovepipe* arquitectónico, aunque implica un "aislamiento" conceptual, es distinto de sistemas puramente aislados diseñados intencionalmente para la seguridad o el rendimiento. Por ejemplo, los sistemas eléctricos aislados de tierra (Sistema IT) se utilizan para garantizar la máxima disponibilidad 5, y el aislamiento térmico interior se enfoca en el confort físico.6 

La distinción clave es que, aunque un *stovepipe* parece aislado superficialmente (un silo vertical), la fragilidad inherente 2 demuestra que no es funcionalmente independiente. Si un 

sistema estuviera verdaderamente aislado y bien acoplado, un cambio interno no debería propagar la fragilidad. La fragilidad del software es, por lo tanto, la métrica clave que diferencia un *stovepipe* (un anti-patrón caracterizado por un acoplamiento rígido u oculto) de un servicio verdaderamente bien aislado (un patrón de diseño deseable). 
## **Causas, Diagnóstico y Riesgos Operacionales** 
### **Factores Habilitadores de la Arquitectura *Stovepipe*** 
La aparición del anti-patrón *stovepipe* rara vez es intencional; suele ser el resultado de una convergencia de presiones organizacionales y fallos técnicos. 

Un factor dominante es la **presión organizacional y táctica**. Las decisiones de desarrollo a menudo están impulsadas por una necesidad urgente de reducir el tiempo de comercialización (*time-to-market*). Los equipos priorizan la entrega de funcionalidad inmediata sobre la adhesión a un plan arquitectónico empresarial que fomente la reutilización. Cada equipo construye lo que necesita, resultando en la replicación de funcionalidades que ya existen en otros silos. Este fenómeno se agrava por el **Síndrome de 'No Inventado Aquí' (NIH)**, una tendencia cultural que lleva a los desarrolladores a rechazar o ignorar componentes compartidos o existentes, favoreciendo la construcción de soluciones específicas para su silo.2 

Otro factor poderoso es el efecto de la **Ley de Conway**. Cuando la estructura de la organización está dividida en silos funcionales verticales (ejemplo: un equipo para nómina, otro para inventario), el software desarrollado por esos equipos inevitablemente replica esa estructura, creando silos técnicos o *stovepipes*. 

Desde una perspectiva técnica, la causa subyacente más crítica es el **fallo en la definición de la granularidad de servicio**.8 Los equipos no logran establecer el tamaño y los límites 

adecuados para los componentes de software, lo que conduce a una mala separación de 

responsabilidades y un acoplamiento excesivo. 
### **Consecuencias Estratégicas y Técnicas del Anti-Patrón** 
El impacto del *stovepipe* se siente directamente en la línea de resultados y en la capacidad de innovación de la empresa. 
1. #### **Costo y Riesgo Elevados** 
El mantenimiento de los sistemas *stovepipe* es inherentemente caro. La falta de reutilización implica que funciones similares deben ser actualizadas, parcheadas y probadas de forma aislada en múltiples sistemas, lo que duplica el esfuerzo de ingeniería.2 Además, la fragilidad estructural introduce un riesgo de regresión constante. 
2. #### **Deterioro de la Agilidad Empresarial** 
La rigidez del *stovepipe* lo convierte en un freno para la agilidad. Cualquier cambio de negocio requiere no solo modificar el silo, sino también lidiar con el acoplamiento apretado. Cuando se busca integrar o modificar procesos, esto exige una orquestación y coreografía de servicios extremadamente complejas entre los silos, lo que ralentiza drásticamente la capacidad de la empresa para responder a nuevas demandas.8 
3. #### **La Amenaza de la 'Gran Bola de Lodo Distribuido'** 
Un riesgo crucial al intentar modernizar un *stovepipe* es recaer en lo que se denomina el anti-patrón de la 'Gran Bola de Lodo Distribuido' (*Big Ball of Distributed Mud*). Esto ocurre si una organización intenta descomponer un *stovepipe* monolítico o heredado en microservicios sin resolver el problema fundamental de la granularidad. El resultado es que las 

complejidades y el acoplamiento rígido del *stovepipe* original se replican y magnifican a través de la red de servicios. 

La única manera de evitar esta recaída es seguir un proceso riguroso para refinar la granularidad de los servicios con cada cambio arquitectónico.8 La fragilidad del *stovepipe* es, 

de hecho, una manifestación de una mala granularidad de servicio, ya sea porque el servicio es demasiado grande o porque, aunque dividido, está mal cortado y fuertemente acoplado. La solución no reside solo en la tecnología (adoptar Microservicios), sino en la metodología: definir la granularidad basándose en capacidades de negocio y límites contextuales, y ajustarla de forma iterativa. 

La matriz a continuación resume el impacto de estas características operativas: Table I: Matriz de Impacto del Anti-Patrón Stovepipe 



|**Dimensión** |**Manifestación** |**Impacto Estratégico** |
| - | - | - |
|Acoplamiento/Integración |Integración Vertical y Rígida |Alto riesgo de fragilidad del software; cambios en un área causan regresiones en otras.2 |
|Reutilización |Funcionalidad limitada a entradas específicas |Duplicación constante de código e inversión (costos elevados).2 |
|Agilidad |Mala granularidad de servicio |Ralentización de *time-to-market* y necesidad de orquestación excesiva para cambios.8 |
## **Análisis Comparativo con Arquitecturas Modernas** 
### **El *Stovepipe* en Contraste con la Arquitectura Orientada a Servicios (SOA)** 
   La Arquitectura Orientada a Servicios (SOA) surgió, en parte, como un intento de resolver los problemas de reutilización y rigidez de los sistemas monolíticos y *stovepipes*. SOA tiene como 

   objetivo fomentar la comunicación fluida entre servicios mediante un acoplamiento flexible.9 

   El componente central de la comunicación en SOA es el Bus de Servicios Empresariales (ESB). El ESB actúa como un hub centralizado y estandarizado que recibe, transforma y distribuye datos, administrando y coordinando los servicios a nivel empresarial.9 Sin embargo, la centralización del ESB presenta riesgos estructurales similares a los del *stovepipe*. El ESB puede convertirse en un Punto Único de Fallo (SPOF) para toda la empresa. Además, si un servicio conectado al bus experimenta una ralentización, el sistema empresarial completo puede verse afectado.10 Esta situación, aunque más sofisticada que el *stovepipe* puro, concentra la rigidez estructural y el riesgo de acoplamiento en un único punto de integración. 
### **Microservicios: El Antídoto al *Stovepipe*** 
La arquitectura de microservicios representa una respuesta arquitectónica moderna y efectiva contra los desafíos impuestos por el modelo *stovepipe*. Los microservicios son un enfoque *cloud-native* donde una aplicación se compone de numerosos componentes pequeños, desplegables de manera independiente y separados por su capacidad de negocio (ej., información de producto, datos de cliente, carrito).9 Esto es la antítesis de la integración vertical y no reusable que define el anti-patrón *stovepipe*. 

Existen diferencias cruciales en la forma en que los microservicios resuelven la rigidez: 

1. **Comunicación y Resiliencia:** Los servicios heredados y SOA a menudo dependen de llamadas síncronas. En contraste, una arquitectura de microservicios prioriza patrones de interacción basados en la comunicación asíncrona, como el *event sourcing* o modelos de publicación/suscripción. Este enfoque es fundamental porque evita la introducción de dependencias en tiempo real, lo que garantiza una mayor resiliencia y reduce la latencia que puede afectar el rendimiento.10 
1. **Independencia de Datos:** En un *stovepipe*, los datos suelen estar centralizados. En una arquitectura de microservicios, la independencia se maximiza asegurando que cada servicio tenga acceso local a todos los datos que necesita, incluso si esto resulta en cierta duplicación de datos.10 Este intercambio—aceptar la complejidad de la duplicación de datos a cambio de ganancias significativas en agilidad y rendimiento—es vital para evitar la centralización y el acoplamiento apretado característicos de los sistemas 

   *stovepipe*. 

La siguiente tabla compara los principios estructurales fundamentales: Table II: Comparación Arquitectónica: Stovepipe vs. Microservicios 



|**Criterio de Diseño** |**Arquitectura Stovepipe (Anti-Patrón)** |**Arquitectura de Microservicios (Patrón)** |
| - | :- | :- |
|Acoplamiento |Alto (acoplamiento apretado y rígido) |Bajo (desacoplamiento radical) 9 |
|Granularidad |Mal definida, orientada a una función vertical |<p>Pequeños componentes independientes, definidos por capacidad de negocio </p><p>9 </p>|
|Reutilización |Nula o muy limitada 2 |Promovida por APIs ligeras y contratos bien definidos |
|Comunicación |Generalmente síncrona, rígida |Preferentemente asíncrona (mayor resiliencia) 10 |
|Riesgo Estratégico |Fragilidad del software, altos costos de mantenimiento 2 |Complejidad de gestión distribuida (evitable con buena granularidad) 8 |
### **Estudio de Caso: La Integración Vertical como Opción Pragmática** 
Aunque el *stovepipe* es considerado un anti-patrón a nivel de la arquitectura empresarial, es importante reconocer que la integración vertical o el aislamiento funcional total puede ser una elección arquitectónica justificada bajo ciertas circunstancias específicas.2 

El principal beneficio de la integración vertical rigurosa es evitar el "infierno de dependencias" (*dependency hell*), donde la complejidad y las incompatibilidades de las librerías externas ralentizan el desarrollo. Al aislar completamente el sistema, el equipo mantiene un control estricto sobre su entorno y ciclo de desarrollo. Un ejemplo citado es el equipo de Microsoft Excel, que optó por evitar dependencias y mantener su propio compilador C. Esta estrategia les permitió controlar la calidad, entregar el producto a tiempo y generar código binario pequeño y multiplataforma.2 Este ejemplo demuestra que el aislamiento (como elección deliberada) no es inherentemente negativo, siempre que no resulte en la fragilidad que define al anti-patrón *stovepipe*. 
## **Estrategias de Remediación y Transformación Digital** 
### **La Migración a la Nube como Catalizador** 
Los desafíos de escalabilidad y agilidad presentados por los *stovepipes* monolíticos inflexibles son una de las principales fuerzas impulsoras detrás de la migración a la nube.11 La migración debe entenderse no como un evento aislado, sino como un **proceso continuo de mejora**.12

Aprovechar la agilidad de la nube exige la adopción de nuevas tecnologías y prácticas, incluyendo arquitecturas de microservicios, computación sin servidor (*serverless*) y la 

implementación de DevOps para sostener la agilidad y la innovación.12 
### **Marco Estratégico de los '7 R's' para Sistemas Heredados** 
La mitigación y el desmantelamiento de los sistemas *stovepipe* requieren una estrategia de migración clara y basada en el valor de negocio. Las siete estrategias de migración (7 R's) ofrecen un marco para la toma de decisiones.13 

1. **Retirar (*Retire*):** Si el *stovepipe* no tiene valor de negocio o está clasificado como una "aplicación zombie" (bajo uso de CPU/memoria), esta es la estrategia más rentable. La jubilación del sistema elimina inmediatamente los costos de mantenimiento y reduce los riesgos de seguridad asociados con el uso de sistemas operativos no compatibles.13 
1. **Retener (*Retain*):** Esta estrategia se aplica a los *stovepipes* que, por razones de cumplimiento (ej., residencia de datos) o dependencia de hardware especializado sin equivalente en la nube, deben permanecer en su entorno de origen. Es una decisión táctica para posponer la refactorización.13 
1. **Recompra (*Repurchase*):** También conocido como "drop and shop," implica reemplazar la funcionalidad del *stovepipe* con una alternativa SaaS. Esta opción evita la necesidad de recodificar o refactorizar la aplicación heredada, reduciendo los costos de licenciamiento e infraestructura.13 
1. **Volver a Alojar (*Rehost - Lift and Shift*):** Trasladar el *stovepipe* a la nube sin realizar ningún cambio en la aplicación.13 Es crucial entender que *Rehost* no rompe el anti-patrón; simplemente traslada su complejidad a un entorno más flexible. Sin embargo, esta táctica es valiosa. Permite acelerar migraciones a gran escala al minimizar 

   la interrupción y el tiempo de inactividad, haciendo que la modernización (Refactorización) sea una tarea diferida que se aborda una vez que la carga de trabajo ya está en la infraestructura nativa de la nube.13 

5. **Redefinir la Plataforma (*Replatform*):** Implica un nivel de optimización limitado, como migrar una base de datos tradicional a un servicio administrado (ej., Amazon RDS). Mejora la eficiencia y reduce costos operativos sin abordar el acoplamiento fundamental del *stovepipe*.13 
5. **Reubicar (*Relocate*):** Mover un gran volumen de servidores o instancias a una región o cuenta diferente de la nube. Es un cambio logístico, no arquitectónico, y no afecta directamente al anti-patrón.13 
5. **Refactorizar o Rediseñar (*Refactor or Re-architect*):** Esta es la única estrategia que ataca y rompe la estructura del *stovepipe*. Implica modificar la arquitectura para aprovechar al máximo las funciones nativas de la nube, típicamente mediante la división de aplicaciones monolíticas en microservicios.12 Sin embargo, esta modernización es la más compleja, consume más tiempo y, por lo tanto, no se recomienda para la fase inicial de una migración a gran escala.13 

La selección de la estrategia debe sopesar la complejidad (principalmente asociada a Refactorizar) contra la necesidad de eliminar el anti-patrón: 

Table III: Evaluación de Estrategias de Migración (7 R's) para Sistemas Stovepipe 



|**Estrategia (R)** |**Definición** |**Efectividad contra Anti-Patrón** |**Complejidad y Riesgo** |
| - | - | :- | :- |
|Retirar (Retire) |Apagar o archivar el sistema 13 |Eliminación total del anti-patrón |<p>Baja (si no hay valor de negocio) </p><p>13 </p>|
|Retener (Retain) |<p>Mantener *on-premises* </p><p>13 </p>|Nula (pospone el problema) |Alta (si el riesgo o cumplimiento es crítico) |
|Recompra (Repurchase) |Reemplazo con SaaS 13 |Eliminación del anti-patrón sin esfuerzo de refactorización |Media (requiere migración de datos y reentrenamiento) |
|Volver a Alojar (Rehost) |<p>Traslado 1:1 a la nube (Lift and Shift) </p><p>13 </p>|Nula (traslada el anti-patrón) |Baja (rápido para grandes volúmenes) |



|Refactorizar (Refactor) |<p>Modificar la arquitectura (división en microservicios) </p><p>12 </p>|Alta (rompe el acoplamiento y la rigidez) |Muy Alta (no recomendado para migraciones a gran escala iniciales) 13 |
| :- | :- | :- | :- |
## **Implementación de la Refactorización Arquitectónica** 
1. ### **El Desmantelamiento del *Stovepipe*: Técnicas de Descomposición** 
Cuando la estrategia elegida es Refactorizar, la técnica más exitosa para desmantelar un sistema *stovepipe* heredado es el **Patrón Estrangulador (*Strangler Fig Pattern*)**. Esta técnica implica construir un nuevo sistema (basado en microservicios) alrededor del *stovepipe* legado, reemplazando gradualmente su funcionalidad. A medida que se implementan nuevas capacidades, se retira la funcionalidad correspondiente del sistema antiguo, hasta que este último pueda ser "estrangulado" y desactivado. 

La clave del éxito reside en la **descomposición basada en el negocio**. Para garantizar que los nuevos servicios no se conviertan en nuevos *stovepipes* distribuidos, la división debe seguir límites contextuales y capacidades claras de negocio (ej., separar el servicio de carrito del servicio de datos de cliente).9 Además, cada nuevo microservicio debe tener su propio acceso local a los datos necesarios, garantizando la independencia y cumpliendo con el principio de desacoplamiento de datos de los microservicios.10 
2. ### **Gobernanza de la Granularidad y Orquestación** 
El principal desafío posterior a la refactorización es la **gobernanza continua de la granularidad**. Si no se mantiene, los nuevos microservicios pueden degenerar en un sistema acoplado tan rígido como el *stovepipe* original, pero con la complejidad adicional de la distribución. 

Para evitar esta recaída, los arquitectos deben implementar un **proceso iterativo de ajuste** 

**de granularidad**. Este proceso exige monitorear continuamente si los cambios introducidos requieren "orquestación o coreografía adicional" entre los servicios.8 Si se detecta una 

necesidad excesiva de coordinación entre servicios, es una señal directa de que la granularidad es incorrecta y debe ser refinada, ya sea haciendo los servicios más grandes o más pequeños para reducir el acoplamiento innecesario. Este enfoque metodológico es fundamental para evitar la 'Gran Bola de Lodo Distribuido'.8 

La comunicación interna debe privilegiar protocolos ligeros como HTTP/REST y JMS, y favorecer la comunicación asíncrona para maximizar la resiliencia.10 Es vital que cada servicio 

refactorizado sea capaz de desplegarse, probarse y operarse de manera independiente. 
## **Conclusiones y Recomendaciones Estratégicas** 
La Arquitectura *Stovepipe* es un anti-patrón costoso y peligroso, impulsado principalmente por la falta de reutilización de código y la mala definición de la granularidad de servicio, resultando en una arquitectura intrínsecamente frágil.2 Esta fragilidad actúa como una barrera directa a la agilidad empresarial, ya que el acoplamiento apretado obliga a una orquestación compleja para cualquier cambio.8 

La modernización es imperativa, siendo la migración a la nube el catalizador clave. Sin embargo, el análisis indica que la estrategia de modernización debe ser matizada: 

1. **Priorizar la Eliminación por Valor:** Se recomienda utilizar las estrategias Retirar y Recompra para eliminar rápidamente los *stovepipes* de bajo valor y las aplicaciones obsoletas, liberando recursos para la modernización de sistemas de misión crítica.13 
1. **Abordar la Fragilidad Estructural mediante Refactorización:** Para sistemas de alto valor que requieren agilidad y escalabilidad, el único camino es el Refactor/Re-architect, implementado mediante el Patrón Estrangulador. Este proceso debe centrarse en la descomposición por capacidades de negocio y la garantía de independencia de datos.9 
1. **Garantizar la Gobernanza Post-Migración:** Implementar un proceso continuo de refinamiento de la granularidad de servicios, monitoreando el acoplamiento y la necesidad de orquestación.8 Esto asegura que la nueva arquitectura de microservicios 

   no degenere en una réplica distribuida del anti-patrón original. 

4. **Adoptar DevOps y Colaboración:** La agilidad obtenida al romper el *stovepipe* solo se mantiene mediante la integración de la automatización CI/CD (DevOps) y el fomento de una cultura de colaboración estrecha entre las unidades de TI y de negocio.12 
#### **Fuentes citadas** 
1. Lesson 133 - Stovepipe Architecture AntiPattern (February 28, 2022) | Developer 

   to Architect, acceso: octubre 22, 2025, <https://developertoarchitect.com/lessons/lesson133.html> 

2. Stovepipe system - Wikipedia, acceso: octubre 22, 2025, <https://en.wikipedia.org/wiki/Stovepipe_system> 
2. Definiciones de clasificaciones de sistemas de tuberías mecánicas en Revit - Autodesk, acceso: octubre 22, 2025, [https://www.autodesk.com/es/support/technical/article/caas/sfdcarticles/sfdcartic les/ESP/Pipe-System-Classifications-difference-between-Hydronic-Supply-Retur n-and-Other.html](https://www.autodesk.com/es/support/technical/article/caas/sfdcarticles/sfdcarticles/ESP/Pipe-System-Classifications-difference-between-Hydronic-Supply-Return-and-Other.html) 
2. Ultimate StovePipe™ (USP) - Security Chimneys, acceso: octubre 22, 2025, <https://securitychimneys.com/product/ultimate-stove-pipe> 
2. Sistema IT: Sistemas aislados de tierra para máxima disponibilidad - Bender Latinamerica, acceso: octubre 22, 2025, <https://www.bender-latinamerica.com/informacion-tecnica/tecnologia/sistema-it/> 
2. Aislamiento térmico interior: ventajas y desventajas - BibLus - ACCA software, acceso: octubre 22, 2025, [https://biblus.accasoftware.com/es/aislamiento-termico-interior-ventajas-y-desv entajas/](https://biblus.accasoftware.com/es/aislamiento-termico-interior-ventajas-y-desventajas/) 
2. Pánel Aislado: 4 Ventajas y Desventajas - DCF Accesorios y Decks, acceso: octubre 22, 2025, <https://dcfacero.com/4-ventajas-y-desventajas-del-panel-aislado/> 
2. Lesson 133 - Stovepipe Architecture AntiPattern - YouTube, acceso: octubre 22, 2025, <https://www.youtube.com/watch?v=zLAh7SkNKUs> 
2. ESB vs. Microservices: What's the Difference? - IBM, acceso: octubre 22, 2025, <https://www.ibm.com/think/topics/esb-vs-microservices> 
2. SOA vs. Microservices: What's the Difference? - IBM, acceso: octubre 22, 2025, <https://www.ibm.com/think/topics/soa-vs-microservices> 
2. Los 5 principales desafíos de la migración a la nube - Check Point Software, acceso: octubre 22, 2025, [https://www.checkpoint.com/es/cyber-hub/cloud-security/what-is-cloud-migrati on/top-5-cloud-migration-challenges/](https://www.checkpoint.com/es/cyber-hub/cloud-security/what-is-cloud-migration/top-5-cloud-migration-challenges/) 
2. Migración a la nube híbrida: Pasos y estrategias clave - Veeam, acceso: octubre 22, 2025, <https://www.veeam.com/es/glossary/hybrid-cloud-migration.html> 
2. Acerca de las estrategias de migración - AWS Guía prescriptiva, acceso: octubre 22, 2025, [https://docs.aws.amazon.com/es_es/prescriptive-guidance/latest/large-migration -guide/migration-strategies.html](https://docs.aws.amazon.com/es_es/prescriptive-guidance/latest/large-migration-guide/migration-strategies.html) 
