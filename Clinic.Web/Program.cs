using Blazorise;
using Clinic.Web.Areas.Appointments;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Clinic.Web.Components;

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
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddSingleton<PageHistoryState>();

AppointmentsApplicationModule.Load(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
