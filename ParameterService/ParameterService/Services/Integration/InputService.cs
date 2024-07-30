using Newtonsoft.Json;
using ParameterService.Models.RequestModels;
using ParameterService.Services.Interface;
using RabbitMQ.Client;
using System.Text;

namespace ParameterService.Services.Integration
{
    public class InputService : IInputService
    {
        private readonly IConnection _connection;

        public InputService(IConnection connection)
        {
            _connection = connection;
        }

        public void SendMessage(InputRequestModel input)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "inputQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = JsonConvert.SerializeObject(input);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "inputQueue", basicProperties: null, body: body);
            }
        }
    }

}
