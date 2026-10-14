using _0_Framework.Application;
using CommentManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using AccountManagement.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost;
using ShopManagement.Configuration;
using _0_Framework.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

ShopManagementBootstrapper.Configure(builder.Services, ConnectionString);
DiscountManagementBootstrapper.Configure(builder.Services, ConnectionString);
InventoryManagementBootstrapper.Configure(builder.Services, ConnectionString);
BlogManagementBootstrapper.Configure(builder.Services, ConnectionString);
CommentManagementBootstrapper.Configure(builder.Services, ConnectionString);
AccountManagementBootstrapper.Configure(builder.Services, ConnectionString);

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminArea",builder => builder.RequireRole(new List<string> { Roles.Admin, Roles.ContentUploader }))
    .AddPolicy("Shop", builder => builder.RequireRole(new List<string> { Roles.Admin }))
    .AddPolicy("Inventory", builder => builder.RequireRole(new List<string> { Roles.Admin }))
    .AddPolicy("Discount", builder => builder.RequireRole(new List<string> { Roles.Admin }))
    .AddPolicy("Account", builder => builder.RequireRole(new List<string> { Roles.Admin }));

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "Inventory");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
