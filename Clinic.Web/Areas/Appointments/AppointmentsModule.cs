using Clinic.Core.Appointments.Persistence;
using Microsoft.EntityFrameworkCore;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Web.Areas.Appointments.Patients;
using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Web.Areas.Appointments.Visits;

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

            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IVisitRepository, VisitRepository>();

            // UI
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IVisitService, VisitService>();
            builder.Services.AddScoped<IAppointmentsServices, AppointmentsServices>();
        }
    }
}
