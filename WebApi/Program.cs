using DataLayer.EF;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((context, configuration) =>
    {
        configuration.Enrich.FromLogContext()
             .Enrich.WithMachineName()
             .WriteTo.File(new RenderedCompactJsonFormatter(), "Logs/logs.json")
             .WriteTo.Console()
             .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
             {
                 IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                 AutoRegisterTemplate = true,
                 NumberOfShards = 2,
                 NumberOfReplicas = 1
             })
             .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
             .ReadFrom.Configuration(context.Configuration);
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AppServices();
builder.Services.AppDataProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpMetrics();

app.MapControllers();
app.MapMetrics();

app.Run();