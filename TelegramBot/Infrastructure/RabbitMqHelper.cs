using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace TelegramBot.Infrastructure
{
    public class RabbitMqHelper<TModel> where TModel : class
    {
        private readonly IConnection _connection;
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        public RabbitMqHelper(IConfiguration configuration)
        {
            _hostName = configuration.GetSection("RabbitMQ")["HostName"]!;
            _userName = configuration.GetSection("RabbitMQ")["UserName"]!;
            _password = configuration.GetSection("RabbitMQ")["Password"]!;
            //var factory = new ConnectionFactory() { HostName = _hostName };
        }
        public async Task PublishMessage(TModel classModel, string? queueName, string? exchangeName, exchangeType exchange)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = _userName,
                Password = _password
            };
            var connection = await connectionFactory.CreateConnectionAsync();
            var model = await connection.CreateChannelAsync();

            if (exchange == exchangeType.Direct)
            {
                await model.QueueDeclareAsync(queueName!, true, false, false, null);
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(classModel));
                await model.BasicPublishAsync("", queueName!, body);
            }
            else if (exchange == exchangeType.Fanout)
            {
                await model.ExchangeDeclareAsync(exchangeName!, ExchangeType.Fanout,
                    true, false, null);
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(classModel));
                await model.BasicPublishAsync(exchangeName!, "", body);
            }
        }

        public async Task ConsumeMessage(string? queueName, string? exchangeName, exchangeType exchange)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = _userName,
                Password = _password
            };
            var connection = await connectionFactory.CreateConnectionAsync();
            var model = await connection.CreateChannelAsync();

            if (exchange == exchangeType.Direct)
            {
                await model.QueueDeclareAsync(queueName!, true, false, false, null);
            }

            else if (exchange == exchangeType.Fanout)
            {
                await model.ExchangeDeclareAsync(exchangeName!, ExchangeType.Fanout, true, false, null);
            }

            var consumer = new AsyncEventingBasicConsumer(model);

            consumer.ReceivedAsync += (sender, args) =>
            {
                var body = Encoding.ASCII.GetString(args.Body.ToArray());
                if (true)
                {
                    model.BasicAckAsync(args.DeliveryTag, false);
                    var message = JsonSerializer.Deserialize<TModel>(body);
                }

                return Task.CompletedTask;
            };
            if (exchange == exchangeType.Direct)
            {
                await model.BasicConsumeAsync(queueName!, true, consumer);
            }

            else if (exchange == exchangeType.Fanout)
            {
                await model.BasicConsumeAsync(exchangeName!, false, consumer);
            }
        }
        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
    public class RabbitMqHelper
    {
        private readonly IConnection _connection;
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        public RabbitMqHelper(IConfiguration configuration)
        {
            _hostName = configuration.GetSection("RabbitMQ")["HostName"]!;
            _userName = configuration.GetSection("RabbitMQ")["UserName"]!;
            _password = configuration.GetSection("RabbitMQ")["Password"]!;
            var factory = new ConnectionFactory() { HostName = _hostName };
        }
        public async Task PublishMessage(string message, string? queueName, string? exchangeName, exchangeType exchange)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = _userName,
                Password = _password
            };
            var connection = await connectionFactory.CreateConnectionAsync();
            var model = await connection.CreateChannelAsync();

            if (exchange == exchangeType.Direct)
            {
                await model.QueueDeclareAsync(queueName!, true, false, false, null);
                var body = Encoding.UTF8.GetBytes(message);
                await model.BasicPublishAsync("", queueName!, body);
            }
            else if (exchange == exchangeType.Fanout)
            {
                await model.ExchangeDeclareAsync(exchangeName!, ExchangeType.Fanout,
                    true, false, null);
                var body = Encoding.UTF8.GetBytes(message);
                await model.BasicPublishAsync(exchangeName!, "", body);
            }
        }

        public async Task ConsumeMessage(string? queueName, string? exchangeName, exchangeType exchange)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = _userName,
                Password = _password
            };
            var connection = await connectionFactory.CreateConnectionAsync();
            var model = await connection.CreateChannelAsync();

            if (exchange == exchangeType.Direct)
            {
                await model.QueueDeclareAsync(queueName!, true, false, false, null);
            }

            else if (exchange == exchangeType.Fanout)
            {
                await model.ExchangeDeclareAsync(exchangeName!, ExchangeType.Fanout, true, false, null);
            }

            var consumer = new AsyncEventingBasicConsumer(model);

            consumer.ReceivedAsync += (sender, args) =>
            {
                var body = Encoding.ASCII.GetString(args.Body.ToArray());
                if (true)
                {
                    model.BasicAckAsync(args.DeliveryTag, false);
                    var message = JsonSerializer.Deserialize<string>(body);

                }

                return Task.CompletedTask;
            };
            if (exchange == exchangeType.Direct)
            {
                await model.BasicConsumeAsync(queueName!, true, consumer);
            }

            else if (exchange == exchangeType.Fanout)
            {
                await model.BasicConsumeAsync(exchangeName!, false, consumer);
            }
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }

}
public enum exchangeType
{
    Direct,
    Fanout,
    Headers,
    Topic
}

