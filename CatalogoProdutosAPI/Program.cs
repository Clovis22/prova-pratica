using Microsoft.EntityFrameworkCore;
using CatalogoProdutosAPI.Data;
using CatalogoProdutosAPI.Repositories;

// Carrega o arquivo .env
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// No arquivo .env ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=catalogoProdutosDB;Username=postgres;Password=digitarsenha
// AddDbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// AddScoped
builder.Services.AddScoped<IDataContext>(provider =>
    provider.GetRequiredService<DataContext>());

// Repository
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
