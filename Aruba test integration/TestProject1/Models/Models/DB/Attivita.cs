using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DB
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
