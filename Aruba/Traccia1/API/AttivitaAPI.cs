using Domain.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Traccia1.API
{
    public static class AttivitaAPI
    {
        public static void setAttivitaAPI(WebApplication app) {
            app.MapGet("/attivitaitems", async (ArubaDB db) =>
                Results.Ok(await db.Attivita.ToListAsync()))
                    .WithDescription("Restituisce una lista di tutti gli elementi di attività nel database.")
                    .WithSummary("Ottieni tutte le attività")
                    .Produces<List<Attivita>>(StatusCodes.Status200OK);

            app.MapGet("/attivitaitems/complete", async (ArubaDB db) =>
               Results.Ok(await db.Attivita.Where(t => t.IsComplete).ToListAsync()))
                    .WithDescription("Restituisce tutte le attività completate")
                    .Produces<List<Attivita>>(StatusCodes.Status200OK);

            app.MapGet("/attivitaitem/{id}", async (int id, ArubaDB db) =>
                    await db.Attivita.FindAsync(id)
                        is Attivita item
                            ? Results.Ok(item)
                            : Results.NotFound($"Elemento con id:{id} non trovato"))
                      .WithDescription("Restituisce l'attività in base all'id")
                      .Produces<Attivita>(StatusCodes.Status200OK)
                      .Produces<string>(StatusCodes.Status404NotFound);

            app.MapPost("/attivitaitem", async (Attivita item, ArubaDB db) =>
            {
                db.Attivita.Add(item);
                await db.SaveChangesAsync();

                return Results.Ok(item);
            }).WithDescription("Crea una nuova attività").Produces<Attivita>(StatusCodes.Status201Created);

            app.MapPut("/attivitaitem/{id}", async (int id,[FromBody] Attivita item, ArubaDB db) =>
            {
                var tempItem = await db.Attivita.FindAsync(id);

                if (tempItem is null) return Results.NotFound($"Elemento con id:{id} non trovata");

                tempItem.Nome = item.Nome;
                tempItem.IsComplete = item.IsComplete;
                tempItem.Descrizione= item.Descrizione;
                await db.SaveChangesAsync();

                return Results.Ok(tempItem);
            }).WithDescription("Aggiorna l'attività").Produces<Attivita>(StatusCodes.Status204NoContent).Produces<string>(StatusCodes.Status404NotFound);

            app.MapDelete("/attivitaitem/{id}", async (int id, ArubaDB db) =>
            {
                if (await db.Attivita.FindAsync(id) is Attivita item)
                {
                    db.Attivita.Remove(item);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }

                return Results.NotFound($"Elemento non trovato con id {id}");
            }).WithDescription("Elimina l'attività in base all'id").Produces(StatusCodes.Status204NoContent).Produces<string>(StatusCodes.Status404NotFound);
        }
    }
}
