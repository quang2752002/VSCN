var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register session and distributed memory cache if you're using sessions
builder.Services.AddSession(); // Add session service
builder.Services.AddDistributedMemoryCache(); // Add distributed memory cache



// Register IHttpContextAccessor to allow access to HttpContext in non-controller classes
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Now, build the app after all the services have been configured
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable session middleware
app.UseSession();

// Enable routing and authorization middleware
app.UseRouting();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "AdminArea",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();