﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DB
{
    public class ArubaDB : DbContext
    {
        public ArubaDB(DbContextOptions<ArubaDB> options) : base(options) {
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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Attivita>()
        //        .HasKey(a => a.Id); 

        //    modelBuilder.Entity<Attivita>()
        //        .Property(a => a.Id)
        //        .ValueGeneratedOnAdd(); 

        //    modelBuilder.Entity<Attivita>()
        //        .Property(a => a.Nome)
        //        .IsRequired();
        //}
    }
}
