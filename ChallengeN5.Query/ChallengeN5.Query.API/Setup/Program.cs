using ChallengeN5.Command.Infrastructure.Application.Service;
using ChallengeN5.Query.API.Application.Query.GetPermissionByUserId;
using ChallengeN5.Query.API.Architecture.Extension;
using ChallengeN5.Query.Domain.Application.Service;
using ChallengeN5.Query.Infrastructure.Application.Service;
using ChallengeN5.Query.Infrastructure.Application.Service.BackGroundService;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Challenge N5 Query API",
            Description = "Technical Challenge N5",
            Contact = new OpenApiContact
            {
                Name = "Contact José Moya",
                Url = new Uri("https://www.linkedin.com/in/jmoyau/")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        });
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });


builder.Services.AddSingleton<IElasticSearchService, ElasticSearchService>();
builder.Services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
//builder.Services.AddHostedService<KafkaConsumerBackgroundService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetPermissionByUserIdHandler>());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
