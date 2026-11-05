using Blazorise;
using Clinic.Web.Areas.Appointments;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Clinic.Core.Appointments.Persistence;
using Microsoft.EntityFrameworkCore;
using Clinic.Web.Models;
using Clinic.Shared;
using Clinic.Web.Security;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddBlazorise(options =>
    {
        //options.ChangeTextOnKeyPress = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Clinic.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Clinic.Core.Appointments.Application.Patients.CreatePatientCommand>());

builder.Services.AddSingleton<PageHistoryState>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IClinicAccountService, ClinicAccountService>();

AppointmentsModule.Load(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<AppointmentsDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub().RequireAuthorization();
app.MapFallbackToPage("/_Host").RequireAuthorization();

app.Run();
