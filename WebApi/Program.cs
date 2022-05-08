using DataLayer.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;
using WebApi.Extensions;
using WebApi.monitoring.Switchers;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((context, configuration) =>
    {
        configuration.Enrich.FromLogContext()
             .Enrich.WithMachineName()
             .WriteTo.File(new RenderedCompactJsonFormatter(), "monitoring/Logs/logs.json")
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

builder.Services.Configure<ProductSwitchers>(
    builder.Configuration.GetSection("ProductSwitchers"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Template App",
        Description = "Template architecture",
        TermsOfService = new Uri("https://github.com/sigmade"),
        Contact = new OpenApiContact
        {
            Name = "Egor Sychyov",
            Url = new Uri("https://github.com/sigmade")
        },
        License = new OpenApiLicense
        {
            Name = "Docs",
            Url = new Uri("http://localhost:5129/docs")
        }
    });

    var xmlFilename = $"WebApi";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AppServices();
builder.Services.AppDataProviders();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles();
    app.UseDirectoryBrowser(new DirectoryBrowserOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs")),
        RequestPath = "/docs"
    });
}

app.UseRouting();
app.UseHttpMetrics();

app.MapControllers();
app.MapMetrics();

app.Run();