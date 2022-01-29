var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseStatusCodePages();

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