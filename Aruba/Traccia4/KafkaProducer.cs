using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Traccia4
{
    public class KafkaProducer
    {
        private readonly ProducerConfig _config;
        private readonly string _topic="ArubaTopic";


        public KafkaProducer(string bootstrapServers)
        {
            _config = new ProducerConfig { BootstrapServers = bootstrapServers };
        }

        public async Task ProduceAsync( string message)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }

    }
}
