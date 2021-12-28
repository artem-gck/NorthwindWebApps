using Northwind.Services;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<NorthwindContext>(opt =>
    opt.UseInMemoryDatabase("NorthwindList"));
builder.Services.AddTransient<IProductManagementService, ProductManagementService>();
builder.Services.AddTransient<IProductCategoryManagementService, ProductCategoryManagementService>();
builder.Services.AddTransient<IProductCategoryPicturesService, ProductCategoryPicturesService>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
