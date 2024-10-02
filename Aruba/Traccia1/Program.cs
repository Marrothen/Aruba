using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using Traccia1;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ArubaDB>(options =>
    options.UseInMemoryDatabase("ArubaDB"), ServiceLifetime.Singleton);

ProgramConfig.setSwagger(builder);

var app = builder.Build();

ProgramConfig.setSwagger(app);

#region endpoint

app.MapGet("/attivitaitems", async (ArubaDB db) =>
    await db.Attivita.ToListAsync())
        .WithDescription("Restituisce una lista di tutti gli elementi di attivit� nel database.").WithSummary("Ottieni tutte le attivit�");


app.MapGet("/attivitaitems/complete", async (ArubaDB db) =>
    await db.Attivita.Where(t => t.IsComplete).ToListAsync())
        .WithDescription("Restituisce tutte le attivit� completate");

app.MapGet("/attivitaitem/{id}", async (int id, ArubaDB db) =>
        await db.Attivita.FindAsync(id)
            is Attivita todo
                ? Results.Ok(todo)
                : Results.NotFound("Elemento non trovato"))
          .WithDescription("Restituisce l'attivit� in base all'id");


app.MapPost("/attivitaitem", async  (Attivita item, ArubaDB db) =>
{
    db.Attivita.Add(item);
    await db.SaveChangesAsync();

    return Results.Created($"/attivitaitem/{item.Id}", item);
}).WithDescription("Crea una nuova attivit�");

app.MapPut("/attivitaitem/{id}", async (int id, Attivita item, ArubaDB db) =>
{
    var tempItem = await db.Attivita.FindAsync(id);

    if (tempItem is null) return Results.NotFound("Attivit� non trovata");

    tempItem.Nome = item.Nome;
    tempItem.IsComplete = item.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
}).WithDescription("Aggiorna l'attivit�");

app.MapDelete("/attivitaitem/{id}", async (int id, ArubaDB db) =>
{
    if (await db.Attivita.FindAsync(id) is Attivita item)
    {
        db.Attivita.Remove(item);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
}).WithDescription("Elimina l'attivit� in base all'id");
#endregion

app.Run();
