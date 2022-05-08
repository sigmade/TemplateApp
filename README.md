https://medium.com/@snero90/create-api-documentation-with-docfx-using-visual-studio-and-cli-b6eb3afa5827

Run Documentation
cd .\Documentation\
docfx --serve

Add Xml to Swagger
https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio&viewFallbackFrom=aspnetcore-3.0#xml-comments

in project file
<GenerateDocumentationFile>true</GenerateDocumentationFile>
<DocumentationFile>WebApi</DocumentationFile>

in startup/program

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
            Name = "MIT",
            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
        }
    });

    var xmlFilename = $"WebApi";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});