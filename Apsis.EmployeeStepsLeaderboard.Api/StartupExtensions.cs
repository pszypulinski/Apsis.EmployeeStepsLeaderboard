using Apsis.EmployeeStepsLeaderboard.Api.ExceptionHandlers;
using Apsis.EmployeeStepsLeaderboard.Application;
using Apsis.EmployeeStepsLeaderboard.Persistence;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Apsis.EmployeeStepsLeaderboard.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices();

            builder.Services.AddControllers();

            builder.Services.AddProblemDetails();

            builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
            builder.Services.AddExceptionHandler<ConcurrencyExceptionHandler>();
            builder.Services.AddExceptionHandler<ValidationExceptionHandler>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employee Steps Leaderboard API",
                    Description = "An API for a company-wide steps leaderboard application for teams of employees."
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseExceptionHandler();
            app.UseStatusCodePages();

            if (app.Environment.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
