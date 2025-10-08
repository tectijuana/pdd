# 📌 Análisis de Problemas Estructurales – Uso Innecesario de Bridge

## 🎯 Tema 15: Usar Bridge donde la herencia simple era suficiente

En este documento se detallan **3 problemas estructurales (Code Smells)** identificados al aplicar el patrón Bridge cuando no era necesario. Se explica la justificación del error y cómo debería haberse resuelto con herencia simple.

---

## 🚨 Problema 1: Aplicación del patrón Bridge
### 🔎 Identificación de Code Smell
- Se crearon múltiples clases abstractas e interfaces solo para cumplir con la estructura del Bridge.  
- Estas clases no aportaban lógica adicional, solo actuaban como "paso intermedio".  
- Esto generó **boilerplate innecesario** y mayor curva de aprendizaje.  
- Se usó Bridge como si se fueran a tener muchas implementaciones de fuente de poder, 
  pero en la práctica siempre existía una sola. Eso generó código repetitivo y difícil de leer.
- *Un boilerplate significa los codigos y textos que se repiten excesivamente sin aportar valor*

### 🔁 Código antes del Bridge
```csharp
    // Clase abstracta que obliga a heredar aunque no aporta mucho.
    // Se usa para "desacoplar", pero en este caso no hacía falta.
    public abstract class Dispositivo
    {
        protected IFuentePoder fuente; // Interfaz innecesaria
        protected Dispositivo(IFuentePoder fuente) => this.fuente = fuente;
        public abstract void Encender(); // Forzamos a sobreescribir sin necesidad
    }

    // Interfaz para la fuente de poder, aunque en realidad siempre será la misma.
    public interface IFuentePoder
    {
        void Suministrar();
    }

    // Clase concreta que implementa el dispositivo (ejemplo: Televisor).
    public class Televisor : Dispositivo
    {
        // Obligados a recibir una "fuente de poder" aunque siempre es la misma.
        public Televisor(IFuentePoder fuente) : base(fuente) { }

        public override void Encender()
        {
            Console.WriteLine("Encendiendo televisor...");
            // Delegamos la acción de encender a la "fuente",
            // pero esta abstracción no tenía variantes reales.
            fuente.Suministrar();
        }
    }
```


### 🔧 Uso de Bridge en este caso
```csharp
    // Implementador: diferentes fuentes de poder
    public interface IFuentePoder
    {
        void Suministrar();
    }

    public class Fuente110V : IFuentePoder
    {
        public void Suministrar() => Console.WriteLine("Energía 110V lista.");
    }

    public class FuenteBateria : IFuentePoder
    {
        public void Suministrar() => Console.WriteLine("Energía desde batería.");
    }

    // Abstracción: cualquier dispositivo que necesita energía
    public abstract class Dispositivo
    {
        protected IFuentePoder fuente;
        protected Dispositivo(IFuentePoder fuente) => this.fuente = fuente;
        public abstract void Encender();
    }

    // Abstracciones refinadas
    public class Televisor : Dispositivo
    {
        public Televisor(IFuentePoder fuente) : base(fuente) { }
        public override void Encender()
        {
            Console.Write("Encendiendo TV → ");
            fuente.Suministrar();
        }
    }

    public class Radio : Dispositivo
    {
        public Radio(IFuentePoder fuente) : base(fuente) { }
        public override void Encender()
        {
            Console.Write("Encendiendo Radio → ");
            fuente.Suministrar();
        }
    }

    // Ejemplo de uso
    class Program
    {
        static void Main()
        {
            var tv = new Televisor(new Fuente110V());
            var radio = new Radio(new FuenteBateria());

            tv.Encender();     // Encendiendo TV → Energía 110V lista.
            radio.Encender();  // Encendiendo Radio → Energía desde batería.
        }
    }
```

### ✅ Justificación de la Solución
El Bridge se usó pensando en "variar la fuente de poder". Sin embargo, en el sistema **no existía más de una implementación real de fuente de poder**. Bastaba una clase concreta.
Ahora **Bridge** separa el Dispositivo que necesita la energía según su fuente de poder, permitiendo cambiar la fuente sin cambiar el dispositivo y viceversa

---

## 🚨 Problema 2: Aplicación del patrón Bridge
### 🔎 Identificación de Code Smell
En el código original, cada clase de Documento (PDF, Word) estaba acoplada directamente a una clase de Exportador (Pantalla, Impresora).
Esto provocaba:

- Duplicación de código.
- Falta de flexibilidad.

*Un ejemplo es que si se quisiera agregar otro exportador (por ejemplo, correo electrónico), habría que modificar todas las clases de documentos.*

