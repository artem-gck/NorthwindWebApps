using Northwind.Services;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore;
using System.Data.SqlClient;
using Northwind.DataAccess;
using Northwind.Services.DataAccess;
using Northwind.DataAccess.SqlServer;
using Northwind.DataAccess.Employees;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<NorthwindContext>(opt =>
    opt.UseInMemoryDatabase("NorthwindList"));
builder.Services.AddScoped(service =>
{
    var connectToDbString = builder.Configuration.GetConnectionString("DefaultConnection");
    var sqlConnection = new SqlConnection(connectToDbString);
    sqlConnection.Open();
    return sqlConnection;
});
//builder.Services.AddTransient<NorthwindDataAccessFactory, SqlServerDataAccessFactory>();
//builder.Services.AddTransient<IProductManagementService, ProductManagementDataAccessService>();
//builder.Services.AddTransient<IProductCategoryManagementService, ProductCategoriesManagementDataAccessService>();
//builder.Services.AddTransient<IProductCategoryPicturesService, ProductCategoryPicturesManagementDataAccessService>();
//builder.Services.AddTransient<IEmployeeManagementService, EmployeeManagementDataAccessService>();

builder.Services.AddTransient<IProductManagementService, ProductManagementService>();
builder.Services.AddTransient<IProductCategoryManagementService, ProductCategoryManagementService>();
builder.Services.AddTransient<IProductCategoryPicturesService, ProductCategoryPicturesManagementService>();
builder.Services.AddTransient<IEmployeeManagementService, EmployeeManagementService>();

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
