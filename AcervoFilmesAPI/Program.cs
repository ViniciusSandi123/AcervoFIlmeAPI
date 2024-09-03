using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.EntityFrameworkCore;
using AcervoFilmesAPI.Domain.Interfaces;
using AcervoFilmesAPI.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner
builder.Services.AddControllers();

// Configura��o do DbContext
builder.Services.AddDbContext<Context>(options =>
    //string de conex�o est� setado no appsettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Registro dos reposit�rios
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


// Configura��o do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura��o do pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Este m�todo � crucial para mapear controladores

app.Run();