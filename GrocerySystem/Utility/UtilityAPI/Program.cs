using ApplicationCore.Extensions;
using AuthenticationLib.Extensions;
using CloudServices.Extensions;
using DbConnectivity.Extensions;
using Infrastracture.Extensions;
try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    ConfigurationManager configuration = builder.Configuration;
    string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    _ = configuration.AddEnvironmentVariables().AddJsonFile(environment != "local" ? ("appsettings." + environment + ".json") : "appsettings.json", optional: true, reloadOnChange: true);
    _ = builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    _ = builder.Services.AddEndpointsApiExplorer();
    _ = builder.Services.AddSwaggerGen();
    _ = builder.Services.AddDBInsfrastractionRegistrationServices(configuration);
    _ = builder.Services.AddInsfrastractionRegistrationServices(configuration);
    _ = builder.Services.AddApplicationServices();
    _ = builder.Services.RegisterAuthenticationService(configuration);
    _ = builder.Services.AddCloudServiceRegistration(configuration);

    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI();
    }
    _ = app.UseAuthentication();
    _ = app.UseAuthorization();
    _ = app.UseAuthenticationMiddleware(configuration);
    _ = app.UseHttpsRedirection();

    _ = app.UseAuthorization();

    _ = app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
