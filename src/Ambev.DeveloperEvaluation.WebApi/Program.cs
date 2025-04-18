using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Common.Filter;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using AutoMapper.EquivalencyExpression;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Infrastructure;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen(c =>
                {
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    var comments = new XPathDocument(xmlPath);
                    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                    c.EnableAnnotations();
                    c.MapType<FilterRequest>(() => new OpenApiSchema
                    {
                        Type = "object",
                        Example = new OpenApiString(
                            JsonSerializer.Serialize
                        (
                            new
                            {
                                _page = 1,
                                _size = 10,
                                _order = "price desc, title asc",
                                title = "beer",
                                _minPrice = 10,
                                _maxPrice = 1000,
                                _minDate = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd"),
                                _maxDate = DateTime.Now.ToString("yyyy-MM-dd")
                            }, new JsonSerializerOptions() { WriteIndented = true })
                            )
                    });

                });

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddDbContext<NoSQLContext>(options =>
            {
                var connection = new MongoUrl(builder.Configuration.GetConnectionString("MongoDb"));
                var settings = MongoClientSettings.FromUrl(connection);
                var client = new MongoClient(settings);
                options.UseMongoDB(client, connection.DatabaseName);
            });

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(cfg => {
                cfg.AddCollectionMappers();
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
            }, [typeof(Program).Assembly, typeof(ApplicationLayer).Assembly]);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
