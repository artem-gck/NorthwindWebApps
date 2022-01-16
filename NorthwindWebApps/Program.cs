using Northwind.Services;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore;
using System.Data.SqlClient;
using Northwind.DataAccess;
using Northwind.Services.DataAccess;
using Northwind.DataAccess.SqlServer;
using Northwind.DataAccess.Employees;
using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.Blogging;
using Northwind.Services.EntityFrameworkCore.Blogging;
using Northwind.Services.EntityFrameworkCore.Blogging.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<NorthwindContext>(opt =>
//    opt.UseInMemoryDatabase("NorthwindList"));
builder.Services.AddDbContext<NorthwindContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<string[]>();

//builder.Services.AddScoped(service =>
//{
//    var connectToDbString = builder.Configuration.GetConnectionString("DefaultConnection");
//    var sqlConnection = new SqlConnection(connectToDbString);
//    sqlConnection.Open();
//    return sqlConnection;
//});
//builder.Services.AddTransient<NorthwindDataAccessFactory, SqlServerDataAccessFactory>();
//builder.Services.AddTransient<IProductManagementService, ProductManagementDataAccessService>();
//builder.Services.AddTransient<IProductCategoryManagementService, ProductCategoriesManagementDataAccessService>();
//builder.Services.AddTransient<IProductCategoryPicturesManagementService, ProductCategoryPicturesManagementDataAccessService>();
//builder.Services.AddTransient<IEmployeeManagementService, EmployeeManagementDataAccessService>();

builder.Services.AddTransient<IProductManagementService, ProductManagementService>();
builder.Services.AddTransient<IProductCategoryManagementService, ProductCategoryManagementService>();
builder.Services.AddTransient<IProductCategoryPicturesManagementService, ProductCategoryPicturesManagementService>();
builder.Services.AddTransient<IEmployeeManagementService, EmployeeManagementService>();
builder.Services.AddTransient<IBloggingService, BloggingService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
