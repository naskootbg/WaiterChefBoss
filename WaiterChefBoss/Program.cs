using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Middleware;
using WaiterChefBoss.ModelBinders;
using WaiterChefBoss.Services;
using WaiterChefBoss.Services.Category;
using WaiterChefBoss.Services.Product;
using WaiterChefBoss.Services.Review;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;

})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews(
    options =>
    {
        options.ModelBinderProviders.Insert(0, new DoubleModelBinderProvider());
    });

builder.Services.AddTransient<IBossService, BossService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IEditAddService, EditAddService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
 
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();
builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = "650052678393469";
        options.AppSecret = "fa35d015b2bab31696852c23cd8b752f";
    });
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseHsts();
}



app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");

app.UseResponseCaching();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use the custom logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
});
 



app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
