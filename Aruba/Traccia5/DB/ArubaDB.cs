using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Traccia5.DB
{
    public class ArubaDB : DbContext
    {
        public ArubaDB(DbContextOptions<ArubaDB> options) : base(options)
        {
            InitializeDatabase();
        }

        public DbSet<Attivita> Attivita { get; set; }

        private void InitializeDatabase()
        {

            Attivita.AddRange(new List<Attivita>
            {
                new Attivita
                {
                    Id = 1,
                    Nome = "Comprare il latte",
                    Descrizione = "Comprare latte intero al supermercato",
                    IsComplete = false,
                    Priority = "Alta",
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd")
                },
                new Attivita
                {
                    Id = 2,
                    Nome = "Completare il progetto",
                    Descrizione = "Completare la parte finale del progetto.",
                    IsComplete = false,
                    Priority = "Media",
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd")
                },
                new Attivita
                {
                    Id = 3,
                    Nome = "Pulire la casa",
                    Descrizione = "Fare una pulizia generale della casa.",
                    IsComplete = true,
                    Priority = "Bassa",
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd")
                }
            });
            SaveChanges();
        }
    }
}
