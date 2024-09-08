using Clinic.Shared;

namespace Clinic.Core.Appointments.Domain
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class AppointmentsDomainException : DomainException
    {
        public AppointmentsDomainException(string code,
            string sourceModule,
            string sourceMethod,
            params object[] values) : base(code, sourceModule, sourceMethod, values)
        { }

        public AppointmentsDomainException(string code,
            string sourceModule,
            string sourceMethod,
            Exception innerException,
            params object[] values)
            : base(innerException, code, sourceModule, sourceMethod, values)
        { }

    }
}
