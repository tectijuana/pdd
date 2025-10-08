# Asistencia de Inteligencia Artificial

## **Prompts utilizados**:

 - "Genera un codigo con codesmells y un adaptador con múltiples responsabilidades en C#"

 - "Genera los pasos a seguir para implementar correctamente el patron Adapter"

 - ¿Qué mejoras puedo impelementar en el código?   

 - ¿Separa el codigo original en partes y explicame que mejoras de las propuestas puedo utilizar en esa seccion en especifico?

## **Herramientas utilizadas**:
 - ChatGPT (GPT-4o)

## **Cambios y validación**:

  - El código original fue modificado para adaptar las nuevas funcionalidades 
  - Se aplico el Principio de Responsabilidad Única (SRP) se reviso que cada clase tiene una única responsabilidad clara validación, conversión, logging, etc y funcionan correctamente.
  - Se implemento la Inversión de Dependencias (DIP) se reviso que el adaptador dependa de otras interfaces (ILogger, INotificationService, ITransactionRepository) y no de implementaciones concretas.
  - Pruebas y mantenimiento con inyección de dependencias y separación de responsabilidades, el código se reviso y es fácil de entender.


## **Reflexión personal**:

Los modelos de IA son excelentes guias de estudio en temas que podriamos considerar complejos, con los pasos prooporcionados y la division del codigo en partes mas pequeñas la IA me ayudo a aplicar de manera mas eficiente el patron adapter, ademas las implementaciones sugeridas por el chatbot no fueron complejas. Aunque la IA pueda pareser la panacea del conocimiento siempre debemos verificar y consultar las fuentes de informacion y realizar testeos en caso de solicitar segmentos de codigo.
## **Fecha**: 2025-09-30

 **Plataforma de hardware utilizada**: Windows 11 (usando GitHUb)
