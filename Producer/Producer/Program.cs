using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "qwebapp",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                int count = 0;
                while (count < 1000)
                {
                    string message = $"Count: {count}, Msg: Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "qwebapp",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"[x] Sent {message}");
                    count++;
                }
            }
        }
    }
}
