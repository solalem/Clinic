using Clinic.Core.Appointments.Persistence;
using Microsoft.EntityFrameworkCore;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.Web.Areas.Appointments.Patients;
using Clinic.Web.Areas.Appointments.Visits;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Core.Appointments.Domain.Visits;

namespace Clinic.Web.Areas.Appointments
{
    public static class AppointmentsModule
    {
        public static void Load(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppointmentsDbContext>(c =>
            {
                c.UseSqlite(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddDbContext<AppointmentsQueryDbContext>(c =>
            {
                c.UseSqlite(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IVisitRepository, VisitRepository>();

            // UI
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IVisitService, VisitService>();
            builder.Services.AddScoped<IAppointmentsServices, AppointmentsServices>();
        }
    }
}
