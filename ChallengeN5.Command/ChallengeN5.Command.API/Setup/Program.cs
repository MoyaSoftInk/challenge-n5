using ChallengeN5.Command.API.Application.Command.PostRequestPermision;
using ChallengeN5.Command.API.Architecture.Extension;
using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Domain.Application.Service;
using ChallengeN5.Command.Domain.Architecture.Core;
using ChallengeN5.Command.Infrastructure.Application;
using ChallengeN5.Command.Infrastructure.Application.Data;
using ChallengeN5.Command.Infrastructure.Application.Repository;
using ChallengeN5.Command.Infrastructure.Application.Service;
using ChallengeN5.Command.Infrastructure.Architecture.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

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
            Title = "Challenge N5 Command API",
            Description = "Technical Challenge N5",
            Contact = new OpenApiContact
            {
                Name = "Contact Jos� Moya",
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

builder.Services
    .AddDbContext<N5Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionDocker"),
        options => options.MigrationsAssembly("ChallengeN5.Command.Persistance")));

builder.Services.AddTransient<IKafkaProducerService, KafkaProducerService>(
    c => new KafkaProducerService(builder.Configuration["KAFKA_BOOTSTRAP_SERVERS"] ?? "localhost:9092"));

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PostRequestPermisionHandler>());

builder.Services.Decorate(typeof(IRequestHandler<,>), typeof(UnitOfWorkHandlerDecorator<,>));

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseAuthorization();

app.MapControllers();

SetupDatabase(app);

app.Run();

static void SetupDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<N5Context>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}