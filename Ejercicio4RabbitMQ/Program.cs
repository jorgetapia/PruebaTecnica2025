using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ejercicio4RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cadena de conexión (host, usuario, pass, etc.).
            // Podrías obtenerla de configuración (appsettings.json, variables de entorno, etc.)
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            // establecemos la conexión y creamos el canal
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                string queueName = "C_Ejemplo";

                // Declaramos la cola 
                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

               
                // Creamos el consumidor
                var consumidor = new EventingBasicConsumer(channel);

                // Manejamos el evento cuando llegue un nuevo mensaje
                consumidor.Received += (model, ea) =>
                {
                    // Conteo de reintentos                  
                    int retryCount = 0;
                    if (ea.BasicProperties.Headers != null &&
                        ea.BasicProperties.Headers.ContainsKey("x-retry"))
                    {
                        retryCount = Convert.ToInt32(ea.BasicProperties.Headers["x-retry"]);
                    }

                    Console.WriteLine($"\n[INFO] Mensaje recibido. Reintentos previos: {retryCount}");

                    try
                    {
                        // convertimos el body a texto (UTF8)
                        var body = ea.Body.ToArray();
                        var mensaje = Encoding.UTF8.GetString(body);

                        // Procesamos el mensaje
                        ProcesarMensaje(mensaje);

                        // Confirmamos con Ack
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        Console.WriteLine("Mensaje procesado ok.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: Ocurrió un error al procesar el mensaje: {ex.Message}");

                        // Mecanismo de reintento sencillo:
                        if (retryCount < 3) //intento hasta 3
                        {
                            
                            retryCount++;

                            // Re-publicamos el mensaje en la misma cola con el nuevo contador
                            var props = channel.CreateBasicProperties();
                            props.Persistent = true;
                            props.Headers = props.Headers ?? new System.Collections.Generic.Dictionary<string, object>();
                            props.Headers["x-retry"] = retryCount;

                            // Re-envíamos el mismo contenido
                            channel.BasicPublish(
                                exchange: "",
                                routingKey: queueName,
                                basicProperties: props,
                                body: ea.Body
                            );

                            // Hacemos Ack del mensaje actual para que no se quede en cola
                            channel.BasicAck(ea.DeliveryTag, multiple: false);

                            Console.WriteLine("[RETRY] Se reenvió el mensaje para otro intento.");
                        }
                        else
                        {
                            
                            channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                            Console.WriteLine("[FAIL] Se descartó el mensaje después de varios reintentos.");
                        }
                    }
                };

                // Iniciamos el consumo asíncrono
                channel.BasicConsume(
                    queue: queueName,
                    autoAck: false, 
                    consumer: consumidor
                );

                Console.WriteLine("[INFO] Esperando mensajes... (Presiona ENTER para salir)");
                Console.ReadLine();
            }
        }

        // Método que simula la lógica de negocio
        static void ProcesarMensaje(string mensaje)
        {
            // Por ejemplo, si el contenido del mensaje es "ERROR", forzamos una excepción
            if (mensaje == "ERROR")
            {
                throw new Exception("Falla en el procesamiento.");
            }

            // Posible procesamiento (parsear JSON, guardar en DB, etc.)
            Console.WriteLine($"[INFO] Procesando: {mensaje}");
        }
    }
}
