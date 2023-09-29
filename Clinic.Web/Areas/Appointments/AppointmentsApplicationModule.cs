using System;
using System.Reflection;
using MediatR;
using Clinic.Core.Appointments.Persistence;
using Microsoft.EntityFrameworkCore;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Web.Areas.Appointments.Patients;

namespace Clinic.Web.Areas.Appointments
{
    public static class AppointmentsApplicationModule
    {
        public static void Load(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppointmentsDbContext>(c =>
            {
                //var mediator = c.Resolve<IMediator>();

                c.UseSqlite(options =>
                    {
                        options.MigrationsAssembly(typeof(AppointmentsDbContext).GetTypeInfo().Assembly.GetName().Name);
                    });

            });

            builder.Services.AddScoped<IPatientRepository, PatientRepository>();

            // UI
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IAppointmentsServices, AppointmentsServices>();
        }
    }
}
