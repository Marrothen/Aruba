using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Traccia4;

string _bootstrapServers = "localhost:9092"; 


Console.WriteLine("Kafka Producer App");
Console.WriteLine("-------------------");

KafkaProducer _kafkaProducer = new KafkaProducer(_bootstrapServers);
KafkaConsumer _kafkaConsumer = new KafkaConsumer();
CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
CancellationToken cancellationToken = cancellationTokenSource.Token;
_kafkaConsumer.StartBackgroundService(cancellationToken);
while (true)
{
    Console.WriteLine("\nSeleziona un'opzione:");
    Console.WriteLine("1: Invia messaggio");
    Console.WriteLine("2: Esci");

    var scelta = Console.ReadLine();

    switch (scelta)
    {
        case "1":
            Console.Write("Inserisci il messaggio da inviare: ");
            var messaggio = Console.ReadLine();
            await _kafkaProducer.ProduceAsync(messaggio);
            break;

        case "2":
            Console.WriteLine("Uscita...");
            cancellationTokenSource.Cancel();
            break;
        default:
            Console.WriteLine("Scelta non valida, riprova.");
            break;
    }
}