using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(
    $"http://0.0.0.0:{Environment.GetEnvironmentVariable("PORT") ?? "8080"}"
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Servers.Clear();

        document.Servers.Add(new OpenApiServer
        {
            Url = "https://aulaapionrender.onrender.com"
        });

        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.MapControllers();
app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "API v1.0";
    options.Theme = ScalarTheme.BluePlanet;
});

app.Run();