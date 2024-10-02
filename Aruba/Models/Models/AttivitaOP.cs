﻿using Domain.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AttivitaOP
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public string Descrizione { get; set; }
        public bool IsComplete { get; set; }
        public string Priority { get; set; }

        public AttivitaOP() { }

        public AttivitaOP(Attivita attivita) =>
            (Id, Nome, Descrizione, IsComplete, Priority) = (attivita.Id, attivita.Nome, attivita.Descrizione, attivita.IsComplete, attivita.Priority);
    }
}
