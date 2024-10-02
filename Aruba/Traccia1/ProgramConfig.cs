using Domain.Models;
using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Traccia1
{
    public static class ProgramConfig
    {
        public static void setDB(WebApplicationBuilder builder) {
            builder.Services.AddDbContext<ArubaDB>(options =>
                options.UseInMemoryDatabase("ArubaDB"), ServiceLifetime.Singleton);
        }

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
                c.RoutePrefix = string.Empty;

            });
        }
    }
}
