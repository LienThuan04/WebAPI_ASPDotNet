using LearnASPDotNet.Middlewares;
using LearnASPDotNet.Extensions.MongoDB;
using LearnASPDotNet.Extensions.JwtAuthentication;
using LearnASPDotNet.Extensions.Swaggers;
using dotenv.net;
using LearnASPDotNet.Features.Auths;
using LearnASPDotNet.Features.Users;
using LearnASPDotNet.Features.Sessions;

// Create a builder for the web application
var builder = WebApplication.CreateBuilder(args);

//configure dotenv to load env variables from .env file
DotEnv.Load();

// Add controller services
builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwagger(); // Add Swagger services in Folder Extensions/Swaggers/SwaggerServiceExtensions.cs

// Configure MongoDB settings in Folder Settings/MongoDbSettings.cs
builder.Services.AddMongoDb();

// Add JWT authentication in Folder Middlewares/JwtAuthentication.cs
builder.Services.AddJwtAuthentication();

builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor

builder.Services.AddAuthFeature();
builder.Services.AddUserFeature(); // Register User feature services
builder.Services.AddSessionFeature(); // Register Session feature services


var app = builder.Build(); // Build the application

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<MiddlewareException>(); // Custom middleware to handle JWT errors

app.UseHttpsRedirection();

app.UseAuthentication(); // Enable authentication middleware

app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map controller routes

app.Run();
