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
        private readonly IProducer<Null, string> producer;

        public KafkaProducer(string bootstrapServers)
        {
            _config = new ProducerConfig { BootstrapServers = bootstrapServers };
            producer= new ProducerBuilder<Null, string>(_config).Build();
        }

        public async Task ProduceAsync( string message)
        {
            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });

        }


    }
}
