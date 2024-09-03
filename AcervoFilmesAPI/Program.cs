using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.EntityFrameworkCore;
using AcervoFilmesAPI.Domain.Interfaces;
using AcervoFilmesAPI.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllers();

// Configuração do DbContext
builder.Services.AddDbContext<Context>(options =>
    //string de conexão está setado no appsettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Registro dos repositórios
builder.Services.AddScoped<IGenero, GeneroRepository>();
builder.Services.AddScoped<IStreaming, StreamingRepository>();
builder.Services.AddScoped<IFilme, FilmeRepository>();
builder.Services.AddScoped<IAvaliacao, AvaliacaoRepository>();

//adicionando CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:7103")
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});


// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Este método é crucial para mapear controladores

app.Run();