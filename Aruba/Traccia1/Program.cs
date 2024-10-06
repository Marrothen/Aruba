using Traccia1;
using Traccia1.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();

var app = builder.Build();
app.setMiddlewareSwagger();
app.setAttivitaAPI();
//app.setEndPoint();
app.Run();
