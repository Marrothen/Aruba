using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Traccia5.DB
{
    public class Utente
    {
        public class Attivita
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Password { get; set; }
        }
    }
}
