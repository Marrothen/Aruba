using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traccia4
{
    public class KafkaConsumer 
    {

        public async Task Start(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "test-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();

            consumer.Subscribe("ArubaTopic");

            while (!cancellationToken.IsCancellationRequested)
            {
                var cr = consumer.Consume(cancellationToken);
                consumer.Commit(cr);
                Console.WriteLine($"Consumed message '{cr.Value}' from '{cr.TopicPartitionOffset}'.");
            }

        }


        public Task StartBackgroundService(CancellationToken stoppingToken) {
            return Task.Run(() =>
            {
                Start(stoppingToken);
            }, stoppingToken);
        }
    }

}
