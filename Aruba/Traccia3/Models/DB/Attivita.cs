using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Traccia3.Models.DB
{
    public class Attivita
    {
        [BsonId] 
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public bool IsComplete { get; set; }
        public string Priority { get; set; }
        public string CreatedDate { get; set; }
    }
}
