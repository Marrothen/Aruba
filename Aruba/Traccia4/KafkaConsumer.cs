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
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false
            };

            using var consumer = new ConsumerBuilder<string, string>(config)
            .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    foreach (var partition in partitions)
                    {
                        Console.WriteLine($" Partizione assegnata {partition}");
                    }
                })
            .SetPartitionsRevokedHandler((c, partitions) =>
            {

                foreach (var partition in partitions)
                {
                    Console.WriteLine($" Partizione revocata {partition.Offset}");
                }

                c.Commit();

            }).Build();

            consumer.Subscribe("ArubaTopic");

            while (!cancellationToken.IsCancellationRequested)
            {
                var cr = consumer.Consume(cancellationToken);

                Console.WriteLine($"messaggio consumato ma non committato '{cr.Value}' da '{cr.TopicPartitionOffset}' e offset: '{cr.Offset}'.");

                consumer.StoreOffset(cr);

                if (cr.Offset % 2 == 0)
                {
                    consumer.Commit();
                    Console.WriteLine($"Commit effettuato fino all'offset: {cr.Offset}.");
                }

            }
            consumer.Close();

        }


        public Task StartBackgroundService(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                Start(stoppingToken);
            }, stoppingToken);
        }
    }

}
