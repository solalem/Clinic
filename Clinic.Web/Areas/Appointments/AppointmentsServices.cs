using Clinic.Web.Areas.Appointments.Patients;
using System;

namespace Clinic.Web.Areas.Appointments
{
    public class AppointmentsServices : IAppointmentsServices
    {
        public String Name { get; set; }

        
        public IPatientService PatientService { get; set; }
        
        public AppointmentsServices(
            IPatientService PatientsService, 
            String name = null)
        {
        
            PatientService = PatientsService;
        
            Name = name;
        }

    }

    // Collection of services
    public interface IAppointmentsServices
    {
        public IPatientService PatientService { get; set; }

    }
}
