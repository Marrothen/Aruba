using Domain.Models;
using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Traccia1
{
    public static class ProgramConfig
    {
        public static void setSwagger(WebApplicationBuilder builder) {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aruba api", Version = "v1" });
            });
        }

        public static void setSwagger(WebApplication app) {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aruba api");
            });
        }
        #region endpoint
        public static void setEndPoint(WebApplication app) {
            var mapGroup = app.MapGroup("/attivitaitems");
            mapGroup.MapGet("/", async Task<IResult> (ArubaDB db) =>
                TypedResults.Ok(await db.Attivita.ToListAsync()))
                    .WithDescription("Restituisce una lista di tutti gli elementi di attività nel database.");


            mapGroup.MapGet("/complete", async Task<IResult> (ArubaDB db) =>
                 TypedResults.Ok(await db.Attivita.Where(t => t.IsComplete).ToListAsync()));

            mapGroup.MapGet("/{id}", async Task<IResult> (int id, ArubaDB db) =>
                await db.Attivita.FindAsync(id)
                    is Attivita todo
                        ? TypedResults.Ok(todo)
                        : TypedResults.NotFound("Elemento non trovato"));

            mapGroup.MapPost("/", async Task<IResult> (AttivitaOP item, ArubaDB db) =>
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

                return TypedResults.Created($"/attivitaitem/{item.Id}", item);
            });

            mapGroup.MapPut("/{id}", async Task<IResult> (int id, AttivitaOP item, ArubaDB db) =>
            {
                var tempItem = await db.Attivita.FindAsync(id);

                if (tempItem is null) return TypedResults.NotFound($"Item con id {id} non trovato");

                tempItem.Nome = item.Nome;
                tempItem.Descrizione = item.Descrizione;
                tempItem.IsComplete = item.IsComplete;
                tempItem.Priority = item.Priority;

                await db.SaveChangesAsync();

                return TypedResults.NoContent();
            });

            mapGroup.MapDelete("/{id}", async Task<IResult> (int id, ArubaDB db) =>
            {
                if (await db.Attivita.FindAsync(id) is Attivita item)
                {
                    db.Attivita.Remove(item);
                    await db.SaveChangesAsync();
                    return TypedResults.NoContent();
                }

                return TypedResults.NotFound();
            });
            #endregion
        }
    }
}
