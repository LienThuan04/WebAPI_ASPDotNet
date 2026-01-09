using LearnASPDotNet.Settings;
using MongoDB.Driver;
using LearnASPDotNet.Middlewares;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using LearnASPDotNet.Sessions.Services;
using LearnASPDotNet.Users.Services;
using dotenv.net;



var builder = WebApplication.CreateBuilder(args);

//configure dotenv to load env variables from .env file
DotEnv.Load();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Auth API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
       Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat= "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }

    });
});

// Configure MongoDB settings
builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION")!;
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGO_DB")!;
});
// Register MongoDB client
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
//builder.Services.AddScoped<AuthApi.Services.UserService>(); // Register UserService
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

// Add JWT authentication
builder.Services.AddJwtAuthentication();

builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor

builder.Services.AddScoped<JwtService>(); // Register JwtService
builder.Services.AddScoped<UserService>(); // Register UserService
builder.Services.AddScoped<SessionService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Enable authentication middleware

app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map controller routes

app.Run();
