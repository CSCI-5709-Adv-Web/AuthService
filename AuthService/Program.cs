using Serilog;
using AuthService.infrastructure.extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseCors("AllowAll");
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();