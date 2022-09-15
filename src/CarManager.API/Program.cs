var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers()
    .ConfigureApplicationPartManager(manager => manager.FeatureProviders.Add(new InternalControllerFeatureProvider()))
    .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwagger();
builder.Services.AddProblemDetails();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    builder.Configuration
        .AddUserSecrets<Program>()
        .AddJsonFile($"appsettings.{app.Environment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
}
else
{
    builder.Configuration.AddAzureKeyVault(builder.Configuration["AzureKeyVault:Url"],
        builder.Configuration["AzureKeyVault:ClientId"],
        builder.Configuration["AzureKeyVault:SecretKey"]);
}

app.UseHttpsRedirection();

app.UseProblemDetails();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", context => context.Response.WriteAsync("CarManager API Gateway"));

app.Run();