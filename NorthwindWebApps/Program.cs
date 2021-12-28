using Northwind.Services;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore;
using System.Data.SqlClient;
using Northwind.DataAccess;
using Northwind.Services.DataAccess;
using Northwind.DataAccess.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<NorthwindContext>(opt =>
    opt.UseInMemoryDatabase("NorthwindList"));
builder.Services.AddScoped(service =>
{
    var sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    sqlConnection.Open();
    return sqlConnection;
});
builder.Services.AddTransient<NorthwindDataAccessFactory, SqlServerDataAccessFactory>();
builder.Services.AddTransient<IProductManagementService, ProductManagementDataAccessService>();
builder.Services.AddTransient<IProductCategoryManagementService, ProductCategoriesManagementDataAccessService>();
builder.Services.AddTransient<IProductCategoryPicturesService, ProductCategoryPicturesManagementDataAccessService>();
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
