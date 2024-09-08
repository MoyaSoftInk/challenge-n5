namespace ChallengeN5.Query.Infrastructure.Application.Service;

using ChallengeN5.Query.Domain.Application.Model;
using ChallengeN5.Query.Domain.Application.Service;
using Microsoft.Extensions.Configuration;
using Nest;
using System.Threading;
using System.Threading.Tasks;

public class ElasticSearchService : IElasticSearchService
{
    private readonly ElasticClient _client;

    public ElasticSearchService(IConfiguration configuration)
    {
        // Leer configuración de Elasticsearch desde appsettings.json
        var elasticConfig = configuration.GetSection("Elasticsearch");
        var settings = new ConnectionSettings(new Uri(elasticConfig["Uri"]))
            .DefaultIndex(elasticConfig["DefaultIndex"]);

        _client = new ElasticClient(settings);
    }

    public async Task<ISearchResponse<Permission>> SearchPermissionByEmployeeIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _client.SearchAsync<Permission>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Employee.Id)
                    .Query(id.ToString())
                )
            ), cancellationToken
        );
    }

    public async Task IndexPermission(Permission permission, CancellationToken cancellationToken)
    {
        // Crear un ID único combinando employeeId y permissionTypeId
        var documentId = $"{permission.EmployeeId}-{permission.PermissionTypeId}";

        // Usar IndexAsync para especificar el ID del documento
        var response = await _client.IndexAsync(permission, idx => idx
            .Index("permissions-index")
            .Id(documentId),
            cancellationToken);

        if (response.IsValid)
        {
            Console.WriteLine("Document indexed successfully!");
        }
        else
        {
            Console.WriteLine($"Failed to index document: {response.OriginalException.Message}");
        }
    }
}
