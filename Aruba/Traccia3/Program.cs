using Microsoft.OpenApi.Models;
using Traccia3.Models.DB;
using Traccia3.Repository;
using Traccia3.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = null;
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aruba api", Version = "v1" });


    var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
    c.IncludeXmlComments(filePath);
});
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<MongoDbContext>();

builder.Services.AddScoped<IAttivitaRepository, AttivitaRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
