using Traccia1;
using Traccia1.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();

var app = builder.Build();

ProgramConfig.setMiddlewareSwagger(app);
AttivitaAPI.setAttivitaAPI(app);
//AttivitaAPI2.setEndPoint(app);
app.Run();
