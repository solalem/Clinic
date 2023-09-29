using Solo.Appointments.Services;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Solo.Appointments
{
    public static class AppointmentsServicesConfiguration
    {
        public static IServiceCollection AddAppointmentsServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
       
            services.AddScoped<IAppointmentsServices, AppointmentsServices>();
            return services;
        }
    }
}
