using System;

namespace Solo.Appointments.Services
{
    public class AppointmentsServices : IAppointmentsServices
    {
        public String Name { get; set; }

        
        public IAppointmentService AppointmentService { get; set; }
        
        public IPatientService PatientService { get; set; }
        
        public IAttendanceService AttendanceService { get; set; }

        public AppointmentsServices(
            IAppointmentService AppointmentsService, 
            IPatientService PatientsService, 
            IAttendanceService AttendancesService, 
            String name = null)
        {
        
            AppointmentService = AppointmentsService;
        
            PatientService = PatientsService;
        
            AttendanceService = AttendancesService;
            Name = name;
        }

    }

    // Collection of services
    public interface IAppointmentsServices
    {
        
        public IAppointmentService AppointmentService { get; set; }
        
        public IPatientService PatientService { get; set; }
        
        public IAttendanceService AttendanceService { get; set; }

    }
}
