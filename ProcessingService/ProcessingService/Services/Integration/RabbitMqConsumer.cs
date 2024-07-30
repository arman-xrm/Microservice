using Newtonsoft.Json;
using ProcessingService.Models.RequestModels;
using ProcessingService.Services.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ProcessingService.Services.Integration
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IProcessingService _processingService;
        private readonly ILogger<RabbitMqConsumer> _logger;

        public RabbitMqConsumer(IConnection connection, IProcessingService processingService, ILogger<RabbitMqConsumer> logger)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _processingService = processingService;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel.QueueDeclare(queue: "inputQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var input = JsonConvert.DeserializeObject<InputRequestModel>(message);

                _processingService.ProcessMessage(input);
            };

            _channel.BasicConsume(queue: "inputQueue", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
