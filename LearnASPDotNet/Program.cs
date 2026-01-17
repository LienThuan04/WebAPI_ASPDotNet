using LearnASPDotNet.Middlewares;
using LearnASPDotNet.Extensions.MongoDB;
using LearnASPDotNet.Extensions.JwtAuthentication;
using LearnASPDotNet.Extensions.Swaggers;
using LearnASPDotNet.Extensions.Supabase;
using dotenv.net;
using LearnASPDotNet.Features.Auths;
using LearnASPDotNet.Features.Users;
using LearnASPDotNet.Features.Sessions;
using LearnASPDotNet.Features.Files;

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

// Add Supabase services in Folder Extensions/Supabase/SupabaseServiceExtensions.cs
builder.Services.AddSupabase();

// Add JWT authentication in Folder Middlewares/JwtAuthentication.cs
builder.Services.AddJwtAuthentication();

builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor

builder.Services.AddAuthFeature();
builder.Services.AddUserFeature(); // Register User feature services
builder.Services.AddSessionFeature(); // Register Session feature services
builder.Services.AddFileFeature();


var app = builder.Build(); // Build the application

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "LearnASPDotNet API V1"); //Document title 
        options.ConfigObject.PersistAuthorization = true; // Persist authorization data
    });
}

app.UseMiddleware<MiddlewareException>(); // Custom middleware to handle JWT errors

app.UseHttpsRedirection();

app.UseAuthentication(); // Enable authentication middleware

app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map controller routes

app.Run();
