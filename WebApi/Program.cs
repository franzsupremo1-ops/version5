using Application.Builders;
using Application.Mapping;
using Application.Strategies;
using Application.UseCases;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Decorators;
using Infrastructure.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });


});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infrastructure")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Repositories
builder.Services.AddScoped<IUsuario, UsuarioRepositorio>();
builder.Services.AddScoped<IAgua, AguaRepositorio>();
builder.Services.AddScoped<IReporte, ReporteRepositorio>();

// Casos de Uso
builder.Services.AddScoped<CrearUsuario>();
builder.Services.AddScoped<CrearAgua>();

//nbuilers
builder.Services.AddTransient<AguaBuilder>();
builder.Services.AddTransient<UsuarioBuilder>();
//docorator crt+alt+o
builder.Services.AddScoped<IAgua>(provider =>
{
    var context = provider.GetRequiredService<AppDbContext>();
    var repo = new AguaRepositorio(context);
    var logger = provider.GetRequiredService<ILogger<LoggingDecorator>>();
    return new LoggingDecorator(repo, logger);
});

builder.Services.AddScoped<IUsuario>(provider =>
{
    var context = provider.GetRequiredService<AppDbContext>();
    var repo = new UsuarioRepositorio(context);
    var logger = provider.GetRequiredService<ILogger<LoggingUsuarioDecorator>>();
    return new LoggingUsuarioDecorator(repo, logger);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
