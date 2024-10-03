using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Traccia3.Models.DB
{
    public class Attivita
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("IdAttivita")]
        public string Id { get; set; }
        [JsonPropertyName("NomeAttivita")]
        public string Nome { get; set; }
        [JsonPropertyName("DescrizioneAttivita")]
        public string Descrizione { get; set; }
        [JsonPropertyName("IsComplete")]
        public bool IsComplete { get; set; }
        public string Priority { get; set; }
        [JsonPropertyName("CreazioneAttivita")]
        public string CreatedDate { get; set; }
    }
}
