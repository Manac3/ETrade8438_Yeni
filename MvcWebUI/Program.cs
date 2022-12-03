using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Windows Authentication
//string connectionString = "server=.\\SQLEXPRESS;database=ETrade8438;trusted_connection=true;multipleactiveresultsets=true;trustservercertificate=true;";
// SQL Server Authentication
//string connectionString = "server=.\\SQLEXPRESS;database=ETrade8438;user id=sa;password=sa;multipleactiveresultsets=true;trustservercertificate=true;";
string connectionString = builder.Configuration.GetConnectionString("ETradeDb");
#region IoC Container (Inversion of Control Container)
builder.Services.AddDbContext<ETradeContext>(options => options.UseSqlServer(connectionString));



//builder singleton uygulama çalýþtýðý mühletçe newleme iþlemini bi kere yapar ve hep onu kullanýr alttaki scope ise newledikten sonra iþi bitince dispose eder siler
//builder.Services.AddSingleton<ProductRepoBase, ProductRepoBase>();


//biz bunu kullanýcaz
builder.Services.AddScoped<ProductRepoBase,ProductRepo>();


//bu trancient her enjeksiyonu gördüðü yerde newleme yapar objeyle iþi bitince dispose eder siler
//transient newler hafýzada bekler garbage collector zamaný geince siler
//builder.Services.AddTransient<ProductRepoBase, ProductRepo>();

builder.Services.AddScoped<IProductService, ProductService>();
#endregion


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
