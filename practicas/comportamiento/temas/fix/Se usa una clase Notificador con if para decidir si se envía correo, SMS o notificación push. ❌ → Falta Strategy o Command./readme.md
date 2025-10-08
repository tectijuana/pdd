Codigo espaguetti
Ejercicio de notificador
Johan Herrera 20212936

using System;

namespace NotificacionesApp
{
    public class Notificador
    {
        public void EnviarNotificacion(string tipo, string mensaje, string destinatario)
        {
            // Código espagueti: un solo método con demasiadas responsabilidades
            if (tipo == "correo")
            {
                Console.WriteLine($"[Correo] Enviando correo a {destinatario}: {mensaje}");
                // Simulación de envío de correo...
            }
            else if (tipo == "sms")
            {
                Console.WriteLine($"[SMS] Enviando mensaje de texto a {destinatario}: {mensaje}");
                // Simulación de envío de SMS...
            }
            else if (tipo == "push")
            {
                Console.WriteLine($"[Push] Enviando notificación push a {destinatario}: {mensaje}");
                // Simulación de envío push...
            }
            else
            {
                Console.WriteLine("❌ Tipo de notificación no soportado");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var notificador = new Notificador();

            Console.WriteLine("Seleccione tipo de notificación (correo/sms/push):");
            string tipo = Console.ReadLine();

            Console.WriteLine("Ingrese el destinatario:");
            string destinatario = Console.ReadLine();

            Console.WriteLine("Escriba el mensaje:");
            string mensaje = Console.ReadLine();

            notificador.EnviarNotificacion(tipo, mensaje, destinatario);
        }
    }
}

Codigo refactorizado al patron de diseño command
Mejoras del codigo: 
Se eliminaron los if/else que habia y se utilizo el patron de diseño command y se crea comandos para llamar a la accion que se necesita

using System;
using System.Collections.Generic;

namespace NotificacionesApp
{
    
    public interface INotificacionCommand
    {
        //Va ejecutar el comando, enviando notificacion
        void Ejecutar(string mensaje, string destinatario);
    }

    
    public class NotificacionCorreo : INotificacionCommand
    {
        public void Ejecutar(string mensaje, string destinatario)
        {
            //Envio de correo
            Console.WriteLine($"[Correo] Enviando correo a {destinatario}: {mensaje}");
            
        }
    }

    public class NotificacionSMS : INotificacionCommand
    {
        public void Ejecutar(string mensaje, string destinatario)
        {
            //Envio de sms
            Console.WriteLine($"[SMS] Enviando mensaje de texto a {destinatario}: {mensaje}");
        }
    }

    public class NotificacionPush : INotificacionCommand
    {
        public void Ejecutar(string mensaje, string destinatario)
        {
            //Envio de push(notificacion)
            Console.WriteLine($"[Push] Enviando notificación push a {destinatario}: {mensaje}");
        }
    }

    // Invoker
    // El invoker conoce los comandos y los ejecuta segun el tipo solicitado
    public class Notificador
    {
        //El diccionario asocia el tipo de notificacion con su comando correspondiente
        private readonly Dictionary<string, INotificacionCommand> _comandos;

        //El constructor que inicializa los comandos
        public Notificador()
        {
            _comandos = new Dictionary<string, INotificacionCommand>
            {
                { "correo", new NotificacionCorreo() },
                { "sms", new NotificacionSMS() },
                { "push", new NotificacionPush() }
            };
        }

        //Envia la notificacion segun el tipo especificado(correo, sms, push)
        public void Enviar(string tipo, string mensaje, string destinatario)
        {
            if (_comandos.ContainsKey(tipo))
            {
                _comandos[tipo].Ejecutar(mensaje, destinatario);
            }
            else
            {
                Console.WriteLine("❌ Tipo de notificación no soportado");
            }
        }
    }

    // La clase donde el cliente interactua con el usuario y utiliza el invoker
    public class Program
    {
        public static void Main(string[] args)
        {
            var notificador = new Notificador();

            Console.WriteLine("Seleccione tipo de notificación (correo/sms/push):");
            string tipo = Console.ReadLine()?.ToLower();

            Console.WriteLine("Ingrese el destinatario:");
            string destinatario = Console.ReadLine();

            Console.WriteLine("Escriba el mensaje:");
            string mensaje = Console.ReadLine();

            //El cliente solicita el envio
            notificador.Enviar(tipo, mensaje, destinatario);
        }
    }
}
