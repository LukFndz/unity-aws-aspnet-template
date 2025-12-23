using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Backend.Api.Configuration;
using Backend.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.Configure<AwsOptions>(builder.Configuration.GetSection(AwsOptions.SectionName));
builder.Services.Configure<UnityClientOptions>(builder.Configuration.GetSection(UnityClientOptions.SectionName));

// Controllers
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

// AWS SDK clients (extend as you add services)
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

// Dependency injection
builder.Services.AddSingleton<IUnityConfigService, UnityConfigService>();

// CORS for Unity (WebGL/editor)
builder.Services.AddCors(options =>
{
    options.AddPolicy("UnityClient", policy =>
    {
        policy
            .WithOrigins(builder.Configuration[$"{UnityClientOptions.SectionName}:AllowedOrigins"]?.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? Array.Empty<string>())
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Swagger (solo dev)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("UnityClient");
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
