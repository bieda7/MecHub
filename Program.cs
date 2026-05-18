using Microsoft.EntityFrameworkCore;
using MecHub.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using QuestPDF.Infrastructure;
using MecHub.Services;
using Microsoft.AspNetCore.HttpOverrides;

var supportedCultures = new[]
{
    new CultureInfo("pt-BR"),
    new CultureInfo("en-US")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

var builder = WebApplication.CreateBuilder(args);

// AUTH
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = "332036407856-q5a4k358e7ntlftp0frl181ek2f179oi.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

// EMAIL
// builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHttpClient<IEmailService, EmailService>();

// QUESTPDF
QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddScoped<OrdemServicoPdfService>();

// LOCALIZATION
builder.Services.AddLocalization(options =>
    options.ResourcesPath = "Resources");

// DATABASE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

// MVC
builder.Services.AddControllersWithViews();

// FORWARDED HEADERS - Railway/Proxy HTTPS
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto;

    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// FORWARDED HEADERS - precisa vir antes do HTTPS
app.UseForwardedHeaders();

// PIPELINE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();

app.UseAuthorization();

// ROTA PADRÃO
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();