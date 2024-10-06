using Domain.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Traccia1.API
{
    public static class AttivitaAPI
    {
        public static void setAttivitaAPI(this WebApplication app)
        {
            app.MapGet("/attivitaitems", async (ArubaDB db) =>
                Results.Ok(await db.Attivita.ToListAsync()))
                    .WithName("GetAllItem")
                    .WithDescription("Restituisce una lista di tutti gli elementi di attività nel database.")
                    .WithSummary("Ottieni tutte le attività")
                    .Produces<List<Attivita>>(StatusCodes.Status200OK);

            app.MapGet("/attivitaitems/complete", async (ArubaDB db) =>
               Results.Ok(await db.Attivita.Where(t => t.IsComplete).ToListAsync()))
                    .WithName("GetAllItemComplete")
                    .WithDescription("Restituisce tutte le attività completate")
                    .WithSummary("Ottieni tutte le attività completate")
                    .Produces<List<Attivita>>(StatusCodes.Status200OK);

            app.MapGet("/attivitaitem/{id}", async (int id, ArubaDB db) =>
                    await db.Attivita.FindAsync(id)
                        is Attivita item
                            ? Results.Ok(item)
                            : Results.NotFound($"Elemento con id:{id} non trovato"))
                      .WithName("GetItemById")
                      .WithDescription("Restituisce l'attività in base all'id")
                      .WithSummary("Restituisce l'attività in base all'id")
                      .Produces<Attivita>(StatusCodes.Status200OK)
                      .Produces<string>(StatusCodes.Status404NotFound);

            app.MapPost("/attivitaitem", async (Attivita item, ArubaDB db) =>
            {
                if (string.IsNullOrEmpty(item.Nome)) return Results.BadRequest($"Nome null non ammissibile");
                if (string.IsNullOrEmpty(item.Descrizione)) return Results.BadRequest($"Descrizione null non ammissibile");
                if (string.IsNullOrEmpty(item.Priority)) return Results.BadRequest($"Priority null non ammissibile");
                item.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                db.Attivita.Add(item);
                await db.SaveChangesAsync();
                return Results.Ok(item);
            })
                .WithName("AddItem")
                .WithDescription("Crea una nuova attività")
                .WithSummary("Crea una nuova attività")
                .Produces<string>(StatusCodes.Status400BadRequest)
                .Produces<Attivita>(StatusCodes.Status201Created);

            app.MapPut("/attivitaitem/{id}", async (int id, Attivita item, ArubaDB db) =>
            {
                var tempItem = await db.Attivita.FindAsync(id);

                if (tempItem is null) return Results.NotFound($"Elemento con id:{id} non trovata");
                if(string.IsNullOrEmpty(item.Nome)) return Results.BadRequest($"Nome null non ammissibile");
                if(string.IsNullOrEmpty(item.Descrizione)) return Results.BadRequest($"Descrizione null non ammissibile");

                tempItem.Nome = item.Nome;
                tempItem.IsComplete = item.IsComplete;
                tempItem.Descrizione = item.Descrizione;
                await db.SaveChangesAsync();

                return Results.NoContent();
            })
                .WithName("UpdateItem")
                .WithDescription("Aggiorna l'attività in base all'id")
                .WithSummary("Restituisce l'attività in base all'id")
                .Produces(StatusCodes.Status204NoContent)
                .Produces<string>(StatusCodes.Status404NotFound)
                .Produces<string>(StatusCodes.Status400BadRequest);

            app.MapDelete("/attivitaitem/{id}", async (int id, ArubaDB db) =>
            {

                if (await db.Attivita.FindAsync(id) is Attivita item)
                {
                    db.Attivita.Remove(item);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }

                return Results.NotFound($"Elemento non trovato con id {id}");
            })
                .WithName("DeleteItem")
                .WithDescription("Elimina l'attività in base all'id")
                .WithSummary("Elimina l'attività in base all'id")
                .Produces(StatusCodes.Status204NoContent).Produces<string>(StatusCodes.Status404NotFound);
        }
    }
}
