using Microsoft.AspNetCore.Identity;
using WebApplication3.Model;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using ApplicationSecurity.Controllers;
using AspNetCore.ReCaptcha;
using ApplicationSecurity.Captcha;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    // Set max failed acces attempts for login before locking the user out
    opt.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddDataProtection();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password Settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 12;
    options.Password.RequiredUniqueChars = 0;

});

// Session Implementation
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache(); //save session in memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
});

// Redirect back to Login if unauthorized access
builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.ExpireTimeSpan = TimeSpan.FromSeconds(30);
    Config.LoginPath = "/Login";
    Config.SlidingExpiration = true;
});

// ReCaptcha
builder.Services.Configure<GoogleCaptchaConfig>(builder.Configuration.GetSection("GoogleReCaptcha"));
builder.Services.AddTransient(typeof(GoogleCaptchaService));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Error handling
app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// Using Session
app.UseSession();


app.MapRazorPages();

app.Run();
