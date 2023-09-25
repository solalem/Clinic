using System;
using System.Reflection;
using MediatR;
using Clinic.Core.Appointments.Domain.AggregatesModel;
using Clinic.Core.Appointments.Persistence;
using Clinic.Application.Abstractions.Requests;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Web.Appointments
{
    public static class AppointmentsApplicationModule
    {
        public static void Load(WebApplicationBuilder builder)
        {
            RegisterTypes(builder);

            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var mediator = c.Resolve<IMediator>();

                var options = new DbContextOptionsBuilder<AppointmentsDbContext>();
                options.UseSqlServer(QueriesConnectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(AppointmentsDbContext).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });

                return new AppointmentsDbContext(options.Options, mediator);
            }).InstancePerLifetimeScope();

            builder.Services.AddScoped<IRequestManager, AppointmentsRequestManager>();
        }
        
        private static void RegisterTypes(WebApplicationBuilder builder)
        {
            builder.RegisterType<PatientRepository>()
                .As<IPatientRepository>()
                .InstancePerLifetimeScope();

        }
    }
}
