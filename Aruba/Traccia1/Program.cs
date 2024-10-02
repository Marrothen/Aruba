using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using Traccia1;
using Traccia1.API;

var builder = WebApplication.CreateBuilder(args);

ProgramConfig.setDB(builder);

ProgramConfig.setSwagger(builder);

var app = builder.Build();

ProgramConfig.setSwagger(app);
AttivitaAPI.setAttivitaAPI(app);

app.Run();
