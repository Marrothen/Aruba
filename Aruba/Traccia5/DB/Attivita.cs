using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Traccia5.DB
{
    public class Attivita
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Nome { get; set; }

            public string Descrizione { get; set; }

            public bool IsComplete { get; set; }

            public string Priority { get; set; }

            public string CreatedDate { get; set; }
    }
}
