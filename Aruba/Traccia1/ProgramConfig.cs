using Domain.Models;
using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Traccia1
{
    public static class ProgramConfig
    {

        public static void setMiddlewareSwagger(WebApplication app) {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aruba api");
                c.RoutePrefix = string.Empty;
            });
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddDbContext<ArubaDB>(options =>
                 options.UseInMemoryDatabase("ArubaDB"), ServiceLifetime.Singleton);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aruba api", Version = "v1" });

              }); ;
        }
    }
}
