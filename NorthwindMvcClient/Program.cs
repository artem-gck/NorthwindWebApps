var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseStatusCodePages();
//app.UseHttpsRedirection();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("paginationOfEmployee",
        "Employees/Page{productPage}",
        new { Controller = "Employees", action = "Index" });

    endpoints.MapControllerRoute("paginationOfBlogArticles",
        "BlogArticles/Page{productPage}",
        new { Controller = "BlogArticles", action = "Index" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=BlogArticles}/{action=Index}/{id?}");
});

app.Run();