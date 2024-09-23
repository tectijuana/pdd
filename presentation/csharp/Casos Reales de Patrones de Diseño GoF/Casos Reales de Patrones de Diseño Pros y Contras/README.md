![logo tec](https://github.com/user-attachments/assets/0b5a18fc-0968-45d2-a1cd-914a75adfa59)


<pre>

	<p align=center>

<b>Tecnológico Nacional de México
Instituto Tecnológico de Tijuana

Departamento de Sistemas y Computación
Ingeniería en Sistemas Computacionales

Semestre:
Agosto - Diciembre 2024

Materia:
Patrones de Diseño

Docente:
M.C. Rene Solis Reyes 

Unidad:
1

Título del trabajo:
Pros y Contras de los Patrones de Diseño

Estudiantes:
Ortiz Garcia Nayeli 21210406
Rodriguez Cruz Luis Fernando 21210421</b>

	</p>

</pre>


<div align="center">		
<h1>Pros y Contras de los Patrones de Diseño</h1>
</div>	

![Portada_Blog__1_](https://github.com/user-attachments/assets/67201be1-ce83-4606-b113-e3b3344990bb)	
	
			
<body>
	
<p style="text-align: =justify;">

# Pros 


## ¿Por qué Debería Aprender Sobre Patrones?
Es posible que puedas trabajar durante años como programador sin conocer un solo patrón de
diseño. De hecho, muchas personas lo hacen. Incluso en ese caso, podrías estar
**implementando patrones sin saberlo**. Entonces, ¿por qué invertir tiempo en aprender sobre
ellos?
  
## 1. Herramientas Probadas para Resolver Problemas Comunes
Los **patrones de diseño** representan un conjunto de **soluciones comprobadas** para
problemas comunes que surgen en el diseño de software. Aunque es posible que nunca te
enfrentes a estos problemas directamente, conocer los patrones sigue siendo útil porque te
ayuda a **resolver una amplia variedad de problemas** mediante el uso de **principios del
diseño orientado a objetos**.
  
Aprender sobre patrones te permite:
- Reconocer problemas de diseño recurrentes.
- Aplicar soluciones probadas de manera más rápida y eficiente.
- Mejorar tus habilidades de diseño y toma de decisiones en la arquitectura de software.
  
## 2. Un Lenguaje Común para el Equipo
Otra gran ventaja de conocer los patrones de diseño es que proporcionan un **lenguaje
común** que puedes utilizar para comunicarte de manera más eficiente con tus compañeros de
equipo. Al mencionar un patrón por su nombre, como un **singleton** o un **factory**, todos en
el equipo comprenderán inmediatamente la idea que estás sugiriendo sin necesidad de entrar
en detalles o explicaciones adicionales.
  
### Ejemplo de Comunicación Eficiente:
- En lugar de explicar en detalle cómo implementar una solución, podrías simplemente decir:
> "Oh, utiliza un singleton para eso."
Si el equipo conoce el patrón, ya sabrán qué significa y cómo aplicarlo, lo que **agiliza la
comunicación** y mejora la colaboración.
  
## 3. Beneficio a Largo Plazo
El conocimiento de los patrones de diseño no solo mejora la calidad de tu código, sino que
también incrementa tu **capacidad para adaptarte a nuevos desafíos** y trabajar de manera
más efectiva en equipos colaborativos. Los patrones de diseño son una forma de compartir
conocimientos y buenas prácticas en el ámbito del desarrollo de software, lo que te permite
contribuir mejor al éxito de un proyecto.

## Conclusión
Como conclusión, aprender sobre patrones no solo te proporciona herramientas para resolver
problemas comunes, sino que también te permite comunicarte de manera más eficiente con tus
compañeros de equipo. Aunque podrías prescindir de ellos, su conocimiento es una **inversión
a largo plazo** en tus habilidades como desarrollador.

# Contras 

## Crítica de los Patrones de Diseño
	
A pesar de su popularidad en el mundo del desarrollo de software, los **patrones de diseño**
no están exentos de críticas. A continuación, se presentan los argumentos más comunes en
contra de su uso y algunos de los posibles inconvenientes que los acompañan.
  
## 1. Chapuzas para un Lenguaje de Programación Débil
Uno de los argumentos más frecuentes en contra del uso de patrones de diseño es que estos
son necesarios cuando se trabaja con **lenguajes de programación que no tienen un nivel
adecuado de abstracción**. En este sentido, los patrones funcionan como una especie de
**"chapuza"** para compensar las carencias del lenguaje elegido.
Por ejemplo, algunos lenguajes modernos de programación permiten soluciones más elegantes
sin necesidad de patrones. Un claro ejemplo es el **patrón Strategy**, que puede
implementarse de manera más eficiente utilizando **funciones anónimas** o **lambdas**. Esto
reduce significativamente la complejidad y la cantidad de código, lo que pone en cuestión si la
implementación tradicional del patrón sigue siendo relevante en lenguajes que ya ofrecen
abstracciones adecuadas.
En resumen, los patrones de diseño pueden parecer soluciones forzadas o innecesarias en
entornos donde el lenguaje de programación ofrece mecanismos más potentes.
  
## 2. Soluciones Ineficientes
Otro argumento común es que los patrones de diseño, al intentar ofrecer soluciones
estandarizadas, a menudo terminan siendo implementados de manera **ineficiente**. Muchos
desarrolladores ven en los patrones una suerte de **dogma** que debe seguirse estrictamente,
sin considerar las particularidades de cada proyecto. Esta práctica de implementar los patrones
“**al pie de la letra**” puede llevar a soluciones que no estén optimizadas para el contexto en el
que se utilizan.
El principal problema aquí es que, al no adaptar los patrones a las necesidades específicas del
proyecto, se pierde flexibilidad. Un patrón que ha sido desarrollado para resolver un problema
genérico puede no ser la mejor solución en un caso particular. En estos casos, el uso ineficiente
de patrones puede aumentar la complejidad del código y reducir el rendimiento general de la
aplicación.
Por lo tanto, es importante que los desarrolladores utilicen los patrones como **guías** y no
como reglas absolutas. La capacidad de adaptar un patrón a las necesidades de un proyecto
específico es clave para evitar soluciones ineficaces.
  
## 3. Uso Injustificado
Existe una famosa frase que describe perfectamente uno de los mayores problemas al que se
enfrentan muchos desarrolladores al familiarizarse con los patrones de diseño: **"Si lo único
que tienes es un martillo, todo te parecerá un clavo."**
Esto se refiere al **uso excesivo e injustificado** de patrones por parte de desarrolladores que,
una vez que los aprenden, tienden a aplicarlos en cualquier situación, incluso cuando una
solución más simple sería perfectamente válida. El entusiasmo por aplicar los patrones puede
llevar a que se utilicen en situaciones en las que su uso no está justificado, agregando
innecesariamente complejidad al código.
Un ejemplo común de esto es el uso del **patrón Singleton**, que, aunque puede ser útil en
ciertos escenarios, a menudo se implementa sin que realmente exista una necesidad de

restringir la creación de instancias de una clase. En muchos casos, la implementación de este
patrón no solo es innecesaria, sino que también puede dificultar el mantenimiento y las pruebas
del código.
Es esencial que los desarrolladores no caigan en la trampa de usar patrones simplemente
porque existen, sino que evalúen cuidadosamente si un patrón es la mejor solución para el
problema en cuestión.
  
## Conclusión
Los patrones de diseño son herramientas poderosas, pero deben ser utilizados con **criterio** y
**moderación**. Si bien proporcionan soluciones estandarizadas a problemas comunes, su uso
inadecuado o excesivo puede llevar a ineficiencia, complejidad innecesaria y dificultades en la
adaptación de los proyectos. Los desarrolladores deben enfocarse en utilizar los patrones
cuando realmente añadan valor, adaptándolos a las características específicas del proyecto y
evitando implementaciones rígidas que no consideren el contexto particular.
Al final del día, los patrones son guías, no dogmas, y el verdadero arte del desarrollo de
software reside en saber cuándo y cómo usarlos correctamente.


</p>

</body>
