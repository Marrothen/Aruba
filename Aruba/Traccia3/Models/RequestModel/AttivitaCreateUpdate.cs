using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Traccia3.Models.RequestModel
{
    public partial class AttivitaCreateUpdate
    {
        [JsonPropertyName("NomeAttivita")]
        public string Nome { get; set; }
        [JsonPropertyName("DescrizioneAttivita")]
        public string Descrizione { get; set; }
        [JsonPropertyName("IsComplete")]
        [Required]
        public bool IsComplete { get; set; }
        public string Priority { get; set; }
    }
}
