using AuthService.infrastructure.configuration;
using Duende.IdentityServer.Models;
using Serilog;

namespace AuthService.infrastructure.extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLoggingServices();
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddCorsPolicy();
        services.ConfigureIdentityServer(configuration);
    }

    private static void AddLoggingServices(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
    }

    private static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }

    private static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        var apiScopes = configuration.GetSection("IdentityServer:ApiScopes").Get<List<ApiScope>>() ?? new List<ApiScope>();
        var apiResources = configuration.GetSection("IdentityServer:ApiResources").Get<List<ApiResource>>() ?? new List<ApiResource>();
        var clientConfigs = configuration.GetSection("IdentityServer:Clients").Get<List<ClientConfig>>() ?? new List<ClientConfig>();

        if (!clientConfigs.Any())
        {
            throw new ArgumentException("IdentityServer Clients are missing in appsettings.json");
        }

        services.AddIdentityServer(options =>
        {
            options.KeyManagement.Enabled = false;
            options.Events.RaiseSuccessEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.EmitStaticAudienceClaim = true;
        })
        .AddDeveloperSigningCredential()
        .AddInMemoryApiScopes(apiScopes)
        .AddInMemoryApiResources(apiResources)
        .AddInMemoryClients(clientConfigs.Select(client => new Client
        {
            ClientId = client.ClientId,
            ClientSecrets = { new Secret(client.ClientSecret.Sha256()) },
            AllowedGrantTypes = client.GrantType == "password" ? GrantTypes.ResourceOwnerPassword : GrantTypes.ClientCredentials,
            AllowedScopes = client.Scopes,
            AllowOfflineAccess = client.AllowOfflineAccess
        }).ToList());
    }
}