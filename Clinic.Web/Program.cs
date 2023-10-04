using Blazorise;
using Clinic.Web.Areas.Appointments;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Clinic.Core.Appointments.Persistence;
using Microsoft.EntityFrameworkCore;
using Clinic.Web.Models;

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
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Clinic.Core.Appointments.Application.Patients.CreatePatientCommand>());

builder.Services.AddSingleton<PageHistoryState>();

AppointmentsModule.Load(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetService<AppointmentsDbContext>();
        context.Database.Migrate();
    }
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
