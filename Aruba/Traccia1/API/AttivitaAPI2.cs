using Domain.Models.DB;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Traccia1.API
{
    public static class AttivitaAPI2
    {
        public static void setEndPoint(WebApplication app)
        {
            var mapGroup = app.MapGroup("/attivitaitems");

            mapGroup.MapGet("/", getItem).WithDescription("Restituisce una lista di tutti gli elementi di attività nel database.");
            mapGroup.MapGet("/complete", getItemComplete).WithDescription("Restituisce una lista di tutti gli elementi di attività completati nel database.");
            mapGroup.MapGet("/{id}", getItemById).WithDescription("Restituisce una un elemento by id.");
            mapGroup.MapPost("/", createItem).WithDescription("Crea una nuova attività");
            mapGroup.MapPut("/{id}", updateItem).WithDescription("Aggiorna un item");
            mapGroup.MapDelete("/{id}", deleteItem).WithDescription("Elimina un item by id");
        }

        private static async Task<IResult> getItem(ArubaDB db)
        {
            return TypedResults.Ok(await db.Attivita.ToListAsync());
        }

        private static async Task<IResult> getItemComplete(ArubaDB db)
        {
            return TypedResults.Ok(await db.Attivita.Where(t => t.IsComplete).ToListAsync());
        }

        private static async Task<IResult> getItemById(int id, ArubaDB db)
        {
            return await db.Attivita.FindAsync(id)
                    is Attivita item
                        ? TypedResults.Ok(item)
                        : TypedResults.NotFound($"Elemento con {id} non trovato");
        }

        private static async Task<IResult> createItem(AttivitaOP item, ArubaDB db)
        {
            var newItem = new Attivita
            {
                Nome = item.Nome,
                Descrizione = item.Descrizione,
                IsComplete = item.IsComplete,
                Priority = item.Priority,
                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd")
            };
            db.Attivita.Add(newItem);
            await db.SaveChangesAsync();

            return TypedResults.Ok(newItem);
        }

        private static async Task<IResult> updateItem(int id, AttivitaOP item, ArubaDB db)
        {
            var tempItem = await db.Attivita.FindAsync(id);

            if (tempItem is null) return TypedResults.NotFound($"Item con id {id} non trovato");

            tempItem.Nome = item.Nome;
            tempItem.Descrizione = item.Descrizione;
            tempItem.IsComplete = item.IsComplete;
            tempItem.Priority = item.Priority;

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        private static async Task<IResult> deleteItem(int id, ArubaDB db)
        {
            if (await db.Attivita.FindAsync(id) is Attivita item)
            {
                db.Attivita.Remove(item);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }

    }
}
