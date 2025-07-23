using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Data;
using Repository;
using Repository.Interfaces;
using Services;
using Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Services added to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"))
);
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//builder.Services.AddSingleton<AuthenticationHelper>();
builder.Services.AddScoped<IAuthenticationUserService, AuthenticationUserService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IContentService, ContentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware para manejo de excepciones
app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exceptionMessage = exceptionHandlerPathFeature?.Error.Message;

        var errorResponse = new
        {
            Message = app.Environment.IsDevelopment()
                ? exceptionMessage // Desarrollo
                : "Error inesperado en el servidor. Contacte al soporte.",//Producci�n
            TraceId = context.TraceIdentifier
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

//Middleware para servir archivos estáticos (imágenes, videos, etc)
var contentFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "ContentUploaded/");
Console.WriteLine("🔸🔸🔸 -> Static file path: " + contentFolderPath);

if (!Directory.Exists(contentFolderPath))
    Directory.CreateDirectory(contentFolderPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ContentUploaded/")),
    RequestPath = "/media"
});


app.UseHttpsRedirection();
app.UseCors("NewPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
