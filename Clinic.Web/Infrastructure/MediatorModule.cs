namespace Clinic.Web.Infrastructure
{
    public class MediatorModule 
    {
        public static void Load(WebApplicationBuilder builder)
        {
            //builder.Services.AddScoped((typeof(IMediator).GetTypeInfo().Assembly)
            //    .AsImplementedInterfaces();

            //// Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            //// TODO: this loads only from given assembly
            //var coreAssembly = typeof(Clinic.Core.Appointments.Application.Commands.CategoryCommands.CreateCategoryCommand).GetTypeInfo().Assembly;
            //builder.RegisterAssemblyTypes(coreAssembly)
            //    .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //// Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            //builder.RegisterAssemblyTypes(coreAssembly)
            //    .AsClosedTypesOf(typeof(INotificationHandler<>));

            //// Register the Command's Validators (Validators based on FluentValidation library)
            //builder
            //    .RegisterAssemblyTypes(coreAssembly)
            //    .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            //    .AsImplementedInterfaces();

            //builder.Register<ServiceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            //});

            //builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //// TODO: Add every context's validator and transaction behaviours 
            //builder.RegisterGeneric(typeof(Patient.PatientValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(Patient.PatientTransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
    

        }
    }
}
