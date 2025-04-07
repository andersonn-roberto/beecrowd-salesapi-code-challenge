using Microsoft.EntityFrameworkCore;
using SalesApi.Application;
using SalesApi.Application.Interfaces;
using SalesApi.Domain.Repositories;
using SalesApi.Infrastructure;
using SalesApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<DefaultContext>(
    options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("SalesApiDb"),
            b => b.MigrationsAssembly("SalesApi.Infrastructure")
        )
    );

builder.Services.AddScoped<DbContext>(provider => provider.GetService<DefaultContext>()!);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddTransient<SaleDiscountService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

await app.RunAsync();