### ❌ Antes (sin Bridge, código rígido)
```csharp
    // Cada documento implementa su forma de exportar directamente
    public class DocumentoPDF
    {
        public void ExportarEnPantalla()
            => Console.WriteLine("Mostrando PDF en pantalla...");
        public void ExportarEnImpresora()
            => Console.WriteLine("Imprimiendo PDF...");
    }

    public class DocumentoWord
    {
        public void ExportarEnPantalla()
            => Console.WriteLine("Mostrando Word en pantalla...");
        public void ExportarEnImpresora()
            => Console.WriteLine("Imprimiendo Word...");
    }
```
### 🔧 Uso de Bridge en este caso
```csharp
    // Implementador: exportadores
    public interface IExportador
    {
        void Exportar(string contenido);
    }

    public class ExportadorPantalla : IExportador
    {
        public void Exportar(string contenido)
            => Console.WriteLine($"Mostrando en pantalla: {contenido}");
    }

    public class ExportadorImpresora : IExportador
    {
        public void Exportar(string contenido)
            => Console.WriteLine($"Imprimiendo: {contenido}");
    }

    // Abstracción: documento
    public abstract class Documento
    {
        protected IExportador exportador;
        protected Documento(IExportador exportador) => this.exportador = exportador;
        public abstract void Mostrar();
    }

    // Abstracciones refinadas
    public class DocumentoPDF : Documento
    {
        public DocumentoPDF(IExportador exportador) : base(exportador) { }
        public override void Mostrar() => exportador.Exportar("Contenido PDF");
    }

    public class DocumentoWord : Documento
    {
        public DocumentoWord(IExportador exportador) : base(exportador) { }
        public override void Mostrar() => exportador.Exportar("Contenido Word");
    }

    // Ejemplo de uso
    class Program
    {
        static void Main()
        {
            Documento pdfPantalla = new DocumentoPDF(new ExportadorPantalla());
            Documento wordImpresora = new DocumentoWord(new ExportadorImpresora());

            pdfPantalla.Mostrar();     // Mostrando en pantalla: Contenido PDF
            wordImpresora.Mostrar();   // Imprimiendo: Contenido Word
        }
    }
```

### ✅ Justificación de la solución
Con **Bridge**, se desacoplaron los documentos de los exportadores:

- Ahora, los Documentos se enfocan en su contenido (PDF, Word).

- Los Exportadores se encargan de **cómo mostrar o enviar ese contenido** (pantalla, impresora, correo, etc.).

- **Se puede agregar un nuevo exportador** o un nuevo tipo de documento sin tocar el resto del código.

---

## 🚨 Problema 3: Aplicación del patrón Bridge
### 🔎 Identificación de Code Smell
En el código original, cada clase de Notificación (Email, SMS) contenía su propia lógica para distintos canales de envío (Gmail, Outlook, Twilio, etc.).
Esto causaba:
- **Código duplicado:** cada notificación repetía lógica de envío.
- **Alto acoplamiento:** un cambio en la forma de enviar afectaba a todas las notificaciones.
- **Dificultad de extensión:** agregar un nuevo canal requería modificar todas las clases de notificación.

### ❌ Antes (sin Bridge, código rígido)
```csharp
    public class NotificacionEmail
    {
        public void EnviarPorGmail(string mensaje)
            => Console.WriteLine($"[Gmail] Enviando Email: {mensaje}");

        public void EnviarPorOutlook(string mensaje)
            => Console.WriteLine($"[Outlook] Enviando Email: {mensaje}");
    }

    public class NotificacionSMS
    {
        public void EnviarPorTwilio(string mensaje)
            => Console.WriteLine($"[Twilio] Enviando SMS: {mensaje}");

        public void EnviarPorNexmo(string mensaje)
            => Console.WriteLine($"[Nexmo] Enviando SMS: {mensaje}");
    }
```

### 🔧 Uso de Bridge en este caso
```csharp
// Implementador: canal de envío
public interface ICanalEnvio
{
    void Enviar(string mensaje);
}

public class Gmail : ICanalEnvio
{
    public void Enviar(string mensaje)
        => Console.WriteLine($"[Gmail] {mensaje}");
}

public class Twilio : ICanalEnvio
{
    public void Enviar(string mensaje)
        => Console.WriteLine($"[Twilio] {mensaje}");
}

// Abstracción: notificación
public abstract class Notificacion
{
    protected ICanalEnvio canal;
    protected Notificacion(ICanalEnvio canal) => this.canal = canal;
    public abstract void Enviar(string mensaje);
}

// Abstracciones refinadas
public class NotificacionEmail : Notificacion
{
    public NotificacionEmail(ICanalEnvio canal) : base(canal) { }
    public override void Enviar(string mensaje)
        => canal.Enviar($"Email: {mensaje}");
}

public class NotificacionSMS : Notificacion
{
    public NotificacionSMS(ICanalEnvio canal) : base(canal) { }
    public override void Enviar(string mensaje)
        => canal.Enviar($"SMS: {mensaje}");
}

// Ejemplo de uso
class Program
{
    static void Main()
    {
        Notificacion email = new NotificacionEmail(new Gmail());
        Notificacion sms = new NotificacionSMS(new Twilio());

        email.Enviar("Reunión mañana a las 10am");
        sms.Enviar("Tu código de verificación es 1234");
    }
}
```

### ✅ Justificación del error
Con **Bridge**, la abstracción (Notificación) se separó de la implementación (Canal de envío):
- Se elimina la duplicación: ya no hay múltiples métodos para cada canal en cada notificación.
- Se reduce el acoplamiento: los canales pueden cambiar sin afectar las notificaciones.
- Se gana flexibilidad: agregar WhatsApp, Telegram o Slack como canal es tan simple como crear una nueva clase ICanalEnvio, sin tocar las notificaciones.

---

# 📌 Conclusión
El patrón Bridge es útil cuando:
- Hay **múltiples variaciones** de la abstracción y de la implementación.  
- Se necesita **desacoplar ambos ejes de variación**.  

