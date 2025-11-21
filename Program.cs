using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ventas.Core.Interfaces;
using Ventas.Infraestructura.Data;
using Ventas.Infraestructura.Repositorio;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Ventas.Infraestructura.Data.VentasContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VentasContext") ?? throw new InvalidOperationException("Connection string 'VentasContext' not found.")));

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("myApp", polibuilder =>
    {
        polibuilder.AllowAnyOrigin();
        polibuilder.AllowAnyHeader();
        polibuilder.AllowAnyMethod();
    });

});

builder.WebHost.UseUrls("http://0.0.0.0:8080");


builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();
builder.Services.AddScoped<IDetallePedidoRepositorio, DetallePedidoRepositorio>();
builder.Services.AddScoped<IRutaRepositorio, RutaRepositorio>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    //app.UseSwagger();
    //app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("myApp");

app.MapControllers();

app.Run();
