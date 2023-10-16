using Clinic.Core.Appointments.Services;
using Clinic.Web.Areas.Appointments.Patients;
using System;

namespace Clinic.Web.Areas.Appointments
{
    public class AppointmentsServices : IAppointmentsServices
    {
        public String Name { get; set; }

        
        public IPatientService PatientService { get; set; }
        public IVisitService VisitService { get; set; }
        
        public AppointmentsServices(
            IPatientService PatientsService, 
            IVisitService visitService,
            String name = null)
        {
        
            PatientService = PatientsService;
            VisitService = visitService;
            Name = name;
        }

    }

    // Collection of services
    public interface IAppointmentsServices
    {
        public IPatientService PatientService { get; set; }
        public IVisitService VisitService { get; set; }

    }
}
